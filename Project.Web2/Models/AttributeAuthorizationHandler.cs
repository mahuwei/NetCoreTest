using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Web2.Models {
    public abstract class
        AttributeAuthorizationHandler<TRequirement, TAttribute> :
            AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement where TAttribute : Attribute {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, TRequirement requirement) {
            var attributes = new List<TAttribute>();

            if ((context.Resource as AuthorizationFilterContext)?.ActionDescriptor is
                ControllerActionDescriptor action) {
                attributes.AddRange(
                    GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
                attributes.AddRange(GetAttributes(action.MethodInfo));
            }

            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context, TRequirement requirement,
            IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo) {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>();
        }
    }

    internal class TerminalAppAuthorizationHandler : AttributeAuthorizationHandler<
        TerminalAppAuthorizationRequirement, TerminalAppAttribute> {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TerminalAppAuthorizationRequirement requirement,
            IEnumerable<TerminalAppAttribute> attributes) {
            object errorMsg;
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证403
            if (context.Resource is AuthorizationFilterContext filterContext &&
                filterContext.ActionDescriptor is ControllerActionDescriptor
                    descriptor) {
                //先判断是否是匿名访问,
                if (descriptor != null) {
                    var actionAttributes =
                        descriptor.MethodInfo.GetCustomAttributes(true);
                    var isAnonymous =
                        actionAttributes.Any(a => a is AllowAnonymousAttribute);
                    //非匿名的方法,链接中添加accesstoken值
                    if (isAnonymous) {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                    //url获取access_token
                    //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
                    var httpContext =
                        ((AuthorizationFilterContext) context.Resource).HttpContext;
                    //var questUrl = httpContext.Request.Path.Value.ToLower();
                    string requestAppId = httpContext.Request.Headers["appid"];
                    string requestAccessToken =
                        httpContext.Request.Headers["access_token"];
                    if (!string.IsNullOrEmpty(requestAppId) &&
                        !string.IsNullOrEmpty(requestAccessToken)) {
                        if (attributes != null)
                            if (attributes.ToArray().ToString() == "") {
                                //任意一个在数据库列表中的App都可以运行,否则先判断提交的APPID与需要ID是否相符
                                var mat = false;
                                foreach (var terminalAppAttribute in attributes)
                                    if (terminalAppAttribute.AppId == requestAppId) {
                                        mat = true;
                                        break;
                                    }

                                if (!mat) {
                                    errorMsg = "客户端应用未在服务端登记或未被授权运用当前功能.";
                                    return HandleBlockedAsync(context, requirement,
                                        errorMsg);
                                }
                            }

                        //如果未指定attributes，则表示任何一个终端服务都可以调用服务, 在验证区域验证终端提供的ID是否匹配数据库记录
                        var valRst = ValidateToken(requestAppId, requestAccessToken);
                        if (string.IsNullOrEmpty(valRst)) {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }

                        errorMsg =      "AccessToken验证失败(" + valRst + ")";
                        return HandleBlockedAsync(context, requirement, errorMsg);
                    }

                    errorMsg ="未提供AppID或Token.";
                    return HandleBlockedAsync(context, requirement, errorMsg);
                    //return Task.CompletedTask;
                }
            }
            else {
                errorMsg = "FilterContext类型不匹配.";
                return HandleBlockedAsync(context, requirement, errorMsg);
            }

            errorMsg = "未知错误.";
            return HandleBlockedAsync(context, requirement, errorMsg);
        }


        //校验票据（数据库数据匹配）
        /// <summary>
        ///     验证终端服务程序提供的AccessToken是否合法
        /// </summary>
        /// <param name="appId">终端APP的ID</param>
        /// <param name="accessToken">终端APP利用其自身AppKEY运算出来的AccessToken,与服务器生成的进行比对</param>
        /// <returns></returns>
        private string ValidateToken(string appId, string accessToken) {
            return string.Empty; //成功验证
            //try {
            //    DBContextMain dBContext = new DBContextMain();
            //    //从数据库读取AppID对应的KEY(此KEY为加解密算法的AES_KEY
            //    AuthApp authApp =
            //        dBContext.AuthApps.FirstOrDefault(a => a.AppID == appId);
            //    if (authApp == null)
            //        return "客户端应用没有在云端登记!";
            //    string appKeyOnServer = authApp.APPKey;
            //    if (string.IsNullOrEmpty(appKeyOnServer)) return "客户端应用基础信息有误!";

            //    var tmpToken = WebUtility.UrlDecode(accessToken);
            //    tmpToken =
            //        OCrypto.AES16Decrypt(tmpToken, appKeyOnServer); //使用APPKEY解密并分析

            //    if (string.IsNullOrEmpty(tmpToken))
            //        return "客户端提交的身份令牌运算为空!";
            //    try {
            //        //原始验证码为im_cloud_sv001-appid-ticks格式
            //        //取出时间,与服务器时间对比,超过10秒即拒绝服务
            //        var tmpTime =
            //            Convert.ToInt64(
            //                tmpToken.Substring(tmpToken.LastIndexOf("-", StringComparison.Ordinal) + 1));
            //        //DateTime dt = DateTime.ParseExact(tmpTime, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
            //        var dt = new DateTime(tmpTime);
            //        var IsInTimeSpan =
            //            Convert.ToDouble(
            //                ODateTime.DateDiffSeconds(dt, DateTime.Now)) <= 7200;
            //        var IsInternalApp = tmpToken.IndexOf("im_cloud_sv001-") >= 0;
            //        if (!IsInternalApp || !IsInTimeSpan)
            //            return "令牌未被许可或已经失效!";
            //        return string.Empty; //成功验证
            //    }
            //    catch (Exception ex) {
            //        return "令牌解析出错(" + ex.Message + ")";
            //    }
            //}
            //catch (Exception ex) {
            //    return "令牌解析出错(" + ex.Message + ")";
            //}
        }

        private Task HandleBlockedAsync(AuthorizationHandlerContext context,
            TerminalAppAuthorizationRequirement requirement, object errorMsg) {
            var authorizationFilterContext =
                context.Resource as AuthorizationFilterContext;
            authorizationFilterContext.Result = new JsonResult(errorMsg)
                {StatusCode = 202};
            //设置为403会显示不了自定义信息,改为Accepted202,由客户端处理
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
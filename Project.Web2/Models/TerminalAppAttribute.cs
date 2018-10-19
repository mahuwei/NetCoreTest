using System;
using Microsoft.AspNetCore.Authorization;

namespace Project.Web2.Models {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =
        true)]
    internal class TerminalAppAttribute : AuthorizeAttribute {
        /// <summary>
        ///     指定客户端访问API
        /// </summary>
        /// <param name="appId"></param>
        public TerminalAppAttribute(string appId = "") : base("TerminalApp") {
            AppId = appId;
        }

        public string AppId { get; }
    }
}
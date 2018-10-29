using System;
using System.Security.Cryptography;
using System.Text;

namespace Project.Signatures {
    public class RsaHelper {
        public RsaHelper() {
            //初始化时生成公钥和私钥
            var provider = new RSACryptoServiceProvider(1024);
            PrivateKey = provider.ToXmlStringEx(true); //生成私钥
            PublicKey = provider.ToXmlStringEx(false); //生成公钥      
        }

        public string PrivateKey { get; set; }

        public string PublicKey { get; set; }

        /// <summary>
        ///     生成公钥、私钥文件
        /// </summary>
        /// <param name="PrivateKeyPath">私钥文件保存路径，包含文件名</param>
        /// <param name="PublicKeyPath">公钥文件保存路径，包含文件名</param>
        public void RsaKey() {
            var provider = new RSACryptoServiceProvider();
            provider.ToXmlString(true); //生成私钥文件
            provider.ToXmlString(false); //生成公钥文件
        }

        /// <summary>
        ///     签名
        /// </summary>
        /// <param name="str">需签名的数据</param>
        /// <returns>签名后的值</returns>
        public string Sign(string str) {
            //根据需要加签时的哈希算法转化成对应的hash字符节
            var bt = Encoding.GetEncoding("utf-8").GetBytes(str);
            var sha256 = new SHA256CryptoServiceProvider();
            var rgbHash = sha256.ComputeHash(bt);

            var key = new RSACryptoServiceProvider();
            key.FromXmlStringEx(PrivateKey);
            var formatter = new RSAPKCS1SignatureFormatter(key);
            formatter
                .SetHashAlgorithm(
                    "SHA256"); //此处是你需要加签的hash算法，需要和上边你计算的hash值的算法一致，不然会报错。
            var inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        ///     签名验证
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <param name="sign">加签之后的字符串</param>
        /// <returns>签名是否符合</returns>
        public bool SignCheck(string str, string sign) {
            try {
                var bt = Encoding.GetEncoding("utf-8").GetBytes(str);
                var sha256 = new SHA256CryptoServiceProvider();
                var rgbHash = sha256.ComputeHash(bt);

                var key = new RSACryptoServiceProvider();
                key.FromXmlStringEx(PublicKey);
                var deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("SHA256");
                var rgbSignature = Convert.FromBase64String(sign);
                if (deformatter.VerifySignature(rgbHash, rgbSignature)) return true;
                return false;
            }
            catch {
                return false;
            }
        }
    }
}
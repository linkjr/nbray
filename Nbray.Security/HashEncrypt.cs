using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;

namespace Nbray.Security
{
    /// <summary>
    /// 表示Hash加密类。
    /// </summary>
    public class HashEncrypt
    {
        /// <summary>
        /// 加密字符串文本
        /// </summary>
        /// <param name="plaintext">要加密的字符串</param>
        /// <param name="passwordFormat">加密方式</param>
        /// <returns></returns>
        [Obsolete]
        private static string Encrypt(string plaintext, FormsAuthPasswordFormat passwordFormat)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(plaintext, passwordFormat.ToString());
        }

        /// <summary>
        /// 根据MD5加密字符串文本
        /// </summary>
        /// <param name="plaintext">要加密的字符串</param>
        /// <returns></returns>
        [Obsolete]
        public static string MD5Encrypt(string plaintext)
        {
            return Encrypt(plaintext, FormsAuthPasswordFormat.MD5);
        }

        /// <summary>
        /// MD5哈希加密字符串。
        /// </summary>
        /// <param name="inputText">要加密的文本。</param>
        /// <returns>返回MD5加密后的哈希值。</returns>
        public static string MD5Hash(string inputText)
        {
            //var algorithm = MD5.Create();
            var algorithm = new MD5CryptoServiceProvider();
            var bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            var hexString = BitConverter.ToString(algorithm.Hash).Replace("-", string.Empty).ToUpper();
            return hexString;
        }

        /// <summary>
        /// SHA1哈希加密字符串。
        /// </summary>
        /// <param name="inputText">要加密的文本。</param>
        /// <returns>返回SHA1加密后的哈希值。</returns>
        public static string SHA1Hash(string inputText)
        {
            //var algorithm = SHA1.Create();
            var algorithm = new SHA1CryptoServiceProvider();
            var bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputText));
            var hexString = BitConverter.ToString(algorithm.Hash).Replace("-", string.Empty).ToUpper();
            return hexString;
        }
    }
}

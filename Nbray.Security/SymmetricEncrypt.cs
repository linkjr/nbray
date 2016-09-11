using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Security
{
    /// <summary>
    /// 对称加密。
    /// </summary>
    public class SymmetricEncrypt
    {
        //AES、DES、RC2
        //常用的对称加密有：
        //DES、IDEA、RC2、RC4、SKIPJACK、RC5、AES算法等

        /// <summary>
        /// DES算法加密。
        /// </summary>
        /// <param name="plaintext">要加密的明文。</param>
        /// <param name="desKey">加密密钥。</param>
        /// <returns></returns>
        public static string DESEncrypt(string plaintext, string desKey)
        {
            var des = new DESCryptoServiceProvider
            {

                Key = ASCIIEncoding.ASCII.GetBytes(desKey),
                IV = ASCIIEncoding.ASCII.GetBytes(desKey)
            };
            var bytes = Encoding.Default.GetBytes(plaintext);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();

                    var sbEncrypt = new StringBuilder();
                    ms.ToArray()
                        .ToList()
                        .ForEach(m => sbEncrypt.AppendFormat("{0:X2}", m));
                    return sbEncrypt.ToString();
                }
            }
        }

        /// <summary>
        /// DES算法解密。
        /// </summary>
        /// <param name="ciphertext">要解密的密文。</param>
        /// <param name="desKey">DES解密密钥。</param>
        /// <returns></returns>
        public static string DESDecrypt(string ciphertext, string desKey)
        {
            var bytes = new byte[ciphertext.Length / 2];
            for (int x = 0; x < ciphertext.Length / 2; x++)
            {
                int i = (Convert.ToInt32(ciphertext.Substring(x * 2, 2), 16));
                bytes[x] = (byte)i;
            }
            var des = new DESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(desKey),
                IV = ASCIIEncoding.ASCII.GetBytes(desKey)
            };
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }
        }
    }
}

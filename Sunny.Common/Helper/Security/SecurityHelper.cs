using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sunny.Common.Helper
{
    ///<summary>安全策略</summary>
    public static class SecurityHelper
    {
        private static DESCryptoServiceProvider descryptoServiceProvider = null;
        static SecurityHelper()
        {
            descryptoServiceProvider = new DESCryptoServiceProvider();
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="key">用于加解密的key</param>
        /// <returns>加密后的字符串</returns>
        public static string DesEncrypt(string input, string key)
        {
            try
            {
                byte[] inputByteArray;
                inputByteArray = Encoding.Default.GetBytes(input);
                descryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));
                descryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in memoryStream.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("给字符串" + input + "加密时出现异常:" + ex);
            }

        }

        /// <summary>
        /// 解密DES字符串
        /// </summary>
        /// <param name="DESString">DES字符串</param>
        /// <param name="key">用于加解密的key</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(string DESString, string key)
        {


            try
            {
                int len = DESString.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(DESString.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                descryptoServiceProvider.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));
                descryptoServiceProvider.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("解密DES字符串" + DESString + "时出现异常:" + ex);
            }

        }

        /// <summary>
        /// 以MD5的方式加密字符串
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>加密后的MD5格式字符串</returns>
        public static string MD5Encrypt(string input)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    var strResult = BitConverter.ToString(result);
                    return strResult.Replace("-", "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将字符串加密成MD5格式时,出现异常:" + ex);
            }

        }


        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string SHA1Encrypt(string input)
        {

            try
            {
                byte[] StrRes = Encoding.Default.GetBytes(input);
                HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
                StrRes = iSHA.ComputeHash(StrRes);
                StringBuilder EnText = new StringBuilder();
                foreach (byte iByte in StrRes)
                {
                    EnText.AppendFormat("{0:x2}", iByte);
                }
                return EnText.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("将字符串加密成SHA1格式时,出现异常:" + ex);
            }

        }
    }
}

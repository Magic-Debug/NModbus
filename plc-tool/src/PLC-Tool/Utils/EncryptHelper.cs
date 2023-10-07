using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    /// <summary>
    /// 加密/解密
    /// </summary>
    public class EncryptHelper
    {
        private static readonly string encryptKey = "LthS"; //定义密钥  

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key">24位长度的Key，使用英文字符和数字，不能用中文</param>
        /// <returns></returns>
        public static string Encrypt3DES(string value, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                while (key.Length < 24)
                {
                    key += key;
                }
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 24));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(value);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            else
            {
                throw new Exception("Key值不能为空，请传入24长度的非中文字符串");
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string value, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                while (key.Length < 24)
                {
                    key += key;
                }
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 24));
                DES.Mode = CipherMode.ECB;
                DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                string result = "";
                try
                {
                    byte[] Buffer = Convert.FromBase64String(value);
                    result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
                catch (Exception e)
                {
                    throw e;
                }
                return result;
            }
            else
            {
                throw new Exception("Key值不能为空，请传入24长度的非中文字符串");
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt3DES(byte[] value, string key)
        {
            while (key.Length < 24)
            {
                key += key;
            }

            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 24));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            return DESEncrypt.TransformFinalBlock(value, 0, value.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Decrypt3DES(byte[] value, string key)
        {
            while (key.Length < 24)
            {
                key += key;
            }
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 24));
            DES.Mode = CipherMode.ECB;
            DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            byte[] result = null;
            try
            {
                result = DESDecrypt.TransformFinalBlock(value, 0, value.Length);
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string str)
        {
            var descsp = new DESCryptoServiceProvider(); //实例化加/解密类对象   
            var key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
            var data = Encoding.Unicode.GetBytes(str); //定义字节数组，用来存储要加密的字符串 
            using (var stream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(stream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length); //向加密流中写入数据      
                    cryptoStream.FlushFinalBlock(); //释放加密流     
                }
                return Convert.ToBase64String(stream.ToArray()); //返回加密后的字符串  
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string str)
        {
            var descsp = new DESCryptoServiceProvider(); //实例化加/解密类对象    
            var key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
            var data = Convert.FromBase64String(str); //定义字节数组，用来存储要解密的字符串  
            using (var stream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(stream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length); //向解密流中写入数据     
                    cryptoStream.FlushFinalBlock(); //释放解密流      
                }
                return Encoding.Unicode.GetString(stream.ToArray()); //返回解密后的字符串  
            }
        }

        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="plaintext">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        public static string MD5Encrypt(string plaintext, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(plaintext));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
            if (bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }
    }
}

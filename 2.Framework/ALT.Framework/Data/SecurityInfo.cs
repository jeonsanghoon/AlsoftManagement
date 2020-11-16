using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Security.Application;
using System.IO;

namespace ALT.Framework.Data
{
    #region >> 보안 관련 클래스
    /// <summary>
    /// 보안 관련 클래스
    /// </summary>

    public class SecurityInfo
    {
        public string getGetSafeHtml(string val)
        {
            return Sanitizer.GetSafeHtml(val);
        }
        /// <summary>
        /// SQL Injection
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string getSqlInjectIon(string str)
        {
            if (str == null) return string.Empty;
            str = str.Replace("'", "''");
            /*str = str.Replace(";", "");
            str = str.Replace("--", "");
            str = str.Replace("#", "");
            str = str.Replace("\\", "");
            str = str.Replace("&", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("=", "");
            str = str.Replace("+", "");*/
            return str;
        }
        /// <summary>
        /// 윈도우에서 SHA1을 사용할 경우
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt_SHA1(string data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 패스워드 암호화
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt_PW(string data)
        {
            return Encrypt_SHA256(data);
        }

        /// <summary>
        /// 데이터암호화
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt_data(string data)
        {
            return Encrypt_AES256(data);
        }

        /// <summary>
        /// 데이터복호화
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Decrypt_data(string data)
        {
            return Decrypt_AES256 (data);
        }

        /// <summary>
        /// 윈도우에서 SHA256을 사용할 경우
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt_SHA256(string data)
        {
            
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string Encrypt_SHA512(string Data)

        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {

                stringBuilder.AppendFormat("{0:X2}", b);

            }

            return stringBuilder.ToString();

        }


        /// <summary>
        /// 윈도우에서 MD5를 사용
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt_MD5(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        public String Encrypt_AES256(String val)
        {
            return Encrypt_AES256(val, Global.ConfigInfo.AesKey);
        }
        //AES_256 암호화
        public String Encrypt_AES256(String val, String key)
        {
            if (string.IsNullOrEmpty(val)) return "";
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key.PadLeft(32,'0'));
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(val);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }


        //
        /// <summary>
        /// AES_256 복호화
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// 
        public String Decrypt_AES256(string val)
        {
            return Decrypt_AES256(val, Global.ConfigInfo.AesKey);
        }

        public String Decrypt_AES256(String val, String key)
        {
            if (string.IsNullOrEmpty(val)) return "";
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key.PadLeft(32, '0'));
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(val);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }


        /// <summary>
        /// AES_128 암호화
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public String AESEncrypt128(String val, String key)
        {

            if (string.IsNullOrEmpty(val)) return "";
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(val);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        
        /// <summary>
        /// AES_128 복호화
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public String AESDecrypt128(String val, String key)
        {
            if (string.IsNullOrEmpty(val)) return "";

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(val);
            byte[] Salt = Encoding.ASCII.GetBytes(key.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(key, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
    }
   #endregion
}

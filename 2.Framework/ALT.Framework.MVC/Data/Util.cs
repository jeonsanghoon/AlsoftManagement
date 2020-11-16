using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Web.Mvc;
using System.Text.RegularExpressions;

using System.Web.Security;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;

namespace ALT.Framework.Mvc.Data
{
    public class Util
    {
        #region >> 쿠키 저장 및 파기
        public void SetCookie(string cookieName, string cookieValue, bool bAuto = false)
        {
            string msg = string.Empty;
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);

                if (bAuto)
                    cookie.Expires = DateTime.Now.AddYears(1);
                else
                    cookie.Expires = DateTime.Now.AddHours(20);

                cookie.Value = HttpUtility.UrlEncode(cookieValue);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex) { msg = ex.Message; }
        }
        public string getCookie(string cookieName)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[cookieName] == null)
                    return string.Empty;
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public T getLoginInfo<T>(ControllerContext controller, string key)
        {
            return JsonConvert.DeserializeObject<T>(this.getCookie(key));
        }

        public void RemoveCookie(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(CookieName))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        #endregion

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
            return Decrypt_AES256(data);
        }
        #region >> 단방향 암호화
        [Obsolete]
        public string Encrypt_SHA(string sValue)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, "SHA1");
        }

        public string Encrypt_SHA256(string sValue)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(sValue));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        public string Encrypt_SHA512(string sValue)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(sValue));

            StringBuilder stringBuilder = new StringBuilder(128);
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        [Obsolete]
        public string Encrypt_MD5(string sValue)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, "MD5");
        }

        [Obsolete]
        public string Encrypt(string sValue, string type)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, type);
        }
        #endregion


        public  string Base64Encoding(string EncodingText, System.Text.Encoding oEncoding = null)
        {
            if (oEncoding == null)
                oEncoding = System.Text.Encoding.UTF8;

            byte[] arr = oEncoding.GetBytes(EncodingText);
            return System.Convert.ToBase64String(arr);
        }

        public  string Base64Decoding(string DecodingText, System.Text.Encoding oEncoding = null)
        {
            if (oEncoding == null)
                oEncoding = System.Text.Encoding.UTF8;

            byte[] arr = System.Convert.FromBase64String(DecodingText);
            return oEncoding.GetString(arr);
        }

        public String Encrypt_AES256(String val)
        {
            return Encrypt_AES256(val, Global.ConfigInfo.AesKey);
        }
        //AES_256 암호화
        public String Encrypt_AES256(String val, String key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key.PadLeft(32, '0'));
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
        public String AESEncrypt128(String Input, String key)
        {

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(Input);
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
        public String AESDecrypt128(String Input, String key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(Input);
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

        #region >> 브라우저체크 후 리다이렉트
        /// <summary>
        /// 브라우저체크 후 리다이렉트
        /// </summary>
        /// <param name="redirectUtl">리다이렉트 경로</param>
        /// <param name="ChkUrl">체크 URL</param>
        public void BrowerCheckRedirect( string redirectUtl, params string[] ChkUrl)
        {
            
            Regex regex = new Regex(@"iPhone|iPod|iPad|Android|Windows CE|BlackBerry|Symbian|Windows Phone|webOS|Opera Mini|Opera Mobi|POLARIS|IEMobile|lgtelecom|nokia|SonyEricsson|LG|SAMSUNG", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(HttpContext.Current.Request.UserAgent))
            {
                foreach ( string sUrl in ChkUrl)
                {
                    if (HttpContext.Current.Request.Url.AbsoluteUri == sUrl)
                    {
                        HttpContext.Current.Response.Redirect(redirectUtl, true);
                    }
                }
            }
        }
        #endregion
    }
}


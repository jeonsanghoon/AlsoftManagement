using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace ALT.Framework.Data
{
    /// <summary>
    /// 윈도우즈용 웹서비스 호출
    /// </summary>
    public class WebServiceInWin
    {
        public T GetAPIServer<T>(string url, object Model, string method = "POST")
        {
            

            if (method == "GET")
            {
                url =  url + "?" + string.Join("&", Model.GetType().GetProperties().Select(p => p.Name + "=" + p.GetValue(Model, null)));
            }
            
            
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.ContentType = "application/json";
            request.Method = method;

            if (method == "POST")
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Model));
                request.ContentLength = bytes.Length;

                Stream data = request.GetRequestStream();
                data.Write(bytes, 0, bytes.Length);
                data.Close();
            }
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string message = reader.ReadToEnd();

                    var responseReuslt = JsonConvert.DeserializeObject<T>(message);
                    return responseReuslt;
                }
            }
        }
    }
}

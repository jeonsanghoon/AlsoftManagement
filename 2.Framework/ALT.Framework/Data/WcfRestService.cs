using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;

using Newtonsoft.Json;
namespace ALT.Framework.Data
{
    public class WcfRestService
    {
        

        /// <summary>
        /// Rest 방식 단일 객체 호출일 경우
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>저장일 경우 Result값이 있을 경우 에러</returns>
        public  T GetRestStringService<T>(object param, string url, string method = "POST", Boolean isKakao = false) //method :  GET, POST
        {
            
            string sJson = string.Empty;
            if (param.GetType().Name.ToUpper() != "STRING")
                sJson = JsonConvert.SerializeObject(param);
            else
                sJson = param.ToString();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, sJson);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";

			if (isKakao)
			{
				webClient.UseDefaultCredentials = true;
				webClient.Credentials = new NetworkCredential("KakaoAK", "f14891693c4c1e9ed1ab2195e941c3dd");
			}
			webClient.Encoding = Encoding.UTF8;
			string sVal = "";
			if (method.ToUpper() == "POST")
			{
				sVal = webClient.UploadString(url, method, data);
			}
			else
				sVal = webClient.DownloadString(url);
			return JsonConvert.DeserializeObject<T>(sVal);
        }


		/// <summary>
		/// Rest 방식 단일 객체 호출일 경우
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="param"></param>
		/// <param name="url"></param>
		/// <param name="method"></param>
		/// <returns>저장일 경우 Result값이 있을 경우 에러</returns>
		public T GetRestGetService<T>(string url, Boolean isKakao = false) //method :  GET, POST
		{
			return GetRestStringService<T>("", url, "GET", isKakao);
		}
	}
}

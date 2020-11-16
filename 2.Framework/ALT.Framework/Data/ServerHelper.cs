using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;

namespace ALT.Framework.Data
{
    public static class ServerHelper
    {
        //public static string ServerUrl = "http://192.168.15.38:49916/UPRetailCloudService.svc";

        /// <summary>
        /// Rest 방식 단일 객체 호출일 경우
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>저장일 경우 Result값이 있을 경우 에러</returns>
        public static string GetRestService<T>(T param, string url, string method = "POST") //method :  GET, POST
        {


            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, param);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string sVal = webClient.UploadString(url, method, data);


            return sVal;
        }

        /// <summary>
        /// Rest 방식 배열 객체 호출일 경우
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetRestListService<T>(IList<T> param, string url, string method = "POST") //method :  GET, POST
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, param);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string sVal = webClient.UploadString(url, method, data);


            return sVal;
        }
    }
}

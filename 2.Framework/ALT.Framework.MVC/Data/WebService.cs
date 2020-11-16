using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace ALT.Framework.Mvc.Data
{
    public class WebService
    {
        string baseURL = string.Empty;
        private  HttpClient CreateHttpClientDBManager()
        {
            
            /*if (GlobalMvc.Host.ToLower().Contains("localhost")) baseURL = System.Configuration.ConfigurationManager.AppSettings["LocalDBManagerWebAPI"];
            else if (GlobalMvc.Host.ToLower().Contains("192.168.15.38")) baseURL = System.Configuration.ConfigurationManager.AppSettings["TestDBManagerWebAPI"];
            else baseURL = System.Configuration.ConfigurationManager.AppSettings["DBManagerWebAPI"];*/
           // baseURL = System.Configuration.ConfigurationManager.AppSettings["DBManagerWebAPI"];
            HttpClient webClient = new HttpClient();
         //   webClient.BaseAddress = new Uri(baseURL);
            webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            webClient.Timeout = new TimeSpan(0, 1, 30);
            return webClient;
        }


        public T GetAPIServer<T>(string url, Boolean isKakao = false)
        {

            HttpClient client = CreateHttpClientDBManager();
            if (isKakao)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", Global.ConfigInfo.KakaoRestKey);
            }
            HttpResponseMessage response = client.GetAsync(baseURL + url).Result;
           
            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            T Data = response.Content.ReadAsAsync<T>().Result;
            return Data;
        }

        public T GetPostAPIServer<T>(string url, object Model, Boolean isKakao = false)
        {
            HttpResponseMessage response;

            HttpClient client = CreateHttpClientDBManager();
            if (isKakao)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("KakaoAK", Global.ConfigInfo.KakaoRestKey);
            }

            response = client.PostAsJsonAsync(baseURL + url, Model).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

            if (!response.IsSuccessStatusCode) throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            return response.Content.ReadAsAsync<T>().Result;

        }
    }
}

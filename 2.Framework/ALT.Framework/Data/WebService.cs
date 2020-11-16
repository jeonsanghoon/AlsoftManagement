using log4net;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ALT.Framework.Data
{
    /// <summary>
    /// 웹에서 호출할 경우에만 사용
    /// </summary>
    public class WebService
    {


        public ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string baseURL = string.Empty;
        private string Host
        {
            get { return System.Web.HttpContext.Current.Request.Url.Scheme + "" + "://" + System.Web.HttpContext.Current.Request.Url.Authority; }
        }

      
        private  HttpClient CreateHttpClientDBManager()
        {

            //if (Host.ToLower().Contains("localhost")) baseURL = System.Configuration.ConfigurationManager.AppSettings["LocalDBManagerWebAPI"];
            //else if (Host.ToLower().Contains("192.168.15.38")) baseURL = System.Configuration.ConfigurationManager.AppSettings["TestDBManagerWebAPI"];
            //else
                
                baseURL = System.Configuration.ConfigurationManager.AppSettings["DBManagerWebAPI"];

            HttpClient webClient = new HttpClient();
            webClient.BaseAddress = new Uri(baseURL);
            webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            webClient.Timeout = new TimeSpan(0, 1, 30);
            return webClient;
        }


        public  T GetAPIServer<T>(string url)
        {

            HttpClient client = CreateHttpClientDBManager();
            HttpResponseMessage response = client.GetAsync(baseURL + url).Result;

            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            T Data = response.Content.ReadAsAsync<T>().Result;
            return Data;
        }

        public T GetPostAPIServer<T>(string url, object Model)
        {
            HttpClient client = CreateHttpClientDBManager();


            HttpResponseMessage response = client.PostAsJsonAsync(baseURL + url, Model).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            return response.Content.ReadAsAsync<T>().Result;

        }





        string baseSyncURL = string.Empty;


        private HttpClient CreateHttpClientDBManager_TabSync()
        {
            
            baseSyncURL = System.Configuration.ConfigurationManager.AppSettings["SyncApi"];

            HttpClient webClient = new HttpClient();
            webClient.BaseAddress = new Uri(baseSyncURL);
            webClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            webClient.Timeout = new TimeSpan(0, 1, 30);
            return webClient;
        }


        public T GetPostSyncAPIServer<T>(string url)
        {

            HttpClient client = CreateHttpClientDBManager_TabSync();
            HttpResponseMessage response = client.GetAsync(baseSyncURL + url).Result;

            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            T Data = response.Content.ReadAsAsync<T>().Result;
            return Data;
        }


        public T GetPostSyncAPIServer<T>(string url, object Model ,string company_code)
        {
            HttpClient client = CreateHttpClientDBManager_TabSync();

            client.DefaultRequestHeaders.Add("CompanyID", company_code);
            logger.Debug(baseSyncURL + url);
            logger.Debug("Headers  "+ client.DefaultRequestHeaders.ToString());
            HttpResponseMessage response = client.PostAsJsonAsync(baseSyncURL + url, Model).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

          
            if (!response.IsSuccessStatusCode)
                throw new Exception(string.Format("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase));

            // Parse the response body. Blocking!
            return response.Content.ReadAsAsync<T>().Result;

        }




    }
}

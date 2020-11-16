using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using ALT.Framework;

namespace ALT.Framework.Mvc.Helpers
{
    public enum Method
    {
        GET,
        POST
    }
    public class SMSHelper
    {
        private WebRequest request;
        private Stream dataStream;
        private string status;
        private string URL = string.Empty;

        

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public SMSHelper()
        {
        }
        public SMSHelper(string url)
        {
            // Create a request using a URL that can receive a post.
            URL = url;
            request = WebRequest.Create(url);
        }

        public SMSHelper(string url, Method method)
            : this(url)
        {
            request.Method = (method == Method.GET) ? Method.GET.ToString() : Method.POST.ToString();
        }

        public SMSHelper(string url, Method method, string data)
            : this(url, method)
        {
            this.SetHttpRequest(data);
        }

        public void SetHttpRequest(string data)
        {
            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            if (request == null)
                request = WebRequest.Create(URL);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();
        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}

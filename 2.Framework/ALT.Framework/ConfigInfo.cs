using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALT.Framework
{
    public partial class ConfigInfo
    {
        public string DefaultDBSource
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["DefaultDBSource"]; }
        }

        public string SqlXmlPath
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SqlXmlPath"]; }
        }

        public string FtpUser
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FtpUser"]; }
        }

        public string FtpPw
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FtpPw"]; }
        }

        public string FtpDomain
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FtpDomain"]; }
        }

        public string FileUser
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FileUser"]; }
        }
        public string LoginSessionName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["LoginSessionName"]; }
        }
        public string DefaultCultureName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["DefaultCultureName"]; }
        }

        public string KakaoAppKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["KakaoAppKey"]; }
        }
        public string KakaoRestKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["KakaoRestKey"]; }
        }
        
        public string DaumApiKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["DaumApiKey"]; }
        }

        public string AesKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["AesKey"]; }
        }
        public string DaumMapScript
        {
            get { return "DaumMapScript"; }
        }
        public string KakaoMapScript
        {
            get { return "KakaoMapScript"; }
        }

        public string DaumPostScript
        {
            get { return "DaumPostScript"; }
        }

        public string GoogleAppKey
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["GoogleAppKey"]; }
        }
        public string FacebookAppKey { get { return System.Configuration.ConfigurationManager.AppSettings["FacebookAppKey"]; } }
        //public string RoundCalc
        //{
        //    get { return System.Configuration.ConfigurationManager.AppSettings["RoundCalc"]; }
        //}

        public decimal dRoundCal
        {
            get { return (int)Math.Pow(10, Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["RoundCalc"])); }
        }

        public string UploadUrl
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["UploadUrl"]; }
        }

        public string HOSTING_SITE
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["HOSTING_SITE"]; }
        }

        public string ProjectTitle { get { return System.Configuration.ConfigurationManager.AppSettings["ProjectTitle"]; } }
        public string FaceBookAppID
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["FaceBookAppID"]; }
        }
        public int PROJECT_SITE { get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PROJECT_SITE"]); } }


        public string MANAGEMENT_SITE { get { return System.Configuration.ConfigurationManager.AppSettings["MANAGEMENT_SITE"]; } }

        public string MONGODB_HOST { get { return System.Configuration.ConfigurationManager.AppSettings["MONGODB_HOST"]; } }
        public string MONGODB_PORT { get { return System.Configuration.ConfigurationManager.AppSettings["MONGODB_PORT"]; } }
        public string MONGODB_USERNAME { get { return System.Configuration.ConfigurationManager.AppSettings["MONGODB_USERNAME"]; } }
        public string MONGODB_PASSWORD { get { return System.Configuration.ConfigurationManager.AppSettings["MONGODB_PASSWORD"]; } }
        public string MONGODB_DATABASE { get { return System.Configuration.ConfigurationManager.AppSettings["MONGODB_DATABASE"]; } }
    }
}

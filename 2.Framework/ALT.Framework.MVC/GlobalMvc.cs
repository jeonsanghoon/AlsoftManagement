using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALT.Framework.Mvc
{
    public class GlobalMvc
    {
        static Common _Common;

        public static Common Common
        {
            get
            {
                if (_Common == null)
                {
                    _Common = new Common();
                }
                return _Common;
            }
        }


        static Data.Util _Util;
        public static Data.Util Util
        {
            get
            {
                if (_Util == null)
                {
                    _Util = new Data.Util();
                }
                return _Util;
            }
        }

        static Data.WebService _WebService;
        public static Data.WebService WebService
        {
            get
            {
                if (_WebService == null)
                {
                    _WebService = new Data.WebService();
                }
                return _WebService;
            }
        }
        public static string Host
        {
            get { return System.Web.HttpContext.Current.Request.Url.Scheme + "" + "://" + System.Web.HttpContext.Current.Request.Url.Authority ; }
        }

        static Helpers.SMSHelper _smsHelper;
        public static Helpers.SMSHelper SMSHelper
        {
            get
            {
                if (_smsHelper == null)
                {
                    _smsHelper = new Helpers.SMSHelper();
                }
                return _smsHelper;
            }
        }


        static ALT.Framework.Mvc.Helpers.FileInfo _fileInfo;
        public static Helpers.FileInfo FileInfo
        {
            get
            {
                if (_fileInfo == null)
                {
                    _fileInfo = new Helpers.FileInfo();
                }
                return _fileInfo;
            }
        }
    }
}

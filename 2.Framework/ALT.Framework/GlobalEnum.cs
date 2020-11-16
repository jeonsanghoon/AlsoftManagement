using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALT.Framework
{
    public class GlobalEnum
    {
        public enum DBAgentSQLType
        {
            FixedSQL = 0,
            DynamicSQL
        }

        public enum LogType
        {
            Info = 0,
            Debug,
            Warn,
            Error,
            Fatal
        }

        public enum DateFormat
        {
            yyyy, yyyyMM, yyyyMMdd, yyyyMMddKor

        }

        public enum LIST_GUBUN
        {
            NORMAL, EASYUI_GRID
        }

        public enum Encrypt
        {
            SHA1, MD5, SHA256, SHA384, SHA512
        }

        /// <summary>
        /// 언어 구분
        /// </summary>
        public enum Language
        {
            Korean, English, Arabic, German, Spanish, French, Italian, Japanese, Chinese
        }


       /* public enum ServiceName
        {
            BaseService = "LH"
            ,CommonService = ""

        }*/
    }
}

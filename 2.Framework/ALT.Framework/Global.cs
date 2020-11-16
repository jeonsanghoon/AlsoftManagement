using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ALT.Framework.Data;
using ALT.Framework.DataBase;


namespace ALT.Framework 
{
    /// <summary>
    /// -------------------------------------------------------------------------------------------------------------
    /// 클래스이름	:	Global
    /// 설	   명	:	글로벌 변수 및 클래스를 관리하는 클래스
    /// 작  성  자	:	전상훈
    /// 최초작성일	:	2013년 05월 15일
    /// 최종수정일   :  2013년 05월 15일
    /// ------------------------------------------------------------------------------------------------------------- 
    /// 수정  내역	:	
    ///					
    /// </summary>
    public static class Global
    {

        static ConfigInfo _ConfigInfo;

        /// <summary>
        /// 환경정보 가져오기
        /// </summary>
        public static ConfigInfo ConfigInfo
        {
            get
            {
                if (_ConfigInfo == null)
                {
                    _ConfigInfo = new ConfigInfo();
                }
                return _ConfigInfo;
            }
        }

        static DBAgent _DBAgent;

        /// <summary>
        /// 데이터베이스 정보 가져오기
        /// </summary>
        public static DBAgent DBAgent
        {
            get
            {
                if (_DBAgent == null)
                {
                    _DBAgent = new DBAgent();
                }
                return _DBAgent;
            }
        }


        /// <summary>
        /// 데이터
        /// </summary>
        static Format _Format;
        public static Format Format
        {
            get
            {
                if (_Format == null)
                {
                    _Format = new Format();
                }
                return _Format;
            }
        }

        /// <summary>
        /// 파일정보
        /// </summary>
        static FileInformation _FileInformationl;
        public static FileInformation FileInformation
        {
            get
            {
                if (_FileInformationl == null)
                {
                    _FileInformationl = new FileInformation();
                }
                return _FileInformationl;
            }
        }

        private static NetInfo _netInfo;
        public static NetInfo NetInfo
        {
            get
            {
                if (_netInfo == null)
                {
                    _netInfo = new NetInfo();
                }
                return _netInfo;
            }
        }

        private static SecurityInfo _securityInfo;
        public static SecurityInfo SecurityInfo
        {
            get
            {
                if (_securityInfo == null)
                {
                    _securityInfo = new SecurityInfo();
                }
                return _securityInfo;
            }
        }

        private static WcfRestService _wcfRestService;
        public static WcfRestService WcfRestService
        {
            get
            {
                if (_wcfRestService == null)
                {
                    _wcfRestService = new WcfRestService();
                }
                return _wcfRestService;
            }
        }


        private static WebServiceInWin _WebServiceInWin;
        public static WebServiceInWin WebServiceInWin
        {
            get
            {
                if (_WebServiceInWin == null)
                {
                    _WebServiceInWin = new WebServiceInWin();
                }
                return _WebServiceInWin;
            }
        }

      
    }
}

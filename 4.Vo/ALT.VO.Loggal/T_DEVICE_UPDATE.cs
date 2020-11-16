using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 로컬박스별 업데이트상태 테이블(T_DEVICE_UPDATE)
    /// <summary>       
    /// 로컬박스별 업데이트상태 테이블(T_DEVICE_UPDATE)
    /// </summary>	   
    public class T_DEVICE_UPDATE
    {
        /// <summary>       
        /// 테이블명
        /// </summary>	   
        public string TABLE_NAME { get; set; }
        /// <summary>       
        /// 로컬박스고유번호
        /// </summary>	   
        public string DEVICE_NUMBER { get; set; }

        public long? DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 관련 API명
        /// </summary>	   
        public string API_NAME { get; set; }
        /// <summary>       
        /// 최종업데이트수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 최종업데이트시간
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 수신요청한시간
        /// </summary>
        public DateTime? RECEIVE_DATE { get; set; }
    }
    #endregion >> 로컬박스별 업데이트상태 테이블(T_DEVICE_UPDATE) END 


    public class DEVICE_INFO_COND
    {
        public long? DEVICE_CODE { get; set; }
        public string TABLE_NAME { get; set; }
        public string DEVICE_NUMBER { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }

    public class RTN_DEVICE_INFO_DATA
    {
        public long? DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public List<LOGGAL_MAIN_CONTENTLIST> MAIN_LIST { get; set; }
        public List<LOGGAL_AD_DATA> AD_LIST { get; set; }
    }
}

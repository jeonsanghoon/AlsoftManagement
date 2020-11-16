using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    #region >> 공유테이블(배너/로컬박스)(T_SHARE)조회조건
    /// <summary>       
    /// 공유테이블(배너/로컬박스)(T_SHARE)
    /// </summary>	   
    public class T_SHARE_COND
    {
        /// <summary> 
        /// 페이지당 건수 (기본 20건)
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 선택된 페이지 기본 1
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 조회순서
        /// </summary>
        public string SORT_ORDER { get; set; }
        /// <summary>
        /// 1 : 배너공유정보 2:로컬박스공유정보
        /// </summary>
        public string GUBUN { get; set; }
        /// <summary>       
        /// 공유코드(순번)
        /// </summary>	   
        public int? SHARE_CODE { get; set; }
        /// <summary>       
        /// 공유보낸자
        /// </summary>	   
        public int? SEND_MEMBER_CODE { get; set; }
        /// <summary>
        /// 공유보낸자 이름
        /// </summary>
        public string SEND_MEMBER_NAME { get; set; }
        /// <summary>       
        /// 공유받은자
        /// </summary>	   
        public int? RECEIVE_MEMBER_CODE { get; set; }
        /// <summary>
        /// 받은사람 이름
        /// </summary>
        public string RECEIVE_MEMBER_NAME { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 배너제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 로컬박스코드(T_DEVICE)
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 공유확인여부
        /// </summary>	   
        public bool? IS_VIEW { get; set; }
  
        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool? HIDE { get; set; }
    }

    #endregion >> 공유테이블(배너/로컬박스)(T_SHARE)조회조건 END 

    #region >> 공유테이블(배너/로컬박스)(T_SHARE)
    /// <summary>       
    /// 공유테이블(배너/로컬박스)(T_SHARE)
    /// </summary>	   
    public class T_SHARE
    {
        /// <summary>       
        /// 공유코드(순번)
        /// </summary>	   
        public long SHARE_CODE { get; set; }
        /// <summary>       
        /// 공유보낸자
        /// </summary>	   
        public int? SEND_MEMBER_CODE { get; set; }
        /// <summary>
        /// 공유보낸자 이름
        /// </summary>
        public string SEND_MEMBER_NAME { get; set; }

        /// <summary>
        /// 공유 받은자 이름(콤마로 구분, T_SHARE_DTL 테이블의 공유 받은 사람)
        /// </summary>
        public string RECEIVE_MEMBER_NAMES { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 로컬박스코드(T_DEVICE)
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 코멘트
        /// </summary>	   
        public string COMMENT { get; set; }

        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER 테이블 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER 테이블 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
        public string SORT { get; set; }

        public List<T_SHARE_DTL> detaillist { get; set; }
    }
    #endregion >> 공유테이블(배너/로컬박스)(T_SHARE) END 
    #region >> 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL)조회조건
    /// <summary>       
    /// 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL)
    /// </summary>	   
    public class T_SHARE_DTL_COND
    {
        /// <summary> 
        /// 페이지당 건수 (기본 20건)
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 선택된 페이지 기본 1
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 조회순서
        /// </summary>
        public string SORT_ORDER { get; set; }
    }

    #endregion >> 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL)조회조건 END 

    #region >> 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL)
    /// <summary>       
    /// 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL)
    /// </summary>	   
    public class T_SHARE_DTL
    {
        /// <summary>       
        /// 공유코드(순번)
        /// </summary>	   
        public long SHARE_CODE { get; set; }
        /// <summary>       
        /// 공유받은자
        /// </summary>	   
        public int RECEIVE_MEMBER_CODE { get; set; }
        /// <summary>       
        /// 공유확인여부
        /// </summary>	   
        public bool? IS_VIEW { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER 테이블 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER 테이블 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
   
    }
    #endregion >> 공유 상세테이블(배너/로컬박스) (T_SHARE_DTL) END 
}

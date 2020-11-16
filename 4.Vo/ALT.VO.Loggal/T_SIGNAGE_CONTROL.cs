using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 사이니지 제어테이블(T_SIGNAGE_CONTROL)조회조건
    /// <summary>       
    /// 사이니지 제어테이블(T_SIGNAGE_CONTROL)
    /// </summary>	   
    public class T_SIGNAGE_CONTROL_COND
    {
        /// <summary>
        /// SIGN_CODE와 같음
        /// </summary>
        public Int64? ID { get; set; }
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
        /// 순번(기본키)
        /// </summary>	   
        public Int64? IDX { get; set; }
        private Int64? _signCode;
        /// <summary>       
        /// 사이니지테이블(T_SIGNAGE)의 SIGN_CODE
        /// </summary>	   
        public Int64? SIGN_CODE { get { return ID == null ? _signCode : ID; } set { _signCode = value; } }
        /// <summary>
        /// 사이니지명
        /// </summary>
        public string SIGN_NAME { get; set; }
        /// <summary>       
        /// 재생유형(T_COMMON : A010)
        /// </summary>	   
        public int? PLAY_TYPE { get; set; }
        /// <summary>       
        /// 요청시간(FR)
        /// </summary>	   
        public string FR_PLAY_REQ_TIME { get; set; }
        /// <summary>       
        /// 요청시간(TO)
        /// </summary>	   
        public string TO_PLAY_REQ_TIME { get; set; }
        /// <summary>
        /// 완료여부
        /// </summary>
        public bool? IS_COMPLEATED { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }

        /// <summary>
        /// 사용자아이디
        /// </summary>
        public string USER_ID { get; set; }
    }

    #endregion >> 사이니지 제어테이블(T_SIGNAGE_CONTROL)조회조건 END 

    public class T_SIGNAGE_CONTROL_UPDATE
    {
        public Int64 IDX { get; set; }
        public string PLAY_FR_TIME { get; set; }
        public string PLAY_TO_TIME { get; set; }
        public int UPDATE_CODE { get; set; }

    }

    #region >> 사이니지 제어테이블(T_SIGNAGE_CONTROL)
    /// <summary>       
    /// 사이니지 제어테이블(T_SIGNAGE_CONTROL)
    /// </summary>	   
    public class T_SIGNAGE_CONTROL
    {
        
        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>       
        /// 사이니지테이블(T_SIGNAGE)의 SIGN_CODE
        /// </summary>	   
        public Int64? SIGN_CODE { get; set; }
        /// <summary>
        /// 사이니지명
        /// </summary>
        public string SIGN_NAME { get; set; }
        /// <summary>       
        /// 재생유형(T_COMMON : A010)
        /// </summary>	   
        public int? PLAY_TYPE { get; set; } = 1;
        /// <summary>       
        /// 요청시간
        /// </summary>	   
        public DateTime? PLAY_REQ_TIME { get; set; }
        /// <summary>       
        /// 시작시간
        /// </summary>	   
        public DateTime? PLAY_FR_TIME { get; set; }
        /// <summary>       
        /// 종료시간
        /// </summary>	   
        public DateTime? PLAY_TO_TIME { get; set; }
        /// <summary>       
        /// 콘텐츠
        /// </summary>	   
        public string CONTENT_URL { get; set; }
        /// <summary>       
        /// 내용
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>       
        /// 요청자
        /// </summary>	   
        public string REQUEST_NAME { get; set; }
        /// <summary>       
        /// 요청자이메일
        /// </summary>	   
        public string REQUEST_EMAIL { get; set; }
        /// <summary>
        /// 재생완료일자(시간)
        /// </summary>
        public DateTime? COMPLEATED_DATE { get; set; }
        /// <summary>       
        /// 숨김여부 1:숨김
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자코드
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일자
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자코드
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일자
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
        public string SORT { get; set; }
    }
    #endregion >> 사이니지 제어테이블(T_SIGNAGE_CONTROL) END 

}

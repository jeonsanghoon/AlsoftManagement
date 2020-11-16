using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 로컬사인배너 연결 테이블(T_AD_SIGNINFO_SIGNAGE)

    public class T_AD_SIGNINFO_SIGNAGE_COND
    {

        /// <summary>       
        /// 순번(기본키)
        /// </summary>	
        public int? IDX { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블의 AD_CODE)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>       
        /// 로컬사인기기테이블(T_SIGNAGE의 SIGN_CODE)
        /// </summary>	   
        public Int64? SIGN_CODE { get; set; }
        /// <summary>       
        /// 배너 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool? IS_VERTICAL { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int? PLAY_TIME { get; set; }
        /// <summary>       
        /// 유형 1:이미지 2:동영상 T_COMMON :A010
        /// </summary>	   
        public int? SIGN_TYPE { get; set; }
        /// <summary>       
        /// 0:사용 1:미사용 T_COMMON테이블의 U004
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>
        /// 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 정렬 순서
        /// </summary>
        public string SORT_ORDER { get; set; }
        
    }

    public class T_AD_SIGNINFO_SIGNAGE_SAVE
    {
        public long SIGN_CODE { get; set; }
        public int REG_CODE { get; set; }
        public List<T_AD_SIGNINFO_SIGNAGE> list { get; set; }
    }
    /// <summary>       
    /// 로컬사인배너 연결 테이블(T_AD_SIGNINFO_SIGNAGE)
    /// </summary>	   
    public class T_AD_SIGNINFO_SIGNAGE
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>
        /// 조회 순번
        /// </summary>
        public long? ROW_IDX { get; set; }
        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블의 AD_CODE)
        /// </summary>	   
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 배너제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool IS_VERTICAL { get; set; }
        /// <summary>       
        /// 세로여부명
        /// </summary>	
        public string IS_VERTICAL_NAME { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int PLAY_TIME { get; set; }
        /// <summary>
        /// 실행시간
        /// </summary>
        public string PLAY_TIME_NAME { get; set; }
        /// <summary>       
        /// 유형 1:이미지 2:동영상 T_COMMON :A010
        /// </summary>	   
        public int SIGN_TYPE { get; set; }
        /// <summary>
        /// 유형명
        /// </summary>
        public string SIGN_TYPE_NAME { get; set; }
        /// <summary>
        /// SING_TYPE => (1 이미지 주소, 2 동영상 주소 3:유튜브 Video ID
        /// </summary>
        public string CONTENT_URL { get; set; }
        /// <summary>       
        /// 로컬사인기기테이블(T_SIGNAGE의 SIGN_CODE)
        /// </summary>	   
        public Int64? SIGN_CODE { get; set; }
        /// <summary>
        /// 시작일자(yyyyMMdd)
        /// </summary>
        public string FR_DATE { get; set; }
        /// <summary>
        /// 종료일자(yyyyMMdd)
        /// </summary>
        public string TO_DATE { get; set; }
        /// <summary>
        /// 시작시간(HH:mm)
        /// </summary>
        public string FR_TIME { get; set; }
        /// <summary>
        /// 종료시간(HH:mm)
        /// </summary>
        public string TO_TIME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 0:사용 1:미사용 T_COMMON테이블의 U004
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        /// <summary>
        /// 총조회건수
        /// </summary>
        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 로컬사인배너 연결 테이블(T_AD_SIGNINFO_SIGNAGE) END 

}

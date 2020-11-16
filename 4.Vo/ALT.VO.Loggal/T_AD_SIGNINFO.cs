using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 로컬사인용 배너 테이블(T_AD_SIGNINFO)

    public class T_AD_SIGNINFO_COND
    {
        /// <summary>
        /// 순번(일련번호) 기본키
        /// </summary>
        public long? IDX { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 배너코드
        /// </summary>	   
        public long? AD_CODE { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool? IS_VERTICAL { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public int? PUBLIC_TYPE { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public string PUBLIC_TYPE_NAME { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int? PLAY_TIME { get; set; }
        /// <summary>       
        /// 유형 1:이미지 2:동영상 T_COMMON :A010
        /// </summary>	   
        public int? SIGN_TYPE { get; set; }
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
        /// 해당 사이니지에 있는 콘텐츠 제외
        /// </summary>
        public long? EXT_SIGN_CODE { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }
        /// <summary>
        /// 현재 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당건수
        /// </summary>
        public int? PAGE_SIZE { get; set; } 
        /// <summary>
        /// 정렬순서
        /// </summary>
        public string SORT { get; set; }
    }
    /// <summary>       
    /// 로컬사인용 배너 테이블(T_AD_SIGNINFO)
    /// </summary>	   
    public class T_AD_SIGNINFO
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>
        /// 순번(일련번호) 기본키
        /// </summary>
        public long? IDX { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 배너코드
        /// </summary>	   
        public long? AD_CODE { get; set; }
        /// <summary>       
        /// 순번
        /// </summary>	   
        public int SEQ { get; set; }
        /// <summary>       
        /// 0:사용 1:미사용
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool IS_VERTICAL { get; set; }
        /// <summary>
        /// 세로여부
        /// </summary>
        public string IS_VERTICAL_NAME { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public int? PUBLIC_TYPE { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public string PUBLIC_TYPE_NAME { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int PLAY_TIME { get; set; }
        public string PLAY_TIME_NAME { get; set; }
        /// <summary>       
        /// 유형 1:이미지 2:동영상 T_COMMON :A010
        /// </summary>	   
        public int SIGN_TYPE { get; set; }
        /// <summary>
        /// 유형 1:이미지 2:동영상 T_COMMON :A010
        /// </summary>
        public string SIGN_TYPE_NAME { get; set; }
        /// <summary>                                     
        /// 내용 URL 정보
        /// </summary>	   
        public string CONTENT_URL { get; set; }
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
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 총조회건수
        /// </summary>
        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 로컬사인용 배너 테이블(T_AD_SIGNINFO) END 

}

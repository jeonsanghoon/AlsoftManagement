using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 로컬박스 메인 Display(T_DEVICE_MAIN)
    /// <summary>       
    /// 로컬박스 메인 Display(T_DEVICE_MAIN)
    /// </summary>	   
    public class T_DEVICE_MAIN
    {
        /// <summary>
        /// N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 로컬박스코드 T_DEVICE 테이블 참조
        /// </summary>	   
        public Int64 DEVICE_CODE { get; set; }
        /// <summary>
        /// 그룹순번 T_DEVICE_MAIN_GROUP(DEVICE_CODE, GROUP_SEQ) Relationships
        /// </summary>
        public int? GROUP_SEQ { get; set; }
        /// <summary>
        /// 최초배너가 만들어진 로컬박스 코드
        /// </summary>
        public Int64 ORI_DEVICE_CODE { get; set; }
        /// <summary>
        /// 최초배너가 만들어진 로컬박스 명
        /// </summary>
        public string ORI_DEVICE_NAME { get; set; }
        /// <summary>       
        /// Display 순번
        /// </summary>	   
        public int SEQ { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005)
        /// </summary>
        public int? BANNER_TYPE { get; set; }

        public string BANNER_TYPE_NAME { get; set; }
        /// <summary>       
        /// 콘텐츠유형 0:이미지 1:Html, T_COMMON : A004
        /// </summary>	   
        public int? CONTENT_TYPE { get; set; }

        /// <summary>
        /// 시작일자(배너노출 - YYYYMMDD)
        /// </summary>
        public string FR_DATE { get; set; }
        /// <summary>
        /// 종료일자(배너노출 - YYYYMMDD)
        /// </summary>
        public string TO_DATE { get; set; }
        /// <summary>
        /// 시작시간(배너노출 - HH:MM)
        /// </summary>
        public string FR_TIME { get; set; }
        /// <summary>
        /// 종료시간(배너노출 - HH:MM)
        /// </summary>
        public string TO_TIME { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public int? PUBLIC_TYPE { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public string PUBLIC_TYPE_NAME { get; set; }
        /// <summary>
        /// 공유승인상태(T_COMMON : A009) 0:원본 1:요청 8:반려 9:승인 11:취소
        /// </summary>
        public int? SHARE_STATUS { get; set; }
        /// <summary>
        /// 공유된 갯수
        /// </summary>
        public int? SHARE_CNT { get; set; }
        /// <summary>       
        /// CONTENT 광고정보(이미지 또는 Html)
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>
        /// 메인클릭시 상세내용(HTML)
        /// </summary>
        public string CONTENT_DETAIL { get; set; }
        /// <summary>       
        /// 부가정보1
        /// </summary>	   
        public string REF_DATA1 { get; set; }
        /// <summary>       
        /// 부가정보2
        /// </summary>	   
        public string REF_DATA2 { get; set; }
        /// <summary>       
        /// 부가정보3
        /// </summary>	   
        public string REF_DATA3 { get; set; }
        /// <summary>       
        /// 부가정보4
        /// </summary>	   
        public string REF_DATA4 { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }

        /// <summary>
        /// 광고코드 (T_AD 테이블의 AD_CODE)
        /// </summary>
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 광고시작시간
        /// </summary>
        public string AD_FR_DATE { get; set; }
        /// <summary>
        /// 광고종료시간
        /// </summary>
        public string AD_TO_DATE { get; set; }

        public int? AD_TYPE { get; set; }
        /// <summary>       
        /// 숨김여부(0:보임 1:숨김)
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>
        /// 모바일배너 게시여부
        /// </summary>
        public bool AD_HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 승인업체코드 T_AD_DEIVCE 테이블, T_COMPANY 테이블 참조
        /// </summary>
        public int? APPROVAL_COMPANY_CODE { get; set; }

        #region >> T_DEVICE_MAIN_GROUP 정보
        /// <summary>       
        /// 화면분할(T_COMMON => MAIN_CODE: H002)
        /// </summary>	   
        public int? FRAME_TYPE { get; set; }
        /// <summary>
        /// 화면분할갯수 명칭(T_COMMON => MAIN_CODE: H002)
        /// </summary>
        public string FRAME_TYPE_NAME { get; set; }
        /// <summary>
        /// 콘텐츠유형 1:이미지 2:동영상 3:유튜브 T_COMMON :A010
        /// </summary>
        public int? CONTENT_TYPE2 { get; set; }
        /// <summary>
        /// 콘텐츠유형
        /// </summary>
        public string CONTENT_TYPE_NAME { get; set; }
        /// <summary>
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>
        public int? PLAY_TIME { get; set; }
        /// <summary>
        /// 실행시간
        /// </summary>
        public string PLAY_TIME_NAME { get; set; }
		#endregion

    }
    #endregion >> 로컬박스 메인 Display(T_DEVICE_MAIN) END 

    public class T_DEVICE_MAIN_COND
    {
        /// <summary>       
        /// 로컬박스코드 T_DEVICE 테이블 참조
        /// </summary>	   
        public Int64 DEVICE_CODE { get; set; }
        public int? GROUP_SEQ { get; set; }
        /// <summary>       
        /// Display 순번
        /// </summary>	   
        public int? SEQ { get; set; }
        /// <summary>
        /// 공개유형 0:비공개 1:내부공개(기기만) 2:외부공개(모바일)
        /// </summary>
        public int? PUBLIC_TYPE { get; set; }
        
        /// <summary>       
        /// 숨김여부(0:보임 1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
    }

    public class T_DEVICE_MAIN_SEQ_CHANGE
    {
        /// <summary>       
        /// 로컬박스코드 T_DEVICE 테이블 참조
        /// </summary>	   
        public Int64 DEVICE_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? PRE_SEQ { get; set; }
        /// <summary>       
        /// Display 순번
        /// </summary>	   
        public int? SEQ { get; set; }
        public int? REG_CODE { get; set; }
    }
    public class DEVICE_MAIN_SHARE_COND
    {
        public int? id { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        public long? DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public long? AD_CODE { get; set; }
        public string TITLE { get; set; }
        public string REP_CATEGORY_CODE { get; set; }
        public int? SHARE_STATUS { get; set; }
        public int? COMPANY_CODE { get; set; }
        public int? MNG_COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int? SHARE_COMPANY_CODE { get; set; }
        public int? AD_STORE_CODE { get; set; }
        public int? DEVICE_STORE_CODE { get; set; }
        public string GUBUN { get; set; }
    }
    public class DEVICE_MAIN_SHARE_LIST
    {
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
  
        public int SEQ { get; set; }
        public long AD_CODE { get; set; }
        public string LOGO_URL { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
     
        public DateTime UPDATE_DATE { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int UPDATE_CODE { get; set; }
        public string UPDATE_NAME { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public bool USE_YN { get; set; }
        public string SHARE_STATUS_NAME { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }

    public class DEVICE_MAIN_SHARE_REQ_SAVE
    {
        public int TARGET_DEVICE_CODE { get; set; }
        public int REG_CODE { get; set; }
        public List<DEVICE_MAIN_SHARE_REQ_SAVE_DETAIL> list { get; set; }
    }
    public class DEVICE_MAIN_SHARE_REQ_SAVE_DETAIL
    {
        public long DEVICE_CODE { get; set; }
        public long AD_CODE { get; set; }
        
    }

    public class DEVICE_MAIN_SHARE_APPROVAL_LIST
    {
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public long? MNG_DEVICE_CODE { get; set; }
        public string MNG_DEVICE_NAME { get; set; }
        public int? SEQ { get; set; }
        public string TITLE { get; set; }
        public string LOGO_URL { get; set; }
        public int BANNER_TYPE { get; set; }
        public string BANNER_TYPE_NAME { get; set; }
        public int CONTENT_TYPE { get; set; }
        public string CONTENT_TYPE_NAME { get; set; }
        public long AD_CODE { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int MNG_COMPANY_CODE { get; set; }
        public string MNG_COMPANY_NAME { get; set; }
        public int SHARE_STATUS { get; set; }
        public string SHARE_STATUS_NAME { get; set; }
		public string REMARK2 { get; set; }
        public string UPDATE_NAME { get; set; }
        public DateTime UPDATE_DATE { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }
    }

    /// <summary>
    /// 로컬박스메인 승인 저장
    /// </summary>
    public class DEVICE_MAIN_SHARE_APPROVAL_SAVE
    {
        /// <summary>
        /// 변경전 승인코드
        /// </summary>
        public int PRE_SHARE_STATUS { get; set; }
        /// <summary>
        /// 변경후 승인코드
        /// </summary>
        public int SHARE_STATUS { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public int? REG_CODE { get; set; }
        /// <summary>
        /// 등록자
        /// </summary>
        public List<DEVICE_MAIN_SHARE_APPROVAL_SAVE_LIST> list { get; set; }
    }
    /// <summary>
    /// 로컬박스메인 승인 저장 상세리스트
    /// </summary>
    public class DEVICE_MAIN_SHARE_APPROVAL_SAVE_LIST
    {
        public long DEVICE_CODE { get; set; }
        public long AD_CODE { get; set; }
        public int SEQ { get; set; }
    }

    public class DEVICE_MAIN_SHARE_TOTAL_COND
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string SORT { get; set; }
        public int COLUMN_COUNT { get; set; }
        public string GUBUN { get; set; }
        public string SEARCH_TEXT { get; set; }

        public int? COMPANY_CODE { get; set; }
    }

    public class DEVICE_MAIN_SHARE_TOTAL_LIST
    {
        public long IDX { get; set; }
        public string GUBUN { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }

        public int CNT1 { get; set; }
        public int CNT2 { get; set; }
        public int CNT3 { get; set; }
        public int CNT4 { get; set; }
        public int CNT5 { get; set; }
        public int CNT6 { get; set; }
        public DateTime? REQ_DATE { get; set; }
        public int TOT_CNT { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }

    public class DEVICE_MAIN_SHARE_SAVE
    {
        public int? REG_CODE { get; set; }
        public List<T_DEVICE> DEVICE_LIST {get;set;}
        public List<T_DEVICE_MAIN> MAIN_LIST { get; set; }
    }

    public class AD_SHARE_DEVICE_MAIN_COND
    {
        /// <summary>
        /// 구분 0:내배너 1:모바일배너
        /// </summary>
        public int GUBUN { get; set; }
        public int PAGE { get; set; }
        public int PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        public long AD_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
    }
    public class AD_SHARE_DEVICE_MAIN
    {
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int SHARE_STATUS { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }


    public class AD_SHARE_SIGNAGE_COND
    {
        /// <summary>
        /// 구분 0:내배너 1:모바일배너
        /// </summary>
        public int GUBUN { get; set; }
        public int PAGE { get; set; }
        public int PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        public long AD_CODE { get; set; }
        public string SIGN_NAME { get; set; }
    }
    public class AD_SHARE_SIGNAGE
    {
        public long SIGN_CODE { get; set; }
        public string SIGN_NAME { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }
}

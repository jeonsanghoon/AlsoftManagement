using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 로컬사인(사이니지)정보(T_SIGNAGE)


    public class T_SIGNAGE_COND
    {
        public long? id { get; set; }
        /// <summary>       
        /// 사인코드 
        /// </summary>	   
        public long? SIGN_CODE { get; set; }
        /// <summary>       
        /// 사인명
        /// </summary>	   
        public string SIGN_NAME { get; set; }
        /// <summary>       
        /// 대표여부(공통코드 B003, 0:아니요 1:예)
        /// </summary>	   
        public bool? IS_REPRESENT { get; set; }
        /// <summary>       
        /// 대표 사이니지코드
        /// </summary>	   
        public Int64? REPRE_SIGN_CODE { get; set; }
        /// <summary>
        /// 대표사이니지명
        /// </summary>
        public string REPRE_SIGN_NAME { get; set; }
        /// <summary>
        /// 인증번호 T_DEVICE_AUTH_NUMBER 테이블 참조
        /// <summary>
        /// 인증번호 T_DEVICE_AUTH_NUMBER 테이블 참조
        /// </summary>
        public long? AUTH_NUMBER { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool? IS_VERTICAL { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int? PLAY_TIME { get; set; }
        /// <summary>
        /// 인증여부 1:인증 0:미인증
        /// </summary>
        public string AUTH_YN { get; set; }
        /// <summary>       
        /// T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 매장명
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>       
        /// 요청한사용자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 요청자이름
        /// </summary>	   
        public string MEMBER_NAME { get; set; }
        /// <summary>       
        /// 담당업체코드 T_COMPANY테이블의 COMPANY_CODE
        /// </summary>	   
        public int? CONTACT_COMPANY_CODE { get; set; }
        /// <summary>
        /// 당담업체명
        /// </summary>
        public string CONTACT_COMPANY_NAME { get; set; }
        /// <summary>       
        /// 담당지점코드 T_STORE테이블의 STORE_CODEE
        /// </summary>	   
        public int? CONTACT_STORE_CODE { get; set; }
        /// <summary>
        /// 담당지점명
        /// </summary>
        public string CONTACT_STORE_NAME { get; set; }
        /// <summary>       
        /// 담당자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? CONTACT_CODE { get; set; }
        /// <summary>       
        /// 담당자명
        /// </summary>	   
        public string CONTACT_NAME { get; set; }

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
        public string SORT { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }
    }
    /// <summary>       
    /// 로컬사인(사이니지)정보(T_SIGNAGE)
    /// </summary>	   
    public class T_SIGNAGE
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 사인코드 
        /// </summary>	   
        public long? SIGN_CODE { get; set; }
        /// <summary>       
        /// 사인명
        /// </summary>	   
        public string SIGN_NAME { get; set; }
        /// <summary>
        /// 하위사이니지 갯수
        /// </summary>
        public int SUB_SIGN_CNT { get; set; }
        /// <summary>       
        /// 대표여부(공통코드 B003, 0:아니요 1:예)
        /// </summary>	   
        public bool? IS_REPRESENT { get; set; }
        /// <summary>       
        /// 대표 사이니지코드
        /// </summary>	   
        public Int64? REPRE_SIGN_CODE { get; set; }
        /// <summary>
        /// 대표사이니지명
        /// </summary>
        public string REPRE_SIGN_NAME { get; set; }
        /// <summary>
        /// 인증번호 T_DEVICE_AUTH_NUMBER 테이블 참조
        /// </summary>
        public long? AUTH_NUMBER { get; set; }
        /// <summary>
        /// 로컬박스 고유번호 T_DEVICE_AUTH_NUMBER 테이블과 연계
        /// </summary>
        public string DEVICE_NUMBER { get; set; }
        /// <summary>       
        /// 세로여부 0:가로 1:세로 T_COMMON :U005 
        /// </summary>	   
        public bool IS_VERTICAL { get; set; } = false;
        /// <summary>
        /// 가로/세로
        /// </summary>
        public string IS_VERTICAL_NAME { get; set; }
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public int? PLAY_TIME { get; set; } = 80;
        /// <summary>       
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>	   
        public string PLAY_TIME_NAME { get; set; }
        /// <summary>       
        /// 주소1
        /// </summary>	   
        public string ADDRESS1 { get; set; }
        /// <summary>       
        /// 주소2
        /// </summary>	   
        public string ADDRESS2 { get; set; }
        /// <summary>       
        /// 우편번호
        /// </summary>	   
        public string ZIP_CODE { get; set; }
        /// <summary>       
        /// 위도
        /// </summary>	   
        public decimal? LATITUDE { get; set; }
        /// <summary>       
        /// 경도
        /// </summary>	   
        public decimal? LONGITUDE { get; set; }
        /// <summary>       
        /// 반경(m 단위)
        /// </summary>	   
        public int? RADIUS { get; set; } = 3000;
        /// <summary>       
        /// 사용여부 0:사용 1:미사용 (T_COMMON : U004)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>
        /// 광고갯수
        /// </summary>
        public int? AD_SIGNINFO_CNT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 매장명
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>       
        /// 요청한사용자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 요청자이름
        /// </summary>	   
        public string MEMBER_NAME { get; set; }
        /// <summary>       
        /// 담당업체코드 T_COMPANY테이블의 COMPANY_CODE
        /// </summary>	   
        public int? CONTACT_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 담당지점코드 T_STORE테이블의 STORE_CODEE
        /// </summary>	   
        public int? CONTACT_STORE_CODE { get; set; }
        /// <summary>       
        /// 담당자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? CONTACT_CODE { get; set; }
        /// <summary>       
        /// 담당자명
        /// </summary>	   
        public string CONTACT_NAME { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자명
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
    #endregion >> 로컬사인(사이니지)정보(T_SIGNAGE) END 

    /// <summary>
    /// 모바일 사이니지 리스트
    /// </summary>
    public class MOBILE_SIGNAGE_LIST
    {
        public long SIGN_CODE { get; set; }
        public string SIGN_NAME { get; set; }
        public decimal DISTANCE { get; set; }
        public decimal PLACE_DISTANCE { get; set; }
        public string REMARK { get; set; }
        public string COMPANY_NAME { get; set; }
        public decimal LATITUDE { get; set; }
        public decimal LONGITUDE { get; set; }
        public int RADIUS { get; set; }
    }

    /// <summary>
    /// 모바일 사이니지 조건
    /// </summary>
    public class MOBILE_SIGNAGE_COND
    {
        public decimal LATITUDE { get; set; }
        public decimal LONGITUDE { get; set; }
        /// <summary>
        /// AES256으로 암호화된 위도
        /// </summary>
        public String SEARCH_LAT;
        /// <summary>
        /// AES256으로 암호화된 경도
        /// </summary>
        public String SEARCH_LONG;
        public string SIGN_NAME { get; set; }
        public int PAGE { get; set; }
        public int PAGE_COUNT { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 회사정보(T_COMPANY)


    public class T_COMPANY_COND
    {
        /// <summary>
        /// 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 조회 Row 건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }

        /// <summary>       
        /// 자동순번(기본키)
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 회사아이디(Unique)
        /// </summary>	   
        public string COMPANY_ID { get; set; }
        /// <summary>       
        /// 회사명
        /// </summary>	   
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 회사유형(T_COMMON : C003)
        /// </summary>
        public int? COMPANY_TYPE { get; set; }
        /// <summary>       
        /// 상태(T_COMMON테이블의 MAIN_CODE : S001
        /// </summary>	   
        public int? STATUS { get; set; }
        public string INSERT_NAME { get; set; }

    }
    /// <summary>       
    /// 회사정보(T_COMPANY)
    /// </summary>	   
    public class T_COMPANY
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 자동순번(기본키)
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 회사아이디(Unique)
        /// </summary>	   
        public string COMPANY_ID { get; set; }
        /// <summary>       
        /// 암호(SHA1암호화)
        /// </summary>	   
        public string PASSWORD { get; set; }
        /// <summary>       
        /// 회사명
        /// </summary>	   
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 회사유형(T_COMMON : C003)
        /// </summary>
        public int? COMPANY_TYPE { get; set; }
      
        /// <summary>
        /// 회사유형
        /// </summary>
        public string COMPANY_TYPE_NAME { get; set; }
        /// <summary>
        /// 회사유형2(T_COMMON : C004)
        /// </summary>
        public int? COMPANY_TYPE2 { get; set; }

        /// <summary>
        /// 회사유형(T_COMMON : C004)
        /// </summary>
        public string COMPANY_TYPE_NAME2 { get; set; }
        /// <summary>       
        /// 전화번호
        /// </summary>	   
        public string PHONE { get; set; }
        /// <summary>       
        /// 핸드폰번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>       
        /// 기본주소
        /// </summary>	   
        public string ADDRESS1 { get; set; }
        /// <summary>       
        /// 상세주소
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
        /// 대표자명
        /// </summary>	   
        public string OWNER_NAME { get; set; }
        /// <summary>       
        /// 대표자전화
        /// </summary>	   
        public string OWNER_PHONE { get; set; }
        /// <summary>       
        /// 대표자핸드폰
        /// </summary>	   
        public string OWNER_MOBILE { get; set; }
        /// <summary>       
        /// 대표자이메일
        /// </summary>	   
        public string OWNER_EMAIL { get; set; }
        /// <summary>
        /// T_STORE 테이블 STORE_TYPE 1(본점) 과 데이터 동기화 여부
        /// </summary>
        public bool? STORE_SYNC { get; set; }
        /// <summary>       
        /// 상태(T_COMMON테이블의 MAIN_CODE : S001
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>
        /// 상태명
        /// </summary>
        public string STATUS_NAME { get; set; }
        /// <summary>       
        /// 문화권(언어-국가, ko-KR)
        /// </summary>	   
        public string CULTURE_NAME { get; set; }
        /// <summary>       
        /// 테마명
        /// </summary>	   
        public string THEME_NAME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
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
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자이름
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        /// <summary>
        /// 준비중 매장수 
        /// </summary>
        public int? STORE_CNT0 { get; set; }
        /// <summary>
        /// 운영중 매장수 
        /// </summary>
        public int? STORE_CNT1 { get; set; }
        /// <summary>
        /// 폐점 매장수 
        /// </summary>
        public int? STORE_CNT9 { get; set; }
        /// <summary>
        /// 내소유 직접 관리
        /// </summary>
        public int? AD_CNT0 { get; set; }
        /// <summary>
        /// 내소유이면서  대행
        /// </summary>
        public int? AD_CNT1 { get; set; }
        /// <summary>
        /// 대행
        /// </summary>
        public int? AD_CNT2 { get; set; }
        
        /// <summary>
        /// 내소유 직접 관리
        /// </summary>
        public int? DEVICE_CNT0 { get; set; }
        /// <summary>
        /// 내소유이면서  대행
        /// </summary>
        public int? DEVICE_CNT1 { get; set; }
        /// <summary>
        /// 대행
        /// </summary>
        public int? DEVICE_CNT2 { get; set; }

        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 회사정보(T_COMPANY) END 


}

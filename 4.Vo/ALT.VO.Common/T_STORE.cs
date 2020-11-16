using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    public enum enDisplayMode
    { 
        Login, Total
    }
    public class T_STORE_COND {
        /// <summary>
        /// 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 조회 Row 건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        public int? COMPANY_CODE { get; set; }
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public int? STATUS { get; set; }
        public string INSERT_NAME { get; set; }
        /// <summary>
        /// 0:사업장만 표시 1일경우 업체>사업장으로 표시
        /// </summary>
        public int COMBO_DISPLAY { get; set; }
       

        public enDisplayMode enDisplay { get; set; } = enDisplayMode.Login;
    }
    #region >> 회사별매장정보(T_STORE)
    /// <summary>       
    /// 회사별매장정보(T_STORE)
    /// </summary>	   
    public class T_STORE
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// T_COMPANY의 COMPANY_CODE(회사코드)
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 회사아이디
        /// </summary>
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 매장코드(일련번호)
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 매장아이디(Unique)
        /// </summary>	   
        public string STORE_ID { get; set; }
        /// <summary>       
        /// 암호(SHA1암호화)
        /// </summary>	   
        public string PASSWORD { get; set; }
        /// <summary>       
        /// 회사명
        /// </summary>	   
        public string STORE_NAME { get; set; }
        /// <summary>
        /// 회사유형(T_COMMON : C003)
        /// </summary>
        public int? COMPANY_TYPE { get; set; }
        /// <summary>
        /// 회사유형2(T_COMMON : C004)
        /// </summary>
        public int? COMPANY_TYPE2 { get; set; }
        /// <summary>
        /// 매장유형(T_COMMON : C004) PARENT_CODE - COMPANY, T_COMMON:C003의 REF_DATA1
        /// </summary>
        public int? STORE_TYPE { get; set; }
        public string STORE_TYPE_NAME { get; set; }
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
        /// 상태(T_COMMON테이블의 MAIN_CODE : S001
        /// </summary>	   
        public int? STATUS { get; set; }
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
        /// 
        /// </summary>
        public decimal? TIME_ZONE { get; set; }
        /// <summary>
        /// 사업자등록번호
        /// </summary>
        public string BUSI_REG_NUMBER { get; set; }

        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }

        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
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

        /// <summary>
        /// 근무직원수
        /// </summary>
        public int EMPLOYEE_CNT1 { get; set; }
        /// <summary>
        /// 미근무직원수
        /// </summary>
        public int EMPLOYEE_CNT2 { get; set; }
    }
    #endregion >> 회사별매장정보(T_STORE) END 
}

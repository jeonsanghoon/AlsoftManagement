using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 분류코드 테이블(T_CATEGORY) 조회 조건 클래스 - CATEGORY_COND
    public class CATEGORY_COND
    {
        /// <summary>       
        /// 카테고리 일련번호 => 값이 있으면 equal 검색
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역=> 값이 있으면 equal 검색
        /// </summary>	   
        public int? CATEGORY_TYPE { get; set; }
        public string CATEGORY_TYPES { get; set; }
        /// <summary>       
        /// 상위카테고리코드 => 값이 있으면 equal 검색
        /// </summary>	   
        public int? PARENT_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 레벨깊이 => 값이 있으면 equal 검색
        /// </summary>	   
        public int? LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100 => 값이 있으면 LIKE 뒤에 % 검색 예) SEARCH_CATEGORY_CODE LIKE '1|12|%'
        /// </summary>	   
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리명 => 값이 있으면 앞뒤 LIKE 검색 예) CATEGORY_NAME LIKE N'%레스토%'
        /// </summary>	   
        public string CATEGORY_NAME { get; set; }
        /// <summary>
        /// 숨김 처리여부 false(0:보임), true(1:숨김)
        /// </summary>
        public bool? HIDE { get; set; }
    }
    #endregion

    #region >> 분류코드 테이블(T_CATEGORY) 반환 리스트 클래스
    /// <summary>
    /// 분류코드 테이블(T_CATEGORY) 반환 리스트 클래스
    /// </summary>
    public class CATEGORY_LIST
    {
        /// <summary>       
        /// 카테고리 번호
        /// </summary>	   
        public int CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역
        /// </summary>	   
        public int CATEGORY_TYPE { get; set; }
        /// <summary>       
        /// 상위카테고리코드
        /// </summary>	   
        public int PARENT_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 레벨깊이, 기본키
        /// </summary>	   
        public int LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100
        /// </summary>	   
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리명
        /// </summary>	   
        public string CATEGORY_NAME { get; set; }
    }
    #endregion

    #region >> 분류코드(T_CATEGORY) 기본클래스(저장용)
    /// <summary>       
    /// 광고분류테이블(T_CATEGORY)
    /// </summary>	   
    public class T_CATEGORY
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 분류번호, MAX + 1로생성함, 기본키
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역
        /// </summary>	   
        public int? CATEGORY_TYPE { get; set; }
        /// <summary>       
        /// 카테고리유형2 (T_COMMON 테이블의 MAIN_CODE : B005) 1:광고>일반 2:지역>일반 3:지역>핫플레이스
        /// </summary>	   
        public int? CATEGORY_TYPE2 { get; set; }
        /// <summary>       
        /// 상위카테고리코드
        /// </summary>	   
        public int PARENT_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 레벨깊이, 기본키
        /// </summary>	   
        public int? LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100
        /// </summary>	   
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리명
        /// </summary>	   
        public string CATEGORY_NAME { get; set; }
        /// <summary>       
        /// 카테고리 표시명
        /// </summary>	   
        public string CATEGORY_DISPlAY_NAME { get; set; }
        /// <summary>       
        /// 정렬순서
        /// </summary>	   
        public int ORDER_SEQ { get; set; }
        /// <summary>
        /// 검색순서용 코드
        /// </summary>
        public string SEARCH_ORDER_SEQ { get; set; }
        /// <summary>       
        /// 숨김(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 분류코드(T_CATEGORY) END 


    public class PAGE_COND
    {
        public long CODE { get; set; }
        public string TYPE1 { get; set; }
        public string TYPE2 { get; set; }
        public string NAME { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        
    }

    public class AD_REGION {
        public string MRC_EDIT_MODE { get; set; }
        public long AD_CODE { get; set; }
        public int CATEGORY_CODE { get; set; }
        public string SEARCH_CATEGORY_CODE { get; set; }
        public string CATEGORY_NAME1        { get; set;} 
        public string CATEGORY_NAME2        { get; set;}
        
        public string CATEGORY_NAME3        { get; set;}
        
        public int TOTAL_ROWCOUNT { get; set; }


    }

    public class DEVICE_REGION
    {
        public string MRC_EDIT_MODE { get; set; }
        public long DEVICE_CODE { get; set; }
        public int CATEGORY_CODE { get; set; }
        public string SEARCH_CATEGORY_CODE { get; set; }
        public string CATEGORY_NAME1 { get; set; }
        public string CATEGORY_NAME2 { get; set; }

        public string CATEGORY_NAME3 { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }

    }


    #region >> 카테고리 페이지 리스트

    public class CATEGORY_PAGE_COND
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        /// <summary>       
        /// 분류코드번호  MAX + 1로생성함, 기본키
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역
        /// </summary>	   
        public int? CATEGORY_TYPE { get; set; }
        /// <summary>       
        /// 카테고리유형2 (T_COMMON 테이블의 MAIN_CODE : B005) 1:광고>일반 2:지역>일반 3:지역>핫플레이스
        /// </summary>	   
        public int? CATEGORY_TYPE2 { get; set; }
        /// <summary>       
        /// 상위카테고리코드
        /// </summary>	   
        public int? PARENT_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 레벨깊이, 기본키
        /// </summary>	   
        public int? LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100
        /// </summary>	   
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리명
        /// </summary>	   
        public string CATEGORY_NAME { get; set; }
        /// <summary>       
        /// 숨김(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	  
        public bool? HIDE { get; set; }

        public string TABLE_NAME { get; set; }

    }

    /// <summary>       
    /// 카테고리 페이지 리스트 광고분류테이블(T_CATEGORY)
    /// </summary>	   
    public class CATEGORY_PAGE_LIST
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 분류코드번호  MAX + 1로생성함, 기본키
        /// </summary>	   
        public int CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역
        /// </summary>	   
        public int CATEGORY_TYPE { get; set; }
        public string CATEGORY_TYPE_NAME { get; set; }
        /// <summary>       
        /// 카테고리유형2 (T_COMMON 테이블의 MAIN_CODE : B005) 1:광고>일반 2:지역>일반 3:지역>핫플레이스
        /// </summary>	   
        public int CATEGORY_TYPE2 { get; set; }
        public string CATEGORY_TYPE2_NAME { get; set; }
        /// <summary>       
        /// 상위카테고리코드
        /// </summary>	   
        public int PARENT_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 레벨깊이, 기본키
        /// </summary>	   
        public int LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100
        /// </summary>	   
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 카테고리명
        /// </summary>	   
        public string CATEGORY_NAME { get; set; }
        /// <summary>       
        /// 정렬순서
        /// </summary>	   
        public int ORDER_SEQ { get; set; }
        /// <summary>       
        /// 숨김(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public bool HIDE { get; set; }
      
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }

        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }

    }
    #endregion >> 광고분류테이블(T_CATEGORY) END 

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    #region >> 키워드 테이블(T_KEYWORD)
    public class T_KEYWORD_COND
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        /// <summary>       
        /// 키워드유형(T_COMMON테이블 MAIN_CODE : B006 => 1:카테고리, 2:하위단어 ) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public int? KEYWORD_TYPE { get; set; }
        /// <summary>       
        /// 키워드코드
        /// </summary>	   
        public int? KEYWORD_CODE { get; set; }
        /// <summary>       
        /// 기준되는코드, 값이 같은 경우 같은 키워드로 판단함, 키워드 검색시 해당코드가 같은 것중에 IS_SYNONYM이 0인 것으로 서버에서 조회 한다.
        /// </summary>	   
        public int? BASE_CODE { get; set; }
        /// <summary>       
        /// 동의어 여부(0:기본어, 1:동의어) T_COMMON : B003
        /// </summary>	   
        public bool? IS_SYNONYM { get; set; }
        /// <summary>       
        /// 키워드명
        /// </summary>	   
        public string KEYWORD_NAME { get; set; }
        /// <summary>
        /// 등록상태(T_COMMON 테이블 : K001)1	신규, 9	완료, 11	취소
        /// </summary>
        public int? STATUS { get; set; }
        /// <summary>       
        /// 숨김여부(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public bool? HIDE { get; set; }

        public int? CATEGORY_CODE { get; set;  }
        public string TABLE_NAME { get; set; }
    }

    /// <summary>       
    /// 키워드 테이블(T_KEYWORD)
    /// </summary>	   
    public class T_KEYWORD
    {
        public string MRC_EDIT_MODE { get; set; }
        public long? ROW_SEQ { get; set; }
        /// <summary>       
        /// 키워드코드
        /// </summary>	   
        public int? KEYWORD_CODE { get; set; }
        /// <summary>       
        /// 키워드유형(T_COMMON테이블 MAIN_CODE : B006 => 1:카테고리, 2:하위단어 ) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public int? KEYWORD_TYPE { get; set; }
        /// <summary>       
        /// 기준되는코드, 값이 같은 경우 같은 키워드로 판단함, 키워드 검색시 해당코드가 같은 것중에 IS_SYNONYM이 0인 것으로 서버에서 조회 한다.
        /// </summary>	   
        public int BASE_CODE { get; set; }
        /// <summary>       
        /// 동의어 여부(0:기본어, 1:동의어) T_COMMON : B003
        /// </summary>	   
        public bool IS_SYNONYM { get; set; }
        /// <summary>       
        /// BASE_CODE, IS_SYNONYM 별 순번
        /// </summary>	   
        public int? SEQ { get; set; }
        /// <summary>       
        /// 키워드명
        /// </summary>	   
        public string KEYWORD_NAME { get; set; }
        /// <summary>       
        /// 키워드설명
        /// </summary>	   
        public string KEYWORD_DESC { get; set; }
        /// <summary>       
        /// 위도
        /// </summary>	   
        public decimal? LATITUDE { get; set; }
        /// <summary>       
        /// 경도
        /// </summary>	   
        public decimal? LONGITUDE { get; set; }
        /// <summary>       
        /// 초성
        /// </summary>	   
        public string CHOSUNG { get; set; }
        /// <summary>       
        /// 중성
        /// </summary>	   
        public string JUNGSUNG { get; set; }
        /// <summary>       
        /// 종성
        /// </summary>	   
        public string JONGSUNG { get; set; }
        /// <summary>       
        /// 초성,중성,종성이분리
        /// </summary>	   
        public string KEYWORD_UNITS { get; set; }
        /// <summary>       
        /// 등록상태(T_COMMON 테이블 : K001)
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 조회횟수
        /// </summary>	   
        public int SEARCH_CNT { get; set; }
        /// <summary>       
        /// 숨김여부(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public bool HIDE { get; set; }
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
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 해당카테고리에 있는 키워드인지 구분
        /// </summary>
        public bool IS_CATEGORY { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }
        /// <summary>
        /// 동의어
        /// </summary>
        public string SYNONYM_NAMES { get; set; }
    }
    #endregion >> 키워드 테이블(T_KEYWORD) END 




    #region >> 키워드조건
    /// <summary>
    /// 키워드조건
    /// </summary>
    public class KEYWORD_COND
    {
        public string KEYWORD_TYPE { get; set; }
        public string KEYWORD_NAME { get; set; }
        public string CHOSUNG { get; set; }
        public string JUNGSUNG { get; set; }
        public string JONGSUNG { get; set; }
        public string KEYWORD_UNITS { get; set; }
    }
    #endregion 
    //#region >> 키워드리스트
    ///// <summary>
    ///// 키워드리스트
    ///// </summary>
    //public class KEYWORD_LIST
    //{
    //    /// <summary>
    //    /// 키워드코드
    //    /// </summary>
    //    public int KEYWORD_CODE { get; set; }
    //    /// <summary>
    //    /// 키워드명
    //    /// </summary>
    //    public string KEYWORD_NAME { get; set; }
    //}
    //#endregion >> 키워드리스트

    #region >> 코드클래스
    public class CODE_DATA
    {
        /// <summary>
        /// 코드(숫자형)
        /// </summary>
        public int? CODE { get; set; }
        /// <summary>
        /// T_KEYWORD 테이블의 KEYWORD_TYPE B006 1:지역 2:일반 3:기기
        /// </summary>
        public string CODE_TYPE { get; set; }
        /// <summary>
        /// 조회용 코드(예-SEARCH_CATEGORY_CODE)
        /// </summary>
        public string SEARCH_CODE { get; set; }
        /// <summary>
        /// 코드명
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 위도
        /// </summary>
        public decimal? LATITUDE { get; set; }
        /// <summary>
        /// 경도
        /// </summary>
        public decimal? LONGITUDE { get; set; }

        /// <summary>
        /// T_CATEGORY_KEYWORD 테이블의 CK_CODE
        /// </summary>
        public int? CK_CODE { get; set; }


    }
    #endregion >> 코드클래스

    public class KEYWORD_SYNONYM_SAVE
    {
        public int BASE_CODE { get; set; }
        public List<T_KEYWORD> KEYWORD_LIST { get; set; }
    }

    public class KEYWORD_SAVE_DATA
    {
        public long? AD_CODE { get; set; }
        public long? DEVICE_CODE { get; set; }
        public int? REG_CODE { get; set; }
        public List<KEYWORD_DATA> KEYWORDS { get; set; }
    }
}

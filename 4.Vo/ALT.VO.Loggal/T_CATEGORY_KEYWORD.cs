using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 카테고리와 키워드 연결 테이블(T_CATEGORY_KEYWORD)
    /// <summary>       
    /// 카테고리와 키워드 연결 테이블(T_CATEGORY_KEYWORD)
    /// </summary>	   
    public class T_CATEGORY_KEYWORD
    {
        /// <summary>       
        /// 일련번호(기본키)
        /// </summary>	   
        public int? CK_CODE { get; set; }
        /// <summary>       
        /// 카테고리코드 T_CATEGORY 테이블과 Relation
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 키워드코드 T_KEYWORD 테이블과 Relation
        /// </summary>	   
        public int? KEYWORD_CODE { get; set; }
        /// <summary>       
        /// 키워드명
        /// </summary>	   
        public string KEYWORD_NAME { get; set; }
        /// <summary>       
        /// 위도
        /// </summary>	   
        public decimal? LATITUDE { get; set; }
        /// <summary>       
        /// 경도
        /// </summary>	   
        public decimal? LONGITUDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(0:보이기, 1:숨김) T_COMMON : MAIN_CODE=>B003
        /// </summary>	   
        public bool? HIDE { get; set; }
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
    #endregion >> 카테고리와 키워드 연결 테이블(T_CATEGORY_KEYWORD) END 

    /// <summary>
    /// 카테고리키워드 테이블 조회 조건
    /// </summary>
    public class CATEGORY_KEYWORD_COND
    {
        public int? CATEGORY_CODE { get; set; }
        public string SEARCH_CATEGORY_CODE { get; set; }
        public int? CATEGORY_TYPE { get; set; }
        public int? LEVEL_DEPTH { get; set; }
        public int? KEYWORD_TYPE { get; set; }

        public long? AD_CODE { get; set; }
        public long? DEVICE_CODE { get; set; }
        public int? KEYWORD_CODE { get; set; }
        public int? CK_CODE { get; set; }
        public string KEYWORD_NAME { get; set; }

    }

    public class CATEGORY_KEYWORD_SAVE
    {
        public int? CATEGORY_CODE { get; set; }
        public string KEYWORD_CODES { get; set; }
        public int? REG_CODE { get; set; }
    }
}

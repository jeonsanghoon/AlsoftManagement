using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{

    #region >> 메모테이블(T_MEMO)조회조건
    /// <summary>       
    /// 메모테이블(T_MEMO)
    /// </summary>	   
    public class T_MEMO_COND
    {
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
        public long? IDX { get; set; }
        /// <summary>       
        /// 테이블명
        /// </summary>	   
        public string TABLE_NAME { get; set; }
        /// <summary>       
        /// 테이블기본키
        /// </summary>	   
        public string TABLE_KEY { get; set; }
    }

    #endregion >> 메모테이블(T_MEMO)조회조건 END 

    #region >> 메모테이블(T_MEMO)
    /// <summary>       
    /// 메모테이블(T_MEMO)
    /// </summary>	   
    public class T_MEMO
    {
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>       
        /// 테이블명
        /// </summary>	   
        public string TABLE_NAME { get; set; }
        /// <summary>       
        /// 테이블기본키
        /// </summary>	   
        public string TABLE_KEY { get; set; }
        /// <summary>       
        /// 메모
        /// </summary>	   
        public string MEMO { get; set; }
        /// <summary>       
        /// 메모1
        /// </summary>	   
        public string MEMO1 { get; set; }
        /// <summary>       
        /// 메모2
        /// </summary>	   
        public string MEMO2 { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
        public string SORT { get; set; }
    }
    #endregion >> 메모테이블(T_MEMO) END 
}

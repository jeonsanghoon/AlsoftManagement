using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 서브배너테이블



    /// <summary>       
    /// 서브배너테이블
    /// </summary>	   
    public class T_AD_SUB
    {
        public string SAVE_MODE { get; set; }
        /// <summary>
        /// 순번(기본키)
        /// </summary>
        public int? IDX { get; set; }
        /// <summary>       
        /// 배너테이블(T_AD)의 기본키
        /// </summary>	   
        public Int64 AD_CODE { get; set; }
        /// <summary>
        /// 배치순번
        /// </summary>
        public int SEQ { get; set; }
        /// <summary>
        /// 이미지경로
        /// </summary>
        public string IMG_URL { get; set; }
        /// <summary>       
        /// 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 관계
        /// </summary>	   
        public string RELATION { get; set; }
        /// <summary>       
        /// 내용
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>
        /// 등록자
        /// </summary>
        public string REG_NAME { get; set; }
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
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 순번(기본키)(T_AD_SUB) END 

}

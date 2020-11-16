using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 행정구역경계정보테이블(T_GEO)
    /// <summary>       
    /// 행정구역경계정보테이블(T_GEO)
    /// </summary>	   
    public class T_GEO
    {
        /// <summary>
        /// 저장유형 N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 유형(T_COMMON 테이블의 G001, 1:시도,2:시군구,3:읍면동)
        /// </summary>	   
        public int? GEO_TYPE { get; set; }
        /// <summary>       
        /// 코드
        /// </summary>	   
        public string CODE { get; set; }
        /// <summary>       
        /// 명
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 경계폴리곤데이터
        /// </summary>	   
        public string COORDINATES { get; set; }
        /// <summary>       
        /// 등록자코드 T_MEMBER
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
    }
    #endregion >> 행정구역경계정보테이블(T_GEO) END 

}

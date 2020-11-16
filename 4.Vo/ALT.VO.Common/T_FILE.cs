using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 파일정보(T_FILE)
    /// <summary>       
    /// 파일정보(T_FILE)
    /// </summary>	   
    public class T_FILE
    {
        /// <summary>
        /// 저장유형 N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 테이블명
        /// </summary>	   
        public string TABLE_NAME { get; set; }
        /// <summary>       
        /// 테이블키(키가 멀티일 경우 |를 구분자로 사용함)
        /// </summary>	   
        public string TABLE_KEY { get; set; }
        /// <summary>       
        /// 파일순번
        /// </summary>	   
        public int? FILE_SEQ { get; set; }
        /// <summary>       
        /// 파일유형(MAIN_CODE : F001) 1:이미지, 2:동영상, 3:유튜브, 4:HTML 
        /// </summary>	   
        public int? FILE_TYPE { get; set; }
        /// <summary>       
        /// 파일명
        /// </summary>	   
        public string FILE_NAME { get; set; }
        /// <summary>       
        /// 파일확장자
        /// </summary>	   
        public string FILE_EXT { get; set; }
        /// <summary>       
        /// 파일경로
        /// </summary>	   
        public string FILE_URL { get; set; }
        /// <summary>       
        /// 참조파일1
        /// </summary>	   
        public string REF_DATA1 { get; set; }
        /// <summary>       
        /// 참조파일2
        /// </summary>	   
        public string REF_DATA2 { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
    }
    #endregion >> 파일정보(T_FILE) END 

    public class FILE_INFO
    {
        public string URL { get; set; }
        public string FULL_URL { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_EXT { get; set; }
        public string return_msg { get; set; } = "";
    }

    public class FILE_COND
    {
       public string FolderName { get; set; }
       public int? ImageWidth { get; set; }
       public int? ImageHeight { get; set; }
    }
}

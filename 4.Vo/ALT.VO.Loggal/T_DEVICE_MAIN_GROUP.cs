using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 내배너그룹테이블(T_DEVICE_MAIN_GROUP)
    /// <summary>       
    /// 내배너그룹테이블(T_DEVICE_MAIN_GROUP)
    /// </summary>	   
    public class T_DEVICE_MAIN_GROUP
    {
        /// <summary>
        /// 저장유형(N:신규 U:수정 D:삭제)
        /// </summary>
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// T_DEVICE 테이블의 DEVICE_CODE
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>       
        /// 그룹순번
        /// </summary>	   
        public int? GROUP_SEQ { get; set; }
        /// <summary>       
        /// 그룹명
        /// </summary>	   
        public string GROUP_NAME { get; set; }
        /// <summary>       
        /// 화면분할(T_COMMON => MAIN_CODE: H002)
        /// </summary>	   
        public int? FRAME_TYPE { get; set; }
        /// <summary>
        /// 화면분할갯수 명칭(T_COMMON => MAIN_CODE: H002)
        /// </summary>
        public string FRAME_TYPE_NAME { get; set; }
        /// <summary>
        /// 콘텐츠유형 1:이미지 2:동영상 3:유튜브 T_COMMON :A010
        /// </summary>
        public int? CONTENT_TYPE { get; set; }
        /// <summary>
        /// 콘텐츠유형
        /// </summary>
        public string CONTENT_TYPE_NAME { get; set; }
        /// <summary>
        /// 실행시간(초단위) T_COMMON :U006 
        /// </summary>
        public int? PLAY_TIME { get; set; }
        /// <summary>
        /// 실행시간
        /// </summary>
        public string PLAY_TIME_NAME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자
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
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        /// <summary>
        /// 메인배너 갯수
        /// </summary>
        public int? MAIN_CNT { get; set; }
    }
    #endregion >> 내배너그룹테이블(T_DEVICE_MAIN_GROUP) END 

}

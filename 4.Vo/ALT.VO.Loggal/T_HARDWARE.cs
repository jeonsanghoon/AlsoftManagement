using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 장비(디스플레이)정보(T_HARDWARE)
    /// <summary>       
    /// 장비(디스플레이)정보(T_HARDWARE)
    /// </summary>	   
    //[Table(Name = "HARDWARE")]
    public class T_HARDWARE //: BASE_VO
    {
        /// <summary>
        /// 저장유형
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public int? HARDWARE_CODE { get; set; }
        /// <summary>       
        /// 장비이름
        /// </summary>	   
        public string HARDWARE_NAME { get; set; }
        /// <summary>       
        /// 브랜드 H004
        /// </summary>	   
        public int? BRAND { get; set; }
        public string BRAND_NAME { get; set; }
        /// <summary>       
        /// 모델명
        /// </summary>	   
        public string MODEL_NAME { get; set; }
        /// <summary>       
        /// 하드웨어넓이(cm)
        /// </summary>	   
        public int? HARDWARE_WIDTH { get; set; }
        /// <summary>       
        /// 하드웨어높이(cm)
        /// </summary>	   
        public int? HARDWARE_HEIGHT { get; set; }
        /// <summary>       
        /// 해상도 H003
        /// </summary>	   
        public int? DISPLAY_RESOLUTION { get; set; }
        /// <summary>       
        /// 해상도
        /// </summary>	 
        public string DISPLAY_RESOLUTION_NAME { get; set; }
        /// <summary>       
        /// 설명
        /// </summary>	   
        public string HARDWARE_DESC { get; set; }
        /// <summary>       
        /// 구매일자
        /// </summary>	   
        public string PURCHASE_DATE { get; set; }
        /// <summary>       
        /// 구매한회사
        /// </summary>	   
        public int? PURCHASE_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 구매한지점
        /// </summary>	   
        public int? PURCHASE_STORE_CODE { get; set; }
        /// <summary>       
        /// 구매한지점담당자
        /// </summary>	   
        public int? PURCHASE_EMPLOYEE_CODE { get; set; }
        /// <summary>       
        /// 담당회사
        /// </summary>	   
        public int? CONTACT_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 담당지점
        /// </summary>	   
        public int? CONTACT_STORE_CODE { get; set; }
        /// <summary>       
        /// 담당자
        /// </summary>	   
        public int? CONTACT_EMPLOYEE_CODE { get; set; }
        /// <summary>
        /// 총페이지수
        /// </summary>
        public int TOTAL_ROWCOUNT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool HIDE { get; set; }
        public string HIDE_NAME { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
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
        /// 로컬박스코드
        /// </summary>
        public long? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
    }
    #endregion >> 장비(디스플레이)정보(T_HARDWARE) END 

  
    [Table(Name = "HARDWARE_COND")]
    public class T_HARDWARE_COND : PAGE_COND_VO
    {
        /// <summary>
        /// 표현형태 Popup : 팝업, 그외 일반
        /// </summary>
        public string DISPLAY_MODE { get; set; }
        /// <summary>
        /// 구매일자(조건)
        /// </summary>
        public string FR_DATE { get; set; }
        /// <summary>
        /// 구매일자(조건)
        /// </summary>
        public string TO_DATE { get; set; }

        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public int? HARDWARE_CODE { get; set; }
        /// <summary>       
        /// 장비이름
        /// </summary>	   
        public string HARDWARE_NAME { get; set; }
        /// <summary>       
        /// 브랜드
        /// </summary>	   
        public int? BRAND { get; set; }
        /// <summary>       
        /// 모델명
        /// </summary>	   
        public string MODEL_NAME { get; set; }
        /// <summary>
        /// 담당자
        /// </summary>
        public int? CONTACT_EMPLOYEE_CODE { get; set; }
        /// <summary>
        /// 담당자명
        /// </summary>
        public string CONTACT_EMPLOYEE_NAME { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }
    }
}

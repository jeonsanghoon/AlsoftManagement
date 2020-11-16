using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 박스 스테이션(위치가 없는 로컬박스의 묶음 - 관리자가 생성가능)(T_DEVICE_STATION)
    /// <summary>       
    /// 박스 스테이션(위치가 없는 로컬박스의 묶음 - 관리자가 생성가능)(T_DEVICE_STATION)
    /// </summary>	   
    public class T_DEVICE_STATION
    {
        /// <summary>
        /// 저장 유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>
        /// 조회 순번
        /// </summary>
        public long IDX { get; set; }
        /// <summary>       
        /// 기본코드(순번)
        /// </summary>	   
        public int? STATION_CODE { get; set; }
        /// <summary>       
        /// 스테이션명
        /// </summary>	   
        public string STATION_NAME { get; set; }
        /// <summary>       
        /// 카테고리(T_COMMON 테이블 현재 미사용코드)
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
        /// <summary>       
        /// 주소
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
        /// 스테이션설명
        /// </summary>	   
        public string STATION_DESC { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REAMRK { get; set; }
        /// <summary>
        /// 숨김여부 1: 숨김 0 : 보임
        /// </summary>
        public bool? HIDE { get; set; }
        
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자
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
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        /// <summary>
        /// 스테이션당 로컬박스수
        /// </summary>
        public int DEVICE_CNT { get; set; }

        /// <summary>
        /// 최근 일주일간 디바이스 등록건수
        /// </summary>
        public int NEW_DEVICE_CNT { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }

        public string LOGO_URL { get; set; }
    }

    public class T_DEVICE_STATION_COND
    {
        /// <summary>       
        /// 기본코드(순번)
        /// </summary>	   
        public int? STATION_CODE { get; set; }
        /// <summary>       
        /// 스테이션명
        /// </summary>	   
        public string STATION_NAME { get; set; }
        /// <summary>       
        /// 카테고리(T_COMMON 테이블 현재 미사용코드)
        /// </summary>	   
        public int? CATEGORY_CODE { get; set; }
     
        public bool? HIDE { get; set; }

        private string _SORT = "A.STATION_CODE";
        /// <summary>
        /// 정렬
        /// </summary>
        public string SORT { get { return _SORT; }  set { _SORT = string.IsNullOrEmpty(value) ? _SORT : value; } }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }

        public string PV_TYPE { get; set; }
    }
    #endregion >> 박스 스테이션(위치가 없는 로컬박스의 묶음 - 관리자가 생성가능)(T_DEVICE_STATION) END 

    public class DEVICE_STATION_UPDATE
    {
        public string DEVICE_CODES { get; set; }
        public int? STATION_CODE { get; set; }
        public int? UPDATE_CODE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
	#region >> 광고&단말기테이블 로그(T_AD_DEVICE_LOG)조회조건
	/// <summary>       
	/// 광고&단말기테이블 로그(T_AD_DEVICE_LOG)
	/// </summary>	   
	public class T_AD_DEVICE_LOG_COND
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
		/// 광고코드
		/// </summary>	   
		public long? AD_CODE { get; set; }
		/// <summary>       
		/// 제목
		/// </summary>	   
		public string TITLE { get; set; }
		/// <summary>       
		/// 로컬박스 코드
		/// </summary>	
		public long? DEVICE_CODE { get; set; }
		/// <summary>       
		/// 로컬박스 명
		/// </summary>	
		public string DEVICE_NAME { get; set; }
		/// <summary>       
		/// 광고시작일(yyyyMMdd)
		/// </summary>	   
		public string FR_DATE { get; set; }
		/// <summary>       
		/// 광고종료일(yyyyMMdd)
		/// </summary>	   
		public string TO_DATE { get; set; }
		/// <summary>       
		/// 승인상태 T_COMMON테이블( MAIN_CODE : A009) 1:요청 2:반려 9:승인
		/// </summary>	   
		public int? STATUS { get; set; }
		/// <summary>       
		/// 배너업체 코드
		/// </summary>	   
		public int? AD_STORE_CODE { get; set; }
		/// <summary>       
		/// 로컬박스업체 코드
		/// </summary>	   
		public int? DEVICE_STORE_CODE { get; set; }
		/// <summary>       
		/// 수정자 아이디
		/// </summary>	   
		public string USER_ID { get; set; }
		/// <summary>       
		/// 수정자 이름
		/// </summary>	   
		public string USER_NAME { get; set; }
	}
	#endregion >> 광고&단말기테이블 로그(T_AD_DEVICE_LOG)조회조건 END 
	
	#region >> 광고&단말기테이블 로그(T_AD_DEVICE_LOG)
	/// <summary>       
	/// 광고&단말기테이블 로그(T_AD_DEVICE_LOG)
	/// </summary>	   
	public class T_AD_DEVICE_LOG
	{
		/// <summary>       
		/// 로그순번
		/// </summary>	   
		public long IDX { get; set; }
		/// <summary>       
		/// T_AD_DEVICE 테이블의 기본키
		/// </summary>	   
		public long AD_DEVICE_CODE { get; set; }
		/// <summary>       
		/// 광고코드
		/// </summary>	   
		public long? AD_CODE { get; set; }
		/// <summary>       
		/// 광고테이블
		/// </summary>	   
		public long? DEVICE_CODE { get; set; }
		/// <summary>       
		/// 광고시작일(yyyyMMdd)
		/// </summary>	   
		public string FR_DATE { get; set; }
		/// <summary>       
		/// 광고종료일(yyyyMMdd)
		/// </summary>	   
		public string TO_DATE { get; set; }
		/// <summary>       
		/// 광고종료시간
		/// </summary>	   
		public int? CLICK_CNT { get; set; }
		/// <summary>       
		/// 승인회사코드(T_COMPANY 테이블 참조)
		/// </summary>	   
		public int? APPROVAL_COMPANY_CODE { get; set; }
		/// <summary>       
		/// 승인상태 T_COMMON테이블( MAIN_CODE : A009) 1:요청 2:반려 9:승인
		/// </summary>	   
		public string STATUS { get; set; }
		/// <summary>       
		/// 비고
		/// </summary>	   
		public string REMARK { get; set; }
		/// <summary>       
		/// 로그저장시 코멘트
		/// </summary>	   
		public string REMARK2 { get; set; }
		/// <summary>       
		/// 수정자
		/// </summary>	   
		public int? UPDATE_CODE { get; set; }
		/// <summary>       
		/// 수정일
		/// </summary>	   
		public DateTime? UPDATE_DATE { get; set; }
		/// <summary>
		/// 제목
		/// </summary>
		public string TITLE { get; set; }
		/// <summary>
		/// 프레임
		/// </summary>
		public int AD_FRAME_TYPE { get; set; }
		/// <summary>
		/// 로컬박스명
		/// </summary>
		public string DEVICE_NAME { get; set; }
		/// <summary>
		/// 배너업체명
		/// </summary>
		public string AD_STORE_NAME { get; set; }
		/// <summary>
		/// 로컬박스업체명
		/// </summary>
		public string DEVICE_STORE_NAME { get; set; }
		public int TOTAL_ROWCOUNT { get; set; }
	}
	#endregion >> 광고&단말기테이블 로그(T_AD_DEVICE_LOG) END 

}

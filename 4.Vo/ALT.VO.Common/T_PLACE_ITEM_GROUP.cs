using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
	#region >> 장소 설정그룹(T_PLACE_ITEM_GROUP)조회조건
	/// <summary>       
	/// 장소 설정그룹(T_PLACE_ITEM_GROUP)
	/// </summary>	   
	public class T_PLACE_ITEM_GROUP_COND
	{
		/// <summary>
		/// 조회순서
		/// </summary>
		public string SORT_ORDER { get; set; }
	}
	#endregion >> 장소 설정그룹(T_PLACE_ITEM_GROUP)조회조건 END 

	#region >> 장소 설정그룹(T_PLACE_ITEM_GROUP)
	/// <summary>       
	/// 장소 설정그룹(T_PLACE_ITEM_GROUP)
	/// </summary>	   
	public class T_PLACE_ITEM_GROUP
	{
		public string MRC_EDIT_MODE { get; set; }
		/// <summary>       
		/// 그룹코드(순번)
		/// </summary>	   
		public int GROUP_CODE { get; set; }
		/// <summary>       
		/// 그룹명
		/// </summary>	   
		public string GROUP_NAME { get; set; }
		/// <summary>       
		/// 정렬순번, 중복되서는 안됨
		/// </summary>	   
		public int GROUP_SEQ { get; set; }
		/// <summary>       
		/// 그룹유형(T_COMMON:B007 1:배너, 2:로컬박스)
		/// </summary>	   
		public int GROUP_TYPE { get; set; }
		/// <summary>       
		/// 판매 유형
		/// </summary>	   
		public int? SALE_TYPE { get; set; }
		/// <summary>       
		/// 숨김여부(0:보임, 1:숨김)
		/// </summary>	   
		public bool HIDE { get; set; }
		/// <summary>       
		/// 비고
		/// </summary>	   
		public string REMARK { get; set; }
		/// <summary>       
		/// 등록자(T_MEMBER의 MEMBER_CODE)
		/// </summary>	   
		public string INSERT_NAME { get; set; }
		/// <summary>       
		/// 등록일
		/// </summary>	   
		public DateTime INSERT_DATE { get; set; }
		/// <summary>       
		/// 수정자(T_MEMBER의 MEMBER_CODE)
		/// </summary>	   
		public string UPDATE_NAME { get; set; }
		/// <summary>       
		/// 수정일
		/// </summary>	   
		public DateTime UPDATE_DATE { get; set; }
		public int TOTAL_ROWCOUNT { get; set; }
		public string SORT { get; set; }
	}
	#endregion >> 장소 설정그룹(T_PLACE_ITEM_GROUP) END 

	#region >> 가상 영역 유형 아이템 조회(T_MEMBER_PLACE_ITEM_COND)
	public class T_MEMBER_PLACE_ITEM_COND
	{
		/// <summary>       
		/// 멤버 코드
		/// </summary>
		public int? MEMBER_CODE { get; set; }
		/// <summary>       
		/// 배너 OR 로컬 코드
		/// </summary>
		public int? CODE { get; set; }
		/// <summary>       
		/// 시작일
		/// </summary>
		public string FR_DATE { get; set; }
		/// <summary>       
		/// 종료일
		/// </summary>
		public string TO_DATE { get; set; }
		/// <summary>       
		/// 가상 영역 아이템 타입
		/// </summary>
		public int? ITEM_TYPE { get; set; }
		/// <summary>       
		/// 가상 영역 아이템 타입 최대값
		/// </summary>
		public int? ITEM_TYPE_LIMIT { get; set; }
		/// <summary>       
		/// 최초 생성 체크
		/// </summary>
		public bool? INIT_CHECK { get; set; }
		/// <summary>       
		/// 아이템 그룹 타입
		/// </summary>
		public int? GROUP_TYPE { get; set; }
	}
	#endregion

	#region >> 가상 영역 유형 아이템 (T_MEMBER_PLACE_ITEM)
	public class T_MEMBER_PLACE_ITEM
	{
	    public long? MEMBER_ITEM_IDX { get; set; }
		/// <summary>       
		/// 상품 타입
		/// </summary>
		public int? ITEM_TYPE { get; set; }
		/// <summary>       
		/// 상품 이름
		/// </summary>
		public string ITEM_NAME { get; set; }
		/// <summary>       
		/// 구매 유형
		/// </summary>
		public int PURCHASE_TYPE { get; set; }
		/// <summary>       
		/// 구매 품목 개수
		/// </summary>
		public int ITEM_PURCHASE_CNT { get; set; }
		/// <summary>       
		/// 사용 품목 개수
		/// </summary>
		public int? ITEM_USE_CNT { get; set; }
		/// <summary>       
		/// 최초 개수에서 변경된 개수
		/// </summary>
		public int? ITEM_ALTER_CNT { get; set; }
		/// <summary>       
		/// 시작일
		/// </summary>
		public string FR_DATE { get; set; }
		/// <summary>       
		/// 종료일
		/// </summary>
		public string TO_DATE { get; set; }
	}
	#endregion
}

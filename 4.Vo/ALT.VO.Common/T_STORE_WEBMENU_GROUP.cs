using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 웹메뉴 그룹(템플릿)테이블(T_STORE_WEBMENU_GROUP)
    /// <summary>       
    /// 웹메뉴 그룹(템플릿)테이블(T_STORE_WEBMENU_GROUP)
    /// </summary>	   
    public class T_STORE_WEBMENU_GROUP
    {
        /// <summary>
        /// 저장유형(N:신규 U:수정 D:삭제)
        /// </summary>
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>
        public int? PROJECT_SITE { get; set; }
        /// <summary>       
        /// 매장(사업장)코드 T_STORE 테이블의 STORE_CODE
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 메뉴그룹코드
        /// </summary>	   
        public int? GROUP_CODE { get; set; }
        /// <summary>
        /// 정렬순번
        /// </summary>
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 메뉴명
        /// </summary>	   
        public string GROUP_NAME { get; set; }
        /// <summary>       
        /// 부서코드=> T_STORE_DEPT의 DEPT_CODE=>  권한 적용될 부서 콤마(,)로 이어서 등록
        /// </summary>	   
        public string DEPT_AUTH { get; set; }
        /// <summary>       
        /// 직급코드=> T_STORE_POSTITION 테이블의 POSITION_TYPE 가 1인 데이터=> 권한이 적용될 직위 일경우 콤마(,)로 이이서 등록
        /// </summary>	   
        public string COMP_POSITION_AUTH { get; set; }
        /// <summary>       
        /// 직책코드=> T_STORE_POSTITION 테이블의 POSITION_TYPE 가 2인 데이터=> 권한이 적용될 직위 일경우 콤마(,)로 이이서 등록
        /// </summary>	   
        public string COMP_TITLE_AUTH { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(0:보임 1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자 이름
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 웹메뉴 그룹(템플릿)테이블(T_STORE_WEBMENU_GROUP) END 
}

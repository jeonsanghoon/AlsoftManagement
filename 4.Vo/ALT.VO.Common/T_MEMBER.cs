using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{

    public static class VoCommonHelper
    {
        public static string ToFormatDateVo(this string value, string format = "yyyy.MM.dd")
        {
            if (value == null || value.Count() < 8) return string.Empty;
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Count() != 8) return string.Empty;

            DateTime dDate = Convert.ToDateTime(value.Substring(0, 4) + "-" + value.Substring(4, 2) +"-" + value.Substring(6, 2));
            return dDate.ToString(format);

        }
    }


    #region >> 사용자정보(T_MEMBER)
    /// <summary>       
    /// 사용자정보(T_MEMBER)
    /// </summary>	   
    public class T_MEMBER
    {
        /// <summary>
        /// N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_MODE { get; set; }
        /// <summary>       
        /// 순번(일련번호)
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// T_COMMON 테이블의 MAIN_CODE:U001 사용
        /// </summary>	   
        public int? USER_TYPE { get; set; }
        /// <summary>       
        /// 사용자아이디(E-Mail)
        /// </summary>	   
        public string USER_ID { get; set; }
        /// <summary>       
        /// 암호(SHA1으로 암호화)
        /// </summary>	   
        public string PASSWORD { get; set; }
        /// <summary>       
        /// 사용자명
        /// </summary>	   
        public string USER_NAME { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>       
        /// 일반전화
        /// </summary>	   
        public string PHONE { get; set; }
        /// <summary>       
        /// 모바일번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// 기본주소
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



        private string _birth = string.Empty;
        /// <summary>
        /// 생년월일(yyyyMMdd)
        /// </summary>
        public string BIRTH { get {

                _birth = VoCommonHelper.ToFormatDateVo(_birth);
                return _birth; } set { _birth = value; } }
        /// <summary>
        /// 성별 : T_COMMON 테이블의 MAIN_CODE : H001(1:남, 2:여) 사용 
        /// </summary>
        public int? GENDER { get; set; }
        /// <summary>       
        /// 비밀번호 변경시 URL, 요청시 EMAIL + MEMBER_CODE + USER_NAME를 SHA1으로 암호화하여 저장, 변경후 빈값으로 초기화
        /// </summary>	   
        public string PASSWORD_CHANGE_URL { get; set; }
        /// <summary>       
        /// 비밀번호 변경시 URL, 요청시 EMAIL + MEMBER_CODE + USER_NAME를 SHA1으로 암호화하여 저장, 변경후 빈값으로 초기화
        /// </summary>	   
        public DateTime? PASSWORD_AUTH_TIME { get; set; }

        public string _SHARE_AUTH_NUMBER = "00";
        /// <summary>
        /// 공유인증번호 기본:00
        /// </summary>
        public string SHARE_AUTH_NUMBER { get { return ((_SHARE_AUTH_NUMBER != null && _SHARE_AUTH_NUMBER.Count() == 2) ? _SHARE_AUTH_NUMBER : "00"); } set { _SHARE_AUTH_NUMBER = value; } }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 카카오계정로그인아이디
        /// </summary>
        public string KAKAO_ID { get; set; }
        /// <summary>
        /// 구글계정로그인아이디
        /// </summary>
        public string GOOGLE_ID { get; set; }
        /// <summary>
        /// 네이버계정로그인아이디
        /// </summary>
        public string NAVER_ID { get; set; }
        /// <summary>
        /// 페이스북계정로그인아이디
        /// </summary>
        public string FACEBOOK_ID { get; set; }
        /// <summary>
        /// 사용자프로필이미지
        /// </summary>
        public string thumnailPath { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
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
        /// 통합회원가입여부
        /// </summary>
        public Boolean? IS_EMPLOYEE { get; set; }
    }
    #endregion >> 사용자정보(T_MEMBER) END 
    public class T_MEMBER_COND {
        /// <summary>       
        /// 사용자아이디(E-Mail)
        /// </summary>	   
        public string USER_ID { get; set; }
        public int? USER_TYPE { get; set; }
        public string MOBILE { get; set; }
        public string PHONE { get; set; }
        public bool? EMPLOYEE_YN { get; set; }
        public string PASSWORD { get; set; }
        public string PASSWORD_CHANGE_URL { get; set;}
        public string KAKAO_ID { get; set; }
        public string GOOGLE_ID { get; set; }
        public string NAVER_ID { get; set; }
        public string FACEBOOK_ID { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool? HIDE { get; set; }

        public long? MEMBER_CODE { get; set; }
    }
    public class MOBILE_MEMBER_LOGIN_COND
    {
        public string USER_ID { get; set; }
        public string KAKAO_ID { get; set; }
        public string GOOGLE_ID { get; set; }
        public string NAVER_ID { get; set; }
        public string FACEBOOK_ID { get; set; }
        public string PASSWORD { get; set;  }
        public string NEW_PASSWORD { get; set; }
    }

    public class MOBILE_LOGIN_DATA
    {
        public int MEMBER_CODE { get; set; }
        public string ERROR_MESSAGE { get; set; }
        public int    ERROR_TYPE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set;  }
        public string thumnailPath { get; set; }
    }

    /// <summary>
    /// 모바일에서 회원가입시 SNS ID 업데이트시 사용(등록된 아이디가 있는데 요청하는 SNS정보가 없을 경우
    /// </summary>
    public class T_MEMBER_SNS_UPDATE
    {
        public string USER_ID {get;set;}
        public string PASSWORD { get; set; }
        public int? SNS_TYPE { get; set; } = 1;
        public string KAKAO_ID { get; set; }
        public string GOOGLE_ID { get; set; }
        public string NAVER_ID { get; set; }
        public string FACEBOOK_ID { get; set; }
        public int UPDATE_CODE { get; set; } = 0;
    }


    public class T_MEMBER_PASSWROD_CHANGE
    {
        public string USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public int? UPDATE_CODE { get; set; }
    }

}

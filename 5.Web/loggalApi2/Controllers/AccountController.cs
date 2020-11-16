using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using loggalServiceBiz;
using System.Web.Http;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework;

namespace loggalApi2.Controllers
{
    public class AccountController : BaseController
    {

        #region >>  회원정보
        /// <summary>
        /// 회원정보가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<T_MEMBER> GetMemberList([FromBody]T_MEMBER_COND Cond)
        {
            IList<T_MEMBER> list = new AccountService().GetMemberList(Cond);
            return list;
        }

        /// <summary>
        /// 모바일 로그인 회원정보가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<T_MEMBER> GetMobileLoginMemberList([FromBody]T_MEMBER_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            IList<T_MEMBER> list = new AccountService().GetMemberList(Cond);
            return list;
        }

        /// <summary>
        /// 회원정보저장
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [Compress]
        public RTN_SAVE_DATA SaveMember([FromBody]T_MEMBER Param)
        {
            Param.USER_ID = Global.SecurityInfo.Decrypt_data(Param.USER_ID);
            Param.USER_NAME = Global.SecurityInfo.Decrypt_data(Param.USER_NAME);
            Param.EMAIL = Global.SecurityInfo.Decrypt_data(Param.EMAIL);
            Param.KAKAO_ID = Global.SecurityInfo.Decrypt_data(Param.KAKAO_ID);
            Param.GOOGLE_ID = Global.SecurityInfo.Decrypt_data(Param.GOOGLE_ID);
            Param.NAVER_ID = Global.SecurityInfo.Decrypt_data(Param.NAVER_ID);
            Param.FACEBOOK_ID = Global.SecurityInfo.Decrypt_data(Param.FACEBOOK_ID);
            Param.thumnailPath = Global.SecurityInfo.Decrypt_data(Param.thumnailPath);
            
            RTN_SAVE_DATA rtnData = new AccountService().SaveMember(Param);
            return rtnData;
        }



        #endregion >> 회원정보


        #region >>  회원별 북마크
        /// <summary>
        /// 회원 북마크정보가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<T_MEMBER_BOOKMARK> GetMemberbookmarkList([FromBody]T_MEMBER_BOOKMARK_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            IList<T_MEMBER_BOOKMARK> list = new AccountService().GetMemberbookmarkList(Cond);
            return list;
        }

        /// <summary>
        /// 회원별 북마크정보저장
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA MemberbookmarkSave([FromBody]T_MEMBER_BOOKMARK Param)
        {
            Param.USER_ID = Global.SecurityInfo.Decrypt_data(Param.USER_ID);
            List<T_MEMBER_BOOKMARK> savelist = new List<T_MEMBER_BOOKMARK>();
            savelist.Add(Param);
            RTN_SAVE_DATA rtnData = new AccountService().MemberbookmarkSave(savelist);
            return rtnData;
        }

        [HttpPost]
        [DeflateCompression]
        public List<BOOKMARK_AD_LIST> GetBookmarkAdList([FromBody]BOOKMARK_AD_COND Cond)
        {
            return new AccountService().GetBookmarkAdList(Cond);
        }

        #endregion

        #region >> 모바일 로그인 정보 가져오기
        [HttpPost]
        [DeflateCompression]
        public MOBILE_LOGIN_DATA GetMobileLogin([FromBody]MOBILE_MEMBER_LOGIN_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            Cond.KAKAO_ID = Global.SecurityInfo.Decrypt_data(Cond.KAKAO_ID);
            Cond.GOOGLE_ID = Global.SecurityInfo.Decrypt_data(Cond.GOOGLE_ID);
            Cond.NAVER_ID = Global.SecurityInfo.Decrypt_data(Cond.NAVER_ID);
            Cond.FACEBOOK_ID = Global.SecurityInfo.Decrypt_data(Cond.FACEBOOK_ID);
            return new AccountService().GetMobileLogin(Cond);
        }
        #endregion
        [HttpPost]
        [DeflateCompression]
        public MOBILE_LOGIN_DATA MobilePasswordChange([FromBody]MOBILE_MEMBER_LOGIN_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            return new AccountService().MobilePasswordChange(Cond);
        }

        [HttpPost]
        [DeflateCompression]
        public IList<T_COMPANY> GetCompanyList([FromBody]T_COMPANY_COND Cond)
        {
            return new StoreService().GetCompanyList(Cond);
        }
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA MemberSnsIDUpdate(T_MEMBER_SNS_UPDATE Param)
        {
            Param.USER_ID = Global.SecurityInfo.Decrypt_data(Param.USER_ID);
            Param.KAKAO_ID = Global.SecurityInfo.Decrypt_data(Param.KAKAO_ID);

            return new AccountService().MemberSnsIDUpdate(Param);
        }

        [Compress]
        public RTN_SAVE_DATA MemberPasswordChange(T_MEMBER_PASSWROD_CHANGE Param)
        {
            string msg = string.Empty;
            Param.UPDATE_CODE = 0;
            Param.USER_ID = Global.SecurityInfo.Decrypt_data(Param.USER_ID);
            return new AccountService().MemberPasswordChange(Param);
           
        }
    }
}
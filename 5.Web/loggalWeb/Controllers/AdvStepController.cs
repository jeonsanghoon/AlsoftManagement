using ALT.Framework.Data;
using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using loggalWeb.CommonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWeb.Controllers
{
    public class AdvStepController : BaseController
    {
        // GET: AdvStep
        [Compress]
        public ActionResult Index()
        {
            return View();
        }

        [Compress]
        public ActionResult Step1()
        {
            return View();
        }

        /// <summary>
        /// 현재 광고의 Status ViewBag에 저장
        /// </summary>
        /// <param name="id"></param>
        [Compress]
        public void SetpStatus(long? id)
        {
            id = (id == null) ? ((SessionHelper.LoginInfo.AD_CODE ==null)? 0 : SessionHelper.LoginInfo.AD_CODE) : id;
            SessionHelper.LoginInfo.AD_CODE = id;
            T_AD data = new AdvertisingService().GetT_AD_List((int)id).FirstOrDefault();
            if (data == null) data = new T_AD() { AD_CODE = 0, STATUS = 2 };
            data.AD_CODE = (int)id;
            ViewBag.data = data;
        }

        [Compress]
        public JsonResult Step1Save(T_MEMBER Param)
        {
            string msg = string.Empty;
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            Param.BIRTH = Param.BIRTH.RemoveDateString();
            Param.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            RTN_SAVE_DATA data = new AccountService().SaveMember(Param);
            data.RETURN_URL = "/advstep/step2";
            IList<T_MEMBER> list = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID });
            SessionHelper.LoginInfo.MEMBER = list.First();

            return new JsonResult { Data = data };
        }

        [Compress]
        public ActionResult Step2(long? id = null)
        {
            this.SetpStatus(id);
            return View();
        }


        [Compress]
        public JsonResult Step2Save(T_AD Param)
        {

            T_AD dataSel = new AdvertisingService().GetT_AD_List((int)Param.AD_CODE).FirstOrDefault();

            if(dataSel == null)
            {
                dataSel = new T_AD();
            }
            dataSel.TITLE = Param.TITLE;
            dataSel.SUB_TITLE = Param.SUB_TITLE;
            dataSel.LOGO_URL = Param.LOGO_URL;
            dataSel.CONTENT = Param.CONTENT;
            dataSel.STATUS = Param.STATUS;
            dataSel.INSERT_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            dataSel.MEMBER_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = new AdvertisingService().T_AD_Save(dataSel);
            T_COMMON COMM = new BaseService().GetCommon(new T_COMMON_COND { MAIN_CODE = "A001", SUB_CODE = Param.STATUS }).FirstOrDefault();
            if (COMM != null) data.RETURN_URL = COMM.REF_DATA1.Split('|')[1]+ "?id=" + data.DATA;
            SessionHelper.LoginInfo.AD_CODE = Convert.ToInt32(data.DATA);

            return new JsonResult { Data = data };
        }

        [Compress]
        public ActionResult step3(long? id = null)
        {
            this.SetpStatus(id);
            id = (id == null ? SessionHelper.LoginInfo.AD_CODE : id);
            ViewBag.KeywordList = new KeywordService().GetAdDeviceSearchKeyword(new CATEGORY_KEYWORD_COND { AD_CODE = id });
            return View();
        }


        [Compress]
        public JsonResult Step3Save(STEP3_SAVE Param)
        {
            Param.REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = new AdvertisingService().Step3_Save(Param);
            data.RETURN_URL = "/advstep/step4?id=" + Param.AD_CODE.ToString();
            return new JsonResult { Data = data };
        }


        [Compress]
        public ActionResult step4(int? id = null)
        {
            this.SetpStatus(id);
            return View();
        }

        [Compress]
        public PartialViewResult PV_Step4list(STEP_LOCAL_COND Param)
        {
            Param.AD_CODE = SessionHelper.LoginInfo.AD_CODE;
            ViewBag.list = new RequestADService().GetStep4localAddList(Param);
            return PartialView("~/Views/AdvStep/Partial/PV_Step4list.cshtml");
        }



        [Compress]
        public PartialViewResult PV_Step4list2(STEP_LOCAL_COND Param)
        {
            Param.SEARCH_CODE = SessionHelper.LoginInfo.AD_CODE.ToString();
            ViewBag.list = new RequestADService().GetStep4localAddList2(Param);
            return PartialView("~/Views/AdvStep/Partial/PV_Step4list2.cshtml");
        }

        [Compress]
        public JsonResult Step4Save(STEP4_SAVE Param)
        {
            Param.AD_CODE = SessionHelper.LoginInfo.AD_CODE;
            Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = new RequestADService().Step4Save(Param);
            if(Param.SAVE_TYPE=="SAVE") data.RETURN_URL = "/advstep/step5?id=" + Param.AD_CODE.ToString();
            return new JsonResult { Data = data };
        }


        [Compress]
        public ActionResult step5(long? id = null)
        {
            this.SetpStatus(id);
            return View();
        }

        [Compress]
        public JsonResult Step5Save()
        {
            RTN_SAVE_DATA data = new RequestADService().Step5Save(new STEP5_SAVE { AD_CODE = SessionHelper.LoginInfo.AD_CODE, REG_CODE=SessionHelper.LoginInfo.MEMBER.MEMBER_CODE });
            data.RETURN_URL = "/advstep/steplist";
            return new JsonResult { Data = data };
        }

        [Compress]
        public ActionResult StepList()
        {
            return View();
        }

        [Compress]
        public PartialViewResult PV_Steplist(STEPLIST_COND Cond)
        {
            Cond.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            ViewBag.list = new RequestADService().GetStepList(Cond);
            return PartialView("~/Views/AdvStep/Partial/PV_Steplist.cshtml");
        }


        [Compress]
        public JsonResult AdDelete(long[] AD_CODE)
        {
           
            RTN_SAVE_DATA data = new AdvertisingService().AdDelete(AD_CODE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
          
            return new JsonResult { Data = data };
        }


        [Compress]
        public ActionResult MemberModify()
        {
            return View();
        }

        [Compress]
        public JsonResult MemberModifySave(T_MEMBER Param)
        {
            string msg = string.Empty;
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            Param.BIRTH = Param.BIRTH.RemoveDateString();

            T_MEMBER  memData = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID }).FirstOrDefault();
            RTN_SAVE_DATA data = new RTN_SAVE_DATA();
            if (memData.PASSWORD != Param.PASSWORD)
            {
                 data = new RTN_SAVE_DATA { ERROR_MESSAGE = "비밀번호가 맞지 않습니다.", FOCUS_TAG_ID = "PASSWORD" };
                return new JsonResult { Data = data };
            }
            
            Param.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            Param.MEMBER_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            data = new AccountService().SaveMember(Param);
            data.RETURN_URL = "/advstep/steplist";

            memData = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID }).FirstOrDefault();
             SessionHelper.LoginInfo.MEMBER = memData;

            return new JsonResult { Data = data };
        }

        [Compress]
        public PartialViewResult PV_Step4RegionList(PAGE_COND Cond)
        {
            Cond.CODE = (long)SessionHelper.LoginInfo.AD_CODE;
            ViewBag.list = new AdvertisingService().GetAdRegionList(Cond);

            return PartialView("~/Views/AdvStep/Partial/PV_Step4RegionList.cshtml");
        }

        [Compress]
        public JsonResult AdRegionSave(List<AD_REGION> list)
        {

            RTN_SAVE_DATA data = new AdvertisingService().AdRegionSave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE );

            return new JsonResult { Data = data };
        }
    }
}
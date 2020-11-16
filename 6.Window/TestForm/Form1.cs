using ALT.BizService;
using ALT.Framework;
using ALT.Framework.Mvc;
using ALT.VO.loggal;
using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
  
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

			string lat = "38", longi = "127";
			//dynamic data = Global.WcfRestService.GetRestGetService<dynamic>( "https://localhost:44324/api/common/GetCoord2address?lat=") + lat + "& longi=" + longi +" + " & output=json");


			var a = string.Empty;
			/* T_AD_PLAY_LOG_COND Cond = new T_AD_PLAY_LOG_COND() {
				 PAGE = 1, PAGE_COUNT = 10, TITLE = "스", SORT_ORDER="TITLE DESC"
			 };

			 var rtn = new MongoDBService().MongoDBToSqlServer("20190514", "20190514");
			 */
			/* foreach (var data in rtn.list)
			 {

			 }*/
			/*var complist =   new BasicService().GetCompanyList(new ALT.VO.Common.T_COMPANY_COND() { PAGE_COUNT = 100000 }).Where(w=>w.LONGITUDE == null && !string.IsNullOrEmpty(w.ADDRESS1));

            foreach (var data in complist)
            {
                dynamic rtn = GlobalMvc.WebService.GetAPIServer<dynamic>("https://apis.daum.net/local/geo/addr2coord?apikey=" + DaumKey + "&q=" + data.ADDRESS1 + "&output=json");
                try
                {
                    data.LATITUDE = Convert.ToDecimal(rtn["channel"]["item"][0]["lat"]);
                    data.LONGITUDE = Convert.ToDecimal(rtn["channel"]["item"][0]["lng"]);
                    new BasicService().CompanySave(data);
                }
                catch (Exception) { }
            }

            var storelist = new BasicService().GetStoreList(new ALT.VO.Common.T_STORE_COND() { PAGE_COUNT = 100000 }).Where(w => w.LONGITUDE == null && !string.IsNullOrEmpty(w.ADDRESS1));
            foreach (var data in storelist)
            {
                dynamic rtn = GlobalMvc.WebService.GetAPIServer<dynamic>("https://apis.daum.net/local/geo/addr2coord?apikey=" + DaumKey + "&q=" + data.ADDRESS1 + "&output=json");
                try
                {
                    data.LATITUDE = Convert.ToDecimal(rtn["channel"]["item"][0]["lat"]);
                    data.LONGITUDE = Convert.ToDecimal(rtn["channel"]["item"][0]["lng"]);
                    new BasicService().StoreSave(data);
                }
                catch (Exception) { }

                
               
            }*/
		}
    }
}

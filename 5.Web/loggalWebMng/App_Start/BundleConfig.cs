using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace loggalWebMng.App_Start
{
	public class BundleConfig
	{
		// 번들 작성에 대한 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=301862 링크를 참조하십시오.
		public static void RegisterBundles(BundleCollection bundles)
		{
			Bundle cssBundle = new StyleBundle("~/BundleStyle").Include("~/Common/plugin/css/easy-autocomplete/easy-autocomplete.css"
																	  , "~/Common/plugin/css/easy-autocomplete/easy-autocomplete.themes.min.css"
																	  , "~/Common/plugin/css/jquery.datetimepicker.min.css"
																	  , "~/Common/plugin/css/messagebox.css"
																	  , "~/Common/plugin/css/uploadfile.css"
																	  , "~/Common/css/style.css"
																	  , "~/Common/plugin/semantic-ui/semantic-common.css"
                                                                      , "~/Common/plugin/css/jquery-tokeninput/token-input.css"
																	  , "~/Common/plugin/css/jquery-tokeninput/token-input-mac.css"
																	  , "~/Common/plugin/css/jquery-tokeninput/token-input-facebook.css"
																	  , "~/Common/plugin/css/MrcUI/Mrc.DataGrid.css"
																	  , "~/Common/plugin/css/MrcUI/Mrc.DataGrid.fixedHeader.css"
																	  , "~/Common/plugin/css/tinymce/tinymce_common.css"
																	);
			cssBundle.Transforms.Add(new CssMinify());
			cssBundle.Transforms.Add(new FileHashVersionBundleTransform());
			bundles.Add(cssBundle);

			Bundle scriptBundle = new ScriptBundle("~/BundleScript").Include("~/Common/plugin/js/jquery.uploadfile.min.js"
																			 , "~/Common/plugin/js/moment.js"
																			 , "~/Common/plugin/js/messagebox.min.js"
																			 , "~/Common/plugin/js/jquery-tokeninput/jquery.tokeninput.js"
																			 , "~/Common/plugin/js/MrcUI/freeze-table.js"
																			 , "~/Common/plugin/js/jquery.number.min.js"
																			 , "~/Common/js/jquery.fullscreen-min.js"
																			 , "~/Common/plugin/js/security/crypto-js.min.js"


																	);
			scriptBundle.Transforms.Add(new CssMinify());
			scriptBundle.Transforms.Add(new FileHashVersionBundleTransform());
			bundles.Add(scriptBundle);


			BundleTable.EnableOptimizations = true;

		}
	}
}
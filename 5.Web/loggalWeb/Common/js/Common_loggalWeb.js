//// 로그인정보
var login = {
    doLogin: function (formObjName, returnUrl, baseUrl) {
        var params = BaseCommon.FormInfo.serializeJson(formObjName, "LOGIN_");

        if (returnUrl == undefined) returnUrl = "";
        if (baseUrl == undefined) baseUrl = "";

        params = JSON.stringify(params);

        ajax.GetAjax(baseUrl + "/account/doLogin", params, "json", function (result) {
            if (result.error_message != "") {
                $.MessageBox(result.error_message);
            } else {
                //location.href = String(result.returnUrl).toLowerCase();
              
                var sParam = result.returnUrl.split('?');
                var url = result.returnUrl.split('?')[0];
                sParam = sParam.length == 2 ? result.returnUrl.split('?')[1] : "";
                var arrParam = sParam.split("&");
                var objParam = new Object();
                if (sParam != "")
                {
               
                    $.each(arrParam, function (i, val) {
                        objParam.name = val.split('=')[0];
                        objParam.value = val.split('=')[1];
                        arrParam[i] = objParam;
                        
                        //objParam[arrParam[i].name] = arrParam[i].value;
                    });
                }
                
                //history.pushState({ data: null }, $('title').text(), location.href);
                BaseCommon.FormInfo.PostSubmit(url, arrParam);

            }
        })
    },doLogin2: function (id,password, returnUrl, baseUrl) {
        var params = new Object();
        params.USER_ID = id;
        params.PASSWORD = password;

        if (returnUrl == undefined) returnUrl = "";
        if (baseUrl == undefined) baseUrl = "";

        params = JSON.stringify(params);

        ajax.GetAjax(baseUrl + "/account/doLogin", params, "json", function (result) {
            if (result.error_message != "") {
                $.MessageBox(result.error_message);
            } else {
                location.href = String(result.returnUrl).toLowerCase();
            }
        })
},
    doLogout: function (baseUrl) {
        if(baseUrl == undefined) baseUrl = "";
        ajax.GetAjax(baseUrl + "/account/doLogOut", "", "json", function (result) {
            location.href = result.returnUrl;
        });
    }
}
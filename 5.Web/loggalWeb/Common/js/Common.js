/*************/
/* Ajax 관련 */
/*************/
var ajax =
{

    /* dateType : html/ */
    GetAjax: function (url, params, dateType, callback, basync, errorcallback, bloadingbar) {
        if (dateType == undefined || $.trim(dateType) == "") {
            dateType = "json";
        }
        else
            dateType = dateType;

        //// true 일 경우 비동기 false일 경우 동기
        if (basync == undefined) basync = true;
        var tmpParam;
        try {
            tmpParam = JSON.parse(params);
        } catch (e) { }

        bloadingbar = (bloadingbar == undefined ? true : bloadingbar);

        $.ajax({
            type: "POST",
            url: url,
            timeout: 30000,
            data: params,
            cache: false,
            async: basync,
            dataType: dateType,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                try {
                    $("button").removeClass("disabled");
                } catch (e) { }
                if (result != null && result != "") {
                    try {
                        if (JSON.parse(result).logout != undefined && JSON.parse(result).logout == true) {
                            $.MessageBox("세션이 만료되어 로그아웃처리 됩니다.").done(function () {
                                BaseCommon.LoginInfo.doLogOut();
                            });
                        }
                    } catch (e) { }
                }

                try {
                    if (dateType == "html") {
                        Setfooter();

                        $('input[type="checkbox"]').addClass('ui mini checkbox');

                    }
                } catch (e) { }

                callback(result);
            },
            error: function (e) {

                //bootbox.alert('Error:' + e.status + " - " + e.statusText);
                if (bloadingbar == true) {
                    try {
                        $.MessageBox('Error:' + e.status + " - " + e.statusText);
                    } catch (e) { }
                    try {
                        errorcallback(null);
                    } catch (e) { }
                }
            }, beforeSend: function () {
                try {
                    if (bloadingbar == true) {
                        $("#dvLoading").removeClass("active");
                        $("#dvLoading").addClass("active");
                    }
                } catch (e) { }
                // Handle the beforeSend event
            }, complete: function () {
                try {
                    $("button").removeAttr("disabled");
                } catch (e) { }
                try {
                    if (bloadingbar == true) {
                        $("#dvLoading").removeClass("active");
                    }
                } catch (e) { }
            }
        });
    }
}
var Input = {
    inputForm: {
        makeForm: function (ActionURL) {
            var frm = document.createElement("form");
            frm.setAttribute("method", "post");
            if (!(ActionURL == undefined || ActionURL == "")) {
                frm.setAttribute("action", ActionURL);
            }
            document.body.appendChild(frm);
            return frm;
        }
        , AddData: function (name, value, frm) {
            var i = document.createElement("input");
            i.setAttribute("type", "hidden");
            i.setAttribute("name", name);
            i.setAttribute("value", value);
            frm.appendChild(i);
        }
    },
    /***************/
    /* 라디오 버튼 */
    /***************/
    RadioButton:
    {
        CheckedVal: function (inputName) {

            var rtnVal = "";

            rtnVal = ($(':radio[name="' + inputName + '"]:checked').val() == undefined) ? "" : $(':radio[name="' + inputName + '"]:checked').val();
            return rtnVal;

        }
        , SetCheck: function (inputName, bChk, val1) {
            if (val1 == undefined)
                $('input:radio[name=' + inputName + ']').prop("checked", bChk);
            else {
                //$('input[name="' + inputName + '"][value="' + val1 + '"]').prop('checked',  bChk);
                $('input:radio[name=' + inputName + ']:input[value="' + val1 + '"]').prop("checked", bChk);
            }
        },
        isCheck: function (inputName) {
        }
    }
    /*******************/
    /* ComboBox-Select */
    /*******************/
    , ComboBox:
    {
        SetValue: function (objName, val) { //Loop through sequentially//
            $(objName + " option").each(function () {
                if ($(this).val() == val) {
                    $(this).attr('selected', 'selected');
                }
            });
        },
        SetText: function (objName, text) {
            $(objName + " option").each(function () {
                if ($(this).text() == text) {
                    $(this).attr('selected', 'selected');
                }
            });
        }
        , GetText: function (objName) {
            return $(objName + " option:selected").text();
        }
    }

    /***************/
    /* 체크박스 */
    /***************/
    , CheckBox:
    {
        isChecked: function (objId, objName) {
            objName = objName = objName == undefined ? "" : objName + " ";
            return $(objName + "input:checkbox[id='" + objId + "']").is(":checked");
        }, Checked: function (inputName, bChk) {
            try {
                $("input:checkbox[id='" + inputName + "']").attr('checked', bChk);
            } catch (e) { }
            try {
                $("input:checkbox[id='" + inputName + "']").prop('checked', bChk);
            } catch (e) { }
        }, CheckedAll: function (inputName, bChk, objName) {
            try {
                objName = objName == undefined ? "" : objName + " ";
                // objName=> Table Or div 등등
                $(objName + "input[name='" + inputName + "']").each(function () {
                    try {
                        $(this).attr("checked", bChk);;
                    } catch (e) { }
                    try {
                        if (bChk)
                            $(this).prop("checked", true);
                        else $(this).prop("checked", false);
                    } catch (e) { }

                });
            } catch (e) { }
        }, CheckedVal: function (objId) {
            if (objId.indexOf("#") < 0) objId = "#" + objId;
            if ($(objId + ":checked").val() == undefined) {
                return null;
            }
            return $(objId + ":checked").val();
        }, CheckedValues: function (inputName, objName) {
            objName = objName == undefined ? "" : objName + " ";
            var checked = []
            $(objName + "input[name='" + inputName + "']:checked").each(function () {
                checked.push($(this).val());
            });
            return checked;
        }, CheckedStringValues: function (inputName, objName) {
            objName = objName == undefined ? "" : objName + " ";
            var rtnValue = "";
            $(objName + "input[name='" + inputName + "']:checked").each(function () {
                rtnValue = rtnValue + $(this).val() + "|";
            });
            rtnValue = rtnValue.substr(0, rtnValue.length - 1);
            return rtnValue;
        }
    }
    /*******************/
    /* Number Box 정의 */
    /*******************/
    , NumberBox:
    {
        Init: function (objName) {

            try {
                $(objName).keydown(function (event) {
                    if (window.event) { // IE,  Chrome,  Safari
                        keynum = event.keyCode;
                    }
                    if (event.which) {    // Firefox,  Opera,  Netscape,  safari
                        keynum = event.which;
                    }
                    if (!(keynum == 8 || keynum == 9) && ((keynum < 48) || (keynum > 57))) {
                        event.returnValue = false;
                        return false;
                    }
                });
                if ((keynum < 48) || (keynum > 57))
                    event.returnValue = false;
            } catch (e) { }
            $(objName).blur(function (event) {
                $(this).val(Input.TextBox.commaNum($(this).val()));
            });
            $(objName).focus(function (event) {
                $(this).select();
                var tmpData = BaseCommon.StringInfo.replaceAll($(this).val(), ",", "");
                tmpData = BaseCommon.StringInfo.replaceAll(tmpData, " ", "");
                $(this).val(tmpData);
            });
            var tmpData = Input.NumberBox.RemoveFormat(objName);
            Input.NumberBox.commaNum(objName);
        }, Focus: function (objName) {
            $(objName).focus(function (event) {
                $(this).select();
                Input.NumberBox.RemoveFormat(objName);
            });
        }, RemoveFormat: function (objName, bData) {

            var tmpData = BaseCommon.StringInfo.replaceAll($(objName).val(), ",", "");
            tmpData = BaseCommon.StringInfo.replaceAll(tmpData, " ", "");
            if (bData == undefined)
                $(objName).val(tmpData);
            else
                return tmpData;
        }, commaNum: function (objName, digits) {

            var num = $(objName).val();
            if (num == undefined || isNaN(num)) {
                num = "0";
            }
            else {
                var tmpData = BaseCommon.StringInfo.replaceAll(num, ",", "");
                tmpData = BaseCommon.StringInfo.replaceAll(tmpData, " ", "");
                num = tmpData;
                if (digits != undefined && digits > 0) {
                    num = eval(num, "{0:" + digits + 1 + "}");
                }
                else {
                    num = Math.floor(num);
                }
            }
            var parts = num.toString().split(".");
            num = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ", ") + (parts[1] ? "." + parts[1] : "")

            $(objName).val(num);
        }, Round: function (val, digits) {
            return Math.round(val * Math.pow(10, digits)) / Math.pow(10, digits);
        }
    }
    /***************/
    /*Text박스 */
    /***************/
    , TextBox:
    {
        readOnly: function (objName, bChk) {
            if (bChk == undefined) bChk = true;
            $(objName).prop("readonly", bChk);
        }
        , notHangul: function (objName) {
            $(objName).keyup(function (event) {
                if (window.event) { // IE,  Chrome,  Safari
                    keynum = event.keyCode;
                }
                if (event.which) {    // Firefox,  Opera,  Netscape,  safari
                    keynum = event.which;
                }
                if (!(keynum >= 37 && keynum <= 40)) {
                    var inputVal = $(this).val();
                    $(this).val(inputVal.replace(/[^a-z0-9]/gi, ''));
                }
            });
        }
        , onlyNumber: function (objName) {
            try {

                $(objName).keydown(function (event) {
                    if (window.event) { // IE,  Chrome,  Safari
                        keynum = event.keyCode;
                    }
                    if (event.which) {    // Firefox,  Opera,  Netscape,  safari
                        keynum = event.which;
                    }
                    if (!((keynum == 189 && $(this).val() == "") || keynum == 8 || keynum == 9) && ((keynum < 48) || (keynum > 57))) {
                        event.returnValue = false;
                        return false;
                    }
                });
                if ((keynum < 48) || (keynum > 57))
                    event.returnValue = false;
            } catch (e) { }
        }, blurNumber: function (obj) {
            if (isNaN($(obj).val())) {
                $(obj).val("0");
            }
        }
        , commaNum: function (num, digits) {
            if (isNaN(num)) {
                num = "0";
            }
            else {
                if (digits != undefined && digits > 0) {
                    num = eval(num, "{0:" + digits + 1 + "}");
                }
                else {
                    num = Math.floor(num);
                }
            }
            var parts = num.toString().split(".");
            num = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ", ") + (parts[1] ? "." + parts[1] : "")

            return num;
        }, Round: function (val, digits) {
            return Math.round(val * Math.pow(10, digits)) / Math.pow(10, digits);
        }, NumberBox:
        {
            increaseNum: 1
            , minNumber: 1
            , maxNumber: 100

            , getVal: function (id) {
                return BaseCommon.StringInfo.replaceAll($("#" + id).val(), ', ', '');
            },
            setVal: function (objName, num, digits) {

                if (isNaN(num)) {
                    num = 0;
                    $(objName).val("0");
                }
                else {
                    if (num <= Input.TextBox.NumberBox.maxNumber
                        && num >= Input.TextBox.NumberBox.minNumber
                    ) $(objName).val(Input.TextBox.commaNum(num, digits));
                }

            }
            ,
            Init: function (id, digits) {

                $("#" + id + "_UP").click(function () {
                    Input.TextBox.NumberBox.setVal("#" + id, String(Number(Input.TextBox.NumberBox.getVal(id)) + Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id + "_DOWN").click(function () {
                    Input.TextBox.NumberBox.setVal("#" + id, String(Number(Input.TextBox.NumberBox.getVal(id)) - Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id).keydown(function (event) {
                    if (((event.keyCode < 48) || (event.keyCode > 57)))
                        event.preventDefault();
                });
                $("#" + id).blur(function (event) {
                    if (isNaN($("#" + id).val())) {
                        $("#" + id).val("0");
                    }
                    else {
                        var num = $("#" + id).val();

                        if (num > Input.TextBox.NumberBox.maxNumber) $("#" + id).val(String(Input.TextBox.NumberBox.maxNumber));
                        else if (num < Input.TextBox.NumberBox.minNumber) $("#" + id).val(String(Input.TextBox.NumberBox.minNumber));

                    }
                });

                $("#" + id).focus(function (event) {
                    $("#" + id).val(BaseCommon.StringInfo.replaceAll($("#" + id).val(), ', ', ''));
                    $("#" + id).select();
                });
            }
        }

    }
    , Image: {
        GetImageSize: function (url, callback) {
            var img = new Image;
            img.src = url;
            img.onload = function () { callback(this.width, this.height); };
        }
        , GetImageObjectSize: function (img, callback) {
            img.onload = function () { callback(this.width, this.height); };
        }
    }
}


var BaseCommon =
{
    ArrayInfo: {

        Remove: function (arrData, index) {
            var rtn = [];
            for (var i = 0; i < arrData.length; i++) {
                if (i == index) { continue; }
                rtn.push(arrData[i]);
            }

            return rtn;
        }
    },
    LoadingBar: {
        Show: function () {
            $("#dvLoading").removeClass("active");
            $("#dvLoading").addClass("active");

        }, Hide: function () {
            $("#dvLoading").removeClass("active");

        }
    }, ExistsScript: function (src) {
        return document.querySelectorAll('script[src="${src}"]').length > 0;
    },
    AddScript: function (src) {
        if (!BaseCommon.ExistsScript(src)) {
            var s = document.createElement('script');
            s.setAttribute('src', src);
            document.body.appendChild(s);
        }
    },
    ToDefault: function (data, defaultVal) {
        return (data == undefined || data == null) ? defaultVal : data;
    },
    SetHtml: function (el, html) {
        var oldEl = (typeof el === "string" ? document.getElementById(el) : el);

        var newEl = oldEl.cloneNode(false);
        newEl.innerHTML = html;
        oldEl.parentNode.replaceChild(newEl, oldEl);
        /* Since we just removed the old element from the DOM, return a reference
        to the new element, which can be used to restore variable references. */
        return newEl;
    },
    HostInfo: {
        Host: function () {
            return $(location).attr("host");
        }
        , Pathname: function () {
            return $(location).attr("pathname");
        }
        , Srarch: function () {
            return $(location).attr("search");
        }

    }, LoginInfo: {
        doLogOut: function () {

            ajax.GetAjax("/account/doLogOut", null, "json", function (result) {
                location.href = "/home/login";
            })
        }
    }

    /****************/
    /*   서버정보   */
    /****************/
    , ServerInfo:
    {
        Host: function () {
            var Dns;
            Dns = location.href;
            Dns = Dns.split("//");
            Dns = Dns[1].substr(0, Dns[1].indexOf("/"));
            return Dns;
        },
        getBrowser: function () {

            var agt = navigator.userAgent.toLowerCase();
            if (agt.indexOf("chrome") != -1) return 'chrome';
            if (agt.indexOf("opera") != -1) return 'Opera';
            if (agt.indexOf("staroffice") != -1) return 'Star Office';
            if (agt.indexOf("webtv") != -1) return 'WebTV';
            if (agt.indexOf("beonex") != -1) return 'Beonex';
            if (agt.indexOf("chimera") != -1) return 'Chimera';
            if (agt.indexOf("netpositive") != -1) return 'NetPositive';
            if (agt.indexOf("phoenix") != -1) return 'Phoenix';
            if (agt.indexOf("firefox") != -1) return 'Firefox';
            if (agt.indexOf("safari") != -1) return 'Safari';
            if (agt.indexOf("skipstone") != -1) return 'SkipStone';
            if (agt.indexOf("msie") != -1) return 'msie';
            if (agt.indexOf("netscape") != -1) return 'Netscape';
            if (agt.indexOf("mozilla/5.0") != -1) return 'Mozilla';
            if (agt.indexOf('\/') != -1) {
                if (agt.substr(0, agt.indexOf('\/')) != 'mozilla') {
                    return navigator.userAgent.substr(0, agt.indexOf('\/'));
                } else
                    return 'Netscape';
            }
            else if (agt.indexOf(' ') != -1) return navigator.userAgent.substr(0, agt.indexOf(' '));
            else return navigator.userAgent;
        }, getLanguage: function () {
            var type1 = navigator.appName;
            var lang = "";

            if (type1 == "Netscape")
                lang = navigator.language.split('-')[0];
            else
                lang = navigator.userLanguage;
            return lang;

        }
    }
    , FormInfo:
    {
        IsVisible: function (obj) {
            if ($(obj).css('display') != undefined && ($(obj).css('display') == 'none' || $(obj).css("visibility") == "hidden")) {
                return false;
            }
            else return true;

        }
        , InputsToJson: function (id) {
            var data = $("#" + id).serializeArray();

            return JSON.stringify(BaseCommon.FormInfo.arrayToJSON(data));
        }
        , arrayToJSON: function (serializedForm, replaceData) {
            var data = {};
            if (replaceData == undefined) replaceData = "";
            for (var cnt in serializedForm) {

                if (data[serializedForm[cnt].name.replace(replaceData, "")] == undefined) {
                    var objData = $("input[name=" + serializedForm[cnt].name + "]");
                    if (objData.length == 0) {
                        objData = $("#" + serializedForm[cnt].name)
                    }
                    if (objData.hasClass("datetime") == true) {
                        var dateFormat = "YYYY-MM-DD HH:mm";
                        var dChk = moment(objData.datetimepicker('getValue'), dateFormat);
                        if (dChk.isValid()) {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = moment(objData.datetimepicker('getValue')).format(dateFormat);
                        }
                        else data[serializedForm[cnt].name.replace(replaceData, "")] = "";

                    } else if (objData.hasClass("date") == true) {
                        var dateFormat = "YYYY-MM-DD";
                        var dChk = moment(objData.datetimepicker('getValue'), dateFormat);
                        if (dChk.isValid()) {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = moment(objData.datetimepicker('getValue')).format("YYYY-MM-DD");
                        }
                        else data[serializedForm[cnt].name.replace(replaceData, "")] = "";
                    } else if (objData.hasClass("time") == true) {
                        var dateFormat = "HH:mm";
                        var dChk = moment(objData.datetimepicker('getValue'), dateFormat);
                        if (dChk.isValid()) {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = moment(objData.datetimepicker('getValue')).format("HH:mm");
                        }
                        else data[serializedForm[cnt].name.replace(replaceData, "")] = "";
                    } else if (objData.closest(".ui.dropdown").hasClass("boolean") == true) {

                        if (serializedForm[cnt].value == "1") {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = true;
                        } else if (serializedForm[cnt].value == "0") {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = false;
                        }
                        else {
                            data[serializedForm[cnt].name.replace(replaceData, "")] = ((serializedForm[cnt].value == "") ? null : serializedForm[cnt].value);
                        }
                    }
                    else
                        data[serializedForm[cnt].name.replace(replaceData, "")] = serializedForm[cnt].value;
                }
            }
            return data;
        }, serializeJson: function (objTag, replaceData) {
            if (replaceData == undefined) replaceData = "";
            var serializedForm = $(objTag).find("input, textarea, select").serializeArray();

            return BaseCommon.FormInfo.arrayToJSON(serializedForm, replaceData);
        }, SetFormData: function (jsonData, objID, addId) {
            if (addId == undefined) addId = "";
            if (objID == undefined) objID = "body"

            $.each(jsonData, function (key, value) {
                // alert('key:' + key + ' / ' + 'value:' + value);
                key = addId + key;
                var objTag = $(objID + " #" + key);
                if (objTag.is("input")) {
                    var typeName = $(objID + " #" + key).attr("type").toLowerCase();

                    switch (typeName) {
                        case "checkbox":
                            if (value == "1" || value == true) {
                                Input.CheckBox.Checked(key, true);
                            }
                            else
                                Input.CheckBox.Checked(key, false);
                            break;
                        case "radio":
                            Input.RadioButton.SetCheck(key, true, value);
                            break;
                        default:
                            objTag.val(value);
                            break;
                    }
                }
                else if ($(objID + "#" + key).is("select")) {
                    if ($(objID + "#" + key).attr("multiple") == undefined) {
                        objTag.val(value.split(','));
                    }
                    else
                        objTag.val(value);
                }
            });
        }
        //////////////////////
        // 동적 Form Submit
        // 예제 : var arrData = new Array();
        //        arrData.push({name:"inputName1", value:"1111"});
        //        arrData.push({name:"inputName2", value:"2222"});
        //        BaseCommon.FormInfo.PostSubmit('/@SessionHelper.LoginInfo.LANGUAGE/order/index',  arrData);
        //////////////////////
        , PostSubmit: function (url, arrData, target) {
            history.pushState({ data: null }, $('title').text(), location.href);
            var $form = $('<form></form>');
            $form.attr('action', url);
            $form.attr('method', 'post');
            if (target != undefined) {
                $form.attr('target', target);
            }
            $form.appendTo('body');

            $.each(arrData, function (i, data1) {
                $form.append('<input type="hidden" value="' + data1.value + '" name="' + data1.name + '">')
            });
            $form.submit();
        }
    }

    /****************/
    /*   문자정보   */
    /****************/
    , StringInfo:  // String 관련 함수 정의
    {
        textAreaLimit: function (textid, limit, limitid) {
            $('#' + textid).keyup(function () {
                if (limitid == undefined) limitid = "sp_" + textid;
                BaseCommon.StringInfo.limitCharacters(textid, limit, limitid);
            })
        },
        // textarea id,  제한 글자 수,  입력 결과 메세지 저장 ID
        limitCharacters: function (textid, limit, limitid) { //(textBox Id,  제한글자수,  현자글자 체크 span Id) 
            // 잆력 값 저장
            var text = $('#' + textid).val();
            // 입력값 길이 저장
            var textlength = text.length;
            if (textlength > limit) {
                /*$('#' + limitid).html('글내용을 '+limit+
                '자 이상 쓸수 없습니다!');*/
                // 제한 글자 길이만큼 값 재 저장
                $('#' + limitid).html(String(limit));
                $('#' + textid).html(text.substr(0, limit));
                $('#' + textid).val(text.substr(0, limit));
                //return String(limit);
                return false;
            }
            else {
                /*$('#' + limitid).html('쓸수 있는 글자가 '+ (limit - textlength) +
                ' 자 남았습니다.');*/
                $('#' + limitid).html(String(limit));
                //return Sting(textlength);
                $('#' + limitid).html(String(textlength));
                return true;
            }
            val = BaseCommon.StringInfo.replaceAll(val, '-', '');
        }, smsByteChk: function (inputName, spanId) {
            $('#' + textid).keyup(function () {
                if (limitid == undefined) limitid = "sp_" + textid;
                BaseCommon.StringInfo.smsByteChk(textid, limit, limitid);
            })
        }
        , smsByteChk: function (inputName, spanId) {
            var totalByte = 0;
            var message = $("#" + inputName).val();
            if (spanId == undefined)
                spanId = "sp" + inputName;
            for (var i = 0; i < message.length; i++) {
                var currentByte = message.charCodeAt(i);
                if (currentByte > 128) totalByte += 2;
                else totalByte++;
            }
            // frm.messagebyte.value = totalByte;
            $("#sp").html(totalByte + " byte Sms로 전송");
            if (totalByte > limitByte) {
                $("#" + spanId).html(totalByte + " byte lms로 전송");
            }
        }
        , replaceAll: function (inputString, targetString, replacement) { //문자열 바꾸기
            var v_ret = null;
            var v_regExp = new RegExp(targetString, "g");
            v_ret = inputString.replace(v_regExp, replacement);
            return v_ret;
        }, GetCommaValue: function (n) {      //천단위 숫자변환
            var reg = /(^[+-]?\d+)(\d{3})/;   // 정규식
            n += '';                          // 숫자를 문자열로 변환

            while (reg.test(n))
                n = n.replace(reg, '$1' + ',' + '$2');

            return n;
        }, sqlInjection: function (val1) {
            val1 = BaseCommon.StringInfo.replaceAll(val1, "'", "''");
            //val1 = BaseCommon.StringInfo.replaceAll(val1,  "'",  "\"");
            return val1;
        }
    }
    /****************/
    /*   객체정보   */
    /****************/
    , ObjectInfo:
    {

        // 오브젝트 카피
        CopyObject: function (obj) {
            return JSON.parse(JSON.stringify(obj));
        }
        , union_arrays: function (x, y) {//Json Union
            var res;
            if (x.length == 0) return y;
            else if (y.length == 0) return x;
            res = x;
            for (var i = 0; i < y.length; i++) {
                res.push(y[i]);
            }

            return res;
        }
    }

    /****************/
    /*   날짜정보   */
    /****************/
    , DateInfo:
    {
        /*********************************************/
        /* Json으로 넘어온 데이터를 Date형으로 변경  */
        /*********************************************/
        ConvertJsonToDate: function (val) {
            if (val == undefined || val == null) {
                return "";
            }
            val = val.replace(/[^0-9 +]/g, '');
            return new Date(parseInt(val));
        }
        , RemoveDateFormat: function (val) {
            val = BaseCommon.StringInfo.replaceAll(val, '-', '');
            val = BaseCommon.StringInfo.replaceAll(val, '/', '');
            val = val.replace('.', '').replace('.', '');
            return val;
        },
        ConvertDate: function (val) {
            val = (val == null || val.trim() == "") ? "" : val;
            if (val == null || val.length < 6 || isNaN(val)) return null;

            if (val.length == 6) {
                val = val + "01";
            }

            if (val.length < 9) {
                var YYYY = val.substring(0, 4);
                var mm = val.substring(4, 6);
                var dd = val.substring(6, 8);
                val = YYYY + "-" + mm + "-" + dd + " 00:00:00";
            }

            var sDate = val.split(' ')[0];
            var sHms = val.split(' ')[1]

            return new Date(sDate.split('-')[0], Number(sDate.split('-')[1]) - 1, sDate.split('-')[2], sHms.split(':')[0], sHms.split(':')[1], sHms.split(':')[2]);
        },
        ConvertDateTime: function (sTime, sDate) {
            if (sTime == null || sTime == "" || isNaN(sTime) || sTime.length < 4) return null;
            if (sTime.length == 5) sTime = BaseCommon.StringInfo.replaceAll(sTime, ":", "");
            sDate = sDate == undefined ? this.NowDate("") : sDate;
            return new Date(sDate.substr(0, 4), Number(sDate.substr(4, 2)) - 1, sDate.substr(6, 2), sTime.substr(0, 2), sTime.substr(3, 2));
        },
        NowDate: function (delimiter) {
            var date = new Date();
            return BaseCommon.DateInfo.SetFormatDate(date, delimiter);
        },
        SetFormatDate: function (date, delimiter) {

            if (delimiter == undefined)
                delimiter = "/";

            date = new Date(date);
            var year, month, day;
            year = date.getFullYear();
            month = (String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1);
            day = String(date.getDate()).length == "1" ? "0" + String(date.getDate()) : String(date.getDate());

            switch (delimiter) {
                case "/":
                    return month + "/" + day + "/" + year;
                    break;
                case "-":
                    return year + "-" + month + "-" + day;
                    break;
                case "":
                    return year + month + day;
                    break;
                case "|":
                    return year + month + day;
                    break;
                case "d":
                default:
                    return year + "/" + month + "/" + day;
                    break;

            }

        },

        SetFormatDateByMoment: function (obj, formatstring) {
            if ($(obj).datetimepicker('getValue') == null) {
                return "";
            }
            else {
                return moment($(obj).datetimepicker('getValue')).format(formatstring);
            }

        },
        /////////////////////////////////////////
        // ex) BaseCommon.DateInfo.dateAdd(구분자,  시간,  add 값)
        //     BaseCommon.DateInfo.dateAdd('minute',  d,  30); 
        /////////////////////////////////////////
        dateAdd: function (interval, date, units) {

            var ret = new Date(date); //don't change original date
            switch (interval.toLowerCase()) {
                case 'year': ret.setFullYear(ret.getFullYear() + units); break;
                case 'quarter': ret.setMonth(ret.getMonth() + 3 * units); break;
                case 'month': ret.setMonth(ret.getMonth() + units); break;
                case 'week': ret.setDate(ret.getDate() + 7 * units); break;
                case 'day': ret.setDate(ret.getDate() + units); break;
                case 'hour': ret.setTime(ret.getTime() + units * 3600000); break;
                case 'minute': ret.setTime(ret.getTime() + units * 60000); break;
                case 'second': ret.setTime(ret.getTime() + units * 1000); break;
                default: ret = undefined; break;
            }
            return ret;
        },
        dateSubtract: function (interval, date, units) {

            var ret = new Date(date); //don't change original date
            switch (interval.toLowerCase()) {
                case 'hour': ret.setTime(ret.getTime() - units * 3600000); break;
                case 'minute': ret.setTime(ret.getTime() - units * 60000); break;
                case 'second': ret.setTime(ret.getTime() - units * 1000); break;
                default: ret = undefined; break;
            }
            return ret;
        },
        SetDayText: function (day) {
            var ret;

            switch (day) {
                case 0:
                    return ret = "Sunday"
                    break;
                case 1:
                    return ret = "Monday"
                    break;
                case 2:
                    return ret = "Tuesday"
                    break;
                case 3:
                    return ret = "Wednesday"
                    break;
                case 4:
                    return ret = "Thursday"
                    break;
                case 5:
                    return ret = "Friday"
                    break;
                case 6:
                    return ret = "Saturday"
                    break;
            }
            return ret
        }


    }
    /****************/
    /*   시간정보   */
    /****************/
    , TimeInfo:
    {
        /************************************************************/
        /*   시간지연함수                                           */
        /*   사용법 :      BaseCommon.TimeInfo.Delay(function () {  */
        /*                      함수명();                           */
        /*                    }, 200);                              */
        /************************************************************/
        Delay: (function () {
            var timer = 0;
            return function (callback, ms) {
                clearTimeout(timer);
                timer = setTimeout(callback, ms);
            };
        })()
    }
    //쿼리스트링 가져오기
    , GetQueryString: function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    , validation:
    {
        //이메일 형식 체크
        emailValidate: function (objName) {
            var filter = /^[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2, 3}$/i;

            if (filter.test($(objName).val()))
                return true;
            else
                return false;
        }

        //전화번호 포멧
        , PhoneValidate: function (objName) {
            phoneRegExp = /^\d{2, 3}-?\d{3, 4}-?\d{4}$/;
            if (phoneRegExp.test($(objName).val()))
                return true;
            else
                return false;
        }
        , MobilePhoneValidate: function (objName, lang) {

            var regExp = ""
            if (lang == undefined) lang = "ko";
            if (String(lang).toLowerCase() == "ko") {
                regExp = /^\d{3}\d{3,4}\d{4}$/;
            }
            else if (lang == undefined)
                regExp = /^([\d]{1})-?([\d]{3})-?([\d]{3})-?([\d]{4})$/;
            var chkData = BaseCommon.StringInfo.replaceAll($(objName).val(), "-", "");

            if (!regExp.test(chkData)) {
                //  alert("잘못된 휴대폰 번호입니다. 숫자,  - 를 포함한 숫자만 입력하세요.");
                return false
            }
            else {
                return true;
            }
        }

        //이메일 형식 체크 $(document).ready(function () {
        , ready_emailValidate: function (emailheader, emailfooter) {
            $(emailfooter).focusout(function () {
                var str = $(emailheader).val() + '@' + $(emailfooter).val();
                if (str != '' && !BaseCommon.validation.emailValidate(str)) {
                    alert('이메일 형식이 잘못되었습니다.');
                    $(emailheader).val('')
                    $(emailfooter).val('')
                    $(emailheader).focus();
                    return false;
                }
                return true;
            });
        }

        //아이디 형식
        , IsIDValidation: function (obj) {
            //$(obj).focusout(function () {
            var str = $(obj).val();

            var reg1 = /^[a-z]+[a-z0-9, ., _, -]{3, 11}$/g;;

            if (str != '') {
                if (!reg1.test(str)) {
                    alert("영문 소문자로 시작하는 4~12자의 영문/숫자로 입력해 주세요.\n아이디는 영문과 숫자 .(점) _(언더바) -(대시) 로만 구성 할 수 있습니다.");
                    $(obj).focus();
                }
                else
                    return true;
            }
            else {
                alert("아이디를 입력해 주시기 바랍니다.");
                $(obj).focus();
            }

            return false;
            // });
        }

        //비밀번호 형식
        , IsPasswordValidation: function (str) {
            var reg1 = /[!, @, #, $, %, ^, &, *, ?, _, ~]/g;
            var reg2 = /[a-zA-Z]/g;
            var reg3 = /[0-9]/g;

            if (str.length == 0) {
                alert("비밀번호를 입력해 주시기 바랍니다.");
                return false;
            }
            else if (!(str.length >= 8 && str.length <= 20)) {
                alert("8~20자리의 영문,  특수문자 및 숫자 조합으로 입력해 주세요.");
                return false;
            } else if (!(reg1.test(str) && reg2.test(str) && reg3.test(str))) {
                alert("영문,  특수문자 및 숫자 조합으로 입력해 주세요.");
                return false;
            }

            return true;
        }

        //비밀번호 비교&형식 $(document).ready(function () {
        , ready_passwordValidate: function (obj1, obj2) {
            $(obj1).focusout(function () {
                if ($(obj1).val() != "") {
                    var str = $(obj1).val();
                    if (!BaseCommon.validation.IsPasswordValidation(str)) {
                        $(obj1).focus();
                        $(obj1).select();
                    }
                }
            });

            $(obj2).focusout(function () {
                if ($(obj2).val() != "" && $(obj2).val() != $(obj1).val()) {
                    alert("비밀번호가 일치하지 않습니다.");
                    $(obj2).focus();
                } else if ($(obj2).val() != "") {
                }
            });
        }

    }
    , NumberInfo:
    {
        ConvertString: function (val, digits) {
            var arrd = String(val).split('.');
            if (arrd.length == 1) {
                val = String(val) + ".";
                for (var i = 0; i < digits; i++) {
                    val = val + "0";
                }

            }
            else if (arrd.length == 2) {
                for (var i = 0; i < digits - arrd[1].length; i++) {
                    val = String(val) + "0";
                }
            }
            return val;
        },
        PerchantCheck: function (obj) {
            var _pattern = /^(\d{1, 3}([.]\d{0, 2})?)?$/;
            var _value = event.srcElement.value;
            if (!_pattern.test(_value)) {
                event.srcElement.value = event.srcElement.value.substring(0, event.srcElement.value.length - 1);
                event.srcElement.focus();
            }

            if ($(obj).attr("max") != undefined) {
                if (Number($(obj).val()) > Number($(obj).attr("max"))) {
                    $(obj).val($(obj).attr("max"));
                }

                if (Number($(obj).val()) < Number($(obj).attr("min"))) {
                    $(obj).val($(obj).attr("min"));
                }
            }

            if ($(obj).val() == "e") {
                $(obj).val("0");
            }
        }
    }
    , windowInfo:
    {
        Popup: function (url, title, w, h) {
            if (w == undefined) {
                var nWidth = 1000;
                if ($(window).width() > 1400) {
                    nWidth = 1050;
                }
                else if ($(window).width() > 1100)
                    nWidth = 1000;
                else if ($(window).width() > 800) nWidth = 750;
                else if ($(window).width() < 450) {
                    nWidth = $(window).width();
                }
                else {
                    nWidth = $(window).width() - 50;
                }
                w = nWidth;
            }
            if (h == undefined) {
                h = $(window).height() - 100;
            }

            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + (top - 30) + ', left=' + left);
        }
    }
}




//////////////////////
///  파일 업로드   ///
//////////////////////

//var FileUpload = {
//    tableName: ""
//    , Idx: ""
//    , Init: function (tableName, Idx, folder) { /* folder/gallery */
//        $('#fileupload')
//            .fileupload(
//                'option',
//                {
//                    maxFileSize: 4000000,
//                    autoUpload: true
//                }
//            ).bind('fileuploadstop', function (e) { try { flieUploadAfter(); } catch (e) { } });;

//        if (!(folder == undefined || folder == "")) {
//            $("#hidFOLDER").val(folder);
//        }
//        FileUpload.tableName = tableName;
//        FileUpload.Idx = Idx;
//    }
//    , SingleInit: function (tableName, Idx, folder) {
//        // $("#sfile").html("<input type='file' name='files' >");
//        $('#fileupload')
//            .fileupload(
//                'option',
//                {
//                    maxFileSize: 4000000,
//                    autoUpload: true
//                    , singleFileUploads: true
//                    , maxNumberOfFiles: 1
//                }
//            );

//        if (!(folder == undefined || folder == "")) {
//            $("#hidFOLDER").val(folder);
//        }
//        FileUpload.tableName = tableName;
//        FileUpload.Idx = Idx;
//    }
//    , getFileInfo: function () {
//        var arrFiles = new Array();
//        $('.files > tr').each(function () {
//            var param = new Object();
//            param.FILE_DIRECTORY = $(this).find(".name a").attr("href").split("?")[1].split("&")[1].split("=")[1];
//            param.FILE_NAME = $(this).find(".name a").text();
//            param.REAL_FILE_NAME = $(this).find(".name a").attr("href").split("?")[1].split("&")[0].split("=")[1];
//            param.FILE_SIZE = $(this).find(".size").text();
//            arrFiles.push(param);
//        });
//        return arrFiles;
//    }
//    , fileClear: function () {
//        $('.delete .btn-danger').trigger('click');
//    },
//    fileList: function () {
//        var params = new Object();
//        params.FILE_KEY = FileUpload.tableName + "_" + FileUpload.Idx;
//        params = JSON.stringify(params);
//        var url = _baseurl + "/Common/PV_FileList/";
//        ajax.GetAjax(url, params, "html", function (result) {
//            $("#ddFile").html(result);
//        });
//    }
//    , fileDelete: function (fileKey, Seq) {
//        bootbox.confirm("Are you sure?", function (result) {
//            if (result) {
//                var params = new Object();
//                params.FILE_KEY = fileKey;
//                params.SEQ = Seq;
//                params = JSON.stringify(params);
//                var url = _baseurl + "/Common/FIleDelete/";
//                ajax.GetAjax(url, params, "", function (result) {

//                    if (result.message == null || result.message == "") {
//                        try {
//                            FileUpload.fileList();
//                        } catch (e) { }
//                        try {
//                            fileDeleteAfter();
//                        } catch (e) { }
//                    }
//                    else {
//                        bootbox.alert(result.message);
//                    }

//                });
//            }
//        });
//    }
//}

var SNS = {
    Share: function (site, msg, url) {

        switch (String(site).toUpperCase()) {
            case "TWITTER":
                // url = "http://twitter.com/home?status=" + encodeURIComponent(msg) + " " + encodeURIComponent("http://www.p2c.co.kr");
                url = "http://twitter.com/home?status=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "FACEBOOK":
                url = "http://www.facebook.com/sharer.php?u=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "GOOGLEPLUS":
                url = "https://plus.google.com/share?url=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            case "PINTEREST":
                url = "http://www.pinterest.com/pin/create/button/?url=http%3A%2F%2Fwww.flickr.com%2Fphotos%2Fkentbrew%2F6851755809%2F&media=http://p2c.co.kr/Common/images/main/main_img1.png&description=Next%20stop%3A%20Pinterest" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
            default:
                url = "http://www.facebook.com/sharer.php?u=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
        }
        var a = window.open(url, site, 'width=800,  height=500');
        if (a) {
            a.focus();
        }
    },
    ShareFacebook: function (shareurl, stitle, sdetail) {
        var shareLocation = shareurl, sDetail = sdetail;
        FB.ui({
            method: 'share',
            name: stitle,
            href: shareLocation,  // 로컬로는 테스트 안됨. 38이나 실서버 링크를 걸면 됨(페북에서 로컬환경은 지원을 안하는듯)
            description: sDetail // 사이트 설명
        }, function (response) { });
    }
}



String.format = function (text) {
    //check if there are two arguments in the arguments list
    if (arguments.length <= 1) {
        //if there are not 2 or more arguments there's nothing to replace
        //just return the original text
        return text;
    }
    //decrement to move to the second argument in the array
    var tokenCount = arguments.length - 2;
    for (var token = 0; token <= tokenCount; token++) {
        //iterate through the tokens and replace their
        // placeholders from the original text in order
        text = text.replace(new RegExp("\\{" + token + "\\}", "gi"),
            arguments[token + 1]);
    }
    return text;
};

///////////////////////////////////
/// Date 관련 함수 정의
//////////////////////////////////
var DateInfo = {
    ConvertDate: function (val) {

        if (val.length == 6) {
            val = val + "01";
        }

        if (val.length < 9) {
            var YYYY = val.substring(0, 4);
            var mm = val.substring(4, 6);
            var dd = val.substring(6, 8);
            var val = YYYY + "-" + mm + "-" + dd + " 00:00:00";
        }

        var sDate = val.split(' ')[0];
        var sHms = val.split(' ')[1]

        return new Date(sDate.split('-')[0], Number(sDate.split('-')[1]) - 1, sDate.split('-')[2], sHms.split(':')[0], sHms.split(':')[1], sHms.split(':')[2]);
    },
    NowDate: function (delimiter) {
        var date = new Date();
        return DateInfo.SetFormatDate(date, delimiter);
    }
    , SetFormatDate: function (date, delimiter) {

        if (delimiter == undefined)
            delimiter = "/";

        date = new Date(date);
        var year, month, day;
        year = date.getFullYear();
        month = (String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1);
        day = String(date.getDate()).length == "1" ? "0" + String(date.getDate()) : String(date.getDate());

        return year + delimiter + month + delimiter + day
    }
    ,
    GlobalAddDate: function (Interval, AddVal, date) {
        if (date == undefined) {
            date = new Date();
        }
        else {
            date = new Date(date);
        }
        var year, month, day;
        year = date.getFullYear();
        month = Number((String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1));
        day = date.getDate();

        switch (Interval) {
            case "year":
                year = (year * 1) + (AddVal * 1);
                break;
            case "month":
                month = (month * 1) + (AddVal * 1);
                break;
            default: //day
                day = (day * 1) + (AddVal * 1);
                break;
        }
        date = new Date(year, month - 1, day);

        return date;

    }
    ,
    /////////////////////////////////////////
    // ex) DateInfo.AddDate("month",  -3,  "12/20/2013"); 
    //     => "09/20/2013"
    /////////////////////////////////////////
    AddDate: function (Interval, AddVal, date, delimiter) {
        if (delimiter == undefined) {
            if (date.indexOf("/") >= 0) {
                delimiter = "/";
            }
            else {
                delimiter = "-";
            }
        }


        date = new Date(date);
        var year, month, day;
        year = date.getFullYear();
        month = Number((String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1));
        day = date.getDate();

        switch (Interval) {
            case "year":
                year = (year * 1) + (AddVal * 1);
                break;
            case "month":
                month = (month * 1) + (AddVal * 1);
                break;
            default: //day
                day = (day * 1) + (AddVal * 1);
                break;
        }
        date = new Date(year, month - 1, day);
        return DateInfo.SetFormatDate(date);
    },

    GetDatetimeValue: function (strDT) {
        var dt = new Object();
        dt.year = "";
        dt.month = "";
        dt.day = "";
        dt.hour = "";
        dt.min = "";

        if (strDT.length == 12) {
            dt.year = strDT.substr(0, 4);
            dt.month = strDT.substr(4, 2);
            dt.day = strDT.substr(6, 2);
            dt.hour = strDT.substr(8, 2);
            dt.min = strDT.substr(10, 2);
            return dt;
        }
        else if (strDT.length == 8) {
            dt.year = strDT.substr(0, 4);
            dt.month = strDT.substr(4, 2);
            dt.day = strDT.substr(6, 2);
            return dt;
        }
        else return null;
    }

}
$("document").ready(function () {
    $("input[type=text]").focus(function () {
        $(this).select();
    });
    $("input[type=number]").focus(function () {
        $(this).select();
    });
    $(".percent").keyup(function () {
        BaseCommon.NumberInfo.PerchantCheck(this);
    });
    $("input[type=number]").each(function () {
        if ($(this).val() == undefined || $(this).val() == "") {
            $(this).val("0.00");
        }
    });
});

/**
 * 유투브 객체
 */
var youtube = {
    GetContentID: function (url) {
        var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
        var match = url.match(regExp);
        return (match && match[7].length == 11) ? match[7] : false;
    }
    , Thumbnail: {
        /* 동영상 배경 썸네일*/
        GetImage480x360: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/0.jpg";
        }, /*동영상 시작지점*/
        GetImage120x90_1: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/1.jpg";
        }, /*동영상 중간지점*/
        GetImage120x90_2: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/2.jpg";
        }, /*동영상 끝지점*/
        GetImage120x90_3: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/3.jpg";
        },/*고품질 썸네일*/
        GetImage480x360_HQ: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/hqdefault.jpg";
        },/*중간품질 썸네일*/
        GetImage320x180_MQ: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/mqdefault.jpg";
        },/*보통품질 썸네일*/
        GetImage120x90_default: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/default.jpg";
        }
        ,/*보통품질 썸네일*/
        GetImage640x480_SD: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/sddefault.jpg";
        },/* 최대 해상도 썸네일(1920x1080) */
        GetImage1920x1080_MAX: function (videoID) {
            return "://img.youtube.com/vi/" + videoID + "/maxresdefault.jpg";
        }
    }

}

/*
History 조작법
https://developer.mozilla.org/ko/docs/Web/API/History_API
window.history.back();
window.history.forward();
*/
//(function ($) {
//    var originalVal = $.fn.val;
//    $.fn.val = function (value) {
//        
//        this.trigger("change");
//        return originalVal.call(this,  value);
//    };
//})(jQuery);


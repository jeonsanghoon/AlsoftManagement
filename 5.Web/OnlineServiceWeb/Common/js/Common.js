var _baseurl = "/alt/en";

/*************/
/* Ajax 관련 */
/*************/
var ajax =
{    /* dateType : html/ */
    GetAjax: function (url, params, dateType, callback, bLogingBar) {
        if (dateType == undefined || $.trim(dateType) == "") {
            dateType = "json";
        }
        else
            dateType = dateType;

        if (BaseCommon.ObjectInfo.isObject(params))
        {
            params = JSON.stringify(params);
        }
        $("#aa-preloader-area").hide();
        if (bLogingBar == undefined)
        { $("#aa-preloader-area").show(); }
        else if (bLogingBar == false) { }
    
        $.ajax({
            type: "POST",
            url: url,
            timeout: 30000,
            data: params,
            cache: false,
            dataType: dateType,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                
                callback(result);
                $("#aa-preloader-area").hide();
            },
            error: function (e) {
                //console.log(e);
                $("#aa-preloader-area").hide();
                //bootbox.alert('Error:' + e.status + " - " + e.statusText);
            },
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
            /*$("input[name='" + inputName + "']:checked").each(function () {
                var obj = new Object();
                rtnVal = $(this).val();
            });*/
            rtnVal = ($(':radio[name="' + inputName + '"]:checked').val() == undefined) ? "" : $(':radio[name="' + inputName + '"]:checked').val();
            return rtnVal;

        }
         , SetCheck: function (inputName, bChk, val1) {
             if (val1 == undefined)
                 $('input:radio[name=' + inputName + ']').prop("checked", bChk);
             else {
                 //$('input[name="' + inputName + '"][value="' + val1 + '"]').prop('checked', bChk);
                 $('input:radio[name=' + inputName + ']:input[value="' + val1 + '"]').prop("checked", bChk);
             }
             /*if (bChk) bChk = "checked";
             else bChk = "";
             if (val1 == undefined) val1 = "";
             $("input[name=" + inputName + "]").filter("input[value=" + val1 + "]").attr("checked", "checked");*/
         },
        isCheck: function (inputName) {
        }
    }
    /*******************/
    /* ComboBox-Select */
    /*******************/
    , ComboBox:
    {
        SetText: function (objId, text) {
            // $("#" + objId + " option[text='" + text + "']").attr('selected', 'selected');
            //$("#" + objId).val(text).attr("selected", "selected");

            $("#" + objId + " option").each(function () {
                if ($(this).text() == text) {
                    $(this).attr('selected', 'selected');
                }
            });
        }
        , GetText: function (objId) {
            return $("#" + objId + " option:selected").text();
        }
    }

    /***************/
    /* 체크박스 */
    /***************/
    , CheckBox:
    {
        isChecked: function (objId) {
       
            return $("input:checkbox[id='" + objId + "']").is(":checked");
        }, Checked: function (inputName, bChk) {
            if (bChk)
            {
                $("input:checkbox[id='" + inputName + "']").prop('checked', true);
                $("input:checkbox[id='" + inputName + "']").attr('checked', bChk);
            }
            else
                {
                $("input:checkbox[id='" + inputName + "']").attr('checked', bChk);
                $("input:checkbox[id='" + inputName + "']").removeAttr("checked")
            }
        }, CheckedAll: function (inputName, bChk) {
            try {
                
                $("input[name='" + inputName + "']").each(function () {
                    if (bChk) {
                        $(this).prop('checked', true);
                        $(this).attr("checked","checked");
                    }
                    else if (!bChk) $(this).removeAttr("checked");
                });
               
            } catch (e) { }
        }, CheckedVal: function (objId) {
            return $("#" + objId + ":checked").val();
        }, CheckedValues: function (inputName) {
            var checked = []
            $("input[name='" + inputName + "']:checked").each(function () {
                var obj = new Object()
                obj.Value = $(this).val();
                checked.push(obj);
            });
            return checked;
        }, CheckedStringValues: function (inputName) {
            var rtnValue = "";
            $("input[name='" + inputName + "']:checked").each(function () {
                rtnValue = rtnValue + $(this).val() + "|";
            });
            rtnValue = rtnValue.substr(0, rtnValue.length - 1);
            return rtnValue;
        }
    }
    /***************/
    /*Text박스 */
    /***************/
    , TextBox:
    {
        onlyNumber: function (event) {

            if ((event.keyCode < 48) || (event.keyCode > 57))
                event.returnValue = false;
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
            num = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (parts[1] ? "." + parts[1] : "")

            return num;
        }
        , Round: function (val, digits) {
            return Math.round(val * Math.pow(10, digits)) / Math.pow(10, digits);
        }

        ,
        NumberBox:
        {
            increaseNum: 1
            , minNumber: 1
            , maxNumber: 100

            , getVal: function (id) {
                return BaseCommon.StringInfo.replaceAll($("#" + id).val(), ',', '');
            },
            setVal: function (id, num, digits) {

                if (isNaN(num)) {
                    num = 0;
                    $("#" + id).val("0");
                }
                else {
                    if (num <= Input.TextBox.NumberBox.maxNumber
                        && num >= Input.TextBox.NumberBox.minNumber
                        ) $("#" + id).val(Input.TextBox.commaNum(num, digits));
                }

            }
            ,
            Init: function (id, digits) {

                $("#" + id + "_UP").click(function () {
                    Input.TextBox.NumberBox.setVal(id, String(Number(Input.TextBox.NumberBox.getVal(id)) + Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id + "_DOWN").click(function () {
                    Input.TextBox.NumberBox.setVal(id, String(Number(Input.TextBox.NumberBox.getVal(id)) - Input.TextBox.NumberBox.increaseNum));
                });
                $("#" + id).keypress(function (event) {
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
                    $("#" + id).val(BaseCommon.StringInfo.replaceAll($("#" + id).val(), ',', ''));
                    $("#" + id).select();
                });
            }
        }

    }

}


var BaseCommon =
{
    /****************/
    /*   서버정보   */
    /****************/
    ServerInfo:
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
 
        InputsToJson: function (id) {
            var data = $("#" + id).serializeArray();

            return JSON.stringify(BaseCommon.FormInfo.arrayToJSON(data));
        }
        , arrayToJSON: function (serializedForm, replaceData) {
            var data = {};
            if (replaceData == undefined) replaceData = "";
            for (var cnt in serializedForm) {
                if (data[serializedForm[cnt].name.replace(replaceData,"")] == undefined)
                    data[serializedForm[cnt].name.replace(replaceData, "")] = serializedForm[cnt].value;
            }
            return data;
        }, serializeJson: function (objTag, replaceData)
        {
            if (replaceData == undefined) replaceData = "";
            var serializedForm = $(objTag).serializeArray();
           return BaseCommon.FormInfo.arrayToJSON(serializedForm, replaceData);
        }
        //////////////////////
        // 동적 Form Submit
        // 예제 : var arrData = new Array('GROUP_CODE' + '|' + groupCode, 'ITEM_NAME' + '|' + String(itemName));
        //       BaseCommon.FormInfo.PostSubmit('/@SessionHelper.LoginInfo.LANGUAGE/order/index', arrData);
        //////////////////////
        , PostSubmit: function (url, arrData, target) {

            var $form = $('<form></form>');
            $form.attr('action', url);
            $form.attr('method', 'post');
            if (target != undefined) {
                $form.attr('target', target);
            }
            $form.appendTo('body');

            for (var i = 0; i < arrData.length; i++) {
                var pData = arrData[i].split('|');
                $form.append('<input type="hidden" value="' + pData[1] + '" name="' + pData[0] + '">')
            }
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
        // textarea id, 제한 글자 수, 입력 결과 메세지 저장 ID
        limitCharacters: function (textid, limit, limitid) { //(textBox Id, 제한글자수, 현자글자 체크 span Id) 
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
            //val1 = BaseCommon.StringInfo.replaceAll(val1, "'", "\"");
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
        }, isObject: function (val) {
           
            if (val === null) { return false; }
            return ((typeof val === 'function') || (typeof val === 'object'));
        }
    }

    /****************/
    /*   날짜정보   */
    /****************/
    , DateInfo:
      {
          RemoveDateFormat: function (val) {
              val = BaseCommon.StringInfo.replaceAll(val, '-', '');
              val = BaseCommon.StringInfo.replaceAll(val, '/', '');
              val = val.replace('.', '').replace('.', '');
              return val;
          },
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
                  default:
                      return year + "/" + month + "/" + day;
                 
              }
          },
          /////////////////////////////////////////
          // ex) BaseCommon.DateInfo.dateAdd(구분자, 시간, add 값)
          //     BaseCommon.DateInfo.dateAdd('minute', d, 30); 
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
          }
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
           emailValidate: function (name) {
               var filter = /^[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}$/i;

               if (filter.test($("#" + name).val()))
                   return true;
               else
                   return false;
           }

           //전화번호 포멧
           , PhoneValidate: function (phone) {
               phoneRegExp = /^\d{2,3}-?\d{3,4}-?\d{4}$/;
               if (phoneRegExp.test($("#" + phone).val()))
                   return true;
               else
                   return false;
           }
            , MobilePhoneValidate: function (name, lang) {
                var regExp = ""
                if (String(lang).toLowerCase() == "ko") {
                    regExp = /^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})$/;
                }
                else if (lang == undefined)
                    regExp = /^([\d]{1})-?([\d]{3})-?([\d]{3})-?([\d]{4})$/;


                if (!regExp.test($("#" + name).val())) {
                    //  alert("잘못된 휴대폰 번호입니다. 숫자, - 를 포함한 숫자만 입력하세요.");
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

               var reg1 = /^[a-z]+[a-z0-9,.,_,-]{3,11}$/g;;

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
               var reg1 = /[!,@,#,$,%,^,&,*,?,_,~]/g;
               var reg2 = /[a-zA-Z]/g;
               var reg3 = /[0-9]/g;

               if (str.length == 0) {
                   alert("비밀번호를 입력해 주시기 바랍니다.");
                   return false;
               }
               else if (!(str.length >= 8 && str.length <= 20)) {
                   alert("8~20자리의 영문, 특수문자 및 숫자 조합으로 입력해 주세요.");
                   return false;
               } else if (!(reg1.test(str) && reg2.test(str) && reg3.test(str))) {
                   alert("영문, 특수문자 및 숫자 조합으로 입력해 주세요.");
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
         }
     }
}




//////////////////////
///  파일 업로드   ///
//////////////////////
var FileUpload = {
    tableName: ""
    , Idx: ""
    , Init: function (tableName, Idx, folder) { /* folder/gallery */
        $('#fileupload')
        .fileupload(
            'option',
            {
                maxFileSize: 4000000,
                autoUpload: true
            }
        ).bind('fileuploadstop', function (e) { try { flieUploadAfter(); } catch (e) { } });;

        if (!(folder == undefined || folder == "")) {
            $("#hidFOLDER").val(folder);
        }
        FileUpload.tableName = tableName;
        FileUpload.Idx = Idx;
    }
    , SingleInit: function (tableName, Idx, folder) {
        // $("#sfile").html("<input type='file' name='files' >");
        $('#fileupload')
        .fileupload(
            'option',
            {
                maxFileSize: 4000000,
                autoUpload: true
                , singleFileUploads: true
                , maxNumberOfFiles: 1
            }
        );

        if (!(folder == undefined || folder == "")) {
            $("#hidFOLDER").val(folder);
        }
        FileUpload.tableName = tableName;
        FileUpload.Idx = Idx;
    }
    , getFileInfo: function () {
        var arrFiles = new Array();
        $('.files > tr').each(function () {
            var param = new Object();
            param.FILE_DIRECTORY = $(this).find(".name a").attr("href").split("?")[1].split("&")[1].split("=")[1];
            param.FILE_NAME = $(this).find(".name a").text();
            param.REAL_FILE_NAME = $(this).find(".name a").attr("href").split("?")[1].split("&")[0].split("=")[1];
            param.FILE_SIZE = $(this).find(".size").text();
            arrFiles.push(param);
        });
        return arrFiles;
    }
    , fileClear: function () {
        $('.delete .btn-danger').trigger('click');
    },
    fileList: function () {
        var params = new Object();
        params.FILE_KEY = FileUpload.tableName + "_" + FileUpload.Idx;
        params = JSON.stringify(params);
        var url = _baseurl + "/Common/PV_FileList/";
        ajax.GetAjax(url, params, "html", function (result) {
            $("#ddFile").html(result);
        });
    }
    , fileDelete: function (fileKey, Seq) {
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                var params = new Object();
                params.FILE_KEY = fileKey;
                params.SEQ = Seq;
                params = JSON.stringify(params);
                var url = _baseurl + "/Common/FIleDelete/";
                ajax.GetAjax(url, params, "", function (result) {

                    if (result.message == null || result.message == "") {
                        try {
                            FileUpload.fileList();
                        } catch (e) { }
                        try {
                            fileDeleteAfter();
                        } catch (e) { }
                    }
                    else {
                        bootbox.alert(result.message);
                    }

                });
            }
        });
    }
}



function sendSns(sns, url, txt) {
    var o;
    var _url = encodeURIComponent(url);
    var _txt = encodeURIComponent(txt);
    var _br = encodeURIComponent('\r\n');

    switch (sns) {
        case 'facebook':
            o = {
                method: 'popup',
                url: 'http://www.facebook.com/sharer/sharer.php?u=' + _url
            };
            break;

        case 'twitter':
            o = {
                method: 'popup',
                url: 'http://twitter.com/intent/tweet?text=' + _txt + '&url=' + _url
            };
            break;

        case 'me2day':
            o = {
                method: 'popup',
                url: 'http://me2day.net/posts/new?new_post[body]=' + _txt + _br + _url + '&new_post[tags]=epiloum'
            };
            break;

        case 'kakaotalk':
            o = {
                method: 'web2app',
                param: 'sendurl?msg=' + _txt + '&url=' + _url + '&type=link&apiver=2.0.1&appver=2.0&appid=dev.epiloum.net&appname=' + encodeURIComponent('Epiloum 개발노트'),
                a_store: 'itms-apps://itunes.apple.com/app/id362057947?mt=8',
                g_store: 'market://details?id=com.kakao.talk',
                a_proto: 'kakaolink://',
                g_proto: 'scheme=kakaolink;package=com.kakao.talk'
            };
            break;

        case 'kakaostory':
            o = {
                method: 'web2app',
                param: 'posting?post=' + _txt + _br + _url + '&apiver=1.0&appver=2.0&appid=dev.epiloum.net&appname=' + encodeURIComponent('Epiloum 개발노트'),
                a_store: 'itms-apps://itunes.apple.com/app/id486244601?mt=8',
                g_store: 'market://details?id=com.kakao.story',
                a_proto: 'storylink://',
                g_proto: 'scheme=kakaolink;package=com.kakao.story'
            };
            break;

        case 'band':
            o = {
                method: 'web2app',
                param: 'create/post?text=' + _txt + _br + _url,
                a_store: 'itms-apps://itunes.apple.com/app/id542613198?mt=8',
                g_store: 'market://details?id=com.nhn.android.band',
                a_proto: 'bandapp://',
                g_proto: 'scheme=bandapp;package=com.nhn.android.band'
            };
            break;

        default:
            alert('지원하지 않는 SNS입니다.');
            return false;
    }

    switch (o.method) {
        case 'popup':
            window.open(o.url);
            break;

        case 'web2app':
            if (navigator.userAgent.match(/android/i)) {
                // Android
                setTimeout(function () { location.href = 'intent://' + o.param + '#Intent;' + o.g_proto + ';end' }, 100);
            }
            else if (navigator.userAgent.match(/(iphone)|(ipod)|(ipad)/i)) {
                // Apple
                setTimeout(function () { location.href = o.a_store; }, 200);
                setTimeout(function () { location.href = o.a_proto + o.param }, 100);
            }
            else {
                alert('이 기능은 모바일에서만 사용할 수 있습니다.');
            }
            break;
    }
}

var SNS = {
    Share: function (site, msg, url) {
        if (url == undefined)
            url = location.href;
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
            case "KAKOSTORY":
                url = "https://story.kakao.com/share?url=" + encodeURIComponent(url);
                break;
            default:
                url = "http://www.facebook.com/sharer.php?u=" + encodeURIComponent(msg) + " " + encodeURIComponent(url);
                break;
        }
        var a = window.open(url, site, 'width=800, height=500');
        if (a) {
            a.focus();
        }
    },
    ShareFacebook: function (shareurl, stitle, sdetail) {
        var shareLocation = shareurl, sDetail = sdetail;
        FB.ui({
            method: 'share',
            name: stitle,
            href: shareLocation, // 로컬로는 테스트 안됨. 38이나 실서버 링크를 걸면 됨(페북에서 로컬환경은 지원을 안하는듯)
            description: sDetail // 사이트 설명
        }, function (response) { });
    }
}



$(document).ready(function () {
    
    $("form").submit(function (event) {
        event.preventDefault();
        return false;
    });
    $("input").focus(function () {
        $(this).select();
    });
});


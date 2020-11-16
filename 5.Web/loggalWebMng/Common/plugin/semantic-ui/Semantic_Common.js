
var SemanticUI = {
    ComponentInit: function (obj) {
        if (obj == undefined) {
            obj = "body";

        }
        
		$(obj).find("select").dropdown({
			onShow: function () {
				$('.ajax-file-upload form').hide();
			},
			onHide: function () {
				$('.ajax-file-upload form').show();
			},
			onChange: function (value, text, obj) {
				
                try {
                    SemanticUI_ComboChange($(this));
                }
                catch (e) { }
                try { Grid_SemanticUI_ComboChange($(this)); }
                catch (e) { }
                if (value != "") {
                    $(this).closest(".ui.dropdown").removeClass("error");
                }
            }
		});

	    $(obj).find("select").each(function () {
            if (String($(this).find("option:eq(0)").attr("value")).trim() == "") {
                var defaultText = $(this).find("option:eq(0)").text()
                if (!(defaultText == undefined || defaultText == "")) {
                    $(this).parent().find("div.menu").prepend('<div class="item defaultlabel" data-value="">' + defaultText + '</div>')
                }
            }
        })


        $(obj).find(".ui.dropdown").each(function () {
            if ($(this).find("select").attr("style") != undefined) {

                $(this).attr("style", $(this).find("select").attr("style"));
                $(".ui.selection.dropdown").css("max-width:100%");
            }
        });
        $(obj).find('.ui.checkbox').checkbox({
            onChange: function () {
                try {
                    if ($(this).parent().hasClass("checked")) {
                        $(this).attr('checked', true);
                    }
                    else {
                        $(this).removeAttr('checked');
                    }
                } catch (e) { }
            }
        });
        $(obj).find("input:checkbox").removeClass("ui");
        $(obj).find("input:checkbox").removeClass("checkbox");
        $(obj).find("input:checkbox").addClass("ui");
        $(obj).find("input:checkbox").addClass("checkbox");
        $(obj).find("input:checkbox").checkbox();
        $(obj).find('.right.menu.open').on("click", function (e) {
            e.preventDefault();
            $(obj).find('.ui.vertical.menu').toggle();
        });
        try {

            $(obj).find('input.date').datetimepicker({
                timepicker: false,
                format: 'Y.m.d',
            });
        } catch (e) { }
        try {

            $(obj).find('input.time').datetimepicker({
                datepicker: false,
                //allowTimes: ['12:00', '13:00', '15:00', '17:00', '17:05', '17:20', '19:00', '20:00'],
                step: 30,
                format: 'G:i',
                minDate: null,
                maxDate: null,
                onChangeDateTime: function (dp, $input) {
                    if ($input.val() != "") $input.parent().removeClass("error")
                }
            });
            //$(obj).find('input.time').trigger("blur");
        } catch (e) { }
        try {

            $(obj).find('input.datetime').datetimepicker({
                datepicker: true,
                //allowTimes: ['12:00', '13:00', '15:00', '17:00', '17:05', '17:20', '19:00', '20:00'],
                step: 30,
                format: 'G:i',
                minDate: null,
                maxDate: null,
                onChangeDateTime: function (dp, $input) {
                    if ($input.val() != "") $input.parent().removeClass("error")
                }
            });
            //$(obj).find('input.time').trigger("blur");
        } catch (e) { }

        if ($(".ui .content.frtodate input").length == 2) {
            DatetimePicker.frtoInit('#' + $(".ui .content.frtodate input:eq(0)").attr("id"), '#' + $(".ui .content.frtodate input:eq(1)").attr("id"));
        }
        try {
            $('.ui.accordion').accordion({
                duration: 500
                , onClosing: function () {
                    $(this).find(".field *").addClass("hidden");


                }, onOpening: function () {
                    $(this).find(".field *").removeClass("hidden");


                    // $('.ui.accordion').height($(this).find(".content.field").height() );
                }, onOpen: function () {
                    try {
                        if ($("window").width() >= 768) {
                            $(this).css("height", $(this).find(".ui.segment.field").height() + Number($(".ui.footer.segment").css("bottom").replace("px", "")));
                        }
                        else
                            $(this).css("height", $(this).find(".ui.segment.field").height() + 30);
                    } catch (e) { }
                    try {
                        accordionOpenCallback($(this));
                    } catch (e) { }
                }, onClose: function () {
                    $(".cd-main-header").slideDown("slow", function () {
                        // Animation complete.
                    });
                }
            });
        } catch (e) { }
    },
    ComboBox: {
        AddItem: function (objName, option_value, option_text, selected) {
            option_value = option_value.trim();
            option_text  = option_text.trim();
            $(objName).append('<option value="' + option_value + '" selected>' + option_text + '</option>');

            if ($(objName).attr("multiple") != undefined) {
                var arrdata = SemanticUI.ComboBox.GetAllValues(objName);
                arrdata.push(option_value);

                setTimeout(function () {
                    $(objName).dropdown('set selected', arrdata);
                }, 220);
            }
            else {
                if (selected == undefined) selected = false;
                setTimeout(function () {
                    $(objName).dropdown('set selected', option_value);
                }, 220);
            }
            $(objName).dropdown("refresh");
        },
        GetObject: function (id) {
            SemanticUI.ComboBox.GetVal()
            return $("id").parent();
        },
        SetVal: function (objName, val) {
            try {
                $(objName).dropdown('set selected', String(val));
            } catch (e) { }
        },
        GetVal: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            return $(objName).dropdown('get value');
            //  $(objName).val(val);
        }, IsVal: function (objName, val) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            return ($.inArray(val, $(objName).dropdown('get value')) >= 0 ? true : false);
        }
        , GetText: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            return $(objName).dropdown('get text');
            //  $(objName).val(val);
        }

        , GetCompStoreVal: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            var rtn = $(objName).dropdown('get value');
            var obj = new Object();
            obj.COMPANY_CODE = "";
            obj.STORE_CODE = "";
            if (!(rtn == undefined || rtn == "")) {
                obj.COMPANY_CODE = String(rtn).split('|')[0];
                obj.STORE_CODE = String(rtn).split('|')[1];
            }

            return obj;
        },
        GetValues: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            var arrData = $(objName).dropdown('get value');
            if (arrData == null) return new Array();
            return arrData;
        },
        GetTexts: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;
            var arrData = $(objName).dropdown('get text');
            if (arrData == null) return new Array();
            return arrData;
        }, GetValuesToString: function (objName, gubun) {
            var rtnVal = "";
            try {
                if (String(objName).indexOf("#") < 0) objName = "#" + objName;
                if (gubun == undefined) gubun = ",";
                var arrData = $(objName).dropdown('get value');

                // 값이 없을때 공백으로리턴
                if (arrData == null) return "";

               // arrData = arrData[arrData.length - 1];

                for (var i = 0; i < arrData.length; i++) {
                    if (i > 0) rtnVal += gubun;

                    rtnVal += arrData[i].trim();

                }
            } catch (e) { return ""; }
            return rtnVal;
            //  $(objName).val(val);
        }
        /*콤보에서 선택된 아이템만 가져오기*/
        , GetSelectedArray: function (objName, value_name, text_name, objDefault) {
            if (value_name == undefined) value_name = "value";
            if (text_name == undefined) text_name = "text";
            var arrData = new Array();
            $(objName).parent().find(".ui.label").each(function () {
                var obj = new Object();
                obj[value_name] = $(this).attr("data-value");
                obj[text_name] = $(this).text();
                if (objDefault != undefined) {
                    $.each(objDefault, function (key, val) {
                        obj[key] = val;
                    })
                }

                arrData.push(obj);
            });
            return arrData;
        },
        /*콤보에 모든 아이템 가져오기*/
        GetAllData: function (objName, value_name, text_name, objDefault) {
            if (value_name == undefined) value_name = "value";
            if (text_name == undefined) text_name = "text";
            var arrData = new Array();
            $(objName).parent().find(".menu div.item").each(function () {
                var obj = new Object();
                obj[value_name] = $(this).attr("data-value");
                obj[text_name] = $(this).text();
                if (objDefault != undefined) {
                    $.each(objDefault, function (key, val) {
                        obj[key] = val;
                    })
                }
                arrData.push(obj);
            });
            return arrData;
        }/*콤보에 모든 아이템 가져오기*/
        , GetAllValues: function (objName) {

            var arrData = new Array();

            $(objName).parent().find(".menu div.item").each(function () {
                arrData.push($(this).attr("data-value"));
            });
            return arrData;
        }

        , SetWidth: function (objName, nwidth) {
            $(objName).parent().css("width", nwidth);
        }, Disable: function (objName, bChk) {
            if (bChk) {
                $(objName).closest('.ui.dropdown').addClass("disabled");
            }
            else {
                $(objName).closest('.ui.dropdown').removeClass("disabled");
            }
            
        }
    }, CheckBox:
        {
        GetVal: function (objName) {

                if ($(objName).parent().hasClass("checked")) {
                    return $(objName).val();
                }
                else {
                    if ($(objName).val() == "1") return "0";
                    else return null;
                };
        }, SetVal: function (objName, bCheck) {
                objName = objName.replace("#", "");
                $("input:checkbox[id='" + objName + "']").prop("checked", bCheck);

                try {
                    setTimeout("try{dv_" + objName + "_Click('" + val + "');}catch(e){}", 10);
                } catch (e) { }
            }
        }, YesOrNo: {
            GetVal: function (objName) {
                return $(objName).val();
            }, SetVal: function (objName, val, bEvt) {
                name = String(objName).replace("#", "").replace(".", "");
                try {
                    bEvt = (bEvt == undefined ? true : bEvt);
                    window["dv_" + name + "_Click"](val, bEvt);
                } catch (e) { }
            }
        }
    , Form: {
        Invalidation: {
            /// 에러 체크시 첫번째 태그에 포커스
            ErrorFocus: function () {
                try {
                    var elem = $('.field.error input,.field.error select,.field.error checkbox,.field.error radio').eq(0);
                    $(elem).focus();
                    $(elem).select();
                } catch (e) { }
            }
        }, addErrorMessage: function (event, tagName, errorObj) {
            $(tagName).html("");
            if (tagName == undefined) {

                if ($(".ui.error.message ul.list").length == 0) {
                    $(".ui.error.message").html("<ul class='list'></ul>")
                }
                tagName = ".ui.error.message"
            };

            $(errorObj).closest(".ui.field,.ui.dropdown").addClass("error");

            //$(tagName + " ul.list").html("");
            for (var i = 0; i < event.length; i++) {
                if (String($(tagName).text()).indexOf(event[i]) < 0) {
                    $(tagName + " ul.list").append("<li>" + event[i] + "</li>");
                }
            }
            if ($(tagName + " li").length > 0) {
                $(tagName).show();
            } else {
                $(tagName).hide();
            }
        }, Clear: function () {
            $('.ui.form').form('clear');
        }
    },
    ErrorBoxHide: function () {
        $(".ui.error.message").hide();
        $(".ui.error.message").html("");

    }, Popup:
        {
            ContentResize: function (objName, minusSize) {

                try {
                    if (minusSize == undefined) minusSize = 50;
                    var contentHeight = $(objName).height() - $(objName + " .header").height() - $(objName + " .actions").height();

                    if ($(".ui.form.popSearch").length == 1) {
                        contentHeight = contentHeight - $(".ui.form.popSearch").height();
                    }

                    if ($(window).width() < 768) {
                        
                        $(objName + " .content").height(contentHeight);
                        //$(objName + " .content").css("top", "100px;");
                        $(objName).css("margin-left", (-1 * $(objName).width()) / 2 );
                    }
                    else {
                        $(objName + " .content").height(contentHeight - minusSize);
                    }
                    var nScroll = 0;
                    try { nScroll + $(document).scrollTop()} catch (e) { }
                    $(objName).css("margin-top", -1 * ($(objName).height() / 2 + 40));

                    

                    window.dispatchEvent(new Event('resize'));
                } catch (e) { }

            }, Show: function (objName) {
                $(objName).modal({ detachable: false }).modal('show');
                setTimeout(SemanticUI.Popup.ContentResize(objName), 1000);


            }
        },
    Tab:
        {
            GetActiveTab: function (objName) {
                return $(objName).find(".item.active").attr("data-tab");
            }
            , SetActiveTab: function(objName, tabName) {
                //$(objName).tab('change tab', tabName);
                SemanticUI.Tab.isShow(objName, tabName, true);
            }, isShow: function(objName, dataTabName, bShow) {
                $(objName + ".ui.menu .item").removeClass("active");
                $(objName).parent().find(".ui.tab.segment").removeClass("active");
                if (bShow == true) {
                    $(objName + " .item[data-tab='" + dataTabName + "']").addClass("active");
                    $(objName).parent().find(".ui.tab.segment[data-tab='" + dataTabName + "']").addClass("active");
                }
            }
        },
    Modal:
        {
            CenterShow: function (objName) {
                $(objName).css("margin-top", -(Number($(objName).height()) / 2));

            }
        },
    Button:
        {
            GetBooleanVal:function(objName) {
                if ($(objName).val() == "1" || String($(objName).val()).toLocaleLowerCase() == "ture") {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
}

$(document).ready(function () {
    SemanticUI.ComponentInit();
});


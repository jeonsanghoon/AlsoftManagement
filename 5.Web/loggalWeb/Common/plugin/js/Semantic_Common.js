var SemanticUI = {
    ComponentInit: function (objName) {
        
        
        objName = (objName == undefined || objName =="") ? "body" : objName;
        $(objName).find("select").dropdown({
            onChange:function(value, text, obj) {
                try{ SemanticUI_ComboChange($(this));}
                catch(e){}
            }
        });
        $(objName).find("select").each(function () {
            if (String($(this).find("option:eq(0)").attr("value")).trim() == "") {
                var defaultText = $(this).find("option:eq(0)").text()
                if (!(defaultText == undefined || defaultText == "")) {
                    $(this).parent().find("div.menu").prepend('<div class="item defaultlabel" data-value="">' + defaultText + '</div>')
                }
            }
        })

        $(objName).find(".ui.dropdown").each(function () {
            if ($(this).find("select").attr("style") != undefined) {

                $(this).attr("style", $(this).find("select").attr("style"));
                $(".ui.selection.dropdown").css("max-width:100%");
            }
        });
        $(objName).find('.ui.checkbox').checkbox({
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
        $(objName + "input:checkbox").removeClass("ui");
        $(objName + "input:checkbox").removeClass("checkbox");
        $(objName + "input:checkbox").addClass("ui");
        $(objName + "input:checkbox").addClass("checkbox");
        $(objName + "input:checkbox").checkbox();
        $(objName + '.right.menu.open').on("click", function (e) {
            e.preventDefault();
            $(objName + '.ui.vertical.menu').toggle();
        });

        //$('.ui.dropdown').dropdown();
    }
    , ComboBox: {
        GetObject: function (id) {
            return $("id").parent();
        },
        SetVal: function (objName, val) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;

            $(objName).dropdown('set selected', val);
            //  $(objName).val(val);
        },
        GetVal: function (objName) {
            if (String(objName).indexOf("#") < 0) objName = "#" + objName;

            
            return $(objName).dropdown('get value')[0];
            //  $(objName).val(val);
        }, SetWidth: function (objName, nwidth) {
            $(objName).parent().css("width", nwidth);
        }
    }, CheckBox:
    {
        GetVal: function (id) {
            if ($("#" + id).parent().hasClass("checked")) {
                return $("#" + id).val();
            }
            else {
                if ($("#" + id).val() == "1") return "0";
                else return null;
            };
        }
    }, Form: {
        Invalidation: {
            /// 에러 체크시 첫번째 태그에 포커스
            ErrorFocus: function () {
                try {
                    var elem = $('.field.error input,.field.error select,.field.error checkbox,.field.error radio').eq(0);
                    $(elem).focus();
                    $(elem).select();
                } catch (e) { }
            }
        }, addErrorMessage: function (event, tagName) {
            if (tagName == undefined) tagName = ".ui.error.message";
            if ($(tagName).css("display") == "none") {
                $(tagName).show();
            }
            for (var i = 0; i < event.length; i++) {
                if (String($(tagName).text()).indexOf(event[i]) < 0) {
                    $(tagName).html($(tagName).html() + "<li>" + event[i] + "</li>");
                }
            }
        }
    }
}

$(document).ready(function () {
    SemanticUI.ComponentInit();
});


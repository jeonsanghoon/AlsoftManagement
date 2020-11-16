/*************************************************/
/* 작성자 : 전상훈                               */
/* 작성일 : 2016.10.01                           */
/* 설  명 : 
  class 속성 
    1 : .mrc-table : 테이블 클래스 속성임
     
  Html Attribute 속성
   - column_name = "데이터객체를 만들기 위한 칼럼명
   - data_sort   = "정렬시 사용되는 실제 칼럼명 => 만일 a.name 로 설정했을 경우 sort 값이 a.name가 됨 없을 경우 column_name 를 사용 => mrcGrid_Sort 함수에서 사용됨
  class 속성
   - sort = 정렬 사용 시 추가
/*************************************************/


$.mrc_table = {
    Init: function (objName, bEdit, sort) {

        if (objName == ".mrc-table") objName = "";
        if (objName == "" || objName == undefined) objName = "";
        //$(".mrc-table input").attr("readonly", "readonly");
        bEdit = (bEdit == undefined) ? false : true;
        if ($(objName + ".mrc-table").hasClass("editableGrid")) {
            $(objName + ".mrc-table th.editableCheck").remove()
            $(objName + ".mrc-table").removeClass("editableGrid")
        }
        if (bEdit != undefined && bEdit == true && !$(objName + ".mrc-table").hasClass("editableGrid")) {
            $(objName + ".mrc-table").addClass("editableGrid");
            $(objName + ".mrc-table thead tr").prepend('<th class="editableCheck" column_name="MRC_EDIT_MODE">&nbsp;</th>');
            $(objName + ".mrc-table tbody tr").each(function (idx) {
                if ($(this).find(".editableCheck").length == 0) $(this).prepend('<td class="editableCheck"><input type="checkbox" class="chkEditMode ui checkbox" /><span class="editMode"></span></td>');
            })

        }

        $(objName + " th.sort").each(function (index) {
            if ($(this).find("i.sort").length == 0) {
                $(this).append("&nbsp;<i class='fa fa-sort sort' style='cursor:pointer'></i>");
                $(this).closest("th").css("cursor", "pointer");
            }
            else {
                var bSort = (sort == undefined || sort == "") ? false : bSort;
                if (bSort == false) {
                    $(this).find("i.sort").removeClass("fa-sort").removeClass("fa-sort-down").removeClass("fa-sort-up");
                    $(this).find("i.sort").addClass("fa-sort");
                }
            }
        })
        $(objName + ".mrc-table tbody tr").addClass("mrcrow");
        $(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").css("white-space", "nowrap");
        if (!$(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").hasClass("addTD")) {
            $(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").addClass("addTD");
        }

        $(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").prepend('<i name="btnExpend" class="fa fa-plus-circle" aria-hidden="true"></i>');
        //$(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").find("i").css("float", "left");

        /* Cells in even rows (2,4,6...) are one color */
        $(objName + ".mrc-table tr:nth-child(even) td").addClass("even");

        /* Cells in odd rows (1,3,5...) are another (excludes header cells) */
        $(objName + ".mrc-table tr:nth-child(odd) td").addClass("odd");

        $.mrc_table.trTageEventInit(objName, bEdit);
        $.mrc_table.ExpandEventInit(objName);

        $(objName + ".mrc-table th").each(function (index) {
            // alert($(this).attr("display_size"));
            var objColumn = new Object();

            if ($(this).attr("display_size") != undefined) {
                $(this).closest(".mrc-table").find("td:nth-child(" + String(index + 1) + ")").attr("display_size", $(this).attr("display_size"));
                $(this).closest(".mrc-table").find("td:nth-child(" + String(index + 1) + ")").attr("title", $(this).text());
                objColumn["display_size"] = $(this).attr("display_size");
            }
        })

        $(objName + ".mrc-table tbody tr").click(function (index) {
            $(objName + ".mrc-table tbody tr").removeClass("active");
            $(this).addClass("active");
            try {
                var param = new Object();

                var columnInfo = $.mrc_table.columnInfo(objName);
                var trObj = $(this);

                for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                    param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD(trObj.find("td:eq(" + nCol + ")"));
                }

                mrc_table_trClick(objName, param);
            } catch (e) { }
        })
        $(objName + " th.sort").unbind();
        $(objName + " th.sort").click(function () {

            var orderType = "";
            if ($(this).find("i.sort").hasClass("fa-sort")) {
                orderType = "DESC";
            }
            else if ($(this).find("i.sort").hasClass("fa-sort-down")) {
                orderType = "ASC";
            }

            $(this).parent().find("th i.sort").removeClass("fa-sort").removeClass("fa-sort-down").removeClass("fa-sort-up");
            $(this).parent().find("th i.sort").addClass("fa-sort");

            var className = "fa-sort" + ((orderType == "") ? "" : "-" + (orderType.toLowerCase() == "asc" ? "up" : "down"));
            //alert(className);
            $(this).find("i.sort").removeClass("fa-sort");
            $(this).find("i.sort").addClass(className);
            var datasort = ($(this).attr("data-sort") == undefined ? $(this).attr("column_name") : $(this).attr("data-sort"));
            try {
                mrcGrid_Sort(objName, datasort, orderType);
            } catch (e) { }
        })
        if (bEdit == true) {
            $("#chkAll").click(function () {

                Input.CheckBox.CheckedAll("chkUse", Input.CheckBox.isChecked($(this).attr("id")));

                $(this).closest(".mrc-table").find("tbody .editableCheck").each(function () {
                    $(this).find(".editMode").text("U");
                });
            })
        }
    },
    columnInfo: function (objName) {
        var arrColumnInfo = new Array();
        $(objName).find("thead tr:last th").each(function (index) {

            var param = new Object();
            param.COLUMN_INDEX = index;
            param.COLUMN_NAME = $(this).attr("column_name") == undefined ? "NOT_COLUMN_" + String(index) : $(this).attr("column_name");
            param.DISPLAY_SIZE = $(this).attr("display_size") == undefined ? "ALL" : $(this).attr("display_size");
            arrColumnInfo[index] = param;
        });

        if (arrColumnInfo.length != $(objName).find("thead tr:last th").length) alert("column count does not match table th count.");
        return arrColumnInfo;

    },
    GetCheckedData: function (objName, saveType) {
        var arrParam = new Array();
        var roopData = $(".mrc-table td:nth-child(1) input:checked");
        $(".mrc-table td:nth-child(1) input:checked").each(function (index) {


            if (saveType == undefined) saveType = $(this).closest("tr").find("td:eq(0)").find(".editMode").text();
            var trObj = $(this).closest("tr");
            if (saveType == "N") { $(this).closest("tr").hide(); return true; }
            var param = new Object();

            var columnInfo = $.mrc_table.columnInfo(objName);
            var trObj = $(this).closest("tr");
            if (saveType != undefined) {
                param["MRC_EDIT_MODE"] = saveType;
            }
            for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD(trObj.find("td:eq(" + nCol + ")"));
            }
            param.MRC_EDIT_MODE = saveType;
            arrParam.push(param);

        })
        return arrParam;
    },
    GetSaveData: function (objName) {
        return $.mrc_table.GetData(objName, true);
    }
    , GetSelectRowData: function (objName, index) {
        var columnInfo = $.mrc_table.columnInfo(objName);
        var arrData = new Array();
        $(objName + " tbody tr.mrcrow eq(" + index + ")").each(function (nRow) {
            var param = new Object();
            for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD($(this).find("td:eq(" + nCol + ")"));
            }
            arrData.push(param);
        })
        if (arrData.length == 1) {
            return arrData[0];
        }
        else new Object();
    }
    , GetActiveRowData: function (objName) {
        var columnInfo = $.mrc_table.columnInfo(objName);
        var arrData = new Array();

        $(objName + " tbody tr.mrcrow.active").each(function (nRow) {
            var param = new Object();

            for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD($(this).find("td:eq(" + nCol + ")"));
            }

            arrData.push(param);
        })
        return arrData;
    }
    ,
    GetData: function (objName, bEdit, editMode) {
        if (bEdit == undefined) bEdit = false;

        editMode = editMode == undefined ? "" : editMode;
        var columnInfo = $.mrc_table.columnInfo(objName);
        var arrData = new Array();
        var saveRow = 0;

        $(objName + " tbody tr.mrcrow").each(function (nRow) {
            var param = new Object();
            if (bEdit == true && columnInfo[0].COLUMN_NAME == "MRC_EDIT_MODE") {
                var saveType = $(this).find("td:eq(0)").find(".editMode").text();
                if ($(this).find("td:eq(0)").hasClass("new")) {
                    saveType = "N";
                }
                if ((editMode == "" && saveType != "")
                    || (editMode != "" && saveType == editMode)
                    ) {
                    for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                        param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD($(this).find("td:eq(" + nCol + ")"));
                    }
                    param.MRC_EDIT_MODE = saveType;
                    param.ROW_NUM = nRow;
                    arrData[saveRow] = param;
                    saveRow++;
                }

            }
            else {
                for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                    param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD($(this).find("td:eq(" + nCol + ")"));
                }

                arrData[saveRow] = param;
                saveRow++;
            }
        })
        return arrData;
    },
    AddRowHtml: function (objName, trHtml, bLast) {
        bLast = (bLast == undefined) ? true : bLast;
        var bEdit = false;
        if ($(objName + " thead tr:last th:eq(0)").attr("column_name") == "MRC_EDIT_MODE") {
            bEdit = true;

            //if (String(trHtml).indexOf("chkEditMode") < 0) {
            //    trHtml = String(trHtml).replace("<tr>", "<tr>" + '<td class="new" style="text-align:center"><input type="checkbox" class="chkEditMode" /><span class="editMode">N</span></td>')
            //}
        }

        if (bLast) {
            $(objName + " tbody").append(trHtml);
            $(objName + " tbody tr:last").addClass("mrcrow");
            //$(objName + " tbody tr:last td:eq(" + (bEdit ? "1" : "0") + ")").prepend('<i name="btnExpend" class="fa fa-plus-circle" aria-hidden="true"></i>');
            //$.mrc_table.ExpandEventInit(objName, $(objName + " tbody tr.mrcrow").length - 1);
            //if (bEdit) $(objName + " tbody tr:last").prepend('<td class="new" style="text-align:center"><input type="checkbox" class="chkEditMode" /><span class="editMode">N</span></td>');
        }
        else {
            $(objName + " tbody").prepend(trHtml);
            $(objName + " tbody tr:first").addClass("mrcrow");
            //$(objName + " tbody tr:first td:eq(" + (bEdit ? "1" : "0") + ")").prepend('<i name="btnExpend" class="fa fa-plus-circle" aria-hidden="true"></i>');
            //$.mrc_table.ExpandEventInit(objName, 1);
            // if (bEdit) $(objName + " tbody tr:first").prepend('<td class="new" style="text-align:center"><input type="checkbox" class="chkEditMode" /><span class="editMode">N</span></td>');
        }

        $(objName + ".mrc-table th").each(function (index) {
            // alert($(this).attr("display_size"));
            if ($(this).attr("display_size") != undefined) {
                $(".mrc-table td:nth-child(" + String(index + 1) + ")").attr("display_size", $(this).attr("display_size"));
                $(".mrc-table td:nth-child(" + String(index + 1) + ")").attr("title", $(this).text());

            }
        })


    },
    DeleteRow: function (obj, callback) {
        var result = $.mrc_table.subToMainEvent(obj);
        return callback(result.selitem, result.trObject);
    }

    , GetDataInTD: function (objTD) {
        //
        var val = "";
        var tdData = objTD;
        if (tdData.find(".ui.dropdown").length == 1) {
            var data1 = tdData.find(".ui.dropdown").dropdown('get value');;
            if ($.isArray(data1)) {
                val = data1.join(",");
            }
            else
                val = data1;

        }
        else if (tdData.find("input").length == 1) {
            if (tdData.find("input").attr("type") == "checkbox") {
                //val = tdData.find("input")
                if (tdData.find("input").attr("value") == undefined || tdData.find("input").attr("value") == null || tdData.find("input").val() == "on") {
                    val = tdData.find("input").is(":checked")

                } else {
                    if (tdData.find("input").is(":checked")) val = tdData.find("input").val();
                    else val = null;

                }
            }
            else if (tdData.find("select")) {
                val = tdData.find("input").val();
            }
            else {
                val = tdData.text();
            }
        } else if (tdData.find("select").length == 1) {
            val = tdData.find("select").val();
        }
        else {
            val = tdData.text();
        }
        return val;
    }
    , subToMainEvent: function (obj) {
        try {
            if ($(obj).closest(".mrc_content").length == 1) {
                if (!(event.type == null || event.type == undefined)) {
                    var colIndex = $(obj).closest("div").parent().find("div").index($(obj).closest("div"));

                    $(obj).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(colIndex).find("input,button,select").trigger(event.type);

                }
                return null;
            }
        } catch (e) { }

        //  //
        var columnInfo1 = $.mrc_table.columnInfo(obj.closest("table"));
        var rtnParam = new Object();
        for (var i = 0; i < columnInfo1.length; i++) {

            var tdData = $(obj).parent().parent().find("td").eq(i);
            rtnParam[columnInfo1[i].COLUMN_NAME] = $.mrc_table.GetDataInTD(tdData);

        }
        var result = new Object();
        result.selitem = rtnParam;
        result.trObject = $(obj).parent().parent();
        return result;

        //$(obj).parent().parent().find("td").each(function (index) {

        //})


    },
    trTageEventInit: function (objName, bEdit) {

        $(objName + ".mrc-table .mrcrow").find("input, select").change(function () {
            var tagIndex = 0;
            try {

                $(this).attr("value", $(this).val());
                if ($(this).attr("type") == "checkbox") {
                    if ($(this).is(":checked")) { $(this).attr("checked", "checked"); }
                    else { $(this).removeAttr("checked"); }
                }
                if ($(this).closest('tr').next().hasClass("mrc_content")) {
                    tagIndex = $(this).closest("td[display_size=pc],td[display_size=tablet]").parent().find("td").index($(this).closest("td[display_size=pc],td[display_size=tablet]")) - ((bEdit) ? 2 : 1);

                    //$(this).closest('tr').next().find("div[display_size=pc],div[display_size=tablet]").eq(tagIndex).find("input,select").val($(this).val());



                    var contentTagObj = $(this).closest('tr').next().find("div[display_size=pc],div[display_size=tablet]").eq(tagIndex).find("input,select");
                    //
                    if ($(this).closest("td").find(".ui.dropdown").length == 1) {
                        // alert($(this).closest(".ui.dropdown").dropdown('get value'));
                        // contentTagObj.closest(".ui.dropdown").dropdown('clear');
                        contentTagObj.closest(".ui.dropdown").dropdown('set selected', $(this).closest("td").find(".ui.dropdown").dropdown('get value'));
                    }
                    else if (contentTagObj.attr("type") != undefined && contentTagObj.attr("type") == "checkbox") {
                        if ($(this).is(":checked")) {
                            contentTagObj.attr("checked", "checked"); contentTagObj.addClass("checked");
                            contentTagObj.prop("checked", true);
                            //contentTagObj.checkbox('check');
                        }
                        else {
                            contentTagObj.removeAttr("checked");
                            contentTagObj.removeClass("checked");
                            contentTagObj.prop("checked", false);
                            //contentTagObj.checkbox('uncheck');
                        }
                    }
                    else
                        if ($(this).closest(".ui.dropdown") == undefined || $(this).closest(".ui.dropdown") == null || $(this).closest(".ui.dropdown").length == 0)
                            contentTagObj.val($(this).val());
                        else {
                            contentTagObj.closest(".ui.dropdown").dropdown('set selected', $(this).val());
                        }
                }

                var chkType = $(this).attr("type") == undefined ? "text" : $(this).attr("type");
                if (!(chkType == "checkbox" && $(this).hasClass("chkEditMode"))) {
                    $(this).closest('tr').find(".editMode").text("U");
                    if (!$(this).closest('tr').find(".editMode").closest("td").hasClass("new")) {
                        $(this).closest('tr').find(".editMode").closest("td").addClass("update");
                    }
                }
            } catch (e) { }


        });
    }
    ,
    ExpandEventInit: function (objName, rowindex) {
        var sIndex = (rowindex == undefined || rowindex == null) ? "" : ":eq(" + rowindex + ")"
        $(objName + ".mrc-table tbody tr.mrcrow" + sIndex + " td i[name=btnExpend]").click(function () {

            if ($(this).hasClass("fa-plus-circle")) {
                if ($(this).closest("tr").next().hasClass("mrc_content")) {
                    $(this).closest("tr").next().remove();
                }
            }

            if ($(this).hasClass("fa-plus-circle")) {
                $(this).removeClass("fa-plus-circle");
                $(this).addClass("fa-minus-circle");
                var preTr = $(this).parent().parent();
                if (!preTr.next().hasClass("mrc_content")) {
                    var sHtml = "";
                    preTr.after('<tr class="mrc_content" ><td  colspan=' + String($(".mrc-table th").length) + '></td></tr>');

                    var maxWidth = 0;
                    preTr.find("td[display_size]").each(function (index) {
                        $(this).parent().next().find("td").append("<div " + ($(this).hasClass("hidden") == true ? "class='hidden'" : "") + "><div class='mrc_content_div' data-index=" + String(index)
                            + " display_size='" + $(this).attr("display_size") + "'><span class='title'>" + $(this).attr("title")
                            + "</span><span class='content'>" + $(this).html() + "</span></div></div><br/>");


                        var nWidth1 = $(this).parent().next().find("td .title:last").width();
                        if (maxWidth < nWidth1) {
                            maxWidth = nWidth1;
                        }
                    })

                    preTr.next().find("select").each(function (index) {
                        //
                        if ($(this).closest(".ui.dropdown").length == 1) {
                            //alert($(this).val());
                            //alert($(this).closest(".ui.dropdown").html());
                            $(this).closest(".ui.dropdown").dropdown('set selected', $(this).attr("value"));
                        }
                    });
                    preTr.next().find("td .title").css("min-width", maxWidth);
                    $(".mrc_content td span input").parent().css("padding-top", "0em");

                    preTr.next().find("input, select").change(function () {
                        //alert($(this).val());

                        var index = $(this).closest(".mrc_content_div").attr("data-index");//$(this).closest("div").parent().find("div").index($(this).closest("div"));
                        try {

                            var contentTagObj = $(this).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(index).find("input,select");
                            if (contentTagObj.closest("td").find(".ui.dropdown").length == 1) {
                                //alert($(this).closest(".ui.dropdown").dropdown('get value'));

                                //contentTagObj.closest("td").find(".ui.dropdown").dropdown('clear');
                                var insertHtml1 = contentTagObj.closest("td").find(".ui.dropdown select").outerHTML();

                                contentTagObj.closest("td").html(insertHtml1);

                                $("select").dropdown();
                                contentTagObj = $(this).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(index).find("input,select");
                                contentTagObj.closest(".ui.dropdown").dropdown('set selected', $(this).closest(".ui.dropdown").dropdown('get value'));
                            }
                            else if (contentTagObj.attr("type") != undefined && contentTagObj.attr("type") == "checkbox") {

                                if ($(this).is(":checked")) {
                                    contentTagObj.attr("checked", "checked"); contentTagObj.addClass("checked");
                                    contentTagObj.prop("checked", true);
                                    //contentTagObj.checkbox('check');
                                }
                                else {
                                    contentTagObj.removeAttr("checked");
                                    contentTagObj.removeClass("checked");
                                    contentTagObj.prop("checked", false);
                                    //contentTagObj.checkbox('uncheck');
                                }
                            }
                            else // if ($(this).closest(".ui.dropdown").length == 0)
                                contentTagObj.val($(this).val());
                            //else {
                            //    contentTagObj.closest(".ui.dropdown").dropdown("clear");
                            //    contentTagObj.closest(".ui.dropdown").dropdown('set selected', $(this).closest(".ui.dropdown").dropdown("get value"));
                            //}
                            $(this).closest(".mrc_content").prev().find(".editMode").text("U");
                            $(this).closest(".mrc_content").prev().find(".editMode").closest("td").addClass("update");
                        } catch (e) { }

                        ////$(obj).closest('td').parent()[0]
                        //var checkboxTag = $(this).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(index).find("input[type=checkbox]");
                        //var thisCheckboxTag = $(this);
                        //if (thisCheckboxTag.attr("type") == "checkbox") {
                        //    if (thisCheckboxTag.is(":checked")) $(this).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(index).find("input[type=checkbox]").attr('checked', true);
                        //    else $(this).closest(".mrc_content").prev().find("td[display_size=pc],td[display_size=tablet]").eq(index).find("input[type=checkbox]").attr('checked', false);
                        //    $(this).closest(".mrc_content").prev().find(".editMode").text("U");
                        //    $(this).closest(".mrc_content").prev().find(".editMode").closest("td").addClass("update");
                        //}
                    });
                }
                //alert($(this).html());

                $(this).parent().parent().next().show();


            }
            else {
                $(this).removeClass("fa-minus-circle");
                $(this).addClass("fa-plus-circle");
                $(this).parent().parent().next().hide();
            }
            try { SemanticUI.ComponentInit(); }
            catch (e) { }
        })
    }
    , EditInit: function (objName) {
        $.mrc_table.Init(objName, true)
    }

}


$.fn.outerHTML = function () {
    var el = $(this);
    if (!el[0]) return "";

    if (el[0].outerHTML) {
        return el[0].outerHTML;
    } else {
        var content = el.wrap('<p/>').parent().html();
        el.unwrap();
        return content;
    }
}

$(window).resize(function () {
    if ($(window).width() >= 1024) {
        //  setTimeout($(".mrc_content").remove(), 1000);

        $(".mrc-table tbody tr td [name='btnExpend']").removeClass("fa-minus-circle");
        $(".mrc-table tbody tr td [name='btnExpend']").addClass("fa-plus-circle");

        $(".mrc-table tbody tr.mrc_content").hide();
    }

});
/*************************************************/
/* 작성자 : 전상훈                               */
/* 작성일 : 2016.10.01                           */
/* 설  명 : 
  class 속성 
    1 : .mrc-table : 테이블 클래스 속성임
     
  Html Attribute 속성
   - column_name = "데이터객체를 만들기 위한 칼럼명
   - data-sort   = "정렬시 사용되는 실제 칼럼명 => 만일 a.name 로 설정했을 경우 sort 값이 a.name가 됨 없을 경우 column_name 를 사용 => mrcGrid_Sort 함수에서 사용됨
  class 속성
   - sort = 정렬 사용 시 추가
/*************************************************/
// 가로 스크롤바
(function ($) {
    $.fn.hasHorizontalScrollBar = function () {
        return this.get(0) ? this.get(0).scrollWidth > this.innerWidth() : false;
    }
})(jQuery);

// 세로 스크롤바
(function ($) {
    $.fn.hasVerticalScrollBar = function () {
        return this.get(0) ? this.get(0).scrollHeight > this.innerHeight() : false;
    }
})(jQuery);



$.mrc_table = {
    /**
     * 초기화
     * @param {any} objName 오브젝트명
     * @param {any} bEdit   입력여부
     * @param {any} sort    정렬
     * @param {any} addClass 추가클래스
     */
    Init: function (objName, bEdit, sort, addClass) {
        
        if (objName == ".mrc-table") objName = "";
        if (objName == "" || objName == undefined) objName = "";
        //$(".mrc-table input").attr("readonly", "readonly");
        bEdit = (bEdit == undefined) ? false : bEdit;
        if ($(objName + ".mrc-table").hasClass("editableGrid")) {
            $(objName + ".mrc-table th.editableCheck").remove()
            $(objName + ".mrc-table").removeClass("editableGrid")
        }
        if (bEdit != undefined && bEdit == true && !$(objName + ".mrc-table").hasClass("editableGrid")) {
            $(objName + ".mrc-table").addClass("editableGrid");
            $(objName + ".mrc-table thead tr").prepend('<th class="editableCheck" column_name="MRC_EDIT_MODE">&nbsp;</th>');
            $(objName + ".mrc-table tbody tr").each(function (idx) {
                if ($(this).find(".editableCheck").length == 0) {

                    $(this).prepend('<td class="editableCheck"><input type="checkbox" name="chkEditMode" class="chkEditMode ui checkbox" /><span class="editMode">');
                    if ($(this).hasClass("new")) {
                        $(this).find(".editableCheck input").prepend('N');
                    }
                    $(this).prepend('</span ></td > ');
                }
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

        $(objName + ".mrc-table tbody tr td:nth-child(" + ((bEdit) ? "2" : "1") + ")").each(function () {
            if ($(this).find(".fa-plus-circle").length == 0 || $(this).find(".fa-plus-circle") == undefined) {
                $(this).prepend('<i name="btnExpend" class="fa fa-plus-circle" aria-hidden="true"></i>');
                
                //$(this).css("padding-right", "2em");
            }
        })

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
            if (objName.indexOf(" .new") >= 0)
                objName = objName.replace(".new", "");
            objName = objName.trim();
            $(objName + ".mrc-table tbody tr").removeClass("active");
            $(objName + ".mrc-table tbody td").removeClass("active");
            $(this).addClass("active");
            try {
                var param = new Object();

                var columnInfo = $.mrc_table.columnInfo(objName);
                var trObj = $(this);
                for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                    param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD(trObj.find("td:eq(" + nCol + ")"));
                }
                mrc_table_trClick(objName, param);
                try {
                    var addFuncName = BaseCommon.StringInfo.replaceAll(objName, "#", "");
                    addFuncName = BaseCommon.StringInfo.replaceAll(addFuncName, " ", "");
                    addFuncName = BaseCommon.StringInfo.replaceAll(addFuncName, ".", "");
                    window["mrc_table_trClick_" +addFuncName].apply(null, [objName, param]);
                } catch (e) { }
            } catch (e) { }
        })

        $(objName + ".mrc-table tbody tr td").click(function (index) {
            if (objName.indexOf(" .new") >= 0)
                objName = objName.replace(".new", "");
            objName = objName.trim();
            $(objName + ".mrc-table tbody tr").removeClass("active");
            $(this).addClass("active");
            $(this).closest("tr").addClass("active");
            try {
                var param = new Object();

                var columnInfo = $.mrc_table.columnInfo(objName);
                var trObj = $(this);

                for (var nCol = 0; nCol < columnInfo.length ; nCol++) {
                    param[columnInfo[nCol].COLUMN_NAME] = $.mrc_table.GetDataInTD(trObj.find("td:eq(" + nCol + ")"));
                }
                mrc_table_tdClick(objName, param);
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

            var className = "fa-sort" + ((orderType == "") ? "" : "-" + (orderType.toLowerCase() == "asc" ? "up" :"down"));
            //alert(className);
            $(this).find("i.sort").removeClass("fa-sort");
            $(this).find("i.sort").addClass(className);
            var datasort = ($(this).attr("data-sort") == undefined ? $(this).attr("column_name") : $(this).attr("data-sort"));
            try {
                mrcGrid_Sort(objName, datasort, orderType);
            } catch (e) { }
            try {
                var addFuncName = BaseCommon.StringInfo.replaceAll(objName, "#", "");
                addFuncName = BaseCommon.StringInfo.replaceAll(addFuncName, " ", "");
                addFuncName = BaseCommon.StringInfo.replaceAll(addFuncName, ".", "");

                window["mrcGrid_Sort_" + addFuncName].apply(null, [objName, datasort, orderType]);
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
        
        if ($(objName + " th[display_size=detail]").length > 0) {
            $(objName + " tbody tr td i[name=btnExpend]").show();
        }
        try {
            objName = (objName == "" ? ".mrc-table" : objName);
            if (addClass != undefined) {
                SemanticUI.ComponentInit(objName + " ." + addClass);

            }
            else {
                SemanticUI.ComponentInit(objName);
            }

        }
        catch (e) { }
        $(objName + ".mrc-table input").focus(function () {
            $(this).select();
        })

        

        $.mrc_table.gridResize(objName);
        setTimeout($.mrc_table.gridHeaderResize(objName), 200);

    },
    /**
     * 페이지 조건 셋팅
     * @param {any} pageParam     페이지조건오브제트
     * @param {any} searchObjName 조건부오브젝트명
     * @param {any} minusHeight   기본높이 마이너스
     * @param {any} rowHeight     행높이
     */
    SetPageSize: function (pageParam, searchObjName, minusHeight, rowHeight) {
        minusHeight = minusHeight == undefined ? 400 : minusHeight;
        rowHeight = rowHeight == undefined ? 35 : rowHeight;
        var pageCount = ($(window).height() - $(".cd-main-header").outerHeight() + $(".ui.header").outerHeight() + $(searchObjName).outerHeight() + $(".ui.footer").outerHeight() - minusHeight) / rowHeight;

        pageParam.PAGE_COUNT = Number(Math.floor(pageCount));
        if ($(window).width()  < 768) {
            pageParam.PAGE_SIZE = 5;
           
        }
        else {
            pageParam.PAGE_SIZE = 10;
        }
        return pageParam;
    },
    /**
     * 칼럼정보
     * @param {any} objName 오브젝트명
     */
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
    /*칼럼순번*/
    columnIndex: function (objName, ColumnNmae) {
        
        var _index = -1;
        $(objName).find("thead tr:last th").each(function (index) {
            var name = $(this).attr("column_name") == undefined ? "NOT_COLUMN_" + String(index) : $(this).attr("column_name");
            if (name == ColumnNmae) {
                _index = index;
                return false;
            }
        });
        return _index;
    }
    ,/*체크 선택된 데이터 정보*/
    GetCheckedData: function (objName, saveType,updateCode) {
        var arrParam = new Array();
        var roopData = $(objName + ".mrc-table td:nth-child(1) input:checked");
        $(objName + ".mrc-table td:nth-child(1) input:checked").each(function (index) {
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
            if (updateCode != undefined) {
                param["UPDATE_CODE"] = updateCode;
            }
            param.MRC_EDIT_MODE = saveType;
            arrParam.push(param);

        })
        return arrParam;
    },/*수정된 저정데이터*/
    GetSaveData: function (objName) {
        return $.mrc_table.GetData(objName, true);
    } /* 인의의 로우 정보*/
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
    },
    SetFocus:function(name, row, col)
    {
        var currentTD = $(name).find("tbody tr:eq(" + row + ") td:eq(" + col + ")");
        if ($(currentTD).find(".ui.dropdown").length == 1)
        {
            $(name).closest(".dropdown").addClass("visible");
            $(name).closest(".ui.dropdown").find(".menu").removeClass("hidden");
            $(name).closest(".ui.dropdown").find(".menu").addClass("visible");
        }
        else if ($(currentTD).find("select").length == 1) {
            $(currentTD).find("select").focus();
        }
        else if ($(currentTD).find("input").length == 1) {
            $(currentTD).find("input").focus();
            $(currentTD).find("input").select();
        }
        else if ($(currentTD).find("textarea").length == 1) {
            $(currentTD).find("textarea").focus();
            $(currentTD).find("textarea").select();
        }
    },
    /*Row 임의 선택*/
    SetActiveIndex: function (name, index) {
        $(name + " tbody tr").removeClass("active");
        $(name + " tbody tr").eq(index).addClass("active");
    }, /* 선택된 로우 Index*/
    GetActiveIndex: function (name) {

        return $(name + " tr").index($(name + " tr.active"));
    }
     /* 선택된(Active) Row 정보*/
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
    , /*테이블 전체 데이터 정보*/
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
                if ($(this).hasClass("new") || $(this).find("td:eq(0)").hasClass("new")) {
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
    /* 데이터 행 추가 */
    AddRowHtml: function (objName, trHtml, bLast, ColfocusIdx) {
        bLast = (bLast == undefined) ? true : bLast;
        var bEdit = false;
        if ($(objName + " thead tr:last th:eq(0)").attr("column_name") == "MRC_EDIT_MODE") {
            bEdit = true;
        }
        var ActiveTr;
        if (bLast) {
            $(objName + " tbody").append(trHtml);
            $(objName + " tbody tr:last").addClass("mrcrow");
            ActiveTr = $(objName + " tbody tr:last");
        }
        else {
            $(objName + " tbody").prepend(trHtml);
            $(objName + " tbody tr:first").addClass("mrcrow");
            ActiveTr = $(objName + " tbody tr:first");
        }

        $(objName + ".mrc-table th").each(function (index) {
            // alert($(this).attr("display_size"));
            if ($(this).attr("display_size") != undefined) {
                $(this).closest(".mrc-table").find("td:nth-child(" + String(index + 1) + ")").attr("display_size", $(this).attr("display_size"));
                $(this).closest(".mrc-table").find("td:nth-child(" + String(index + 1) + ")").attr("title", $(this).text());
            }
        })
        try {
            $(objName + " tbody tr").removeClass("active");
            $(ActiveTr).addClass("active");
            var inputFocus;
            if (ColfocusIdx == undefined) {
                inputFocus = ActiveTr.find("td:eq(" + String(0) + ") input");
            }
            else {
                inputFocus = ActiveTr.find("td:eq(" + String(ColfocusIdx) + ") input");
            }

            inputFocus.select();
            inputFocus.focus();

            if ($(objName).closest(".table-wrapper") == undefined || $(objName).closest(".table-wrapper").length == 0) {
                
                $(window).scrollTop($(window).height());
            }
            else $(objName).closest(".table-wrapper").scrollTop($(objName).closest(".table-wrapper")[0].scrollHeight);
        } catch (e) { }
    },
    /* Key Value 추가*/
    getObjects: function (obj, key, val) {
        var objects = [];
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                objects = objects.concat(getObjects(obj[i], key, val));
            } else if (i == key && obj[key] == val) {
                objects.push(obj);
            }
        }
        return objects;
    },
    /*행삭제*/
    DeleteRow: function (obj, callback) {
        var result = $.mrc_table.subToMainEvent(obj);
        return callback(result.selitem, result.trObject);
    },
    /* td 안에 값 가져오기 */
    GetDataInTD: function (objTD) {
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
            else if (tdData.find("input").hasClass("date")) {
                try {
                    var objInput = tdData.find("input");
                    var id = tdData.find("input").attr("id");
                    if (!(id == undefined || id == null)) objInput = $("#" + id);
                    if (objInput.val() != "" && objInput.datetimepicker('getValue') != null) val = moment(objInput.datetimepicker('getValue')).format("YYYYMMDD");
                } catch (e) { val = "" }
            }
            else if (tdData.find("input").hasClass("time")) {
                try {
                    var objInput = tdData.find("input");
                    var id = tdData.find("input").attr("id");
                    if (!(id == undefined || id == null)) objInput = $("#" + id);
                    if (objInput.val() != "" && objInput.datetimepicker('getValue') != null) val = moment(objInput.datetimepicker('getValue')).format("HH:mm");
                } catch (e) { val = "" }
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
    },
    /* 콘텐츠에 이벤트를 행에 추가? 사용?  */
    subToMainEvent: function (obj) {
        try {
            if ($(obj).closest(".mrc_content").length == 1) {
                if (!(event.type == null || event.type == undefined)) {
                    var colIndex = $(obj).closest("div").parent().find("div").index($(obj).closest("div"));
                    $(obj).closest(".mrc_content").prev().find(",td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(colIndex).find("input,button,select").trigger(event.type);
                }
                return null;
            }
        } catch (e) { }

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
    },
    /*그리드 행에 이벤트 추가*/
    trTageEventInit: function (objName, bEdit) {

        //// 테이블 tr 안에 데이터 수정시
        $(objName + ".mrc-table .mrcrow").find("input, select").change(function () {

            var tagIndex = 0;
            try {

                $(this).attr("value", $(this).val());
                if ($(this).attr("type") == "checkbox") {
                    if ($(this).is(":checked")) { $(this).attr("checked", "checked"); }
                    else { $(this).removeAttr("checked"); }
                }
                if ($(this).closest('tr').next().hasClass("mrc_content")) {
                    //tagIndex = $(this).closest("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").parent().find("td").index($(this).closest("td[display_size=detail],td[display_size=pc],td[display_size=tablet]")) - ((bEdit) ? 2 : 1);
                    tagIndex = $(this).closest("td").attr("data-index");
                    //$(this).closest('tr').next().find("div[display_size=pc],div[display_size=tablet]").eq(tagIndex).find("input,select").val($(this).val());
                    var contentTagObj = $(this).closest('tr').next().find(".mrc_content_div").eq(tagIndex).find("input,select");

                    if ($(this).closest("td").find(".ui.dropdown").length == 1) {
                        // alert($(this).closest(".ui.dropdown").dropdown('get value'));
                        // contentTagObj.closest(".ui.dropdown").dropdown('clear');
                        try {

                            var arrCheck = $(this).closest("td").find(".ui.dropdown").dropdown('get value');
                            contentTagObj.closest(".mrc_content_div").find(".ui.dropdown select option").each(function (index) {
                                if ($.inArray($(this).attr("value"), arrCheck) >= 0) {
                                    $(this).attr("selected", "selected");
                                } else $(this).removeAttr("selected");
                            });

                            var insertHtml1 = contentTagObj.closest(".mrc_content_div").find(".ui.dropdown select, .ui input").outerHTML();

                            contentTagObj.closest(".mrc_content_div").html(insertHtml1);

                            SemanticUI.ComponentInit($(this).closest('tr').next().find(".mrc_content_div").eq(tagIndex));
                        }catch(e){}
                        //contentTagObj.closest(".ui.dropdown").dropdown('set selected', $(this).closest("td").find(".ui.dropdown").dropdown('get value'));
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
                        if ($(this).hasClass("date") || $(this).hasClass("time")) {
                            if ($(this).val() == "") {
                                contentTagObj.val("");
                            }
                            else {
                                contentTagObj.datetimepicker({ value: $(this).datetimepicker('getValue') });
                            }
                        }
                        else if ($(this).closest(".ui.dropdown") == undefined || $(this).closest(".ui.dropdown") == null || $(this).closest(".ui.dropdown").length == 0) {
                            contentTagObj.val($(this).val());
                        }
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
    },
    /*그리드 상세보기 이벤트시 상세에 이벤트 추가*/
    ExpandEventInit: function (objName, rowindex) {
        var sIndex = (rowindex == undefined || rowindex == null) ? "" : ":eq(" + rowindex + ")"
        $(objName + ".mrc-table tbody tr.mrcrow" + sIndex + " td i[name=btnExpend]").unbind("click");
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
                        $(this).attr("data-index", String(index));
                        $(this).parent().next().find("td").append("<div class='" + ($(this).hasClass("hidden") == true || $(this).hasClass("hide") ? "hidden " : "") + "mrc_content_div' data-index=" + String(index)
                            + " display_size='" + $(this).attr("display_size") + "'><span class='title'>" + $(this).attr("title")
                            + "</span><span class='content'>" + $(this).html() + "</span></div>");


                        if ($(this).hasClass("hidden") != true || $(this).hasClass("hide") != true) {
                            var nWidth1 = $(this).closest("tr.mrcrow").next().find(".title:last").width();
                            if (maxWidth < nWidth1) {
                                maxWidth = nWidth1;
                            }
                        }
                    })

                    preTr.next().find("select").each(function (index) {
                        if ($(this).closest(".ui.dropdown").length == 1) {
                            //alert($(this).val());
                            //alert($(this).closest(".ui.dropdown").html());
                            $(this).closest(".ui.dropdown").dropdown('set selected', $(this).attr("value"));
                        }
                    });

                    if (maxWidth == 0) maxWidth = 70;
                    preTr.next().find("td .title").css("min-width", maxWidth);
                    $(".mrc_content td span input").parent().css("padding-top", "0em");

                    preTr.next().find("input, select").change(function () {
                        //alert($(this).val());

                        var index = $(this).closest(".mrc_content_div").attr("data-index");//$(this).closest("div").parent().find("div").index($(this).closest("div"));
                        try {

                            var contentTagObj = $(this).closest(".mrc_content").prev().find("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(index).find("input,select");

                            if ($(this).hasClass("date") || $(this).hasClass("time")) {
                                if ($(this).val() == "") {
                                    contentTagObj.val("");
                                }
                                else {
                                    contentTagObj.datetimepicker({ value: $(this).datetimepicker('getValue') });
                                }
                            }
                            else if (contentTagObj.closest("td").find(".ui.dropdown").length == 1) {
                                //alert($(this).closest(".ui.dropdown").dropdown('get value'));

                                //contentTagObj.closest("td").find(".ui.dropdown").dropdown('clear');
                                var arrCheck = $(this).closest(".ui.dropdown").dropdown('get value');
                                contentTagObj.closest("td").find(".ui.dropdown select option").each(function (index) {
                                    if ($.inArray($(this).attr("value"), arrCheck) >= 0) {
                                        $(this).attr("selected", "selected");
                                    } else $(this).removeAttr("selected");
                                });

                                var insertHtml1 = contentTagObj.closest("td").find(".ui.dropdown select").outerHTML();

                                contentTagObj.closest("td").html(insertHtml1);

                                //$(this).closest(".mrc_content").prev().find("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(index);
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

                            SemanticUI.ComponentInit($(this).closest(".mrc_content").prev().find("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(index));
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
          
        })
    }
    , EditInit: function (objName) {
        $.mrc_table.Init(objName, true)
    },
    /*그리드 헤더 고정*/
    fixedHeder: function (objName) {
        //if ($(objName).closest(".table-wrapper") == undefined || $(objName).closest(".table-wrapper").length == 0) {
        //    //$(objName).parent().prepend('<div class="mrc-fixed-table-container"><div class="header-bg"></div><div class="table-wrapper">');
        //    //$(objName).parent().append("</div>");
        //    //$(objName).parent().append("</div>");

        //    //$(objName).parent().html('<div class="mrc-fixed-table-container"><div class="header-bg"></div><div class="table-wrapper">' + $(objName).outerHTML() + "</div></div>");
        //}
        $(objName).find("thead th").each(function () {
            if ($(this).find(".th-text").length == 0) {
                var sHtml = $(this).html();
                $(this).html("<div class='th-text'>" + sHtml + "</div>");
            }
        });

        setTimeout($.mrc_table.gridHeaderResize(objName), 200);
        try { window.dispatchEvent(new Event('resize')); } catch (e) { }
        //$(objName).find("thead th").each(function () {
        //    $(this).find(".th-text").width($(this).width());
        //})
     
        $(window).resize(function () {
            $.mrc_table.gridHeaderResize(objName);
            $(objName).find("thead th").each(function () {
                
                if ($(this).width() <= 0) $(this).find(".th-text").width($(this).css("min-width"));
                else $(this).find(".th-text").width($(this).width());
            })
        });

        if ($(objName).closest(".table-wrapper").get(0).scrollHeight > $(objName).closest(".table-wrapper").innerHeight()) {
            $(objName).css("border-bottom", "0px");
            $(objName + " td:last-child").css("border-right", "1px solid #b2b1b1");
        }
        $(".ui.footer").css("top", -45);
        setTimeout( $.mrc_table.gridHeaderResize(objName),200);
    },
    
    gridHeaderResize:function(objName)
    {
        var nWidth = $(objName).closest(".ui.tab") != undefined ? $(objName).closest(".ui.tab").width() : $(objName).width();
        $(objName).find("thead th").each(function () {

            if ($(this).width() <= 0) $(this).find(".th-text").width($(this).css("min-width"));
            else $(this).find(".th-text").width($(this).width());
        })
    },
    /*해상도에 따라 사이즈 재조정*/
    gridResize: function (objName) {
        
        if ($(window).width() >= 1024) {
            //  setTimeout($(".mrc_content").remove(), 1000);
            if (objName == undefined) objName = ".mrc-table";
            $(objName + " tbody tr td [name='btnExpend']").removeClass("fa-minus-circle");
            $(objName + " tbody tr td [name='btnExpend']").addClass("fa-plus-circle");

            $(objName + " tbody tr.mrc_content").hide();
        }

        if ($(objName + " tbody tr td [name='btnExpend']").is(":visible") == true)
            $(objName + " tbody tr td [name='btnExpend']").parent().css("padding-right", "3em");
        else
            $(objName + " tbody tr td [name='btnExpend']").parent().removeAttr("padding-right");

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
    $.mrc_table.gridResize();
});

function Grid_SemanticUI_ComboChange(obj) {
    if ($(obj).closest(".mrc-table") != undefined) {
        if ($(obj).closest(".mrc_content").length > 0) {
            var index = $(obj).closest(".mrc_content_div").attr("data-index");
            var contentTagObj = $(obj).closest(".mrc_content").prev().find("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(index).find("input,select");
            //contentTagObj.closest("td").find(".ui.dropdown").dropdown('clear');
            var arrCheck = $(obj).closest(".ui.dropdown").dropdown('get value');
            contentTagObj.closest("td").find(".ui.dropdown select option").each(function (index) {
                if ($.inArray($(this).attr("value"), arrCheck) >= 0) {
                    $(this).attr("selected", "selected");
                } else $(this).removeAttr("selected");
            });

            var insertHtml1 = contentTagObj.closest("td").find(".ui.dropdown select").outerHTML();
            contentTagObj.closest("td").html(insertHtml1);
            SemanticUI.ComponentInit($(obj).closest(".mrc_content").prev().find("td[display_size=detail],td[display_size=pc],td[display_size=tablet]").eq(index));
        }
        else {
            var index = $(obj).closest("td").attr("data-index");
            var contentTagObj = $(obj).closest('tr').next().find(".mrc_content_div").eq(index).find("input,select");
            var arrCheck = $(obj).closest("td").find(".ui.dropdown").dropdown('get value');
            contentTagObj.closest(".mrc_content_div").find(".ui.dropdown select option").each(function (index) {
                if ($.inArray($(this).attr("value"), arrCheck) >= 0) {
                    $(this).attr("selected", "selected");
                } else $(this).removeAttr("selected");
            });
            var insertHtml1 = contentTagObj.closest(".mrc_content_div").find(".ui.dropdown select, .ui input").outerHTML();

            contentTagObj.closest(".mrc_content_div").html(insertHtml1);

            SemanticUI.ComponentInit($(obj).closest('tr').next().find(".mrc_content_div").eq(index));
        }
    }
}
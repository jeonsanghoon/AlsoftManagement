
var KendoUI = {
    //// Date Time Picker Info
    DatePicker: {
        // From To Picker
        FormTo: function (from, to, sFormat) {
            if (sFormat == undefined) {
                sFormat = "MM/dd/yyyy";
            }
            // search date
            var start = $("#" + from).kendoDatePicker({
                format: sFormat,
                footer: false,
                change: function () {
                    var startDate = $("#" + from).val(),
                                    endDate = $("#" + to).val();

                    if (startDate) {
                        startDate = new Date(startDate);
                        startDate.setDate(startDate.getDate());
                        end.min(startDate);
                    } else if (endDate) {
                        start.max(new Date(endDate));
                    } else {
                        endDate = new Date();
                        start.max(endDate);
                        end.min(endDate);
                    }
                }
            }).data("kendoDatePicker");

            var end = $("#" + to).kendoDatePicker({
                format: sFormat,
                footer: false,
                change: function () {
                    var endDate = $("#" + to).val(),
                                startDate = $("#" + from).val();

                    if (endDate) {
                        endDate = new Date(endDate);
                        endDate.setDate(endDate.getDate());
                        start.max(endDate);
                    } else if (startDate) {
                        end.min(new Date(startDate));
                    } else {
                        endDate = new Date();
                        start.max(endDate);
                        end.min(endDate);
                    }
                }
            }).data("kendoDatePicker");

            start.max($("#" + to).val());
            end.min($("#" + from).val());
        },
        SetNowValue: function (id, value) {
            if (value == undefined) {
                var date = new Date();
                var sYear = String(date.getFullYear());
                var sMonth = (String(date.getMonth() + 1).length == 1) ? "0" + String(date.getMonth() + 1) : String(date.getMonth() + 1);
                var sDay = (String(date.getDay() + 1).length == 1) ? "0" + String(date.getDay() + 1) : String(date.getDay() + 1);
                $("#" + id).val(sMonth + "/" + sday + "/" + sYear);

            }
        }
    },
    DateTimePicker: {
        dateTimeEditor: function (container, options) {
            $('<input data-text-field="' + options.field + '" data-value-field="' + options.field + '" data-bind="value:' + options.field + '" data-format="' + options.format + '"/>')
            .appendTo(container)
            .kendoDateTimePicker({});
        }
    },
    //// Grid Info
    Grid: {
        rowIndex: 0
        , cellIndex: 0
        , Width: function (nWid) {
            return $(window).innerWidth() < 1200 ? "" : String(nWid) + "px";
        }
        ,

        selectRow: function (gridID, rowIndex) {

            var grid = $("#" + gridID).data("kendoGrid"),
                                    rowIndex = rowIndex,
                                    row = grid.tbody.find(">tr:not(.k-grouping-row)").eq(rowIndex);

            grid.select(row);
        },
        selectedRowIndex: function (gridID) {
            try {
                var grid = $("#" + gridID).data("kendoGrid");
                var dataRows = grid.items();
                return dataRows.index(grid.select());
            } catch (e) {
                return -1;
            }
        },
        DataURLSource: function (sUrl) {
            var data = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: sUrl,
                        dataType: "json"
                    }
                }
            });
            return data;
        },
        Data: function (id) {
            if ($("#" + id).data("kendoGrid") == undefined) {
                return $("#" + id).data("kendoExcelGrid")
            } else
                return $("#" + id).data("kendoGrid")
        }
        , DataSource: function (jsonData) {
            var dataSource = new kendo.data.DataSource({
                data: jsonData
            });
            return dataSource;
        },
        RowCount: function (id) {
            return $("#" + id).data("kendoGrid").dataSource.total();
        },
        ColumnCount: function (id) {
            return $("#" + id).data("kendoGrid").columns.length;
        },
        Columns: function (id) {
            return $("#" + id).data("kendoGrid").columns;
        },
        SelectItem: function (id) {
            var entityGrid = KendoUI.Grid.Data(id);
            return entityGrid.dataItem(entityGrid.select());
        },
        SetDataSource: function (id, jsonData) {
            var dataSource = new kendo.data.DataSource({
                data: jsonData
            });
            KendoUI.Grid.Data(id).setDataSource(dataSource);
        }
        ,
        GetDataSource: function (id) {
            return KendoUI.Grid.Data(id).dataSource.data();
        }
        , Progress: function (id, bShow) {
            try {
                kendo.ui.progress($("#" + id), bShow);
            } catch (e) { };
        }, GetSaveList: function (id) {
            var objList = KendoUI.Grid.GetDataSource(id);
            var saveList = new Array();
            var savemode = "";

            var nSave = 0;
            for (var i = 0; i < objList.length; i++) {
                try {
                    savemode = objList[i].mode;
                } catch (e) { }

                switch (savemode) {
                    case "N":
                    case "U":
                    case "D":

                        saveList.push(objList[i]);
                        nSave++;
                        break;
                }
            }


            return saveList;
        }, EditableInit: function (id) {
            KendoUI.Grid.EditableColorInit(id);
            KendoUI.Grid.MoveEvent(id);
        }

        , EditableColorInit: function (id) {
            
            var columns = KendoUI.Grid.Columns(id);
            for (var nRow = 0; nRow < KendoUI.Grid.RowCount(id) ; nRow++) {
                for (var nCol = 0; nCol < columns.length; nCol++) {
                    if (columns[nCol].field == "rowNumber") {
                        $("#" + id + " table tbody tr:eq(" + nRow + ") td:eq(" + nCol + ")").html(Number(nRow + 1));
                    }
                    //if (columns[nCol].editable != undefined && columns[nCol].editable == true) {
                    //    if (nRow % 2 == 1)
                    //        $("#" + id + " table tbody tr:eq(" + nRow + ") td:eq(" + nCol + ")").attr("style", "background-color:yellow;color:black;");
                    //    else
                    //        $("#" + id + " table tbody tr:eq(" + nRow + ") td:eq(" + nCol + ")").attr("style", "background-color:lightyellow;color:black;");
                    //}
                }
            };
        }, RowNumber: function (id) {
            var columns = KendoUI.Grid.Columns(id);
            for (var nRow = 0; nRow < KendoUI.Grid.RowCount(id) ; nRow++) {
                for (var nCol = 0; nCol < columns.length; nCol++) {
                    if (columns[nCol].field == "rowNumber") {
                        $("#" + id + " table tbody tr:eq(" + nRow + ") td:eq(" + nCol + ")").html(Number(nRow + 1));
                    }
                }
            };
        },

        MoveEvent: function (gridID) {

            $("#" + gridID).on("click", "td", function (e) {

                KendoUI.Grid.rowIndex = $(this).parent().index();
                KendoUI.Grid.cellIndex = $(this).index();
                window.onkeydown = function (event) {
                    if (event.keyCode == 9) {

                        try {
                            while (true) {
                                if (KendoUI.Grid.rowIndex == KendoUI.Grid.RowCount(gridID) - 1) {
                                    break;
                                }
                                if (KendoUI.Grid.ColumnCount(gridID) - 1 == KendoUI.Grid.cellIndex) {
                                    KendoUI.Grid.rowIndex = KendoUI.Grid.rowIndex + 1;
                                    KendoUI.Grid.cellIndex = 1;
                                } else {
                                    var grdColumn = KendoUI.Grid.Columns(gridID)[KendoUI.Grid.cellIndex + 1];
                                    if (grdColumn.editable == undefined || grdColumn.editable == false) {

                                        KendoUI.Grid.cellIndex = KendoUI.Grid.cellIndex + 1;
                                    }
                                    else {
                                        break;
                                    }
                                }
                            }
                        } catch (e) {
                            return;
                        }

                        $("#" + gridID).data("kendoGrid").editCell($(".k-grid-content").find("table").find("tbody").find("tr:eq(" + KendoUI.Grid.rowIndex + ")").find("td:eq(" + KendoUI.Grid.cellIndex + ")").next().focusin($("#" + gridID).data("kendoGrid").closeCell($(".k-grid-content").find("table").find("tbody").find("tr:eq(" + KendoUI.Grid.rowIndex + ")").find("td:eq(" + KendoUI.Grid.cellIndex + ")").parent())));
                        KendoUI.Grid.cellIndex = KendoUI.Grid.cellIndex + 1;
                    }
                    else if (event.keyCode == 13) {
                        KendoUI.Grid.rowIndex = KendoUI.Grid.rowIndex + 1;

                        $("#" + gridID).data("kendoGrid").editCell($(".k-grid-content").find("table").find("tbody").find("tr:eq(" + KendoUI.Grid.rowIndex + ")").find("td:eq(" + Number(KendoUI.Grid.cellIndex - 1) + ")").next().focusin($("#" + gridID).data("kendoGrid").closeCell($(".k-grid-content").find("table").find("tbody").find("tr:eq(" + KendoUI.Grid.rowIndex + ")").find("td:eq(" + Number(KendoUI.Grid.cellIndex - 1) + ")").parent())));


                    }
                }
            });

        }, resizeGrid: function (gridID, gridExceiptSize, contentSize, gridRowHeight) {
            
            if (gridRowHeight == undefined) gridRowHeight = 31;
            var nGnb = 0;
            if ($(".lNavi").css("display") != "none") nGnb = $(".lNavi").height();
            
            contentSize = 213; 
            contentSize = contentSize + nGnb + $(".form-inline").height();
            if (gridExceiptSize == undefined) gridExceiptSize = 30;
            else if (isNaN(gridExceiptSize)) gridExceiptSize = gridExceiptSize;
            else if (gridExceiptSize == true) gridExceiptSize = 70;
            
            var gridElement = $("#" + gridID),
                dataArea = gridElement.find(".k-grid-content"),
                gridHeight = gridElement.innerHeight(),
                otherElements = gridElement.children().not(".k-grid-content"),
                otherElementsHeight = 0;
            otherElements.each(function () {
                otherElementsHeight += $(this).outerHeight();
            });
            dataArea.height($(window).height() - contentSize - (gridExceiptSize - 30));
            
            pageSize = Math.round(Number(($(window).height() - contentSize - gridExceiptSize) / gridRowHeight));
            return pageSize;
        }
    },
    MultiSelect: {
        Init: function (id, data, valueField, textField) {
            if (valueField == undefined) {
                valueField = "value";
            }
            if (textField == undefined) {
                textField = "text";
            }
            $("#" + id).kendoMultiSelect({
                dataTextField: textField,
                dataValueField: valueField,
                dataSource: data
                , change: function (e) {
                    var value = this.value();
                    // Use the value of the widget
                    if (value != "") {
                        var selectid = e.sender._tagID.replace("_tag_active", "");
                        $('#' + id + "_taglist").parent().parent().removeClass("input-validation-error");
                    }
                }
            });
        }
        , InitFormDataSource: function (id, valueField, textField, url) {
            KendoUI.MultiSelect.Init(id, KendoUI.MultiSelect.GetDataSource(url, valueField), valueField, textField);
        }
        , GetDataSource: function (url, sId) {
            var gridDataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: url,
                        dataType: "json",
                        type: "POST"
                    }
                },
                schema: {
                    model: {
                        id: sId
                    }
                }
            });
            return gridDataSource;
        }
        /// selected List
        , GetDataList: function (id, opt) {
            if (opt == undefined) opt = "list";

            switch (opt) {
                case "tag":
                    return $("#" + id).data("kendoMultiSelect").tagList;
                case "options":
                    return $("#" + id).data("kendoMultiSelect").options;
                case "ul":
                    return $("#" + id).data("kendoMultiSelect").ul;
                case "data":
                    return $("#" + id).data("kendoMultiSelect").data();
                case "dataItems":
                    return $("#" + id).data("kendoMultiSelect").dataItems();
                default:
                    return $("#" + id).data("kendoMultiSelect").list;
            }
        }
        , SetValue: function (id, valueList) {
            //ex) valueList = ["1000", "1001"]
            if (valueList == "") {
                $("#" + id).data("kendoMultiSelect").value([""]);
            }
            else
                $("#" + id).data("kendoMultiSelect").value(valueList);
        }
        , SelectClear: function (id) {
            $("#" + id).data("kendoMultiSelect").value("");

        }
        , AddValue: function (id, value) {
            $("#" + id).data("kendoMultiSelect").dataSource.add(value);

        }, validationCheck: function (id) {
            $("#" + id).parent().addClass("input-validation-error");

        }
    }
    , ComboBox: {
        
        Init: function (id) {
            
            $("#" + id).kendoDropDownList({
                animation: false
            });
        }
         , SetIndex: function (id, index) {
             $("#" + id).data("kendoDropDownList").select($("#" + id).data("kendoDropDownList").ul.children().eq(index));
         }
        , SetValue: function (id, value) {
            $("#" + id).data("kendoDropDownList").value(value);
        }
        , Refresh: function (id) {
            $("#" + id).data("kendoDropDownList").refresh();
        }
        , GetValue: function (id) {
            return $("#" + id).data("kendoDropDownList").value();
            
        }
    }// kendoComboBox=>kendoDropDownList
    ,
    Upload: {
        Reset: function (id) {
            var up = $("#" + id).data().kendoUpload;
            var allLiElementsToBeRemoved = up.wrapper.find('.k-file');
            up._removeFileEntry(allLiElementsToBeRemoved);
        }
    }
    /* kendoNumericTextBox */
    , NumberBox: {
        SetVal: function (id, val) {
            return KendoUI.NumberBox.Data(id).value(val);
        },
        GetVal: function (id) {
            return KendoUI.NumberBox.Data(id).value();
        },
        Data: function (id) {
            return $("#" + id).data("kendoNumericTextBox");
        },
        GridColumnEditor: function (container, options) {

            //sangjoon 2014.08.06 : (수/발주)PO Management 발주단위(POUNIT_QTY)별 step 설정. 
            var step = (options.model.POUNIT_QTY == undefined) ? 1 : (options.model.POUNIT_QTY != 0 ? options.model.POUNIT_QTY : 1);

            $('<input id="numeric" name="' + options.field + '"/>')
                    .appendTo(container)
                    .kendoNumericTextBox({
                        format: "{0:n2}",
                        decimals: 2,
                        step: step,
                        min: 0
                    });

            //sangjoon 2014.08.18 : (수/발주)PO Management PO_TYPE별 Enable 설정.
            if (options.field == "PO_PRICE")
            {
                var IsEnable = options.model.PO_TYPE == "2" ? true : false;

                $("#grdList_active_cell #numeric").data("kendoNumericTextBox").enable(IsEnable);
            }
        }
    }
}
//////////////////////////////////////////////////////////////
// Del 키 클릭시 Textbox : Blank NumericTextbox : 0 으로 변경
//////////////////////////////////////////////////////////////
$(document).ready(function () {
    try{
        //setTimeout("kendoKeyupClear();", 500);
    } catch (e) { }
});


function kendoKeyupClear() {
    $(".k-textbox").keyup(function (e) {
        if (e.keyCode == 46) {
            $(this).val("");
        }
    });

    $(".k-input").each(function (index) {
        if ($(this).parent().parent().hasClass("k-numerictextbox")) {
            $(this).keyup(function (e) {
                if (e.keyCode == 46) {
                    $(this).val("0");
                }
            });
        }
    });
    $(".k-grid").each(function (index) {
        var gridid = $(this).attr("id");
        if ($(this).attr("data-role") == "grid") {
            $("#" + gridid).data("kendoGrid").bind("edit", function (e) {
                $(".k-input").keyup(function (e) {
                    if ($(this).attr("data-role") == "numerictextbox") {
                        if (e.keyCode == 46) {
                            $(this).val("0");
                        }
                    }
                    else if ($(this).attr("type") == "text") {
                        if (e.keyCode == 46) {
                            $(this).val("");
                        }
                    }
                });
            });
        }
        else if ($(this).attr("data-role") == "excelgrid") {
            $("#" + gridid).data("kendoExcelGrid").bind("edit", function (e) {
               
                $(".k-input").keyup(function (e) {

                    if ($(this).attr("data-role") == "numerictextbox") {
                        if (e.keyCode == 46) {
                            $(this).val("0");
                        }
                    }
                    else if ($(this).attr("type") == "text") {
                        if (e.keyCode == 46) {
                            $(this).val("");
                        }
                    }
                });
            });
        }
    });
    
}


$(document).ready(function () {
    try {
        $('.k-input,.k-textbox').on('focus', function () {
            var input = $(this);
            setTimeout(function () { input.select(); }, 200);
        });

        //$("input").focus(function () {
        //    $(this).select();
        //}).mouseup(
        //    function (e) {
        //        e.preventDefault();
        //    }
        //);

        setTimeout("kendoKeyupClear();", 500);

    } catch (e) { }
});
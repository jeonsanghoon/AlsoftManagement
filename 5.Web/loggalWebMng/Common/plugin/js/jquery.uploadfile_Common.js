var uploadFile = {


    init: function (id, actionUrl, fromData2, bMulti, callback, acceptFileTypes, bDrag) {

        if (fromData2 == undefined) fromData2 = null;
        if (bMulti == undefined) bMulti = false;

        if (acceptFileTypes == undefined || acceptFileTypes == null || acceptFileTypes == "image") {
            acceptFileTypes = ".gif, .jpg, .png"
        } else if (acceptFileTypes == "movie") {
            acceptFileTypes = ".flv,.mp4,.avi,.wmv,.3gp";
        }

        /*if (acceptFileTypes != undefined && acceptFileTypes != null) {
            $.each(String(acceptFileTypes).split('|'), function (index, value) {
                if (String(value).toLowerCase() == "image") {
                    if (acceptFiles.length > 0) acceptFiles = acceptFiles + ",";
                    acceptFiles = acceptFiles + ".gif,.jpg,.png,.bmp"
                }
                if (String(value).toLowerCase() == "movie") {
                    if (acceptFiles.length > 0) acceptFiles = acceptFiles + ",";
                    acceptFiles = acceptFiles + ".flv,.mp4,.avi,.wmv,.3gp"
                }
            })
        }*/
        try {

            return $("#" + id).uploadFile({
                url: actionUrl,
                multiple: bMulti,
                formData: fromData2,
                acceptFiles: ((acceptFileTypes == undefined || acceptFileTypes == "*") ? "*" : acceptFileTypes),
                maxFileSize: 200000000, //20 MB
                sizeErrorStr: "는(은) 허용사이즈를 초과하였습니다.. 허용되는 파일사이즈: ",
                extErrorStr: "는(은) 허용되지 않습니다. 허용되는 확장자 :",
                uploadErrorStr: "업로드를 실패하였습니다.",

                onSelect: function (files) {

                }
                , onLoad: function (files) {
                    $("#dvLoading").removeClass("active");

                    $(".ajax-upload-dragdrop form").append("<input type=\"text\" style=\"display: none;\" />");
                    $(".ajax-upload-dragdrop form").attr("onsubmit", "return false;");
                    //    $("#dvLoading").addClass("active");
                    try {
                        upLoadFileLoad(id);

                    } catch (e) { }

                    if (!(bDrag == undefined || bDrag == true)) { $(id).prev().find("span").hide; }
                    callback(null, "road");
                },

                //  fileName: "myfile",
                onSubmit: function (files) {
                    $("#dvLoading").removeClass("active");
                    $("#dvLoading").addClass("active");
                    //                $("#eventsmessage").html($("#eventsmessage").html() + "<br/>Submitting:" + JSON.stringify(files));

                },
                onSuccess: function (files, data, xhr) {
                    //                $("#eventsmessage").html($("#eventsmessage").html() + "<br/>Success for: " + JSON.stringify(data));

                    callback(data);
                    $("#dvLoading").removeClass("active");
                    /*if (data.return_msg != "")
                        alert(data.return_msg);
                    else
                        alert("성공");*/
                },
                afterUploadAll: function () {
                    // $("#eventsmessage").html($("#eventsmessage").html() + "<br/>All files are uploaded");
                    $("#dvLoading").removeClass("active");
                },
                onError: function (files, status, errMsg) {
                    $("#dvLoading").removeClass("active");
                    $.MessageBox(errMsg);
                    //$("#eventsmessage").html($("#eventsmessage").html() + "<br/>Error for: " + JSON.stringify(files));
                }
            });
        } catch (e) {
            alert(e);
        }


    }
}
var uploadFile = {
    init: function (id, actionUrl, fromData2, bMulti, callback) {

        if (fromData2 == undefined) fromData2 = null;
        if (bMulti == undefined) bMulti = false;


        $("#" + id).uploadFile({
            url: actionUrl,
            multiple: bMulti,
            formData: fromData2,
            onLoad: function (files) {

                $("#dvLoading").removeClass("active");
                $(".ajax-upload-dragdrop form").append("<input type=\"text\" style=\"display: none;\" />");

                $(".ajax-upload-dragdrop form").attr("onsubmit", "return false;");
                //    $("#dvLoading").addClass("active");
                callback(files, "load");
            },
            //  fileName: "myfile",
            onSubmit: function (files) {
                //                $("#eventsmessage").html($("#eventsmessage").html() + "<br/>Submitting:" + JSON.stringify(files));

            },
            onSuccess: function (files, data, xhr) {
                //                $("#eventsmessage").html($("#eventsmessage").html() + "<br/>Success for: " + JSON.stringify(data));

                callback(data);
                /*if (data.return_msg != "")
                    alert(data.return_msg);
                else
                    alert("성공");*/
            },
            afterUploadAll: function () {
                // $("#eventsmessage").html($("#eventsmessage").html() + "<br/>All files are uploaded");

            },
            onError: function (files, status, errMsg) {
                //$("#eventsmessage").html($("#eventsmessage").html() + "<br/>Error for: " + JSON.stringify(files));
            }
        });

        


    }
}
/// ReplaceAll
$.fn.ReplaceAll = function (rtn1, rtn2) {
    return $("#" + this.selector).val().split(rtn1).join('');//.replace(eval("/" + rtn1 + "/gi"), rtn2);
}
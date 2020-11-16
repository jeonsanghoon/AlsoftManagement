DatetimePicker = {
	SetVal: function (objName, val) {
		try {
			if (val == null || val == undefined || val == "") val = null;
			else {
				val = val.split('.').join('');
				val = val.split('-').join('');
				val = val.split('/').join('');

				val = new Date(val.substr(0, 4), val.substr(4, 2), val.substr(6, 2))
			}
			$(objName).datetimepicker({ value: val });
		} catch (e) { $(objName).datetimepicker({ value: null }); }

	},
	GetVal: function (objName, format)
	{
		if ($(objName).val() == "") return "";
		try {
			if (format == undefined) format = "YYYYMMDD";
			return moment($(objName).datetimepicker('getValue')).format(format);
		} catch (e) {
			return "";
		}
	},GetDate : function(id)
	{
		return $(objName).datetimepicker('getValue');
    }, SetMinDate: function (obj, val) {
        obj.setOptions({
            minDate: val ? val : false
        });
    }, SetMaxDate: function (obj, val) {
        obj.setOptions({
            maxDate: val ? val : false
        });
    }, setLayout: function (obj) {
        if (obj.offset().left < 242 && $(window).width() - obj.offset().left < 242) {
            setTimeout('$($(".xdsoft_datetimepicker")[0]).css("left","1em")', 10);
        }
    },/*From To DateTimePicker 초기화*/
    frtoInit: function (frObj, toObj) { 
        frObj = $(frObj);
        toObj = $(toObj);
        frObj.datetimepicker({
            timepicker: false,
            format: 'Y.m.d',
            onShow: function (ct) {
                DatetimePicker.setLayout(frObj);
                DatetimePicker.SetMaxDate(this, toObj.val());
            }
        });

        toObj.datetimepicker({
            timepicker: false,
            format: 'Y.m.d',
            onShow: function (ct) {
                DatetimePicker.SetMinDate(this, frObj.val());
            }
        });
    }
}
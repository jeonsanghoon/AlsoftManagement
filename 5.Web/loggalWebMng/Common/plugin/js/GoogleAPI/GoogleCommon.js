
var googleApi = {
    Getlocation : function(addr, callback)
    {
        var address = encodeURIComponent(addr);
        var geocode = "http://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&sensor=false";
        jQuery.ajax({
            url: geocode,
            type: 'POST',
            success: function (myJSONResult) {
                if (myJSONResult.status == 'OK') {
                    var tag = "";
                    var i;
                    var rtnObj = new Object();
                    for (i = 0; i < myJSONResult.results.length; i++) {
                        rtnObj.address = myJSONResult.results[i].formatted_address; //주소
                        rtnObj.lat = myJSONResult.results[i].geometry.location.lat; //위도
                        rtnObj.lng = myJSONResult.results[i].geometry.location.lng; //경도
                        callback(rtnObj);
                    }
                } else if (myJSONResult.status == 'ZERO_RESULTS') {
                    alert("지오코딩이 성공했지만 반환된 결과가 없음을 나타냅니다.\n\n이는 지오코딩이 존재하지 않는 address 또는 원격 지역의 latlng을 전달받는 경우 발생할 수 있습니다.")
                } else if (myJSONResult.status == 'OVER_QUERY_LIMIT') {
                    alert("할당량이 초과되었습니다.");
                } else if (myJSONResult.status == 'REQUEST_DENIED') {
                    alert("요청이 거부되었습니다.\n\n대부분의 경우 sensor 매개변수가 없기 때문입니다.");
                } else if (myJSONResult.status == 'INVALID_REQUEST') {
                    alert("일반적으로 쿼리(address 또는 latlng)가 누락되었음을 나타냅니다.");
                }
            }
        });
    }
}
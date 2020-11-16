//var json1 = { "channel": { "totalCount": "1", "link": "http:\/\/developers.daum.net\/services", "result": "1", "generator": "Daum Open API", "pageCount": "1", "lastBuildDate": "", "item": [{ "mountain": "", "mainAddress": "0", "point_wx": "562238", "point_wy": "974842", "isNewAddress": "N", "buildingAddress": "", "title": "\uacbd\uae30 \uc548\uc131\uc2dc", "placeName": "Not avaliable", "zipcode": "", "newAddress": "", "localName_2": "\uc548\uc131\uc2dc", "localName_3": "", "localName_1": "\uacbd\uae30", "lat": 37.0079949936, "point_x": 127.2797136494, "lng": 127.2797136494, "zone_no": "", "subAddress": "0", "id": "RB7218", "point_y": 37.0079949936 }], "title": "Search Daum Open API", "description": "Daum Open API search result" } };
var DaumApi = {
    Map: {
        /*주소→좌표 변환*/
        addr2coord: function (address, callback, errorcallback, basync) {
            /*https://developers.daum.net/services/apis/local/geo/addr2coord 참조*/

            if (basync == undefined) basync = true;
            var params = new Object();
            params.ADDRESS = address;
            params = JSON.stringify(params);
            ajax.GetAjax("/common/GetDaumMapApiAddressTolnglat/", params, "json", function (result) {
         
                
               
               
                if (result.length != 0) {
                    //$("#LATITUDE").val(obj.channel.item[0].lat);
                    //$("#LONGITUDE").val(obj.channel.item[0].lng);
                    try {
                        result[0].lat = result[0].y;
                        result[0].lng = result[0].x;
                    } catch (e) { }
                    callback(result[0]);
                }
                else {
                    var rtnObj = new Object();
                    rtnObj.lat = 0;
                    rtnObj.lng = 0;
                    callback(rtnObj);
                }
            }, basync, function (result) {
                errorcallback(result);
            });
            
        }
    }
}


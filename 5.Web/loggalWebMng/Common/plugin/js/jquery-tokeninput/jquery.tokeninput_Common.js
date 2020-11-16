
var tokeninput =
    {
        GetData: function (id) {
            return $("#" + id).tokenInput("get");
        }
        , AddData: function (id, param) {
            /*selector.tokenInput("remove", {id: x});*/
            $("#" + id).tokenInput("add", param);
        }, RemoveData: function (id, param) {
            /* selector.tokenInput("remove", {id: x});
            selector.tokenInput("remove", {name: y});
            */
            $("#" + id).tokenInput("remove", param);
        }, ClearData: function (id) {
            $("#" + id).tokenInput("clear");
        }

    }



    /*tokeninput 생성 정의*/
    /*
    var tokeninput = {
        _id : "",
        _param: new Object(),
        _url:"",
        init: function (id, url, param, callback) {
            if (param == undefined || param == null || param == "") param = new Object();
            _id = id;
            _param = param;
            _url=url;
    
            $("#" + id).tokenInput(url, {
                theme: (param.theme == undefined ? "facebook" : param.theme)
               , method: (param.method == undefined ? "GET" : param.method)
               , preventDuplicates: (param.preventDuplicates == undefined ? true : param.preventDuplicates)
               , queryParam: (param.queryParam == undefined ? 'q' : param.queryParam)
               , searchDelay: (param.searchDelay == undefined ? 0 : param.searchDelay)
               , minChars: (param.minChars == undefined ? 0 : param.minChars)
               , tokenDelimiter: (param.tokenDelimiter == undefined ? ',' : param.tokenDelimiter)
               , hintText: (param.hintText == undefined ? "검색어를 입력하세요" : param.hintText)
               , noResultsText: (param.noResultsText == undefined ? "데이터가 없습니다." : param.noResultsText)
               , searchingText: (param.searchingText == undefined ? "조회중..." : param.searchingText)
               , onResult: function (item) {
                   if (_param.isNoItemAdd == undefined || _param.isNoItemAdd == true) {
                       if ($.isEmptyObject(item)) {
                           return [{ id: $("#token-input-" + _id).val(), name: $("#token-input-" + _id).val() }];
                       } else {
                           return item;
                       }
                    }
                   return callback("onResult", _id, _param, item);
                }, onAdd: function (item) { return callback("onAdd", _id, _param, item); }
               , onReady: function (item) { return callback("onReady", _id, _param, item); }
    
            });
        }
    }
*/
/* https://github.com/CreativeDream/jquery.modal */
//////
// Jquery.Modal 공통화 작업
var MsgBox = {
    MsgType : {
        alert: 'alert'
        , confirm: 'confirm'
        , prompt: 'prompt'
        , success: 'success'
        , warning: 'warning'
        , info: 'info'
        , error: 'error'
        , inverted: 'inverted'
        , primary: 'primary'
    },
    Alert : function(title, text, callbackFunc)
    {
        
        modal({
            type: MsgBox.MsgType.alert,
            title: (title == "") ? '알림' : title,
            text: text
                  , closeClick: false
                 , callback: function (result){
                     try {
                         if (callbackFunc == undefined) return null;
                            callbackFunc(result);
                        } catch (e) { }
                    }
        });
    },
    Warning: function (title, text, callbackFunc) {
        
      
        modal({
            type: MsgBox.MsgType.warning,
            title: (title == "") ? '경고' : title,
            text: text
                  , closeClick: false
                 , callback: function (result) {
                     try {
                         if (callbackFunc == undefined) return null;
                         callbackFunc(result);
                     } catch (e) { }
                 }
        });
    },
    Confirm: function (title, text, callbackFunc) {
        
        modal({ /* https://github.com/CreativeDream/jquery.modal */
            type: MsgBox.MsgType.confirm,
            title: (title == "") ? '선택' : title,
            text: text
                  , closeClick: false
                 , callback: function (result) {
                     try {
                         if (callbackFunc == undefined) return null;
                         callbackFunc(result);
                     } catch (e) { }
                 }
        });
    },
    Message: function (type, title, text, callbackFunc)
    {
        
        modal({ /* https://github.com/CreativeDream/jquery.modal */
            type: type,
            title: title,
            text: text
                  , closeClick: false
                  , callback: function (result) {
                      try {
                          if (callbackFunc == undefined) return null;
                          callbackFunc(result);
                      } catch (e) { }
                  }
        });
    }
}
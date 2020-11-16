/*******************************************/
/* Semantic UI Pagination                  */
/* 작성자 : 전상훈                         */
/* 작성일 : 2016.09.01                     */
/* 수정일 : 2018.10.01                     */
/* 내  용 : 현재 옵션값가져오기            */
/* 이메일 : mrc0700@gmail.com              */
/*******************************************/

/* 사용예) 
  <div class="ui right floated pagination menu"></div>
    $(document).ready(function () {
            $(".ui.pagination.menu").pagination({
                total: 35, page: 1, pagesize: 10, onPageClick:function(item, page) {
                    alert(page);
                }
            })
    })
*/

(function($) {
   
    $.fn.pagination = function (options) {

        $.fn.pagination.defaults.obj = $(this);
        options = $.extend({}, $.fn.pagination.defaults, options);
        var element = $(this);
        
        if (options.total == 0) $(this).hide();
        var SetPage = function () {

            options.obj.html("");
            if (options.isFirstLast) {
                options.obj.append("<a class=\"first icon item\"><i style=\"font-weight:bold\" class=\"angle double left icon\"></i></a>");
            }
            options.obj.append("<a class=\"prev icon item\"><i style=\"font-weight:bold\" class=\"angle left icon\"></i></a>");

            options.totalPage = Math.ceil(options.total / options.perpage);

            var nFirst = Math.floor((options.page - 1) / options.pagesize) * options.pagesize + 1;
            var nLast = nFirst - 1 + options.pagesize;
            var nLastPageFirstNum = Math.floor((options.totalPage - 1) / options.pagesize) * options.pagesize + 1;

            nLast = options.totalPage < nLast ? options.totalPage : nLast;
            nLast = (nLast <= 0) ? 1 : nLast;
            for (var i = nFirst; i <= nLast  ; i++) {
                options.obj.append("<a id=\"page_" + i.toString() + "\" class=\"page item" + ((i == options.page) ? " active" : "") + "\">" + i + "</a>");
            }
            if (options.totalPage == 0) {
                options.obj.find("#page_1").addClass("disabled");
            }
            options.obj.append("<a class=\"next icon item\"><i style=\"font-weight:bold\" class=\"angle right icon\"></i></a>");
            if (options.isFirstLast) {
                options.obj.append("<a class=\"last icon item\"><i style=\"font-weight:bold\"  class=\"angle double right icon\"></i></a>");
            }

            if (options.page <= options.pagesize) {
                options.obj.find(".first").addClass("disabled");
                options.obj.find(".prev").addClass("disabled");
            }

            if (options.page >= nLastPageFirstNum) {
                options.obj.find(".last").addClass("disabled");
                options.obj.find(".next").addClass("disabled");
            }

            options.obj.css("margin-left", (options.obj.parent().width() / 2.00) - ((options.obj.width() < 50) ? 169.6 / 2.00 : options.obj.width() / 2.00));
            $("window").resize(function () {
                options.obj.css("margin-left", (options.obj.parent().width() / 2.00) - ((options.obj.width() < 50) ? 169.6 / 2.00 : options.obj.width() / 2.00));
            });
            options.obj.css("postion", "absolute");
            
        }

        var SetPageEvent = function () {
            SetPage();
            options.obj.find(".item").on("click", function (event) {
                if ($(this).hasClass("first")) {
                    options.page = 1;
                }
                else if ($(this).hasClass("last")) {
                    options.page = options.totalPage;
                }
                else if ($(this).hasClass("prev")) {
                    options.page = options.page - options.pagesize;
                }
                else if ($(this).hasClass("next")) {
                    options.page = options.page + options.pagesize;
                }
                else {
                    options.page = Number($(this).text());
                }
                if (options.page < 1) options.page = 1;
                if (options.page > options.totalPage) options.page = options.totalPage;
                //  SetPage();
                SetPageEvent();
                options.onPageClick.call(this, this, options.page);
            })
            options.obj.find("a.disabled").unbind("click");;
            options.obj.find("a").removeClass("active");

            options.obj.find("#page_" + options.page.toString()).addClass("active");
            $.fn.pagination.options = options;
        }
        SetPageEvent();
        $.fn.pagination.options = options;
    }
    $.fn.pagination.defaults = {
        total: 1,  /*총건수*/
        pagesize: 10, /*한번에보여줄페이지수*/
        perpage: 15, /*페이지 당 출력수*/
        page: 1,      /*현재 페이지*/
        totalPage: 1, /*표현되는 페이지 총수*/
        isFirstLast: true, /*처음으로가기, 마지마으로 가기 옵션*/
        obj: $(this)   /*페이징처리된 객체*/
    };
    $.fn.pagination.options = {};

})(jQuery);


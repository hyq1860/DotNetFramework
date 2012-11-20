/*
http://www.cnblogs.com/tonywang711/archive/2012/03/29/2419985.html
*/
//为了更好的兼容，开始前面有个分号
; (function ($) {
    $.fn.tabpanel = function (options) {
        // 默认参数
        var defaults = {
            width: 500,
            height: 400
        };

        // 配置参数合并
        var config = $.extend(defaults, options || {});

        // 功能函数
        var methods = {
            //初始化
            init:function (options) {
                
            },

            add: function () {
                alert(arguments[0]);
            }
        };

        // 函数调用逻辑
        var isMethod = typeof options === 'string';
        var method;
        if (isMethod) {
            method = options;
            if (methods[method]) {
                return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
            } else if (typeof method === 'object' || !method) {
                return methods.init.apply(this, arguments);
            } else {
                $.error('Method ' + method + ' does not exist on jQuery.tabpanel');
            }
        }
        

        var tabContainer = this;
        var ulWarp = "<ul class='ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all' />";
        var liItem = "<li class='ui-state-default ui-corner-top'><a href='#tabs-1'>标签1</a><a class='ui-icon  ui-icon-circle-close tabclose'></a></li>";
        var panelItem = "<div id='tabs-1' class='ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide'></div>";
        var iframeItem = "<iframe name='' id='' width='100%' height='100%' src='' frameBorder='0' />";


        init();
        initTabCloseEvent();

        // 插件的回调方法
        // 初始化成功后回调函数
        //if (typeof config.initFn == 'function') { // 确保类型为函数类型
        if ($.isFunction(config.initFn)) {
            config.initFn.call(this); // 执行回调函数
        }

        /*私有方法开始*/

        // 初始化，设置默认的选项卡
        function init() {
            tabContainer.addClass("ui-tabs ui-widget ui-widget-content ui-corner-all");
            tabContainer.append($(ulWarp).append(liItem));
            var pannelItemTemp = $(panelItem);
            if (pannelItemTemp.hasClass("ui-tabs-hide")) {
                pannelItemTemp.removeClass("ui-tabs-hide");
            }
            tabContainer.append(pannelItemTemp.append(iframeItem));
        }

        // 关闭按钮绑定事件
        function initTabCloseEvent() {
            tabContainer.find("ul>li>a.tabclose").each(function () {
                $(this).bind('click', function () {
                    //关闭事件
                    alert("haha");
                });
            });
        }
        /*私有方法开始*/
    };
})(jQuery);  
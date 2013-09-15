//为了更好的兼容，开始前面有个分号
;(function($) {
    var tabId = 0,
	listId = 0;

    function getNextTabId() {
        return ++tabId;
    }

    function getNextListId() {
	    return ++listId;
    }

    // html模板
    var template = {
        idPrefix: "ui-tabs-",
        ulContainer: "<ul class='ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all' />",
        liItem: "<li class='ui-state-default ui-corner-top'><a href='#tabs-1'>首页</a><a class='ui-icon  ui-icon-closethick tabclose'></a></li>",
        panelContainer: "<div></div>",
        iframeItem: "<iframe width='100%' frameBorder='0' />"
    };

    var methods = {
        
        // 初始化
        init:function (options) {
            // 默认参数
            var defaults = {
                items:[],
                width: 500,
                height: 400
            };
            // 默认参数与用户提供参数合并
            var config = $.extend(defaults, options || { });

            return this.each(function() {
                var $this = $(this);

                //add({title:'首页',type:'html',content:'dddddddddddd'});
                //初始化默认首页
                initDefault($this);
                //initDefaultTabCloseEvent($this);
                
                data = $this.data('tabpanel');
                // 如果插件还没有初始化
                if(!data) {

                }
            });
        },
        //
        add:function (option) {
            /*
            参数格式：
            var obj = {
                title:"",
                type:"iframe,html",
                content:''//if type==iframe 则content为url html时，则content为html
            };
            
            */
            

            // 未提供参数
            if(typeof(option) == "undefined"||!option) {
                alert("参数不能为undefined或null");
                return;
            }

            var self = this;
            
            // 判断标题一样的再次选中，激活
            var lias = $("ul>li", self);
            // 是否找到同样标题的tab
            var flage = false;
            if(lias) {
                $.each(lias, function() {
                    if($(this).find("a").first().text() != option.title) {
                        return true;
                    }
                    //标题相等的,激活，不再新增
                    //除自身以外的，其他激活的都去掉选中与激活的
                    lias.not($(this)).removeClass("ui-tabs-selected ui-state-active");
                    //激活选中自身
                    $(this).addClass("ui-tabs-selected ui-state-active");
                    //panel
                    var id = $(this).attr("id").replace("tab", "panel");
                    self.find(">div").addClass("ui-tabs-hide");
                    $("#" + id).removeClass("ui-tabs-hide");
                    flage = true;
                    return false;
                });
            }
            if(flage) {
                return;
            }
            var count=1;
//            if(self.data('tabpanelCount')) {
//                count = self.data('tabpanelCount');
//                self.data('tabpanelCount',++count);
//            }else {
//                self.data('tabpanelCount', ++count);
//            }
//            if(count>3) {
//                alert("最多只有三个选项卡");
//                return;
//            }
            var li = $(template.liItem);
            //设置标题
            $(">a", li).text(option.title);

            var currentIndex = getNextTabId();
            li.attr("id", "tab-id-"+currentIndex);

            //去除其他的标签的激活状态
            var list = self.find( "ol,ul" ).eq( 0 );
		    var lis = $( " > li:has(a[href])", list );
            lis.removeClass( "ui-tabs-selected ui-state-active" );
            
            //去除panel的其他激活，隐藏
            var panels = self.find(">div");//找panel集合
            panels.addClass("ui-tabs-hide");

            //激活新增的标签
            if(!li.hasClass("ui-tabs-selected")) {
                li.addClass("ui-tabs-selected");
            }
            if(!li.hasClass("ui-state-active")) {
                li.addClass("ui-state-active");
            }
            // 选项卡选中事件
            li.bind('click', function() {
                $(this).parent().find("> li").removeClass("ui-tabs-selected ui-state-active");
                $(this).addClass("ui-tabs-selected ui-state-active");
                // 解决panel的选中问题
                self.find(">div").addClass("ui-tabs-hide");
                var pannelId= $(this).attr("id").replace("tab","panel");
                $("#"+pannelId).removeClass("ui-tabs-hide");
            });
            //关闭按钮添加事件
            li.find(".tabclose").bind('click', function() {
                //li处理
                li.prev().addClass("ui-tabs-selected ui-state-active");
                var pannelId= li.attr("id").replace("tab","panel");
                $("#" + pannelId).prev().removeClass("ui-tabs-hide");
                $("#"+pannelId).remove();
                li.remove();
                //panel处理
            });
            
            
            self.find("ul").append(li);
            var pannel = $(template.panelContainer);
            pannel.attr("id", "panel-id-"+currentIndex);
            pannel.addClass("ui-tabs-panel ui-widget-content ui-corner-bottom");
            //pannel.append("<div>"+currentIndex+"</div>");
            
            /**/

            if(option.type=="iframe") {
                var iframe = $(template.iframeItem);
                iframe.attr("src", option.content).attr("id", "iframe-" + currentIndex);
                iframe.load(function() {
                    //自适应高度 目前不能解决跨域的问题 暂时只支持同一域名
                    var mainheight = $(this).contents().find("body").height() + 10;
                    var height = $(this).parent().parent().parent().height() - 80;
                    $(this).height(mainheight < height ? height : mainheight);
                });
                iframe.appendTo(pannel);
            }else if(option.type="html") {
                pannel.html(option.content);
            }
            
            /**/
            

            self.append(pannel);
            if ($.isFunction(option.initFn)) {
                option.initFn.call(this);// 执行回调函数
            }
        }
    };

    //内部方法开始
    // 初始化，设置默认的选项卡
    function initDefault(tabContainer) {
        tabContainer.addClass("ui-tabs ui-widget ui-widget-content ui-corner-all");
        
         //设置标题
         var li = $(template.liItem);
         $(">a", li).text("首页");

         var currentIndex = getNextTabId();
         li.attr("id", "tab-id-"+currentIndex);
         li.bind('click', function() {
                $(this).parent().find("> li").removeClass("ui-tabs-selected ui-state-active");
                $(this).addClass("ui-tabs-selected ui-state-active");
                // 解决panel的选中问题
                tabContainer.find(">div").addClass("ui-tabs-hide");
                var pannelId= $(this).attr("id").replace("tab","panel");
                $("#"+pannelId).removeClass("ui-tabs-hide");
            });

        tabContainer.append($(template.ulContainer).append(li));
        var pannelItemTemp = $(template.panelContainer).addClass("ui-tabs-panel ui-widget-content ui-corner-bottom").attr("id", "panel-id-"+currentIndex);
        if (pannelItemTemp.hasClass("ui-tabs-hide")) {
            pannelItemTemp.removeClass("ui-tabs-hide");
        }

        tabContainer.append( pannelItemTemp.append( $(template.iframeItem).attr("src","http://www.baidu.com")));
    }

    // 关闭按钮绑定事件,第一个选项卡不允许关闭
    function initDefaultTabCloseEvent(tabContainer) {
        tabContainer.find("ul>li>a.tabclose").each(function() {
            $(this).bind('click', function() {
                //关闭事件
                alert("haha");
            });
        });
    }
    //内部方法结束

    $.fn.tabpanel = function(method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.tabpanel');
        }
    };

})(jQuery);
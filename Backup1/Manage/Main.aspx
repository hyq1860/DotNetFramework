<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="../../tabpanel/jQuery.TabPanelV3.js"></script>
    <script type="text/javascript" src="../../jQuery/themeswitchertool.js/"></script>
    <script type="text/javascript" src="../../jQuery/jquery.layout.min.js"></script>
   <%-- <link rel="stylesheet" href="../../jQueryUI/css/ui-lightness/jquery-ui-1.8.20.custom.css" type="text/css" media="all" />--%>
    <link type="text/css" href="../../jQuery/layout-default.css" rel="Stylesheet" />	
    
<%--    <script type="text/javascript" src="../../ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="../../ueditor/editor_config.js"></script>
<script type="text/javascript" src="../../ueditor/editor_all.js"></script>
<link rel="stylesheet" href="../../ueditor/themes/default/ueditor.css"/>--%>
<script type="text/javascript">
    $(document).ready(function () {

        //皮肤切换
        $('#switcher').themeswitcher();

        var options = {
            width: 100,
            items: [{
                title: "首页",
                html: '<iframe width="100%" height="100%" frameborder="0"></iframe>',
                closeable: false
            }],
            initFn: function () {
                alert('初始化成功后回调');
            }
        };

        $('.tab-link').click(addTab);

        $("#tabs2").tabpanel(options);

        $("#accordion")
            .accordion({
                header: "> div > h5",
                collapsible: true,
                autoHeight: true,
                navigation: false,
                fillSpace: false,
                icons: { "header": "ui-icon-plus", "headerSelected": "ui-icon-minus" }
            })
            .sortable({
                axis: "y",
                handle: "h5",
                stop: function (event, ui) {
                    // IE doesn't register the blur when sorting
                    // so trigger focusout handlers to remove .ui-state-focus
                    ui.item.children("h3").triggerHandler("focusout");
                }
            });


        $('body').layout({
            applyDefaultStyles: false, //应用默认样式  
            north__closable: false, //可以被关闭  
            north__resizable: false, //可以改变大小  
            north__size: 70, //
            west__closable: true, //可以被关闭  
            west__resizable: false, //可以改变大小  
            west__size: 170, //
            spacing_open: 6, //边框的间隙  
            spacing_closed: 6, //关闭时边框的间隙
            togglerTip_open: "关闭", //pane打开时，当鼠标移动到边框上按钮上，显示的提示语  
            togglerTip_closed: "打开", //pane关闭时，当鼠标移动到边框上按钮上，显示的提示语 
            togglerAlign_open: "left", //pane打开时，边框按钮显示的位置  
            togglerAlign_closed: "left"//pane关闭时，边框按钮显示的位置 
            //            togglerContent_open:	'<div class="ui-icon"></div>',//pane打开时，边框按钮中需要显示的内容可以是符号"<"等。需要加入默认css样式.ui-layout-toggler .content 
            //	        togglerContent_closed:	'<div class="ui-icon"></div>'//pane关闭时，同上。 
            //            scrollToBookmarkOnLoad: false, //页加载时滚动到标签  
            //            showOverflowOnHover: false, //鼠标移过显示被隐藏的，只在禁用滚动条时用。  


            //            resizerTip: "可调整大小", //鼠标移到边框时，提示语  
            //            //resizerCursor:"resize-p" 鼠标移上的指针样式  
            //            resizerDragOpacity: 0.9, //调整大小边框移动时的透明度  
            //            maskIframesOnResize: "#ifa", //在改变大小的时候，标记iframe（未通过测试）  
            //            sliderTip: "显示/隐藏侧边栏", //在某个Pane隐藏后，当鼠标移到边框上显示的提示语。  
            //            sliderCursor: "pointer", //在某个Pane隐藏后，当鼠标移到边框上时的指针样式。  
            //            slideTrigger_open: "dblclick", //在某个Pane隐藏后，鼠标触发其显示的事件。(click", "dblclick", "mouseover)  
            //            slideTrigger_close: "click", //在某个Pane隐藏后，鼠标触发其关闭的事件。("click", "mouseout")  

            //            togglerLength_open: 100, //pane打开时，边框按钮的长度  
            //            togglerLength_closed: 200, //pane关闭时，边框按钮的长度  
            //            hideTogglerOnSlide: true, //在边框上隐藏打开/关闭按钮(测试未通过)  

            //            togglerContent_open: "<div style='background:red'>1</div>", //pane打开时，边框按钮中需要显示的内容可以是符号"<"等。需要加入默认css样式.ui-layout-toggler .content   
            //            togglerContent_closed: "<img/>", //pane关闭时，同上。  
            //            enableCursorHotkey: true, //启用快捷键CTRL或shift + 上下左右。  
            //            customHotkeyModifier: "shift", //自定义快捷键控制键("CTRL", "SHIFT", "CTRL+SHIFT"),不能使用alt  
            //            south__customHotkey: "shift+0", //自定义快捷键（测试未通过）  
            //            fxName: "drop", //打开关闭的动画效果  
            //            fxSpeed: "slow"//动画速度  
            //fxSettings: { duration: 500, easing: "bounceInOut" }//自定义动画设置(未通过测试)  
            //initClosed:true,//初始时，所有pane关闭  
            //initHidden:true //初始时，所有pane隐藏  
        });
    });

    function addTab() {
       
        var title = $(this).attr("title");
        var src = $(this).attr("rel");
        $("#tabs2").tabpanel("add", { title: title, type: 'iframe', content: src });
//        $.get("../Ajax/UserControlContainer.ashx?control=" + $(this).attr("control"),function(data) {
//            $("#tabs2").tabpanel("add", {title:title,type:'html',content:data});
//        });

        //$("#tabs2").tabpanel("add", title, src);
    }

</script>

<style type="text/css">
    *{ padding: 0;margin: 0;}
    .group{ padding: 0;}
    .group div{ padding: 0;}
    .group h5{ font-size: 12px;}
    .group ul li{ font-size: 12px;}
    .group ul{list-style-type:none;margin:0px; }
    .group ul li{ padding:0px;}
    .group ul li a{line-height:24px;text-decoration: none;}
    .group ul li div{margin:0px 0px;padding-left:0px;padding-top:2px;}
    .group ul li div .hover{border:1px dashed #99BBE8; background:#E0ECFF;cursor:pointer;}
    .group ul li div .hover a{color:#416AA3;}
    .group ul li div.selected{border:1px solid #99BBE8; background:#E0ECFF;cursor:default;}
    .group ul li div.selected a{color:#416AA3; font-weight:bold;}
    
.icon {
    background: url("css/tabicons.png") no-repeat scroll 0 0 transparent;
    display: inline-block;
    line-height: 18px;
    width: 18px;
}
.icon-sys {
    background-position: 0 -200px;
}
.icon-set {
    background-position: -380px -200px;
}
.icon-add {
    background-position: -20px 0;
}

.icon-nav {
    background-position: -100px -20px;
}
.icon-users {
    background-position: -100px -480px;
}
.icon-role {
    background-position: -360px -200px;
}
.icon-set {
    background-position: -380px -200px;
}
.icon-log {
    background-position: -380px -80px;
}

.icon-delete {
    background-position: -140px -120px;
}
.icon-edit {
    background-position: -380px -320px;
}
.icon-magic {
    background-position: 0 -500px;
}
.icon-database {
    background-position: -20px -140px;
}

.icon-depart {
    background-position: -220px -140px;
}
.icon-wuzi {
    background-position: -60px -140px;
}
.icon-jihua {
    background-position: -260px -80px;
}
.icon-gongyinshang {
    background-position: -240px -80px;
}
.icon-code {
    background-position: -80px -80px;
}

     .ui-tabs .ui-tabs-nav li a.tabclose{ padding: 0;}
</style>
</head>
<body>
    
    <div id="tabContent" class="ui-layout-center">
        
        <!--结构开始-->
        <div id="tabs2" >

        </div>
        <!--结构结束-->

    </div>

    <div class="ui-layout-north">
    
        <div id="switcher"></div>
        你好：<%=UserName %>
    </div>

    <div class="ui-layout-west">

        <div id="accordion">
	        <div class="group">
		        <h5><a href="#">产品管理</a></h5>
		        <div>
			    <ul>
                    <li>
                        <div class="">
                            <a class="tab-link" title="产品列表" rel="ProductList.aspx" control="UserControls/ProductForm" ><span class="icon icon-users">&nbsp;</span><span class="nav">产品列表</span></a>
                        </div>
                    </li>
                    <li>
                        <div class="">
                            <a class="tab-link" title="产品分类" rel="ProductCategoryList.aspx" ><span class="icon icon-role">&nbsp;</span><span class="nav">产品分类</span></a>
                        </div>
                    </li> 
                </ul>
		        </div>
	        </div>
	        <div class="group">
		        <h5><a href="#">网站设置</a></h5>
		        <div>
                <ul>
                    <li>
                        <div class="">
                            <a class="tab-link" title="基本内容" rel="BasicContentList.aspx" ><span class="icon icon-users">&nbsp;</span><span class="nav">基本内容</span></a>
                        </div>
                    </li>
                    <li>
                        <div class="">
                            <a class="tab-link" title="新增内容" rel="BasicContentForm.aspx" ><span class="icon icon-role">&nbsp;</span><span class="nav">新增内容</span></a>
                        </div>
                    </li> 
                </ul>
		        </div>
	        </div>
	        <div class="group">
		        <h5><a href="#">其他</a></h5>
		        <div>
			        <p>jj</p>
		        </div>
	        </div>
        </div>

    </div>






</body>
</html>

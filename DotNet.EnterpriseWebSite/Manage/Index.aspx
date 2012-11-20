<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Index" %>
<%@ Import Namespace="Combres" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>后台管理</title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
    <link href="<%=RootPath %>/Styles/default.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/windowControl.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/gciframe.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.panel.extension.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/treegrid.loadFilter.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/Scripts/common.js"></script>
    
    
    
    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>  
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/CommonValidation.js"></script>  
    <%--<%= WebExtensions.CombresLink("backendJs")%>--%>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <script type="text/javascript">
    var _menus = <% =json%> ;
    $(document).ready(function() {
        InitLeftMenu();
        tabClose();
        $('#mb').menubutton({
            menu: '#mm'
        });
        //var clock = new Clock();
        //clock.display(document.getElementById("clock"));
    });

    //收起页面顶栏
    function shouQi() {
        //$('.easyui-layout').layout('collapse','north');
        $('#win').window({
            width: 600,
            height: 400,
            modal: true,
            href:"HtmlSnippet.aspx"
        });
    }
    function menuModify() {

            alert("menuModify");
        }
    </script>

<style type="text/css">
.maintop{float: right;padding: 10px 20px 0 0;}.maintop p{ margin:5px 0 0;}
.maintop a{margin: 0 10px 0 0;}
</style>
</head>
<body class="easyui-layout">

    <div region="north"  split="true" border="false" style="overflow: hidden; height: 30px;
      background: url(../images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px;color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <%--<span id="clock"></span>--%>
        <span style="padding-left:10px; font-size: 16px; "><img src="../images/blocks.gif" width="20" height="20" align="absmiddle" /> 网站后台管理</span>
        <span style="float:right; padding-right:20px;">
            <a href="javascript:void(0)" id="mb" menu="#mm">系统设置</a>
            <div id="mm" style="width:150px;">
    		    <%--<div iconCls="icon-undo" onclick="menuModify()">按钮-</div>
		        <div class="menu-sep"></div>--%>
		        <div>
			        <span>换肤</span>
			        <div style="width:150px;">
				        <div onclick="sy.changeTheme('default')">默认</div>
				        <div onclick="sy.changeTheme('gray')">灰色</div>
                        <div onclick="sy.changeTheme('sunny')">sunny</div>
			        </div>
		        </div>
	        </div>
        </span>
        <span style="float:right; padding-right:20px;" class="head">
            欢迎 <%= UserName%>
            <%--<a href="#" id="editpass">修改密码</a>--%>
            <%--<a href="#" id="loginOut">安全退出</a>--%>
        </span>
        
        
</div>

<div region="west" title="功能导航" split="true" title="West" style="width:150px;">
    <div id="nav" class="easyui-accordion" fit="true" border="false">
	</div>
</div>

<div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden">
    <div id="tabs" class="easyui-tabs"  fit="true" border="false">
		<div id="introduce" title="欢迎使用" style="padding:20px;overflow:hidden; color:red; display: none" >
			<div>
			系统使用须知
			</div>
		</div>
        
<%--        <div id="test" title="欢迎使用" href="HtmlSnippet.aspx" style="padding:4px;overflow:hidden; " >
		</div>--%>
<%--         <div id="Div1" title="欢迎使用" href="jqgridHtmlSnippet.aspx" style="padding:4px;overflow:hidden; " >
		</div>--%>

	</div>
</div>



</body>
<div id="win"></div>
</html>






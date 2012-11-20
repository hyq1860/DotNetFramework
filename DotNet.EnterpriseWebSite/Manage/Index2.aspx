<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Index2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>后台管理</title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/gciframe.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Scripts/common.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <link href="<%=RootPath %>/Styles/default.css" rel="stylesheet" type="text/css" />
    
<%--    <script type="text/javascript" src="<%=RootPath %>/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="<%=RootPath %>/ueditor/themes/default/ueditor.css"/>
--%>
    <script type="text/javascript" src="<%=RootPath %>/ueditor1.2.1.0/editor_config.js"></script>
    <%--<script type="text/javascript" src="../ueditor1.2.1.0/editor_all.js"></script>--%>
    <link rel="stylesheet" href="<%=RootPath %>/ueditor1.2.1.0/themes/default/ueditor.css"/>
    <link rel="stylesheet" href="<%=RootPath %>/ueditor1.2.1.0/themes/default/iframe.css"/>
    <script type="text/javascript" charset="utf-8" src="<%=RootPath %>/ueditor1.2.1.0/editor_api.js"></script>


    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>  
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/CommonValidation.js"></script>  

    <script type="text/javascript">
    var _menus = <% =json%> ;
    $(document).ready(function() {
        InitLeftMenu();
        tabClose();
        var clock = new Clock();
        clock.display(document.getElementById("clock"));
    });

    //收起页面顶栏
    function shouQi() {
        //$('.easyui-layout').layout('collapse','north');
        $('#tabs').tabs('add', {
            title: 'New Tab',
            content: 'Tab Body',
            closable: true,
            href:"Snippet2.aspx"
        });
    }
    </script>

<style type="text/css">
.maintop{float: right;padding: 10px 20px 0 0;}.maintop p{ margin:5px 0 0;}
.maintop a{margin: 0 10px 0 0;}
</style>
</head>
<body class="easyui-layout">

<div region="north" border="false" style="background: url(images/11.jpg) repeat-x;height:58px;">
<a onclick="shouQi()">点我</a>

</div>

<div region="west" title="功能导航" split="true" title="West" style="width:150px;">
    <div id="nav" class="easyui-accordion" fit="true" border="false">
	</div>
</div>

<div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden">
    <div id="tabs" class="easyui-tabs"  fit="true" border="false">

        
        <div id="test" title="欢迎使用" href="Snippet.aspx" cache="false" style="padding:4px;overflow:hidden; " >
		</div>
        <div id="Div1" title="欢迎使用2" href="Snippet2.aspx" cache="false" closable="true" style="padding:4px;overflow:hidden; " >
		</div>

	</div>
</div>



</body>
<div id="win"></div>
</html>






<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
        <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/UserCenter.ashx"></script>
    	<script type="text/javascript">
    	    $(document).ready(init);
    	    function init() {
    	        $('#login').window({
    	            title: '系统登录',
    	            width: 320,
    	            height: 240,
    	            closable: false,
    	            collapsible: false,
    	            minimizable: false,
    	            maximizable: false,
    	            draggable: false,
    	            resizable:true,
    	            modal: true
    	        });

    	        $(window).keydown(function (event) {
    	            switch (event.keyCode) {
    	                case 13:
    	                    login();
    	                    break;
    	            }
    	        });
    	    }
    	    function login() {
    	        $.DotNet.UserCenter.Login($("#name").val(), $("#password").val(), function (result) {
    	            if (parseInt(result) > 0) {
    	                window.location.href = "Index.aspx";
    	            }
    	            else {
    	                $.messager.alert('提示消息','登录失败');
    	            }
    	        });
    	    }
	</script>
</head>
<body>
<div id="login">
<form id="form1">
    <table style="width:100%;height: 100%">
        <tr>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td align="right">用户名：</td>
            <td><input type="text" id="name" name="name" required="true" /></td>
        </tr>
        <tr>
            <td align="right">密码：</td>
            <td><input  type="password" id="password" name="password"  required="true" /></td>
        </tr>
        <tr>
            <td></td>
            <td><a href="#" class="easyui-linkbutton" iconCls="icon-save" onclick="login()">登录</a></td>
        </tr>
    </table>

</form>
</div>
</body>
</html>

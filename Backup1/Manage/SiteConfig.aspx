<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteConfig.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.SiteConfig" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/windowControl.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/gciframe.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.panel.extension.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/treegrid.loadFilter.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/Scripts/common.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <link href="<%=RootPath %>/Styles/default.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>  
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/CommonValidation.js"></script> 
     
</head>--%>
<%--<body>--%>
    <table id="SiteConfigProperty"></table>

<%--</body>--%>

<script type="text/javascript">
    $(function () {
        $('#SiteConfigProperty').propertygrid({
            title: '网站配置',
            width: 600,
            height: 'auto',
            url: '<%=RootPath %>/Ajax/Common.ashx/GetSiteConfig',
            showGroup: false,
            scrollbarSize: 0,
            showHeader: true,
            columns: [[
            { field: 'name', title: '属性', width: 100 },
            { field: 'value', title: '值', width: 500 }
            ]],
            onAfterEdit: function (rowIndex, rowData, changes) {
                $.DotNet.Common.UpdateSiteById(rowData.OptionId, rowData.value, function(result) {
                    if (result == 0) {
                        alert("保存失败，请重试或者联系管理员");
                    }
                });
            }
        });
    });
</script>
<%--</html>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ueditor_demo01.aspx.cs" Inherits="DotNet.EnterpriseWebSite.ueditor_demo.ueditor_demo01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script type="text/javascript" src="../ueditor/editor_config.js"></script>
<script type="text/javascript" src="../ueditor/editor_all.js"></script>
<link rel="stylesheet" href="../ueditor/themes/default/ueditor.css"/>
</head>
<body>
<div id="myEditor"></div>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor();
    editor.render("myEditor");
</script>
</body>
</html>

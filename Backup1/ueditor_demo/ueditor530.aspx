<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ueditor530.aspx.cs" Inherits="DotNet.EnterpriseWebSite.ueditor_demo.ueditor530" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="../ueditor1.2.1.0/editor_config.js"></script>
<%--<script type="text/javascript" src="../ueditor1.2.1.0/editor_all.js"></script>--%>
<link rel="stylesheet" href="../ueditor1.2.1.0/themes/default/ueditor.css"/>
<script type="text/javascript" charset="utf-8" src="../ueditor1.2.1.0/editor_api.js"></script>
<script type="text/javascript" src="../ckfinder/ckfinder.js"></script>

<!--
在ui/editorui.js中，添加'Mycard'/*自定义按钮*/,
-->

<script type="text/javascript">
    function SetFileField(fileUrl) {
        var html = "<img src=" + fileUrl + " />";
        editor_a.execCommand("inserthtml", html);
    }

</script>
<script type="text/javascript">
    //实现插件的功能代码
    baidu.editor.commands['ckfinder'] = {
        execCommand: function () {
            var finder = new CKFinder();
            finder.basePath = '../'; // The path for the installation of CKFinder (default = "/ckfinder/").
            finder.resourceType = 'Images'; //Resource type to display. By default CKFinder displays all available resource types. If ResourceType property is set, CKFinder will display only specified resource type. 
            finder.selectActionFunction = SetFileField;
             
            finder.removePlugins = 'help,basket';// Remove the "Help" button.和零时文件夹
            finder.popup();
            return true;
        }
    };
</script>
</head>
<body>
<!--style给定宽度可以影响编辑器的最终宽度-->
    <script type="text/plain" id="myEditor" style="width:1000px">
        <p>这里我可以写一些输入提示</p>
    </script>
    <script type="text/javascript">
        var editor_a = new baidu.editor.ui.Editor();
        editor_a.render('myEditor');
    </script>
</body>
</html>

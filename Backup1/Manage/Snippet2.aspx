<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Snippet2.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Snippet2" %>
<%--<div id="loading" style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: #DDDDDB; text-align: center; padding-top: 20%;"></div>--%>

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

            finder.removePlugins = 'help,basket'; // Remove the "Help" button.和零时文件夹
            finder.popup();
            return true;
        }
    };
</script>


<!--style给定宽度可以影响编辑器的最终宽度-->
    <script type="text/plain" id="myEditor" style="width:1000px">
        <p>这里我可以写一些输入提示</p>
    </script>
    <script type="text/javascript">
        var editor_a = new baidu.editor.ui.Editor();
        editor_a.render('myEditor');
    </script>


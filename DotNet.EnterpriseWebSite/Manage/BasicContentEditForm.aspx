<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicContentEditForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.BasicContentEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="../../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="../../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="../../jQueryUI/css/ui-lightness/jquery-ui-1.8.20.custom.css" />
    <script type="text/javascript" src="../../Ajax/Common.ashx"></script>
    <script type="text/javascript" src="../ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="../ueditor/editor_config.js"></script>
    <script type="text/javascript" src="../ueditor/editor_all.js"></script>
    <link rel="stylesheet" href="../ueditor/themes/default/ueditor.css"/>
    <script type="text/javascript">
        $(function () {
            //按钮设置区域
            $("#save").button();
            $("#return").button();

            //表单区域
            var options = {
                url: '../Ajax/Common.ashx/UpdateBasicContent',
                success: function () {
                    alert('保存成功');
                }
            };
            $('#form1').ajaxForm(options);
        })

    </script>
</head>
<body>
<form id="form1" runat="server">
<table>
    <tr>
        <td>标题：</td>
        <td>
            <input type="hidden" name="BasicContentId" value="<%=BasicContent.Id %>"/>
            <input type="text" name="Title" value="<%=BasicContent.Title %>"/>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>分类名称：</td>
        <td><label><%=BasicContent.Category %></label></td>
        <td></td>
    </tr>
    <tr>
        <td>导航链接：</td>
        <td><input type="text" name="LinkPath" value="<%=BasicContent.LinkPath %>"/></td>
        <td></td>
    </tr>
    <tr>
        <td>内容：</td>
        <td>
            <script type="text/plain" id="myEditor"><%=BasicContent.Content %></script>
            <script type="text/javascript">
                var editor = new baidu.editor.ui.Editor();
                editor.render("myEditor");
            </script>
        </td>
        <td></td>
    </tr>
    <tr>
        <td><input id="save" type="submit" value="保存"/></td>
        <td><a id="return" href="BasicContentList.aspx">返回</a></td>
        <td></td>
    </tr>
</table>
</form>
</body>
</html>

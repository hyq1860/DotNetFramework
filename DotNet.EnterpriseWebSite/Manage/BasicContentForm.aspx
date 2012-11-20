<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicContentForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.BasicContentForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.form.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
        <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>  
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/CommonValidation.js"></script>  

    <script type="text/javascript">
        var editor;
        $(function () {
            initCkeditor();
            //按钮设置区域
            $('#save').linkbutton();
            $("#return").linkbutton();
            InitValite();
            //表单区域

        });
        function initCkeditor() {
            editor = CKEDITOR.replace('Content', {
                filebrowserBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html")%>',
                filebrowserImageBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Images")%>',
                filebrowserFlashBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Flash")%>',
                filebrowserUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files")%>',
                filebrowserImageUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images")%>',
                filebrowserFlashUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash")%>'
            });
        }

        function InitValite() {
            //开始验证
            $('#form1').validate({
                submitHandler: function () {
                },

                /*设置验证规则 */
                rules: {
                    Title: { required: true },
                    Content: { required: true }
                },

                /*设置错误信息 */
                messages: {
                    Title: { required: "标题必填" },
                    Content: { required: "内容必填" }
                },

                /*设置验证触发事件 */
                focusInvalid: false,
                onkeyup: false,

                /*设置错误信息提示DOM */
                errorPlacement: function (error, element) {
                    error.appendTo(element.parent());
                }
            });
        }
        
        function save() {
            $('#Content').val(editor.getData());
            if($("#form1").valid()) {
                $('#form1').ajaxSubmit({
                    url: <%=(BasicContent==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertBasicContent':'<%=RootPath %>/Ajax/Common.ashx/UpdateBasicContent',
                    success: function (result) {
                        if (result) {
                            alert('保存成功');
                            window.location = "BasicContentManage.aspx";
                        } else {
                            alert("保存失败");
                        }
                    }
                });
            }
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div class="datagrid-body border_solid">
<table cellspacing="0" cellpadding="4" border="0">
    <tr>
        <td>标题：</td>
        <td>
            
             <input name="BasicContentId" type="hidden" value="<%=BasicContent!=null?BasicContent.Id:0 %>"/>
            <input type="text" name="Title" value="<% =BasicContent != null?BasicContent.Title:"" %>" style="width:400px"/>
        </td>
        <td></td>
    </tr>
<%--    <tr>
        <td>分类名称：</td>
        <td><label></label></td>
        <td></td>
    </tr>--%>
<%--    <tr>
        <td>导航链接：</td>
        <td><input type="text" name="LinkPath"/></td>
        <td></td>
    </tr>--%>
    <tr>
        <td>内容：</td>
        <td>
            <textarea cols="50" id="Content"  name="Content" rows="20"><% =BasicContent != null?BasicContent.Content:"" %></textarea>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <a href="#" id="save" onclick="save()" iconCls="icon-search">保存</a> 
            <a id="return" iconCls="icon-search" href="BasicContentManage.aspx">返回</a>
        </td>
        <td></td>
    </tr>
</table>
</div>
</form>
</body>
</html>

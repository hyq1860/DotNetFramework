<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoryEditForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductCategoryEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
     <link rel="Stylesheet" type="text/css" href="../jQueryUI/css/ui-lightness/jquery-ui-1.8.20.custom.css" />
     <script type="text/javascript" src="../Ajax/Common.ashx"></script>
     <script type="text/javascript" src="../ckfinder/ckfinder.js"></script>
     <script type="text/javascript" src="../ueditor530/editor_config.js" ></script>
<script type="text/javascript" src="../ueditor530/editor_all.js" ></script>
<link rel="stylesheet" href="../ueditor530/themes/default/ueditor.css"/>
     <script type="text/javascript">
         $(document).ready(function () {

             var options = {
                 target: '#divToUpdate',
                 url: '../Ajax/Common.ashx/UpdateProductCategoryByCategoryId',
                 success: function () {
                     alert('Thanks for your comment!');
                 }
             };
             $('#form1').ajaxForm(options);
             $("#return").button();
             $("#save").button();
             $("#cancelabc").button().click(function () {
                 //$("#editFrame", window.parent.document).parent().dialog("destory");
                 window.parent.CloseEditPage();

             });
             $.DotNet.Common.GetAllProductCategory(function (data) {

                 var space = "&nbsp&nbsp";
                 $.each(data, function (i, item) {
                     var option = "<option value ='{0}'>{1}</option>";
                     var text = "";
                     for (var i = 0; i < item.leval; i++) {
                         text += space;
                     }
                     text += item.Name;
                     var temp = option.replace("{0}", item.Id).replace("{1}", text);
                     $("#parentSelect").append(temp);
                 });
                 $("#parentSelect").val(<%=ProductCategory.Id %>).attr("disabled","disabled");
             });

         });


         function BrowseServer() {
             // You can use the "CKFinder" class to render CKFinder in a page:
             var finder = new CKFinder();
             finder.basePath = '../ckfinder/'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.selectActionFunction = SetFileField;
             finder.popup();
         }

         // This is a sample function which is called when a file is selected in CKFinder.
         function SetFileField(fileUrl) {
             document.getElementById('mediumpicture').value = fileUrl;
         }
     </script>
</head>
<body>
<form id="form1" runat="server">
<table>
    <tr>
        <td>上级分类：</td>
        <td>
            <input name="CategoryId" type="hidden" value="<%=ProductCategory.Id %>"/>
            <select id="parentSelect" name="parentSelect">
            </select>
        </td>
    </tr>
    <tr>
        <td>产品分类名称：</td>
        <td><input type="text" name="CategoryName"value="<%=ProductCategory.Name %>" /></td>
    </tr>
    <tr>
        <td>分类图片：</td>
        <td><input id="mediumpicture" name="mediumpicture" type="text" value="<%=ProductCategory.MediumPicture %>" /><input type="button" value="选择产品图片" onclick="BrowseServer();"/></td>

    </tr>
    <tr>
        <td>分类描述：</td>
        <td>
            <textarea name="desc" ><%=ProductCategory.Desc %></textarea>
        </td>
    </tr>
    <tr>
        <td><button id="save" type="submit">保存</button></td>
        <td>
            <a id="return" href="ProductCategoryList.aspx">返回</a>
        </td>
    </tr>
</table>

</form>
</body>
</html>

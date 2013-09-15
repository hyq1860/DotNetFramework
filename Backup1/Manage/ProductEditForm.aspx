<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEditForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductEditForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
     <script>

         function BrowseServer() {
             // You can use the "CKFinder" class to render CKFinder in a page:
             var finder = new CKFinder();
             finder.basePath = '../'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.selectActionFunction = SetFileField;
             finder.popup();

             // It can also be done in a single line, calling the "static"
             // popup( basePath, width, height, selectFunction ) function:
             // CKFinder.popup( '../', null, null, SetFileField ) ;
             //
             // The "popup" function can also accept an object as the only argument.
             // CKFinder.popup( { basePath : '../', selectActionFunction : SetFileField } ) ;
         }

         // This is a sample function which is called when a file is selected in CKFinder.
         function SetFileField(fileUrl) {
             document.getElementById('productpic').value = fileUrl;
         }
     </script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#save").button();
            $('#return').button();
            var options = {
                target: '#divToUpdate',
                url: '../Ajax/Common.ashx/UpdateProduct',
                success: function() {
                    alert('保存成功');
                }
            };
            $('#form1').ajaxForm(options);
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
                var temp = "";
                if(<%=Product.CategoryId %>==item.Id) {
                    temp = "<option value ='{0}' {2}>{1}</option>";
                     temp = temp.replace("{0}", item.Id).replace("{1}", text).replace("{2}",'selected="selected"');
                } else {
                    temp = option.replace("{0}", item.Id).replace("{1}", text);
                }
                
                $("#parentSelect").append($(temp));
            });
            //$("#parentSelect").attr("disabled","disabled");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>产品名称：</td>
            <td>
                <input type="text" name="title" value="<%=Product.Title %>"/>
                <input type="hidden" name="ProductId" value="<%=Product.Id %>"/>
            </td>
        </tr>
        <tr>
            <td>产品分类：</td>
            <td>
                <select id="parentSelect" name="parentSelect">
                </select>
            </td>
        </tr>
        <tr>
            <td>产品图片:</td>
            <td><input id="productpic" name="productpic" type="text" value="<%=Product.MediumPicture %>"/><input type="button" value="选择产品图片" onclick="BrowseServer();"/></td>
        </tr>
        <tr>
            <td>产品描述：</td>
            <td>
                <script type="text/plain" id="myEditor"><%=Product.Desc %></script>
                <script type="text/javascript">
                    var editor = new baidu.editor.ui.Editor();
                    editor.render("myEditor");
                </script>
            </td>
        </tr>
        <tr>
            <td><button id="save" type="submit">保存</button></td>
            <td>
                <a id="return" href="ProductList.aspx">返回</a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

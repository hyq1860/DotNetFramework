<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductForm.ascx.cs" Inherits="DotNet.EnterpriseWebSite.UserControls.ProductForm" %>



<%--<script type="text/javascript" src="UserControls/js.js" ></script>--%>
<%--http://www.cnblogs.com/sanshi/archive/2011/02/28/1967367.html--%>
<%--http://www.phpchina.com/archives/view-36864-1.html--%>
<%--http://blog.sina.com.cn/s/blog_44c781ec0100kn1l.html--%>
<%--http://pastebin.com/124Vy92q--%>
<%--http://www.cnblogs.com/webflash/archive/2010/01/04/1638621.html--%>
<link rel="stylesheet" href="ueditor530/themes/default/ueditor.css"/>


    <form id="form1">
    <table>
        <tr>
            <td>产品名称：</td><td><input type="text" name="title"/></td>
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
            <td><input id="productpic" name="productpic" type="text"/><input type="button" value="选择产品图片" onclick="BrowseServer();"/></td>
        </tr>
        <tr>
            <td>产品描述：</td>
            <td>

            <!--style给定宽度可以影响编辑器的最终宽度-->
            <div id="myEditor"></div>

            </td>
        </tr>
        <tr>
            <td><button id="save" type="submit">保存</button></td>
            <td></td>
        </tr>
    </table>

    </form>


    <script type="text/javascript">
        function BrowseServer() {
            // You can use the "CKFinder" class to render CKFinder in a page:
            var finder = new CKFinder();
            finder.basePath = '../ckfinder/'; // The path for the installation of CKFinder (default = "/ckfinder/").
            finder.selectActionFunction = SetFileField;
            finder.popup();
        }

        // This is a sample function which is called when a file is selected in CKFinder.
        function SetFileField(fileUrl) {
            document.getElementById('productpic').value = fileUrl;
        }

        


        
        $(document).ready(function () {

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

                $("#save").button();
                var options = {
                    target: '#divToUpdate',
                    url: 'Ajax/Common.ashx/InsertProduct',
                    success: function () {
                        alert('保存成功');
                    }
                };
                $('#form1').ajaxForm(options);
            });
            

            
            
        });
    </script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoryFormSnippet.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductCategoryFormSnippet" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract.Entity" %>

<form id="form1" runat="server">

    <table cellspacing="0" cellpadding="4" border="0">
    <%if (Category == null) %>
    <%
      {
          %>
    <tr>
        <td>上级分类：</td>
        <td>
            <ul id="parentSelect" name="parentSelect" style="width:200px;"  ></ul>
        </td>
    </tr>
    <% } %>
    <tr>
        <td>产品分类名称：</td>
        <td>
            <input name="CategoryId" id="CategoryId" type="hidden" value="<%=Category!=null?Category.Id:0 %>"/>
            <input type="text" name="CategoryName" id="CategoryName" style="width:400px" value="<%=Category!=null?Category.Name:"" %>"  validType="CHS" />
        </td>
    </tr>
    <tr>
        <td>分类图片：</td>
        <td><input id="mediumpicture" name="mediumpicture" readonly="readonly" type="text" style="width:400px" value="<%=Category!=null?Category.MediumPicture:"" %>"  />
        <input type="button" value="选择分类图片" onclick="BrowseServer('Images','mediumpicture');"/></td>
    </tr>
    <tr>
        <td>分类描述：</td>
        <td>
            <textarea name="desc" rows="5" cols="100"><%=Category!=null?Category.Desc:"" %></textarea>
        </td>
    </tr>
    <tr>
        <td>分类排序：</td>
        <td>
            <input name="DisplayOrder" value="<%=Category!=null?Category.DisplayOrder.ToString():"0" %>" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <a id="save" onclick="save()">保存</a>
        </td>
    </tr>
</table>

</form>
    <script type="text/javascript">
        var flag = false;

        initCategoryComboTree();
        InitValite();
        initAjaxForm();

         
        function initAjaxForm() {
            var options = {
                target: '#divToUpdate',
                url: '<%=RootPath %>/Ajax/Common.ashx/InsertProductCategory',
                success: function (result) {
                    if (result == "1") {
                        alert("新增产品分类成功");
                    }
                }
            };
            $('#form1').ajaxForm(options);
            $('#save').linkbutton();
        }

        function initCategoryComboTree() {
            $('#parentSelect').combotree({
                url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory?id=0',
                onLoadSuccess: function(node, data) {
                    if (<%=(Category!=null).ToString().ToLower() %>==true) {

                        var id = data[0].id;
                        
                        $('#parentSelect').combotree('setValue', <%=(Category!=null).ToString().ToLower()%> ? <% =(Category??new ProductCategoryInfo()).Id%> : id);
                    } 
                    else {
                        if (data[0] && data[0].id) {
                            $('#parentSelect').combotree('setValue', data[0].id);
                        }
                    }
                }
            });
        }
         

//        function BrowseServer() {
//            // You can use the "CKFinder" class to render CKFinder in a page:
//            var finder = new CKFinder();
//            finder.basePath = '../ckfinder/'; // The path for the installation of CKFinder (default = "/ckfinder/").
//            finder.selectActionFunction = SetFileField;
//            finder.popup();
//        }
        
        function BrowseServer(fileType, control) {
             var finder = new CKFinder();
             finder.basePath = '<%=RootPath %>/'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.resourceType = fileType; //Images,Files
             finder.selectActionFunction = function (fileUrl) {
                 document.getElementById(control).value = fileUrl;
             };
             finder.popup();
         }

    // This is a sample function which is called when a file is selected in CKFinder.
        function SetFileField(fileUrl) {
            document.getElementById('mediumpicture').value = fileUrl;
        }
         
        function InitValite() {
        //开始验证
        $('#form1').validate({
            submitHandler: function () {
            },

            /*设置验证规则 */
            rules: {
                CategoryName: {required: true},
                mediumpicture: {required: true},
                desc: {required: true}
            },

            /*设置错误信息 */
            messages: {
                CategoryName: {required: "分类名称必填"},
                mediumpicture: {required: "分类图片必填"},
                desc: { required: "分类简介必填"}
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
            if($("#form1").valid()) {
                $('#form1').ajaxSubmit({
                url: <%=(Category==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertProductCategory':'<%=RootPath %>/Ajax/Common.ashx/UpdateProductCategoryByCategoryId',
                success: function (result) {
                    if (result) {
                        alert('保存成功');
                        //window.location = "ProductCategoryManage.aspx";
                        $('#win').window('close');
                           // $('#win').window('refresh');
                        $('#ProductCategoryGrid').treegrid("reload");
                    } else {
                        alert("保存失败");
                    }
                }
            });
            }
        }
    </script>

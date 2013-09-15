<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductForm" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract.Entity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css" />
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
         function BrowseServer(fileType,control) {
             var finder = new CKFinder();
             finder.basePath = '<%=RootPath %>/'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.resourceType = fileType; //Images,Files
             finder.selectActionFunction = function (fileUrl) {
                 document.getElementById(control).value = fileUrl;
             };
             finder.popup();
         }
     </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#save").linkbutton();
            $("#return").linkbutton();
            $("#selectPic").linkbutton();
            $("#selectFile").linkbutton();
            
            initProductCategory();
            InitValiteAndAjaxForm();
            initCkeditor();
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

        function initProductCategory() {
            $('#productCategory').combotree({
            //checkbox: true,
                url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory',
                onSelect: function(node, data) {
                    //返回树对象  
                    var tree = $(this).tree;
                    //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
                    var isLeaf = tree('isLeaf', node.target);
                    if (!isLeaf) {
                        //清除选中  
                        $('#productCategory').combotree('clear');
                    }
                },
                onLoadSuccess: function(node, data) {
                    if (<%=(Product!=null).ToString().ToLower() %>==true) {
                        var id = data[0].id;
                        $('#productCategory').combotree('setValue', <%=(Product!=null).ToString().ToLower()%> ? <% =(Product??new ProductInfo()).CategoryId%> : id);
                    } 
                    else {
                        if (data[0] && data[0].id) {
                            $('#productCategory').combotree('setValue', data[0].id);
                        }
                    }
                }
            });
        }

        function save() {
            $('#Content').val(editor.getData());
            if ($("#form1").valid()&&$('#productCategory').combotree("getValue")) {
                $('#form1').ajaxSubmit({
                    url: <%=(Product==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertProduct':'<%=RootPath %>/Ajax/Common.ashx/UpdateProduct',
                    success: function (result) {
                        if (result) {
                            alert('保存成功');
                            window.location = "ProductManage.aspx";
                        } else {
                            alert("保存失败");
                        }
                    }
                });
            }
        }
        function InitValiteAndAjaxForm() {
            //开始验证
            $('#form1').validate({
                submitHandler: function () {
//                    var options = {
//                        url: '<%=RootPath %>/Ajax/Common.ashx/InsertProduct',
//                        success: function (result) {
//                            if (result) {
//                                alert('保存成功');
//                                window.location = "ProductManage.aspx";
//                            } else {
//                                alert("保存失败");
//                            }
//                        }
//                    };
//                    $('#form1').ajaxSubmit(options);
                },

                /*设置验证规则 */
                rules: {
                    title: {required: true},
                    mediumpicture: {required: true},
                    Content: {required: true},
                    filedown:{required: true},
                    productCategory:{required: true}
                },

                /*设置错误信息 */
                messages: {
                    title: {required: "产品名称必填"},
                    mediumpicture: {required: "产品图片必填"},
                    Content: { required: "产品简介必填"},
                    filedown: { required: "产品资料必填" },
                    productCategory:{required: "产品分类必填"}
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
    </script>
     <style type="text/css">
        body{ padding:4px;margin: 0;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="datagrid-body border_solid">
    <table cellspacing="0" cellpadding="4" border="0">
        <tr>
            <td width="80">产品名称：</td>
            <td>
                <input name="ProductId" type="hidden" value="<%=Product!=null?Product.Id:0 %>"/>
                <input type="text" value="<%=Product!=null?Product.Title:string.Empty %>" name="title" style="width:400px;" />
            </td>
        </tr>
        <tr>
            <td>产品分类：</td>
            <td>
                <%--<select id="parentSelect" name="parentSelect">
                </select>--%>
                <ul id="productCategory" name="productCategory" style="width:200px;"></ul>
            </td>
        </tr>
        <tr>
            <td>产品图片:</td>
            <td><input id="mediumpicture" name="mediumpicture" style="width:400px;" type="text" value="<%=Product!=null?Product.MediumPicture:string.Empty %>"/>
            <a type="button" id="selectPic" onclick="BrowseServer('Images','mediumpicture');">选择产品图片</a></td>
        </tr>
        <tr>
            <td>产品资料:</td>
            <td><input id="filedown" name="filedown" style="width:400px;" type="text" value="<%=Product!=null?Product.Title:string.Empty %>"/>
            <a type="button" id="selectFile" onclick="BrowseServer('Files','filedown');">选择产品资料</a></td>
        </tr>
        <tr>
            <td>产品简介：</td>
            <td>
                <textarea name="Desc" rows="5" cols="100"><%=Product!=null?Product.Desc:""%></textarea>
            </td>
        </tr>
        <tr>
            <td>产品描述：</td>
            <td>
                <textarea cols="50" id="Content"  name="Content" rows="5"><%=Product!=null?Product.Content:""%></textarea>
            </td>
        </tr>
        <tr>
            <td></td> 
            <td>
                <a id="save" onclick="save()">保存</a>
                <a id="return" href="ProductManage.aspx">返回</a>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

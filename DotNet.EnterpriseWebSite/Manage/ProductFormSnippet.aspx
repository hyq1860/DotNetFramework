<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductFormSnippet.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductFormSnippet" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract.Entity" %>
<%--http://www.easyui.info/archives/164.html--%>
<%--http://www.cnblogs.com/cocowool/archive/2010/09/04/1817989.html--%>
<%--http://stackoverflow.com/questions/1794219/ckeditor-instance-already-exists--%>
<%--http://www.cnblogs.com/jishu/archive/2011/07/28/2119200.html--%>
<%--http://www.cnblogs.com/zhangxiaolin/archive/2012/05/23/jqueryeasyui.html--%>
     <script type="text/javascript">
         
         function BrowseServer(fileType, control) {
             var finder = new CKFinder();
             finder.basePath = '<%=RootPath %>/'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.resourceType = fileType; //Images,Files
             finder.selectActionFunction = function (fileUrl) {
                 document.getElementById(control).value = fileUrl;
             };
             finder.popup();
         }

        var editor;
        function initCkeditor() {

            if( CKEDITOR.instances['Content'] ){
                CKEDITOR.remove(CKEDITOR.instances['Content']);
            }

            editor = CKEDITOR.replace('Content', {
                height: 400,
                width:706,
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
                url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory?id=1',
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
                        //alert(<%=(Product!=null).ToString().ToLower() %>);
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
            if ($('#productCategory').combotree("getValue")) {
//                if(<%=(Product==null).ToString().ToLower()%>) {
//                    $.DotNet.Common.InsertProduct(function (result) {
//                        if(result) {
//                            alert('保存成功');
//                            $('#win').window('close');
//                            $('#prGrid').datagrid("reload");
//                        } else {
//                            alert("保存失败");
//                        }
//                    })
//                } else {
//                    $.DotNet.Common.UpdateProduct(function (result) {
//                        if(result) {
//                            alert('保存成功');
//                            $('#win').window('close');
//                            $('#prGrid').datagrid("reload");
//                        } else {
//                            alert("保存失败");
//                        }
//                    })
//                }
                $('#ProductForm').ajaxSubmit({
                    type:'post',
                    resetForm:true,
                    url: <%=(Product==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertProduct':'<%=RootPath %>/Ajax/Common.ashx/UpdateProduct',
                    success: function (result) {
                        if (result) {
                            alert('保存成功');
                            $('#win').window('close');
                           // $('#win').window('refresh');
                            $('#prGrid').datagrid("reload");
                            //$('#form1').clearForm();
                        } else {
                            alert("保存失败");
                        }
                    }
                });
            }
        }
        
        function InitValiteAndAjaxForm() {
            //开始验证
            $('#ProductForm').validate({
                submitHandler: function () {
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

        initProductCategory();
        initCkeditor();
        //InitValiteAndAjaxForm();
    </script>

    <form id="ProductForm" method="post">
    <div class="datagrid-body border_solid">
    <table cellspacing="0" cellpadding="4" border="0">
        <tr>
            <td width="70">产品名称：</td>
            <td>
                <input name="ProductId" type="hidden" value="<%=Product!=null?Product.Id:0 %>"/>
                <input type="text" value="<%=Product!=null?Product.Title:string.Empty %>" name="title" style="width:400px;" class="easyui-validatebox" required="true" />
            </td>
        </tr>
        <tr>
            <td>产品分类：</td>
            <td>
                <ul id="productCategory" name="productCategory" style="width:200px;" class="easyui-validatebox" required="true"></ul>
            </td>
        </tr>
        <tr>
            <td>产品图片:</td>
            <td><input id="mediumpicture" class="easyui-validatebox" required="true" name="mediumpicture" style="width:400px;" type="text" readonly="readonly" value="<%=Product!=null?Product.MediumPicture:string.Empty %>"/>
            <a type="button" class="easyui-linkbutton" id="selectPic" onclick="BrowseServer('Images','mediumpicture');">选择产品图片</a></td>
        </tr>
        <tr>
            <td>产品资料:</td>
            <td><input id="filedown" class="easyui-validatebox" required="true"  name="filedown" style="width:400px;" type="text" readonly="readonly" value="<%=Product!=null?Product.FileUrl:string.Empty %>"/>
            <a type="button" class="easyui-linkbutton" id="selectFile" onclick="BrowseServer('Files','filedown');">选择产品资料</a></td>
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
                <textarea cols="100" id="Content"  name="Content" rows="1"><%=Product!=null?Product.Content:""%></textarea>
            </td>
        </tr>
        <tr>
            <td>seo关键字：</td>
            <td>
                <input id="Keywords"  name="Keywords" value='<%=Product!=null?Product.Keywords:""%>' style="width:400px;" />
            </td>
        </tr>
        <tr>
            <td>seo描述：</td>
            <td>
                <input id="Description"  name="Description" value='<%=Product!=null?Product.MetaDesc:""%>' style="width:400px;" />
            </td>
        </tr>
        <tr>
            <td></td> 
            <td>
                <a id="save" class="easyui-linkbutton" iconCls="icon-save" onclick="save()">保存</a>
            </td>
        </tr>
    </table>
    </div>
    </form>



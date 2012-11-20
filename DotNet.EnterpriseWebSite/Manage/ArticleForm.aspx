<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ArticleForm" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>

<form id="ArticleForm">

<input id="ArticleId" name="ArticleId" type="hidden" value="<%=Article==null?0:Article.Id %>" />

<table>

	<tr>
        <td align="right">标题：</td>
        <td>
            <input id="Title" name="Title" value="<%=Article==null?"":Article.Title %>" style="width: 350px;"/>
        </td>
        <td>
            副标题：
        </td>
        <td>
            <input name="SubTitle" style="width: 220px;" value="<%=Article==null?string.Empty:Article.SubTitle %>" />
        </td>
        
    </tr>
    <tr>
        <td align="right">栏目：</td>
        <td>
            <ul style="width: 357px;" id="ArticleCategory" name="ArticleCategory"   ></ul>
            <input id="ArticleCategoryId" name="ArticleCategoryId" type="hidden"  />
        </td>
        <td><label>SEO关键字：</label></td>
        <td><input name="Keywords" style="width: 220px;" value="<%=Article==null?string.Empty:Article.Keywords %>"/></td>
    </tr>
	<tr>
        <td align="right">摘要：</td>
        <td>
        <textarea style="width: 350px;" id="Summary"  name="Summary" rows="2" ><%=Article==null?string.Empty:Article.Summary %></textarea>
        </td>
        <td>SEO描述信息：</td>
        <td><textarea  name="MetaDesc" style="width: 220px;"><%=Article==null?string.Empty:Article.MetaDesc %></textarea></td>
    </tr>
    <tr>
        <td align="right">缩微图：</td>
        <td colspan="3">
            <input id="FocusPicture" name="FocusPicture" value="<%=Article==null?string.Empty:Article.FocusPicture %>"  style="width: 350px;"/>
            <a type="button" class="easyui-linkbutton" id="selectPic" onclick="BrowseServer('Images','FocusPicture');">选择缩微图</a>
        </td>
    </tr>
	<tr>
        <td align="right"><label>内容：</label></td>
        <td colspan="3"><textarea style="width: 100%" name="Content" id="Content" ><%=Article==null?string.Empty:Article.Content %></textarea></td>
        
    </tr>
	<tr style="display: none;">
        <td align="right">来源：</td>
        <td><input name="Source" /></td>
        <td></td>
        <td></td>
    </tr>
	<tr style="display: none;">
        <td align="right">是否允许评论：</td>
        <td><input name="AllowComments" /></td>
        <td></td>
        <td></td>
    </tr>
	<tr style="display: none;">
        <td align="right">点击数：</td>
        <td><input id="Clicks" name="Clicks" /></td>
        <td></td>
        <td></td>
    </tr>
	<tr style="display: none;">
        <td align="right">阅读密码：</td>
        <td><input name="ReadPassword" /></td>
        <td></td>
        <td></td>
    </tr>
	<tr style="display: none;">
        <td align="right">发布时间：</td>
        <td><input name="PublishDate" id="PublishDate"  value="<%=DateTime.Now %>"/></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td align="right"></td>
        <td><a href="#" id="save"  class="easyui-linkbutton" iconCls="icon-save" onclick="SaveArticle()" iconCls="icon-search">保存</a> </td>
        <td></td>
        <td></td>
    </tr>
</table>
</form>

<script type="text/javascript">
    initValidate();
    initCkeditor();
    initArticleCategoryComboTree();
    
     function BrowseServer(fileType, control) {
             var finder = new CKFinder();
             finder.basePath = '<%=RootPath %>/'; // The path for the installation of CKFinder (default = "/ckfinder/").
             finder.resourceType = fileType; //Images,Files
             finder.selectActionFunction = function (fileUrl) {
                 document.getElementById(control).value = fileUrl;
             };
             finder.popup();
         }

    //initArticleForm();
    //初始化内容框
    function initCkeditor() {
        if (CKEDITOR.instances['Content']) {
            CKEDITOR.remove(CKEDITOR.instances['Content']);
        }
        editor = CKEDITOR.replace('Content', {
            height: 350,
            filebrowserBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html")%>',
            filebrowserImageBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Images")%>',
            filebrowserFlashBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Flash")%>',
            filebrowserUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files")%>',
            filebrowserImageUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images")%>',
            filebrowserFlashUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash")%>'
        });
    }
    
    function initArticleCategoryComboTree() {
        $('#ArticleCategory').combotree({
            url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeArticleCategory?id=1',
            onLoadSuccess: function(node, data) {
                if (<%=(Article!=null).ToString().ToLower() %>) {
                    $('#ArticleCategory').combotree('setValue', <%=(Article!=null).ToString().ToLower()%> ? <% =(Article??new ArticleInfo()).CategoryId%> : data[0].id);
                    $("#ArticleCategoryId").val(<%=(Article!=null).ToString().ToLower()%> ? <% =(Article??new ArticleInfo()).CategoryId%> : data[0].id);
                    $('#ArticleCategory').combotree('disable');
                } else {
                    if (data[0] && data[0].id) {
                        //$("#ArticleCategoryId").val(data[0].id);
                        $('#ArticleCategory').combotree('setValue', data[0].id);
                    }
                }
            }
        });
    }

    function initValidate() {
        $('#Title').validatebox({
            required: true
        });
        $('#Clicks').numberspinner({
            editable: false,
            min:1,
            value:1
        });
        $('#PublishDate').datetimebox({
            showSeconds: false
        });
    }
    
    function initArticleForm() {
        $('#ArticleForm').form({
           type:"post",
           url: <%=(Article==null).ToString().ToLower()%> ? '<%=RootPath %>/Ajax/Common.ashx/InsertArticle' : '<%=RootPath %>/Ajax/Common.ashx/UpdateArticle',
           onSubmit:function () {
               //alert($("#ArticleForm").form('validate'));
               return $("#ArticleForm").form('validate');  
           },
           success: function(result) {
               if (result > 0) {
                   alert('保存成功');
                   //window.location = "ProductCategoryManage.aspx";
                   $('#win').window('close');
                   // $('#win').window('refresh');
                   $('#ArticleList').datagrid("reload");
               } else {
                   alert("保存失败");
               }
           }
       });
    }
   
   function SaveArticle() {
       $('#Content').val(editor.getData());
       //$('#ArticleForm').submit();  
       $('#ArticleForm').ajaxSubmit({
           type:"post",
           url: <%=(Article==null).ToString().ToLower()%> ? '<%=RootPath %>/Ajax/Common.ashx/InsertArticle' : '<%=RootPath %>/Ajax/Common.ashx/UpdateArticle',
           beforeSubmit:function () {
               //alert($("#ArticleForm").form('validate'));
               return $("#ArticleForm").form('validate');  
           },
           success: function(result) {
               if (result > 0) {
                   alert('保存成功');
                   $('#win').window('close');
                   $('#ArticleList').datagrid("reload");
               } else {
                   alert("保存失败");
               }
           }
       });
   }
</script>
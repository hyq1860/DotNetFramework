<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleCategoryForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ArticleCategoryForm" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract.Entity" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/windowControl.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/gciframe.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.panel.extension.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/treegrid.loadFilter.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/Scripts/common.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <link href="<%=RootPath %>/Styles/default.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.form.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.min.js"></script> 
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>  
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/CommonValidation.js"></script>
    

</head>
<body>--%>
<form id="ArticleCategoryForm">
<input type="hidden" name="ArticleCategoryId" value="<%=ArticleCategory==null?0:ArticleCategory.Id %>"/>
<table>
    <%if (ArticleCategory == null) %>
    <%{%>
    <tr>
        <td>上级栏目：</td>
        <td>
            <%--<input id="ParentArticleCategory" name="ParentArticleCategory" > --%>
            <ul id="ParentArticleCategory" name="ParentArticleCategory" style="width:200px;"   ></ul>
        </td>
    </tr>
    <% }%>
    <tr>
        <td>栏目名称：</td>
        <td><input name="ArticleCategoryTitle" id="ArticleCategoryTitle" style="width:200px;" value="<%=ArticleCategory==null?string.Empty:ArticleCategory.Title %>" /></td>
    </tr>
    <tr>
        <td>栏目类型：</td>
        <td>
            <select id="ArticleCategoryType" name="ArticleCategoryType" style="width:80px;"></select>
        </td>
    </tr>
    <tr>
        <td>SEO关键字：</td>
        <td><textarea name="ArticleCategoryKeywords" id="ArticleCategoryKeywords" rows="2" cols="80"><%=ArticleCategory==null?string.Empty:ArticleCategory.Keywords %></textarea></td>
    </tr>
    <tr>
        <td>SEO描述：</td>
        <td><textarea name="ArticleCategoryMetaDesc" id="ArticleCategoryMetaDesc" rows="2" cols="80"><%=ArticleCategory==null?string.Empty:ArticleCategory.MetaDesc %></textarea></td>
    </tr>
    <tr>
        <td>备注：</td>
        <td><textarea name="ArticleCategoryDesc" id="ArticleCategoryDesc" rows="4" cols="80"><%=ArticleCategory==null?string.Empty:ArticleCategory.Description %></textarea></td>
    </tr>
    <tr>
        <td></td>
        <td><a id="save" class="easyui-linkbutton" iconCls="icon-save" onclick="Save()">保存</a></td>
    </tr>
</table>

</form>
<%--</body>

</html>--%>
    <script type="text/javascript">
        initValidate();
//        $(function() {

            initCategoryComboTree();
            initArticleCategoryType();
//            $('#ArticleCategoryForm').form({
//                url: <%=(ArticleCategory==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertArticleCategory':'<%=RootPath %>/Ajax/Common.ashx/UpdateArticleCategoryByCategoryId',
//                onSubmit: function() {
//                    // do some check  
//                    // return false to prevent submit;  
//                },
//                success: function(data) {
//                    if(data>1) {
//                        alert("保存成功");
//                        $('#win').window('close');
//                           // $('#win').window('refresh');
//                        $('#ArticleCategoryGrid').treegrid("reload");
//                    } else {
//                        
//                    }
//                }
//            });

//        });
            
                    //初始化分类
        function initCategoryComboTree() {
            $('#ParentArticleCategory').combotree({
                url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeArticleCategory?id=0',
                onLoadSuccess: function(node, data) {
                    if (<%=(ArticleCategory!=null).ToString().ToLower() %>) {

                        var id = data[0].id;
                        
                        $('#ParentArticleCategory').combotree('setValue', <%=(ArticleCategory!=null).ToString().ToLower()%> ? <% =(ArticleCategory??new ArticleCategoryInfo()).Id%> : id);
                    } 
                    else {
                        if (data[0] && data[0].id) {
                            $('#ParentArticleCategory').combotree('setValue', data[0].id);
                        }
                    }
                }
            });
        }
        
        // 初始化栏目类型
        function initArticleCategoryType() {
            //GetCommonList
            $('#ArticleCategoryType').combobox({
                url: '<%=RootPath %>/Ajax/Common.ashx/GetCommonList?group=ArticleCategoryType&defaultValue=<%=ArticleCategory==null?0:ArticleCategory.CategoryType %>',
                valueField: 'Value',
                textField: 'Text',
                editable:false,
                onLoadSuccess:function (data) {
                    //alert(data[0][0]);
                }
            });
        }
        
        function initValidate() {
            $('#ArticleCategoryTitle').validatebox({
                required: true
            });
            $('#ArticleCategoryKeywords').validatebox({
                required: true
            });
            
        }
        
        function Save() {
            //$('#ArticleCategoryForm').submit();
//            $('#ArticleCategoryForm').form('submit', {
//                url: <%=(ArticleCategory==null).ToString().ToLower()%> ?'<%=RootPath %>/Ajax/Common.ashx/InsertArticleCategory':'<%=RootPath %>/Ajax/Common.ashx/UpdateArticleCategoryByCategoryId',
//                onSubmit: function() {
//                    // do some check  
//                    // return false to prevent submit;  
//                },
//                success: function(data) {
//                    if(data>1) {
//                        alert("保存成功");
//                        $('#win').window('close');
//                           // $('#win').window('refresh');
//                        $('#ArticleCategoryGrid').treegrid("reload");
//                    } else {
//                        
//                    }
//                }
//            });

            $('#ArticleCategoryForm').ajaxSubmit({
                url: <%=(ArticleCategory==null).ToString().ToLower()%> ? '<%=RootPath %>/Ajax/Common.ashx/InsertArticleCategory' : '<%=RootPath %>/Ajax/Common.ashx/UpdateArticleCategoryByCategoryId',
                beforeSubmit: function() {
                    //alert($(this).form('validate'));
                    //此处要注意 因为没有使用easyui的form 必须要用#from 不能用this
                    return $("#ArticleCategoryForm").form('validate');
                },
                success: function(result) {
                    //alert(result);
                    if (result == "1") {
                        //$('#ArticleCategoryTitle').validatebox('remove');
                        alert('保存成功');
                        //window.location = "ProductCategoryManage.aspx";
                        $('#win').window('close');
                        // $('#win').window('refresh');
                        $('#ArticleCategoryGrid').treegrid("reload");
                    } else {
                        alert("保存失败");
                    }
                }
            });
        }
    </script>

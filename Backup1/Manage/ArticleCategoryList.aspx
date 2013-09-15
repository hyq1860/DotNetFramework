<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleCategoryList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ArticleCategoryList" %>

<table id="ArticleCategoryGrid"></table>

<script type="text/javascript">
    initGrid();
    function modifyArticleCategory(id) {
        $('#win').window({
            title: "修改栏目",
            width: 800,
            height: 600,
            modal: true,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            cache: false,
            href: "ArticleCategoryForm.aspx?id=" + id
        });
    }

    function DeleteArticleCategoryById(id) {
        $.DotNet.Common.DeleteArticleCategoryById(id, function (result) {
            if (result > 0) {
                alert("删除成功");
                $('#ArticleCategoryGrid').treegrid("reload");
            }
        });
    }

    function initGrid() {
        $('#ArticleCategoryGrid').treegrid({
            title: '栏目管理',
            iconCls: 'icon-save',
            //width: 700,
            //height: 350,
            nowrap: false,
            rownumbers: true,
            animate: true,
            collapsible: false,
            url: '<%=RootPath %>/Ajax/Common.ashx/GetAllArticleCategory',
            idField: 'Id',
            treeField: 'Name',
            columns: [[
                { field: 'Name', title: '栏目名称', width: 220 },
                { field: 'TypeName', title: '栏目类型', width: 60, align: 'center' },
                { field: 'Description', title: '备注', width: 220 },
                {
                    field: 'DataStatus',
                    title: '状态',
                    width: 220,
                    formatter: function (value, rowData) {
                        return rowData.DataStatus == 1 ? "可用" : "禁用";
                    }
                },
                {
                    field: 'Title',
                    title: '操作',
                    width: 320,
                    align: 'center',
                    formatter: function (value, rowData) {
                        var del = "";
                        //console.log(value);
                        del = "<a href='#' onclick=\"DeleteArticleCategoryById('" + rowData.Id + "');\" >删除</a>  ";
                        //var up = "  <a href='#' onclick=\"Up('" + rowData.Id + "');\" >上移</a>  ";
                        //var down = "<a href='#' onclick=\"Down('" + rowData.Id + "');\" >下移</a> ";
                        var modify = "<a href='#' onclick=\"modifyArticleCategory('" + rowData.Id + "');\" >修改</a>  ";
                        var f = rowData.DataStatus == 0 ? '<a href="#" onclick="EnableDataStatus(' + rowData.Id + ')">取消逻辑删除</a>' : '<a href="#" onclick="DisableDataStatus(' + rowData.Id + ')">逻辑删除</a>';
                        return modify + del + f;// +up + down;

                    }
                }
            ]],
            toolbar: [{
                text: '新增',
                iconCls: 'icon-add',
                handler: function () {
                    $('#win').window({
                        title: "新增栏目",
                        width: 800,
                        height: 600,
                        cache: false,
                        modal: true,
                        collapsible: false,
                        minimizable: false,
                        maximizable: false,
                        href: "ArticleCategoryForm.aspx"
                    });
                }
            }]
        });
    }

    function EnableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("ArticleCategory", "DataStatus", 1, "Id", id, function (result) {
            if (result == 1) {
                alert("取消成功");
                $('#ArticleCategoryGrid').treegrid("reload");
            }
        });
    }

    function DisableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("ArticleCategory", "DataStatus", 0, "Id", id, function (result) {
            if (result == 1) {
                alert("取消成功");
                $('#ArticleCategoryGrid').treegrid("reload");
            }
        });
    }
    
    function Up(id) {
        $.DotNet.Common.CommonUp("ArticleCategory", "Id", id, "DisplayOrder", function (result) {
            if (result == 2) {
                alert("上移成功");
                $('#ArticleCategoryGrid').treegrid("reload");
            }
        });
    }

    function  Down(id) {
        $.DotNet.Common.CommonDown("ArticleCategory", "Id", id, "DisplayOrder", function (result) {
            if (result == 2) {
                alert("下移成功");
                $('#ArticleCategoryGrid').treegrid("reload");
            }
        });
    }
</script>
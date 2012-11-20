<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ArticleList" %>

<table id="ArticleList"></table>

<script type="text/javascript">
    var ArticleData;
    initGrid();
    function initGrid() {
        $('#ArticleList').datagrid({
            queryParams: { page: 1, rows: 2, sort: 'DisplayOrder', order: 'desc' },
            title: '文章列表',
            iconCls: 'icon-save',
            method: "get",
            striped: true, //是否隔行换色
            url: '<%=RootPath %>/Ajax/Common.ashx/GetArticles',
            idField: 'Id',
            pagination: true,
            rownumbers: false,
            singleSelect: true,
            pageSize: 20,
            pageList: [20, 40],
            loadMsg: '正在加载...',
            autoRowHeight: false,
            //border:false,
            //fit: true,
            //fitColumns: true,
            //nowarp:false,
            columns: [[
	                { field: 'Id', title: '编号', width: 40 },
	                { field: 'Title', title: '文章标题', width: 400 },
	                { field: 'CategoryName', title: '文章栏目', width: 250 },
	                { field: 'InDate', title: '发布时间', width: 125 },
                    { field: 'DisplayOrder', title: 'DisplayOrder', width: 125 },
                    { field: 'Operate', title: '操作', width: 200, align: "center",
                        formatter: function (value, rowData, rowIndex) {
                            var f = rowData.DataStatus == 1 ? '<a href="#" onclick="DisableDataStatus(' + rowData.Id + ')">禁用</a>' : '<a href="#" onclick="EnableDataStatus(' + rowData.Id + ')">启用</a>';
                            var s = '<a href="#" onclick=modifyArticle(' + rowData.Id + ')>修改</a> ';
                            var d = '<a href="#" onclick="deleteArticle(' + rowData.Id + ')">删除</a> ';
                            var up;
                            var down;
                            if (rowIndex == 0) {
                                ArticleData = $("#ArticleList").datagrid("getData").rows;
                                down = "<a href='#' onclick=\"Down(" + rowData.Id + "," + ArticleData[rowIndex + 1].Id + "," + rowData.DisplayOrder + "," + ArticleData[rowIndex + 1].DisplayOrder + ");\" >下移</a> ";
                                return s + d +f+ down;
                            } else if (rowIndex == ArticleData.length - 1) {
                                up = "  <a href='#' onclick=\"Up(" + rowData.Id + "," + ArticleData[rowIndex - 1].Id + "," + rowData.DisplayOrder + "," + ArticleData[rowIndex - 1].DisplayOrder + ");\" >上移</a>  ";
                                return s + d+f + up;
                            }
                            else {
                                down = "<a href='#' onclick=\"Down(" + rowData.Id + "," + ArticleData[rowIndex + 1].Id + "," + rowData.DisplayOrder + "," + ArticleData[rowIndex + 1].DisplayOrder + ");\" >下移</a> ";
                                up = "  <a href='#' onclick=\"Up(" + rowData.Id + "," + ArticleData[rowIndex - 1].Id + "," + rowData.DisplayOrder + "," + ArticleData[rowIndex - 1].DisplayOrder + ");\" >上移</a>  ";
                                return s + d +f+ up + down;
                            }
                            //console.log("我先formatter");
                        }
                    }
	            ]],
            toolbar: [{
                text: '新增',
                iconCls: 'icon-add',
                handler: function () {
                    $('#win').window({
                        title: "新增文章",
                        width: 800,
                        height: 600,
                        cache: false,
                        modal: true,
                        collapsible: false,
                        minimizable: false,
                        maximizable: false,
                        href: "ArticleForm.aspx"
                    });
                }
            }],
            onLoadSuccess: function (data) {
                //console.log("我先onLoadSuccess");
            }
        });
    }

    function modifyArticle(id) {
        $('#win').window({
            title: "修改文章",
            width: 800,
            height: 600,
            modal: true,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            cache: false,
            href: "ArticleForm.aspx?id=" + id
        });
    }

    function deleteArticle(id) {
        $.DotNet.Common.DeleteArticleById(id, function (result) {
            if (result != 0) {
                alert("删除成功");
                $('#ArticleList').datagrid("reload");
            }
        });
    }

    function Up(p1,p2,o1,o2) {
        $.DotNet.Common.MoveUp("Article", "Id", "DisplayOrder", p1, p2, o1, o2, function (result) {
            if (result == 2) {
                alert("上移成功");
                $('#ArticleList').datagrid("reload");
            }
        });
    }

    function Down(p1, p2, o1, o2) {
        $.DotNet.Common.MoveDown("Article", "Id", "DisplayOrder",p1, p2, o1, o2, function (result) {
            if (result == 2) { 
                alert("下移成功");
                $('#ArticleList').datagrid("reload");
            }
        });
    }

    function EnableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("Article", "DataStatus", 1, "Id", id, function (result) {
            if (result == 1) {
                alert("启用成功");
                $('#ArticleList').datagrid("reload");
            }
        });
    }
    function DisableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("Article", "DataStatus", 0, "Id", id, function (result) {
            if (result == 1) {
                alert("禁用成功");
                $('#ArticleList').datagrid("reload");
            }
        });
    }
</script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoryList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductCategoryList" %>


    
<table id="ProductCategoryGrid"></table>

<script type="text/javascript">
    initGrid();
    function modifyProductCategory(id) {
        $('#win').window({
            title: "修改产品",
            width: 800,
            height: 600,
            modal: true,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            cache: false,
            href: "ProductCategoryFormSnippet.aspx?id=" + id
        });
    }
    
    function DeleteProductCategoryById(id) {
        $.DotNet.Common.DeleteProductCategoryById(id, function(result) {
            if (result > 0) {
                alert("删除成功");
                $('#ProductCategoryGrid').treegrid("reload");
            } else {
                alert("删除失败");
            }
        });
    }

    function DeleteProductCategoryOnlyById(id) {
        $.DotNet.Common.DeleteProductCategoryOnlyById(id, function (result) {
            if (result > 0) {
                alert("删除成功");
                $('#ProductCategoryGrid').treegrid("reload");
            } else {
                alert("删除失败");
            }
        });
        
    }

    function initGrid() {
        $('#ProductCategoryGrid').treegrid({
            title: '产品分类管理',
            iconCls: 'icon-save',
            //width: 700,
            //height: 350,
            nowrap: false,
            rownumbers: true,
            animate: true,
            collapsible: false,
            url: '<%=RootPath %>/Ajax/Common.ashx/GetAllProductCategory',
            idField: 'Id',
            treeField: 'Name',
            columns: [[
				{ field: 'Name', title: '分类名称', width: 350 },
                { field: 'Desc', title: '描述', width: 400 },
                { field: 'opt', title: '操作', width: 200, align: 'center',
                    formatter: function (value, rowData, rowIndex) {
                        var del = "";

                        del = "<a href='#' onclick=\"DeleteProductCategoryById('" + rowData.Id + "');\" >删除</a>  ";

                        var modify = "<a href='#' onclick=\"modifyProductCategory('" + rowData.Id + "');\" >修改</a>  ";
                        //var delcu = "<a href='#' onclick=\"DeleteProductCategoryOnlyById('" + rowData.Id + "');\" >仅删除当前节点</a>  ";
                        var f = rowData.DataStatus == 0 ? '<a href="#" onclick="EnableDataStatus(' + rowData.Id + ')">取消逻辑删除</a>' : '<a href="#" onclick="DisableDataStatus(' + rowData.Id + ')">逻辑删除</a>';
                        var ud = "";
                        return modify + del + f+ud;

                    }
                }
			]],
            toolbar: [{
                text: '新增',
                iconCls: 'icon-add',
                handler: function () {
                    $('#win').window({
                        title: "新增产品分类",
                        width: 800,
                        height: 600,
                        cache: false,
                        modal: true,
                        collapsible: false,
                        minimizable: false,
                        maximizable: false,
                        href: "ProductCategoryFormSnippet.aspx"
                    });
                }
            }]
        });
    }
    
    function EnableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("ProductCategory", "DataStatus", 1, "Id", id, function (result) {
            if (result == 1) {
                alert("取消成功");
                $('#ProductCategoryGrid').treegrid("reload");
            }
        });
    }

    function DisableDataStatus(id) {
        $.DotNet.Common.CommonUpdateInt("ProductCategory", "DataStatus", 0, "Id", id, function (result) {
            if (result == 1) {
                alert("取消成功");
                $('#ProductCategoryGrid').treegrid("reload");
            }
        });
    }
</script>

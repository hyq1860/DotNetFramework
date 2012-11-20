<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HtmlSnippet.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.HtmlSnippet" %>




    <script type="text/javascript">
        var ProductData;
        initGrid();
        function initGrid() {
            $('#prGrid').datagrid({
                queryParams: { page: 1, rows: 2, sort: 'Id', order: 'desc' },
                title: '产品列表',
                iconCls: 'icon-save',
                method: "get",
                striped: true, //是否隔行换色
                url: '<%=RootPath %>/Ajax/Common.ashx/GetProducts',
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
	                    { field: 'Title', title: '产品名称', width: 400 },
	                    { field: 'CategoryName', title: '分类名称', width: 250 },
	                    { field: 'InDate', title: '发布时间', width: 130 },
                        { field: 'Operate', title: '操作', width: 200, align: "center",
                            formatter: function (value, rowData, rowIndex) {
                                var f = rowData.IsFocusPicture == 1 ? '<a href="#" onclick="cancelFocus(' + rowData.Id + ')">取消焦点图</a>' : '<a href="#" onclick="setFocus(' + rowData.Id + ')">设为焦点图</a>';
                                var s = '<a href="#" onclick=modifyProduct(' + rowData.Id + ')>修改</a> ';
                                var d = '<a href="#" onclick="deleteProduct(' + rowData.Id + ')">删除</a> ';
                                
                                var up;
                                var down;
                                if (rowIndex == 0) {
                                    ProductData = $("#prGrid").datagrid("getData").rows;
                                    down = " <a href='#' onclick=\"Down(" + rowData.Id + "," + ProductData[rowIndex + 1].Id + "," + rowData.DisplayOrder + "," + ProductData[rowIndex + 1].DisplayOrder + ");\" >下移</a> ";
                                    return s + d+f + down;
                                } else if (rowIndex == ProductData.length - 1) {
                                    up = " <a href='#' onclick=\"Up(" + rowData.Id + "," + ProductData[rowIndex - 1].Id + "," + rowData.DisplayOrder + "," + ProductData[rowIndex - 1].DisplayOrder + ");\" >上移</a>  ";
                                    return s + d +f+ up;
                                }
                                else {
                                    down = " <a href='#' onclick=\"Down(" + rowData.Id + "," + ProductData[rowIndex + 1].Id + "," + rowData.DisplayOrder + "," + ProductData[rowIndex + 1].DisplayOrder + ");\" >下移</a> ";
                                    up = " <a href='#' onclick=\"Up(" + rowData.Id + "," + ProductData[rowIndex - 1].Id + "," + rowData.DisplayOrder + "," + ProductData[rowIndex - 1].DisplayOrder + ");\" >上移</a>  ";
                                    return s + d+f + up + down;
                                }
                                return s + d+f;
                            }
                        }
	                ]],
                toolbar: [{
                    text: '新增',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#win').window({
                            title: "新增产品",
                            width: 800,
                            height: 600,
                            cache: false,
                            modal: true,
                            collapsible: false,
                            minimizable: false,
                            maximizable: true,
                            href: "ProductFormSnippet.aspx"
                        });
                    }
                }],
                onLoadSuccess: function () {

                }
            });
        }
        
        function cancelFocus(id) {
            $.DotNet.Common.CommonUpdateInt("Product", "IsFocusPicture", 0, "Id", id, function(result) {
                if (result == 1) {
                    alert("取消成功");
                    $('#prGrid').datagrid("reload");
                }
            });
        }

        function setFocus(id) {
            $.DotNet.Common.CommonUpdateInt("Product", "IsFocusPicture", 1, "Id", id, function(result) {
                if (result == 1) {
                    alert("设置成功");
                    $('#prGrid').datagrid("reload");
                }
            });
        }

        function modifyProduct(id) {
            $('#win').window({
                title: "修改产品",
                width: 800,
                height: 600,
                modal: true,
                collapsible: false,
                minimizable: false,
                maximizable:true,
                cache: false,
                href: "ProductFormSnippet.aspx?id="+id
            });
        }
        
        function deleteProduct(id) {
            $.DotNet.Common.DeleteProductById(id, function(result) {
                if (result != 0) {
                    $('#prGrid').datagrid("reload");
                }
            });
        }

        function Up(p1, p2, o1, o2) {
            $.DotNet.Common.MoveUp("Product", "Id", "DisplayOrder", p1, p2, o1, o2, function (result) {
                if (result == 2) {
                    alert("上移成功");
                    $('#prGrid').datagrid("reload");
                }
            });
        }

        function Down(p1, p2, o1, o2) {
            $.DotNet.Common.MoveDown("Product", "Id", "DisplayOrder", p1, p2, o1, o2, function (result) {
                if (result == 2) {
                    alert("下移成功");
                    $('#prGrid').datagrid("reload");
                }
            });
        }
    </script>

<table id="prGrid">
</table>
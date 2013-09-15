<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLinkList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.FriendLinkList" %>

<table id="FriendLinkList"></table>

<script type="text/javascript">

    initGrid();
    function initGrid() {
        $('#FriendLinkList').datagrid({
            queryParams: { page: 1, rows: 2, sort: 'Id', order: 'desc' },
            title: '友情链接列表',
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
	                    { field: 'Title', title: '产品名称', width: 200 },
	                    { field: 'CategoryName', title: '分类名称', width: 150 },
	                    { field: 'InDate', title: '添加时间', width: 120 },
                        { field: 'Operate', title: '操作', width: 120, align: "center",
                            formatter: function (value, rowData, rowIndex) {
                                var s = '<a href="#" onclick=modifyProduct(' + rowData.Id + ')>修改</a> ';
                                var d = '<a href="#" onclick="deleteProduct(' + rowData.Id + ')">删除</a> ';
                                return s + d;
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
                        href: "FriendLinkForm.aspx"
                    });
                }
            }],
            onLoadSuccess: function () {

            }
        });
    }

    function modifyProduct(id) {
        $('#win').window({
            title: "修改文章",
            width: 800,
            height: 600,
            modal: true,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            cache: false,
            href: "FriendLinkForm.aspx?id=" + id
        });
    }

    function deleteProduct(id) {
        $.DotNet.Common.DeleteProductById(id, function (result) {
            if (result != 0) {
                $('#FriendLinkList').datagrid("reload");
            }
        });
    }
    </script>
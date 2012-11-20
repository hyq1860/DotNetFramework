<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicContentListSnippet.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.BasicContentListSnippet" %>


<script type="text/javascript">
    initBasicContent();

    function initBasicContent() {
        $('#basicContent').datagrid({
            queryParams: { page: 1, rows: 2, sort: 'Id', order: 'desc' },
            title: '站点内容管理',
            iconCls: 'icon-save',
            method: "get",
            striped: true,
            url: '<%=RootPath %>/Ajax/Common.ashx/GetBasicContents',
            idField: 'Id',
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            //fit: true,
            //fitColumns: true,
            //nowarp:false,
            columns: [[
	                { field: 'Id', title: '编号', width: 40 },
	                { field: 'Title', title: '标题', width: 400 },
	                { field: 'InDate', title: '添加时间', width: 130 },
                    { field: 'Operate', title: '操作', width: 120,align:"center",
                        formatter: function (value, rowData, rowIndex) {
                            //var s = '<a href="BasicContentFormSnippet.aspx?Id=' + rowData.Id + '">修改</a> ';
                            var s = '<a href="#" onclick=modify(' + rowData.Id + ')>修改</a> ';
                            //var d = '<a href="#" onclick="DeleteProduct(' + rowData.Id + ')">删除</a> ';
                            return s;
                        }
                    }
	            ]],
            //                toolbar: [{
            //                    text: '新增',
            //                    iconCls: 'icon-add',
            //                    handler: function () {
            //                        window.location.href = "BasicContentForm.aspx";
            //                    }
            //                }],
            onLoadSuccess: function () {

            }
        });
    }

    function modify(id) {
        $('#win').window({
            title: "修改信息",
            width: 800,
            height: 600,
            modal: true,
            collapsible: false,
            minimizable: false,
            maximizable: false,
            //fit:true,
            cache: false,
            href: "BasicContentFormSnippet.aspx?Id=" + id
        });
    }
</script>

<table id="basicContent"></table>
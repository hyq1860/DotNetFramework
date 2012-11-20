<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManage.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
    <script type="text/javascript" src="<%=RootPath %>/Scripts/common.js"></script>
<%--    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>--%>
    
    <script type="text/javascript">

    function BrowseServer() {
        // You can use the "CKFinder" class to render CKFinder in a page:
        var finder = new CKFinder();
        finder.basePath = '../'; // The path for the installation of CKFinder (default = "/ckfinder/").
        finder.selectActionFunction = SetFileField;
        finder.popup();
    }

    function SetFileField(fileUrl) {
        document.getElementById('mediumpicture').value = fileUrl;
    }
     </script>

    <style>
        body{ padding:4px;margin: 0;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            loadThemes();
            initGrid();
            //initCkeditor();
        });

        function initCkeditor() {
            var areditor = CKEDITOR.replace('editor_kama', {
                filebrowserBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html")%>',
                filebrowserImageBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Images")%>',
                filebrowserFlashBrowseUrl: '<%=ResolveUrl("~/ckfinder/ckfinder.html?Type=Flash")%>',
                filebrowserUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files")%>',
                filebrowserImageUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images")%>',
                filebrowserFlashUploadUrl: '<%=ResolveUrl("~/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash")%>'
            });
        }

        var buildFlag = false;
        function initGrid() {
            $('#prGrid').datagrid({
                queryParams: { page: 1, rows: 2, sort: 'Id', order: 'desc' },
                title: '产品列表',
                iconCls: 'icon-save',
                method: "get",
                striped: true,//是否隔行换色
                url: '<%=RootPath %>/Ajax/Common.ashx/GetProducts',
                idField: 'Id',
                pagination: true,
                rownumbers: true,
                singleSelect: true,
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
                        { field: 'Operate', title: '操作', width: 120,
                            formatter: function (value, rowData, rowIndex) {
                                var s = '<a href="ProductForm.aspx?Id=' + rowData.Id + '">修改</a> ';
                                var d = '<a href="#" onclick="DeleteProduct(' + rowData.Id + ')">删除</a> ';
                                return s + d;
                            }
                        }
	                ]],
                toolbar: [{
                    text: '新增',
                    iconCls: 'icon-add',
                    handler: function () {
                        window.location.href = "ProductForm.aspx";
                    }
                }],
                onLoadSuccess: function () {

                }
            });
        }
        
    </script>
</head>
<body>
<table id="prGrid">
</table>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicContentManage.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.BasicContentManage" %>

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
    <script type="text/javascript" src="<%=RootPath %>/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/ckfinder/ckfinder.js"></script>
    <script type="text/javascript">
        $(function () {
            initBasicContent();
        });
        
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
	                    { field: 'Title', title: '标题', width: 120 },
	                    { field: 'InDate', title: '添加时间', width: 120 },
                        { field: 'Operate', title: '操作', width: 120,
                            formatter: function (value, rowData, rowIndex) {
                                var s = '<a href="BasicContentForm.aspx?Id=' + rowData.Id + '">修改</a> ';
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
    </script>
    <style>
        body{ padding:4px;margin: 0;}
    </style>
</head>
<body>
<table id="basicContent"></table> 
</body>
</html>

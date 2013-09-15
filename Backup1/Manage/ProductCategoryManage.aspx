<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoryManage.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductCategoryManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css" />
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
<%--    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/tree.loadFilter.js"></script>--%>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/treegrid.loadFilter.js"></script>
<%--    <script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>--%>
    <script type="text/javascript">

//        $(function () {
//            $('#tt3').combotree({
//                //checkbox: true,
//                url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory',
//                onSelect: function (node) {
//                    //返回树对象  
//                    var tree = $(this).tree;
//                    //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
//                    var isLeaf = tree('isLeaf', node.target);
//                    if (!isLeaf) {
//                        //清除选中  
//                        $('#tt3').combotree('clear');
//                    }
//                }
//            });
//        });
        function up(id) {
            var tid = $('#test').treegrid("getLevel", id);
            var tch = $('#test').treegrid("getParent", id);
        }

        $(function () {
            $('#test').treegrid({
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
				    { field: 'Name', title: '分类名称', width: 220 },
                    { field: 'Desc', title: '描述', width: 220 },
                    { field: 'opt', title: '操作', width: 120, align: 'center',
                        formatter: function (value, rowData, rowIndex) {
                            var del = "";
                            if (rowData == 'True') {
                                del = "<a onclick=\"DeleteProductCategoryById('" + rowData.Id + "');\" >删除</a>";
                            }
                            var modify = "<a href=ProductCategoryForm.aspx?id=" + rowData.Id + ">修改</a>";
                            var ud = "<a onclick=\"up('" + rowData.Id + "');\" >测试</a>";
                            return modify + del + ud;

                        }
                    }
				]],
                toolbar: [{
                    text: '新增',
                    iconCls: 'icon-add',
                    handler: function () {
                        window.location.href = "ProductCategoryForm.aspx";
                    }
                }]
            });
        });
    </script>
    
    <style>
        body{ padding:4px;margin: 0;}
    </style>
</head>
<body>
<table id="test"></table>
</body>
</html>

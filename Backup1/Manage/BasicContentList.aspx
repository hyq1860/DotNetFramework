<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicContentList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.BasicContentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../jQueryGrid/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="../jQueryGrid/grid.locale-cn.js"></script> 
    <link rel="Stylesheet" type="text/css" href="../jQueryUI/css/blitzer/jquery-ui-1.8.20.custom.css" />
    <link rel="Stylesheet" type="text/css" href="../jQueryGrid/ui.jqgrid.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#gridBasicContent").jqGrid({
                url: '../Ajax/Common.ashx/GetBasicContents',
                datatype: 'json',
                hidegrid: false,
                rowNum: 2,
                rowList: [10, 20, 30],
                colNames: ['编号', '标题', '分类', "操作"],
                colModel: [
                    { name: 'Id', index: 'Id', sortable: false },
                    { name: 'Title', index: 'Title', sortable: false },
                    { name: 'Category', index: 'Category', sortable: false },
                    { name: 'act', index: 'act', sortable: false, formatter: function (cellvalue, options, rowObject) {
                        //var del = "<a onclick=\"DeleteProductById('" + rowObject.Id + "');\" >删除</a>";
                        var modify = "<a href=BasicContentEditForm.aspx?id=" + rowObject.Id + ">修改</a>";
                        return modify;
                    }
                    }
                ],
                jsonReader: {
                    root: "rows",
                    page: "pageindex",
                    total: "pagecount",
                    records: "total",
                    repeatitems: false
                },
                loadonce: false,
                sortname: 'Id',
                sortorder: 'desc',
                gridComplete: function () {
                },
                pager: "#gridBasicContentPager",
                viewrecords: true,
                toolbar: [true, "top"],
                height: "100%",
                width: "100%",
                caption: "网站基本内容列表"
            });

            jQuery("#gridBasicContent").jqGrid('navGrid', '#gridBasicContentToolBar', { edit: false, add: false, del: false });
            var toolbar = "<a id='addProduct' href='BasicContentForm.aspx'>新增</a>";
            var addProduct = $(toolbar).button({
                text: '新增',
                icons: {
                    primary: "ui-icon-plusthick"
                }
            });
            $("#t_gridBasicContent").append(addProduct);
            //$("input", "#t_gridProduct").click(function () { alert("Hi! I'm added button at this toolbar"); });
        });
    </script>
</head>
<body>
    <table id="gridBasicContent">
    </table>
    <span id="gridBasicContentToolBar"></span>
    <div id="gridBasicContentPager">
    </div>
</body>
</html>

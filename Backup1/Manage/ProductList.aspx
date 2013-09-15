<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ProductList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../jQueryGrid/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
     <script type="text/javascript" src="../../jQueryGrid/grid.locale-cn.js"></script> 
     <link rel="Stylesheet" type="text/css" href="../../jQueryUI/css/ui-lightness/jquery-ui-1.8.20.custom.css" />
     <link rel="Stylesheet" type="text/css" href="../../jQueryGrid/ui.jqgrid.css" />
      <script type="text/javascript" src="../Ajax/Common.ashx"></script>
      <%--http://www.cnblogs.com/mycoding/archive/2011/07/07/2099878.html--%>

</head>
<body>
    <script type="text/javascript">
        var insertClick = function (e) {
            $("<iframe id='editFrame' src='ProductForm.aspx?action=insert' />").dialog({ autoOpen: true, modal: true, title: "新增产品" });
            e.preventDefault();
        };
        var editClick = function (e) {
            var editUrl = "ProductForm.aspx?action=edit&id=" + $(this).attr("rowid");
            $("<iframe id='editFrame'/>").attr("src", editUrl).dialog({ autoOpen: true, modal: true, title: "编辑产品" });
            e.preventDefault();
        };
        //http://www.ok-soft-gmbh.com/jqGrid/SimpleLocalGridWithLinkPager1.htm
        //http://www.ok-soft-gmbh.com/jqGrid/SimpleLocalGridWithLinkPager.htm
        //http://stackoverflow.com/questions/5800400/add-numeric-pager-to-jqgrid/5835542#5835542 自定义分页
        $(document).ready(function () {
            $("#gridProduct").jqGrid({
                url: '../Ajax/Common.ashx/GetProducts',
                datatype: 'json',
                hidegrid: false,
                rowNum: 8,
                pginput: false,
                loadtext: "数据加载中...",
                shrinkToFit: false,
                //rowList: [10, 20, 30],
                rownumbers: true,
                colNames: ['产品编号', '产品名称', '产品分类','添加时间', "操作"],
                colModel: [
                    { name: 'Id', index: 'Id', sortable: false,width:60,align:"center"  },
                    { name: 'Title', index: 'Title', sortable: false, align: "center" },
                    { name: 'CategoryName', index: 'CategoryName', sortable: false, align: "center" },
                    { name: 'InDate', index: 'InDate', sortable: false, align: "center"},
                    { name: 'act', index: 'act',align:"center", sortable: false, formatter: function (cellvalue, options, rowObject) {
                        var del = "<a onclick=\"DeleteProductById('" + rowObject.Id + "');\" >删除</a>";
                        var modify = "<a href=ProductEditForm.aspx?id=" + rowObject.Id + ">修改</a>";
                        return modify+del ;
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
                ondblClickRow: function () {
                    var gd = $("#gridProduct").getGridParam('selrow'); //获取选择行
                    var id = $("#gridProduct").getCell(gd, "Id");  //获取选择行数据
                    if (id) {
                        var ret = jQuery("#gridProduct").jqGrid('getRowData', id); //行对象
                        alert(ret.CategoryName);
                        //jQuery("#table_boxcenter").dialog("open");
                    }
                },
                pager: "#gridProductPager",
                pagerpos: "left", //center,right定义导航栏的位置，默认分为三部分：翻页，导航工具及记录信息
                pginput: true, //是否显示跳转页面的输入框
                pgbuttons: true, //是否显示翻页按钮
                //emptyrecords: "Nothing to display",
                //pgtext: "Page {0} of {1}",
                //recordtext: "View {0} - {1} of {2}",
                viewrecords: true, //设置是否在Pager Bar显示所有记录的总数
                //altRows:true,基偶行
                //toolbar: [true, "top"],
                height: "100%",
                width: "100%",
                caption: "产品列表",
                custom_pager:true
            });

            //jQuery("#gridProduct").jqGrid('navGrid',{ edit: true, add: true, del: true });
            jQuery("#gridProduct").jqGrid('navGrid','#gridProductToolBar',{edit:true,add:true,del:true});
            
            var toolbar = "<a id='addProduct' href='ProductForm.aspx'>新增</a>";
            var toolbar1="<table><tr><td><a style='font-size:14px;line-height:30px;' id='addProduct' href='ProductForm.aspx'>新增</a></td><td></td></tr></table>"

//            var addProduct = $(toolbar).button({
//                text: '新增',
//                icons: {
//                    primary: "ui-icon-plusthick"
//                }
//            });
            //$("#t_gridProduct").append($(toolbar1));
            //$("input", "#t_gridProduct").click(function () { alert("Hi! I'm added button at this toolbar"); });
        });
        
        function DeleteProductById(id) {
            $.DotNet.Common.DeleteProductById(id, function(result) {
                alert(result);
            });
        }
       


        //========================jqgrid自适应窗口========================//
        $(window).bind('resize', function () {
            // Get width of parent container
            //var width = jQuery("body").attr('clientWidth');
            var width = $(document.body)[0].clientWidth;
            //var height = $(document.body)[0].clientHeight;
            //var height = jQuery("body").height();
            if (width == null || width < 1) {// For IE, revert to offsetWidth if necessary
                width = jQuery('body').attr('offsetWidth');
            }
            width = width - 2; // Fudge factor to prevent horizontal scrollbars
            if (width > 0 && Math.abs(width - jQuery("#list").width()) > 5) {
                // Only resize if new width exceeds a minimal threshold
                // Fixes IE issue with in-place resizing when mousing-over frame bars
                jQuery("#gridProduct").setGridWidth(width);
            }
        }).trigger('resize');
    </script>
    <style>
        body{ padding:4px;margin: 0;}
    </style>
    
    <table id="gridProduct">
    </table>
    <span id="gridProductToolBar"></span>
    <div id="gridProductPager">
    </div>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList.ascx.cs" Inherits="DotNet.EnterpriseWebSite.UserControls.ProductList" %>




     


    <div>
        <button id="create">新增</button>
    </div>

    <table id="gridTable">
    </table>
    <div id="gridPager">
    </div>
    
        <script type="text/javascript">
            $("#gridTable").jqGrid({
                url: 'Ajax/Common.ashx/GetVar2',
                datatype: 'json',
                rowNum: 10,
                rowList: [10, 20, 30],
                colNames: ['订单ID', '客户ID', '送货人', '下单时间', "操作"],
                colModel: [
                    { name: 'menuid', index: 'menuid', sortable: false },
                    { name: 'menuname', index: 'menuname', sortable: false },
                    { name: 'url', index: 'url', sortable: false },
                    { name: 'Enable', index: 'Enable', sortable: false },
                    { name: 'act', index: 'act', sortable: false }
                ],
                jsonReader: {
                    root: "rows",
                    page: "pageindex",
                    total: "pagecount",
                    records: "total",
                    repeatitems: false
                },
                loadonce: true,
                sortname: 'menuid',
                sortorder: 'desc',
                gridComplete: function () {
                    var ids = jQuery("#gridTable").jqGrid('getDataIDs');
                    for (var i = 0; i < ids.length; i++) {
                        var cl = ids[i];
                        be = "<a onclick=\"jQuery('#gridTable').editRow('" + cl + "');\" >修改</a>";
                        se = "<a' onclick=\"jQuery('#gridTable').saveRow('" + cl + "');\" >删除</a>";
                        jQuery("#gridTable").jqGrid('setRowData', ids[i], { act: be + " " + se });
                    }
                },
                pager: "#gridPager",
                viewrecords: true,
                height: "100%",
                width: "100%",
                caption: "模块数据"
            });

            // 新增按钮
            //            $("#create")
            //                .button()
            //                .click(insertClick);


            //        var insertClick = function (e) {
            //            $("<iframe id='editFrame' src='ProductForm.aspx?action=insert' />").dialog({ autoOpen: true, modal: true, title: "新增产品" });
            //            e.preventDefault();
            //        };
            //        var editClick = function (e) {
            //            var editUrl = "ProductForm.aspx?action=edit&id=" + $(this).attr("rowid");
            //            $("<iframe id='editFrame'/>").attr("src", editUrl).dialog({ autoOpen: true, modal: true, title: "编辑产品" });
            //            e.preventDefault();
            //        };
    </script>
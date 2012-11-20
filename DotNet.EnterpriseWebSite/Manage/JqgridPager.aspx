<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JqgridPager.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.JqgridPager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Just simple local grid</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="text/javascript" src="../../jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../jQueryGrid/jquery.jqGrid.min.js"></script>
    <script type="text/javascript" src="../../jQueryUI/jquery-ui-1.8.20.custom.min.js"></script>
     <script type="text/javascript" src="../../jQueryGrid/grid.locale-cn.js"></script> 
     <link rel="Stylesheet" type="text/css" href="../../jQueryUI/css/blitzer/jquery-ui-1.8.20.custom.css" />
     <link rel="Stylesheet" type="text/css" href="../../jQueryGrid/ui.jqgrid.css" />
     <script type="text/javascript" src="../Ajax/Common.ashx"></script>
    <style type="text/css">
        td.myPager a { text-decoration:underline !important }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            var mydata = [
                    { id: "1", invdate: "2007-10-01", name: "test", note: "note", amount: "200.00", tax: "10.00", closed: true, ship_via: "TN", total: "210.00" },
                    { id: "2", invdate: "2007-10-02", name: "test2", note: "note2", amount: "300.00", tax: "20.00", closed: false, ship_via: "FE", total: "320.00" },
                    { id: "3", invdate: "2007-09-01", name: "test3", note: "note3", amount: "400.00", tax: "30.00", closed: false, ship_via: "FE", total: "430.00" },
                    { id: "4", invdate: "2007-10-04", name: "test4", note: "note4", amount: "200.00", tax: "10.00", closed: true, ship_via: "TN", total: "210.00" },
                    { id: "5", invdate: "2007-10-31", name: "test5", note: "note5", amount: "300.00", tax: "20.00", closed: false, ship_via: "FE", total: "320.00" },
                    { id: "6", invdate: "2007-09-06", name: "test6", note: "note6", amount: "400.00", tax: "30.00", closed: false, ship_via: "FE", total: "430.00" },
                    { id: "7", invdate: "2007-10-04", name: "test7", note: "note7", amount: "200.00", tax: "10.00", closed: true, ship_via: "TN", total: "210.00" },
                    { id: "8", invdate: "2007-10-03", name: "test8", note: "note8", amount: "300.00", tax: "20.00", closed: false, ship_via: "FE", total: "320.00" },
                    { id: "9", invdate: "2007-09-01", name: "test9", note: "note9", amount: "400.00", tax: "30.00", closed: false, ship_via: "TN", total: "430.00" },
                    { id: "10", invdate: "2007-09-08", name: "test10", note: "note10", amount: "500.00", tax: "30.00", closed: true, ship_via: "TN", total: "530.00" },
                    { id: "11", invdate: "2007-09-08", name: "test11", note: "note11", amount: "500.00", tax: "30.00", closed: false, ship_via: "FE", total: "530.00" },
                    { id: "12", invdate: "2007-09-10", name: "test12", note: "note12", amount: "500.00", tax: "30.00", closed: false, ship_via: "FE", total: "530.00" }
                ],
                grid = $("#list"), MAX_PAGERS = 2;

            grid.jqGrid({
                url: '../Ajax/Common.ashx/GetProducts',
                datatype: 'json',
                colNames: ['产品编号', '产品名称'],
                colModel: [
                    { name: 'Id', index: 'Id', sortable: false,align:"center"  },
                    { name: 'Title', index: 'Title', sortable: false, align: "center" }
                ],
                rowNum: 2,
                rowList: [2, 5, 10, 20],
                pager: '#pager1',
                pginput: false,
                gridview: true,
                rownumbers: true,
                viewrecords: true,
                caption: 'Just simple local grid',
                height: '100%',
                loadComplete: function () {
                    var i, myPageRefresh = function (e) {
                        var newPage = $(e.target).text();
                        grid.trigger("reloadGrid", [{ page: newPage}]);
                        e.preventDefault();
                    };

                    $(grid[0].p.pager + '_center td.myPager').remove();
                    var pagerPrevTD = $('<td>', { class: "myPager" }), prevPagesIncluded = 0,
                        pagerNextTD = $('<td>', { class: "myPager" }), nextPagesIncluded = 0,
                        totalStyle = grid[0].p.pginput === false,
                        startIndex = totalStyle ? this.p.page - MAX_PAGERS * 2 : this.p.page - MAX_PAGERS;
                    for (i = startIndex; i <= this.p.lastpage && (totalStyle ? (prevPagesIncluded + nextPagesIncluded < MAX_PAGERS * 2) : (nextPagesIncluded < MAX_PAGERS)); i++) {
                        if (i <= 0 || i === this.p.page) { continue; }

                        var link = $('<a>', { href: '#', click: myPageRefresh });
                        link.text(String(i));
                        if (i < this.p.page || totalStyle) {
                            if (prevPagesIncluded > 0) { pagerPrevTD.append('<span>,&nbsp;</span>'); }
                            pagerPrevTD.append(link);
                            prevPagesIncluded++;
                        } else {
                            if (nextPagesIncluded > 0 || (totalStyle && prevPagesIncluded > 0)) { pagerNextTD.append('<span>,&nbsp;</span>'); }
                            pagerNextTD.append(link);
                            nextPagesIncluded++;
                        }
                    }
                    if (prevPagesIncluded > 0) {
                        $(grid[0].p.pager + '_center td[id^="prev"]').after(pagerPrevTD);
                    }
                    if (nextPagesIncluded > 0) {
                        $(grid[0].p.pager + '_center td[id^="next"]').before(pagerNextTD);
                    }
                }
            });
        });
    </script>
</head>
<body>
    <table id="list"><tr><td/></tr></table>
    <div id="pager1"></div>
</body>
</html>
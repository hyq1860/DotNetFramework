<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataGridFileView.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.DataGridFileView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script>
        $(function () {
            $('#tt').datagrid({
                title: 'DataGrid - DetailView',
                fit: true,
                remoteSort: false,
                singleSelect: false,
                nowrap: false,
                fitColumns: true,
                url: 'datagrid_data.json',
                columns: [[
					{ field: 'itemid', title: 'Item ID', width: 80 },
					{ field: 'productid', title: 'Product ID', width: 100, sortable: true },
					{ field: 'listprice', title: 'List Price', width: 80, align: 'right', sortable: true },
					{ field: 'unitcost', title: 'Unit Cost', width: 80, align: 'right', sortable: true },
					{ field: 'attr1', title: 'Attribute', width: 40, sortable: true },
					{ field: 'status', title: 'Status', width: 60, align: 'center' }
				]],
                view: fileview,
                textField: "attr1",
                imgFormatter: function (rowIndex, rowData) {
                    return '<img src="images/' + rowData.itemid + '.png" >';
                },
                onDblClickCell: function (rowIndex, field, value) {
                    console.log(rowIndex + ":" + field + ":" + value);
                }
            });
        });
    </script>
</head>
<body>
<table id="tt"></table>
</body>
</html>

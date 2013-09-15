<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyForLoanList.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.ApplyForLoanList" %>

<script type="text/javascript">
    var ProductData;
    initGrid();
    function initGrid() {
        $('#prGrid').datagrid({
            title: '贷款申请列表',
            iconCls: 'icon-save',
            method: "get",
            striped: true, //是否隔行换色
            url: '<%=RootPath %>/Ajax/Common.ashx/GetApplyForLoan',
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
	                    { field: 'ApplyId', title: '申请编号', width: 80 },
	                    { field: 'UserName', title: '申请人姓名', width: 100 },
	                    { field: 'CellPhone', title: '手机', width: 180 },
                        { field: 'Loan', title: '贷款金额(元)', width: 300 },
                        { field: 'LoanType', title: '贷款类型', width: 100 },
                        { field: 'Mortgage', title: '有无抵押', width: 100 },
	                    { field: 'InDate', title: '申请时间', width: 125 }
                        
	                ]],
            onLoadSuccess: function () {

            }
        });
    }
    </script>

<table id="prGrid">
</table>

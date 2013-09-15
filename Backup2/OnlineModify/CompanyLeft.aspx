<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyLeft.aspx.cs" Inherits="yujiajun.Admin.CompanyFileManage.CompanyLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script src="../JQuery zTree v3.3/js/jquery.ztree.all-3.3.min.js" type="text/javascript"></script>
    <link href="../JQuery zTree v3.3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="height:100%;margin:0px;" scroll="yes">
    <div class="zTreeDemoBackground left">
		<ul id="treeDemo" class="ztree"></ul>
	</div>
    <asp:Literal ID="ltrTree" runat="server"></asp:Literal>
</body>
</html>

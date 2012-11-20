<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyMain.aspx.cs" Inherits="yujiajun.Admin.CompanyFileManage.CompanyMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="color:Red"><h2>非专业人士,请勿随意操作</h2></div>
    <div>
        <textarea rows="20" cols="100" runat="server" id="txtContent"></textarea><br/>
        <asp:Button ID="btnOk"  runat="server" Text="确认修改" onclick="btnOk_Click" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pager_01.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Pager.Pager_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
div.pager a
{
    display:block;
    float:left;
    margin-left:3px;
    text-align:center;
    text-decoration:none;
    border:1px solid Gray;
    color:Gray;
    width:26px;
}
#pre
{
    width:80px;
}
#pre.false:hover
{
    border:1px solid Gray;
    color:Gray;
    background-Color:White;
}
#next
{
    width:80px;
}
div.pager a:hover,div.pager a.Active
{
    border:1px solid black;
    background-Color:#4b6c9e;
    color:#f9f9f9;
}
    </style>
</head>
<body>
<% =Paging.RenderPager2()%>
</body>
</html>

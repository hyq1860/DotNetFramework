<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleManage.aspx.cs" Inherits="DotNet.WebSite.ArticleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <form>
<!--普通区域-->
 <input id="Text1" type="text" /><br/>
  <input id="Text3" type="password" /><br/>
 <input id="Text2" type="text" /><br/>
 <textarea id="TextArea1" cols="20" rows="2"></textarea><br/>
<!--普通区域-->

<!--属性区域-->
<div id="propertyContainer" style="border: 1px solid red;">
    <%=Html %>
</div>
<!--属性区域-->
 </form>
</body>
</html>

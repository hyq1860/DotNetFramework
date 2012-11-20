<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Content" %>
<%@ Import Namespace="DotNet.Common" %>
<%@ Import Namespace="DotNet.EnterpriseWebSite" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=this.Option("SiteName")%>-产品页</title>
    <script type="text/javascript" src="jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="soChange/jquery.soChange-min.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="soChange/style.css" />
    <link rel="stylesheet" type="text/css" href="css/css.css" />
    <script>
        $(function () {
            $('#change_1 .a_bigImg').soChange();
        })
    </script>
</head>
<body>
<div class="mod_title">
	<div class="mod mod_title_in">
    	<a href="" target="_blank">中文</a>|<a href="" target="_blank">English</a>
    </div>
</div>
<div class="mod mod_header clearfix">
	<h1><a href="" target="_blank"></a></h1>
    <ul class="nav">
    	<li><a href="<%=this.Lang("HomeUrl") %>" target="_self"><%=this.Lang("HomePage")%></a></li>
        <li><a href="Content.aspx?id=1" target="_self"><%=this.Lang("Company")%></a></li>
        <li><a href="ProductCenter.aspx" target="_self"><%=this.Lang("ProductCenter")%></a></li>
        <li><a href="Content.aspx?id=2" target="_self"><%=this.Lang("About")%></a></li>
        <li><a href="Content.aspx?id=3" target="_self"><%=this.Lang("Jobs")%></a></li>
        <li><a href="Content.aspx?id=4" target="_self"><%=this.Lang("ContactUs")%></a></li>
    </ul>
</div>
<div class="mod mod_focus">
    <div class="focus_content">
	    <div id="change_1">
            <% foreach (ArticleInfo articleInfo in FocusArticle)%>
            <%{%>
                <a href="#1" class="a_bigImg"><img src="<%=articleInfo.FocusPicture %>" width="938" height="285" alt="" /></a>
            <%}%>
	    </div>
    </div>
	<div class="shadow"></div>
</div>

<div class="mod mod_main_content clearfix">
	<div class="bor mod_product_sub">
        <ul>
        <% foreach (var basicContent in BasicContentList)%>
        <%{ %>
            <li class="<%=basicContent.Id==BasicContent.Id?"current":"" %>"><a href="Content.aspx?id=<%=basicContent.Id %>" target="_self"><%=basicContent.Title%></a></li>
        <% }%>
        </ul>
     </div>

     <div class="bor mod_about_content">
     	<h2 class="bread">当前位置：<a href="<%=this.Lang("HomeUrl") %>"><%=this.Lang("HomePage") %></a> > <%=BasicContent.Title %> </h2>
     	<div class="mod_txt">
<%--     	    <h1><%=BasicContent.Title %></h1>--%>
            <div>
                <%=BasicContent.Content %>
            </div>
     	</div>
     </div>
</div>

<div class="mod_foot">
	<div class="mod foot">Copyright 2012 by shywxsy All Right Reserved.<%=this.Option("BeiAn")%></div>
</div>
</body>
</html>
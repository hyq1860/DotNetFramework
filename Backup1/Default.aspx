<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DotNet.EnterpriseWebSite._Default" %>
<%@ Import Namespace="DotNet.Common" %>
<%@ Import Namespace="DotNet.EnterpriseWebSite" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>
<%@ Import Namespace="Combres" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=this.Option("SiteName")%>_首页</title>
    <meta name="keywords" content="<%=this.Option("Keywords") %>">
	<meta name="description" content="<%=this.Option("Description") %>">
    <%= WebExtensions.CombresLink("siteCss") %>
    <%= WebExtensions.CombresLink("siteJs") %>
<%--<script type="text/javascript" src="jQuery/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="soChange/jquery.soChange-min.js"></script>
<link rel="stylesheet" type="text/css" media="all" href="soChange/style.css" />
<link rel="stylesheet" type="text/css" href="css/css.css" />--%>
    <script type="text/javascript">
        $(function () {
            $('#change_1 .a_bigImg').soChange({
                changeType: 'slide'
            });
        })
    </script>
</head>
<body>

    


<div class="mod_title">
	<div class="mod mod_title_in">
    	<a href="" target="_blank"><%=this.Lang("CN") %> </a>|<a href="" target="_blank"><%=this.Lang("US") %></a>
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
<div class="mod mod_product_list">
	<div class="bx_wrap">
        <div class="bx_container">
          <ul id="srcollpic">
            <% foreach (var product in Products)%>
            <% {%>
            <li>
            	<a href="ProductDetail.aspx?id=<%=product.Id %>"><img  alt="#" src="<%=product.MediumPicture %>" width="213" height="188" /></a>
            	<h3><a href="ProductDetail.aspx?id=<%=product.Id %>" target="_self"><%=product.Title %></a></h3>
            </li>
            <% } %>
          </ul>
        </div>
      </div>
</div>

<div class="mod_foot">
	<div class="mod foot">Copyright 2012 by shywxsy All Right Reserved.<%=this.Option("BeiAn")%></div>
</div>
<script type="text/javascript" src="jQueryPlugins/bxCarousel.js"></script>
<script type="text/javascript">
    $(function () {
        $('#srcollpic').bxCarousel({
            display_num: 4,
            move: 1,
            auto: true,
            controls: false,
            margin: 25,
            auto_hover: true
        });
    });
</script>

</body>
</html>

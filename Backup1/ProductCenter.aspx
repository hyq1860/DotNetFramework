<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCenter.aspx.cs" Inherits="DotNet.EnterpriseWebSite.ProductCenter" %>
<%@ Import Namespace="DotNet.Common" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="DotNet.EnterpriseWebSite" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>
<%@ Import Namespace="Combres" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=this.Option("SiteName")%>-产品页</title>
    <%= WebExtensions.CombresLink("siteCss") %>
    <%= WebExtensions.CombresLink("siteJs") %>
<%--    <script type="text/javascript" src="jQuery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="soChange/jquery.soChange-min.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="soChange/style.css" />
    <link rel="stylesheet" type="text/css" href="css/css.css" />--%>
    <script type="text/javascript">
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
<%--	<div class="bor mod_product_sub">
    	<h2>产品分类</h2>
        <ul>
        <% var i = 0; %>
        <% foreach (var productCategory in FirstCagegorys)%>
        <%{ %>
            <%if(Id==0&&i==0) %>
            <%{%>
                <li class="current"><a href="ProductCenter.aspx?id=<%=productCategory.Id %>" target="_self"><%=productCategory.Name %></a></li>
            <%}%>
            <%else%>
            <%{%>
                <li class="<% =(Id==productCategory.Id?"current":"") %>"><a href="ProductCenter.aspx?id=<%=productCategory.Id %>" target="_self"><%=productCategory.Name %></a></li>
            <%}%>
            <% i++; %>
        <% }%>
        </ul>
     </div>--%>
     	<div class="sub fl">
    	<div class="dt"><h2>产品列表</h2></div>
        <% foreach (var cagegory in FirstCagegorys) %>
        <% { %>
        <dl>
            <dd><%=cagegory.Name %></dd>
            <% 
                var childs = AllCagegorys.Where(s => s.Lft > cagegory.Lft && s.Rgt < cagegory.Rgt);
               %>
            <%if(childs.Count()>0) %>
            <%{%>
                <%foreach (var categoryInfo in childs)%>
                <%{%>
                <dt><a href="ProductList.aspx?Id=<%=categoryInfo.Id %>" target="_blank"><%=categoryInfo.Name %></a></dt>
                <%}%>
            <%} %>
         </dl>
        <% } %>

    </div>

     <div class="bor mod_product_content">
     	<h2 class="bread">
     	    <%=this.Lang("CurrentPosition")%>：
            <a href="<%=this.Lang("HomeUrl") %>" target="_self"><%=this.Lang("HomePage")%></a>>
            <a href="ProductCenter.aspx"><%=this.Lang("ProductCenter")%></a>
     	</h2>
     	<div class="mod_txt">
     	    
        <% foreach (var productCategoryInfo in Series)%>
        <%{ %>
        
            <div class="mod_product_item clearfix">
     			<div class="product_pic">
     			    <a href="ProductList.aspx?Id=<%=productCategoryInfo.Id %>">
     			        <img width="200px" height="200px" src="<%=productCategoryInfo.MediumPicture %>" alt="" />
     			    </a>
     			</div>
     			<div class="product_info">
     				<h3><a href="ProductList.aspx?Id=<%=productCategoryInfo.Id %>" target="_blank"><%=productCategoryInfo.Name %></a></h3>
                    
     				<p><strong>型号：</strong><%=productCategoryInfo.Desc %></p>
     			</div>	
     		</div>

        <% } %>
        <%if(Series==null||!Series.Any()) %>
        <%{%>
        暂时没有相关产品
        <%}%>
     	</div>

     </div>
</div>

<div class="mod_foot">
	<div class="mod foot">Copyright 2012 by shywxsy All Right Reserved.<%=this.Option("BeiAn")%></div>
</div>
</body>
</html>



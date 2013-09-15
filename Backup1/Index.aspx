<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Index" %>

<%@ Import Namespace="DotNet.Common" %>
<%@ Import Namespace="DotNet.EnterpriseWebSite" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>
<%@ Import Namespace="Combres" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract.Entity" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="DotNet.Common.Utility" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=this.Option("SiteName")%>_首页</title>
    <meta name="keywords" content="<%=this.Option("Keywords") %>">
	<meta name="description" content="<%=this.Option("Description") %>">
<%--    <%= WebExtensions.CombresLink("siteCss") %>
    <%= WebExtensions.CombresLink("siteJs") %>--%>
    
    
<script type="text/javascript" src="jQuery/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="soChange/jquery.soChange-min.js"></script>
<script type="text/javascript" src="jQuery/jquery.jcarousel.min.js"></script>
<link rel="stylesheet" type="text/css" media="all" href="soChange/style.css" />
<link rel="stylesheet" type="text/css" href="css/css.css" />
    <script type="text/javascript">
        $(function () {
            $('#change_1 .a_bigImg').soChange({
                changeType: 'slide'
            });
            jQuery('#mycarousel').jcarousel({ auto: 1 });
        })
    </script>
    
    <script language="javascript" src="Scripts/scroll.js"></script>
<style type="text/css">
#scrollbox{float:left;display:inline;height:120px; width:855px; overflow:hidden}
#scrollbox li{text-align:center;}
#scrollbox li img{width:145px;height:100px;}
#l2,#r2{cursor:pointer;}
</style>
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
<%--<div class="mod mod_product_list">
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
</div>--%>

<div class="mod clearfix mt20">
	<div class="fl gsjs">
    	<div class="dt"><h2>公司简介</h2></div>
        <div class="js-txt">
        	<img src="images/company.jpg" alt="公司照片" />
            <p>
                <%= BasicContent.Summary.CutString(360)%>
            </p>
            <p class="more"><a href="Content.aspx?id=1" target="_blank">详细>></a></p>
        </div>
    </div>
    <div class="fr xswl"><img src="images/e8ae247c-72aa-4b6d-8929-863b1a46ac9b.jpg" alt="" /></div>
</div>

<div class="mod clearfix mod_product mt20">
	<div class="pro_info">
	    <table width="100%" border="0" cellspacing="0" cellpadding="0">
  			<tr>
    			<td width="30" valign="middle"><img src="images/btnl.png" name="l2" width="22" height="39" id="l2"></td>
    			<td width="810" valign="top">
	    <div id="scrollbox">
			<ul>
			<% foreach (var product in Products)%>
            <% {%>
            <li>
                <a href="ProductDetail.aspx?id=<%=product.Id %>" target="_self">
                    <img alt="<%=product.Title %>" src="<%=product.MediumPicture %>"/>
                </a>
            </li>
            <% } %>
         					
			</ul>
		</div>
            </td>
    <td width="" valign="middle"><img src="images/btnr.png" width="22" height="39" id="r2"></td>
  </tr>
</table>
<script type="text/javascript">
    var scrollPic2 = new ScrollPic();
    scrollPic2.scrollContId = "scrollbox"; //内容容器ID
    scrollPic2.arrLeftId = "l2"; //左箭头ID
    scrollPic2.arrRightId = "r2"; //右箭头ID
    scrollPic2.frameWidth = 810; //显示框宽度
    scrollPic2.pageWidth = 324; //翻页宽度
    scrollPic2.speed = 10; //移动速度(单位毫秒，越小越快)
    scrollPic2.space = 10; //每次移动像素(单位px，越大越快)
    scrollPic2.autoPlay = true; //自动播放
    scrollPic2.autoPlayTime = 3; //自动播放间隔时间(秒)
    scrollPic2.initialize(); //初始化
</script>

<%--    	<ul id="mycarousel">
    	    <% foreach (var product in Products)%>
            <% {%>
            <li><img src="<%=product.MediumPicture %>" alt="" /></li>
            <li>
        		<a href="ProductDetail.aspx?id=<%=product.Id %>" target="_self"><img src="<%=product.MediumPicture %>" alt="%=product.Title %>" /></a>
        	</li>
            <% } %>
    	</ul>--%>
    </div>
</div>

<div class="mod clearfix mt20">
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
    <div class="mainlist fr">
    	<div class="dt"><h2>产品展示</h2></div>
        <div class="inner clearfix">
        	<ul>
        	<% foreach (var product in Products)%>
            <% {%>
                <li>
            	    <a href="ProductDetail.aspx?id=<%=product.Id %>" class="link">
            	        <img  alt="#" src="<%=product.MediumPicture %>" width="213" height="188" />
            	    </a>
                    <h5><a href="ProductDetail.aspx?id=<%=product.Id %> target="self"><%=product.Title %></a></h5>
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

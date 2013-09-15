<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentV2.aspx.cs" Inherits="DotNet.EnterpriseWebSite.ContentV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>郑州康家贷款咨询有限公司</title>
<link href="201201Styles/css/commom.css" type="text/css" rel="stylesheet"/>
<link href="201201Styles/css/css.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="http://www.zzktz.com/statics/js/jquery-1.4.2.min.js"></script>
</head>
<body>
<div id="header" class="header_top">
 	<a href="Home.aspx" target="_blank"><img src="201201Styles/images/logo.jpg" alt="郑州康家贷款咨询有限公司" /></a>
</div>
<div class="mainNav">
	<ul>
		<li><a  id="thisclass" href="Home.aspx" target="_blank">康家首页</a></li>   
		<li><a href="ContentV2.aspx?id=1" target="_blank">康家历史</a></li>
		<li><a href="ProductListV2.aspx" target="_blank">公司产品</a></li>
		<li><a href="ContentV2.aspx?id=3" target="_blank">工作机会</a></li>
		<li><a href="ContentV2.aspx?id=4" target="_blank">联系方式</a></li>
	</ul>
</div>


<div class="mod_container">
	
    
</div>
<div class="mod_hezuo mt20">
	<div class="h2style contentbox bor">
    	<h2><span><%=BasicContent.Title %></span></h2>
        <div class="contentinner">
        	<p><%=BasicContent.Content %></p>
        </div>
    </div>
</div>

<div class="footer">
	<div class="foot">
	    Copyright © 2003-2012   郑州康家贷款咨询有限公司  版权所有    <a href="http://www.miitbeian.gov.cn">豫ICP备12022184号-1</a>
	</div>
</div>
</body>
</html>

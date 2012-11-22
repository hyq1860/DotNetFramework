<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Home" %>
<%@ Import Namespace="DotNet.ContentManagement.Contract" %>
<%@ Import Namespace="DotNet.Common.Utility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>郑州康家贷款咨询有限公司</title>
<link href="201201Styles/css/commom.css" type="text/css" rel="stylesheet"/>
<link href="201201Styles/css/css.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="http://www.zzktz.com/statics/js/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=RootPath %>/Ajax/Common.ashx"></script>
<script type="text/javascript">
    $(function () {
        $("#btnApply").click(function () {
            var userName =$("#UserName").val();
            var cellPhone=$("#CellPhone").val();
            var loan =$("#Loan").val()==""?"10000":$("#Loan").val();
            var p1 = $("input:radio[name='LoanType'][checked]").val(); 
            var p2 = $("input:radio[name='Mortgage'][checked]").val(); 
            $.DotNet.Common.InsertApplyForLoan(userName,cellPhone,loan,p1,p2,function (r) {
                if(r=="1") {
                    alert("提交成功，我们会尽快跟你联系，谢谢。");
                } else {
                }
            })
        });
    })
</script>
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

<div class="bg_banner">
	<div id="banner">
		<ul class="banImgs">
		    <% foreach (ArticleInfo articleInfo in FocusArticle)%>
            <%{%>
                <li><a href="ProductListV2.aspx" class="a_bigImg"><img src="<%=articleInfo.FocusPicture %>" width="948" height="364" alt="" /></a></>
            <%}%>
		</ul>
	</div>
	<div class="jsNav"  id="jsNav">
    	<a href="javascript:void(0)" onfocus="this.blur();" class="trigger current"></a>
		<a href="javascript:void(0)" onfocus="this.blur();" class="trigger"></a>	
	</div>
</div>
<div class="mod_container">
	<div class="mod_main fl">
    	<div class="lishi h2style bor">
        	<h2><span><a href="ContentV2.aspx?id=1" target="_blank">发展历史</a></span></h2>
            <div class="lishi_fl fl">
				<a href="ContentV2.aspx?id=1" target="_blank">
                <img src="201201Styles/images/pic.jpg" alt=""/>
                </a>
            </div>
            <div class="lishi_fr fl">
            	<%--<h3>具体历史：</h3>--%>
                <a href="ContentV2.aspx?id=1" target="_blank">
                <%= BasicContent.Summary.CutString(360)%>
                </a>
            </div>
        </div>
        
        <div class="mod_product h2style bor mt20">
        	<h2>
            	<span>公司产品</span>
                <div class="mod_more"><a href="" target="_blank"><%--更多--%></a></div>
            </h2>
            <div class="list">
                
            	<ul>
            	    <% foreach (var product in Products)%>
                    <% {%>
                        <li>
            	            <a href="#<%=product.Id %>" class="link">
            	                <img  alt="#" src="<%=product.MediumPicture %>" title="<%=product.Desc %>" width="213" height="188" />
            	            </a>
                            <h3 align="center"><a id="<%=product.Id %>" href="ProductListV2.aspx" title="<%=product.Desc %>"><%=product.Title %></a></h3>
                        </li>
                    <% } %>
                    
                </ul>
            </div>
        </div>
    </div>
    <div class="mod_sub fr">
    	<div class="xszx h2style bor">
        	<h2><span>贷款咨询</span></h2>
            <ul>
                <li class="i6">手机：13721432005 /15538383238</li>
                <li class="i6">固话：0371 56770008</li>
                <li class="i4">QQ：2323768610</li>
				<li class="i5">邮箱：2323768610@QQ.com</li>
				<li class="i3">联系人：曾经理</li>
<%--                <div>&nbsp;</div>
                <li class="i6">手机：15538383238 </li>
                <li class="i6">固话：0371 56770008</li>
                <li class="i4">QQ：252933529</li>
                <li class="i5">邮箱：252933529@QQ.com</li>
				<li class="i3">联系人：李经理</li>--%>
			</ul>
         </div>
         
         <div class="mod_form h2style bor mt20">
         	<h2><span>贷款申请</span></h2>
            <table>
            	<tr>
                	<td>贷款类型：</td>
                    <td>
                    	<input name="LoanType" checked="checked" type="radio" value="个人贷款" />个人贷款
                        <input name="LoanType" type="radio" value="企业贷款" />企业贷款
                    </td>
                </tr>
                <tr>
                	<td>有无抵押：</td>
                    <td>
                        <input name="Mortgage" checked="checked" type="radio" value="有抵押" />有抵押
                        <input name="Mortgage" type="radio" value="无抵押" />无抵押
                    </td>
                </tr>
                <tr>
                	<td>姓名：</td>
                    <td>
                    	<input id="UserName" name="UserName" type="text" value="" class="text"/>
                    </td>
                </tr>
                <tr>
                	<td>贷款金额：</td>
                    <td>
                    	<input id="Loan" name="Loan" type="text"  class="text"/>元
                    </td>
                </tr>
                <tr>
                	<td>联系电话：</td>
                    <td>
                    	<input id="CellPhone" style="width: 120px;" name="CellPhone" type="text"  class="text"/>
                    </td>
                </tr>
                <tr>
                	<td></td>
                    <td><input id="btnApply" name="btnApply" type="button" value="贷款申请" /></td>
                </tr>
            </table>
         </div>
    
    </div>
</div>
<div class="mod_hezuo mt20">
	<div class="h2style hezuo bor">
    	<h2><span>合作伙伴</span></h2>
        <ul>
        	<li>
        	    <a href="http://www.icbc.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95588.png"></a>
            </li>
            <li>
                <a href="http://www.ccb.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95533.png"></a>
            </li>
            <li>
                <a href="http://www.boc.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95566.png"></a>
            </li>
            <li>
                <a href="http://www.bankcomm.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95559.png"></a>
            </li>
            <li>
                <a href="http://www.abchina.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95599.png"></a>
            </li>
            <li>
                <a href="http://www.cmbchina.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95555.png"></a>
            </li>
            <li>
                <a href="http://www.psbc.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95580.png"></a>
            </li>
            <li>
                <a href="http://www.cebbank.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95595.png"></a>
            </li>
            <li>
                <a href="http://www.cmbc.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95568.png"></a>
            </li>
            <li>
                <a href="http://bank.pingan.com/" target="_blank"><img src="http://www.hao123.com/erji/hao123/webroot/resource/erji/public/images/pinganyinhang.jpg"></a>
            </li>
            <li>
                <a href="http://www.spdb.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95528.png"></a>
            </li>
            <li>
                 <a href="http://bank.ecitic.com/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95558.png"></a>
            </li>
            <li>
                <a href="http://www.cib.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95561.png"></a>
            </li>
            <li>
                <a href="http://www.hxb.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95577.png"></a>
            </li>
            <li>
                <a href="http://www.cgbchina.com.cn/" target="_blank"><img src="http://www.hao123.com/resource/erji/public/images/erji/95508.png"></a>
            </li>
        </ul>
    </div>
</div>
<div class="footer">
	<div class="foot">
	    Copyright © 2003-2012   郑州康家贷款咨询有限公司  版权所有    <a href="http://www.miitbeian.gov.cn">豫ICP备12022184号-1</a>
	</div>
</div>
<script type="text/javascript">
    $(function () {
        (function () {
            var curr = 0;
            var imglen = $(".banImgs li").length;
            $("#jsNav .trigger").each(function (i) {
                $(this).click(function () {
                    this.blur();
                    curr = i;
                    $(".banImgs li").eq(i).css("z-index", "10").siblings("li").css("z-index", "1");
                    $(".banImgs li").eq(i).fadeIn(1000).siblings("li").fadeOut(1000);
                    $(this).siblings(".trigger").removeClass("current").end().addClass("current");
                    return false;
                });
            });

            var pg = function (flag) {
                //flag:true表示前翻， false表示后翻
                if (flag) {
                    if (curr == 0) {
                        todo = (imglen - 1);
                    } else {
                        todo = (curr - 1) % imglen;
                    }
                } else {

                    todo = (curr + 1) % imglen;
                }
                $("#jsNav .trigger").eq(todo).click();
            };

            //前翻
            $(".banPrev").click(function () {
                pg(true);
                return false;
            });

            //后翻
            $(".banNext").click(function () {
                pg(false);
                return false;
            });

            //自动翻
            var timer = setInterval(function () {
                todo = (curr + 1) % imglen;
                $("#jsNav .trigger").eq(todo).click();
            }, 3000);

            //鼠标悬停在触发器上时停止自动翻
            $("#jsNav a,.banPrev,.banNext").hover(function () {
                clearInterval(timer);
            },
                function () {
                    timer = setInterval(function () {
                        todo = (curr + 1) % imglen;
                        $("#jsNav .trigger").eq(todo).click();
                    }, 8000);
                }
            );
        })();
    });
</script>
</body>
</html>


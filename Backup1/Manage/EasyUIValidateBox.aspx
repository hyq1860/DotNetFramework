<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasyUIValidateBox.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.EasyUIValidateBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/jQuery/jquery.metadata.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/jquery.validate.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jquery-validation/messages_cn.js"></script>
  <script type="text/javascript">
      // only for demo purposes
      $.validator.setDefaults({
          submitHandler: function () {
              alert("submitted!");
          }
      });

      $.metadata.setType("attr", "validate");

      $(document).ready(function () {
          $("#simple").validate();
          $("#form1").validate();
          $("#selecttest").validate();
          initProductCategory();
      });
      
      function initProductCategory() {
          $('#productCategory').combotree({
          //checkbox: true,
              url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory',
              onSelect: function(node, data) {
                  //返回树对象  
                  var tree = $(this).tree;
                  //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
                  var isLeaf = tree('isLeaf', node.target);
                  if (!isLeaf) {
                      //清除选中  
                      $('#productCategory').combotree('clear');
                  }
              },
              onLoadSuccess: function(node, data) {
              }
          });
        }
</script>

<style type="text/css">
.block { display: block; }
form.cmxform label.error { display: none; }	
</style>

</head>
<body>


<div id="main">
<form id="simple" >
    <input type="text" name="v1" validate="required:true"/><br/>
    <input type="text" name="v2" validate="required:true,minlength:2"/><br/>
    <input type="text" name="v3" validate="required:true,minlength:2" title="必填且最小长度为2"/><br/>
    
    <select id="fruit" name="v4" title="至少选择两个" validate="required:true, minlength:2" multiple="multiple">
		<option value="b">Banana</option>
		<option value="a">Apple</option>
		<option value="p">Peach</option>
		<option value="t">Turtle</option>
	</select> <br/>
    <input  name="v5" value="0" max="25" readonly="readonly" size='4' /><br/>
    <input  name="v6"  max="25" size='4' title="最大值为25" validate="required:true" /><br/>
    <select id="productCategory" class="productCategory" name="v7" validate="required:true"></select><br/>
    <input type="submit" value="提交"/>
</form>    


</div>


</body>
</html>
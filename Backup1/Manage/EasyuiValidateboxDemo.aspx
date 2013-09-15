<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasyuiValidateboxDemo.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.EasyuiValidateboxDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    	<script type="text/javascript" src="<%=RootPath %>/jQuery/jquery-1.7.2.min.js"></script>

    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/jquery.easyui.validatebox.extension.js"></script>
    
    <link id="easyuiTheme" type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/gray/easyui.css"/>
    <link type="text/css" rel="stylesheet" href="<%=RootPath %>/jQueryEasyUI/themes/icon.css" />
    <script type="text/javascript" src="<%=RootPath %>/jQueryEasyUI/locale/easyui-lang-zh_CN.js"></script>
      <script type="text/javascript">
          // only for demo purposes




          $(document).ready(function () {
              initProductCategory();

              // 提交 form   
              $("#form1").form('validate');

          });

          function initProductCategory() {
              $('#productCategory').combotree({
                  //checkbox: true,
                  url: '<%=RootPath %>/Ajax/Common.ashx/GetComboTreeCategory',
                  onSelect: function (node, data) {
                      //返回树对象  
                      var tree = $(this).tree;
                      //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
                      var isLeaf = tree('isLeaf', node.target);
                      if (!isLeaf) {
                          //清除选中  
                          $('#productCategory').combotree('clear');
                      }
                  },
                  onLoadSuccess: function (node, data) {
                      if (data[0] && data[0].id) {
                          $('#productCategory').combotree('setValue', data[0].id);
                      }
                  }
              });
          }
          function test() {
              $('#form1').form('submit', {
                  url: "http://localhost:17972/Manage/EasyuiValidateboxDemo.aspx",
                  onSubmit: function () {
                      return $(this).form('validate');  
                  },
                  success: function (data) {
                      alert(data);
                  }
              }); 
          }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td></td>
            <td><ul id="productCategory" class="easyui-validatebox" required="true"></ul></td>
        </tr>
        <tr>
            <td>远程验证：</td>
            <td>
                <input type="text" name="v10" class="easyui-validatebox" validtype="remote['<%=RootPath %>/Ajax/Validate.ashx/ValidateName','name']" invalidMessage="用户名已存在"/>
            </td>
        </tr>
        <tr>
            <td>网址验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="url" invalidMessage="url格式不正确[http://www.example.com]" /></td>
        </tr>
        <tr>
            <td>长度验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="length[8,20]" invalidMessage="有效长度8-20" /><br /></td>
        </tr>
        <tr>
            <td>手机验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="mobile"  /><br /></td>
        </tr>
        <tr>
            <td>邮编验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="ZIP" /><br /></td>
        </tr>
        <tr>
            <td>账号验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="account[8,20]" /><br /></td>
        </tr>
        <tr>
            <td>汉字验证：</td>
            <td><input type="text" class="easyui-validatebox" validtype="CHS" /><br /></td>
        </tr>
        <tr>
            <td></td>
            <td><input type="button" value="提交" onclick="test()"/></td>
        </tr>
    </table>
    
    
    </form>
</body>
</html>

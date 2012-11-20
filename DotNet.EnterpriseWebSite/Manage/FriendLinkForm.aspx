<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLinkForm.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.FriendLinkForm" %>


<form id="FriendLinkForm" method="post">
    <table>
		<tr>
        	<td align="right"><label></label></td>
        	<td><input name="Id" class="easyui-validatebox" required="True"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>网站名称：</label></td>
        	<td><input name="Name" class="easyui-validatebox" required="True"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>友情链接类型：</label></td>
        	<td><input name="Type" class="easyui-validatebox" required="True"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>描述：</label></td>
        	<td><input name="Description" class="easyui-validatebox" required="False"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>链接地址</label></td>
        	<td><input name="Url" class="easyui-validatebox" required="True"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>Logo地址</label></td>
        	<td><input name="Logo" class="easyui-validatebox" required="True"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>排序：</label></td>
        	<td><input name="DisplayOrder" class="easyui-validatebox" required="False"></td>
        	<td></td>
    	</tr>
		<tr>
        	<td align="right"><label>状态：</label></td>
        	<td><input name="Status" class="easyui-validatebox" required="False"></td>
        	<td></td>
    	</tr>
    </table>
</form>

<script type="text/javascript">

</script>


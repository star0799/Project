<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="site_config.aspx.vb" inherits="deskmanager_site_config" validaterequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<script language="JavaScript" type="text/javascript" src="../js/check.js"></script>
	<script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
	<script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>
	<script language="javascript" type="text/javascript" src="../js/editor.js"></script>	
	<script language="javascript" type="text/javascript">
	<!--
	function checkForm(){
		var x, y, o, flag, check, mode;		
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSave").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSave', '');
		return true;
	}
	//-->
	</script>
	<table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
	  <tr>
		<td align="left" valign="top" class="MainText01" style="padding-right:3px;"><table width="100%" border="0" cellspacing="1" cellpadding="0">
		  <tr>
		    <td width="20%" align="left" valign="middle" class="Title03">登入頁</td>
			<td width="80%" align="left" valign="top">&nbsp;</td>
		  </tr>
		  <tr>
			<td align="center" valign="middle" class="Title01">視窗標題文字</td>
			<td align="left" valign="top" class="List06"><input name="loginTitle" type="text" class="Forms04" id="loginTitle" size="50" runat="server" /></td>
		  </tr>
		  <tr>
		    <td align="center" valign="middle" class="Title01">公司名稱</td>
		    <td align="left" valign="top" class="List06"><input name="compName" type="text" class="Forms04" id="compName" size="50" runat="server" /></td>
	      </tr>
		  <tr>
		    <td align="left" valign="middle" class="Title03">後台管理內頁</td>
			<td align="left" valign="top">&nbsp;</td>
		  </tr>
		  <tr>
			<td align="center" valign="middle" class="Title01">視窗標題文字</td>
			<td align="left" valign="top" class="List06"><input name="pageTitle" type="text" class="Forms04" id="pageTitle" size="50" runat="server" /></td>
		  </tr>
		</table></td>
	  </tr>
	  <tr>
		<td style="font-size:1px;  height:5px;"></td>
	  </tr>
	  <tr>
		<td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="確定" runat="server" onclick="return checkForm();" /></td>
	  </tr>
	</table>
</asp:Content>
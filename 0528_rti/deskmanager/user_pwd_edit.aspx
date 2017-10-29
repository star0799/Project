<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="user_pwd_edit.aspx.vb" inherits="deskmanager_user_pwd_edit" %>

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
		
		// pwd
		o = document.getElementById(prefixId + "pwd");
		if (StripSpaces(o.value) == "") {
			alert("請輸入原密碼");
			o.focus();
			return false;
		}
	
		// new pwd
		o = document.getElementById(prefixId + "npwd");
		if (StripSpaces(o.value) == "") {
			alert("請輸入新密碼");
			o.focus();
			return false;
		}
		/*if (!IsLetterAndNumber(o.value)) {
			alert("新密碼需為英文字母或數字");
			o.focus();
			return false;
		}*/
		if (o.value.length < 6 || o.value.length > 20) {
			alert("新密碼長度需為 6 ~ 20 碼");
			o.focus();
			return false;
		}
		
		// new pwd confirm
		o = document.getElementById(prefixId + "npwd2");
		if (StripSpaces(o.value) == "") {
			alert("請輸入新密碼確認");
			o.focus();
			return false;
		}
		
		// confirm new pwd
		if (document.getElementById(prefixId + "npwd").value != document.getElementById(prefixId + "npwd2").value) {
			alert("新密碼與新密碼確認輸入不同");
			document.getElementById(prefixId + "npwd").focus();
			return false;
		}

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
					<td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">帳號</td>
					<td width="80%" align="left" valign="middle" class="List06"><%=userId%></td>
				  </tr>
				  <tr>
					<td align="center" valign="middle" nowrap="nowrap" class="Title01">使用者姓名</td>
					<td align="left" valign="middle" class="List06"><%=userName%></td>
				  </tr>
				  <tr>
					<td align="center" valign="middle" nowrap="nowrap" class="Title01">原密碼</td>
					<td width="80%" align="left" valign="middle" class="List06"><span class="BList01">
					  <input name="pwd" type="password" class="Forms04" id="pwd" size="20" runat="server" />
					</span></td>
				  </tr>
				  <tr>
					<td align="center" valign="middle" nowrap="nowrap" class="Title01">新密碼</td>
					<td align="left" valign="top" class="List06"><span class="BList01">
					  <input name="npwd" type="password" class="Forms04" id="npwd" size="20" runat="server" />
					</span></td>
				  </tr>
				  <tr>
					<td align="center" valign="middle" nowrap="nowrap" class="Title01">新密碼確認</td>
					<td align="left" valign="top" class="List06"><span class="BList01">
					  <input name="npwd2" type="password" class="Forms04" id="npwd2" size="20" runat="server" />
					</span></td>
				  </tr>
				</table></td>
			  </tr>
			  <tr>
				<td style="font-size:1px;  height:5px;"></td>
			  </tr>
			  <tr>
				<td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="修改" runat="server" onclick="return checkForm();" /></td>
			  </tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
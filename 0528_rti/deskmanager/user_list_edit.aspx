<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="user_list_edit.aspx.vb" inherits="deskmanager_user_list_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<script language="JavaScript" type="text/javascript" src="../js/check.js"></script>
	<script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
	<script language="javascript" type="text/javascript">
	<!--							
	function checkForm(){
		var x, y, o, flag, check, mode;
		var prefixId = "master_ContentPlaceHolder1_";			

		// group
		o = document.getElementById(prefixId + "group");
		if (o.value == "") {
			alert("請選擇群組");
			o.focus();
			return false;
		}
		
		// user id
		o = document.getElementById(prefixId + "userId");
		if (StripSpaces(o.value) == "") {
			alert("請輸入帳號");
			o.focus();
			return false;
		}
		if (!IsLetterAndNumber(o.value)) {
			alert("帳號需為英文字母或數字");
			o.focus();
			return false;
		}
		
		// pwd
		o = document.getElementById(prefixId + "pwd");
		if (StripSpaces(o.value) == "") {
			alert("請輸入密碼");
			o.focus();
			return false;
		}
		/*if (!IsLetterAndNumber(o.value)) {
			alert("密碼需為英文字母或數字");
			o.focus();
			return false;
		}*/
		if (o.value.length < 6 || o.value.length > 20) {
			alert("密碼長度需為 6 ~ 20 碼");
			o.focus();
			return false;
		}
		
		// user name
		o = document.getElementById(prefixId + "userName");
		if (StripSpaces(o.value) == "") {
			alert("請輸入使用者姓名");
			o.focus();
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
                    <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">*所屬群組</td>
                    <td width="80%" align="left" valign="top" class="List06"><select name="group" class="Forms03" id="group" runat="server">
                        <option>總管理者</option>
                      </select>                    </td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle" nowrap="nowrap" class="Title01">*帳號</td>
                    <td align="left" valign="top" class="List06"><input name="userId" type="text" class="Forms04" id="userId" size="30" runat="server" /></td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle" nowrap="nowrap" class="Title01">*密碼</td>
                    <td align="left" valign="top" class="List06"><input name="pwd" type="text" class="Forms04" id="pwd" size="30" runat="server" /></td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle" nowrap="nowrap" class="Title01">*使用者姓名</td>
                    <td align="left" valign="top" class="List06"><input name="userName" type="text" class="Forms04" id="userName" size="30" runat="server" /></td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle" nowrap="nowrap" class="Title01">部門</td>
                    <td align="left" valign="top" class="List06"><input name="dept" type="text" class="Forms04" id="dept" size="50" runat="server" /></td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle" nowrap="nowrap" class="Title01">電話</td>
                    <td align="left" valign="top" class="List06"><input name="phone" type="text" class="Forms04" id="phone" size="30" runat="server" /></td>
                  </tr>
                </table></td>
			  </tr>
			  <tr>
				<td style="font-size:1px;  height:5px;"></td>
			  </tr>
			  <tr>
				<td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="修改" onclick="return checkForm();" runat="server" /></td>
			  </tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
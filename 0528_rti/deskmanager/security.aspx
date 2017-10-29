<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="security.aspx.vb" inherits="security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<script language="JavaScript" type="text/javascript" src="../js/check.js"></script>
	<script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
	<script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>
	<script language="javascript" type="text/javascript" src="../js/editor.js"></script>
	<script language="javascript" type="text/javascript">
	function checkForm(){
		if (typeof(Page_ClientValidate) == 'function') {
			if (Page_ClientValidate() == false) {
				Page_BlockSubmit = false;
				return false;
			}
		}
		
	}
	$(function() {
		
	});					
	</script>
	
    
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
			  <tr>
				<td align="left" valign="top" class="MainText01" style="padding-right:3px;"><table width="100%" border="0" cellspacing="1" cellpadding="0">
				  <tr>
					<td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">允許登入IP</td>
					<td width="80%" align="left" valign="middle" class="List06">
                    符告下列IP位址之使用者才允許登入系統<br />
                    <textarea name="ip_mask" cols="40" rows="5" id="ip_mask" runat="server"></textarea>
				    <br />
				    如無輸入則不會限制使用者登入IP　(<a href="docs/系統安全性設定.pdf" target="_blank">設定範例</a>)</td>
				  </tr>
				</table></td>
			  </tr>
			  <tr>
				<td style="font-size:1px;  height:5px;"></td>
			  </tr>
			  <tr>
				<td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><asp:Button ID="btnSave" runat="server" Text="儲存" CssClass="Forms02" onclientclick="return checkForm();" /></td>
			  </tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
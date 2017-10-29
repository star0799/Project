<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="siteflow_set.aspx.vb" inherits="deskmanager_siteflow_set" validaterequest="false" %>

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
	function openUrl(){
		window.open(document.getElementById('<%=siteflowLink.ClientID%>').value);
	}
	//-->						
	</script>
	<table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
	  <tr>
		<td align="left" valign="top" class="MainText01" style="padding-right:3px;"><table width="100%" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">流量程式碼</td>
            <td width="80%" align="left" valign="top" class="List06"><textarea name="siteflowCode" cols="50" rows="7" id="siteflowCode" runat="server"></textarea></td>
          </tr>
          <tr>
            <td align="center" valign="middle" nowrap="nowrap" class="Title01">流量分析頁面路徑</td>
            <td align="left" valign="top" class="List06"><input name="none" type="text" id="none" style="display: none;" />
                <input name="siteflowLink" type="text" class="Forms04" id="siteflowLink" size="50" runat="server" />
                <input name="btnLink" type="button" class="Forms02" id="btnLink" value="開啟" onclick="openUrl();" />
                </td>
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
<%@ Page Language="VB" masterpagefile="~/deskmanager/window.master" AutoEventWireup="false" CodeFile="flash_upload.aspx.vb" Inherits="deskmanager_flash_upload" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="javascript" type="text/javascript">
	<!--							
	function checkForm(){	
		var x, y, o, o2, flag, check, mode, suffix;
		var prefixId = "master_ContentPlaceHolder1_";
	
		// file (required)
		o = document.getElementById("h_fileName");
		if (o != null) {
			check = (o.value == "");
		} else {
			check = true;
		}
		
		o = document.getElementById(prefixId + "fileName");
		if (check && StripSpaces(o.value) == "") {
			alert("請上傳檔案");
			o.focus();
			return false;
		}
		if (o.value != "") {
			ext = o.value.substring(o.value.lastIndexOf(".") + 1).toLowerCase();
			if (ext != "swf") {
				alert("檔案格式需為 swf");
				o.focus();
				return false;
			}
		}
		
		document.getElementById(prefixId + "btnSave").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSave', '');
		return true;
	}
	//-->						
	</script>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
    <tr>
      <td align="left" valign="top" class="MainText01" style="padding-right: 3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">檔案上傳</td>
                  <td width="80%" align="left" valign="top" class="List06"><span id="fileHref" runat="server"></span>
                    <input name="fileName" type="file" id="fileName" size="35" runat="server" class="Forms04" /></td>
                </tr>
              </table></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td style="font-size: 1px; height: 5px;"></td>
    </tr>
    <tr>
      <td align="center" valign="middle" class="MainText01" style="padding-right: 3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="確定上傳" runat="server" onClick="return checkForm();" />
        <input name="btnClose" type="button" class="Forms02" id="btnClose" value="關閉視窗" onClick="window.close();" /></td>
    </tr>
  </table>
</asp:Content>

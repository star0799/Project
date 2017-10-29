<%@ page language="VB" masterpagefile="~/deskmanager/window.master" autoeventwireup="false" CodeFile="img_upload.aspx.vb" inherits="deskmanager_img_upload" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="javascript" type="text/javascript">
	<!--							
	function checkForm(){	
		var x, y, o, o2, flag, check, mode, suffix;
		var prefixId = "master_ContentPlaceHolder1_";
	
		// image (required)
		o = document.getElementById("imgNameThumb");
		if (o != null) {
			check = (o.value == "");
		} else {
			check = true;
		}

		o = document.getElementById(prefixId + "imgName");
		if (check && StripSpaces(o.value) == "") {
			alert("請上傳圖檔");
			o.focus();
			return false;
		}
		if (o.value != "") {
			var ext = o.value.substring(o.value.lastIndexOf(".") + 1).toLowerCase();
			if (ext != "gif" && ext != "jpg" && ext != "jpeg" && ext != "png") {
				alert("圖檔格式需為 gif, jpg, jpeg, png");
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
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">圖檔上傳</td>
                  <td width="80%" align="left" valign="top" class="List06" id="tdImgName" runat="server"><span id="imgHref" runat="server"></span>
                    <input name="imgName" type="file" id="imgName" size="35" runat="server" class="Forms04" /></td>
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

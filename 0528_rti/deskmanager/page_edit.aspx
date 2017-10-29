<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="page_edit.aspx.vb" inherits="deskmanager_page_edit" validaterequest="false" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>

<%@ Reference Control="controls/module_control.ascx" %>
<%@ Register src="controls/module_control.ascx" tagname="module_control" tagprefix="uc" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
	<script language="JavaScript" type="text/javascript" src="../js/check.js"></script>
	<script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
	<script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>
	<script language="javascript" type="text/javascript" src="../js/editor.js"></script>
    <script language="JavaScript" type="text/javascript" src="../js/confirm.js"></script>
	<script language="javascript" type="text/javascript">
	<!--							
	function checkForm(){
		var x, y, o, o2, flag, check, mode, suffix;
		var prefixId = "master_ContentPlaceHolder1_";
		
		// image
		for (x = 1; x <= 1; x++) {
			suffix = (x == 1 ? "" : x);
			o = document.getElementById(prefixId + "imgName" + suffix);		
			if (o.value != "") {
				ext = o.value.substring(o.value.lastIndexOf(".") + 1).toLowerCase();
				if (ext != "gif" && ext != "jpg" && ext != "jpeg" && ext != "png") {
					alert("圖檔格式需為 gif, jpg, png");
					o.focus();
					return false;
				}
			}
			
			o = document.getElementById("imgNameThumb" + suffix);
			o2 = document.getElementById(prefixId + "imgName" + suffix);
			if (o != null) {
				check = (o.value != "" || o2.value != "");
			} else {
				check = (o2.value != "");
			}
	
			o = document.getElementById(prefixId + "imgDesc" + suffix);
			if (o && check && StripSpaces(o.value) == "") {
				alert("請輸入圖片說明");
				o.focus();
				return false;
			}
		}
		
		// file
		for (x = 1; x <= 1; x++) {

			suffix = (x == 1 ? "" : x);
			
			o = document.getElementById("h_fileName" + suffix);
			o2 = document.getElementById(prefixId + "fileName" + suffix);
			if (o != null) {
				check = (o.value != "" || o2.value != "");
			} else {
				check = (o2.value != "");
			}
	
			o = document.getElementById(prefixId + "fileDesc" + suffix);
			if (check && StripSpaces(o.value) == "") {
				alert("請輸入檔案說明");
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
      <td align="left" valign="top"><uc:module_control ID="module_control1" runat="server" /></td>
    </tr>
    <tr>
      <td align="left" valign="top" class="List02" style="display:none">頁面
        <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="True"> </asp:DropDownList>
          <input name="btnCat" type="button" class="Forms02" id="btnCat" value="管理" onserverclick="btnCat_ServerClick" runat="server" />      </td>
    </tr>
    <tr>
      <td align="left" valign="top" class="MainText01" style="padding-right:3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">內容</td>
              <td width="80%" align="left" valign="top" class="List06"><input name="btnEditor" type="button" class="Forms02" id="btnEditor" value="Html編輯器" onclick="toggleEditor('<%=content.ClientID%>');" />
                <br />
                <textarea name="content" cols="55" rows="5" id="content" class="EditTxtbox" runat="server"></textarea></td>
            </tr>
            <tr style="display:none">
              <td align="center" valign="middle" nowrap="nowrap" class="Title01">圖片</td>
              <td align="left" valign="top" class="List06"><table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                  <td align="center" valign="middle" nowrap="nowrap" class="Title01">圖片上傳</td>
                  <td align="left" valign="top" class="List06" id="tdImgName" runat="server"><span id="imgHref" runat="server"></span>
                    <input name="imgName" type="file" id="imgName" size="40" runat="server" class="Forms04" /></td>
                  </tr>
                <tr>
                  <td align="center" valign="middle" nowrap="nowrap" class="Title01">圖片說明</td>
                  <td align="left" valign="top" class="List06"><input name="imgDesc" type="text" class="Forms04" id="imgDesc" size="50" runat="server" /></td>
                  </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">*圖片位置</td>
                  <td width="80%" align="left" valign="top" class="List06"><select name="imgAlign" id="imgAlign" runat="server" class="Forms03">
                    <option value="left" selected="selected">左</option>
                    <option value="center">中</option>
                    <option value="right">右</option>
                    </select></td>
                  </tr>
                </table></td>
              </tr>
            <tr>
              <td align="center" valign="middle" nowrap="nowrap" class="Title01">相關連結</td>
              <td align="left" valign="top" class="List06"><input name="linkHref" type="text" class="Forms04" id="linkHref" size="50" runat="server" /></td>
              </tr>
            <tr>
              <td align="center" valign="middle" nowrap="nowrap" class="Title01">檔案上傳</td>
              <td align="left" valign="top" class="List06"><table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">檔案上傳</td>
                  <td width="80%" align="left" valign="top" class="List06"><span id="fileHref" runat="server"></span>
                    <input name="fileName" type="file" id="fileName" size="40" runat="server" class="Forms04" /></td>
                  </tr>
                <tr>
                  <td align="center" valign="middle" nowrap="nowrap" class="Title01">檔案說明</td>
                  <td align="left" valign="top" class="List06"><input name="fileDesc" type="text" class="Forms04" id="fileDesc" size="50" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
        </tr>
      </table>      </td>
    </tr>
    <tr>
      <td style="font-size:1px;  height:5px;"></td>
    </tr>
    <tr>
      <td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="確定" runat="server" onclick="return checkForm();">      </td>
    </tr>
  </table>
</asp:Content>
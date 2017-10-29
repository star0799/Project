<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="home_layout_edit.aspx.vb" inherits="deskmanager_home_layout_edit" validaterequest="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/editor.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/confirm.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/window.js"></script> 
  <script language="javascript" type="text/javascript">
	<!--							
	function checkForm(){
		var x, y, o, flag, check, mode;
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSave").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSave', '');
		return true;
	}
	function doOpenEditor(o, ctlName) {
		var no = o.id.substring(o.id.lastIndexOf("_") + 1);
		openEditor(o, ctlName + "_" + no);
	}
	//-->						
	</script>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
    <tr>
      <td align="left" valign="top" class="MainText01" style="padding-right:3px;"><table width="100%" border="0" cellspacing="1" cellpadding="0">
          <tr>
            <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">關於央廣</td>
            <td align="left" valign="middle" class="List06"><input name="btnEditor" type="button" class="Forms02" id="btnEditor" value="Html編輯器"
                                onclick="toggleEditor('<%=intro.ClientID%>');" />
              <br />
            <textarea name="intro" cols="55" rows="5" id="intro" runat="server"></textarea></td>
          </tr>
          <tr>
            <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">關於央廣<br />
            連結路徑</td>
            <td width="80%" align="left" valign="middle" class="List06"><input name="none" type="text" id="none" style="display: none;" />
              <input name="linkHref" type="text" class="Forms04" id="linkHref" size="50" runat="server" /></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td style="font-size:1px;  height:5px;"></td>
    </tr>
    <tr>
      <td align="center" valign="middle" class="MainText01" style="padding-right:3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="修改" runat="server" onclick="return checkForm();"></td>
    </tr>
  </table>
  <script language="JavaScript" type="text/javascript" src="../js/dyn_edit_m.js"></script> 
  <script language="JavaScript" type="text/javascript">
	<!--
	var objDynEdit = new Array();
	objDynEdit[0] = new DynEdit();
	objDynEdit[0].aryHidden = new Array("AdItemId", "imgNameThumb");
	document.getElementById("master_ContentPlaceHolder1_tblBack").style.display = "none";
	objDynEdit[0].renewTableElementsId();
	
	objDynEdit[1] = new DynEdit(2);
	objDynEdit[1].aryHidden = new Array("BotAdItemId", "imgNameThumb2");
	document.getElementById("master_ContentPlaceHolder1_tblBack2").style.display = "none";
	objDynEdit[1].renewTableElementsId();
	
	objDynEdit[2] = new DynEdit(3);
	objDynEdit[2].aryHidden = new Array("MarkItemId", "imgNameThumb3");
	document.getElementById("master_ContentPlaceHolder1_tblBack3").style.display = "none";
	objDynEdit[2].renewTableElementsId();
	
	objDynEdit[3] = new DynEdit(4);
	objDynEdit[3].aryHidden = new Array("MarkRItemId", "imgNameThumb4");
	document.getElementById("master_ContentPlaceHolder1_tblBack4").style.display = "none";
	objDynEdit[3].renewTableElementsId();
	//-->
	</script> 
</asp:Content>

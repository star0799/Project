<%@ Page Language="VB" masterpagefile="~/deskmanager/window.master" AutoEventWireup="false" CodeFile="html_editor.aspx.vb" Inherits="deskmanager_html_editor" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
<script language="javascript" type="text/javascript">
<!--
__gSpecificEditor = true;
//-->
</script>
<script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script>
<script language="javascript" type="text/javascript" src="../js/editor.js"></script>
<script language="JavaScript" type="text/javascript" src="../js/jquery.js"></script>

  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td align="center" valign="top"><textarea name="content" id="content" style="width:100%; height:100%; border: 1px solid #6779AA" mce_editable="true"></textarea></td>
    </tr>
    <tr>
      <td height="50" align="center" valign="middle"><input type="button" name="btnSubmit" value="送 出" onClick="Restore_Txt();">
          <input name="btnCancel" type="button" id="btnCancel" value="取 消" onClick="window.close();"></td>
    </tr>
  </table>
<script language="javascript" type="text/javascript">
Receive_Txt();
</script>
</asp:Content>
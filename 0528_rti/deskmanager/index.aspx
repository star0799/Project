<%@ page language="VB" autoeventwireup="false" CodeFile="index.aspx.vb" inherits="deskmanager_index" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>後台管理系統</title>
<link href="web.css" rel="stylesheet" type="text/css" />
<script language="javascript">

function fixPNG(myImage) {     var arVersion = navigator.appVersion.split("MSIE");     var version = parseFloat(arVersion[1]);      if ((version >= 5.5) && (version < 7) && (document.body.filters))     {         var imgID = (myImage.id) ? "id='" + myImage.id + "' " : "";         var imgClass = (myImage.className) ? "class='" + myImage.className + "' " : "";         var imgTitle = (myImage.title) ? "title='" + myImage.title  + "' " : "title='" + myImage.alt + "' ";         var imgStyle = "display:inline-block;" + myImage.style.cssText;         var strNewHTML = "<span " + imgID + imgClass + imgTitle             + " style=\"" + "width:" + myImage.width             + "px; height:" + myImage.height             + "px;" + imgStyle + ";"             + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"             + "(src=\'" + myImage.src + "\', sizingMethod='scale');\"></span>";         myImage.outerHTML = strNewHTML;     } } 
    
</script>
<script language="javascript" type="text/javascript" src="../js/check.js"></script>
<script language="javascript" type="text/javascript">
<!--
function initFocus(){
	var x, y, o, flag, check, mode;
	var prefixId = "";
	
	if (document.getElementById("userId").value.length == 0) {
		document.getElementById("userId").focus();
	} else {
		document.getElementById("pwd").focus();
	}
}
function checkForm() {
	var x, y, o, flag, check, mode;
	var prefixId = "";
			
	o = document.getElementById("userId");
	if (StripSpaces(o.value) == "") {
		alert("請輸入帳號");
		o.focus();
		return false;
	}
	
	o = document.getElementById("pwd");
	if (StripSpaces(o.value) == "") {
		alert("請輸入密碼");
		o.focus();
		return false;
	}
	
	document.getElementById("btnLogin").disabled = true;
	__doPostBack('btnLogin', '');
	return true;
}
//-->
</script>
<style type="text/css">
<!--
body {
	margin-top: 0px;
}
-->
</style>
</head>

<body class="LoBg" onLoad="initFocus();">
<form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server" > </asp:ScriptManager>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td align="center" valign="top" class="LoginMainBg"><table border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="63"  class="LoTopMidCenter"><%=compName%></td>
          </tr>
          <tr>
            <td height="270" align="left" valign="top" class="LoMain"><table border="0" cellspacing="3" cellpadding="0">
                <tr>
                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td align="left" valign="middle" class="LoText01" nowrap="true">管理帳號：</td>
                        <td align="left" valign="middle" class="LoText01"><input name="userId" type="text" class="FormBar01" id="userId" onKeyPress="if (event.keyCode == 13) {this.form.btnLogin.focus(); this.form.btnLogin.click();}" runat="server" ></td>
                      </tr>
                      <tr>
                        <td align="left" valign="middle" class="LoText01">密碼：</td>
                        <td align="left" valign="middle" class="LoText01"><input name="pwd" type="password" class="FormBar01" id="pwd" onKeyPress="if (event.keyCode == 13) {this.form.btnLogin.focus(); this.form.btnLogin.click();}" runat="server"></td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td height="30" align="left" valign="bottom">&nbsp;</td>
                      </tr>
                    </table></td>
                  <td><input name="btnLogin" type="button" class="FormBut02" id="btnLogin" value="確定登入" runat="server" onClick="return checkForm();"></td>
                </tr>
              </table></td>
          </tr>
        </table></td>
    </tr>
  </table>
  <asp:PlaceHolder ID="placeHolder" runat="server" />
</form>
</body>
</html>
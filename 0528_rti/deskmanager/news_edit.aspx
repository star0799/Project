<%@ Page Language="VB" MasterPageFile="~/deskmanager/back_frame.master" AutoEventWireup="false"
    CodeFile="news_edit.aspx.vb" Inherits="deskmanager_news_edit" ValidateRequest="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/editor.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/confirm.js"></script> 
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
		$("#<%=date_s.ClientID%>, #<%=date_e.ClientID%>").datepicker();
		$("#<%=pdate.ClientID%>").datepicker();
	});
    </script>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
    <tr>
      <td align="left" valign="top" class="MainText01" style="padding-right: 3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><table width="100%" border="0" cellspacing="1" cellpadding="0">
                <tr style="display:none">
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> *類別</td>
                  <td align="left" valign="middle" class="List06"><select name="cat" id="cat" runat="server" class="Forms03">
                      <option value="" selected="selected">無類別</option>
                    </select></td>
                </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> *發布日期</td>
                  <td width="80%" align="left" valign="middle" class="List06"><input type="text" name="pdate" id="pdate" runat="server" value="" size="7" readonly="readonly" tabindex="30" class="Forms04" /></td>
                </tr>
                <tr id="trShowDate" runat="server">
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 顯示日期</td>
                  <td align="left" valign="middle" class="List06"><input type="text" name="date_s" id="date_s" runat="server" value="" size="7" readonly="readonly" tabindex="30" class="Forms04" />
                    <img src="images/erase.gif" onclick="$('#<%=date_s.ClientID%>').val('');" /> ~
                    <input type="text" name="date_e" id="date_e" runat="server" value="" size="7" readonly="readonly" tabindex="30" class="Forms04" />
                    <img src="images/erase.gif" onclick="$('#<%=date_e.ClientID%>').val('');" /></td>
                </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> *標題</td>
                  <td align="left" valign="top" class="List06"><input name="news_title" type="text" class="Forms04" id="news_title" size="50" runat="server" />
                  <asp:RequiredFieldValidator ID="vnews_title" runat="server" ErrorMessage="*必填欄位" ControlToValidate="news_title" Display="Dynamic" CssClass="NormalRed" /></td>
                </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 內容</td>
                  <td align="left" valign="top" class="List06"><input name="btnEditor" type="button" class="Forms02" id="btnEditor" value="Html編輯器"
                                            onclick="toggleEditor('<%=content.ClientID%>');" />
                    <br />
                    <textarea name="content" cols="55" rows="5" id="content" class="EditTxtbox" runat="server"></textarea></td>
                </tr>
                <tr style="display:none">
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 圖片</td>
                  <td align="left" valign="top" class="List06"><table width="100%" border="0" cellspacing="1" cellpadding="0">
                      <tr>
                        <td align="center" valign="middle" nowrap="nowrap" class="Title01"> 圖片上傳</td>
                        <td align="left" valign="top" class="List06" id="tdImgName" runat="server"><span id="imgHref" runat="server"></span>
                          <input name="imgName" type="file" id="imgName" size="40" runat="server" class="Forms04" /></td>
                      </tr>
                      <tr>
                        <td align="center" valign="middle" nowrap="nowrap" class="Title01"> 圖片說明</td>
                        <td align="left" valign="top" class="List06"><input name="imgDesc" type="text" class="Forms04" id="imgDesc" size="50" runat="server" /></td>
                      </tr>
                      <tr>
                        <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> *圖片位置</td>
                        <td width="80%" align="left" valign="top" class="List06"><select name="imgAlign" id="imgAlign" runat="server" class="Forms03">
                            <option value="left" selected="selected">左</option>
                            <option value="center">中</option>
                            <option value="right">右</option>
                          </select></td>
                      </tr>
                    </table></td>
                </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 相關連結</td>
                  <td align="left" valign="top" class="List06"><input name="link_href" type="text" class="Forms04" id="link_href" size="50" runat="server" /></td>
                </tr>
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 檔案上傳</td>
                  <td align="left" valign="top" class="List06"><table width="100%" border="0" cellspacing="1" cellpadding="0">
                      <tr>
                        <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01"> 檔案上傳</td>
                        <td width="80%" align="left" valign="top" class="List06"><span id="fileHref" runat="server"></span>
                          <input name="fileName" type="file" id="fileName" size="40" runat="server" class="Forms04" /></td>
                      </tr>
                      <tr>
                        <td align="center" valign="middle" nowrap="nowrap" class="Title01"> 檔案說明</td>
                        <td align="left" valign="top" class="List06"><input name="fileDesc" type="text" class="Forms04" id="fileDesc" size="50" runat="server" /></td>
                      </tr>
                    </table></td>
                </tr>
                
              </table></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td style="font-size: 1px; height: 5px;"></td>
    </tr>
    <tr>
      <td align="center" valign="middle" class="MainText01" style="padding-right: 3px;">
      <asp:Button ID="btnSave" runat="server" Text="儲存" CssClass="Forms02" onclientclick="return checkForm();" /></td>
    </tr>
  </table>
</asp:Content>

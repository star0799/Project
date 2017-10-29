<%@ Control Language="VB" AutoEventWireup="false" CodeFile="lang_module_control.ascx.vb" Inherits="deskmanager_controls_lang_module_control" %>

<% if (showControl) then %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top" class="List01">管理項目
      <asp:DropDownList ID="ddlSetting" runat="server" AutoPostBack="true">
        <asp:ListItem value="List" Selected="true">訊息管理</asp:ListItem>
        <asp:ListItem value="UnitSet">單元設定</asp:ListItem>
      </asp:DropDownList></td>
  </tr>
  <tr>
    <td align="left" valign="top" class="Line04"></td>
  </tr>
</table>
<% end if %>
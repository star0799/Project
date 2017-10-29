<%@ Control Language="VB" AutoEventWireup="false" CodeFile="pager.ascx.vb" Inherits="controls_pager" %>

<table width="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top" class="PageTool"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td align="left" valign="middle">總筆數:
            <asp:Literal ID="lblPager" runat="server" /></td>
          <td align="right" valign="middle"><table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="center" valign="bottom"><asp:ImageButton ID="btnFirst" runat="server" ImageUrl="~/images/arrow_first.png" CommandName="First" AlternateText="第一頁" /></td>
                <td align="center" valign="bottom"><asp:ImageButton ID="btnPrev" runat="server" ImageUrl="~/images/arrow_per.png" CommandName="Prev" AlternateText="上一頁" /></td>
                <td align="center" valign="middle"><asp:DropDownList ID="ddlPageList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageList_SelectedIndexChanged" /></td>
                <td align="center" valign="bottom"><asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/images/arrow_next.png" CommandName="Next" AlternateText="下一頁" /></td>
                <td align="center" valign="bottom"><asp:ImageButton ID="btnLast" runat="server" ImageUrl="~/images/arrow_list.png" CommandName="Last" AlternateText="最未頁" /></td>
              </tr>
            </table></td>
        </tr>
      </table></td>
  </tr>
</table>

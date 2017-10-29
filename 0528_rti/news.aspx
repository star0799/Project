<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="news.aspx.vb" inherits="news" %>
<%@ Register Src="controls/pager.ascx" TagName="pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="content" class="news">
    <div class="title" id="divPageTitle" runat="server">標題</div>
    <% if (showCategory) then %>
    <div class="contentTool">類別:
      <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="True" />
    </div>
    <% end if %>
    <div class="list Conbg">
      <asp:ListView ID="lvwList" runat="server" OnItemDataBound="lvwList_ItemDataBound" OnDataBound="lvwList_DataBound">
        <LayoutTemplate>
          <table width="100%" border="0" cellspacing="0" cellpadding="0" class="newslist">
            <tr id="trHeader" runat="server">
              <th width="10%" align="center">序號</th>
              <th align="center">標題</th>
              <th width="20%" align="center">日期</th>
              <th align="center" width="20%">類別</th>
            </tr>
            <tr id="itemPlaceholder" runat="server" />
          </table>
        </LayoutTemplate>
        <ItemTemplate>
          <tr id="trItem" runat="server">
            <td align="center"><asp:Literal ID="lblSeqNo" runat="server" /></td>
            <td align="left"><a href="news_in.html" id="hlkTitle" runat="server">「發明專利關聯案聯合面詢」自101年10月1日起開始實施 </a></td>
            <td align="center"><asp:Literal ID="lblPublishDate" runat="server" Text='<%# Eval("pdate") %>' /></td>
            <td align="center"><asp:Literal ID="lblCatName" runat="server" Text='<%# convVal(Eval("cat_name"), "無類別") %>' /></td>
          </tr>
        </ItemTemplate>
        <EmptyDataTemplate>目前沒有資料!</EmptyDataTemplate>
      </asp:ListView>
      <uc:pager ID="Pager1" runat="server" />
    </div>
  </div>
</asp:Content>

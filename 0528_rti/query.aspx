<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="query.aspx.vb" inherits="query" %>
<%@ Register Src="controls/pager.ascx" TagName="pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="content" class="search">
    <div class="title">搜尋結果</div>
    <asp:ListView ID="lvwList" runat="server" OnItemDataBound="lvwList_ItemDataBound" OnDataBound="lvwList_DataBound">
      <LayoutTemplate>
        <div class="list">
          <ul>
            <asp:PlaceHolder id="itemPlaceholder" runat="server" />
          </ul>
        </div>
      </LayoutTemplate>
      <ItemTemplate>
        <li>
          <div>
            <h2><a href="#" id="hlkTitle" runat="server">反共義士駕機來歸</a></h2>
            <h3><strong> 主題 :</strong> <asp:Literal ID="lblSubject" runat="server" /></h3>
            <h3><strong> 主題與關鍵字 :</strong> <asp:Literal ID="lblKeywordName" runat="server" /></h3>
            <h3><strong> 出版日期 :</strong> <asp:Literal ID="lblDate" runat="server" /></h3>
            <h3><strong> 刊名與期數 :</strong> <asp:Literal ID="lblCover" runat="server" /> </h3>
          </div>
        </li>
      </ItemTemplate>
      <EmptyDataTemplate>查無資料!</EmptyDataTemplate>
    </asp:ListView>
    <uc:pager ID="Pager1" runat="server" />
  </div>
  </div>
</asp:Content>

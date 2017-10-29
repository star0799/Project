<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="page_view.aspx.vb" inherits="page_view" %>
<%@ Register Src="controls/navigation.ascx" TagName="navigation" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="content" class="news">
    <div class="title" id="divPageTitle" runat="server">標題</div>
    <div class="txt Conbg"> <%=content%> </div>
    <uc:navigation ID="Navigation1" runat="server" />
  </div>
</asp:Content>

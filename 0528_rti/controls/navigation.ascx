<%@ Control Language="VB" AutoEventWireup="false" CodeFile="navigation.ascx.vb" Inherits="controls_navigation" %>
<% if (showBack) then %>
<i class="icon-circle-arrow-left"></i> <a href="javascript:void(0);" onclick="history.back();">回上一頁</a>
<% end if %>
&nbsp;
<% if (showLink) then %>
<i class="icon-link"></i> <a href="#" target="_blank" id="hlkLink" runat="server">相關連結</a>
<% end if %>
&nbsp;
<% if (showFile) then %>
<i class="icon-download"></i> <a href="#" id="hlkFile" runat="server">檔案下載</a>
<% end if %>

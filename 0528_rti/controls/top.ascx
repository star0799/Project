<%@ Control Language="VB" AutoEventWireup="false" CodeFile="top.ascx.vb" Inherits="controls_top" %>

<div id="wrap-top">
  <div id="top-wrap">
    <div class="top-tool">
      <div class="top-Nav">
        <ul>
          <li><a href ="index.aspx">回首頁</a></li>
          <li><a href ="http://www.rti.org.tw">回央廣首頁</a></li>
          <li><a href ="page_view.aspx?mnuid=1325">著作權聲明</a></li>
          <li><a href ="page_view.aspx?mnuid=1327">會員權益</a></li>
          <li><a href ="order.aspx">訂購</a></li>
          <li><a href ="news.aspx?mnuid=1319">常見問題</a></li>
          <li><a href ="contact.aspx">聯絡我們</a></li>
          
          <% if (not isLogon) then %>
          <li><a id="hlkLogin" runat="server" href ="http://www.rti.org.tw/aboutrti/">會員登錄</a></li>
          <% else %>
          <li>Hi, <%=uName%> 您好</li>
          <li><a id="hlkLogout" runat="server" href="javascript:void(0);">登出</a></li>
          <% end if %>
        </ul>
      </div>
    </div>
    <div id  ="banner"> <img src ="images/banner01.png" > </div>
  </div>
</div>

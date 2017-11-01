<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="arch_view.aspx.vb" inherits="arch_view" %>
<%@ Register Src="controls/navigation.ascx" TagName="navigation" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <link rel="stylesheet" type="text/css" href="css/lightbox.css" media="screen" />
  <script type="text/javascript" src="js/lightbox-2.6.min.js"></script>
  <script>
  $(function() {
  	$('.pic').mouseenter(function() {
		var imgUrl = $(this).attr('m');
		var img = new Image();
		$(img).load(function () {
			$(this).hide();
			$('#<%=imgProd.ClientID%>').attr("src", imgUrl).hide().fadeIn('fast');
		}).attr("src", imgUrl);
		return false;
	});
});
  </script>
  
  <div id="content" class="collection">
    <div class="title" id="divPageTitle" runat="server">標題</div>
    <div class="txt">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="1" align="left" valign="top"><table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td align="center" class='BigPic'><img id="imgProd" runat="server" src="images/img1.png" height="260" /></td>
                    </tr>
                  </table></td>
                <td align="left" valign="top"><h2><%=p_title%></h2>
                  <p><%=getTailText(rdesc,250)%></p>
                  <a href="#" id="hlkOrder" runat="server" class="showBtn">我要訂購 
                    </a>
                    <%--新增--%>
                    <audio src="music/NieR%20Automata%20OST%20-%20Pascal.mp3" controls="controls" preload="autoplay ">
                        <p>Your browser does not support the <code>audio</code> element </p>
                    </audio>                 
                    <%--新增--%>
                </td>      
                  
              </tr>
              
            </table></td>
        </tr>
          
        <tr id="trImg" runat="server">
          <td><asp:ListView ID="lvwList" runat="server" OnItemDataBound="lvwList_ItemDataBound" OnDataBound="lvwList_DataBound">
              <LayoutTemplate>
                <ul class="piclist">
                  
                    <tr id="itemPlaceholder" runat="server" />
                </ul>
              </LayoutTemplate>
              <ItemTemplate>
                <li><a id="hlkTitlePic" runat="server" data-lightbox="prod" class="pic"> <img id="imgPic" runat="server" src="images/spacing.png" width="80" height="80"></a> </li>
              </ItemTemplate>
            </asp:ListView></td>
        </tr>
        <tr id="trFile" runat="server">
          <td class='datalist'><h1>檔案列表</h1>
            <div class='Txt'>
              <asp:ListView ID="lvwList2" runat="server" OnItemDataBound="lvwList2_ItemDataBound" OnDataBound="lvwList2_DataBound">
                <LayoutTemplate>
                  <table width="100%" border="0" cellspacing="3" cellpadding="2" class="newslist">
                    <tr>
                      <th width="10%">序號</th>
                      <th>標題</th>
                      <th width="20%">檔案查閱</th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server" />
                  </table>
                </LayoutTemplate>
                <ItemTemplate>
                  
                    <tr>
                      <td align="center"><asp:Literal runat="server" Text='<%# Eval("item_no") %>' /></td>
                      <td align="left"><asp:Literal runat="server" Text='<%# p_title & " - " & Eval("item_no") %>' /></td>
                      <td align="center"><a href="javascript:void(0);" id="hlkView" runat="server" target="_blank"><img src="images/btn_down.png" width="87" height="27"></a></td>
                    </tr>
                </ItemTemplate>
              </asp:ListView>
            </div></td>
        </tr>
        <tr>
          <td class='datalist'><h1>內容</h1>
            <div class='Txt'>
              <ul>
                <li><strong>識別碼   :</strong><%=identifier%></li>
                <li><strong>標題    :</strong><%=p_title%></li>
                <li><strong>資料類型:</strong><br />
                  類型：<%=rtype%><br />
                  載體：<%=carrier%><br />
                  型式：<%=file_type%></li>
                <li><strong>主題    :</strong><%=subject%></li>
                
                <% if (rdesc.length > 0) then %>
                <li><strong>描述     :</strong>
                  <p><%=rdesc%> </p>
                </li>
                <% end if %>
                <li><strong>出版日期     :</strong> <%=date_show%></li>
                <li><strong>語言     :</strong><%=language%></li>
                <li><strong>所有權人     :</strong><%=rights_owner%> </li>
              </ul>
              <a href="#" class="showBtn">顯示詳細資料</a>
              <div class="txt2">
                <ul>
                  <li><strong>專案 : </strong><%=project%></li>
                  <li><strong>所屬分類 :</strong><%=record%> </li>
                  <% if (creator.length > 0) then %>
                  <li><strong>創作者 :</strong><%=creator%> </li>
                  <% end if %>
                  <% if (cover.length > 0) then %>
                  <li><strong>刊名與期數 :</strong><%=cover%></li>
                  <% end if %>
                  <% if (publisher.length > 0) then %>
                  <li><strong>出版者 : </strong><%=publisher%></li>
                  <% end if %>
                  <li><strong>資料格式 : </strong><br />
                    單位：<%=catalog_level%><br />
                    尺寸：<%=format%><br />
                    頁數：<%=page_count%></li>
                  <li><strong>資料總頁 : </strong><%=total_page%></li>
                  <li><strong>版式 : </strong><%=layout%></li>
                  <li><strong>主題與關鍵字 : </strong><%=keyword_name%></li>
                  <li><strong>目次描述 : </strong><%=rdesc_catalog%></li>
                  <% if (source.length > 0) then %>
                  <li><strong>對應資訊 : </strong><%=source%></li>
                  <% end if %>
                  <% if (connection.length > 0) then %>
                  <li><strong>相關資訊 : </strong><%=connection%></li>
                  <% end if %>
                  <% if (storage_department.length > 0) then %>
                  <li><strong>典藏單位 : </strong><%=storage_department%></li>
                  <% end if %>
                  <% if (reference.length > 0) then %>
                  <li><strong>附註項 : </strong><%=reference%></li>
                  <% end if %>
                  <li><strong>原件權利 : </strong><br />
                    著作權：<%=rights%><br />
                    財產物權：<%=rights_object%></li>
                  <li><strong>數位檔權利    :</strong><br />
                    所有權人：<%=rights_owner%>
                    <% if (rights_restrictions.length > 0) then %>
                    <br />
                    授權狀況：<%=rights_restrictions%>
                    <% end if %>
                  </li>
                </ul>
              </div>
            </div></td>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>

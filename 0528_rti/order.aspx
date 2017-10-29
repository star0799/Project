<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="order.aspx.vb" inherits="order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
  <script language="JavaScript">
$(function() {
});
function checkForm(){
	if (typeof(Page_ClientValidate) == 'function') {
		if (Page_ClientValidate() == false) {
			Page_BlockSubmit = false;
			return false;
		}
	}
}
</script>
  <div id="content" class="news">
    <div class="title" id="divPageTitle" runat="server">標題</div>
    <div class="txt Conbg">
      <table width="98%" border="0" cellspacing="0" cellpadding="0" class="FormTab">
        <% if (aid.length = 0) then %>
        <tr>
          <th width="15%" align="right">*識別碼:</th>
          <td><label for="identifier"></label>
            <input name="identifier" type="text" id="identifier" runat="server" size="20" maxlength="20" />
            <asp:RequiredFieldValidator ID="videntifier" runat="server" ErrorMessage="*必填欄位" ControlToValidate="identifier" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*標題:</th>
          <td><input name="p_title" type="text" id="p_title" runat="server" size="50" maxlength="100" />
            <asp:RequiredFieldValidator ID="vp_title" runat="server" ErrorMessage="*必填欄位" ControlToValidate="p_title" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <% else %>
        <tr>
          <th align="right">*識別碼:</th>
          <td><%=identifier.value%></td>
        </tr>
        <tr>
          <th align="right">*標題:</th>
          <td><%=p_title.value%></td>
        </tr>
        <% end if %>
        
        <tr>
          <th align="right">*用途:</th>
          <td><select name="purpose" id="purpose" runat="server" class="Forms03">
            <option value="1" selected="selected">學術</option>
            <option value="2">廣告</option>
            <option value="3">文創</option>
            <option value="4">其他</option>
          
            </select>
            <asp:RequiredFieldValidator ID="vpurpose" runat="server" ErrorMessage="*必填欄位" ControlToValidate="purpose" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*單位:</th>
          <td><input name="unit" type="text" id="unit" runat="server" size="10" maxlength="10" />
            <asp:RequiredFieldValidator ID="vunit" runat="server" ErrorMessage="*必填欄位" ControlToValidate="unit" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*數量:</th>
          <td><input name="qty" type="text" id="qty" runat="server" size="10" maxlength="10" />
            <asp:RequiredFieldValidator ID="vqty" runat="server" ErrorMessage="*必填欄位" ControlToValidate="qty" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*訂購人:</th>
          <td><input name="userName" type="text" id="userName" runat="server" size="50" maxlength="50" />
            <asp:RequiredFieldValidator ID="vuserName" runat="server" ErrorMessage="*必填欄位" ControlToValidate="userName" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*電話:</th>
          <td><input name="phone" type="text" id="phone" runat="server" size="20" maxlength="20" />
            <asp:RequiredFieldValidator ID="vphone" runat="server" ErrorMessage="*必填欄位" ControlToValidate="phone" Display="Dynamic" CssClass="NormalRed" /></td>
        </tr>
        <tr>
          <th align="right">*Email:</th>
          <td><input name="email" type="text" id="email" runat="server" size="50" maxlength="100" />
            <asp:RequiredFieldValidator ID="vemail" runat="server" ErrorMessage="*必填欄位" ControlToValidate="email" Display="Dynamic" CssClass="NormalRed" />
            <asp:RegularExpressionValidator ID="v2email" runat="server" ControlToValidate="email" ErrorMessage="請輸入有效的電子信箱!" CssClass="NormalRed" display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" /></td>
        </tr>
        <tr>
          <th>&nbsp;</th>
          <td class="Btnsend"><input name="btnSubmit" type="submit" id="btnSubmit" runat="server" value="送 出" onclick="return checkForm();" /></td>
        </tr>
        <tr>
          <th colspan="2" align="left" class="note"><p>註：訂購人俟取得本臺「授權證明書」正本後，方得行使利用之行為。利用本著作後，乙方應寄送叁份「衍生作品」予甲方收存。</p></th>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>

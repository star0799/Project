<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="index.aspx.vb" inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="content" class="index">
    <div class="txt Conbg">
      <div class='IndexTxt'>
        <h2>說明</h2>
        <%=intro%><a href="#" id="hlkMore" runat="server"><img src="images/btnmore.png"></a> 
      </div>
      <div class="indexSearch">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="1" align="left" valign="top"><img src="images/index_pic.png" width="179" height="340"></td>
            <td align="left" valign="top"><h1>典藏查詢</h1>
              <table width="98%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <th width="15%" align="right">標 題:</th>
                  <td><label for="archTitle"></label>
                    <input name="archTitle" type="text" id="archTitle" runat="server" size="50" maxlength="50"></td>
                </tr>
                <tr>
                  <th align="right">主 題:</th>
                  <td>
                  <asp:CheckBoxList ID="subject" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="subject" /></td>
                </tr>
                <tr>
                  <th align="right">關鍵字:</th>
                  <td><input name="keyword" type="text" id="keyword" size="50" maxlength="20" runat="server" /></td>
                </tr>
                <tr>
                  <th align="right">出版日期:</th>
                  <td><input type="text" name="date_s" id="date_s" runat="server" value="" size="7" readonly="readonly" /> ~ <input type="text" name="date_e" id="date_e" runat="server" value="" size="7" readonly="readonly" /></td>
                </tr>
                <tr>
                  <th>&nbsp;</th>
                  <td valign="bottom" class="Btnsend">
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="1" align="left" valign="bottom"><input name="btnSubmit" type="submit" id="btnSubmit" runat="server" value="查 詢" onclick="return checkForm();" /></td>
                    <td align="left" valign="bottom" style="padding:0 0 8px 15px"><a href="adv_search.aspx" > [進階查詢]</a></td>
                  </tr>
                </table>

                   </td>
                </tr>
              </table></td>
          </tr>
        </table>
      </div>
    </div>
  </div>
  <script language="javascript" type="text/javascript">
	$(function() {
		$("#<%=date_s.ClientID%>, #<%=date_e.ClientID%>").datepicker();
		$('#<%=subject.clientid%> input[type="checkbox"]').first().click(function() {
			$('input[type="checkbox"]').prop('checked', $(this).is(':checked'));	
		});
		$('.subject tr td:last-child').css({width: "35%"});
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
</asp:Content>
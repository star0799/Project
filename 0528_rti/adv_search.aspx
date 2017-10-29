<%@ page language="VB" masterpagefile="~/container.master" autoeventwireup="false" CodeFile="adv_search.aspx.vb" inherits="adv_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="content" class="index">
    <div class="txt Conbg">
    
      <div class="indexSearch">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="1" align="left" valign="top"><img src="images/index_pic.png" width="179" height="340"></td>
            <td align="left" valign="top"><h1>進階查詢</h1>
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
                  <th align="right">載 體:</th>
                  <td><select name="carrier" id="carrier" runat="server" class="Forms03">
                  </select></td>
                </tr>
                <tr>
                  <th align="right">型 式:</th>
                  <td><select name="file_type" id="file_type" runat="server" class="Forms03">
                  </select></td>
                </tr>
                <tr>
                  <th align="right">刊名與期數:</th>
                  <td><input name="cover" type="text" id="cover" runat="server" size="30" maxlength="50" /></td>
                </tr>
                <tr>
                  <th align="right">關鍵字1:</th>
                  <td><input name="keyword" type="text" id="keyword" runat="server" size="30" maxlength="20" /></td>
                </tr>
                <tr>
                  <th align="right">關鍵字2:</th>
                  <td><input name="keyword2" type="text" id="keyword2" runat="server" size="30" maxlength="20" /></td>
                </tr>
                <tr>
                  <th align="right">關鍵字3:</th>
                  <td>
                  <input name="keyword3" type="text" id="keyword3" runat="server" size="30" maxlength="20" /></td>
                </tr>
                <tr>
                  <th align="right">出版日期:</th>
                  <td><input type="text" name="date_s" id="date_s" runat="server" value="" size="7" readonly="readonly" /> ~ <input type="text" name="date_e" id="date_e" runat="server" value="" size="7" readonly="readonly" /></td>
                </tr>
                <tr>
                  <th>&nbsp;</th>
                  <td class="Btnsend"><input name="btnSubmit" type="submit" id="btnSubmit" runat="server" value="查 詢" onclick="return checkForm();" /></td>
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
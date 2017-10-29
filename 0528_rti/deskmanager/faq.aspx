<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="faq.aspx.vb" inherits="faq" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>
<%@ Reference Control="controls/module_control.ascx" %>
<%@ Register src="controls/module_control.ascx" tagname="module_control" tagprefix="uc" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/list.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="javascript" type="text/javascript">
	<!--							
	function checkSearchForm(){
		var x, y, o, flag, check, mode;
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSearch").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSearch', '');
		return true;
	}
$(function() {
	$("#<%=pdate_s.ClientID%>, #<%=pdate_e.ClientID%>").datepicker();
});
	//-->						
	</script>
  <telerik:RadAjaxManager id="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" enableajax="true">
    <AjaxSettings>
      <telerik:AjaxSetting AjaxControlID="radList">
        <UpdatedControls>
          <telerik:AjaxUpdatedControl ControlID="radList" />
        </UpdatedControls>
      </telerik:AjaxSetting>
    </AjaxSettings>
  </telerik:RadAjaxManager>
  <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" Skin="Default" />
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top"><uc:module_control ID="module_control1" runat="server" /></td>
          </tr>
          <tr>
            <td align="left" valign="top"><table border="0" width="100%" id="table5">
                <tr>
                  <td align="center" class="Title01">類別</td>
                  <td colspan="3" class="List06"><asp:DropDownList ID="ddlCat" runat="server" /></td>
                </tr>
                <tr>
                  <td width="20%" align="center" class="Title01">發布日期</td>
                  <td colspan="3" class="List06"><input type="text" name="pdate_s" id="pdate_s" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                    <img src="images/erase.gif" onclick="$('#<%=pdate_s.ClientID%>').val('');" /> ~
                    <input type="text" name="pdate_e" id="pdate_e" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                    <img src="images/erase.gif" onclick="$('#<%=pdate_e.ClientID%>').val('');" /></td>
                </tr>
                <tr>
                  <td width="20%" align="center" class="Title01">關鍵字</td>
                  <td colspan="3" class="List06"><input type="text" size="40" name="keyword" id="keyword" runat="server" /></td>
                </tr>
                <tr>
                  <td colspan="4" align="center"><input name="btnSearch" type="button" class="Forms02" id="btnSearch" value="搜尋" runat="server" onclick="return checkSearchForm();" /></td>
                </tr>
              </table></td>
          </tr>
          <tr>
            <td align="left" valign="top" class="List02"><input name="btnCat" type="button" class="Forms02" id="btnCat" value="類別管理" onserverclick="btnCat_ServerClick" runat="server" /></td>
          </tr>
          <tr>
            <td align="left" valign="top"><div id="listContainer" runat="server">
                <telerik:RadGrid EnableLinqExpressions ="false" ID="radList" runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True">
                  <MasterTableView DataKeyNames="id" Font-Size="11" CommandItemDisplay="Top" EditMode="InPlace">
                    <Columns>
                    <telerik:GridTemplateColumn>
                      <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" Tooltip="刪除"
            OnClientClick="javascript:if(!confirm('確定要刪除此筆項目?')){return false;}"
            ImageUrl="images/delete.png" CommandName="Delete" CausesValidation="false" />
                      </ItemTemplate>
                      <EditItemTemplate> </EditItemTemplate>
                      <ItemStyle Width="3%" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="序號">
                      <ItemTemplate>
                        <asp:Label ID="lblSeqNo" runat="server" Text='<%# Container.DataSetIndex+1 %>' />
                      </ItemTemplate>
                      <ItemStyle Width="8%" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="發布日期" SortExpression="pdate">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("pdate") %>' />
                      </ItemTemplate>
                      <ItemStyle Width="15%" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="標題" SortExpression="news_title">
                      <ItemTemplate>
                        <asp:HyperLink ID="hlkTitle" runat="server" Text='<%# Eval("news_title") %>' NavigateUrl='<%# String.Format("faq_edit.aspx?mnuid={0}&nid={1}&cid={2}", mnuid, Eval("id"), cid)%>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="類別" SortExpression="cat_name">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# convVal(Eval("cat_name"),"無類別") %>' />
                      </ItemTemplate>
                      <ItemStyle Width="15%" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="是否在顯示期間" SortExpression="in_show">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# formatYN(Eval("in_show")) %>' />
                      </ItemTemplate>
                      <ItemStyle Width="15%" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="建立帳號" SortExpression="create_user_id">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("create_user_id") %>' />
                      </ItemTemplate>
                      <ItemStyle Width="15%" />
                    </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="True"></PagerStyle>
                  </MasterTableView>
                </telerik:RadGrid>
              </div></td>
          </tr>
        </table></td>
    </tr>
  </table>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="arch_config.aspx.vb" inherits="arch_config" %>
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

});
	//-->						
	</script>
   
  <telerik:RadAjaxManager id="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" enableajax="false">
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
            <td align="left" valign="top" class="List02">
              類型：
            <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="True" CssClass="Forms03"></asp:DropDownList></td>
          </tr>
          <tr>
            <td align="left" valign="top"><div id="listContainer" runat="server">
                <telerik:RadGrid EnableLinqExpressions ="false" ID="radList" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false">
                  <MasterTableView DataKeyNames="id, name" Font-Size="11" CommandItemDisplay="Top" EditMode="InPlace">
                    <Columns>
                    <telerik:GridEditCommandColumn UniqueName="Edit" ButtonType="ImageButton">
                      <ItemStyle Width="3%" Wrap="false" />
                    </telerik:GridEditCommandColumn>
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
                    <telerik:GridTemplateColumn HeaderText="項目名稱">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("name") %>' />
                      </ItemTemplate>
                      <EditItemTemplate>
                        <input type="text" name="item_name" id="item_name" runat="server" size="40" value='<%# Eval("name") %>' class="Forms04" maxlength="50" />
                        <br />
                        <asp:RequiredFieldValidator ID="vitem_name" runat="server" ErrorMessage="*必填欄位" ControlToValidate="item_name" Display="Dynamic" CssClass="NormalRed" />
                      </EditItemTemplate>
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

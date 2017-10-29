<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="arch_str.aspx.vb" inherits="arch_str" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>

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
            <td align="left" valign="top"><table border="0" width="100%" id="table5">
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
            <td align="left" valign="top" class="List02">&nbsp;</td>
          </tr>
          <tr>
            <td align="left" valign="top"><div id="listContainer" runat="server">
                <telerik:RadGrid EnableLinqExpressions ="false" ID="radList" runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True">
                  <MasterTableView DataKeyNames="id, group_name" Font-Size="11" CommandItemDisplay="Top" EditMode="InPlace">
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
                    <telerik:GridTemplateColumn HeaderText="組別名稱" SortExpression="group_name">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("group_name") %>' />
                      </ItemTemplate>
                      <EditItemTemplate>
                        <input type="text" name="group_name" id="group_name" runat="server" size="20" value='<%# Eval("group_name") %>' class="Forms04" maxlength="30" />
                        <br />
                        <asp:RequiredFieldValidator ID="vgroup_name" runat="server" ErrorMessage="*必填欄位" ControlToValidate="group_name" Display="Dynamic" CssClass="NormalRed" />
                      </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="詞庫(請以、分隔多組詞庫)">
                      <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("arch_str") %>' />
                      </ItemTemplate>
                      <EditItemTemplate>
                        <textarea type="text" name="arch_str" id="arch_str" runat="server" cols="40" rows="5" style="width:100%"><%# Eval("arch_str") %></textarea>
                        <br />
                        <asp:RequiredFieldValidator ID="varch_str" runat="server" ErrorMessage="*必填欄位" ControlToValidate="arch_str" Display="Dynamic" CssClass="NormalRed" />
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

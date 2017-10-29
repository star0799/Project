<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="arch.aspx.vb" inherits="arch" %>
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
	$("#<%=create_date_s.ClientID%>, #<%=create_date_e.ClientID%>").datepicker();
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
            <td align="left" valign="top"><uc:module_control ID="module_control1" runat="server" /></td>
          </tr>
          <tr>
            <td align="left" valign="top"><table border="0" width="100%" id="table5">
                <tr>
                  <td width="15%" align="center" class="Title01">識別碼</td>
                  <td width="35%" class="List06"><input type="text" size="20" name="identifier" id="identifier" runat="server" /></td>
                  <td width="15%" align="center" class="Title01">所屬分類</td>
                  <td class="List06"><input type="text" size="20" name="record" id="record" runat="server" /></td>
                </tr>
                <tr>
                  <td align="center" class="Title01">標題</td>
                  <td colspan="3" class="List06"><input type="text" size="40" name="p_title" id="p_title" runat="server" /></td>
                </tr>
                <tr>
                  <td align="center" class="Title01">創作者</td>
                  <td class="List06"><input type="text" size="20" name="creator" id="creator" runat="server" /></td>
                  <td align="center" class="Title01">出版者</td>
                  <td class="List06"><input type="text" size="20" name="publisher" id="publisher" runat="server" /></td>
                </tr>
                <tr>
                  <td align="center" class="Title01">主題</td>
                  <td class="List06"><input type="text" size="20" name="subject" id="subject" runat="server" /></td>
                  <td align="center" class="Title01">關鍵字</td>
                  <td class="List06"><input type="text" size="20" name="keyword_name" id="keyword_name" runat="server" /></td>
                </tr>
                <tr>
                  <td align="center" class="Title01">目次</td>
                  <td class="List06"><input type="text" size="20" name="rdesc_catalog" id="rdesc_catalog" runat="server" /></td>
                  <td align="center" class="Title01">描述</td>
                  <td class="List06"><input type="text" size="20" name="rdesc" id="rdesc" runat="server" /></td>
                </tr>
                
                <tr>
                  <td align="center" class="Title01">登錄者</td>
                  <td colspan="3" class="List06"><input type="text" size="20" name="register" id="register" runat="server" /></td>
                </tr>
                
                <tr>
                  <td align="center" class="Title01">建檔日期</td>
                  <td colspan="3" class="List06"><input type="text" name="create_date_s" id="create_date_s" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                    <img src="images/erase.gif" onclick="$('#<%=create_date_s.ClientID%>').val('');" /> ~
                    <input type="text" name="birth_date_e2" id="create_date_e" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                  <img src="images/erase.gif" onclick="$('#<%=create_date_e.ClientID%>').val('');" /></td>
                </tr>
                <tr>
                  <td colspan="4" align="center"><input name="btnSearch" type="button" class="Forms02" id="btnSearch" value="搜尋" runat="server" onclick="return checkSearchForm();" /></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td align="left" valign="top" class="List02">
            <input type="button" id="btnConfig" runat="server" value="典藏項目維護" class="Forms02" /></td>
          </tr>
          <tr>
            <td align="left" valign="top"><div id="listContainer" runat="server">
                <telerik:RadGrid EnableLinqExpressions ="false" ID="radList" runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True">
                  <MasterTableView DataKeyNames="id" Font-Size="11" CommandItemDisplay="Top">
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
                    <telerik:GridTemplateColumn HeaderText="識別碼" SortExpression="identifier">
                      <ItemTemplate>
                        <asp:HyperLink ID="hlkTitle" runat="server" Text='<%# Eval("identifier") %>' NavigateUrl='<%# String.Format("arch_edit.aspx?mnuid={0}&aid={1}", mnuid, Eval("id"))%>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="資料類型" SortExpression="rtype_name">
                      <ItemTemplate>
                      	<asp:Label runat="server" Text='<%# Eval("rtype_name") %>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="專案" SortExpression="project">
                      <ItemTemplate>
                      	<asp:Label runat="server" Text='<%# Eval("project") %>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="標題" SortExpression="p_title">
                      <ItemTemplate>
                      	<asp:Label runat="server" Text='<%# Eval("p_title") %>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="刊名與期數" SortExpression="cover">
                      <ItemTemplate>
                      	<asp:Label runat="server" Text='<%# Eval("cover") %>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="點閱數" SortExpression="visit">
                      <ItemTemplate>
                      	<asp:Label runat="server" Text='<%# Eval("visit") %>' />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="作業">
                      <ItemTemplate>
                        <asp:Button ID="btnDetail" runat="server" Text="數位檔管理" CssClass="Forms02" CommandName="Detail" />
                      </ItemTemplate>
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

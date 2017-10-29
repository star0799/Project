<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="user_list.aspx.vb" inherits="deskmanager_user_list" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
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
	//-->						
	</script>
  <asp:UpdatePanel ID="upSubmit" runat="server">
    <ContentTemplate>
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td align="left" valign="top" class="MainTable01"><table width="100%" border="0" cellspacing="0" cellpadding="0" id="searchTable" style='<% if (Me.ViewState("doSearch") Is Nothing OrElse Me.ViewState("doSearch") = 0) Then %>display:none<%end if %>'>
              <tr>
                <td class="MainBoxTopLeft"><img src="images/spacer.gif" width="10" height="12" /></td>
                <td class="MainBoxTopCenter"><img src="images/spacer.gif" width="10" height="12" /></td>
                <td class="MainBoxTopRight"><img src="images/spacer.gif" width="10" height="12" /></td>
              </tr>
              <tr>
                <td class="MainBoxMidLeft">&nbsp;</td>
                <td align="left" valign="top" class="MainBoxMidCenter"><table align="center" width="100%" border="0" cellspacing="2" cellpadding="0" >
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01" width="20%">所屬群組</td>
                      <td align="left" valign="top" class="List06"><select name="searchCat" id="searchCat" runat="server" class="Forms03">
                          <option value="" selected="selected">不拘</option>
                        </select></td>
                    </tr>
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01">帳號</td>
                      <td width="80%" align="left" valign="top" class="List06"><input name="userId" type="text" class="Forms04" id="userId" size="40" runat="server" maxlength="100" /></td>
                    </tr>
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01">使用者姓名</td>
                      <td align="left" valign="top" class="List06"><input name="userName" type="text" class="Forms04" id="userName" size="40" runat="server" maxlength="100" /></td>
                    </tr>
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01">使用者狀態</td>
                      <td align="left" valign="top" class="List06"><select name="enabled" id="enabled" runat="server" style="width: 70px;" class="Forms03">
                          <option value="" selected="selected">不拘</option>
                          <option value="Y">啟用</option>
                          <option value="N">停用</option>
                        </select></td>
                    </tr>
                    <tr>
                      <td align="center" colspan="2"><input name="btnSearch" type="button" class="Forms02" id="btnSearch" value="搜尋" runat="server" onclick="return checkSearchForm();" /></td>
                    </tr>
                  </table></td>
                <td class="MainBoxMidRight">&nbsp;</td>
              </tr>
              <tr>
                <td class="MainBoxBotLeft"><img src="images/spacer.gif" width="10" height="12" /></td>
                <td class="MainBoxBotCenter"><img src="images/spacer.gif" width="10" height="12" /></td>
                <td class="MainBoxBotRight"><img src="images/spacer.gif" width="10" height="12" /></td>
              </tr>
              <tr>
                <td height="10"></td>
                <td></td>
                <td></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="left" valign="middle" style="padding-right:3px; padding-left:3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td align="left" valign="middle" class="List02">群組
                        <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" CssClass="Forms03"></asp:DropDownList>
                        <input name="btnGroup" type="button" class="Forms02" id="btnGroup" value="管理" runat="server" /></td>
                      <td width="50%" align="right" valign="middle" class="List02">&nbsp;</td>
                    </tr>
                  </table></td>
              </tr>
              <tr>
                <td align="left" valign="top"><asp:GridView ID="gvwList" runat="server" GridLines="None" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" BorderWidth="0px" CellPadding="0" CellSpacing="1" Width="100%" AllowPaging="True">
                    <Columns>
                    <asp:TemplateField>
                      <HeaderTemplate>
                        <asp:Button ID="btnDelete" runat="server" CssClass="Forms02" Text="刪除" OnClientClick="return delItem(this);" UseSubmitBehavior="False" CommandName="DeleteItem" />
                        <br />
                        <asp:CheckBox ID="chkAll" runat="server" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="5%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="帳號" SortExpression="userid">
                      <ItemTemplate>
                        <asp:HyperLink ID="hlkUserId" runat="server" CssClass="Title02Link"
										  Text='<%# Eval("user_id") %>'></asp:HyperLink>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="30%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="使用者姓名" SortExpression="user_name">
                      <ItemTemplate>
                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("user_name") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="30%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所屬群組" SortExpression="group_name">
                      <ItemTemplate>
                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="30%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="啟用" SortExpression="enabled">
                      <ItemTemplate>
                        <asp:CheckBox ID="chkEnabled" runat="server" AutoPostBack="True" OnCheckedChanged="chkEnabled_CheckedChanged" />
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="5%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td style="font-size:1px;  height:9px;"></td>
                        </tr>
                      </table>
                      <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td class="PageBarTopLeft"><img src="images/spacer.gif" width="6" height="6" /></td>
                          <td class="PageBarTopCenter"><img src="images/spacer.gif" width="6" height="6" /></td>
                          <td class="PageBarTopRight"><img src="images/spacer.gif" width="6" height="6" /></td>
                        </tr>
                        <tr>
                          <td class="PageBarMidLeft">&nbsp;</td>
                          <td align="left" valign="top" class="PageBarMidCenter"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="PageListBox">
                              <tr>
                                <td align="left" valign="middle" class="List04"> Total Rows：<font color="#ff6c00">
                                  <asp:Label ID="lblPager" runat="server" />
                                  </font></td>
                                <td align="right" valign="middle" class="List05"><asp:ImageButton ID="ibtnFirst" runat="server" ImageUrl="~/deskmanager/images/ListBackButton_01.gif" CommandName="Pager" CommandArgument="First" ImageAlign="AbsBottom" AlternateText="第一頁" />
                                  <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Pager" CommandArgument="First" CssClass="PageListLink">First</asp:LinkButton>
                                  <asp:ImageButton ID="ibtnPrev" runat="server" ImageUrl="~/deskmanager/images/ListBackButton.gif" CommandName="Pager" CommandArgument="Prev" ImageAlign="AbsBottom" AlternateText="上一頁" />
                                  <asp:LinkButton ID="lbtnPrev" runat="server" CommandName="Pager" CommandArgument="Prev" CssClass="PageListLink">Prev</asp:LinkButton>
                                  <asp:DropDownList ID="ddlPageList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageList_SelectedIndexChanged" CssClass="List07"></asp:DropDownList>
                                  <asp:ImageButton ID="ibtnNext" runat="server" ImageUrl="~/deskmanager/images/ListNextButton.gif" CommandName="Pager" CommandArgument="Next" ImageAlign="AbsBottom" AlternateText="下一頁" />
                                  <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Pager" CommandArgument="Next" CssClass="PageListLink">Next</asp:LinkButton>
                                  <asp:ImageButton ID="ibtnLast" runat="server" ImageUrl="~/deskmanager/images/ListNextButton_01.gif" CommandName="Pager" CommandArgument="Last" ImageAlign="AbsBottom" AlternateText="最未頁" />
                                  <asp:LinkButton ID="lbtnLast" runat="server" CommandName="Pager" CommandArgument="Last" CssClass="PageListLink">Last</asp:LinkButton></td>
                              </tr>
                            </table></td>
                          <td class="PageBarMidRight">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="PageBarBotLeft"><img src="images/spacer.gif" width="6" height="6" /></td>
                          <td class="PageBarBotCenter"><img src="images/spacer.gif" width="6" height="6" /></td>
                          <td class="PageBarBotRight"><img src="images/spacer.gif" width="6" height="6" /></td>
                        </tr>
                      </table>
                    </PagerTemplate>
                  </asp:GridView>
                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:siteConnectionString %>"
							SelectCommand="SELECT * FROM [users]"></asp:SqlDataSource></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

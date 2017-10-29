<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="user_log.aspx.vb" inherits="deskmanager_user_log" %>
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

		// login start & end date
		if (new Date(document.getElementById(prefixId + "loginStdate").value) > new Date(document.getElementById(prefixId + "loginEddate").value)) {
			alert("登入起始日期需小於結束日期");
			document.getElementById(prefixId + "loginStdate").focus();
			return false;
		}
		
		document.getElementById(prefixId + "btnSearch").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSearch', '');
		return true;
	}
	//-->						
	</script>
  
  
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
                <td align="left" valign="top" class="MainBoxMidCenter"><table align="center" width="100%" border="0" cellspacing="2" cellpadding="0">
                    <tr>
                      <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">登入日期</td>
                      <td width="80%" align="left" valign="middle" class="List06"><span class="BList01">
                        <input name="loginStdate" type="text" class="Forms04" id="loginStdate" size="10" runat="server" readonly="" />
                        <a href="javascript:;" onclick="NewCal('<%=prefixId%>loginStdate','yyyymmdd','100','-3')"><img src="images/calendar.gif" width="16" height="15" border="0" align="absmiddle" /></a><a href="javascript:;" onclick="<%=prefixId%>loginStdate.value = ''"><img src="images/clear.gif" width="22" height="13" border="0" align="absmiddle" /></a> ～
                        <input name="loginEddate" type="text" class="Forms04" id="loginEddate" size="10" runat="server" readonly="" />
                        <a href="javascript:;" onclick="NewCal('<%=prefixId%>loginEddate','yyyymmdd','100','-3')"><img src="images/calendar.gif" width="16" height="15" border="0" align="absmiddle" /></a><a href="javascript:;" onclick="<%=prefixId%>loginEddate.value = ''"><img src="images/clear.gif" width="22" height="13" border="0" align="absmiddle" /></a></span></td>
                    </tr>
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01">帳號</td>
                      <td align="left" valign="top" class="List06"><input name="none" type="text" id="none" style="display: none;" />
                        <input name="userId" type="text" class="Forms04" id="userId" size="40" runat="server" maxlength="100" /></td>
                    </tr>
                    <tr>
                      <td align="center" valign="middle" nowrap="nowrap" class="Title01">IP</td>
                      <td align="left" valign="top" class="List06"><input name="ip" type="text" class="Forms04" id="ip" size="40" runat="server" maxlength="100" /></td>
                    </tr>
                    <tr>
                      <td style="font-size:1px;  height:5px;"></td>
                    </tr>
                    <tr>
                      <td align="center" colspan="2"><input name="btnSearch" type="button" class="Forms02" id="btnSearch" value="搜尋" runat="server" onclick="return checkSearchForm();" /></td>
                    </tr>
                    <tr>
                      <td class="Line01" colspan="2">&nbsp;</td>
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
                <td align="left" valign="top"><asp:GridView ID="gvwList" runat="server" GridLines="None" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" BorderWidth="0px" CellPadding="0" CellSpacing="1" Width="100%" AllowPaging="True">
                    <Columns>
                    <asp:TemplateField HeaderText="登入時間">
                      <ItemTemplate>
                        <asp:Label ID="lblLoginDate" runat="server"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="25%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="登出時間">
                      <ItemTemplate>
                        <asp:Label ID="lblLogoutDate" runat="server"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="25%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="帳號">
                      <ItemTemplate>
                        <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("user_id") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="15%" Wrap="False" />
                      <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IP">
                      <ItemTemplate>
                        <asp:Label ID="lblIP" runat="server" Text='<%# Eval("ip") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle CssClass="Title01" Font-Bold="False" Width="25%" Wrap="False" />
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
                                <td align="right" valign="middle" class="List05"><asp:ImageButton ID="ibtnFirst" runat="server" ImageUrl="~/deskmanager/images/ListBackButton_01.gif" CommandName="Pager" CommandArgument="First" ImageAlign="bottom" AlternateText="第一頁" />
                                  <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Pager" CommandArgument="First" CssClass="Text04Link">First</asp:LinkButton>
                                  <asp:ImageButton ID="ibtnPrev" runat="server" ImageUrl="~/deskmanager/images/ListBackButton.gif" CommandName="Pager" CommandArgument="Prev" ImageAlign="bottom" AlternateText="上一頁" />
                                  <asp:LinkButton ID="lbtnPrev" runat="server" CommandName="Pager" CommandArgument="Prev" CssClass="Text04Link">Prev</asp:LinkButton>
                                  <asp:DropDownList ID="ddlPageList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageList_SelectedIndexChanged" CssClass="List07"></asp:DropDownList>
                                  <asp:ImageButton ID="ibtnNext" runat="server" ImageUrl="~/deskmanager/images/ListNextButton.gif" CommandName="Pager" CommandArgument="Next" ImageAlign="bottom" AlternateText="下一頁" />
                                  <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Pager" CommandArgument="Next" CssClass="Text04Link">Next</asp:LinkButton>
                                  <asp:ImageButton ID="ibtnLast" runat="server" ImageUrl="~/deskmanager/images/ListNextButton_01.gif" CommandName="Pager" CommandArgument="Last" ImageAlign="bottom" AlternateText="最未頁" />
                                  <asp:LinkButton ID="lbtnLast" runat="server" CommandName="Pager" CommandArgument="Last" CssClass="Text04Link">Last</asp:LinkButton></td>
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
                  </asp:GridView></td>
              </tr>
            </table></td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

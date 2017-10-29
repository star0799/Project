<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="page_cat.aspx.vb" inherits="deskmanager_page_cat" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="JavaScript" type="text/javascript" src="../js/list.js"></script>    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
		<table width="100%" border="0" cellpadding="0" cellspacing="0">
		  <tr>
			<td align="left" valign="top" class="MainTable01"><table width="100%" border="0" cellspacing="0" cellpadding="0">
			  <tr style="display:none">
				<td align="left" valign="middle" style="padding-right:3px; padding-left:3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
				  <tr>
					<td align="left" valign="middle" class="List02">&nbsp;</td>
					<td width="50%" align="right" valign="middle" class="List02"><input name="btnAdd" type="button" class="Forms02" id="btnAdd" value="新增" runat="server" onclick="return doAdd();" /></td>
				  </tr>
				</table></td>
			  </tr>
			  <tr>
				<td align="left" valign="top">						
					<asp:GridView ID="gvwList" runat="server" GridLines="None" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" BorderWidth="0px" CellPadding="0" CellSpacing="1" Width="100%" AllowPaging="True">
						<Columns>
                            <asp:TemplateField>
                                  <HeaderTemplate>
                                      <asp:Button ID="btnDelete" runat="server" CssClass="Forms02" Text="刪除" OnClientClick="return delItem(this);" UseSubmitBehavior="False" CommandName="DeleteItem" /><br />
                                      <asp:CheckBox ID="chkAll" runat="server" />
                                  </HeaderTemplate>                      
                                  <ItemTemplate>
                                      <asp:CheckBox ID="chkSelect" runat="server" />                                  </ItemTemplate>
                                  <HeaderStyle CssClass="Title01" Font-Bold="False" Width="5%" Wrap="False" />
                                  <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" />
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="排序">
                                  <ItemTemplate>
                                      <asp:ImageButton ID="ibtnTop" runat="server" CommandName="Move" ImageUrl="~/deskmanager/images/movFirst.gif" /><asp:ImageButton ID="ibtnUp" runat="server" CommandName="Move" ImageUrl="~/deskmanager/images/movPrev.gif" /><asp:ImageButton ID="ibtnDown" runat="server" CommandName="Move" ImageUrl="~/deskmanager/images/movNext.gif" /><asp:ImageButton ID="ibtnBottom" runat="server" CommandName="Move" ImageUrl="~/deskmanager/images/movLast.gif" />
                                      
                                      <asp:TextBox ID="txtSort" CssClass="Forms04" Columns="1" runat="server" onkeypress="if (event.keyCode == 13) {changeSort(this); return false;} "></asp:TextBox><asp:Button ID="btnSort" runat="server" CommandName="MoveSpecified" style="display:none" />
                                  </ItemTemplate>
                                  <HeaderStyle CssClass="Title01" Font-Bold="False" Width="15%" Wrap="False" />
                                  <ItemStyle Cssclass="List06" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            </asp:TemplateField>
							<asp:TemplateField HeaderText="頁面名稱">
								  <ItemTemplate>
									  <asp:HyperLink ID="hlkCatName" runat="server" CssClass="Title02Link" Text='<%# Eval("cat_name") %>'></asp:HyperLink>
								  </ItemTemplate>
								  <HeaderStyle CssClass="Title01" Font-Bold="False" Width="30%" Wrap="False" />
								  <ItemStyle Cssclass="List06" HorizontalAlign="Left" VerticalAlign="Middle" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="頁面路徑">
								  <ItemTemplate>
									  <asp:TextBox ID="txtPath" runat="server" ReadOnly="" Columns="30" />
									  <input type="button" ID="btnCopyPath" runat="server" value="複製" class="Forms02" />
								  </ItemTemplate>
								  <HeaderStyle CssClass="Title01" Font-Bold="False" Width="45%" Wrap="False" />
								  <ItemStyle Cssclass="List06" HorizontalAlign="Left" VerticalAlign="Middle" />
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="顯示">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="chkEnabled" runat="server" AutoPostBack="True" OnCheckedChanged="chkEnabled_CheckedChanged" />                                  </ItemTemplate>
                                  <HeaderStyle CssClass="Title01" Font-Bold="False" Width="5%" Wrap="false" />
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
                                          <asp:LinkButton ID="lbtnLast" runat="server" CommandName="Pager" CommandArgument="Last" CssClass="PageListLink">Last</asp:LinkButton>
                                      </td>
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
				  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:siteConnectionString %>" SelectCommand="SELECT * FROM [lang]"></asp:SqlDataSource>					
				</td>
			  </tr>
			</table></td>
		  </tr>
		</table>
		</ContentTemplate>				
	</asp:UpdatePanel>
</asp:Content>
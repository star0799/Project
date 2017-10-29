<%@ Page Language="VB" MasterPageFile="~/deskmanager/back_frame.master" AutoEventWireup="false"
    CodeFile="arch_str_import.aspx.vb" Inherits="arch_str_import" ValidateRequest="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/tiny_mce/tiny_mce.js"></script> 
  <script language="javascript" type="text/javascript" src="../js/editor.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/confirm.js"></script> 
  <script language="javascript" type="text/javascript">
	function checkForm(){
		var x, y, o, o2, flag, check, mode, suffix;
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSave").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSave', '');
		return true;
	}
	function checkForm2(){
		var x, y, o, o2, flag, check, mode, suffix;
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSave2").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSave2', '');
		return true;
	}					
    </script>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="MainTable01">
    <tr>
      <td align="left" valign="top" class="MainText01" style="padding-right: 3px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><asp:Label ID="lblMessage" runat="server" CssClass="NormalRed" />
              <telerik:RadGrid ID="radList2" runat="server" AutoGenerateColumns="false" AllowPaging="False">
                <MasterTableView Font-Size="11">
                  <Columns>
                  <telerik:GridTemplateColumn HeaderText="檔案行號">
                    <ItemTemplate>
                      <asp:Label runat="server" Text='<%# Eval("line_no") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
                  </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn HeaderText="欄位名稱">
                    <ItemTemplate>
                      <asp:Label runat="server" Text='<%# Eval("column_no") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
                  </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn HeaderText="錯誤內容">
                    <ItemTemplate>
                      <asp:Label runat="server" Text='<%# Eval("msg") %>' />
                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  </Columns>
                  <PagerStyle Mode="NextPrevAndNumeric" PagerTextFormat="{4}  第 &lt;strong&gt;{0}&lt;/strong&gt; 頁，共 &lt;strong&gt;{1}&lt;/strong&gt; 頁　總筆數: &lt;strong&gt;{5}&lt;/strong&gt;" AlwaysVisible="True"></PagerStyle>
                </MasterTableView>
              </telerik:RadGrid>
              <table width="100%" border="0" cellspacing="1" cellpadding="0" id="tblImport" runat="server">
                <tr>
                  <td width="20%" align="center" valign="middle" nowrap="nowrap" class="Title01">*匯入檔案</td>
                  <td width="80%" align="left" valign="top" class="List06"><span id="fileHref" runat="server"></span>
                    <input name="fileName" type="file" id="fileName" size="40" runat="server" class="Forms04" />
                    &nbsp;&nbsp;&nbsp;<a href="docs/權威詞庫(範例).xls">(範例)</a></td>
                </tr>
              </table></td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td style="font-size: 1px; height: 5px;"></td>
    </tr>
    <tr>
      <td align="center" valign="middle" class="MainText01" style="padding-right: 3px;"><input name="btnSave" type="button" class="Forms02" id="btnSave" value="上傳" runat="server" onclick="return checkForm();"></td>
    </tr>
  </table>
</asp:Content>

<%@ page language="VB" masterpagefile="~/deskmanager/back_frame.master" autoeventwireup="false" CodeFile="report.aspx.vb" inherits="report" %>
<%@ MasterType virtualPath="~/deskmanager/back_frame.master"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.1.13.612, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1"> 
    <script language="JavaScript" type="text/javascript" src="../js/list.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/datetimepicker.js"></script> 
  <script language="JavaScript" type="text/javascript" src="../js/check.js"></script> 
  <script language="javascript" type="text/javascript">
	function checkSearchForm(){
		var x, y, o, flag, check, mode;
		var prefixId = "master_ContentPlaceHolder1_";
		
		document.getElementById(prefixId + "btnSearch").disabled = true;
		__doPostBack('master$ContentPlaceHolder1$btnSearch', '');
		return true;
	}
	function checkForm(){
		if (typeof(Page_ClientValidate) == 'function') {
			if (Page_ClientValidate() == false) {
				Page_BlockSubmit = false;
				return false;
			}
		}
		
	}
	$(function() {
		$("#<%=report_date_s.ClientID%>, #<%=report_date_e.ClientID%>").datepicker();
	});				
	</script>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="left" valign="top"><table border="0" width="100%" id="table5">
                <tr>
                  <td width="20%" align="center" class="Title01">報表項目</td>
                  <td class="List06"><select size="1" name="reportCat" id="reportCat" runat="server">
                    <option value="" selected="selected">不拘</option>
                  </select></td>
                </tr>
                <tr>
                  <td width="20%" align="center" class="Title01">報表日期</td>
                  <td class="List06"><input type="text" name="report_date_s" id="report_date_s" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                    <img src="images/erase.gif" onclick="$('#<%=report_date_s.ClientID%>').val('');" /> ~
                    <input type="text" name="report_date_e" id="report_date_e" runat="server" value="" size="7" readonly="readonly" tabindex="30" />
                  <img src="images/erase.gif" onclick="$('#<%=report_date_e.ClientID%>').val('');" /></td>
                </tr>
                <tr>
                  <td colspan="2" align="center"><input name="btnSearch" type="button" class="Forms02" id="btnSearch" value="查詢" runat="server" onclick="return checkSearchForm();" causesvalidation="false" /></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td align="left" valign="top">
            </td>
          </tr>
        </table></td>
    </tr>
  </table>
</asp:Content>

Imports System.Data
Imports System.Data.SqlClient

Partial Class controls_leftpane
    Inherits FrontBaseUserControl

    'Dim s_searchCat(3), s_keyword As String

    'Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

    '    ' // event orders:
    '    ' // ascx init
    '    ' // master init
    '    ' // page load
    '    ' // master load
    '    ' // ascx load


    'End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    loadItem()

    'End Sub

    'Sub loadItem()

    '    cmd.CommandText = "SELECT         dbo.homelayout.link_href " & _
    '    "FROM             dbo.homelayout " & _
    '    "WHERE         (lang_id = @lang_id)"

    '    cmd.Parameters.Clear()
    '    cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = langId
    '    dr = cmd.ExecuteReader
    '    If (dr.Read()) Then
    '        hlkIntro.HRef = dr("link_href")
    '    End If
    '    dr.Close()

    '    loadMenu()
    '    loadSearch()
    'End Sub

    'Sub loadMenu()

    '    If (Not Me.IsPostBack) Then

    '        da.SelectCommand.CommandText = "SELECT     TOP (100) PERCENT dbo.menu.*, dbo.modcat.mod_table_name " & _
    '        "FROM         dbo.menu LEFT OUTER JOIN " & _
    '        "                      dbo.modcat ON dbo.menu.mod_catid = dbo.modcat.id " & _
    '        "WHERE     (dbo.menu.lang_id = @lang_id) AND (dbo.menu.menu_type = @menu_type) AND (dbo.menu.fid = 0) AND (dbo.menu.enabled = 1)  " & _
    '        "ORDER BY dbo.menu.sort"

    '        da.SelectCommand.Parameters.Clear()
    '        da.SelectCommand.Parameters.Add("@lang_id", SqlDbType.Int).Value = langId
    '        da.SelectCommand.Parameters.Add("@menu_type", SqlDbType.NVarChar).Value = "main"
    '        dt.Clear()
    '        da.Fill(dt)
    '        lvwList.DataSource = dt
    '        lvwList.DataBind()
    '    End If
    'End Sub

    'Sub loadSearch()
    '    'Dim pageUnit As String = frontBasePage.getGlobalObject("pageUnit")
    '    'trRetailSearch.Visible = False
    '    'If (pageUnit IsNot Nothing) Then
    '    '    Select Case pageUnit
    '    '        Case "Retail", "AppleShop"
    '    '            Me.Parent.Visible = True
    '    '            trRetailSearch.Visible = True
    '    '    End Select
    '    'End If

    '    'If (trRetailSearch.Visible) Then

    '    '    s_searchCat(0) = cv(Me.ViewState("searchCat1"), cv(Request.QueryString("cid1")))
    '    '    s_searchCat(1) = cv(Me.ViewState("searchCat2"), cv(Request.QueryString("cid2")))
    '    '    s_keyword = cv(Me.ViewState("keyword"), cv(Request.QueryString("keyword")))
    '    '    Dim pg As String = cv(Me.ViewState("pg"), cv(Request.Form("pg"), cv(Request.QueryString("pg"), 1)))
    '    '    Me.ViewState("doSearchPostBack") = 0

    '    '    ' // search
    '    '    loadSearchPostBackItems(False)

    '    '    Dim defaultSearchText As String = "門市名稱"
    '    '    keyword.Value = cv(s_keyword, , defaultSearchText)
    '    '    keyword.Attributes("onfocus") = "if (this.value == '" & defaultSearchText & "') {this.value = '';}"
    '    'End If
    'End Sub

    'Protected Sub lvwList_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
    '    Dim hlkMenu As HtmlAnchor = DirectCast(e.Item.FindControl("hlkMenu"), HtmlAnchor)
    '    Dim href As String = frontBasePage.getMenuLink(e.Item.DataItem, True)
    '    hlkMenu.InnerHtml = e.Item.DataItem("menu_name")
    '    frontBasePage.setMenuOpenMode(hlkMenu, href, e.Item.DataItem, True)

    '    If (basePage.containMenu(e.Item.DataItem("id"), Val(mnuid))) Then
    '        ' // 2nd layer menu
    '        da.SelectCommand.CommandText = "SELECT         dbo.menu.* " & _
    '        "FROM             dbo.menu " & _
    '        "WHERE         (fid = @fid) AND (enabled = 1) " & _
    '        "ORDER BY  sort"

    '        da.SelectCommand.Parameters.Clear()
    '        da.SelectCommand.Parameters.Add("@fid", SqlDbType.Int).Value = e.Item.DataItem("id")
    '        Dim dt2 As New DataTable
    '        da.Fill(dt2)

    '        Dim lvwList2 As ListView = DirectCast(e.Item.FindControl("lvwList2"), ListView)
    '        lvwList2.DataSource = dt2
    '        lvwList2.DataBind()
    '    End If

    '    'If (basePage.containMenu(e.Item.DataItem("id"), mnuid)) Then
    '    '    Dim ul As HtmlGenericControl = DirectCast(lvwList2.FindControl("ul"), HtmlGenericControl)
    '    '    If (ul IsNot Nothing) Then
    '    '        ul.Attributes("class") = "visible"
    '    '    End If
    '    'End If
    'End Sub

    'Protected Sub lvwList2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
    '    Dim hlkMenu As HtmlAnchor = DirectCast(e.Item.FindControl("hlkMenu"), HtmlAnchor)
    '    Dim href As String = frontBasePage.getMenuLink(e.Item.DataItem, True)
    '    hlkMenu.InnerHtml = e.Item.DataItem("menu_name")
    '    frontBasePage.setMenuOpenMode(hlkMenu, href, e.Item.DataItem, True)
    'End Sub
End Class
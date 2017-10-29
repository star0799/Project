Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class page_view
    Inherits FrontBasePage

    Dim cid As String

    Private _showCategory As Boolean
    Public ReadOnly Property showCategory As Boolean
        Get
            Return _showCategory
        End Get
    End Property

    Private _content As String
    Public ReadOnly Property content As String
        Get
            Return _content
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cid = cv(Request.Form(prefixName & "ddlCat"), cv(Request.QueryString("cid")))

        setModCat("modpage")

        validateAccess(objModCatInfo)

        If (cid.Length > 0) Then
            rf(cid, "pagecat", "id", "enabled = 1")
            eq(modid, getColVal("pagecat", "modid", cid))
        End If

        setNavigationPath()

        loadItem()

    End Sub

    Sub loadItem()

        Dim objNavigationClass As New NavigationClass

        '' // drop-down cat
        'ddlCat.Items.Clear()
        'cmd.CommandText = "select * from pagecat where modid = @modid and enabled = 1 order by sort"
        'cmd.Parameters.Clear()
        'cmd.Parameters.Add("@modid", SqlDbType.Int).Value = modid
        'dr = cmd.ExecuteReader
        'While (dr.Read())
        '    ddlCat.Items.Add(New ListItem(dr("cat_name"), dr("id")))
        'End While
        'dr.Close()
        'setDdlVal(ddlCat, cid)

        '_showCategory = (ddlCat.Items.Count > 1)
        'If (cid.Length = 0) Then
        '    NavigateUrl.navigate(NavigateUrl.errorPageUrl)
        'End If

        ' // loading settings for page
        cmd.CommandText = "SELECT         dbo.page.* " & _
        "FROM             dbo.page INNER JOIN " & _
        "                          dbo.pagecat ON dbo.page.catid = dbo.pagecat.id " & _
        "WHERE         (dbo.page.modid = @modid)"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@modid", SqlDbType.Int).Value = modid
        
        dr = cmd.ExecuteReader
        If (dr.Read()) Then

            _content = convStr(dr("content"))

            With objNavigationClass
                .linkUrl = dr("link_href")
                .showLink = (CStr(dr("link_href")).Length > 0)
                .fileUrl = dr("file_name")
                .fileText = dr("file_desc")
                .showFile = (CStr(dr("file_name")).Length > 0)
            End With
            putGlobalObject("navigation", objNavigationClass)
        End If
        dr.Close()
    End Sub

    'Protected Sub ddlCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCat.SelectedIndexChanged
    '    Response.Redirect("page_view.aspx?mnuid=" & mnuid & "&cid=" & cid)
    'End Sub
End Class
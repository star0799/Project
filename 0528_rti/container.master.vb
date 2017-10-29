Imports System.Data
Imports System.Data.Sqlclient

Partial Class container
    Inherits System.Web.UI.MasterPage

    Dim cn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter
    Dim dr As SqlDataReader
    Dim dt As New DataTable
    Dim u As New Utilities(Nothing, cn, cmd, da)

    Private _frontBasePage As FrontBasePage = Nothing
    Protected ReadOnly Property frontBasePage As FrontBasePage
        Get
            If (_frontBasePage Is Nothing) Then
                _frontBasePage = DirectCast(Page, FrontBasePage)
            End If
            Return _frontBasePage
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Me.ID = "master"

        u.openDB()

        Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If (frontBasePage.objLangInfo.subDomainName = "gb") Then
        '    Dim css As HtmlGenericControl
        '    css = New HtmlGenericControl
        '    css.TagName = "style"
        '    css.Attributes.Add("type", "text/css")
        '    css.InnerHtml = "@import ""../css/ch.css"";"
        '    Page.Header.Controls.Add(css)
        'ElseIf (frontBasePage.objLangInfo.subDomainName = "jp") Then
        '    Dim css As HtmlGenericControl
        '    css = New HtmlGenericControl
        '    css.TagName = "style"
        '    css.Attributes.Add("type", "text/css")
        '    css.InnerHtml = "@import ""../css/jp.css"";"
        '    Page.Header.Controls.Add(css)
        'End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        u.closeDB()
    End Sub
End Class
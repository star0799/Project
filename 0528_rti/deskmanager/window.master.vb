Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_window
    Inherits System.Web.UI.MasterPage

    Dim cn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter
    Dim dr As SqlDataReader
    Dim dt As New DataTable
    Dim u As New Utilities(Nothing, cn, cmd, da)

    Private _basePage As BasePage = Nothing
    Protected ReadOnly Property basePage As BasePage
        Get
            If (_basePage Is Nothing) Then
                _basePage = DirectCast(Page, BasePage)
            End If
            Return _basePage
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.ID = "master"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        u.openDB()

        loadItem()

    End Sub

    Sub loadItem()
        Response.Cache.SetNoStore()
        basePage.siteConfig()
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        u.closeDB(cn)
    End Sub
End Class
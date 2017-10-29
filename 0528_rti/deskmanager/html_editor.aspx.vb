Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_html_editor
    Inherits BasePage

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        requireAdminLogin()

        loadItem()

    End Sub

    Sub loadItem()

    End Sub
End Class
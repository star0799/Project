Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_logout
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadItem()

    End Sub

     Sub loadItem()
        Dim objUserController As New ManageUserController(cmd)
        objUserController.signOutCurrentUser()
        Response.Redirect("index.aspx")
    End Sub
End Class
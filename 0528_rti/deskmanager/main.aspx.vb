Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_main
    Inherits BasePage

    Dim m As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        m = cv(Request.QueryString("m"))
        inRange("main", {"main", "top", "footer"})

        requireAdminLogin()
        setNavigationPath()

        loadItem()
        configButton()

    End Sub

    Sub configButton()
        u.hideAllNavButton()
    End Sub

    Sub loadItem()
        If (m.Length > 0) Then
            setCookie("menuType", m, False)
            HttpContext.Current.Items("menuType") = m
        End If
    End Sub
End Class
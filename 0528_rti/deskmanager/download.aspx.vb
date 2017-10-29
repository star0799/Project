Imports System.Data

Partial Class download
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        downloadFile(getQuery("file"))
    End Sub
End Class
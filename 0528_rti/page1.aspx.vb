Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class page1
    Inherits FrontBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadItem()
    End Sub

    Sub loadItem()

        pageTitle = "研究專題區"
    End Sub
End Class
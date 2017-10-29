Imports System.Data
Imports System.Data.SqlClient

Partial Class controls_navigation
    Inherits FrontBaseUserControl

    Public ReadOnly Property pageTitle As String
        Get
            Return convSingleQuote(Page.Header.Title)
        End Get
    End Property

    Public showBack, showLink, showFile As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objNavigationClass As NavigationClass = getGlobalObject("navigation")
        If (objNavigationClass IsNot Nothing) Then
            With objNavigationClass

                ' // back
                showBack = (.showBack AndAlso frontBasePage.layoutMode = "General")

                ' // link href       
                hlkLink.HRef = .linkUrl
                showLink = .showLink

                ' // file name
                hlkFile.HRef = "~/download.aspx?dlfn=" & Server.UrlEncode(.fileUrl)
                hlkFile.Title = .fileText
                showFile = .showFile
            End With
        Else
            Me.Visible = False
        End If
    End Sub
End Class
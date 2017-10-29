Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_img_upload
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        requireAdminLogin()
      
        loadItem()

    End Sub

    Sub loadItem()

    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        Dim imgNameThumb(0) As String
        imgNameThumb(0) = uploadFile(Nothing, Nothing, Request.Files(0), Server.MapPath("../files/"))

        u.rhfPV("filePath", "../files/" & imgNameThumb(0))
        u.doScript("window.opener.document.getElementById('src').value = aspnetForm.filePath.value; window.close();")
    End Sub
End Class
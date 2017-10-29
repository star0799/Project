Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_siteflow_set
    Inherits BasePage

    Dim itemid As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 504

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem()

    End Sub

    Sub loadItem()

        Dim cri As String = Nothing

        u.setFromURL(Me.ViewState)
        u.setInputTextMaxLen("siteflow")

        If (Not Me.IsPostBack) Then

            ' // loading siteflow settings
            cmd.CommandText = "select top 1 * from siteflow"
            dr = cmd.ExecuteReader
            If (dr.Read()) Then
                siteflowCode.Value = dr("siteflow_code")
                siteflowLink.Value = dr("siteflow_link")
            End If
            dr.Close()
        End If
    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        Dim cri As String = Nothing

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try

            cmd.CommandText = "update siteflow set " & _
            "siteflow_code = @siteflow_code, siteflow_link = @siteflow_link"

            cmd.Parameters.Add("@siteflow_code", SqlDbType.NText).Value = DBValueL(Request.Form(prefixName & "siteflowCode"))
            cmd.Parameters.Add("@siteflow_link", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "siteflowLink"))
            cmd.ExecuteNonQuery()

            Dim objEventController As New EventController(cmd)
            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception

            tx.Rollback()
            u.alertAjax(ex.Message)
            Return

        End Try

        Response.Redirect(fullRawUrl)
    End Sub
End Class
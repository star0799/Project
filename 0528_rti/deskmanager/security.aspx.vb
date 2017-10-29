Imports System.Data
Imports System.Data.Sqlclient

Partial Class security
    Inherits BasePage

    Dim itemid, uid As String
   
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 515

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem()
        configButton()

    End Sub

    Sub configButton()
    End Sub

    Sub loadItem()

        If (Not Me.IsPostBack) Then

            cmd.CommandText = "SELECT  d.ip_mask " & _
            "FROM     dbo.unit AS d " & _
            "WHERE  (d.id = @id)"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = authInfo.unitId
            dr = cmd.ExecuteReader
            If (dr.Read()) Then
                ip_mask.Value = getValue(dr("ip_mask"))
            Else
                dr.Close()
                showOops()
            End If
            dr.Close()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (Not Page.IsValid) Then
            Return
        End If

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try
            cmd.CommandText = "update unit set " & _
            "ip_mask = @ip_mask " & _
            "where id = @id"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@ip_mask", SqlDbType.NVarChar).Value = DBValue(ip_mask.Value)
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = authInfo.unitId
            cmd.ExecuteNonQuery()

            tx.Commit()

        Catch ex As Exception
            tx.Rollback()
            showAlert("處理資料時發生意外錯誤!", Request.Url.ToString)
            Return
        End Try

        Response.Redirect(fullRawUrl)
    End Sub
End Class
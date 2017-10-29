Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_site_config
    Inherits BasePage

    Dim itemid As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 501

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem()
        configButton()

    End Sub

    Sub configButton()
        u.hideAllNavButton()
    End Sub

    Sub loadItem()

        u.setInputTextMaxLen("siteconfig")

        If (Not Me.IsPostBack) Then

            ' // loading siteconfig settings
            cmd.CommandText = "select * from siteconfig"
            dr = cmd.ExecuteReader
            If (dr.Read()) Then

                loginTitle.Value = dr("login_title")
                compName.Value = dr("comp_name")
                pageTitle.Value = dr("page_title")

            End If
            dr.Close()
        End If

    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try

            cmd.CommandText = "update siteconfig set " & _
            "login_title = @login_title, comp_name = @comp_name, page_title = @page_title"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@login_title", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "loginTitle"))
            cmd.Parameters.Add("@comp_name", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "compName"))
            cmd.Parameters.Add("@page_title", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "pageTitle"))
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
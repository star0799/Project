Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_user_pwd_edit
    Inherits BasePage

    Dim itemid, catid, uid As String
    Public userId, userName As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 502

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem()
        configButton()

    End Sub

    Sub configButton()
        u.hideAllNavButton()
    End Sub

    Sub loadItem()

        Dim objUserController As New ManageUserController(cmd)
        Dim objUserInfo As ManageUserInfo = objUserController.getCurrentUser()
        If (objUserInfo Is Nothing) Then
            NavigateUrl.navigate(NavigateUrl.loginPageUrl)
        End If

        uid = objUserInfo.ID

        u.setFromURL(Me.ViewState)
        u.setInputTextMaxLen("users")
        u.setInputTextMaxLen("users", "npwd", "pwd", "npwd2", "pwd")

        ' // loading users settings
        cmd.CommandText = "select * from users where id = @id"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = uid

        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            userId = dr("user_id")
            userName = dr("user_name")
        End If

        dr.Close()
    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        'Dim opwd As String = Crypto.Encrypt(Request.Form(prefixName & "pwd"))
        'Dim npwd As String = Crypto.Encrypt(CStr(Request.Form(prefixName & "npwd")).Trim)
        Dim oldPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.Value, "SHA1")
     
        ' // check old pwd
        cmd.CommandText = "select pwd from users where id = @id"
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = uid
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            If (oldPassword <> getValue(dr("pwd")) AndAlso pwd.Value <> getValue(dr("pwd"))) Then
                resetFields()
                u.alertAjax("舊密碼錯誤")
                Return
            End If
        End If
        dr.Close()

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx
        Try
            Dim newPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(npwd.Value, "SHA1")

            cmd.CommandText = "update users set " & _
            "pwd = @pwd " & _
            "where id = @id"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = newPassword
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = uid
            cmd.ExecuteNonQuery()

            Dim objEventController As New EventController(cmd)
            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception

            tx.Rollback()
            u.alertAjax(ex.Message)
            Return

        End Try

        resetFields()
        u.alertAjax("資料修改成功!")
        loadItem()
    End Sub

    Sub resetFields()
        Me.pwd.Value = ""
        Me.npwd.Value = ""
        Me.npwd2.Value = ""
    End Sub
End Class
Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_user_list_edit
    Inherits BasePage

    Dim itemid, uid As String

    Public ReadOnly Property newCreate As Boolean
        Get
            Return (uid.Length = 0)
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 503
        uid = IIf(Request.QueryString("uid") Is Nothing, "", Request.QueryString("uid"))

        requireAdminLogin(itemid)

        If (Not newCreate) Then
            rf(uid, "users", "id")
            eq(authInfo.unitId, getColVal("users", "unit_id", uid))
        End If

        setNavigationPath(itemid)

        loadItem()

    End Sub

    Sub loadItem()

        Dim cri As String = Nothing

        u.setFromURL(Me.ViewState)
        u.setInputTextMaxLen("users")

        ' // group
        If (Not Me.IsPostBack) Then
            group.Items.Clear()

            cmd.Parameters.Clear()
            cri = "1 = 1"
            cmd.CommandText = "select * from usergroup where (" & cri & ") order by readonly DESC, sort"
            dr = cmd.ExecuteReader
            While (dr.Read())
                group.Items.Add(New ListItem(dr("group_name"), dr("id")))
            End While
            dr.Close()
        End If

        If (Not newCreate) Then
            If (Not Me.IsPostBack) Then
                ' // loading users settings
                cmd.CommandText = "select * from users where id = @id"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = uid

                dr = cmd.ExecuteReader
                If (dr.Read()) Then

                    ' // security check
                    If (dr("readonly")) Then
                        dr.Close()
                        Response.Redirect(Me.ViewState("fromURL"))
                    End If

                    group.Value = getValue(dr("group_id"))
                    userId.Value = getValue(dr("user_id"))
                    pwd.Value = Crypto.Decrypt(getValue(dr("pwd")))
                    userName.Value = getValue(dr("user_name"))
                    dept.Value = getValue(dr("dept"))
                    phone.Value = getValue(dr("phone"))
                End If
                dr.Close()
            End If
        End If

        ' // button
        btnSave.Value = IIf(newCreate, "新增", "修改")
    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        Dim userid As String = CStr(Request.Form(prefixName & "userId")).Trim
        Dim pwd As String = Crypto.Encrypt(Request.Form(prefixName & "pwd").Trim)

        ' // check uid
        cmd.Parameters.Clear()
        Dim cri As String = "1 = 1"

        If (Not newCreate) Then
            cri &= " AND id <> @id"
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = uid
        End If

        cmd.CommandText = "SELECT         COUNT(*) AS cnt " & _
        "FROM             dbo.users " & _
        "WHERE         (" & cri & ") AND (user_id = @user_id)"

        cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = userid

        If (cmd.ExecuteScalar > 0) Then
            u.alertAjax("很抱歉，您輸入的帳號 " & userid & " 已經被他人使用，請輸入其他帳號重試。")
            Return
        End If

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx
        Try

            If (newCreate) Then

                cmd.CommandText = "insert into users (" & _
                "unit_id, group_id, user_id, pwd, user_name, user_email, " & _
                "readonly, enabled" & _
                ") values (" & _
                "@unit_id, @group_id, @user_id, @pwd, @user_name, @user_email, " & _
                "@readonly, @enabled" & _
                "); " & _
                "select @@identity"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@unit_id", SqlDbType.Int).Value = authInfo.unitId
                cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@user_email", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@readonly", SqlDbType.Bit).Value = 0
                cmd.Parameters.Add("@enabled", SqlDbType.Bit).Value = 1
                uid = cmd.ExecuteScalar

            End If

            cmd.CommandText = "update users set " & _
            "group_id = @group_id, user_id = @user_id, pwd = @pwd, " & _
            "user_name = @user_name, dept = @dept, phone = @phone " & _
            "where id = @id"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = DBValue(group.Value)
            cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = userid
            cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = pwd
            cmd.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = DBValue(userName.Value)
            cmd.Parameters.Add("@dept", SqlDbType.NVarChar).Value = DBValue(dept.Value)
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = DBValue(phone.Value)
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

        u.alertAjax(Nothing, Me.ViewState("fromURL"))
    End Sub
End Class
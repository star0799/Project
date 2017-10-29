Imports System.Data
Imports System.Data.Sqlclient

<Auth(AuthAttribute.authTypeEnum.Anonymous)> _
Partial Class deskmanager_index
    Inherits BasePage

    Dim done, errcode As String
    Public compName As String
    Public errMsg As String = ""

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ScriptManager.GetCurrent(Me).EnablePartialRendering = False
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        done = IIf(Request.QueryString("done") Is Nothing, "main.aspx", Request.QueryString("done"))
        'errcode = IIf(Request.QueryString("errcode") Is Nothing, "", Request.QueryString("errcode"))

        loadItem()

    End Sub

    Sub loadItem()

        u.setInputTextMaxLen("users")

        Response.Cache.SetNoStore()

        'If (errcode = "not_login") Then
        '    errMsg = "您沒有權限執行此項作業或閒置過久，請重新登入"
        'End If

        ' // loading settings for siteconfig
        cmd.CommandText = "select * from siteconfig"
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            compName = dr("comp_name")
            Page.Header.Title = dr("login_title")
        End If
        dr.Close()

        If (Not Me.IsPostBack AndAlso errMsg.Length > 0) Then
            placeHolder.Controls.Add(New LiteralControl(u.getAlertScriptReference(errMsg)))
        End If

        If (Not Me.IsPostBack) Then
            clearCookieExcept("langCode", "authUserId")
            Session.Clear()
            Session("keepSession") = True
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.ServerClick

        Dim userid, pwd As String
        Dim msg As String = ""
        Dim loginOk As Boolean
        Dim curDate As Date = Now

        userid = cv(Request.Form("userId"))
        pwd = cv(Request.Form("pwd"))

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx
        Try
            cmd.CommandText = "SELECT         dbo.users.*, dbo.usergroup.enabled AS group_enabled,  " & _
            "                          dbo.usergroup.readonly AS group_readonly " & _
            "FROM             dbo.users INNER JOIN " & _
            "                          dbo.usergroup ON dbo.users.group_id = dbo.usergroup.id " & _
            "WHERE         (dbo.users.user_id = @user_id) AND (dbo.users.pwd = @pwd or dbo.users.pwd = @pwd2)"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = userid
            cmd.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = Crypto.Encrypt(pwd)
            cmd.Parameters.Add("@pwd2", SqlDbType.NVarChar).Value = pwd

            dr = cmd.ExecuteReader
            If (dr.Read() = False) Then
                dr.Close()

                msg = "資料錯誤!"

                cmd.CommandText = "insert into userlog (" & _
                "login_key, login_date, logout_date, uid, user_id, " & _
                "ip, login_success" & _
                ") values (" & _
                "@login_key, @login_date, @logout_date, @uid, @user_id, " & _
                "@ip, @login_success" & _
                ")"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@login_key", SqlDbType.UniqueIdentifier).Value = DBNull.Value
                cmd.Parameters.Add("@login_date", SqlDbType.DateTime).Value = curDate
                cmd.Parameters.Add("@logout_date", SqlDbType.DateTime).Value = DBNull.Value
                cmd.Parameters.Add("@uid", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = DBValue(userid)
                cmd.Parameters.Add("@ip", SqlDbType.NVarChar).Value = getRealIP()
                cmd.Parameters.Add("@login_success", SqlDbType.Bit).Value = 0
                cmd.ExecuteNonQuery()

                tx.Commit()

            ElseIf (dr("group_enabled") = False) Then
                msg = "您的帳戶所在群組已被管理者停用，請洽詢系統管理人員尋求協助"
            ElseIf (dr("enabled") = False) Then
                msg = "您的帳戶已被管理者停用，請洽詢系統管理人員尋求協助"
            Else
                Dim unitId As Integer = getValue(Of Integer)(dr("unit_id"))
                Dim adminId As Integer = dr("id")
                Dim groupId As Integer = dr("group_id")
                Dim root As Boolean = dr("group_readonly")
                Dim userName As String = dr("user_name")
                dr.Close()

                'Unit manager
                cmd.CommandText = "select id from unit where admin_uid = @admin_uid"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@admin_uid", SqlDbType.Int).Value = adminId
                dr = cmd.ExecuteReader
                If (dr.Read()) Then
                    unitId = getValue(Of Integer)(dr("id"))
                End If
                dr.Close()

                Dim ip As String = getRealIP()
                Dim unitInfo As UnitInfo = objController.getUnitInfo(unitId)
                If (unitInfo IsNot Nothing AndAlso Not String.IsNullOrEmpty(unitInfo.ipMask)) Then
                    Dim ipList As List(Of String) = getSeparatedValues(unitInfo.ipMask, vbCrLf)
                    If (Not ipList.Any(Function(p) ip.StartsWith(p))) Then
                        showAlert("登入失敗，您的IP不在受信任的來源清單。")
                        resetFields()
                        Return
                    End If
                End If

                If (root) Then

                    cmd.CommandText = "SELECT         TOP 100 PERCENT id " & _
                    "FROM             dbo.lang " & _
                    "WHERE         (published = 1) " & _
                    "ORDER BY  sort"

                    ' // set initial lang
                    setCookie("lang", cmd.ExecuteScalar)
                    loginOk = True

                Else
                    setGroupPerm(groupId, loginOk)

                    ' // set initial lang
                    If (loginOk) Then

                        cmd.CommandText = "SELECT         TOP 1 lang_id " & _
                        "FROM             dbo.groupperm " & _
                        "WHERE         (group_id = @group_id) AND (lang_id IS NOT NULL) " & _
                        "ORDER BY  id"

                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId

                        dr = cmd.ExecuteReader
                        If (dr.Read()) Then
                            setCookie("lang", dr("lang_id"))
                        End If
                        dr.Close()

                    End If
                End If     ' // end if root

                If (loginOk) Then       ' // successful login

                    'Dim unitId As Integer
                    'cmd.CommandText = "select id from unit where admin_uid = @admin_uid"
                    'cmd.Parameters.Clear()
                    'cmd.Parameters.Add("@admin_uid", SqlDbType.Int).Value = adminId
                    'dr = cmd.ExecuteReader
                    'If (dr.Read()) Then
                    '    unitId = getValue(Of Integer)(dr("id"))
                    'Else
                    '    dr.Close()
                    '    showAlert("此帳號無管理權限!", Request.Url.ToString)
                    '    Return
                    'End If
                    'dr.Close()

                    ' // *** account activities log
                    cmd.CommandText = "SELECT     COUNT(*) AS cnt " & _
                    "FROM         dbo.userlogdetail " & _
                    "WHERE     (log_date < DATEADD(d, -30, GETDATE()))"

                    If (cmd.ExecuteScalar > 0) Then

                        cmd.CommandText = "DELETE FROM userlogdetail " & _
                        "WHERE     (log_date < DATEADD(d, -30, GETDATE()))"

                        cmd.ExecuteNonQuery()
                    End If

                    cmd.CommandText = "insert into userlog (" & _
                    "unit_id, login_key, login_date, logout_date, uid, user_id, " & _
                    "ip, login_success" & _
                    ") values (" & _
                    "@unit_id, @login_key, @login_date, @logout_date, @uid, @user_id, " & _
                    "@ip, @login_success" & _
                    "); " & _
                    "select @@identity"

                    cmd.Parameters.Clear()

                    Dim loginKey As Guid = Guid.NewGuid
                    cmd.Parameters.Add("@unit_id", SqlDbType.Int).Value = unitId
                    cmd.Parameters.Add("@login_key", SqlDbType.UniqueIdentifier).Value = loginKey
                    cmd.Parameters.Add("@login_date", SqlDbType.DateTime).Value = curDate
                    cmd.Parameters.Add("@logout_date", SqlDbType.DateTime).Value = DBNull.Value
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Value = adminId
                    cmd.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = userid
                    cmd.Parameters.Add("@ip", SqlDbType.NVarChar).Value = getRealIP()
                    cmd.Parameters.Add("@login_success", SqlDbType.Bit).Value = 1
                    Dim userLogId As Integer = cmd.ExecuteScalar()

                    tx.Commit()

                    setCookie("authId", String.Format("{0}{6}{1}{6}{2}{6}{3}{6}{4}{6}{5}", loginKey.ToString, adminId, userLogId, unitId, userid, userName, Chr(1)), Now.AddMinutes(cookieReserveMin))
                    setCookie("menuType", "main", False)
                    u.alertAjax(Nothing, done)
                    Return
                Else
                    ' // no any manage permissions for all lang sites
                    msg = "您帳號所在的群組並無任何管理權限，請洽詢系統管理人員尋求協助"
                End If
                End If
                dr.Close()

        Catch ex As Exception
            tx.Rollback()
            showAlert("處理登入程序時發生意外錯誤!", fullRawUrl)
            Return
        End Try

        resetFields()
        showAlert(msg)
    End Sub

    Sub setGroupPerm(ByVal groupId As Integer, ByRef loginOk As Boolean)

        Dim msg As String = ""
        Dim x, y As Integer
        Dim fItem As New ArrayList
        Dim fLangItem As New ArrayList
        Dim mnuid As New ArrayList
        Dim dt2 As New DataTable

        ' // fixed item menus
        cmd.CommandText = "SELECT         TOP 100 PERCENT dbo.groupperm.* " & _
        "FROM             dbo.groupperm " & _
        "WHERE         (group_id = @group_id) AND (lang_id IS NULL)"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId

        dr = cmd.ExecuteReader
        While (dr.Read())
            loginOk = True
            fItem.Add(CInt(dr("item_id")))
        End While
        dr.Close()

        Session("fixedItem") = fItem

        ' // loading settings for groupperm
        da.SelectCommand.CommandText = "SELECT DISTINCT TOP 100 PERCENT lang_id " & _
        "FROM             dbo.groupperm " & _
        "WHERE         (group_id = @group_id) AND (lang_id IS NOT NULL) "

        da.SelectCommand.Parameters.Clear()
        da.SelectCommand.Parameters.Add("@group_id", SqlDbType.NVarChar).Value = groupId

        dt.Clear()
        da.Fill(dt)

        With dt
            For x = 0 To .Rows.Count - 1

                loginOk = True

                ' // fixed menus width lang differentiation
                fLangItem.Clear()

                cmd.CommandText = "SELECT         dbo.groupperm.* " & _
                "FROM             dbo.groupperm INNER JOIN " & _
                "                          dbo.menunav ON dbo.groupperm.item_id = dbo.menunav.item_id " & _
                "WHERE         (dbo.groupperm.group_id = @group_id) AND (dbo.groupperm.lang_id = @lang_id)"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId
                cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = .Rows(x)("lang_id")

                dr = cmd.ExecuteReader
                While (dr.Read())
                    fLangItem.Add(CInt(dr("item_id")))
                End While
                dr.Close()

                Session(.Rows(x)("lang_id") & "_fixedLangItem") = fLangItem.Clone

                ' // *** dynamic generated menus
                mnuid.Clear()

                ' // 1st layer menu
                da.SelectCommand.CommandText = "SELECT         dbo.groupperm.* " & _
                "FROM             dbo.groupperm INNER JOIN " & _
                "                          dbo.menu ON dbo.groupperm.item_id = dbo.menu.id " & _
                "WHERE         (dbo.groupperm.group_id = @group_id) AND (dbo.groupperm.lang_id = @lang_id) AND  " & _
                "                          (dbo.menu.menu_level = 1)"

                da.SelectCommand.Parameters.Clear()
                da.SelectCommand.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId
                da.SelectCommand.Parameters.Add("@lang_id", SqlDbType.Int).Value = .Rows(x)("lang_id")

                dt2.Clear()
                da.Fill(dt2)

                With dt2
                    For y = 0 To .Rows.Count - 1

                        mnuid.Add(CInt(.Rows(y)("item_id")))

                    Next y
                End With

                ' // 2nd layer menu
                da.SelectCommand.CommandText = "SELECT         dbo.groupperm.* " & _
                "FROM             dbo.groupperm INNER JOIN " & _
                "                          dbo.menu ON dbo.groupperm.item_id = dbo.menu.id " & _
                "WHERE         (dbo.groupperm.group_id = @group_id) AND (dbo.groupperm.lang_id = @lang_id) AND  " & _
                "                          (dbo.menu.menu_level = 2)"

                da.SelectCommand.Parameters.Clear()
                da.SelectCommand.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId
                da.SelectCommand.Parameters.Add("@lang_id", SqlDbType.Int).Value = .Rows(x)("lang_id")

                dt2.Clear()
                da.Fill(dt2)

                With dt2
                    For y = 0 To .Rows.Count - 1

                        mnuid.Add(CInt(.Rows(y)("item_id")))

                    Next y
                End With

                ' // 3rd layer menu
                da.SelectCommand.CommandText = "SELECT         dbo.groupperm.* " & _
                "FROM             dbo.groupperm INNER JOIN " & _
                "                          dbo.menu ON dbo.groupperm.item_id = dbo.menu.id " & _
                "WHERE         (dbo.groupperm.group_id = @group_id) AND (dbo.groupperm.lang_id = @lang_id) AND  " & _
                "                          (dbo.menu.menu_level = 3)"

                da.SelectCommand.Parameters.Clear()
                da.SelectCommand.Parameters.Add("@group_id", SqlDbType.Int).Value = groupId
                da.SelectCommand.Parameters.Add("@lang_id", SqlDbType.Int).Value = .Rows(x)("lang_id")

                dt2.Clear()
                da.Fill(dt2)

                With dt2
                    For y = 0 To .Rows.Count - 1

                        mnuid.Add(CInt(.Rows(y)("item_id")))

                    Next y
                End With

                Session(.Rows(x)("lang_id") & "_mnuid") = mnuid.Clone
            Next x
        End With
    End Sub

    Sub resetFields()
        Me.userId.Value = ""
        Me.pwd.Value = ""
    End Sub
End Class
Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_user_log
    Inherits BasePage

    Dim itemid, cid, pg As String
    Dim s_searchCat, s_loginStdate, s_loginEddate, s_userId, s_ip As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        itemid = 508
        cid = IIf(Request.Form(prefixName & "ddlCat") Is Nothing, IIf(Request.QueryString("cid") Is Nothing, "", Request.QueryString("cid")), Request.Form(prefixName & "ddlCat"))

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem(False)
        configButton()

    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)

        Dim curDate As Date = DateTime.Now
        Dim cri, oSearchCri As String, searchCri As String = ""

        s_searchCat = IIf(Me.ViewState("searchCat") Is Nothing, "", Me.ViewState("searchCat"))
        s_loginStdate = IIf(Me.ViewState("loginStdate") Is Nothing, "", Me.ViewState("loginStdate"))
        s_loginEddate = IIf(Me.ViewState("loginEddate") Is Nothing, "", Me.ViewState("loginEddate"))
        s_userId = IIf(Me.ViewState("userId") Is Nothing, "", Me.ViewState("userId"))
        s_ip = IIf(Me.ViewState("ip") Is Nothing, "", Me.ViewState("ip"))
        pg = IIf(Me.ViewState("pg") Is Nothing, IIf(Request.Form("pg") Is Nothing, IIf(Request.QueryString("pg") Is Nothing, 1, Request.QueryString("pg")), Request.Form("pg")), Me.ViewState("pg"))

        ' // search
        loginStdate.Value = s_loginStdate
        loginEddate.Value = s_loginEddate
        userId.Value = s_userId
        ip.Value = s_ip


        da.SelectCommand.Parameters.Clear()
        cri = "dbo.userlog.unit_id = @unit_id AND login_success = 1"
        da.SelectCommand.Parameters.Add("@unit_id", SqlDbType.Int).Value = authInfo.unitId

        If (Not Me.ViewState("doSearch") Is Nothing AndAlso Me.ViewState("doSearch") = 1) Then

            searchCri = "1 = 1"
            oSearchCri = searchCri

            ' // login date
            If (s_loginStdate.Length > 0 OrElse s_loginEddate.Length > 0) Then
                If (s_loginStdate.Length > 0 AndAlso s_loginEddate.Length = 0) Then
                    searchCri &= " AND userlog.login_date >= @login_stdate"
                    da.SelectCommand.Parameters.Add("@login_stdate", SqlDbType.DateTime).Value = s_loginStdate
                ElseIf (s_loginStdate.Length = 0 AndAlso s_loginEddate.Length > 0) Then
                    searchCri &= " AND userlog.login_date < @login_eddate"
                    da.SelectCommand.Parameters.Add("@login_eddate", SqlDbType.DateTime).Value = CDate(s_loginEddate).AddDays(1)
                ElseIf (s_loginStdate.Length > 0 AndAlso s_loginEddate.Length > 0) Then
                    searchCri &= " AND userlog.login_date >= @login_stdate AND userlog.login_date < @login_eddate"
                    da.SelectCommand.Parameters.Add("@login_stdate", SqlDbType.DateTime).Value = s_loginStdate
                    da.SelectCommand.Parameters.Add("@login_eddate", SqlDbType.DateTime).Value = CDate(s_loginEddate).AddDays(1)
                End If
            End If

            ' // user id
            If (s_userId.Length > 0) Then
                searchCri &= " AND dbo.userlog.user_id LIKE @user_id"
                da.SelectCommand.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = "%" & s_userId & "%"
            End If

            ' // ip
            If (s_ip.Length > 0) Then
                searchCri &= " AND userlog.ip LIKE @ip"
                da.SelectCommand.Parameters.Add("@ip", SqlDbType.NVarChar).Value = "%" & s_ip & "%"
            End If
            
            Me.ViewState("doSearch") = -CInt(oSearchCri <> searchCri)
        End If

        cri &= IIf(searchCri.Length > 0, " AND " & searchCri, "")


        da.SelectCommand.CommandText = "SELECT     TOP (100) PERCENT dbo.userlog.* " & _
        "FROM         dbo.userlog " & _
        "WHERE         (" & cri & ") " & _
        "ORDER BY dbo.userlog.id DESC"

        da.SelectCommand.Parameters.Add("@now", SqlDbType.DateTime).Value = curDate

        u.makeList(dt, 10, pg, Me.ViewState)

        If (Not Me.IsPostBack OrElse doBind) Then
            gvwBind()
        Else
            gvwDataBound(gvwList, Me.ViewState)
        End If

    End Sub

    Sub configButton()
        'u.showNavButton("Search", True)
    End Sub

    Sub gvwBind()
        gvwList.PageSize = Math.Max(u.showRows, 1)
        gvwList.DataSourceID = Nothing
        gvwList.DataSource = dt
        gvwList.DataBind()
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        u.closeDB()
    End Sub

    Protected Sub gvwList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwList.DataBound
        gvwDataBound(gvwList, Me.ViewState)
    End Sub

    Protected Sub gvwList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwList.RowDataBound
        gvwRowDataBound(e, u, Me.ViewState, gvwList, dt, False)

        If (Me.ViewState("noItem") = 0) Then
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow

                    ' // login date
                    CType(e.Row.FindControl("lblLoginDate"), Label).Text = formatDHMS(dt.Rows(e.Row.RowIndex)("login_date"))

                    ' // logout date
                    CType(e.Row.FindControl("lblLogoutDate"), Label).Text = convVal(formatDHMS(dt.Rows(e.Row.RowIndex)("logout_date")), , spc(1))
            End Select
        End If

    End Sub

    Protected Sub gvwList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvwList.RowCommand
        gvwRowCommand(sender, e, u, Me.ViewState, pg, "userlog", dt, Nothing)

        If (e.CommandName = "DownLog") Then
            Dim objEventController As New EventController(cmd)
            objEventController.downloadLog(Convert.ToInt32(e.CommandArgument))
        End If

        loadItem()
    End Sub

    Protected Sub ddlPageList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.ViewState("pg") = CType(sender, DropDownList).SelectedValue
        loadItem()
    End Sub

    Protected Sub btnSearch_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.ServerClick

        Me.ViewState("loginStdate") = IIf(Request.Form(prefixName & "loginStdate") Is Nothing, "", Request.Form(prefixName & "loginStdate"))
        Me.ViewState("loginEddate") = IIf(Request.Form(prefixName & "loginEddate") Is Nothing, "", Request.Form(prefixName & "loginEddate"))
        Me.ViewState("userId") = IIf(Request.Form(prefixName & "userId") Is Nothing, "", Request.Form(prefixName & "userId"))
        Me.ViewState("ip") = IIf(Request.Form(prefixName & "ip") Is Nothing, "", Request.Form(prefixName & "ip"))

        Me.ViewState("pg") = 1
        Me.ViewState("doSearch") = 1
        loadItem()
    End Sub

    Sub clearSearch()
        If (Not Me.ViewState("doSearch") Is Nothing AndAlso Me.ViewState("doSearch") = 1) Then
            Me.ViewState("loginStdate") = ""
            Me.ViewState("loginEddate") = ""
            Me.ViewState("userId") = ""
            Me.ViewState("ip") = ""
            
            Me.ViewState("pg") = 1
            Me.ViewState("doSearch") = 0
        End If
    End Sub
End Class
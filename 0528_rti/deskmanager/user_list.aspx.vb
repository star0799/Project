Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_user_list
    Inherits BasePage

    Dim objUserInfo As ManageUserInfo

    Dim itemid, gid, pg As String
    Dim s_searchCat, s_userId, s_userName, s_enabled As String
  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        itemid = 503
        gid = IIf(Request.Form(prefixName & "ddlGroup") Is Nothing, "", Request.Form(prefixName & "ddlGroup"))

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem(False)
        configButton()

    End Sub

    Sub configButton()

        AddHandler Master.navAddClick, AddressOf btnAdd_ServerClick

        u.showNavButton("Search")
        u.showNavButton("Add", True)

    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)

        Dim curDate As Date = DateTime.Now
        Dim cri, oSearchCri As String, searchCri As String = ""

        s_searchCat = IIf(Me.ViewState("searchCat") Is Nothing, "", Me.ViewState("searchCat"))
        s_userId = IIf(Me.ViewState("userId") Is Nothing, "", Me.ViewState("userId"))
        s_userName = IIf(Me.ViewState("userName") Is Nothing, "", Me.ViewState("userName"))
        s_enabled = IIf(Me.ViewState("enabled") Is Nothing, "", Me.ViewState("enabled"))
        pg = IIf(Me.ViewState("pg") Is Nothing, IIf(Request.Form("pg") Is Nothing, IIf(Request.QueryString("pg") Is Nothing, 1, Request.QueryString("pg")), Request.Form("pg")), Me.ViewState("pg"))

        Dim objUserController As New ManageUserController(cmd)
        objUserInfo = objUserController.getCurrentUser()
        If (objUserInfo Is Nothing) Then
            NavigateUrl.navigate(NavigateUrl.loginPageUrl)
        End If

        ' // group
        ddlGroup.Items.Clear()
        ddlGroup.Items.Add(New ListItem("全部", ""))

        cmd.Parameters.Clear()
        cri = "unit_id = @unit_id"
        cmd.Parameters.Add("@unit_id", SqlDbType.Int).Value = authInfo.unitId

        cmd.CommandText = "select * from usergroup where (" & cri & ") order by readonly DESC, sort"
        dr = cmd.ExecuteReader
        While (dr.Read())
            ddlGroup.Items.Add(New ListItem(dr("group_name"), dr("id")))
        End While
        dr.Close()

        If (ddlGroup.Items.FindByValue(gid) Is Nothing) Then
            gid = ddlGroup.SelectedValue
        End If
        ddlGroup.SelectedValue = gid


        ' // search
        searchCat.Items.Clear()
        searchCat.Items.Add(New ListItem("不拘", ""))

        cmd.Parameters.Clear()
        cri = "unit_id = @unit_id"
        cmd.Parameters.Add("@unit_id", SqlDbType.Int).Value = authInfo.unitId

        cmd.CommandText = "select * from usergroup where (" & cri & ") order by readonly DESC, sort"
        dr = cmd.ExecuteReader
        While (dr.Read())
            searchCat.Items.Add(New ListItem(dr("group_name"), dr("id")))
        End While
        dr.Close()
        searchCat.Value = s_searchCat

        userId.Value = s_userId
        userName.Value = s_userName
        enabled.Value = s_enabled


        da.SelectCommand.Parameters.Clear()
        cri = "users.unit_id = @unit_id"
        da.SelectCommand.Parameters.Add("@unit_id", SqlDbType.Int).Value = authInfo.unitId

        If (gid.Length > 0 AndAlso (Me.ViewState("doSearch") Is Nothing OrElse Me.ViewState("doSearch") = 0)) Then
            ' // drop-down group
            cri &= " AND group_id = @group_id"
            da.SelectCommand.Parameters.Add("@group_id", SqlDbType.Int).Value = gid
        End If

        If (Not Me.ViewState("doSearch") Is Nothing AndAlso Me.ViewState("doSearch") = 1) Then

            searchCri = "1 = 1"
            oSearchCri = searchCri

            ' // drop-down group
            ddlGroup.SelectedValue = IIf(s_searchCat.Length > 0, s_searchCat, "")

            ' // search cat
            If (s_searchCat.Length > 0) Then
                searchCri &= " AND group_id = @group_id"
                da.SelectCommand.Parameters.Add("@group_id", SqlDbType.Int).Value = s_searchCat
            End If

            ' // user id
            If (s_userId.Length > 0) Then
                searchCri &= " AND users.user_id LIKE @user_id"
                da.SelectCommand.Parameters.Add("@user_id", SqlDbType.NVarChar).Value = "%" & s_userId & "%"
            End If

            ' // user name
            If (s_userName.Length > 0) Then
                searchCri &= " AND users.user_name LIKE @user_name"
                da.SelectCommand.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = "%" & s_userName & "%"
            End If

            ' // enabled
            If (s_enabled.Length > 0) Then
                searchCri &= " AND users.enabled = @enabled"
                da.SelectCommand.Parameters.Add("@enabled", SqlDbType.Bit).Value = IIf(s_enabled = "Y", 1, 0)
            End If

            Me.ViewState("doSearch") = -CInt(oSearchCri <> searchCri)
        End If

        cri &= IIf(searchCri.Length > 0, " AND " & searchCri, "")


        da.SelectCommand.CommandText = "SELECT         TOP 100 PERCENT dbo.users.*, dbo.usergroup.group_name " & _
        "FROM             dbo.users INNER JOIN " & _
        "                          dbo.usergroup ON dbo.users.group_id = dbo.usergroup.id " & _
        "WHERE         (" & cri & ") " & _
        "ORDER BY  users.user_id"

        u.makeList(dt, 50, pg, Me.ViewState)

        If (Not Me.IsPostBack OrElse doBind) Then
            gvwBind()
        ElseIf (Me.IsPostBack) Then
            gvwDataBound(gvwList, Me.ViewState)
        End If
        If (Not Me.IsPostBack) Then
            u.setInputTextMaxLen("users")
        End If

    End Sub

    Sub gvwBind()
        gvwList.PageSize = Math.Max(u.showRows, 1)
        gvwList.DataSourceID = Nothing
        gvwList.DataSource = dt
        gvwList.DataBind()
    End Sub

    Protected Sub gvwList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwList.DataBound
        gvwDataBound(gvwList, Me.ViewState)
    End Sub

    Protected Sub gvwList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwList.RowDataBound
        gvwRowDataBound(e, u, Me.ViewState, gvwList, dt, False)

        If (Me.ViewState("noItem") = 0) Then
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow

                    ' // delete
                    If (dt.Rows(e.Row.RowIndex)("readonly") OrElse objUserInfo.ID = dt.Rows(e.Row.RowIndex)("id")) Then
                        CType(e.Row.FindControl("chkSelect"), CheckBox).Enabled = False
                    End If

                    ' // user name
                    If (dt.Rows(e.Row.RowIndex)("readonly") ) Then
                        CType(e.Row.FindControl("hlkUserId"), HyperLink).NavigateUrl = Nothing
                    Else
                        CType(e.Row.FindControl("hlkUserId"), HyperLink).NavigateUrl = "user_list_edit.aspx?uid=" & dt.Rows(e.Row.RowIndex)("id")
                    End If

                    ' // group
                    CType(e.Row.FindControl("lblGroupName"), Label).Text = dt.Rows(e.Row.RowIndex)("group_name")

                    ' // enabled
                    CType(e.Row.FindControl("chkEnabled"), CheckBox).Checked = dt.Rows(e.Row.RowIndex)("enabled")
                    If (dt.Rows(e.Row.RowIndex)("readonly")) Then
                        CType(e.Row.FindControl("chkEnabled"), CheckBox).Enabled = False
                    End If
            End Select
        End If

    End Sub

    Protected Sub gvwList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvwList.RowCommand
        gvwRowCommand(sender, e, u, Me.ViewState, pg, "users", dt, Nothing)

        If (e.CommandName = "DeleteItem") Then
            delItem()
        End If

        loadItem()
    End Sub

    Protected Sub ddlPageList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.ViewState("pg") = CType(sender, DropDownList).SelectedValue
        loadItem()
    End Sub

    Sub delItem()

        Dim objEventController As New EventController(cmd)

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        Try
            For Each x As GridViewRow In gvwList.Rows
                Dim c As CheckBox = CType(x.FindControl("chkSelect"), CheckBox)

                If (c.Checked) Then
                    Dim pk As Integer = gvwList.DataKeys(x.RowIndex).Value

                    objEventController.addDeleteLog("users", "user_name", pk)

                    cmd.CommandText = "delete from users where id = @id"
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pk
                    cmd.ExecuteNonQuery()

                End If
            Next

            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception
            tx.Rollback()
            Throw ex
            Return
        End Try
    End Sub

    Protected Sub chkEnabled_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim c As CheckBox
        Dim pk As String

        c = CType(sender, CheckBox)
        pk = gvwList.DataKeys(CType(c.Parent.Parent, GridViewRow).RowIndex).Value

        cmd.CommandText = "update users set enabled=" & -c.Checked & " where id =" & pk
        cmd.ExecuteNonQuery()

        loadItem()
    End Sub

    Protected Sub btnSearch_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.ServerClick

        Me.ViewState("searchCat") = IIf(Request.Form(prefixName & "searchCat") Is Nothing, "", Request.Form(prefixName & "searchCat"))
        Me.ViewState("userId") = IIf(Request.Form(prefixName & "userId") Is Nothing, "", Request.Form(prefixName & "userId"))
        Me.ViewState("userName") = IIf(Request.Form(prefixName & "userName") Is Nothing, "", Request.Form(prefixName & "userName"))
        Me.ViewState("enabled") = IIf(Request.Form(prefixName & "enabled") Is Nothing, "", Request.Form(prefixName & "enabled"))

        Me.ViewState("pg") = 1
        Me.ViewState("doSearch") = 1
        loadItem()
    End Sub

    Sub clearSearch()
        If (Not Me.ViewState("doSearch") Is Nothing AndAlso Me.ViewState("doSearch") = 1) Then
            Me.ViewState("searchCat") = ""
            Me.ViewState("userId") = ""
            Me.ViewState("userName") = ""
            Me.ViewState("enabled") = ""

            Me.ViewState("pg") = 1
            Me.ViewState("doSearch") = 0
        End If
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        clearSearch()
        loadItem()
    End Sub

    Protected Sub btnGroup_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroup.ServerClick
        Response.Redirect("group_main.aspx")
    End Sub

    Protected Sub btnAdd_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("user_list_edit.aspx?gid=" & gid)
    End Sub
End Class
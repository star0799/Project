Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_page_cat
    Inherits BasePage

    Dim actionFlag, catName, editId, pg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        actionFlag = IIf(Request.Form("actionFlag") Is Nothing, "", Request.Form("actionFlag"))
        catName = IIf(Request.Form("catName") Is Nothing, "", Request.Form("catName"))
        editId = IIf(Request.Form("editId") Is Nothing, "", Request.Form("editId"))

        setModCat("modpage")

        requireAdminLogin(objModCatInfo)
        validateAccess(objModCatInfo)
        setNavigationMenuPath(objModCatInfo)

        loadItem(False)
        configButton()

    End Sub

    Sub configButton()

        Dim a As HtmlAnchor = CType(FindControlEx(Me, "hlkNavAdd"), HtmlAnchor)

        AddHandler Master.navBackClick, AddressOf btnBack_ServerClick
        AddHandler Master.navAddClick, AddressOf btnAdd_ServerClick

        u.showNavButton("Back")
        u.showNavButton("Add", True)

        ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(a)
        a.Attributes("onclick") = "return doAdd();"

    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)

        pg = IIf(Me.ViewState("pg") Is Nothing, IIf(Request.Form("pg") Is Nothing, IIf(Request.QueryString("pg") Is Nothing, 1, Request.QueryString("pg")), Request.Form("pg")), Me.ViewState("pg"))
        u.rhfPV("actionFlag", "", "catName", "", "catNameMaxLen", u.getInputTextMaxLen("pagecat", "cat_name"), "catText", "頁面名稱", "editId", "")

        da.SelectCommand.CommandText = "SELECT         TOP 100 PERCENT dbo.pagecat.* " & _
        "FROM             dbo.pagecat " & _
        "WHERE         (modid = @modid) " & _
        "ORDER BY  sort"

        da.SelectCommand.Parameters.Clear()
        da.SelectCommand.Parameters.Add("@modid", SqlDbType.Int).Value = modid

        u.makeList(dt, 10, pg, Me.ViewState)

        If (Not Me.IsPostBack OrElse doBind) Then
            gvwBind()
        Else
            gvwDataBound(gvwList, Me.ViewState)
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
        gvwRowDataBound(e, u, Me.ViewState, gvwList, dt)

        If (Me.ViewState("noItem") = 0) Then
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow

                    ' // delete
                    CType(e.Row.FindControl("chkSelect"), CheckBox).Enabled = ((u.curPage - 1) * u.rowsPerPage + e.Row.RowIndex > 0)

                    ' // page name
                    CType(e.Row.FindControl("hlkCatName"), HyperLink).NavigateUrl = "javascript:;"
                    CType(e.Row.FindControl("hlkCatName"), HyperLink).Attributes("onclick") = "return doEdit('" & convSingleQuote(dt.Rows(e.Row.RowIndex)("cat_name")) & "', " & dt.Rows(e.Row.RowIndex)("id") & ");"

                    ' // path
                    CType(e.Row.FindControl("txtPath"), TextBox).Text = "page_view.aspx?modid=" & dt.Rows(e.Row.RowIndex)("modid") & "&cid=" & dt.Rows(e.Row.RowIndex)("id")
                    CType(e.Row.FindControl("btnCopyPath"), HtmlInputButton).Attributes("onclick") = "copyPath(" & CType(e.Row.FindControl("txtPath"), TextBox).ClientID & ");"

                    ' // enabled
                    CType(e.Row.FindControl("chkEnabled"), CheckBox).Checked = dt.Rows(e.Row.RowIndex)("enabled")

            End Select
        End If
    End Sub

    Protected Sub gvwList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvwList.RowCommand
        gvwRowCommand(sender, e, u, Me.ViewState, pg, "pagecat", dt, Nothing)

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

                    ' // image
                    deleteUploadFiles(objModCatInfo, objModCatInfo.contentTableName, "catid", pk, "../files/")

                    objEventController.addDeleteLog("pagecat", "cat_name", pk)

                    cmd.CommandText = "delete from page where catid = @catid"
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@catid", SqlDbType.Int).Value = pk
                    cmd.ExecuteNonQuery()

                    ' // page cat
                    cmd.CommandText = "delete from pagecat where id = @id"
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

        cmd.CommandText = "update pagecat set enabled=" & -c.Checked & " where id =" & pk
        cmd.ExecuteNonQuery()

        loadItem()
    End Sub

    Protected Sub btnAdd_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.ServerClick

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try
            If (actionFlag = "Add") Then

                Dim maxsort As Integer = u.getMaxSort("pagecat")

                ' // page cat
                cmd.CommandText = "insert into pagecat (" & _
                "modid, sort, cat_name, enabled" & _
                ") values (" & _
                "@modid, @sort, @cat_name, @enabled" & _
                "); " & _
                "select @@identity"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@modid", SqlDbType.Int).Value = modid
                cmd.Parameters.Add("@sort", SqlDbType.Int).Value = maxsort
                cmd.Parameters.Add("@cat_name", SqlDbType.NVarChar).Value = catName
                cmd.Parameters.Add("@enabled", SqlDbType.Bit).Value = 1
                Dim maxid As Integer = cmd.ExecuteScalar

                ' // page content
                cmd.CommandText = "insert into page (" & _
                "modid, catid, content, link_href, " & _
                "img_name_thumb, img_desc, img_align, file_name, file_desc, " & _
                "enabled" & _
                ") values (" & _
                "@modid, @catid, @content, @link_href, " & _
                "@img_name_thumb, @img_desc, @img_align, @file_name, @file_desc, " & _
                "@enabled" & _
                ")"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@modid", SqlDbType.Int).Value = modid
                cmd.Parameters.Add("@catid", SqlDbType.Int).Value = maxid
                cmd.Parameters.Add("@content", SqlDbType.NText).Value = ""
                cmd.Parameters.Add("@link_href", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@img_name_thumb", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@img_desc", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@img_align", SqlDbType.NVarChar).Value = "left"
                cmd.Parameters.Add("@file_name", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@file_desc", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@enabled", SqlDbType.Bit).Value = 1
                cmd.ExecuteNonQuery()

            ElseIf (actionFlag = "Edit") Then

                cmd.CommandText = "update pagecat set " & _
                "cat_name = @cat_name " & _
                "where id = @id"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@cat_name", SqlDbType.NVarChar).Value = catName
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = editId
                cmd.ExecuteNonQuery()

            End If

            Dim objEventController As New EventController(cmd)
            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception

            tx.Rollback()
            u.alertAjax("儲存資料時發生意外錯誤!")
            Return

        End Try

        loadItem()
    End Sub

    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("page_edit.aspx?mnuid=" & mnuid)
    End Sub
End Class
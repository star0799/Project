Imports System.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Class arch_config
    Inherits BasePage

    Dim itemid As String
  
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        itemid = 1310

        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
        configButton()
    End Sub

    Sub configButton()
        AddHandler Master.navBackClick, AddressOf btnBack_ServerClick

        u.showNavButton("Back", True)
    End Sub

    Private Sub loadItem()

        If (Not Me.IsPostBack) Then
            loadSearchOptions(ddlCat, "arch", "cat")
        End If
    End Sub

    Protected Sub radList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radList.ItemCommand
        Select Case e.CommandName
            Case RadGrid.InitInsertCommandName
                DirectCast(sender, RadGrid).MasterTableView.ClearEditItems()
            Case RadGrid.EditCommandName, RadGrid.PageCommandName
                e.Item.OwnerTableView.IsItemInserted = False

            Case RadGrid.PerformInsertCommandName, RadGrid.UpdateCommandName, RadGrid.DeleteCommandName

                tx = cn.BeginTransaction
                cmd.Transaction = tx
                da.SelectCommand.Transaction = tx
                Try
                    Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)

                    Dim itemName As String
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        itemName = getValue(item.GetDataKeyValue("name"))
                    Else
                        itemName = DBValue(DirectCast(item.FindControl("item_name"), HtmlInputText).Value)
                    End If

                    Dim relationId As Integer
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        relationId = item.GetDataKeyValue("id")
                        cmd.CommandText = "delete from setting where id = @id"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        cmd.ExecuteNonQuery()
                    Else
                        If (Not Page.IsValid) Then
                            Return
                        End If

                        cmd.CommandText = "select count(*) from setting where table_name='arch' and field_name = @field_name and name = @name"
                        cmd.Parameters.Clear()
                        If (e.CommandName = RadGrid.UpdateCommandName OrElse e.CommandName = RadGrid.DeleteCommandName) Then
                            cmd.CommandText &= " and id <> @id"
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        End If

                        cmd.Parameters.Add("@field_name", SqlDbType.NVarChar).Value = ddlCat.SelectedValue
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = DBValue(itemName)
                        If (cmd.ExecuteScalar > 0) Then
                            e.Canceled = True
                            showAlert(String.Format("項目名稱 '{0}' 已經存在!", itemName))
                            Return
                        End If

                        If (e.CommandName = RadGrid.PerformInsertCommandName) Then
                            Dim maxsort As Integer = u.getMaxSort("setting", "table_name='arch' and field_name='" & ddlCat.SelectedValue & "'")

                            cmd.CommandText = "insert into setting (" & _
                            "table_name, field_name, sort, name, value, " & _
                            "create_user_id" & _
                            ") values (" & _
                            "@table_name, @field_name, @sort, @name, @value, " & _
                            "@create_user_id" & _
                            "); " & _
                            "select SCOPE_IDENTITY()"

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = "arch"
                            cmd.Parameters.Add("@field_name", SqlDbType.NVarChar).Value = ddlCat.SelectedValue
                            cmd.Parameters.Add("@sort", SqlDbType.Int).Value = maxsort
                            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = itemName
                            cmd.Parameters.Add("@value", SqlDbType.NVarChar).Value = maxsort
                            cmd.Parameters.Add("@create_user_id", SqlDbType.NVarChar).Value = authInfo.userId
                            relationId = cmd.ExecuteScalar()
                        Else
                            relationId = item.GetDataKeyValue("id")

                            cmd.CommandText = "update setting set " & _
                            "name = @name " & _
                            "where id = @id"

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = itemName
                            cmd.ExecuteNonQuery()
                        End If
                    End If

                    tx.Commit()

                Catch ex As Exception
                    tx.Rollback()
                    showAlert("處理資料時發生意外錯誤!", Request.Url.ToString)
                    Return
                End Try
        End Select
    End Sub

    Protected Sub radList_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles radList.ItemDataBound

        If (TypeOf e.Item Is GridDataItem) Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            If (Not e.Item.IsInEditMode) Then
            Else
                If (e.Item.ItemIndex <> -1) Then
                Else
                    DirectCast(item.FindControl("lblSeqNo"), Label).Text = ""
                End If
            End If
        ElseIf (TypeOf e.Item Is GridCommandItem) Then
            e.Item.FindControl("RefreshButton").Parent.Visible = False
        End If
    End Sub

    Protected Sub radList_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radList.NeedDataSource

        da.SelectCommand.CommandText = "select * from setting s where table_name='arch' and field_name = @field_name order by sort"
        da.SelectCommand.Parameters.Clear()
        da.SelectCommand.Parameters.Add("@field_name", SqlDbType.NVarChar).Value = ddlCat.SelectedValue

        dt.Clear()
        da.Fill(dt)

        radList.DataSource = dt
    End Sub

    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("arch.aspx?mnuid=" & mnuid)
    End Sub

    Protected Sub ddlCat_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCat.SelectedIndexChanged
        radList.Rebind()
    End Sub
End Class
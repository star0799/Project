Imports System.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Class arch_str
    Inherits BasePage

    Dim itemid As String
    Public cid As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      
        cid = getQuery("cid")
        itemid = 1323

        setLangModule("archstr")
        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
        configButton()
    End Sub

    Sub configButton()

        AddHandler Master.navExportClick, AddressOf btnExport_ServerClick
        AddHandler Master.navImportClick, AddressOf btnImport_ServerClick

        u.showNavButton("Export")
        u.showNavButton("Import", True)
    End Sub

    Private Sub loadItem()

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

                    Dim groupName As String
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        groupName = getValue(item.GetDataKeyValue("group_name"))
                    Else
                        groupName = DBValue(DirectCast(item.FindControl("group_name"), HtmlInputText).Value)
                    End If

                    Dim relationId As Integer
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        relationId = item.GetDataKeyValue("id")
                        cmd.CommandText = "delete from archstr where id = @id"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        cmd.ExecuteNonQuery()
                    Else
                        If (Not Page.IsValid) Then
                            Return
                        End If

                        cmd.CommandText = "select count(*) from archstr where group_name = @group_name"
                        cmd.Parameters.Clear()
                        If (e.CommandName = RadGrid.UpdateCommandName OrElse e.CommandName = RadGrid.DeleteCommandName) Then
                            cmd.CommandText &= " and id <> @id"
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        End If

                        cmd.Parameters.Add("@group_name", SqlDbType.NVarChar).Value = DBValue(groupName)
                        If (cmd.ExecuteScalar > 0) Then
                            e.Canceled = True
                            showAlert(String.Format("組別名稱 '{0}' 已經存在!", groupName))
                            Return
                        End If

                        If (e.CommandName = RadGrid.PerformInsertCommandName) Then
                            cmd.CommandText = "insert into archstr (" & _
                            "group_name, arch_str, enabled, create_user_id" & _
                            ") values (" & _
                            "@group_name, @arch_str, @enabled, @create_user_id" & _
                            "); " & _
                            "select SCOPE_IDENTITY()"

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@group_name", SqlDbType.NVarChar).Value = groupName
                            cmd.Parameters.Add("@arch_str", SqlDbType.NVarChar).Value = DBValue(DirectCast(item.FindControl("arch_str"), HtmlTextArea).Value)
                            cmd.Parameters.Add("@enabled", SqlDbType.Bit).Value = 1
                            cmd.Parameters.Add("@create_user_id", SqlDbType.NVarChar).Value = authInfo.userId
                            relationId = cmd.ExecuteScalar()
                        Else
                            relationId = item.GetDataKeyValue("id")

                            cmd.CommandText = "update archstr set " & _
                            "group_name = @group_name, arch_str = @arch_str " & _
                            "where id = @id"

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                            cmd.Parameters.Add("@group_name", SqlDbType.NVarChar).Value = groupName
                            cmd.Parameters.Add("@arch_str", SqlDbType.NVarChar).Value = DBValue(DirectCast(item.FindControl("arch_str"), HtmlTextArea).Value)
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

        da.SelectCommand.Parameters.Clear()
        Dim cri As String = "1 = 1"

        If (Me.ViewState("searchFlag") IsNot Nothing) Then
            If (Not String.IsNullOrEmpty(keyword.Value)) Then
                cri &= " AND (n.group_name LIKE @keyword OR n.arch_str LIKE @keyword)"
                da.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = "%" & keyword.Value & "%"
            End If
        End If

        da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT n.* " & _
        "FROM     dbo.archstr AS n " & _
        "WHERE  (" & cri & ") " & _
        "ORDER BY n.id"

        dt.Clear()
        da.Fill(dt)

        radList.DataSource = dt
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.ServerClick
        Me.ViewState("searchFlag") = True
        radList.Rebind()
    End Sub

    Protected Sub btnExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        radList.Rebind()

        da.SelectCommand.CommandText = "select * from mapping where table_name = 'arch_str' order by sort"
        Dim dtCol As New DataTable
        da.Fill(dtCol)

        Dim dataRowIndex As Integer = 1
        Dim fs As New FileStream(Server.MapPath("docs\權威詞庫(範例).xls"), FileMode.Open, FileAccess.Read)
        Dim wb As New NPOI.HSSF.UserModel.HSSFWorkbook(fs) '
        For i As Integer = 0 To Math.Min(wb.NumberOfSheets, 1) - 1
            Dim sheet As NPOI.HSSF.UserModel.HSSFSheet = wb.GetSheetAt(i)
            sheet.RemoveRow(sheet.GetRow(dataRowIndex))
            For x As Integer = 0 To dt.Rows.Count - 1
                Dim row As NPOI.HSSF.UserModel.HSSFRow = sheet.CreateRow(dataRowIndex + x)
                For y As Integer = 0 To dtCol.Rows.Count - 1
                    Dim fieldName As String = dtCol.Rows(y)("field_name").ToString
                    Dim value As String = dt.Rows(x)(fieldName).ToString()

                    'Setting fields
                    If (dtCol.Rows(y)("setting_flag") = 1) Then
                        value = dt.Rows(x)(fieldName & "_name").ToString()
                    End If

                    Dim fieldType As String = getValue(dtCol.Rows(y)("field_type"))
                    If (fieldType = "date") Then
                        value = formatDate(value, "/")
                    End If

                    row.CreateCell(y).SetCellValue(value)
                    row.HeightInPoints = 30
                Next y
            Next x
        Next i
        exportExcel("權威詞庫.xls", wb)
    End Sub

    Protected Sub btnImport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("arch_str_import.aspx?mnuid=" & mnuid)
    End Sub
End Class
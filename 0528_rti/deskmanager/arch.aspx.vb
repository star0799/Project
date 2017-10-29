Imports System.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Class arch
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

        AddHandler Master.navExportClick, AddressOf btnExport_ServerClick
        AddHandler Master.navImportClick, AddressOf btnImport_ServerClick

        u.showNavButton("Export")
        u.showNavButton("Import", True)
    End Sub

    Private Sub loadItem()

        If (Not Me.IsPostBack) Then
            'loadOptions(gender, "stu", "gender")
            'loadOptions(stu_type, "stu", "stu_type")
        End If
    End Sub

    Protected Sub radList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Response.Redirect("arch_file.aspx?mnuid=" & mnuid & "&aid=" & item.GetDataKeyValue("id"))

            Case RadGrid.InitInsertCommandName
                Response.Redirect("arch_edit.aspx?mnuid=" & mnuid)

            Case RadGrid.PerformInsertCommandName, RadGrid.UpdateCommandName, RadGrid.DeleteCommandName

                tx = cn.BeginTransaction
                cmd.Transaction = tx
                da.SelectCommand.Transaction = tx

                Try
                    Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
                    Dim relationId As Integer
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        relationId = item.GetDataKeyValue("id")

                        cmd.CommandText = "delete from archsubject where aid = @aid"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@aid", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        cmd.ExecuteNonQuery()

                        cmd.CommandText = "delete from arch where id = @id"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = item.GetDataKeyValue("id")
                        cmd.ExecuteNonQuery()
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
                DirectCast(e.Item.FindControl("btnDetail"), Button).Visible = (e.Item.DataItem("file_count") > 0)
            End If
        ElseIf (TypeOf e.Item Is GridCommandItem) Then
            e.Item.FindControl("RefreshButton").Parent.Visible = False
        End If
    End Sub

    Protected Sub radList_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radList.NeedDataSource

        da.SelectCommand.Parameters.Clear()
        Dim cri As String = "1 = 1"

        If (Me.ViewState("searchFlag") IsNot Nothing) Then
            If (Not String.IsNullOrEmpty(identifier.Value)) Then
                cri &= " AND a.identifier LIKE @identifier"
                da.SelectCommand.Parameters.Add("@identifier", SqlDbType.NVarChar).Value = "%" & identifier.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(record.Value)) Then
                cri &= " AND a.record LIKE @record"
                da.SelectCommand.Parameters.Add("@record", SqlDbType.NVarChar).Value = "%" & record.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(p_title.Value)) Then
                cri &= " AND a.p_title LIKE @p_title"
                da.SelectCommand.Parameters.Add("@p_title", SqlDbType.NVarChar).Value = "%" & p_title.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(creator.Value)) Then
                cri &= " AND a.creator LIKE @creator"
                da.SelectCommand.Parameters.Add("@creator", SqlDbType.NVarChar).Value = "%" & creator.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(publisher.Value)) Then
                cri &= " AND a.publisher LIKE @publisher"
                da.SelectCommand.Parameters.Add("@publisher", SqlDbType.NVarChar).Value = "%" & publisher.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(subject.Value)) Then
                cri &= " AND a.subject LIKE @subject"
                da.SelectCommand.Parameters.Add("@subject", SqlDbType.NVarChar).Value = "%" & subject.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(keyword_name.Value)) Then
                cri &= " AND a.keyword_name LIKE @keyword_name"
                da.SelectCommand.Parameters.Add("@keyword_name", SqlDbType.NVarChar).Value = "%" & keyword_name.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(rdesc_catalog.Value)) Then
                cri &= " AND a.rdesc_catalog LIKE @rdesc_catalog"
                da.SelectCommand.Parameters.Add("@rdesc_catalog", SqlDbType.NVarChar).Value = "%" & rdesc_catalog.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(rdesc.Value)) Then
                cri &= " AND a.rdesc LIKE @rdesc"
                da.SelectCommand.Parameters.Add("@rdesc", SqlDbType.NVarChar).Value = "%" & rdesc.Value & "%"
            End If
            If (Not String.IsNullOrEmpty(register.Value)) Then
                cri &= " AND a.register LIKE @register"
                da.SelectCommand.Parameters.Add("@register", SqlDbType.NVarChar).Value = "%" & register.Value & "%"
            End If

            If (Not String.IsNullOrEmpty(create_date_s.Value)) Then
                cri &= " AND a.cdate >= @create_date_s"
                da.SelectCommand.Parameters.Add("@create_date_s", SqlDbType.DateTime).Value = fromTwDate(create_date_s.Value)
            End If
            If (Not String.IsNullOrEmpty(create_date_e.Value)) Then
                cri &= " AND cdate <= @create_date_e"
                da.SelectCommand.Parameters.Add("@create_date_e", SqlDbType.DateTime).Value = fromTwDate(create_date_e.Value)
            End If
        End If

        da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT a.*, (select count(*) from archfile where aid=a.id) as file_count, " & _
        "s1.name as rtype_name, " & _
        "s2.name as carrier_name, " & _
        "s3.name as file_type_name, " & _
        "s4.name as catalog_level_name, " & _
        "s5.name as format_name, " & _
        "s6.name as layout_name, " & _
        "s7.name as language_name, " & _
        "s8.name as p_ispublic_name " & _
        "FROM     dbo.arch AS a LEFT OUTER JOIN " & _
        "               dbo.setting AS s1 ON s1.table_name = 'arch' and s1.field_name = 'rtype' AND a.rtype = s1.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s2 ON s2.table_name = 'arch' and s2.field_name = 'carrier' AND a.carrier = s2.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s3 ON s3.table_name = 'arch' and s3.field_name = 'file_type' AND a.file_type = s3.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s4 ON s4.table_name = 'arch' and s4.field_name = 'catalog_level' AND a.catalog_level = s4.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s5 ON s5.table_name = 'arch' and s5.field_name = 'format' AND a.format = s5.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s6 ON s6.table_name = 'arch' and s6.field_name = 'layout' AND a.layout = s6.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s7 ON s7.table_name = 'arch' and s7.field_name = 'language' AND a.language = s7.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s8 ON s8.table_name = 'arch' and s8.field_name = 'p_ispublic' AND a.p_ispublic = s8.value " & _
        "WHERE  (" & cri & ") " & _
        "ORDER BY a.id desc"

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

        da.SelectCommand.CommandText = "select * from mapping where table_name = 'arch' order by sort"
        Dim dtCol As New DataTable
        da.Fill(dtCol)

        Dim dataRowIndex As Integer = 2
        Dim fs As New FileStream(Server.MapPath("docs\數位典藏基本資料(範例).xls"), FileMode.Open, FileAccess.Read)
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

                    If (Not String.IsNullOrEmpty(value)) Then
                        Select fieldName
                            Case "subject"

                                Dim lst As New List(Of String)
                                cmd.CommandText = "SELECT  s1.name " & _
                                "FROM     dbo.archsubject AS a INNER JOIN " & _
                                "               dbo.setting AS s1 ON s1.table_name = 'arch' and s1.field_name = 'subject' AND a.sid = s1.value " & _
                                "WHERE  (a.aid = @aid) " & _
                                "ORDER BY a.id"

                                cmd.Parameters.Clear()
                                cmd.Parameters.Add("@aid", SqlDbType.Int).Value = dt.Rows(x)("id")
                                dr = cmd.ExecuteReader
                                While (dr.Read())
                                    lst.Add(getValue(dr("name")))
                                End While
                                dr.Close()
                                value = String.Join("、", lst)
                        End Select
                    End If

                    row.CreateCell(y).SetCellValue(value)
                    row.HeightInPoints = 30
                Next y
            Next x
        Next i
        exportExcel("典藏資料.xls", wb)
    End Sub

    Protected Sub btnImport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("arch_import.aspx?mnuid=" & mnuid)
    End Sub

    Protected Sub btnConfig_ServerClick(sender As Object, e As System.EventArgs) Handles btnConfig.ServerClick
        Response.Redirect("arch_config.aspx?mnuid=" & mnuid)
    End Sub
End Class
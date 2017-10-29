Imports System.Data
Imports Telerik.Web.UI
Imports NPOI
Imports NPOI.HSSF.UserModel
Imports System.IO
Imports System.Data.SqlClient

Partial Class arch_str_import
    Inherits BasePage

    Dim itemid As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        itemid = 1323

        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
    End Sub

    Sub loadItem()
        If (Not Me.IsPostBack) Then
            radList2.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.ServerClick
        Dim objFile As HttpPostedFile = Request.Files(0)
        If (Not {".xls"}.Contains(Path.GetExtension(objFile.FileName).ToLower())) Then
            lblMessage.Text = "上傳檔案格式需為 XLS"
            Return
        End If

        Dim ds As DataSet = importExcel(Request.Files(0).InputStream, 0, 0)
        If (ds.Tables.Count < 1) Then
            lblMessage.Text = "Excel 檔案無任何工作表!"
            Return
        End If

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx
        Try
            Dim insertCnt As Integer = 0
            Dim updateCnt As Integer = 0
            Dim bolDeleted As Boolean = False
            Dim dataRowNo As Integer = 2
            Dim dtErr As New DataTable
            dtErr.Columns.Add("line_no", GetType(Integer))
            dtErr.Columns.Add("column_no", GetType(String))
            dtErr.Columns.Add("msg", GetType(String))
         
            With ds.Tables(0)

                da.SelectCommand.CommandText = "select * from mapping where table_name = 'arch_str' order by sort"
                Dim dtCol As New DataTable
                da.Fill(dtCol)

                da.SelectCommand.CommandText = "select * from setting"
                Dim dtSetting As New DataTable
                da.Fill(dtSetting)

                da.SelectCommand.CommandText = "Select * From INFORMATION_SCHEMA.COLUMNS Where table_name = @table_name"
                da.SelectCommand.Parameters.Clear()
                da.SelectCommand.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = "archstr"
                Dim dtSchema As New DataTable
                da.Fill(dtSchema)

                Dim tmp(dtCol.Rows.Count - 1) As String
                Dim val(dtCol.Rows.Count - 1) As Object

                Dim traceList As New List(Of String)
                For x As Integer = 0 To dtCol.Rows.Count - 1
                    If (dtCol.Rows(x)("trace_flag") = 1) Then
                        traceList.Add(dtCol.Rows(x)("field_name").ToString)
                    End If
                Next x

                For x As Integer = 0 To .Rows.Count - 1

                    Dim processRowInfo As ProcessRowInfo = processSingleRow(.Rows(x), x + dataRowNo, dtCol, dtSetting, dtSchema, dtErr)
                    If (processRowInfo.success) Then
                        Dim aid As Integer
                        Dim dicParam As Dictionary(Of String, Object) = processRowInfo.dicParam

                        cmd.CommandText = "select * from archstr where group_name = @group_name"
                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@group_name", SqlDbType.NVarChar).Value = dicParam("group_name")
                        dr = cmd.ExecuteReader()
                        If (Not dr.Read()) Then
                            dr.Close()

                            insertCnt += 1

                            cmd.CommandText = "insert into archstr (" & _
                            "group_name, arch_str, enabled, create_user_id" & _
                            ") values (" & _
                            "@group_name, @arch_str, @enabled, @create_user_id" & _
                            "); " & _
                            "select SCOPE_IDENTITY()"

                            appendParams(cmd.CommandText, dicParam)
                            cmd.Parameters("@enabled").Value = 1
                            cmd.Parameters("@create_user_id").Value = authInfo.userId
                            aid = cmd.ExecuteScalar
                        Else
                            Dim source As Dictionary(Of String, Object) = castReader(dr)
                            aid = source("id")
                            dr.Close()

                            updateCnt += 1

                            cmd.CommandText = "update archstr set " & _
                            "arch_str = @arch_str " & _
                            "where group_name = @group_name"

                            appendParams(cmd.CommandText, dicParam)
                            cmd.Parameters.Add("@group_name", SqlDbType.NVarChar).Value = dicParam("group_name")
                            cmd.ExecuteNonQuery()
                        End If
                        dr.Close()
                    End If
                Next x
            End With

            radList2.DataSource = dtErr
            radList2.Rebind()

            lblMessage.Text = prTag("div", String.Format("總異動筆數：{0} 筆，新增 {1} 筆，更正 {2} 筆。", insertCnt + updateCnt, insertCnt, updateCnt), "style", "color:blue")
            If (dtErr.Rows.Count > 0) Then
                Dim failCnt As Integer = dtErr.AsEnumerable.GroupBy(Function(p) p.Field(Of Integer)("line_no")).Count()
                lblMessage.Text &= prTag("div", String.Format("失敗筆數：{0} 筆", failCnt), "style", "color:red")
            End If
            radList2.Visible = (dtErr.Rows.Count > 0)
            tblImport.Visible = False

            btnSave.Visible = False

            tx.Commit()

        Catch ex As Exception
            tx.Rollback()
            showAlert("處理資料時發生意外錯誤!", Request.Url.ToString)
            Return
        End Try
    End Sub

    Private Function getTwDate(value As Object) As Object
        Dim result As Object
        Dim _date As Date
        Dim tmpDate As String = value.ToString().Replace("民國", "")
        Dim tmpYr As String = extractString(tmpDate, "", "年")
        Dim tmpMon As String = extractString(tmpDate, "年", "月")
        Dim tmpDay As String = extractString(tmpDate, "月", "日")
        If (Not String.IsNullOrEmpty(tmpYr) AndAlso Not String.IsNullOrEmpty(tmpMon) AndAlso Not String.IsNullOrEmpty(tmpDay)) Then
            If (tmpYr.Length > 3 AndAlso Integer.TryParse(tmpYr, Nothing)) Then
                tmpYr = Convert.ToInt32(tmpYr) - 1911
                tmpDate = String.Format("{0}{1}{2}", tmpYr, tmpMon.PadLeft(2, "0"c), tmpDay.PadLeft(2, "0"c))
            Else
                tmpDate = String.Empty
            End If
        End If

        Dim len As Integer = tmpDate.Length
        If (len = 6 AndAlso Integer.TryParse(tmpDate, Nothing)) Then
            result = Convert.ToInt32(tmpDate)
        ElseIf (len = 7 AndAlso Integer.TryParse(tmpDate, Nothing)) Then
            result = Convert.ToInt32(tmpDate)
        ElseIf (DateTime.TryParse(tmpDate, _date)) Then
            result = Convert.ToInt32(toTwDate(_date))
        Else
            result = DBNull.Value
        End If
        Return result
    End Function
End Class
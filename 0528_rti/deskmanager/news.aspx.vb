Imports System.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Class deskmanager_news
    Inherits BasePage

    Dim itemid As String
    Public cid As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      
        cid = getQuery("cid")
        itemid = 1319

        setModCat("modnews")
        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
        configButton()
    End Sub

    Sub configButton()
    End Sub

    Private Sub loadItem()

    End Sub

    Protected Sub radList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radList.ItemCommand
        Select Case e.CommandName
            Case RadGrid.InitInsertCommandName
                Response.Redirect("news_edit.aspx?mnuid=" & mnuid)
              
            Case RadGrid.PerformInsertCommandName, RadGrid.UpdateCommandName, RadGrid.DeleteCommandName

                tx = cn.BeginTransaction
                cmd.Transaction = tx
                da.SelectCommand.Transaction = tx

                Try
                    Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
                    Dim relationId As Integer
                    If (e.CommandName = RadGrid.DeleteCommandName) Then
                        relationId = item.GetDataKeyValue("id")

                        deleteUploadFiles(objModCatInfo, objModCatInfo.contentTableName, "id", item.GetDataKeyValue("id"), "~/files/")

                        cmd.CommandText = "delete from news where id = @id"
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
                cri &= " AND (n.news_title LIKE @keyword OR n.content LIKE @keyword)"
                da.SelectCommand.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = "%" & keyword.Value & "%"
            End If

            If (Not String.IsNullOrEmpty(pdate_s.Value)) Then
                cri &= " AND n.pdate >= @pdate_s"
                da.SelectCommand.Parameters.Add("@pdate_s", SqlDbType.Int).Value = pdate_s.Value
            End If
            If (Not String.IsNullOrEmpty(pdate_e.Value)) Then
                cri &= " AND n.pdate <= @pdate_e"
                da.SelectCommand.Parameters.Add("@pdate_e", SqlDbType.Int).Value = pdate_e.Value
            End If
        End If

        da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT n.*, dbo.inShowTw(n.stdate, n.eddate) AS in_show " & _
        "FROM     dbo.news AS n " & _
        "WHERE  (" & cri & ") " & _
        "ORDER BY n.pdate desc"

        dt.Clear()
        da.Fill(dt)

        radList.DataSource = dt
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.ServerClick
        Me.ViewState("searchFlag") = True
        radList.Rebind()
    End Sub
End Class
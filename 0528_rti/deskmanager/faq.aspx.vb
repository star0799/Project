Imports System.Data
Imports Telerik.Web.UI
Imports System.IO

Partial Class faq
    Inherits BasePage

    Dim itemid As String
    Public cid As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
      
        cid = getQuery("cid")
        itemid = 1319

        setModCat("modfaq")

        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
        configButton()
    End Sub

    Sub configButton()
    End Sub

    Private Sub loadItem()

        module_control1.Attributes("modCatId") = objModCatInfo.ID
        module_control1.Attributes("selectedValue") = "List"

        If (Not Me.IsPostBack) Then
            ' // drop-down cat
            ddlCat.Items.Clear()
            ddlCat.Items.Add(New ListItem("不拘", ""))
            ddlCat.Items.Add(New ListItem("無類別", "nocat"))
            Dim lstModuleCats As List(Of ModuleCatInfo) = objController.getModuleCats(modid)
            For Each item In lstModuleCats
                ddlCat.Items.Add(New ListItem(item.catName, item.ID))
            Next
            setDdlVal(ddlCat, cid)
        End If
    End Sub

    Protected Sub radList_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radList.ItemCommand
        Select Case e.CommandName
            Case RadGrid.InitInsertCommandName
                Response.Redirect("faq_edit.aspx?mnuid=" & mnuid)

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

                        cmd.CommandText = "delete from faq where id = @id"
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
            If (Not String.IsNullOrEmpty(ddlCat.SelectedValue)) Then
                If (ddlCat.SelectedValue = "nocat") Then
                    cri &= " AND n.catid IS NULL"
                Else
                    cri &= " AND n.catid = @catid"
                    da.SelectCommand.Parameters.Add("@catid", SqlDbType.Int).Value = ddlCat.SelectedValue
                End If
            End If

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

        da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT n.*, dbo.inShowTw(n.stdate, n.eddate) AS in_show, " & _
        "dbo.modulecat.cat_name " & _
        "FROM     dbo.faq AS n LEFT OUTER JOIN " & _
        "                          dbo.modulecat ON n.catid = dbo.modulecat.id " & _
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

    Protected Sub btnCat_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCat.ServerClick
        Response.Redirect("module_cat.aspx?mnuid=" & mnuid & "&mcid=" & objModCatInfo.ID)
    End Sub
End Class
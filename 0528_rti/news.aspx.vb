Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class news
    Inherits FrontBasePage

    Dim cid As String
    Public showCategory As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cid = cv(Request.Form(prefixName & "ddlCat"), cv(Request.QueryString("cid")))
        putGlobalObject("pageUnit", "News")

        setModCat("modnews")

        validateAccess(objModCatInfo)
       
        If (cid.Length > 0) Then
            rf(cid, "modulecat", "id", "enabled = 1")
            eq(modid, getColVal("modulecat", "module_id", cid))
        End If

        setNavigationPath()

        loadItem(False)
    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)

        ' // drop-down cat
        ddlCat.Items.Clear()
        ddlCat.Items.Add(New ListItem("全部", ""))

        cmd.CommandText = "SELECT         modulecat.* " & _
        "FROM             modulecat " & _
        "WHERE         (modulecat.module_id = @module_id) AND (modulecat.enabled = 1) " & _
        "ORDER BY  modulecat.sort"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@module_id", SqlDbType.Int).Value = modid
        dr = cmd.ExecuteReader
        While (dr.Read())
            ddlCat.Items.Add(New ListItem(dr("cat_name"), dr("id")))
        End While
        dr.Close()
        setDdlVal(ddlCat, cid)
        showCategory = (ddlCat.Items.Count > 1)

        da.SelectCommand.Parameters.Clear()
        Dim cri As String = "1 = 1"

        If (cid.Length > 0) Then
            ' // drop-down cat
            cri &= " AND news.catid = @catid"
            da.SelectCommand.Parameters.Add("@catid", SqlDbType.Int).Value = cid
        End If

        da.SelectCommand.CommandText = "SELECT         TOP 100 PERCENT news.*, row_number() over (order by news.sort) AS row, modulecat.cat_name " & _
        "FROM             news LEFT OUTER JOIN " & _
        "                          modulecat ON news.catid = modulecat.id " & _
        "WHERE         (news.modid = @modid) AND (dbo.inShowTw(news.stdate, news.eddate) = 1) AND " & _
        "                 (news.enabled = 1) AND (modulecat.enabled = 1 OR modulecat.enabled IS NULL) AND " & _
        "                 (" & cri & ") " & _
        "ORDER BY  news.sort"

        da.SelectCommand.Parameters.Add("@modid", SqlDbType.Int).Value = modid

        getDataSource(da, dt, objModuleSetting.rowsPerPage, Pager1)

        If (Not Me.IsPostBack OrElse doBind) Then
            lvwBind()
        End If
    End Sub

    Sub lvwBind()
        lvwList.DataSource = dt
        lvwList.DataBind()
    End Sub

    Protected Sub lvwList_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
        Dim lblSeqNo As Literal = DirectCast(e.Item.FindControl("lblSeqNo"), Literal)
        lblSeqNo.Text = Pager1.Attributes("startRow") + e.Item.DataItemIndex + 1

        Dim hlkTitle As HtmlAnchor = DirectCast(e.Item.FindControl("hlkTitle"), HtmlAnchor)
        hlkTitle.InnerHtml = e.Item.DataItem("news_title")
        'If (e.Item.DataItem("open_mode") = "C") Then
        hlkTitle.HRef = "news_in.aspx?mnuid=" & mnuid & "&nid=" & e.Item.DataItem("id")
        hlkTitle.Target = Nothing
        'Else
        '    hlkTitle.HRef = convVal(e.Item.DataItem("link_href"))
        '    hlkTitle.Target = "_blank"
        'End If

        Dim trItem As HtmlTableRow = DirectCast(e.Item.FindControl("trItem"), HtmlTableRow)
        setVisible(trItem)
        trItem.Cells(3).Visible = showCategory
    End Sub

    Protected Sub lvwList_DataBound(sender As Object, e As System.EventArgs) Handles lvwList.DataBound
        Dim trHeader As HtmlTableRow = DirectCast(lvwList.FindControl("trHeader"), HtmlTableRow)
        If (trHeader IsNot Nothing) Then
            setVisible(trHeader)
            trHeader.Cells(3).Visible = showCategory
        End If
    End Sub

    Protected Sub ddlCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCat.SelectedIndexChanged
        Response.Redirect("news.aspx?mnuid=" & mnuid & "&cid=" & cid)
    End Sub
End Class
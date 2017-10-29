Imports System.Data
Imports System.Data.SqlClient

Partial Class controls_pager
    Inherits FrontBaseUserControl

    Protected Sub ddlPageList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim startRow As Integer = Me.Attributes("startRow")
        Dim totalRow As Integer = Me.Attributes("totalRow")
        Dim curPage As Integer = Me.Attributes("curPage")
        Dim totalPage As Integer = Me.Attributes("totalPage")

        DirectCast(Me.FindControl("lblPager"), Literal).Text = totalRow

        Dim ary As New ArrayList
        Dim tmp As String
        Dim href As String = ""
        Dim pageName As String = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).ToLower

        For x = 0 To Page.Request.QueryString.Count - 1
            If (Page.Request.QueryString.GetKey(x) IsNot Nothing AndAlso Request.QueryString.GetKey(x).ToUpper <> "PG") Then
                tmp = Request.QueryString.GetKey(x) & "=" & Server.UrlEncode(Request.QueryString(x))
                If (ary.Contains(tmp) = False) Then
                    ary.Add(tmp)
                End If
            End If
        Next
        For x = 0 To ary.Count - 1
            href &= IIf(href.Length > 0, "&", "") & ary(x)
        Next x
        href = pageName & IIf(href.Length > 0, "?", "") & href & IIf(href.Length > 0, "&", "?")

        Dim ddl As DropDownList = DirectCast(Me.FindControl("ddlPageList"), DropDownList)
        ddl.Items.Clear()
        For x = 1 To totalPage
            ddl.Items.Add(New ListItem(x, x))
        Next
        ddl.SelectedValue = curPage
        ddl.Attributes("onchange") = "location.href='" & convSingleQuote(href & "pg=") + "' + this.options[this.selectedIndex].value; return false; "
        ddl.Visible = (totalPage > 1)

        'DirectCast(Me.FindControl("btnFirst"), ImageButton).ToolTip = L.portal(22)
        DirectCast(Me.FindControl("btnFirst"), ImageButton).Attributes("onclick") = "location.href='" & convSingleQuote(href & "pg=1") & "'"
        DirectCast(Me.FindControl("btnFirst"), ImageButton).PostBackUrl = "javascript:;"
        DirectCast(Me.FindControl("btnFirst"), ImageButton).Visible = (curPage > 1)

        'DirectCast(Me.FindControl("btnPrev"), ImageButton).ToolTip = L.portal(23)
        DirectCast(Me.FindControl("btnPrev"), ImageButton).Attributes("onclick") = "location.href='" & convSingleQuote(href & "pg=" & curPage - 1) & "'"
        DirectCast(Me.FindControl("btnPrev"), ImageButton).PostBackUrl = "javascript:;"
        DirectCast(Me.FindControl("btnPrev"), ImageButton).Visible = (curPage > 1)

        'DirectCast(Me.FindControl("btnNext"), ImageButton).ToolTip = L.portal(24)
        DirectCast(Me.FindControl("btnNext"), ImageButton).Attributes("onclick") = "location.href='" & convSingleQuote(href & "pg=" & curPage + 1) & "'"
        DirectCast(Me.FindControl("btnNext"), ImageButton).PostBackUrl = "javascript:;"
        DirectCast(Me.FindControl("btnNext"), ImageButton).Visible = (curPage < totalPage)

        'DirectCast(Me.FindControl("btnLast"), ImageButton).ToolTip = L.portal(25)
        DirectCast(Me.FindControl("btnLast"), ImageButton).Attributes("onclick") = "location.href='" & convSingleQuote(href & "pg=" & totalPage) & "'"
        DirectCast(Me.FindControl("btnLast"), ImageButton).PostBackUrl = "javascript:;"
        DirectCast(Me.FindControl("btnLast"), ImageButton).Visible = (curPage < totalPage)

        Me.Visible = (totalPage > 0)
    End Sub
End Class
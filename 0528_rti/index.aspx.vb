Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class index
    Inherits FrontBasePage

    Public intro As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadItem()
    End Sub

    Sub loadItem()
        'Dim c As HttpCookie = Request.Cookies("test7")
        'If (c Is Nothing) Then
        '    out("null")
        'Else
        '    out(c.Value)
        'End If
        'Response.End()

        If (Not Me.IsPostBack) Then
            cmd.CommandText = "select intro,link_href from homelayout where lang_id = @lang_id"
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = langId
            dr = cmd.ExecuteReader
            If (dr.Read()) Then
                intro = convStr(convVal(dr("intro")))
                hlkMore.HRef = getValue(dr("link_href"))
            End If
            dr.Close()

            If (Not Me.IsPostBack) Then
                cmd.CommandText = "select name,value from setting where table_name = 'arch' and field_name = 'subject' order by sort"
                loadOptions(subject, cmd, "name", "value")
                subject.Items.Insert(0, New ListItem("全選", ""))
            End If
        End If
    End Sub

    Protected Sub btnSubmit_ServerClick(sender As Object, e As System.EventArgs) Handles btnSubmit.ServerClick
        Dim lst As String = String.Join(",", subject.Items.Cast(Of ListItem).Where(Function(p) p.Selected).Select(Function(p) p.Value))
        Dim q As String = ""
        If (Not String.IsNullOrEmpty(keyword.Value.Trim)) Then
            q = Server.UrlEncode(stripKeyword(keyword.Value.Trim))
        End If
        Dim url As String = String.Format("query.aspx?a={0}&s={1}&q={2}&sd={3}&ed={4}", Server.UrlEncode(archTitle.Value.Trim), lst, q, date_s.Value, date_e.Value)
        Response.Redirect(url)
    End Sub
End Class
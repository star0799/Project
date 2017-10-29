Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class adv_search
    Inherits FrontBasePage

    Public intro As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadItem()
    End Sub

    Sub loadItem()

        If (Not Me.IsPostBack) Then
            cmd.CommandText = "select name,value from setting where table_name = 'arch' and field_name = 'subject' order by sort"
            loadOptions(subject, cmd, "name", "value")
            subject.Items.Insert(0, New ListItem("全選", ""))

            loadOptions(carrier, "arch", "carrier")
            carrier.Items.Insert(0, New ListItem("不拘", ""))
            loadOptions(file_type, "arch", "file_type")
            file_type.Items.Insert(0, New ListItem("不拘", ""))
        End If
    End Sub

    Protected Sub btnSubmit_ServerClick(sender As Object, e As System.EventArgs) Handles btnSubmit.ServerClick
        Dim lst As String = String.Join(",", subject.Items.Cast(Of ListItem).Where(Function(p) p.Selected).Select(Function(p) p.Value))
        'Dim q As New List(Of String)
        Dim q As String = ""
        Dim q2 As String = ""
        Dim q3 As String = ""
        If (Not String.IsNullOrEmpty(keyword.Value.Trim)) Then
            q = Server.UrlEncode(stripKeyword(keyword.Value.Trim))
        End If
        If (Not String.IsNullOrEmpty(keyword2.Value.Trim)) Then
            q2 = Server.UrlEncode(stripKeyword(keyword2.Value.Trim))
        End If
        If (Not String.IsNullOrEmpty(keyword3.Value.Trim)) Then
            q3 = Server.UrlEncode(stripKeyword(keyword3.Value.Trim))
        End If
        Dim url As String = String.Format("query.aspx?a={0}&s={1}&q={2}&q2={3}&q3={4}&sd={5}&ed={6}&cr={7}&ft={8}&co={9}&adv=true", Server.UrlEncode(archTitle.Value.Trim), lst, q, q2, q3, date_s.Value, date_e.Value, carrier.Value, file_type.Value, cover.Value)
        Response.Redirect(url)
    End Sub
End Class
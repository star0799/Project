Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class query
    Inherits FrontBasePage

    Dim curDate As Date = Now
    Dim a As String
    Dim s As String
    Dim q, q2, q3 As String
    Dim sq, pat As String
    Dim sd As String
    Dim ed As String
    Dim cr, ft, co As String
    Dim adv As String
    Dim dtSubject As New DataTable
    Dim hs As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        a = getQuery("a")
        s = getQuery("s")
        q = getQuery("q")
        q2 = getQuery("q2")
        q3 = getQuery("q3")
        sd = getQuery("sd")
        ed = getQuery("ed")
        cr = getQuery("cr")
        ft = getQuery("ft")
        co = getQuery("co")
        adv = getQuery("adv")

        setLangModule("sitesearch")
        setNavigationPath()

        loadItem(False)
    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)
        For Each item In {a, s, q, sd, ed, cr, ft, co}
            If (hasScriptMetaChars(item)) Then
                showOops()
            End If
        Next
        Try
            If (Not String.IsNullOrEmpty(sd)) Then
                fromTwDate(sd)
            End If
            If (Not String.IsNullOrEmpty(ed)) Then
                fromTwDate(ed)
            End If
            If (Not String.IsNullOrEmpty(cr)) Then
                Integer.Parse(cr)
            End If
            If (Not String.IsNullOrEmpty(ft)) Then
                Integer.Parse(ft)
            End If
        Catch ex As Exception
            showOops()
        End Try

        sq = ""
        pat = ""
        a = stripKeyword(a)
        q = stripKeyword(q)
        q2 = stripKeyword(q2)
        q3 = stripKeyword(q3)

        Dim lstHs As New List(Of String)
        If (adv = "true") Then
            Dim lstKeyword As New List(Of String)
            If (Not String.IsNullOrEmpty(q)) Then
                lstKeyword.Add(q)
            End If
            If (Not String.IsNullOrEmpty(q2)) Then
                lstKeyword.Add(q2)
            End If
            If (Not String.IsNullOrEmpty(q3)) Then
                lstKeyword.Add(q3)
            End If
            If (lstKeyword.Any) Then
                sq = String.Join(" and ", lstKeyword.Select(Function(p) """" & p & """"))
                '   pat = "(" & String.Join("|", lstKeyword.Select(Function(p) Regex.Escape(p))) & ")"
            End If
            lstHs = lstKeyword.ToList()
        Else
            If (Not String.IsNullOrEmpty(q)) Then
                sq = expandKeyword(q)
                lstHs.Add(q)
            End If
        End If
        If (Not String.IsNullOrEmpty(a)) Then
            lstHs.Add(a)
        End If
        If (Not String.IsNullOrEmpty(co)) Then
            lstHs.Add(co)
        End If
        sq = sq.Replace("(", "")
        sq = sq.Replace(")", "")

        pat = "(" & String.Join("|", lstHs.Select(Function(p) Regex.Escape(p))) & ")"

        da.SelectCommand.CommandText = "select name from setting where table_name = 'arch' and field_name = 'subject'"
        dtSubject.Clear()
        da.Fill(dtSubject)

        da.SelectCommand.Parameters.Clear()
        Dim cri As String = "1 = 1"
        Dim joinCri As String = ""

        If (Not String.IsNullOrEmpty(sd)) Then
            cri &= " AND a.date_retrieve >= @sd"
            da.SelectCommand.Parameters.Add("@sd", SqlDbType.Int).Value = sd
        End If
        If (Not String.IsNullOrEmpty(ed)) Then
            cri &= " AND a.date_retrieve <= @ed"
            da.SelectCommand.Parameters.Add("@ed", SqlDbType.Int).Value = ed
        End If
        If (Not String.IsNullOrEmpty(cr)) Then
            cri &= " AND a.carrier = @carrier"
            da.SelectCommand.Parameters.Add("@carrier", SqlDbType.Int).Value = cr
        End If
        If (Not String.IsNullOrEmpty(ft)) Then
            cri &= " AND a.file_type = @file_type"
            da.SelectCommand.Parameters.Add("@file_type", SqlDbType.Int).Value = ft
        End If
        If (Not String.IsNullOrEmpty(co)) Then
            cri &= " AND a.cover LIKE @cover"
            da.SelectCommand.Parameters.Add("@cover", SqlDbType.NVarChar).Value = "%" & co & "%"
        End If

        If (Not String.IsNullOrEmpty(a)) Then
            joinCri &= " INNER JOIN CONTAINSTABLE(arch, p_title, @title) t1 ON a.id=t1.[Key]"
            da.SelectCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = a.Replace("(", "").Replace(")", "")
        End If
        If (Not String.IsNullOrEmpty(q)) Then
            joinCri &= " INNER JOIN CONTAINSTABLE(arch, (p_title, keyword_name, cover), @keyword_name) t2 ON a.id=t2.[Key]"
            da.SelectCommand.Parameters.Add("@keyword_name", SqlDbType.NVarChar).Value = sq
        End If

        If (Not String.IsNullOrEmpty(s)) Then
            Dim lst As New List(Of Integer)
            For Each item As String In s.Split(","c)
                If (Not String.IsNullOrEmpty(item)) Then
                    If (Not Integer.TryParse(item, Nothing)) Then
                        showOops()
                    End If
                    lst.Add(Convert.ToInt32(item))
                End If
            Next
            cri &= " and exists (select 1 from archsubject where aid = a.id and sid in (" & String.Join(",", lst) & "))"
        End If

        da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT a.*, row_number() over (order by a.id desc) AS row, " & _
        "s1.name as rtype_name, " & _
        "s2.name as carrier_name, " & _
        "s3.name as file_type_name, " & _
        "s4.name as catalog_level_name, " & _
        "s5.name as format_name, " & _
        "s6.name as layout_name, " & _
        "s7.name as language_name, " & _
        "s8.name as p_ispublic_name, " & _
        " subject_name = '' " & _
        "FROM     dbo.arch AS a " & joinCri & " " & _
        "               LEFT OUTER JOIN " & _
        "               dbo.setting AS s1 ON s1.table_name = 'arch' and s1.field_name = 'rtype' AND a.rtype = s1.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s2 ON s2.table_name = 'arch' and s2.field_name = 'carrier' AND a.carrier = s2.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s3 ON s3.table_name = 'arch' and s3.field_name = 'file_type' AND a.file_type = s3.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s4 ON s4.table_name = 'arch' and s4.field_name = 'catalog_level' AND a.catalog_level = s4.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s5 ON s5.table_name = 'arch' and s5.field_name = 'format' AND a.format = s5.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s6 ON s6.table_name = 'arch' and s6.field_name = 'layout' AND a.layout = s6.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s7 ON s7.table_name = 'arch' and s7.field_name = 'language' AND a.language = s7.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s8 ON s8.table_name = 'arch' and s8.field_name = 'p_ispublic' AND a.p_ispublic = s8.value " & _
        "WHERE  (" & cri & ") and (a.p_ispublic = 2) " & _
        "ORDER BY a.p_title"

        getDataSource(da, dt, 10, Pager1)

        If (Not Me.IsPostBack OrElse doBind) Then

            Dim lst As New List(Of Integer)
            For x As Integer = 0 To dt.Rows.Count - 1
                lst.Add(Convert.ToInt32(dt.Rows(x)("id")))
            Next x
            If (lst.Any) Then
                da.SelectCommand.CommandText = String.Format("select a.id, subject_name = STUFF(( " & _
                "        SELECT '、' + s1.name FROM archsubject s " & _
                "        inner join setting s1 on s1.table_name='arch' and s1.field_name='subject' and s.sid=s1.value " & _
                "        WHERE s.aid = a.ID " & _
                "         " & _
                "        FOR XML PATH ('')),1,1,'') " & _
                "FROM     dbo.arch AS a " & _
                "WHERE a.id IN ({0})", String.Join(",", lst))

                Dim dt2 As New DataTable
                da.Fill(dt2)

                For x As Integer = 0 To dt.Rows.Count - 1
                    For y As Integer = 0 To dt2.Rows.Count - 1
                        If (dt.Rows(x)("id") = dt2.Rows(y)("id")) Then
                            dt.Rows(x)("subject_name") = dt2.Rows(y)("subject_name")
                            Exit For
                        End If
                    Next y
                Next x
            End If

            lvwBind()
        End If
    End Sub

    Sub lvwBind()
        lvwList.DataSource = dt
        lvwList.DataBind()
    End Sub

    Protected Sub lvwList_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
        Dim oriPat As String = pat
        Dim hlkTitle As HtmlAnchor = DirectCast(e.Item.FindControl("hlkTitle"), HtmlAnchor)
        Dim title As String = getTailText(e.Item.DataItem("p_title"), 20)
        hlkTitle.InnerHtml = Regex.Replace(title, oriPat, AddressOf reMatch, RegexOptions.IgnoreCase)
        hlkTitle.HRef = "arch_view.aspx?mnuid=1310&aid=" & e.Item.DataItem("id")

        DirectCast(e.Item.FindControl("lblSubject"), Literal).Text = e.Item.DataItem("subject_name")

        Dim keywordName As String = getTailText(e.Item.DataItem("keyword_name").ToString, 95)
        keywordName = Regex.Replace(keywordName, oriPat, AddressOf reMatch, RegexOptions.IgnoreCase)
        DirectCast(e.Item.FindControl("lblKeywordName"), Literal).Text = keywordName
        DirectCast(e.Item.FindControl("lblDate"), Literal).Text = e.Item.DataItem("date_show")

        Dim cover As String = e.Item.DataItem("cover").ToString
        cover = Regex.Replace(cover, oriPat, AddressOf reMatch, RegexOptions.IgnoreCase)
        DirectCast(e.Item.FindControl("lblCover"), Literal).Text = cover
    End Sub

    Private Function reMatch(ByVal m As Match) As String
        Return prTag("span", m.ToString, "style", "color:red")
    End Function

    Protected Sub lvwList_DataBound(sender As Object, e As System.EventArgs) Handles lvwList.DataBound
    End Sub

    Private Function expandKeyword(keyword As String) As String

        Dim archStr As String = ""
        cmd.CommandText = "SELECT  TOP (100) PERCENT s.arch_str " & _
        "FROM     dbo.archstr AS s " & _
        "WHERE  (CHARINDEX(@keyword,s.arch_str) > 0) " & _
        "ORDER BY s.id"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = keyword
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            archStr = getValue(dr("arch_str"))
        End If
        dr.Close()

        If (Not String.IsNullOrEmpty(archStr)) Then
            keyword = String.Join(" or ", archStr.Split("、"c).Select(Function(p) """" & p & """"))
        End If

        Return keyword
    End Function
End Class
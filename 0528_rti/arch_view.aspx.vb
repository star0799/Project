Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class arch_view
    Inherits FrontBasePage

    Public rtype, carrier, file_type, project, record, identifier, p_title, creator, cover, publisher, date_show, date_retrieve, catalog_level, format, page_count, total_page, total_file, layout, language, subject, keyword_name, rdesc_catalog, rdesc, source, connection, storage_department, reference, rights, rights_object, rights_owner, p_ispublic, rights_restrictions, file_name, file_format, register, mdate_people, mdate, del_flag, create_date, create_user_id, update_date, update_user_id As String

    Dim aid As String
    Dim dt2 As New DataTable

    Dim isLogon As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        aid = getQuery("aid")
        putGlobalObject("pageUnit", "Arch")

        rf(aid, "arch", "id", "p_ispublic = 2")
        setNavigationPath()

        loadItem()
    End Sub

    Sub loadItem(Optional ByVal doBind As Boolean = True)

        Dim r As New AppSettingsReader()
        Dim soundMediaFileType As String = r.GetValue("soundMediaFileType", GetType(String))
        Dim soundFlag As Boolean = False

        cmd.CommandText = "SELECT  a.*, " & _
        "s1.name as rtype_name, " & _
        "s2.name as carrier_name, " & _
        "s3.name as file_type_name, " & _
        "s4.name as catalog_level_name, " & _
        "s5.name as format_name, " & _
        "s6.name as layout_name, " & _
        "s7.name as language_name, " & _
        "s8.name as p_ispublic_name, " & _
        " subject_name = STUFF(( " & _
        "        SELECT '、' + s1.name FROM archsubject s " & _
        "        inner join setting s1 on s1.table_name='arch' and s1.field_name='subject' and s.sid=s1.value " & _
        "        WHERE s.aid = a.ID " & _
        "         " & _
        "        FOR XML PATH ('')),1,1,'') " & _
        "FROM     dbo.arch AS a " & _
        "               LEFT OUTER JOIN " & _
        "               dbo.setting AS s1 ON s1.table_name = 'arch' and s1.field_name = 'rtype' AND a.rtype = s1.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s2 ON s2.table_name = 'arch' and s2.field_name = 'carrier' AND a.carrier = s2.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s3 ON s3.table_name = 'arch' and s3.field_name = 'file_type' AND a.file_type = s3.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s4 ON s4.table_name = 'arch' and s4.field_name = 'catalog_level' AND a.catalog_level = s4.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s5 ON s5.table_name = 'arch' and s5.field_name = 'format' AND a.format = s5.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s6 ON s6.table_name = 'arch' and s6.field_name = 'layout' AND a.layout = s6.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s7 ON s7.table_name = 'arch' and s7.field_name = 'language' AND a.language = s7.value LEFT OUTER JOIN " & _
        "               dbo.setting AS s8 ON s8.table_name = 'arch' and s8.field_name = 'p_ispublic' AND a.p_ispublic = s8.value " & _
        "WHERE  (a.id = @id)"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = aid
        dr = cmd.ExecuteReader
        If (dr.Read()) Then


            total_file = getValue(dr("total_file"))

            file_name = getValue(dr("file_name"))
            file_format = getValue(dr("file_format"))

            register = getValue(dr("register"))
            create_date = formatDate(dr("cdate"))
            mdate_people = getValue(dr("update_user_id"))
            mdate = formatDate(dr("update_date"))

            identifier = getValue(dr("identifier"))
            rtype = getValue(dr("rtype_name"))
            carrier = getValue(dr("carrier_name"))
            file_type = getValue(dr("file_type_name"))

            project = getValue(dr("project"))
            record = getValue(dr("record"))

            p_title = getValue(dr("p_title"))
            creator = getValue(dr("creator"))
            cover = getValue(dr("cover"))
            publisher = getValue(dr("publisher"))

            catalog_level = getValue(dr("catalog_level_name"))
            format = getValue(dr("format_name"))

            layout = getValue(dr("layout_name"))
            language = getValue(dr("language_name"))
            subject = getValue(dr("subject"))
            keyword_name = getValue(dr("keyword_name"))
            rdesc_catalog = getValue(dr("rdesc_catalog"))
            rdesc = getValue(dr("rdesc"))
            source = getValue(dr("source"))
            connection = getValue(dr("connection"))
            storage_department = getValue(dr("storage_department"))
            reference = getValue(dr("reference"))
            rights = getValue(dr("rights"))
            rights_object = getValue(dr("rights_object"))
            rights_owner = getValue(dr("rights_owner"))

            rights_restrictions = getValue(dr("rights_restrictions"))
            p_ispublic = getValue(dr("p_ispublic"))

            date_show = getValue(dr("date_show"))
            date_retrieve = getValue(dr("date_retrieve"))

            page_count = getValue(dr("page_count"))
            total_page = getValue(dr("total_page"))

            pageTitle = p_title

            Dim fileTypeId As String = getValue(dr("file_type"))
            soundFlag = (fileTypeId = soundMediaFileType)
        End If
        dr.Close()

        da.SelectCommand.CommandText = "select * from archimg where aid = @aid"
        da.SelectCommand.Parameters.Clear()
        da.SelectCommand.Parameters.Add("@aid", SqlDbType.Int).Value = aid
        dt.Clear()
        da.Fill(dt)
        If (Not Me.IsPostBack) Then
            If (dt.Rows.Count > 0) Then
                imgProd.Src = "arch_res.aspx?rid=" & Crypto.Encrypt(dt.Rows(0)("id") & "|m")
            End If
        End If

        da.SelectCommand.CommandText = "select * from archfile where aid = @aid"
        da.SelectCommand.Parameters.Clear()
        da.SelectCommand.Parameters.Add("@aid", SqlDbType.Int).Value = aid
        dt2.Clear()
        da.Fill(dt2)

        If (Not Me.IsPostBack OrElse doBind) Then
            lvwBind()
        End If

        trFile.Visible = (dt2.Rows.Count > 0)

        If (Not Me.IsPostBack) Then
            Dim archView As New List(Of Integer)
            Dim bolFlag As Boolean = False
            If (Session("archView") IsNot Nothing) Then
                archView = DirectCast(Session("archView"), List(Of Integer))
                bolFlag = Not archView.Contains(aid)
            Else
                bolFlag = True
                Session("archView") = archView
            End If
            If (bolFlag) Then
                cmd.CommandText = "update arch set visit=visit+1 where id=@id"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = aid
                cmd.ExecuteNonQuery()

                archView.Add(aid)
            End If
        End If

        Dim userController As New UserController(cmd)
        Dim objUserInfo As UserInfo = userController.getCurrentUser()
        isLogon = (objUserInfo IsNot Nothing)

        If (soundFlag) Then
            imgProd.Src = "images/cover.jpg"
            trImg.Visible = False
        End If

        hlkOrder.HRef = "order.aspx?aid=" & aid
    End Sub

    Sub lvwBind()
        lvwList.DataSource = dt
        lvwList.DataBind()
        lvwList2.DataSource = dt2
        lvwList2.DataBind()
    End Sub

    Protected Sub lvwList_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
        Dim hlkTitlePic As HtmlAnchor = DirectCast(e.Item.FindControl("hlkTitlePic"), HtmlAnchor)
        hlkTitlePic.HRef = "arch_res.aspx?rid=" & Crypto.Encrypt(e.Item.DataItem("id") & "|b")
        hlkTitlePic.Attributes("m") = "arch_res.aspx?rid=" & Crypto.Encrypt(e.Item.DataItem("id") & "|m")

        Dim imgPic As HtmlImage = DirectCast(e.Item.FindControl("imgPic"), HtmlImage)
        imgPic.Src = "arch_res.aspx?rid=" & Crypto.Encrypt(e.Item.DataItem("id") & "|s")
    End Sub

    Protected Sub lvwList_DataBound(sender As Object, e As System.EventArgs) Handles lvwList2.DataBound
    End Sub

    Protected Sub lvwList2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs)
        Dim hlkView As HtmlAnchor = DirectCast(e.Item.FindControl("hlkView"), HtmlAnchor)
        If (Not isLogon) Then
            hlkView.Attributes("onclick") = "alert('請先登入會員!');"
            hlkView.Target = Nothing
        Else
            hlkView.HRef = "arch_file.aspx?rid=" & Crypto.Encrypt(e.Item.DataItem("id"))
            hlkView.Target = "_blank"
        End If
    End Sub

    Protected Sub lvwList2_DataBound(sender As Object, e As System.EventArgs) Handles lvwList2.DataBound
    End Sub
End Class
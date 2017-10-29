Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_news_edit
    Inherits BasePage

    Dim itemid As String
    Dim nid As String

    Public ReadOnly Property newCreate As Boolean
        Get
            Return (nid.Length = 0)
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
 
        nid = getQuery("nid")

        itemid = 1319

        setModCat("modnews")
        requireAdminLoginMenu(itemid)
    
        If (Not newCreate) Then
            rf(nid, "news", "id")
        End If

        setNavigationMenuPath()

        loadItem()
    End Sub

    Sub loadItem()

        u.setFromURL(Me.ViewState)
        u.setInputTextMaxLen("news")

        '' // cat
        'cat.Items.Clear()
        'cat.Items.Add(New ListItem("無類別", ""))
        'Dim lstModuleCats As List(Of ModuleCatInfo) = objController.getModuleCats(unit_id)
        'For Each item In lstModuleCats
        '    cat.Items.Add(New ListItem(item.catName, item.ID))
        'Next

        If (Not newCreate) Then
            If (Not Me.IsPostBack) Then

                cmd.CommandText = "select * from news where id = @id"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = nid
                dr = cmd.ExecuteReader
                If (dr.Read()) Then

                    cat.Value = getValue(dr("catid"))
                    pdate.Value = getValue(dr("pdate"))
                    date_s.Value = getValue(dr("stdate"))
                    date_e.Value = getValue(dr("eddate"))
                    news_title.Value = getValue(dr("news_title"))
                    content.Value = getValue(dr("content"))
                    link_href.Value = getValue(dr("link_href"))
                
                    ' // file name
                    For x = 1 To 1
                        Dim suffix As String = IIf(x = 1, "", x)
                        CType(FindControlEx(Me, "fileHref" & suffix), HtmlGenericControl).InnerHtml = ""
                        If (CStr(dr("file_name" & suffix)).Length > 0) Then
                            CType(FindControlEx(Me, "fileHref" & suffix), HtmlGenericControl).InnerHtml = _
                             ghl(dr("file_name" & suffix), "../files/" & dr("file_name" & suffix), "target", "preview") & _
                              spc(5) & ghl("[刪除]", "javascript:;", "id", "btnRemoveFile", "name", "btnRemoveFile", "onclick", "removeFile(this, 'file');") & br
                        End If
                        u.rhfPV("h_fileName" & suffix, dr("file_name" & suffix))
                        CType(FindControlEx(Me, "fileDesc" & suffix), HtmlInputText).Value = dr("file_desc" & suffix)
                    Next x
                End If
                dr.Close()
            End If
        Else
            pdate.Value = toTwDate(Now)
        End If

        ' // button
        btnSave.Text = IIf(newCreate, "新增", "修改")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim fileName(0) As String

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx
        Try
            If (newCreate) Then

                Dim maxsort As Integer = u.getMinSort("news")

                cmd.CommandText = "insert into news (" & _
                "modid, sort, catid, pdate, stdate, " & _
                "eddate, news_title, content, link_href, " & _
                "file_name, " & _
                "file_desc, create_user_id, enabled" & _
                ") values (" & _
                "@modid, @sort, @catid, @pdate, @stdate, " & _
                "@eddate, @news_title, @content, @link_href, " & _
                "@file_name, " & _
                "@file_desc, @create_user_id, @enabled" & _
                "); " & _
                "select @@identity"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@modid", SqlDbType.Int).Value = modid
                cmd.Parameters.Add("@sort", SqlDbType.Int).Value = maxsort
                cmd.Parameters.Add("@catid", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@pdate", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@stdate", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@eddate", SqlDbType.Int).Value = DBNull.Value
                cmd.Parameters.Add("@news_title", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@content", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@link_href", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@file_name", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@file_desc", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@create_user_id", SqlDbType.NVarChar).Value = authInfo.userId
                cmd.Parameters.Add("@enabled", SqlDbType.Bit).Value = 1
                nid = cmd.ExecuteScalar
            End If

            ' // image
            Dim uf As Dictionary(Of String, String) = getUploadFiles(objModCatInfo, objModCatInfo.contentTableName, "id", nid)

            cmd.CommandText = "update news set " & _
            "catid = @catid, " & _
            "pdate = @pdate, stdate = @stdate, eddate = @eddate, " & _
            "news_title = @news_title, content = @content, link_href = @link_href, " & _
            "file_name = @file_name, file_desc = @file_desc " & _
            "where id = @id"

            For x As Integer = 1 To 1
                Dim suffix As String = IIf(x = 1, "", x)
                fileName(x - 1) = uploadFile(Request.Form("h_fileName" & suffix), uf("file_name" & suffix), Request.Files(x), Server.MapPath("~/files/"))
            Next x

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@catid", SqlDbType.Int).Value = DBValue(cat.Value, , DBNull.Value)
            cmd.Parameters.Add("@pdate", SqlDbType.Int).Value = DBValue(pdate.Value)
            cmd.Parameters.Add("@stdate", SqlDbType.Int).Value = DBValue(date_s.Value, , DBNull.Value)
            cmd.Parameters.Add("@eddate", SqlDbType.Int).Value = DBValue(date_e.Value, , DBNull.Value)
            cmd.Parameters.Add("@news_title", SqlDbType.NVarChar).Value = DBValue(news_title.Value)
            cmd.Parameters.Add("@content", SqlDbType.NVarChar).Value = DBValueL(content.Value)
            cmd.Parameters.Add("@link_href", SqlDbType.NVarChar).Value = DBValue(link_href.Value)
            cmd.Parameters.Add("@file_name", SqlDbType.NVarChar).Value = DBValue(fileName(0), DBValue(Request.Form("h_fileName"), ""))
            cmd.Parameters.Add("@file_desc", SqlDbType.NVarChar).Value = DBValue(fileDesc.Value)
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = nid
            cmd.ExecuteNonQuery()

            tx.Commit()

        Catch ex As Exception
            tx.Rollback()
            showAlert("處理資料時發生意外錯誤!", Request.Url.ToString)
            Return
        End Try

        Response.Redirect("news.aspx?mnuid=" & mnuid)
    End Sub
End Class
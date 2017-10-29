Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_page_edit
    Inherits BasePage

    Dim cid As String

    Public ReadOnly Property newCreate As Boolean
        Get
            Return False
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
 
        cid = IIf(Request.Form(prefixName & "ddlCat") Is Nothing, IIf(Request.QueryString("cid") Is Nothing, "", Request.QueryString("cid")), Request.Form(prefixName & "ddlCat"))

        setModCat("modpage")

        requireAdminLogin(objModCatInfo)
        validateAccess(objModCatInfo)

        If (cid.Length > 0) Then
            rf(cid, "pagecat", "id")
            eq(modid, getColVal("pagecat", "modid", cid))
        End If

        setNavigationMenuPath(objModCatInfo)

        loadItem()
        configButton()

    End Sub

    Sub configButton()

        AddHandler Master.navBackClick, AddressOf btnBack_ServerClick
   
        If (mnuid.Length = 0) Then
            u.showNavButton("Back", True)
        End If

    End Sub

    Sub loadItem()

        Dim x As Integer
        Dim suffix As String
        Dim curDate As Date = Now

        u.setInputTextMaxLen("page")

        ' // setting control
        module_control1.Attributes("modCatId") = objModCatInfo.ID
        module_control1.Attributes("selectedValue") = "Content"

        ' // drop-down cat
        ddlCat.Items.Clear()
        Dim objPageController As New PageController(cmd)
        Dim lstPages As List(Of PageCatInfo) = objPageController.getPageCats(modid)
        For Each item In lstPages
            ddlCat.Items.Add(New ListItem(item.catName, item.ID))
        Next
        setDdlVal(ddlCat, cid)

        If (Not newCreate) Then
            If (Not Me.IsPostBack) Then

                ' // loading settings for page
                cmd.CommandText = "select * from page where catid = @catid"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@catid", SqlDbType.Int).Value = cid

                dr = cmd.ExecuteReader
                If (dr.Read()) Then

                    content.Value = dr("content")
                    linkHref.Value = dr("link_href")

                    ' // image
                    For x = 1 To 1
                        suffix = IIf(x = 1, "", x)
                        CType(FindControlEx(Me, "imgHref" & suffix), HtmlGenericControl).InnerHtml = ""
                        If (CStr(dr("img_name_thumb" & suffix)).Length > 0) Then
                            CType(FindControlEx(Me, "imgHref" & suffix), HtmlGenericControl).InnerHtml = _
                              ghl(dr("img_name_thumb" & suffix), "../files/" & dr("img_name_thumb" & suffix), "target", "preview") & _
                              spc(5) & ghl("[刪除]", "javascript:;", "id", "btnRemoveFile", "name", "btnRemoveFile", "onclick", "removeFile(this, 'img2');") & br
                        End If
                        u.rhfPV("imgNameThumb" & suffix, dr("img_name_thumb" & suffix))

                        If (FindControlEx(Me, "imgDesc" & suffix) IsNot Nothing) Then
                            CType(FindControlEx(Me, "imgDesc" & suffix), HtmlInputText).Value = dr("img_desc" & suffix)
                        End If
                        If (FindControlEx(Me, "imgAlign" & suffix) IsNot Nothing) Then
                            CType(FindControlEx(Me, "imgAlign" & suffix), HtmlSelect).Value = dr("img_align" & suffix)
                        End If
                    Next x

                    ' // file name
                    For x = 1 To 1

                        suffix = IIf(x = 1, "", x)

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
        End If

    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        Dim suffix As String
        Dim imgNameThumb(0), fileName(1) As String
    
        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try

            ' // image
            Dim uf As Dictionary(Of String, String) = getUploadFiles(objModCatInfo, objModCatInfo.contentTableName, "catid", cid)

            cmd.CommandText = "update page set " & _
            "content = @content, link_href = @link_href, " & _
            "img_name_thumb = @img_name_thumb, img_desc = @img_desc, img_align = @img_align, " & _
            "file_name = @file_name, file_desc = @file_desc " & _
            "where catid = @catid"

            imgNameThumb(0) = uploadImgThumbWithFlash(Request.Form("imgNameThumb"), uf("img_name_thumb"), Request.Files(0), Server.MapPath("../files/"), "con_img")

            For x = 1 To 1
                suffix = IIf(x = 1, "", x)
                fileName(x - 1) = uploadFile(Request.Form("h_fileName" & suffix), uf("file_name" & suffix), Request.Files(x), Server.MapPath("../files/"))
            Next x

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@content", SqlDbType.NText).Value = DBValueL(Request.Form(prefixName & "content"))
            cmd.Parameters.Add("@link_href", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "linkHref"))
            cmd.Parameters.Add("@img_name_thumb", SqlDbType.NVarChar).Value = DBValue(imgNameThumb(0), DBValue(Request.Form("imgNameThumb"), ""))
            cmd.Parameters.Add("@img_desc", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "imgDesc"))
            cmd.Parameters.Add("@img_align", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "imgAlign"))
            cmd.Parameters.Add("@file_name", SqlDbType.NVarChar).Value = DBValue(fileName(0), DBValue(Request.Form("h_fileName"), ""))
            cmd.Parameters.Add("@file_desc", SqlDbType.NVarChar).Value = DBValue(Request.Form(prefixName & "fileDesc"))
            cmd.Parameters.Add("@catid", SqlDbType.Int).Value = cid
            cmd.ExecuteNonQuery()

            Dim objEventController As New EventController(cmd)
            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception

            tx.Rollback()
            u.alert("儲存資料時發生意外錯誤!", fullRawUrl)
            Return

        End Try

        u.alertAjax(Nothing, fullRawUrl)
    End Sub

    Protected Sub ddlCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCat.SelectedIndexChanged
        Response.Redirect("page_edit.aspx?mnuid=" & mnuid & "&cid=" & cid)
    End Sub

    Protected Sub btnCat_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCat.ServerClick
        Response.Redirect("page_cat.aspx?mnuid=" & mnuid)
    End Sub

    Protected Sub btnBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("module.aspx?mcid=" & objModCatInfo.ID)
    End Sub
End Class
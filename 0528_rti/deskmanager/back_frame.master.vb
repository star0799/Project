Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_back_frame
    Inherits System.Web.UI.MasterPage

    Dim cn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim da As New SqlDataAdapter
    Dim dr As SqlDataReader
    Dim dt As New DataTable
    Dim u As New Utilities(Nothing, cn, cmd, da)

    Public noframe As String = ""
    Public userId As String = ""
    Public dept As String

    Dim mnuid As String
    Public outMenu As String = ""
    Public blk As String = ""

    Public Event navBackClick As Utilities.navBackClickHandler
    Public Event navUnitSetClick As Utilities.navUnitSetClickHandler
    Public Event navExportClick As Utilities.navExportClickHandler
    Public Event navAddClick As Utilities.navAddClickHandler
    Public Event navCopyClick As Utilities.navCopyClickHandler
    Public Event navImportClick As Utilities.navImportClickHandler

    Private _basePage As BasePage = Nothing
    Protected ReadOnly Property basePage As BasePage
        Get
            If (_basePage Is Nothing) Then
                _basePage = DirectCast(Page, BasePage)
            End If
            Return _basePage
        End Get
    End Property

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Me.ID = "master"

        mnuid = cv(Request.QueryString("mnuid"))

        u.openDB()

        u.hideAllNavButton()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadItem()

    End Sub

    Sub loadItem()

        Response.Cache.SetNoStore()
       
        basePage.siteConfig()

        ' // site lang
        basePage.updateLangOptions()

        loadCPMenu()

        ' // loading settings for dynamic generated menus
        loadDynMenu()
    End Sub

    Sub loadCPMenu()

        Dim t As New MyHtmlTable(tblMenu)

        Dim objUserController As New ManageUserController(cmd)
        Dim objUserInfo As ManageUserInfo = objUserController.getCurrentUser()
        If (objUserInfo Is Nothing) Then
            NavigateUrl.navigate(NavigateUrl.loginPageUrl)
        End If

        Dim authInfo As AuthInfo = objUserController.getAuthInfo()
        If (authInfo Is Nothing) Then
            NavigateUrl.navigate(NavigateUrl.loginPageUrl)
        End If

        Dim objController As New Controller(cmd)
        Dim unitInfo As UnitInfo = objController.getUnitInfo(authInfo.unitId)
        If (unitInfo Is Nothing) Then
            NavigateUrl.navigate(NavigateUrl.loginPageUrl)
        End If
        dept = unitInfo.name

        userId = objUserInfo.userId

        Dim x, y As Integer
        Dim cri As String = Nothing
        Dim href As String = Nothing
        Dim dt2 As New DataTable
        Dim mnuVarName As String = Nothing
        Dim lastMnuVarName As String = Nothing
        Dim content As String = Nothing
        Dim fItem As ArrayList = Nothing
        Dim fLangItem As ArrayList = Nothing
        Dim addMenu As Boolean
        Dim has2ndLayerMenu As Boolean

        Dim menuXPos As Integer = -5
        Dim menuYPos As Integer = 30
        Dim menuWidth As Integer = 161
        Dim menuHeight As Integer = 22

        If (objUserInfo.readonly = False) Then
            fItem = Session("fixedItem")
            fLangItem = Session(basePage.langId & "_fixedLangItem")
        End If

        ' // 1st layer menunav
        Dim firstCPItem As Boolean = True
        t = New MyHtmlTable(tblCPMenu)

        da.SelectCommand.CommandText = "SELECT         dbo.cpmenu.* " & _
        "FROM             dbo.cpmenu " & _
        "WHERE         (enabled = 1) " & _
        "ORDER BY  sort"

        dt.Clear()
        da.Fill(dt)

        With dt
            For x = 0 To .Rows.Count - 1

                ' // 2nd layer menunav (javascript floating menunav)
                da.SelectCommand.Parameters.Clear()
                cri = "1 = 1"

                da.SelectCommand.CommandText = "SELECT         dbo.menunav.* " & _
                "FROM             dbo.menunav " & _
                "WHERE         (main_id = @main_id) AND (" & cri & ") AND (enabled = 1) " & _
                "ORDER BY  sort"

                da.SelectCommand.Parameters.Add("@main_id", SqlDbType.NVarChar).Value = .Rows(x)("main_id")

                dt2.Clear()
                da.Fill(dt2)

                With dt2
                    If (.Rows.Count > 0) Then
                        mnuVarName = "window_mm_menu_20080620_" & x + 1
                        content = "window." & mnuVarName & " = new Menu(" & """" & "root" & """" & "," & menuWidth & "," & menuHeight & "," & """" & "" & """" & ",12," & """" & "#000000" & """" & "," & """" & "#FFFFFF" & """" & "," & """" & "#FFFFFF" & """" & "," & """" & "#055386" & """" & "," & """" & "left" & """" & "," & """" & "middle" & """" & ",3,0,1000,-5,7,true,true,true,0,true,true);" & vbCrLf
                    End If

                    has2ndLayerMenu = False
                    For y = 0 To .Rows.Count - 1
                        addMenu = False
                        If (objUserInfo.readonly) Then
                            addMenu = True
                        ElseIf (.Rows(y)("main_id") = "content") Then
                            addMenu = True
                        End If
                        If (fItem IsNot Nothing AndAlso CType(fItem, ArrayList).Contains(CInt(.Rows(y)("item_id")))) Then
                            addMenu = True
                        End If
                        If (fLangItem IsNot Nothing AndAlso CType(fLangItem, ArrayList).Contains(CInt(.Rows(y)("item_id")))) Then
                            addMenu = True
                        End If

                        If (addMenu) Then
                            has2ndLayerMenu = True
                            href = "location.href='" & .Rows(y)("link_href") & "';"
                            content &= mnuVarName & ".addMenuItem('" & convSingleQuote(.Rows(y)("menu_name")) & "'," & """" & href & """" & ");" & vbCrLf
                            lastMnuVarName = mnuVarName
                        End If
                    Next y

                    If (has2ndLayerMenu) Then       ' // has 2nd layer menunav
                        content &= mnuVarName & ".hideOnMouseOut=true;" & vbCrLf
                        content &= mnuVarName & ".bgColor='#becfeb';" & vbCrLf
                        content &= mnuVarName & ".menuBorder=1;" & vbCrLf
                        content &= mnuVarName & ".menuLiteBgColor='#FFFFFF';" & vbCrLf
                        content &= mnuVarName & ".menuBorderBgColor='#becfeb';" & vbCrLf & vbCrLf

                        If (outMenu.Length = 0) Then
                            outMenu &= "if (window." & mnuVarName & ") {return;}"
                        End If
                        outMenu &= content
                    End If
                End With

                ' // *** 1st layer menunav
                If (.Rows(x)("main_id") = "content" OrElse has2ndLayerMenu) Then

                    ' // icon
                    imgCPMenu.Src = "images/" & .Rows(x)("img_name")

                    If (firstCPItem) Then
                        firstCPItem = False
                        t.NewRowCell(0, 0, imgCPMenu)
                    Else
                        t.NewCell(0, 0, imgCPMenu)
                    End If

                    ' // cp menu name
                    hlkCPMenu.ID = "hlkCPMenu_" & x + 1
                    hlkCPMenu.HRef = "javascript:;"
                    hlkCPMenu.Target = Nothing
                    hlkCPMenu.InnerHtml = .Rows(x)("cp_menu_name")

                    If (dt2.Rows.Count > 0) Then
                        hlkCPMenu.Attributes("onmouseover") = "MM_showMenu(" & mnuVarName & "," & menuXPos & "," & menuYPos & ",null,'" & hlkCPMenu.ClientID & "')"
                        hlkCPMenu.Attributes("onmouseout") = "MM_startTimeout();"
                    Else
                        hlkCPMenu.Attributes("onmouseover") = Nothing
                        hlkCPMenu.Attributes("onmouseout") = Nothing
                    End If

                    t.NewCell(0, 1, hlkCPMenu)
                    ' // h-separator
                    If (x <> .Rows.Count - 1) Then
                        t.NewCell(0, 2)
                    End If
                End If
            Next x
        End With

        If (outMenu.Length > 0) Then
            outMenu &= lastMnuVarName & ".writeMenus();"
        End If
    End Sub

    Sub loadDynMenu()

        Dim t As New MyHtmlTable(tblMenu)

        Dim objUserController As New ManageUserController(cmd)
        Dim objUserInfo As ManageUserInfo = objUserController.getCurrentUser()
        If (objUserInfo Is Nothing) Then
            Return
        End If

        Dim x, y, n As Integer
        Dim t2, t3 As MyHtmlTable
        Dim href As String = Nothing
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim has1stLayerMenu, has2ndLayerMenu, has3rdLayerMenu As Boolean
        Dim mnuid As ArrayList = Nothing

        Dim cri As String = Nothing
        Dim searchCri As String = ""
        Dim content As String = Nothing
        Dim menuId As New ArrayList
        Dim matchMenuId As New ArrayList
        Dim fid As Integer

        If (objUserInfo.readonly = False) Then
            mnuid = Session(basePage.langId & "_mnuid")
            If (mnuid Is Nothing) Then
                Return
            End If
        End If

        ' // menu perms
        da.SelectCommand.Parameters.Clear()
        cri = "1 = 1"
    
        If (Not objUserInfo.readonly) Then

            ' // find all menus
            For x = 0 To mnuid.Count - 1
                menuId.Add(mnuid(x))
                matchMenuId.Add(mnuid(x))
            Next x

            For x = 0 To menuId.Count - 1

                ' // find node parents (this path only)
                fid = menuId(x)
                While (fid <> 0)
                    cmd.CommandText = "select * from menu where id = @id"
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = fid
                    dr = cmd.ExecuteReader
                    If (dr.Read()) Then
                        fid = dr("fid")
                        If (dr("fid") <> 0) Then
                            If (matchMenuId.Contains(dr("fid")) = False) Then
                                matchMenuId.Add(dr("fid"))
                            End If
                        End If
                    Else
                        fid = 0
                    End If
                    dr.Close()
                End While
            Next x

            content = ""
            For x = 0 To matchMenuId.Count - 1
                content &= IIf(content.Length > 0, ", ", "") & matchMenuId(x)
            Next

            If (content.Length > 0) Then
                cri &= " AND menu.id IN (" & content & ")"
            Else
                cri &= " AND 1 = 0"
            End If

        End If

        ' // 1st layer menu
        t = New MyHtmlTable(tblMenu)

        da.SelectCommand.CommandText = "SELECT         dbo.menu.* " & _
        "FROM             dbo.menu " & _
        "WHERE         (lang_id = @lang_id) AND (menu_type = @menu_type) AND (fid = 0) AND (" & cri & ") " & _
        "ORDER BY  sort"

        da.SelectCommand.Parameters.Add("@lang_id", SqlDbType.Int).Value = basePage.langId
        da.SelectCommand.Parameters.Add("@menu_type", SqlDbType.NVarChar).Value = IIf(HttpContext.Current.Items("menuType") IsNot Nothing, HttpContext.Current.Items("menuType"), getCookie("menuType"))

        dt.Clear()
        da.Fill(dt)

        With dt
            For x = 0 To .Rows.Count - 1

                has1stLayerMenu = True

                If (objUserInfo.readonly OrElse CType(mnuid, ArrayList).Contains(CInt(.Rows(x)("id")))) Then
                    hlkMenu.HRef = basePage.getBackMenuLink(.Rows(x))
                    hlkMenu.Attributes("style") = Nothing
                Else
                    hlkMenu.HRef = Nothing
                    hlkMenu.Attributes("style") = "color:#000000"
                End If

                If (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt.Rows(x)("id"), Me.mnuid)) Then
                    hlkMenu.Attributes("class") = "Text01OnLink"
                Else
                    hlkMenu.Attributes("class") = "Text03Link"
                End If

                cmd.CommandText = "SELECT         COUNT(*) AS cnt " & _
                "FROM             dbo.menu " & _
                "WHERE         (fid = @fid)"

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@fid", SqlDbType.Int).Value = .Rows(x)("id")
                has2ndLayerMenu = (cmd.ExecuteScalar > 0)

                ' // expand
                hlkExpand.ID = "hlkExpand_" & x + 1

                If (has2ndLayerMenu) Then
                    hlkExpand.HRef = "javascript:;"
                    hlkExpand.Attributes("onclick") = "myclick(this, 'master_tblMenu2_" & x + 1 & "');"
                    hlkExpand.Visible = True
                Else
                    hlkExpand.HRef = "javascript:;"
                    hlkExpand.Attributes("onclick") = Nothing
                    hlkExpand.Visible = False
                End If

                If (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt.Rows(x)("id"), Me.mnuid) AndAlso dt.Rows(x)("id") <> Me.mnuid) Then
                    imgExpand.Src = "images/Point02(-).gif"
                Else
                    imgExpand.Src = "images/Point02.gif"
                End If

                hlkMenu.InnerHtml = .Rows(x)("menu_name")
                t.NewRowCell(0, tblMenuText1)

                ' // 2nd layer menu
                t2 = New MyHtmlTable(tblMenu2)
                t2.innerTable.ID = "tblMenu2_" & x + 1

                da.SelectCommand.CommandText = "SELECT         dbo.menu.* " & _
                "FROM             dbo.menu " & _
                "WHERE         (fid = @fid) " & _
                "ORDER BY  sort"

                da.SelectCommand.Parameters.Clear()
                da.SelectCommand.Parameters.Add("@fid", SqlDbType.Int).Value = .Rows(x)("id")

                dt2.Clear()
                da.Fill(dt2)

                With dt2
                    For y = 0 To .Rows.Count - 1

                        If (objUserInfo.readonly OrElse CType(mnuid, ArrayList).Contains(CInt(.Rows(y)("id")))) Then
                            hlkMenu2.HRef = basePage.getBackMenuLink(.Rows(y))
                            hlkMenu2.Attributes("style") = Nothing
                        Else
                            hlkMenu2.HRef = Nothing
                            hlkMenu2.Attributes("style") = "color:#000000"
                        End If

                        If (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt2.Rows(y)("id"), Me.mnuid)) Then
                            hlkMenu2.Attributes("class") = "Text02OnLink"
                        Else
                            hlkMenu2.Attributes("class") = "Text02Link"
                        End If

                        cmd.CommandText = "SELECT         COUNT(*) AS cnt " & _
                        "FROM             dbo.menu " & _
                        "WHERE         (fid = @fid) "

                        cmd.Parameters.Clear()
                        cmd.Parameters.Add("@fid", SqlDbType.Int).Value = .Rows(y)("id")
                        has3rdLayerMenu = (cmd.ExecuteScalar > 0)

                        ' // expand
                        hlkExpand2.ID = "hlkExpand2_" & x + 1 & "_" & y + 1

                        If (has3rdLayerMenu) Then
                            hlkExpand2.HRef = "javascript:;"
                            hlkExpand2.Attributes("onclick") = "myclick(this, 'master_tblMenu3_" & x + 1 & "_" & y + 1 & "');"
                            hlkExpand2.Visible = True
                        Else
                            hlkExpand2.HRef = "javascript:;"
                            hlkExpand2.Attributes("onclick") = Nothing
                            hlkExpand2.Visible = False
                        End If

                        If (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt2.Rows(y)("id"), Me.mnuid) AndAlso dt2.Rows(y)("id") <> Me.mnuid) Then
                            imgExpand2.Src = "images/Point02(-).gif"
                        Else
                            imgExpand2.Src = "images/Point02.gif"
                        End If

                        hlkMenu2.InnerHtml = .Rows(y)("menu_name")
                        t2.NewRowCell(0, tblMenuText2)

                        ' // 3rd layer menu
                        t3 = New MyHtmlTable(tblMenu3)
                        t3.innerTable.ID = "tblMenu3_" & x + 1 & "_" & y + 1

                        da.SelectCommand.CommandText = "SELECT         dbo.menu.* " & _
                        "FROM             dbo.menu " & _
                        "WHERE         (fid = @fid) " & _
                        "ORDER BY  sort"

                        da.SelectCommand.Parameters.Clear()
                        da.SelectCommand.Parameters.Add("@fid", SqlDbType.Int).Value = .Rows(y)("id")

                        dt3.Clear()
                        da.Fill(dt3)

                        With dt3
                            For n = 0 To .Rows.Count - 1

                                If (objUserInfo.readonly OrElse CType(mnuid, ArrayList).Contains(CInt(.Rows(n)("id")))) Then
                                    hlkMenu3.HRef = basePage.getBackMenuLink(.Rows(n))
                                    hlkMenu3.Attributes("style") = Nothing
                                Else
                                    hlkMenu3.HRef = Nothing
                                    hlkMenu3.Attributes("style") = "color:#000000"
                                End If

                                If (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt3.Rows(n)("id"), Me.mnuid)) Then
                                    hlkMenu3.Attributes("class") = "Text03OnLink"
                                Else
                                    hlkMenu3.Attributes("class") = "Text02Link"
                                End If

                                ' // expand
                                hlkExpand3.HRef = Nothing
                                hlkExpand3.Attributes("onclick") = Nothing

                                hlkMenu3.InnerHtml = .Rows(n)("menu_name")
                                t3.NewRowCell(0, tblMenuText3)

                            Next n

                            If (.Rows.Count > 0) Then

                                t2.NewRowCell(1, t3.innerTable)

                                If (Not (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt2.Rows(y)("id"), Me.mnuid) AndAlso dt2.Rows(y)("id") <> Me.mnuid)) Then
                                    blk &= "master_tblMenu3_" & x + 1 & "_" & y + 1 & ".style.display = 'none';" & vbCrLf
                                End If

                            End If
                        End With
                    Next y

                    If (.Rows.Count > 0) Then

                        t.NewRowCell(1, t2.innerTable)

                        If (Not (Me.mnuid.Length > 0 AndAlso basePage.containMenu(dt.Rows(x)("id"), Me.mnuid) AndAlso dt.Rows(x)("id") <> Me.mnuid)) Then
                            blk &= "master_tblMenu2_" & x + 1 & ".style.display = 'none';" & vbCrLf
                        End If
                    End If
                End With
            Next x

            If (has1stLayerMenu = False) Then
                t = New MyHtmlTable(tblMenu)
            End If
        End With

    End Sub

    Sub findMenuNodeChildren(ByVal matchMenuId As ArrayList, ByVal unitId As Integer)

        Dim x, fid As Integer
        Dim tmp As New ArrayList

        ' // find menu node children (not include self-node)
        fid = unitId
        If (fid <> 0) Then
            cmd.CommandText = "select * from menu where fid = @fid"
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@fid", SqlDbType.Int).Value = fid
            dr = cmd.ExecuteReader
            While (dr.Read())
                fid = dr("id")
                tmp.Add(dr("id"))
            End While
            dr.Close()
        End If

        For x = 0 To tmp.Count - 1
            If (matchMenuId.Contains(tmp(x)) = False) Then
                matchMenuId.Add(tmp(x))
            End If
            findMenuNodeChildren(matchMenuId, tmp(x))
        Next x
    End Sub

    Protected Sub ddlSiteLang_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSiteLang.SelectedIndexChanged
        setCookie("lang", Request.Form(ddlSiteLang.UniqueID))
        Response.Redirect("main.aspx")
    End Sub

    Protected Sub hlkNavBack_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlkNavBack.ServerClick
        RaiseEvent navBackClick(sender, e)
    End Sub

    Protected Sub hlkNavUnitSet_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlkNavUnitSet.ServerClick
        RaiseEvent navUnitSetClick(sender, e)
    End Sub

    Protected Sub hlkNavExport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlkNavExport.ServerClick
        RaiseEvent navExportClick(sender, e)
    End Sub

    Protected Sub hlkNavAdd_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlkNavAdd.ServerClick
        RaiseEvent navAddClick(sender, e)
    End Sub

    Protected Sub hlkNavImport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles hlkNavImport.ServerClick
        RaiseEvent navImportClick(sender, e)
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        u.closeDB(cn)
    End Sub
End Class
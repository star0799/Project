Imports System.Data
Imports System.Data.Sqlclient

Partial Class deskmanager_home_layout_edit
    Inherits BasePage

    Dim itemid As String

    Public ReadOnly Property newCreate As Boolean
        Get
            Return False
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        itemid = 702

        setLangModule("homelayout")

        requireAdminLogin(itemid)
        setNavigationPath(itemid)

        loadItem()
        configButton()

    End Sub

    Sub configButton()
    End Sub

    Sub loadItem()

        setImageSizeText("intro_video", "imgName")

        If (Not newCreate) Then
            If (Not Me.IsPostBack) Then

                cmd.CommandText = "select * from homelayout where lang_id = @lang_id"
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = langId

                dr = cmd.ExecuteReader
                If (dr.Read()) Then

                    intro.Value = dr("intro")
                    linkHref.Value = dr("link_href")
                End If
                dr.Close()
            End If
        End If

        ' // button
        btnSave.Value = IIf(newCreate, "新增", "修改")
    End Sub

    Protected Sub btnSave_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.ServerClick

        tx = cn.BeginTransaction
        cmd.Transaction = tx
        da.SelectCommand.Transaction = tx

        Try
            cmd.CommandText = "update homelayout set " & _
            "intro = @intro, link_href = @link_href " & _
            "where lang_id = @lang_id"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@intro", SqlDbType.NVarChar).Value = DBValue(Request.Form(intro.UniqueID))
            cmd.Parameters.Add("@link_href", SqlDbType.NVarChar).Value = DBValue(Request.Form(linkHref.UniqueID))
            cmd.Parameters.Add("@lang_id", SqlDbType.Int).Value = langId
            cmd.ExecuteNonQuery()

            Dim objEventController As New EventController(cmd)
            objEventController.writeLog()

            tx.Commit()

        Catch ex As Exception

            tx.Rollback()
            u.alert("儲存資料時發生意外錯誤!", fullRawUrl)
            Return

        End Try

        u.alertAjax(Nothing, Me.ViewState("fromURL"))
    End Sub
End Class
Imports System.Data
Imports System.Data.SqlClient

Partial Class deskmanager_controls_module_control
    Inherits BaseUserControl

    Dim modCatId As Integer
    Public ReadOnly Property showControl As Boolean
        Get
            Return (ddlSetting.Items.Count > 0)
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        modCatId = Me.Attributes("modCatId")

        loadItem()

    End Sub

    Sub loadItem()

        ' // drop-down setting
        If (Not Me.IsPostBack) Then
            ddlSetting.Items.Clear()
            Dim lstModuleControls As List(Of ModuleControlInfo) = objController.getModuleControls(modCatId)
            For Each item In lstModuleControls.Where(Function(p) p.enabled)
                ddlSetting.Items.Add(New ListItem(item.title, item.value))
            Next
            setDdlVal(ddlSetting, Me.Attributes("selectedValue"))
        End If

    End Sub

    Protected Sub ddlSetting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSetting.SelectedIndexChanged
        Dim objModuleControl As ModuleControlInfo = objController.getModuleControl(modCatId, ddlSetting.SelectedValue)
        Dim url As String = objModuleControl.url & If(objModuleControl.url.IndexOf("?") <> -1, "&", "?") & "mnuid=" & mnuid & "&mcid=" & modCatId
        Response.Redirect(url)
    End Sub
End Class
Imports System.Data
Imports Telerik.Web.UI
Imports System.IO
'Imports afiClassLibrary1

Partial Class report
    Inherits BasePage

    Dim itemid As String
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        itemid = 1312

        requireAdminLoginMenu(itemid)
        setNavigationMenuPath()

        loadItem()
        configButton()
    End Sub

    Sub configButton()
    End Sub

    Private Sub loadItem()

        If (Not Me.IsPostBack) Then

            Dim cri As String = "1 = 1"
            
            reportCat.Items.Clear()
            cmd.CommandText = "SELECT  r.code, r.name " & _
            "FROM     dbo.report AS r " & _
            "WHERE (" & cri & ") " & _
            "ORDER BY code"

            cmd.Parameters.Clear()
            dr = cmd.ExecuteReader()
            While (dr.Read())
                reportCat.Items.Add(New ListItem(String.Format("{0}", dr("name")), dr("code")))
            End While
            dr.Close()

            Dim curDate As Date = Now
            report_date_s.Value = toTwDate(New DateTime(curDate.Year, 1, 1))
            report_date_e.Value = toTwDate(curDate)
        End If

        'reportViewer.Visible = False
    End Sub

    'Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.ServerClick

    '    If (String.IsNullOrEmpty(report_date_s.Value) AndAlso String.IsNullOrEmpty(report_date_e.Value)) Then
    '        showAlert("請選擇起迄日期!")
    '        Return
    '    End If

    '    Dim curDate As Date = Now
    '    Dim dt As New DataTable
    '    da.SelectCommand.CommandText = "select * from report_all where 1 = 0"
    '    da.Fill(dt)

    '    da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT e.id, e.name " & _
    '    "FROM     dbo.execstatus AS e " & _
    '    "ORDER BY id"
    '    Dim dtExec As New DataTable
    '    da.Fill(dtExec)

    '    da.SelectCommand.CommandText = "SELECT  TOP (100) PERCENT dept.id, dept.name " & _
    '    "FROM     dbo.unit AS dept " & _
    '    "WHERE  (dept.unit_type = '0') " & _
    '    "ORDER BY id"
    '    Dim dtSchool As New DataTable
    '    da.Fill(dtSchool)

    '    Dim cri As String = String.Format("su.unit_id = {0}", authInfo.unitId)
 
    '        Case Else
    '            If (Not String.IsNullOrEmpty(report_date_s.Value)) Then
    '                cri &= " AND reg_drop_date >= " & report_date_s.Value
    '            End If
    '            If (Not String.IsNullOrEmpty(report_date_e.Value)) Then
    '                cri &= " AND reg_drop_date <= " & report_date_e.Value
    '            End If

    '    End Select

    '    Dim objReport As Telerik.Reporting.Report = getReportObject(reportCat.Value, cri, unitInfo.name, unitInfo.unitType)
    '    Select Case reportCat.Value
    '        Case "0141", "0142", "0144", "1010", "1040", "3001", "3002", "3003", "3004"
    '            'radList.DataSource = dt
    '            'radList.DataBind()
    '            objReport.DataSource = dt
    '    End Select

    '    If (objReport.ReportParameters.Contains("report_date_s")) Then
    '        objReport.ReportParameters("report_date_s").Value = IIf(Not String.IsNullOrEmpty(report_date_s.Value), report_date_s.Value, Nothing)
    '    End If
    '    If (objReport.ReportParameters.Contains("report_date_e")) Then
    '        objReport.ReportParameters("report_date_e").Value = IIf(Not String.IsNullOrEmpty(report_date_e.Value), report_date_e.Value, Nothing)
    '    End If

    '    'Response.Buffer = False
    '    'For Each item As Telerik.Reporting.ReportParameter In objReport.ReportParameters
    '    '    out(item.Name & ": " & item.Type.ToString() & ": " & item.Value & br)
    '    'Next

    '    reportViewer.ShowNavigationGroup = False
    '    reportViewer.ShowRefreshButton = False
    '    reportViewer.ShowPrintButton = False
    '    reportViewer.ShowPrintPreviewButton = False
    '    reportViewer.ViewMode = Telerik.ReportViewer.WebForms.ViewMode.PrintPreview
    '    reportViewer.Visible = True

    '    Dim reportSource As New Telerik.Reporting.InstanceReportSource
    '    reportSource.ReportDocument = objReport
    '    reportViewer.ReportSource = reportSource
    '    reportViewer.RefreshReport()
    'End Sub
End Class
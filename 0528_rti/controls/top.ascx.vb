Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization

Partial Class controls_top
    Inherits FrontBaseUserControl

    Public isLogon As Boolean
    Public uName As String
    Dim userController As New UserController(cmd)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ssid As String = basePage.getParam("ssid")
        If (Not String.IsNullOrEmpty(ssid)) Then
            Try
                Dim htmlController As New HtmlController
                Dim dic As New Dictionary(Of String, String)
                dic.Add("ssid", ssid)
                Dim postData As String = combineString(dic)

                Dim responseData As ResponseData = htmlController.postHtml(String.Format("http://www.rti.org.tw/login2/?ssid={0}", ssid), postData)
                If (responseData.success AndAlso Not String.IsNullOrEmpty(responseData.html)) Then
                    Dim js As New JavaScriptSerializer
                    Dim info As JsonUserInfo = js.Deserialize(Of JsonUserInfo)(responseData.html)
                    If (info.result = 1) Then
                        userController.userLogin(info.userId, info.name, Nothing, Nothing)
                    End If
                End If
            Catch ex As Exception
            End Try
        End If

        'If (Not Me.IsPostBack) Then
        '    hlkLogin.HRef = String.Format("http://www.rti.org.tw/login2/?done={0}&eventId=arch", HttpUtility.UrlEncode(fullRawUrl))
        'End If

        Dim objUserInfo As UserInfo = userController.getCurrentUser()
        If (objUserInfo IsNot Nothing) Then
            isLogon = True
            uName = objUserInfo.name
        End If
    End Sub

    Public Function combineString(dic As Dictionary(Of String, String)) As String
        Return String.Join("&", dic.Select(Function(p) String.Format("{0}={1}", p.Key, HttpUtility.UrlEncode(p.Value))))
    End Function

    Protected Sub hlkLogout_ServerClick(sender As Object, e As System.EventArgs) Handles hlkLogout.ServerClick
        userController.signOutCurrentUser()
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub hlkLogin_ServerClick(sender As Object, e As System.EventArgs) Handles hlkLogin.ServerClick
        'Response.Redirect(String.Format("http://www.rti.org.tw/login2/?done={0}&eventId=arch", HttpUtility.UrlEncode(fullRawUrl)))
        userController.userLogin("test", "test name", Nothing, Nothing)
        Response.Redirect(fullRawUrl)
    End Sub
End Class

Public Class JsonUserInfo
    Public result As Integer
    Public userId As String
    Public name As String
End Class
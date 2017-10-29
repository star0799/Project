Imports System.Data
Imports System.Data.Sqlclient
Imports System.Xml

Partial Class order
    Inherits FrontBasePage

    Public aid As String
    Public identifierText, titleText As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        aid = getQuery("aid")

        rf(aid, "arch", "id", "p_ispublic = 2", False)

        loadItem()
    End Sub

    Sub loadItem()

        If (aid.Length > 0) Then
            cmd.CommandText = "SELECT  a.* " & _
            "FROM     dbo.arch AS a " & _
            "WHERE  (a.id = @id)"

            cmd.Parameters.Clear()
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = aid
            dr = cmd.ExecuteReader
            If (dr.Read()) Then
                identifier.Value = getValue(dr("identifier"))
                p_title.Value = getValue(dr("p_title"))
            End If
            dr.Close()
        End If

        pageTitle = "訂購"
    End Sub

    Protected Sub btnSubmit_ServerClick(sender As Object, e As System.EventArgs) Handles btnSubmit.ServerClick
       
        Try
            Dim m As New Mail()

            'Send mail (to admin)
            Dim receiverEmail As String = Nothing
            Dim mailSubject As String = Nothing
            receiverEmail = "chiang@rti.org.tw" ' "haorshiu@gmail.com"
            mailSubject = "訂購需求通知"

            Dim body As String
            body = IO.File.ReadAllText(Page.Server.MapPath("~/App_Data/order.html"))
            body = Replace(body, "{identifier}", DBValue(identifier.Value))
            body = Replace(body, "{p_title}", DBValue(p_title.Value))
            body = Replace(body, "{purpose}", DBValue(purpose.Value))
            body = Replace(body, "{unit}", DBValue(unit.Value))
            body = Replace(body, "{qty}", DBValue(qty.Value))
            body = Replace(body, "{userName}", DBValue(userName.Value))
            body = Replace(body, "{phone}", DBValue(phone.Value))
            body = Replace(body, "{email}", DBValue(email.Value))

            m.senderName = DBValue(userName.Value)
            m.senderEmail = DBValue(email.Value)
            m.sendMail(receiverEmail, mailSubject, body, Nothing, Nothing)

        Catch ex As Exception
            'tx.Rollback()
            showAlert("處理資料時發生意外錯誤!", Request.Url.ToString)
            Return
        End Try

        showAlert("您的訂購需求已成功送出!", fullRawUrl)
    End Sub
End Class
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging

Partial Class arch_file
    Inherits BasePage

    Dim rid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rid = getQuery("rid")

        loadItem()
    End Sub

    Sub loadItem()
        Dim userController As New UserController(cmd)
        Dim objUserInfo As UserInfo = userController.getCurrentUser()
        If (objUserInfo Is Nothing) Then
            Response.End()
        End If

        Dim id As Integer
        Try
            Dim tmp() As String = Crypto.Decrypt(rid).Split("|"c)
            id = Convert.ToInt32(tmp(0))
        Catch ex As Exception
            Response.End()
        End Try

        Dim r As New AppSettingsReader
        Dim filePath As String = r.GetValue("metaFilePath", GetType(String))
        Dim fileName As String = ""
        cmd.CommandText = "SELECT  f.*, a.identifier " & _
        "FROM     dbo.archfile AS f INNER JOIN " & _
        "               dbo.arch AS a ON f.aid = a.id " & _
        "WHERE  (f.id = @id) AND (a.p_ispublic = 2)"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            Dim identifier As String = getValue(dr("identifier"))
            fileName = String.Format("{0}{1}\{2}", filePath, getIdentifier(identifier), getValue(dr("file_name")))
        End If
        dr.Close()

        If (Not String.IsNullOrEmpty(fileName)) Then
            Try
                Select Case Path.GetExtension(fileName).ToLower
                    Case ".txt"
                        Dim text As String = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(fileName))
                        Using img As Bitmap = drawText(text, New Font("標楷體", 12, FontStyle.Regular))
                            Using ms As New MemoryStream
                                img.Save(ms, ImageFormat.Jpeg)
                                ms.WriteTo(Response.OutputStream)
                            End Using
                        End Using
                        Response.ContentType = "image/jpeg"

                    Case Else
                        Dim strContentDisposition As String

                        ' // file name
                        filePath = fileName
                        fileName = Path.GetFileName(fileName)
                        If (HttpContext.Current.Request.Browser.IsBrowser("IE")) Then
                            fileName = HttpUtility.UrlPathEncode(fileName)
                            strContentDisposition = String.Format("{0}; filename=""{1}""", "attachment", fileName)
                        Else
                            fileName = Server.UrlDecode(fileName)
                            strContentDisposition = String.Format("{0}; filename=""{1}""", "attachment", fileName)
                        End If

                        Response.Clear()
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Disposition", strContentDisposition)
                        Response.TransmitFile(filePath)
                End Select

            Catch ex As Exception
                Response.Write("下載檔案發生意外錯誤!")
            End Try
        End If
        Response.End()
    End Sub
End Class
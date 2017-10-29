Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Imaging

Partial Class arch_res
    Inherits BasePage

    Dim rid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rid = cv(Request.QueryString("rid"))

        loadItem()
    End Sub

    Sub loadItem()

        Dim id As Integer
        Dim type As String = ""
        Try
            Dim tmp() As String = Crypto.Decrypt(rid).Split("|"c)
            id = Convert.ToInt32(tmp(0))
            type = tmp(1)
            inRange(type, {"s", "m", "b"})
        Catch ex As Exception
            Response.End()
        End Try

        Dim r As New AppSettingsReader
        Dim metaImagePath As String = r.GetValue("metaImagePath", GetType(String))
        'Dim filePath As String = r.GetValue("metaFilePath", GetType(String))
        Dim fileName As String = ""
        cmd.CommandText = "SELECT  f.*, a.identifier " & _
        "FROM     dbo.archimg AS f INNER JOIN " & _
        "               dbo.arch AS a ON f.aid = a.id " & _
        "WHERE  (f.id = @id) AND (a.p_ispublic = 2)"

        cmd.Parameters.Clear()
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
        dr = cmd.ExecuteReader
        If (dr.Read()) Then
            Dim identifier As String = getValue(dr("identifier"))
            fileName = String.Format("{0}{1}\{2}", metaImagePath, getIdentifier(identifier), getValue(dr("img_name_" & type)))
        End If
        dr.Close()

        If (Not String.IsNullOrEmpty(fileName)) Then
            Try
                Using img As New Bitmap(fileName)
                    'Dim watermarkImage As Image = Image.FromFile(Server.MapPath("~/images/logo_new1128.png"))
					Dim watermarkImage As Image = Image.FromFile(Server.MapPath("~/images/logo_new113001.png"))
                    Dim gra As Graphics = Graphics.FromImage(img)
                    'Dim imageAttributes As New ImageAttributes()
                    'Dim colorMatrixElements As Single()() = { _
                    'New Single() {1, 0, 0, 0, 0}, _
                    'New Single() {0, 1, 0, 0, 0}, _
                    'New Single() {0, 0, 1, 0, 0}, _
                    'New Single() {0, 0, 0, 0.5, 0}, _
                    'New Single() {0, 0, 0, 0, 1}}
                    'Dim colorMatrix As New ColorMatrix(colorMatrixElements)
                    'ImageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
                    'gra.DrawImage(watermarkImage, New Rectangle((img.Width - watermarkImage.Width) / 16, (img.Height - watermarkImage.Height) / 2, 7 * watermarkImage.Width, 7 * watermarkImage.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ImageAttributes)
                    'Dim width As Integer = watermarkImage.Width / 2
                    'Dim height As Integer = watermarkImage.Height / 2
                    'gra.DrawImage(watermarkImage, New Rectangle((img.Width - watermarkImage.Width) / 2, (img.Height - watermarkImage.Height), width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel)
                    gra.DrawImage(watermarkImage, New Rectangle((img.Width - watermarkImage.Width) / 64, (img.Height - watermarkImage.Height) * 3 / 8, 6 * watermarkImage.Width, 11 * watermarkImage.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel)
                    Using ms As New MemoryStream
                        img.Save(ms, ImageFormat.Jpeg)
                        ms.WriteTo(Response.OutputStream)
                    End Using
                End Using
            Catch ex As Exception
            End Try
        End If
        Response.End()
    End Sub
End Class
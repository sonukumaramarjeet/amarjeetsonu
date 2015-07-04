Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports System.Web.UI




Public Class FirstPage
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim ds As DataSet = New DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con = New SqlConnection("Persist Security Info=False;User ID=sa;Password=Smsc408;Initial Catalog=TestDB;Data Source=192.168.1.29")
        LoadData()
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Public Sub LoadData()
        Dim da As SqlDataAdapter = New SqlDataAdapter("Select * from Emp", con)
        da.Fill(ds, "Emp")
        gridempdata.DataSource = ds.Tables(0)
        gridempdata.DataBind()
    End Sub

    Protected Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        ExportExcel(ds)
    End Sub

    Protected Sub btnpdf_Click(sender As Object, e As EventArgs) Handles btnpdf.Click
        ExportPdf_gridview(gridempdata)
        ' ExportPdf(ds)
    End Sub

    Public Sub ExportExcel(ByVal ds As DataSet)
        Try
            Dim buildexcel As New StringBuilder()
            Dim filename As String
            filename = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-excel"
            buildexcel.Append("<table border='1'><tr><th>ENO</th><th>EName</th><th>Job</th><th>DOB</th><th>Last_Name</th></tr>")
            With ds.Tables(0)
                For i As Integer = 0 To .Rows.Count - 1
                    buildexcel.Append("<tr><td>" & .Rows(i)("ENO") & "</td>")
                    buildexcel.Append("<td>" & .Rows(i)("ENAME") & "</td>")
                    buildexcel.Append("<td>" & .Rows(i)("JOB") & "</td>")
                    buildexcel.Append("<td>" & .Rows(i)("DOB") & "</td>")
                    buildexcel.Append("<td>" & .Rows(i)("lname") & "</td></tr>")
                Next
            End With
            buildexcel.Append("</table>")
            Response.Write(buildexcel.ToString())
            Response.Flush()
            Response.End()
        Catch exp As ThreadAbortException
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub ExportPdf_gridview(ByVal Grid As GridView)
        Dim filename As String
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from employee", con)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim pdftable As PdfPTable = New PdfPTable(gridempdata.HeaderRow.Cells.Count)
            For Each headercell As TableCell In gridempdata.HeaderRow.Cells
                Dim font As New Font
                font.Color = Color.BLUE
                font.Size = 9
                Dim pdfcell As PdfPCell = New PdfPCell(New Phrase(headercell.Text, font))
                pdfcell.BackgroundColor = Color.PINK
                pdftable.AddCell(pdfcell)
            Next

            For Each GridViewRow As GridViewRow In gridempdata.Rows
                For Each TableCell As TableCell In GridViewRow.Cells
                    Dim font As New Font()
                    font.Color = Color.BLACK
                    font.Size = 8
                    Dim pdfcell As PdfPCell = New PdfPCell(New Phrase(TableCell.Text, font))
                    'pdfcell.BackgroundColor = New BaseColor(GridView1.RowStyle.BackColor)
                    pdftable.AddCell(pdfcell)
                Next
            Next
            Dim pdfdocument As Document = New Document(PageSize.A4, 5.0F, 5.0F, 5.0F, 5.0F)
            Dim abc As Document = New Document
            PdfWriter.GetInstance(pdfdocument, Response.OutputStream)
            pdfdocument.Open()
            pdfdocument.Add(pdftable)
            pdfdocument.Close()
            Response.ContentType = "application/pdf"
            filename = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
            Response.AppendHeader("content-disposition", "attachment;filename=" & filename & ".pdf")
            Response.Write(pdfdocument)
            Response.Flush()
            Response.End()
        End If
    End Sub

    Public Sub ExportPdf(ByVal ds As DataSet)
        Dim filename As String
        If ds.Tables(0).Rows.Count > 0 Then
            Dim pdftable As PdfPTable = New PdfPTable(ds.Tables(0).Columns.Count)
            For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                Dim font As New Font
                font.Color = Color.BLUE
                font.Size = 9
                Dim pdfcell As PdfPCell = New PdfPCell(New Phrase(ds.Tables(0).Columns(i).ColumnName, font))
                pdfcell.BackgroundColor = Color.PINK
                pdftable.AddCell(pdfcell)
            Next
            For r As Integer = 0 To ds.Tables(0).Rows.Count - 1
                For c As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    Dim font As New Font()
                    font.Color = Color.BLACK
                    font.Size = 8
                    Dim pdfcell As PdfPCell = New PdfPCell(New Phrase(ds.Tables(0).Rows(r)(c), font))
                    pdftable.AddCell(pdfcell)
                Next
            Next
            Dim pdfdocument As Document = New Document(PageSize.A4, 5.0F, 5.0F, 5.0F, 5.0F)
            Dim abc As Document = New Document
            PdfWriter.GetInstance(pdfdocument, Response.OutputStream)
            pdfdocument.Open()
            pdfdocument.Add(pdftable)
            pdfdocument.Close()
            Response.ContentType = "application/pdf"
            filename = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
            Response.AppendHeader("content-disposition", "attachment;filename=" & filename & ".pdf")
            Response.Write(pdfdocument)
            Response.Flush()
            Response.End()
        End If

    End Sub

    Public Sub ExportWord(ByVal ds As DataSet)
        Try
            Dim buildword As New StringBuilder()
            Dim filename As String
            filename = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".doc")
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/vnd.ms-word "
            buildword.Append("<table><tr><th>ENO</th><th>EName</th><th>Job</th><th>DOB</th><th>Last_Name</th></tr>")
            With ds.Tables(0)
                For i As Integer = 0 To .Rows.Count - 1
                    buildword.Append("<tr><td>" & .Rows(i)("ENO") & "</td>")
                    buildword.Append("<td>" & .Rows(i)("ENAME") & "</td>")
                    buildword.Append("<td>" & .Rows(i)("JOB") & "</td>")
                    buildword.Append("<td>" & .Rows(i)("DOB") & "</td>")
                    buildword.Append("<td>" & .Rows(i)("lname") & "</td></tr>")
                Next
            End With
            buildword.Append("</table>")
            Response.Write(buildword.ToString())
            Response.Flush()
            Response.End()
        Catch exp As ThreadAbortException
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub ExportWord_gridview(ByVal GridView1 As GridView)
        Try
            Dim filename As String = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".doc")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-word "
            Dim sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            GridView1.AllowPaging = False
            GridView1.DataBind()
            GridView1.RenderControl(hw)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()
        Catch exp As ThreadAbortException
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub ExportText(ByVal ds As DataSet)
        Try
            Dim filename As String
            Dim content As New StringBuilder()
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Columns.Count - 1
                    content.Append(ds.Tables(0).Columns(i).ColumnName.ToString() & vbTab)
                Next
                content.Append(vbCr & vbLf)
                For r As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    For c As Integer = 0 To ds.Tables(0).Columns.Count - 1
                        content.Append(ds.Tables(0).Rows(r)(c).ToString() + vbTab)
                    Next
                    content.Append(vbCr & vbLf)
                Next
                filename = DateTime.Now.ToString("yyyy-MM-dd-hh:mm")
                Response.ClearContent()
                Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".txt")
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = "application/text"
                Dim stringWrite As New StringWriter()
                Dim htmlWrite As New HtmlTextWriter(stringWrite)
                Response.Write(content.ToString())
                Response.End()
            End If
        Catch exc As ThreadAbortException
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub ExportAsText()
        Dim filename As String = DateTime.Now.ToString()
        Dim Str As String = String.Empty
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".txt")
        Response.ContentType = "application/text"
        Response.Charset = ""
        For i As Integer = 0 To gridempdata.Columns.Count - 1
            Str += gridempdata.Columns(i).HeaderText.ToString() + vbTab
        Next
        Str += vbCr & vbLf
        For j As Integer = 0 To gridempdata.Rows.Count - 1
            For k As Integer = 0 To gridempdata.Columns.Count - 1
                Str += gridempdata.Rows(j).Cells(k).Text + vbTab
            Next
            Str += vbCr & vbLf
        Next
        Response.Write(Str.ToString())
        Response.End()
    End Sub

    Protected Sub btnword_Click(sender As Object, e As EventArgs) Handles btnword.Click
        'ExportWord(ds)
        ExportWord_gridview(gridempdata)
    End Sub

    Protected Sub btntext_Click(sender As Object, e As EventArgs) Handles btntext.Click
        ExportText(ds)
        'ExportAsText()
    End Sub
End Class
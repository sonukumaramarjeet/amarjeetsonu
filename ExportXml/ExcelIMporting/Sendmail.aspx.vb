﻿Imports System.Drawing
Imports System.Net
Imports System.Net.Mail

Public Class Sendmail
    Inherits System.Web.UI.Page
    Dim mmsg As MailMessage
    Shared count As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnsend_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnsend.Click

        Try
            Dim smtpobj As New SmtpClient()
            smtpobj.Credentials = New NetworkCredential(txtfrom.Text.Trim(), txtpwd.Text.Trim())
            smtpobj.Port = 587 '52
            smtpobj.Host = "smtp.gmail.com"
            smtpobj.EnableSsl = True

            mmsg = New MailMessage()
            mmsg.From = New MailAddress(txtfrom.Text.Trim(), "Sonu's Mail", Encoding.UTF8)
            mmsg.To.Add(txtto.Text.Trim())
            mmsg.Subject = txtsubject.Text.Trim()
            mmsg.Body = txtmsg.Text.Trim()
            If count <> 0 Then
                mmsg.Attachments.Add(New Attachment(MapPath(lblfile1.Text)))
            End If
            mmsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            smtpobj.Send(mmsg)
            lblstatus.Text = "Mail Send Sussfully"
            lblstatus.ForeColor = Color.Green
            txtfrom.Text = String.Empty
            txtpwd.Text = String.Empty
            txtto.Text = String.Empty
            txtsubject.Text = String.Empty
            txtmsg.Text = String.Empty
        Catch ex As Exception
            lblstatus.Text = "Mail Sending Failed!!!!"
            lblstatus.ForeColor = Color.Red
            txtfrom.Text = String.Empty
            txtpwd.Text = String.Empty
            txtto.Text = String.Empty
            txtsubject.Text = String.Empty
            txtmsg.Text = String.Empty
        End Try
    End Sub

    Protected Sub lbattach_Click(sender As Object, e As EventArgs) Handles lbattach.Click
        count += 1
        fileupload1.PostedFile.SaveAs(MapPath(fileupload1.FileName))
        lblfile1.Text = fileupload1.FileName
        lblfile1.ForeColor = Color.Green
        lblfile2.Text = " is uploaded"
        lblfile2.ForeColor = Color.Green
    End Sub
End Class
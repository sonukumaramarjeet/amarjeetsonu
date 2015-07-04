<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sendmail.aspx.vb" Inherits="ExcelIMporting.Sendmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table>
    <tr>
     <th colspan="3">Sending Mail From Your Mail</th>
    </tr>
    <tr>
     <th colspan="3">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"/>
     </th>
    </tr>
     <tr>
     <th>From</th>
     <th>:</th>
      <td align="left"><asp:TextBox runat="server" ID="txtfrom" placeholder="E-Mail ID"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfrom" Display="Dynamic" ErrorMessage="From is Required" style="color: #FF0000">*</asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfrom" ErrorMessage="E-ID not in correct Format" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
         </td>
     </tr>
     <tr>
     <th>Password </th>
     <th>:</th>
      <td align="left"><asp:TextBox runat="server" ID="txtpwd" TextMode="Password" placeholder="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpwd" Display="Dynamic" ErrorMessage="PWd is Required" style="color: #FF0000">*</asp:RequiredFieldValidator>
         </td>
     </tr>
     <tr>
     <th>To </th>
     <th>:</th>
      <td align="left"><asp:TextBox runat="server" ID="txtto" placeholder="E-Mail ID"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtto" Display="Dynamic" ErrorMessage="To is Required" style="color: #FF0000">*</asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtto" ErrorMessage="E-Id not in Correct Format" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
         </td>
     </tr>
    <tr>
     <th>Subject </th>
     <th>:</th>
      <td align="left"><asp:TextBox runat="server" ID="txtsubject" placeholder="Subject"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsubject" Display="Dynamic" ErrorMessage="Subject is Required" style="color: #FF0000">*</asp:RequiredFieldValidator>
        </td>
     </tr>
    <tr>
     <th>Attachments</th>
     <th>:</th>
      <td align="left"><asp:FileUpload runat="server" ID="fileupload1" /><asp:LinkButton ID="lbattach" runat="server" Text="Attach" OnClick="lbattach_Click"></asp:LinkButton><br />
      </td>
     </tr>
     <tr>
     <td></td>
     <td></td>
      <td>
       <asp:Label runat="server" ID="lblfile1"></asp:Label>
       <asp:Label runat="server" ID="lblfile2"></asp:Label>
      </td>
     </tr>
     <tr>
     <th valign="top">Message </th>
     <th></th>
      <td><asp:TextBox runat="server" ID="txtmsg" TextMode="MultiLine" Height="92px" Width="231px" placeholder="Message....."></asp:TextBox></td>
     </tr>
    <tr>
     <th><asp:ImageButton ID="btnsend" runat="server"  ImageUrl="~/send.png" Height="39px" Width="128px" OnClick="btnsend_Click"/></th>
     <th colspan="2" valign="top"><asp:Label ID="lblstatus" runat="server"></asp:Label></th>
     </tr>
    </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FirstPage.aspx.vb" Inherits="ExcelIMporting.FirstPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView runat="server" ID="gridempdata" AutoGenerateColumns="false" >
    <Columns>
    <asp:BoundField DataField="ENO" HeaderText="Emp NO" />
    <asp:BoundField DataField="ENAME" HeaderText="Emp Name" />
    <asp:BoundField DataField="lname" HeaderText="Last Name" />
    <asp:BoundField DataField="JOB" HeaderText="Job" />
    <asp:BoundField DataField="DOB" HeaderText="Dob" />
    </Columns>
    </asp:GridView>
    <br />
    <asp:Button runat="server" ID="btnexport" Text="Export As Excel" /> &nbsp;<asp:Button runat="server" ID="btnpdf" Text="Export As PDF" />
    &nbsp;<asp:Button runat="server" ID="btnword" Text="Export As Word" />&nbsp;<asp:Button runat="server" ID="btntext" Text="Export As Text" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" Theme="EmpSkin" CodeBehind="frmManageLocation.aspx.cs" Inherits="EmpMgmtSys.frmManageLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Location Management</title>
    <link href="Script/ManageLoc.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="containerdiv" id="maindiv" style="">
    <div class="headdiv" id="headingdiv"><b>Manage Location</b></div>
    <asp:Label runat="server" ID="lblmsg"></asp:Label>
    <br />
    <label>Location Name : </label><asp:TextBox runat="server" ValidationGroup="search" AutoPostBack="true" 
            ID="txtLocationName" ontextchanged="txtLocationName_TextChanged"></asp:TextBox>&nbsp;
    <asp:Button runat="server" ID="btnSearch" Text="Search" class="tbnAddNew design" ValidationGroup="search" onclick="btnSearch_Click" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocationName"
           ForeColor="Red" ValidationGroup="search"  ErrorMessage="Enter Location Name"></asp:RequiredFieldValidator>

    <br /><br />

    <asp:Button runat="server" ID="btnSelectAlltop" SkinID="selectAll" Text="SelectAll" 
            onclick="btnSelectAlltop_Click" />&nbsp;
    <asp:Button runat="server" ID="btnDeselectAlltop" SkinID="DeselectAll" 
            Text="De-SelectAll" onclick="btnDeselectAlltop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" ID="btnDeletetop" SkinID="Delete" class="btnDel design" 
            Text="Delete" onclick="btnDeletetop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" ID="btnAddNewtop" SkinID="AddNew" Text="Add New" 
            onclick="btnAddNewtop_Click" /><br />

    <asp:DataList runat="server" Width="42%" ID="datalistManageLocation" RepeatColumns="1" ShowFooter="true">

    <AlternatingItemStyle BackColor="LightGray" />
    <HeaderStyle BackColor="#1975FF" />
    <FooterStyle BackColor="#1975FF" />
    <HeaderTemplate>
    <table width="100%" class="bodytable">
    <tr>
    <td class="tbl-tdhead-css"></td>
    <td class="tbl-tdtrunk-css"><span class="mainhead">Location Name</span></b></td>
    <td class="tbl-tdtrunk-css"><span class="mainhead">Is Active</span></td>
    </tr>
    </table>
    </HeaderTemplate>
    <ItemTemplate>
    <table width="100%" class="bodytable">
    <tr>
    <td class="tbl-tdhead-css">
    <asp:CheckBox runat="server" data-eid='<%# Eval("LocationId") %>' ID="chkbox" />&nbsp;
    <asp:LinkButton runat="server" locid='<%# Eval("LocationId") %>' ID="lnkbtnedit" Text="Edit"></asp:LinkButton></td>
    <td class="tbl-tdtrunk-css" ><%# Eval("LocationName") %></td>
    <td class="tbl-tdtrunk-css"><asp:CheckBox runat="server" ID="chkboxisactive" locid='<%# Eval("LocationId") %>' Checked='<%# Eval("IsActive") %>' /></td>
    </tr>
    </table>
    </ItemTemplate>
    <FooterTemplate>
    <asp:Label Visible='<%#bool.Parse((datalistManageLocation.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Record Found!"></asp:Label>
    <span class="mainhead" >Total Record: <%= datalistManageLocation.Items.Count%></span>
    </FooterTemplate>
    </asp:DataList>
    <asp:Button runat="server" SkinID="selectAll" ID="btnSelectAllbot" Text="SelectAll" 
            onclick="btnSelectAlltop_Click" />&nbsp;
    <asp:Button runat="server" SkinID="DeselectAll" ID="btnDeselectAllbot" 
            Text="De-SelectAll" onclick="btnSelectAlltop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" SkinID="Delete" ID="btnDeletebot" Text="Delete" 
            onclick="btnDeletetop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" SkinID="AddNew" ID="btnAddNewbot" Text="Add New" 
            onclick="btnAddNewtop_Click" /><br />
    </div>
    <br />


    <div class="containerdiv" runat="server" id="divAddlocation">
    <div class="headdiv" id="divheading"><b>Add Location</b></div><br />
    <span style="color:Red">*</span> Marks are Mandatory
        <br />
    <table>
    <tr>
    <th>Locaion Name&nbsp;<span style="color:Red">*</span></th>
    <td>:&nbsp;<asp:TextBox ValidationGroup="grp" runat="server" ID="txtLocation"></asp:TextBox></td>
    </tr>
    <tr>
    <th>Is Active</th>
    <td>:&nbsp;<asp:checkBox runat="server" ID="chkboxActive" /></td>
    </tr>
    <tr>
    <th></th>
    <td><asp:Button runat="server" ValidationGroup="grp" Text="Insert" SkinID="Delete" ID="btnInsert" 
            onclick="btnInsert_Click" />&nbsp;<asp:Button  SkinID="Delete" 
            runat="server" Text="Cancel" ID="Cancel" onclick="Cancel_Click" /></td>
    </tr>
    </table>
    <asp:RequiredFieldValidator ValidationGroup="grp" ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtLocation"
            ErrorMessage="Location is required" ForeColor="Red"></asp:RequiredFieldValidator>
    </div>
    </form>
</body>
</html>

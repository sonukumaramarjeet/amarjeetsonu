<%@ Page Language="C#" AutoEventWireup="true" Theme="EmpSkin" CodeBehind="frmManageDepartment.aspx.cs" Inherits="EmpMgmtSys.frmManageDepartment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DepartMent</title>
    <link href="Script/ManageLoc.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="containerdiv" id="maindiv" style="">
    <div class="headdiv" id="headingdiv"><b>Manage Department</b></div>
    <asp:Label runat="server" ID="lblmsg"></asp:Label>
    <br />
    <label>Department Name : </label>
    <asp:TextBox runat="server" ValidationGroup="search" 
    ID="txtLocationName"></asp:TextBox>&nbsp;&nbsp;
    <label>Location Name</label>
    <asp:DropDownList runat="server" ID="ddllocaion"></asp:DropDownList>
    <asp:Button runat="server" ID="btnSearch" Text="Search" class="tbnAddNew design" ValidationGroup="search" onclick="btnSearch_Click" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtLocationName"
           ForeColor="Red" ValidationGroup="search"  ErrorMessage="Enter Location Name"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" 
            runat="server" ControlToValidate="ddllocaion"
           ForeColor="Red" ValidationGroup="search"  
            ErrorMessage="Enter Location Name" InitialValue="0"></asp:RequiredFieldValidator>

    <br /><br />

    <asp:Button runat="server" ID="btnSelectAlltop" SkinID="selectAll" Text="SelectAll" 
            onclick="btnSelectAlltop_Click" />&nbsp;
    <asp:Button runat="server" ID="btnDeselectAlltop" SkinID="DeselectAll" 
            Text="De-SelectAll" onclick="btnDeselectAlltop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" ID="btnDeletetop" SkinID="Delete" class="btnDel design" 
            Text="Delete" onclick="btnDeletetop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" ID="btnAddNewtop" SkinID="AddNew" Text="Add New" 
            onclick="btnAddNewtop_Click" /><br />

    <!--Write the GridViewCode here -->
    <asp:GridView runat="server" ID="Gvdepartment" AutoGenerateColumns="false"
     AllowPaging="true" AllowSorting="true" DataKeyNames="Deptid"
            onpageindexchanging="Gvdepartment_PageIndexChanging" 
            onrowcancelingedit="Gvdepartment_RowCancelingEdit" onrowdeleting="Gvdepartment_RowDeleting" 
            onrowupdating="Gvdepartment_RowUpdating" 
            onselectedindexchanging="Gvdepartment_SelectedIndexChanging" 
            onrowdatabound="Gvdepartment_RowDataBound" 
            onrowediting="Gvdepartment_RowEditing"
            EmptyDataText="No record Available !!!"
            HeaderStyle-BackColor="#507CD1"
            HeaderStyle-ForeColor="White"
            HeaderStyle-Height="30px"
            onsorting="Gvdepartment_Sorting" >
     <Columns>
     
     <asp:TemplateField>
     <ItemTemplate>
     <asp:CheckBox runat="server" data-did='<%# Eval("Deptid") %>' ID="cbselect" />
     </ItemTemplate>
     </asp:TemplateField>

     <asp:TemplateField>
     <ItemTemplate><asp:Button runat="server" ID="btnedit" SkinID="Edit" Text="Edit" deptid='<%# Eval("Deptid") %>' CommandName="Edit" /></ItemTemplate>
     <EditItemTemplate><asp:Button runat="server" ID="btnupdate" SkinID="Edit" Text="Update" CommandName="update" ></asp:Button>&nbsp;
     <asp:Button runat="server" SkinID="Edit" ID="btncancel" CommandName="Cancel" Text="Cancel" />
      </EditItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Dept Name" SortExpression="DeptName" >
     <ItemTemplate><%# Eval("DeptName") %></ItemTemplate>
     <EditItemTemplate><asp:TextBox runat="server" ID="txtdeptname" Text='<%# Eval("DeptName") %>'></asp:TextBox></EditItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Dept Description" SortExpression="DeptDesc">
     <ItemTemplate><%# Eval("DeptDesc") %></ItemTemplate>
     <EditItemTemplate><asp:TextBox runat="server" ID="txtdeptdesc" TextMode="MultiLine" Text='<%# Eval("DeptDesc") %>'></asp:TextBox></EditItemTemplate>
     </asp:TemplateField>

     <asp:TemplateField HeaderText="Dept Location" SortExpression="Locname">
     <ItemTemplate><%# Eval("Locname")%></ItemTemplate>
     <EditItemTemplate> <asp:DropDownList runat="server" ID="ddlloc"></asp:DropDownList>
        <asp:Label runat="server" ID="lbllocid" Text='<%# Eval("LocationId") %>' style="display:none"></asp:Label>
     </EditItemTemplate>
     </asp:TemplateField>

     <asp:TemplateField HeaderText="Is Active">
     <ItemTemplate><asp:CheckBox runat="server" ID="cbisactive"  Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
     <EditItemTemplate><asp:CheckBox runat="server" ID="cbisactive"  Checked='<%# Eval("IsActive") %>' /></EditItemTemplate>
     </asp:TemplateField>
     </Columns>
    </asp:GridView>
    <asp:Button runat="server" SkinID="selectAll" ID="btnSelectAllbot" Text="SelectAll" 
            onclick="btnSelectAlltop_Click" />&nbsp;
    <asp:Button runat="server" SkinID="DeselectAll" ID="btnDeselectAllbot" 
            Text="De-SelectAll" onclick="btnDeselectAlltop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" SkinID="Delete" ID="btnDeletebot" Text="Delete" 
            onclick="btnDeletetop_Click" />&nbsp;&nbsp;
    <asp:Button runat="server" SkinID="AddNew" ID="btnAddNewbot" Text="Add New" 
            onclick="btnAddNewtop_Click" /><br />
    </div>
    <br />

    <div class="containerdiv" runat="server" id="divAddDept">
    <div class="headdiv" id="divheading"><b>Add Department</b></div><br />
    <span style="color:Red">*</span> Marks are Mandatory
        <br />
    <table>
    <tr>
    <th>Department Name <span style="color:Red">*</span>&nbsp;</th>
    <td>:&nbsp;<asp:TextBox ValidationGroup="grp" runat="server" ID="txtdeptname"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="validate1" ControlToValidate="txtdeptname" ValidationGroup="grp"
     ErrorMessage="Department is Required" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <th>Description &nbsp;<span style="color:Red">*</span>&nbsp; </th>
    <td>:&nbsp;<asp:TextBox runat="server" ID="txtdesc" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="validate2" ControlToValidate="txtdesc" ValidationGroup="grp"
     ErrorMessage="Description is Required" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>

    </tr>
    <tr>
    <th>Location <span style="color:Red">*</span>&nbsp;&nbsp;</th>
    <td>:&nbsp;<asp:DropDownList runat="server" ID="ddladdloc"></asp:DropDownList>
    <asp:RequiredFieldValidator runat="server" ID="rv3" ControlToValidate="ddladdloc" ValidationGroup="grp"
     ErrorMessage="Select Location " InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
    </td>

    </tr>
    <tr>
    <th>Is Active</th>
    <td>:&nbsp;<asp:checkBox runat="server" ID="chkboxActive" /></td>
    </tr>
    <tr>
    <th></th>
    <td><asp:Button runat="server" ValidationGroup="grp" Text="Insert" SkinID="Delete" ID="btnInsert" onclick="btnInsert_Click" />
    &nbsp;<asp:Button  SkinID="Delete" runat="server" Text="Cancel" ID="Cancel" onclick="Cancel_Click" />
  </td>
    </tr>
    </table>
    
    </div>
    </form>
</body>
</html>

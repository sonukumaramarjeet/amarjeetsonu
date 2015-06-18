<%@ Page Language="C#" AutoEventWireup="true" Theme="EmpSkin" CodeBehind="frmManageEmployee.aspx.cs" Inherits="EmpMgmtSys.frmManageEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee</title>
    <link href="Script/ManageLoc.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div class="containerdiv" id="maindiv" style="">
   <div class="headdiv" id="headingdiv"><b>View Employee Details</b></div>
   <asp:Label runat="server" ID="lblmsg"></asp:Label>
   <div style=" text-align:right; width:100%">
   <br />
   <asp:Button runat="server" ID="btnaddemp" SkinID="AddEmp" Text="ADD New Employee Details " />
   <br />&nbsp;
   </div>
   <asp:GridView runat="server"
    ID="GvDepartment"
    DataKeyNames="Deptid"
    AutoGenerateColumns="false"
    AllowPaging="true"
    AllowSorting="true"
    EmptyDataText="No Record Found !!!"
    HeaderStyle-BackColor="#507CD1"
    HeaderStyle-ForeColor="White"
    HeaderStyle-Height="30px"
    Width="100%"
    PageSize="3"
    onrowdatabound="GvDepartment_RowDataBound" onsorting="GvDepartment_Sorting" 
    onpageindexchanging="GvDepartment_PageIndexChanging" 
    onselectedindexchanging="GvDepartment_SelectedIndexChanging">

   <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
   <Columns>
   <asp:TemplateField>
     <ItemTemplate>
     <asp:Button runat="server" ID="btnedit" SkinID="Edit" Text="Edit" deptid='<%# Eval("Deptid") %>' CommandName="Edit" />
     <asp:Button runat="server" ID="btndelete" SkinID="Edit" Text="Delete" CommandName="Delete" />
     </ItemTemplate>
     <EditItemTemplate><asp:Button runat="server" ID="btnupdate" SkinID="Edit" Text="Update" CommandName="update" ></asp:Button>&nbsp;
     <asp:Button runat="server" SkinID="Edit" ID="btncancel" CommandName="Cancel" Text="Cancel" />
      </EditItemTemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Dept Name" SortExpression="DeptName">
    <ItemTemplate >
    <%# Eval("DeptName")%>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Description" SortExpression="DeptDesc">
    <ItemTemplate >
    <%# Eval("DeptDesc")%>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Dept Location" SortExpression="Locname">
    <ItemTemplate >
    <%# Eval("Locname")%>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Is Active" SortExpression="IsActive">
    <ItemTemplate >
    <asp:CheckBox runat="server" ID="Cbisactive" Checked='<%# Eval("IsActive") %>' />
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Chield Rows">
    <ItemTemplate>

    <asp:GridView runat="server"
    ID="GvEmployee"
    Datakey="EmpId"
    AutoGenerateColumns="false"
    EmptyDataText="No Employee Available in this department "
    AllowPaging="true"
    HeaderStyle-BackColor="DarkGreen"
    HeaderStyle-ForeColor="White"
    HeaderStyle-Height="30px"
    AllowSorting="true"
    Width="100%"
    PageSize="5"
    onrowdatabound="GvEmployee_RowDataBound"
    >
    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
    <Columns>
    <asp:TemplateField >
    <ItemTemplate>
    <asp:LinkButton runat="server" ID="lnkbtnedit" Text="Edit" Empid='<%# Eval("EmpId") %>'  CommandName="Edit"></asp:LinkButton>
    <asp:LinkButton runat="server" ID="lnkbtndelete" Text="Delete" CommandName="Delete"></asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>


    <asp:TemplateField HeaderText="Employee Name" SortExpression="EmpName">
    <ItemTemplate>
    <%# Eval("EmpName") %>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Emp Job" SortExpression="empJob">
    <ItemTemplate>
    <%# Eval("EmpJob") %>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Emp Salary" SortExpression="EmpSal">
    <ItemTemplate>
    <asp:Label ID="empsal" Text='<%# Eval("EmpSal") %>' runat="server" ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Emp Join Date" SortExpression="EmpJoinDate">
    <ItemTemplate>
    <%# Eval("EmpJoinDate") %>
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblSummery" Width="100%"></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
   </Columns>
   </asp:GridView>
   <div style="width:100%; background:#D1EBAE; text-align:left;">
   <asp:Label runat="server" ID="lblfooter"></asp:Label>
   </div>
   </div>
   </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentDetails.aspx.cs" Inherits="StudentDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insert Student Details</title>
    <script src="ScriptFiles/jquery-2.1.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        var count = 2;
        function AddNewRow() {
            var content = "<tr>";
            content += "<td><input type='text' id='txtroll_" + count + "' class='roll'/>";
            content += "<td><input type='text' id='fname_" + count + "' class='fname'/>";
            content += "<td><input type='text' id='lname_" + count + "' class='lname'/>";
            content += "<td><input type='text' id='mobile_" + count + "' class='mobile'/>";
            content += "</tr>";
            $("#studenttable tbody").append(content);
            count++;
        }


        function DeleteRow() {
            var row = $("#studenttable tr").length;
          document.getElementById("studenttable").deleteRow(row-2);
      }

      $(document).ready(function () {

          $("#btnsave").click(function () {
              var txt = '';
              var rcount = $("#studenttable tr").length - 1;
              for (var i = 2; i <= rcount; i++) {
                  txt = txt + '{"roll":"' + $('#txtroll_' + i).val() + '","fname":"' + $('#fname_' + i).val() + '","lname":"' + $('#lname_' + i).val() + '","mobile":"' + $('#mobile_' + i).val() + '"},';
              }
              txt = txt.substring(0, txt.lastIndexOf(','));
              txt = '{"data":[' + txt + " ]}";
              $.ajax({
                  url: "student_ajax.aspx",
                  type: "post",
                  async: false,
                  data: { type: 1, content: txt },
                  success: function (res) {
                      alert(res);
                      return false;
                  },
                  error: function (res) {
                      alert("Error section");
                  }
              });
          });

      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="studentdetails">
    <table id="studenttable" border="1">
    <thead><tr>
     <th>Roll No.</th>
     <th>First Name</th>
     <th>Last Name</th>
     <th>Mobile No.</th>
    </tr>
    </thead>
    <tbody id="tblbody">
    </tbody>
    <tfoot>
    <tr>
    <td colspan="4">
    <input type="button" id="btnadd" value="Add New Row" onclick='return AddNewRow();' /> 
    <input type="button" id="btndelete" value="Delete Row" onclick='return DeleteRow();' /> 
    </td>
    </tr>
    </tfoot>
    </table>
    </div>
    <div>
    <input type="button" value="Save Record" id="btnsave" />
    </div>
    </form>
</body>
</html>

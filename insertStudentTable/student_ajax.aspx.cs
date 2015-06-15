using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class student_ajax : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string req = Request["type"].ToString();
        switch (req)
        {
            case "1":
                saveData();
                break;
        }
    }

    public void saveData()
    {
        string studata = Request["content"];
        JObject sobj = JObject.Parse(studata);
        JArray sarr = sobj.SelectToken("data") as JArray;
        structstudent[] sdataarr = new structstudent[sarr.Count - 1];
        DataTable sdt = new DataTable();
        DataRow dr;
        sdt.Columns.Add(new DataColumn("srno", typeof(Int32)));
        sdt.Columns.Add(new DataColumn("roll", typeof(Int32)));
        sdt.Columns.Add(new DataColumn("fname", typeof(string)));
        sdt.Columns.Add(new DataColumn("lname", typeof(string)));
        sdt.Columns.Add(new DataColumn("mobile", typeof(string)));

        for (int i = 0; i < sarr.Count; i++)
        {
            dr = sdt.NewRow();
            dr[0] = i + 1;
            dr[1] = Convert.ToInt32(sarr[i]["roll"].ToString());
            dr[2] = sarr[i]["fname"].ToString();
            dr[3] = sarr[i]["lname"].ToString();
            dr[4] = sarr[i]["mobile"].ToString();
            sdt.Rows.Add(dr);
        }
        cmd = new SqlCommand("insert_sp",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@content",SqlDbType.Structured).Value= sdt;
        cmd.Parameters.Add("@retval", SqlDbType.Int).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@retmsg", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        int rval = Convert.ToInt32(cmd.Parameters["@retval"].Value);
        string msg= cmd.Parameters["@retmsg"].Value.ToString();
        //string str="{""message"":""success""}";

        if (rval > 0)
            Response.Write("Success");
        else
            Response.Write(msg.ToString());
    }


    public struct structstudent
    {
       public int roll;
       public string fname;
       public string lname;
       public string mobile;
    }
}
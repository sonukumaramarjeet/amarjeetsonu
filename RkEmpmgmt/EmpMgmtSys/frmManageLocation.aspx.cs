using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace EmpMgmtSys
{
    public partial class frmManageLocation : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Empconnection"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public DataSet ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                divAddlocation.Visible = false;
                LoadData();
            }
            
        }

        public void LoadData()
        {
            try
            {
               
                cmd = new SqlCommand();
                cmd.CommandText="sp_GetAllLocaion";
                cmd.Connection= con;
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@retval", 0);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds,"Loc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    datalistManageLocation.DataSource = ds.Tables[0];
                    datalistManageLocation.DataBind();
                }
                else
                {
                    btnSelectAllbot.Visible = false;
                    btnDeselectAlltop.Visible = false;
                    btnDeletetop.Visible = false;
                    btnSelectAllbot.Visible = false;
                    btnDeselectAllbot.Visible = false;
                    btnDeletebot.Visible = false;
                }
              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search_Loacaiton(txtLocationName.Text.Trim());
        }

        protected void txtLocationName_TextChanged(object sender, EventArgs e)
        {

            if (txtLocationName.Text.Trim().Length > 1)
                Search_Loacaiton(txtLocationName.Text.Trim());
            else
            {
                LoadData();
            }
        }

        void Search_Loacaiton(string locname)
        {
            int retval = 0;
            string retmsg = string.Empty;
            
            cmd = new SqlCommand("SearchLocation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@locname", locname);
            cmd.Parameters.AddWithValue("@retval", retval).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@retmsg", retmsg).Direction = ParameterDirection.Output;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            ds.Clear();
            da.Fill(ds,"emp");
            retval = (int)cmd.Parameters["@retval"].Value;
            retmsg = (string)cmd.Parameters["@retmsg"].Value;
            if (retval == 1)
            {
                if (ds.Tables["emp"].Rows.Count > 0)
                {
                    datalistManageLocation.DataSource = ds.Tables[0];
                    datalistManageLocation.DataBind();
                }
                else
                {
                    lblmsg.Text = "NO Matching Location Found";
                    lblmsg.ForeColor = Color.Red;
                }
            }
            else
            {
                lblmsg.Text = retmsg;
                lblmsg.ForeColor = Color.Red;
            }
           
        }

        protected void btnSelectAlltop_Click(object sender, EventArgs e)
        {
            SelectAllCheckedBox();
        }

        void SelectAllCheckedBox()
        {
            foreach (DataListItem li in datalistManageLocation.Items)
            {
                CheckBox cb = li.FindControl("chkbox") as CheckBox;
                if (cb != null)
                    if (!cb.Checked)
                        cb.Checked = true;     
            }
        }

        protected void btnDeselectAlltop_Click(object sender, EventArgs e)
        {
            DeselectAllCheckedBox();
        }

        void DeselectAllCheckedBox()
        {
            foreach (DataListItem li in datalistManageLocation.Items)
            {
                CheckBox cb = li.FindControl("chkbox") as CheckBox;
                if (cb != null)
                    if (cb.Checked)
                        cb.Checked = false;
            }
        }

        protected void btnDeletetop_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        void DeleteSelected()
        {
            string locid=string.Empty;
            int count = 0;
            foreach (DataListItem li in datalistManageLocation.Items)
            {
                CheckBox cb = li.FindControl("chkbox") as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                     count++;
                     locid+=cb.Attributes["data-eid"]+",";
                     cb.Checked = false;
                    }
                }
            }
            if (count != 0)
            {
                locid = locid.Substring(0, locid.LastIndexOf(","));
                //Response.Write("<script>alert('Location ID :" + locid + "')</script>");
                DelecteData(locid);
            }
            else
            {
                Response.Write("<script>alert('Select record to delect')</script>");
                return;
            }
        }

        void DelecteData(string locid )
        {
            int retval = 0;
            string retmsg = string.Empty;
            cmd = new SqlCommand("DeleteLocation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@loc_id", locid);
            cmd.Parameters.AddWithValue("@retval", retval).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@retmsg", retmsg).Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            retval = (int)cmd.Parameters["@retval"].Value;
            if (retval == 1)
            {
                lblmsg.Text = "Deletion is Successfull !!!!";
                lblmsg.ForeColor = Color.Green;
                LoadData();
            }
            else
            {
                lblmsg.Text = retmsg;
                lblmsg.ForeColor = Color.Red;
            }
            con.Close();
        }



        protected void btnAddNewtop_Click(object sender, EventArgs e)
        {
            divAddlocation.Visible = true;
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            txtLocation.Text = "";
            chkboxActive.Checked = false;
            divAddlocation.Visible = false;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string locname = string.Empty;
            bool isactive = false;
            if (txtLocation.Text.Trim().Length > 0)
            {
                locname = txtLocation.Text.Trim();
                if (chkboxActive.Checked)
                    isactive = true;
                InsertLocation(locname,isactive);
                LoadData();
             txtLocation.Text = "";
             chkboxActive.Checked = false;
            }
        }

        void InsertLocation(string locname, bool isactive)
        {
            int retval=3;
            string retmsg=string.Empty;
            cmd = new SqlCommand("sp_InsertLocation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@loc_name",locname);
            cmd.Parameters.AddWithValue("@isactive",isactive);
            cmd.Parameters.AddWithValue("@retval",retval).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@retmsg", retmsg).Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            retval = (int) cmd.Parameters["@retval"].Value;
            if (retval == 1)
            {
                lblmsg.Text = "Loacation Added successfully";
                lblmsg.ForeColor = Color.Green;
                LoadData();
            }
            else
            {
                lblmsg.Text = retmsg;
                lblmsg.ForeColor = Color.Red;
            }
            con.Close();
        }

       

    }
}
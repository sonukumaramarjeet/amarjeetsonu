using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;

namespace EmpMgmtSys
{
    public partial class frmManageDepartment : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Empconnection"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public DataSet ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                divAddDept.Visible = false;
                LoadAllDept();
                LoadLocation(1);
            }
        }

        public void LoadAllDept()
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "GetAllDept";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@retval", 0);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "Dept");

               int retval = (int)cmd.Parameters["@retval"].Value;
               string retmsg = (string)cmd.Parameters["@retmsg"].Value;

               if (retval == 1)
               {
                   if (ds.Tables[0].Rows.Count > 0)
                   {
                       Gvdepartment.DataSource = ds.Tables[0];
                       Gvdepartment.DataBind();
                   }
                   else
                   {
                       btnSelectAlltop.Visible = false;
                       btnDeselectAlltop.Visible = false;
                       btnDeletetop.Visible = false;

                       btnSelectAllbot.Visible = false;
                       btnDeselectAllbot.Visible = false;
                       btnDeletebot.Visible = false;

                   }
               }
               else
               {
                   lblmsg.Text = retmsg;
                   lblmsg.ForeColor = Color.Red;
               }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void LoadLocation(int type)
        {
            try
            {

                cmd = new SqlCommand();
                cmd.CommandText = "sp_GetAllLocaion";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@retval", 0);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "Loc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (type == 1)
                    {
                        ddllocaion.DataSource = ds.Tables[0];
                        ddllocaion.DataTextField = "LocationName";
                        ddllocaion.DataValueField = "LocationId";
                        ddllocaion.DataBind();
                        ddllocaion.Items.Insert(0, new ListItem("Select Department", "0"));
                    }
                    else if (type == 2)
                    {
                        ddladdloc.DataSource = ds.Tables[0];
                        ddladdloc.DataTextField = "LocationName";
                        ddladdloc.DataValueField = "LocationId";
                        ddladdloc.DataBind();
                        ddladdloc.Items.Insert(0, new ListItem("Select Department", "0"));
                    }
                   
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = Color.Red;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            //if (txtLocationName.Text.Trim().Length > 1)
            //    Search_Department(txtLocationName.Text.Trim(), Convert.ToInt32(ddllocaion.SelectedValue));
            //else
            //{
            //    LoadAllDept();
            //}
            Search_Department(txtLocationName.Text.Trim(),Convert.ToInt32(ddllocaion.SelectedValue));
        }

        void Search_Department(string deptname, int loc)
        {
            int retval = 0;
            string retmsg = string.Empty;

            try
            {
                cmd = new SqlCommand("searchDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptname", deptname);
                cmd.Parameters.AddWithValue("@locid", loc);
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@retval";
                p.SqlDbType = SqlDbType.Int;
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                SqlParameter pp = new SqlParameter();
                pp.ParameterName = "@retmsg";
                pp.SqlDbType = SqlDbType.VarChar;
                pp.Size = 100;
                pp.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pp);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                ds.Clear();
                da.Fill(ds, "emp");
                retval = (int)p.Value;
                retmsg = (string)pp.Value;
                if (retval == 1)
                {
                    if (ds.Tables["emp"].Rows.Count > 0)
                    {
                        Gvdepartment.DataSource = ds.Tables[0];
                        Gvdepartment.DataBind();
                    }
                    else
                    {
                        lblmsg.Text = "NO Matching Department Found";
                        lblmsg.ForeColor = Color.Red;
                        LoadAllDept();
                    }
                }
                else
                {
                    lblmsg.Text = retmsg.ToString();
                    lblmsg.ForeColor = Color.Red;
                    LoadAllDept();
                }

            }
            catch(Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = Color.Red;
            }
        }

        protected void btnSelectAlltop_Click(object sender, EventArgs e)
        {
            SelectAllCheckedBox();
        }

        void SelectAllCheckedBox()
        {
            foreach ( GridViewRow row in Gvdepartment.Rows)
            {
                CheckBox cb = row.FindControl("cbselect") as CheckBox;
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
            foreach (GridViewRow row in Gvdepartment.Rows)
            {
                CheckBox cb = row.FindControl("cbselect") as CheckBox;
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
            string deptid = string.Empty;
            int count = 0;

            foreach (GridViewRow li in Gvdepartment.Rows)
            {
                CheckBox cb = li.FindControl("cbselect") as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        count++;
                        deptid += cb.Attributes["data-did"] + ",";
                        cb.Checked = false;
                    }
                }
            }
            if (count != 0)
            {
                deptid = deptid.Substring(0, deptid.LastIndexOf(","));
                //Response.Write("<script>alert('Location ID :" + locid + "')</script>");
                DelecteData(deptid);
            }
            else
            {
                Response.Write("<script>alert('Select record to delect')</script>");
                return;
            }
        }
        
        void DelecteData(string deptid)
        {
            int retval = 0;
            string retmsg = string.Empty;
            cmd = new SqlCommand("DeleteDept", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deptid", deptid);
            cmd.Parameters.AddWithValue("@retval", retval).Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@retmsg", retmsg).Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            retval = (int)cmd.Parameters["@retval"].Value;
            if (retval == 1)
            {
                lblmsg.Text = "Deletion is Successfull !!!!";
                lblmsg.ForeColor = Color.Green;
                LoadAllDept();
            }
            else
            {
                lblmsg.Text = retmsg;
                lblmsg.ForeColor = Color.Red;
            }
            con.Close();
        }

        protected void Gvdepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvdepartment.EditIndex = e.NewPageIndex;
            LoadAllDept();
        }

        protected void Gvdepartment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gvdepartment.EditIndex = -1;
            LoadAllDept();
        }

        protected void Gvdepartment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Gvdepartment.EditIndex = e.NewEditIndex;
            LoadAllDept();
        }

        protected void Gvdepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string deptid = e.Keys["Deptid"].ToString();
        }

        protected void Gvdepartment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            string deptid = e.Keys["Deptid"].ToString();
            string deptName = ((TextBox)Gvdepartment.Rows[e.RowIndex].FindControl("txtdeptname")).Text.Trim();
            string deptdesc= ((TextBox) Gvdepartment.Rows[e.RowIndex].FindControl("txtdeptdesc")).Text.Trim();
            string deptloc = ((DropDownList)Gvdepartment.Rows[e.RowIndex].FindControl("ddlloc")).SelectedValue.ToString();
            bool isactive = Convert.ToBoolean(((CheckBox)Gvdepartment.Rows[e.RowIndex].FindControl("cbisactive")).Checked.ToString());

            cmd = new SqlCommand("Update_dept",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@deptid",deptid);
            cmd.Parameters.AddWithValue("@deptname",deptName);
            cmd.Parameters.AddWithValue("@deptdesc",deptdesc);
            cmd.Parameters.AddWithValue("@deptlocid",deptloc);
            cmd.Parameters.AddWithValue("@isactive", isactive);
            cmd.Parameters.AddWithValue("@retval", 0);
            cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@retmsg", string.Empty);
            cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            int rval = Convert.ToInt32(cmd.Parameters["@retval"].Value);
            string retmsg = Convert.ToString(cmd.Parameters["@retmsg"].Value);
            if (rval == 1)
            {
                lblmsg.Text = "Updated Successfully !!!";
                lblmsg.ForeColor = Color.Green;
            }
            else
            {
                lblmsg.Text = "Updation Failed  !!!";
                lblmsg.ForeColor = Color.Red;
            }
            Gvdepartment.EditIndex = -1;
            LoadAllDept();
        }

        protected void Gvdepartment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Gvdepartment.SelectedIndex = e.NewSelectedIndex;
            LoadAllDept();
        }

        protected void Gvdepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlLoc = (DropDownList)e.Row.FindControl("ddlloc");
                if (ddlLoc != null)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "sp_GetAllLocaion";
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@retval", 0);
                    cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                    cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "Loc");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlLoc.DataSource = ds.Tables[0];
                        ddlLoc.DataTextField = "LocationName";
                        ddlLoc.DataValueField = "LocationId";
                        ddlLoc.DataBind();
                    }
                    Label lblLocId = (Label)e.Row.FindControl("lbllocid");
                    if (lblLocId != null)
                    {
                    ddlLoc.SelectedIndex = ddlLoc.Items.IndexOf(ddlLoc.Items.FindByValue(lblLocId.Text));
                    }
                }
            }
        }

        protected void btnAddNewtop_Click(object sender, EventArgs e)
        {
            LoadLocation(2);
            divAddDept.Visible = true;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int retval = 3;
            string retmsg = string.Empty;
            try
            {
                cmd = new SqlCommand("insert_department", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@deptname", txtdeptname.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtdesc.Text.Trim());
                cmd.Parameters.AddWithValue("@locid", ddladdloc.SelectedValue);
                cmd.Parameters.AddWithValue("@isactive", chkboxActive.Checked);
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@retval";
                p.SqlDbType = SqlDbType.Int;
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                SqlParameter pp = new SqlParameter();
                pp.ParameterName = "@retmsg";
                pp.SqlDbType = SqlDbType.VarChar;
                pp.Size = 100;
                pp.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pp);
                
                con.Open();
                cmd.ExecuteNonQuery();
                retval = (int)p.Value;
                retmsg = (string)pp.Value;
                if (retval == 1)
                {
                    lblmsg.Text = "Department Added successfully";
                    lblmsg.ForeColor = Color.Green;
                    Cancel_Click(sender,e);
                    LoadAllDept();
                }
                else
                {
                    lblmsg.Text = retmsg;
                    lblmsg.ForeColor = Color.Red;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = Color.Red;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            txtdeptname.Text = "";
            txtdesc.Text = "";
            ddladdloc.SelectedIndex = 0;
            chkboxActive.Checked = false;
            divAddDept.Visible = false;
        }

        protected void Gvdepartment_Sorting(object sender, GridViewSortEventArgs e)
        {
           string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";

            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            Gvdepartment.DataSource = sortedView;
            Gvdepartment.DataBind(); 
        }

        public DataTable BindGridView()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "GetAllDept";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@retval", 0);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int retval = (int)cmd.Parameters["@retval"].Value;
                string retmsg = (string)cmd.Parameters["@retmsg"].Value;
  
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt;
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }  
    } 
}
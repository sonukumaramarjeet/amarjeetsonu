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
    public partial class frmManageEmployee : System.Web.UI.Page
    {

        double tot;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Empconnection"].ConnectionString);
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public DataSet ds = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["totsal"] = 0.00;
                ViewState["gtotal"] = 0.00;
                ViewState["totemp"] = 0;
                ViewState["gtotemp"] = 0;
                LoadAllDeptInfo();
               
                lblfooter.Text = "Grand Total Employee : " + ViewState["gtotemp"].ToString();
                lblfooter.Text +="&nbsp;&nbsp;Grand Total Employee Salary : "+ ViewState["gtotal"].ToString();
            }
        }

        private void LoadAllDeptInfo()
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
                        GvDepartment.DataSource = ds.Tables[0];
                        GvDepartment.DataBind();
                    }
                    else
                    {
                        
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

        protected void GvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Deptmtid = GvDepartment.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvEmp = e.Row.FindControl("GvEmployee") as GridView;

                cmd = new SqlCommand();
                cmd.CommandText = "GetEmp_Dept_Wise";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = Convert.ToInt32(Deptmtid);
                cmd.Parameters.AddWithValue("@retval", 0);
                cmd.Parameters["@retval"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@retmsg", string.Empty);
                cmd.Parameters["@retmsg"].Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "Emp");

                int retval = (int)cmd.Parameters["@retval"].Value;
                string retmsg = (string)cmd.Parameters["@retmsg"].Value;

                if (retval == 1)
                {
                        gvEmp.DataSource = ds.Tables[0];
                        gvEmp.DataBind();
                }
                else
                {
                    lblmsg.Text = retmsg;
                    lblmsg.ForeColor = Color.Red;
                }

                if (ds.Tables["Emp"].Rows.Count > 0)
                {
                    Label lbltot = (Label)e.Row.FindControl("lblSummery");
                    lbltot.Text = "Total Empoloyee : " + ViewState["totemp"].ToString();
                    lbltot.Text += "&nbsp;&nbsp; Total Salary : " + ViewState["totsal"].ToString();
                    ViewState["totemp"] = 0;
                    ViewState["totsal"] = 0.00;
                }
            }

        }
        
        protected void  GvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double sal = Convert.ToDouble(((Label)e.Row.FindControl("empsal")).Text);
                ViewState["totsal"] = Convert.ToDouble(ViewState["totsal"]) + sal;
                ViewState["gtotal"] = Convert.ToDouble(ViewState["gtotal"])+sal;
                ViewState["totemp"] = Convert.ToInt32(ViewState["totemp"].ToString())+1;
                ViewState["gtotemp"] = Convert.ToInt32(ViewState["gtotemp"].ToString())+1;
            }
            
            if(e.Row.RowType== DataControlRowType.Footer)
            {
               //Response.Write(tot+" ");
               //Label lbl = e.Row.Parent.
                
                
            }
        }


        //Nested Employee GridView
        protected void GvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDepartment.PageIndex = e.NewPageIndex;
            LoadAllDeptInfo();
        }


        protected void GvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDepartment.PageIndex = e.NewPageIndex;
            LoadAllDeptInfo();
        }


        //Nested Employee GridView

        protected void GvDepartment_Sorting(object sender, GridViewSortEventArgs e)
        {

        }


        protected void GvDepartment_Sorting(object sender, GridViewSortEventArgs e)
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
            GvDepartment.DataSource = sortedView;
            GvDepartment.DataBind(); 
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

        protected void GvDepartment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //GvDepartment.SelectedIndex = e.NewSelectedIndex;
           // LoadAllDeptInfo();
        }

        

    }
}
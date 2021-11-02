using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace KPMAMS.Admin
{
    public partial class TimetableList_Admin : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (!(Session["userGUID"] == null))
                {
                    Response.Redirect("Login.aspx");
                }
                BindGridView();
            }

        }

        private void BindGridView()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                String strSelect =
                    "SELECT b.TimetableGUID,Class,convert(VARCHAR(20),b.CreateDate,100) As CreateDate,convert(VARCHAR(20),b.LastUpdateDate,100) As LastUpdateDate " +
                    "FROM Classroom a LEFT JOIN Timetable b ON a.TimetableGUID =b.TimetableGUID " +
                    "WHERE a.TimetableGUID IS NOT NULL";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    GvTimetableList.DataSource = dt;
                    GvTimetableList.DataBind();
                }
                else
                {
                    lblNoData.Visible = false;
                    GvTimetableList.DataSource = dt;
                    GvTimetableList.DataBind();
                }

                con.Close();
            }
            catch (SqlException ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GvTimetableList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "TimetableDetails_Admin.aspx" + "?TimetableGUID=" + DataBinder.Eval(e.Row.DataItem, "TimetableGUID");
            }
        }

        protected void btnCreateTimetable_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateTimetable.aspx");
        }
    }
}
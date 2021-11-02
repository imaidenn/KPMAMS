using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class TimetableList : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (!(Session["userGUID"] != null))
                {
                    Response.Redirect("Login.aspx");
                }
                if (Session["role"].Equals("Teacher")|| Session["role"].Equals("Parent"))
                {
                    BindGridView();
                }
                else {
                    ViewTimeTable();
                }
            }
        }

        private void ViewTimeTable()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string strSelect = "";

                if (Session["role"].Equals("Parent"))
                {
                    strSelect =
                    "SELECT TimetableGUID " +
                    "FROM Parent a LEFT JOIN Student b ON a.ParentGUID=b.ParentGUID " +
                    "LEFT JOIN Classroom c ON b.ClassroomGUID=c.ClassroomGUID " +
                    "WHERE a.ParentGUID='" + Session["userGUID"] + "'";
                }
                else {

                    strSelect =
                        "SELECT TimetableGUID " +
                        "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID=b.ClassroomGUID " +
                        "WHERE StudentGUID='" + Session["userGUID"] + "'";

                }

                SqlCommand cmd = new SqlCommand(strSelect, con);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script language='javascript'>alert('Error:No timetable found');</script>");
                    Server.Transfer("Homepage.aspx", true);
                }
                else
                {
                    Response.Redirect("TimetableDetails.aspx" + "?TimetableGUID=" + dt.Rows[0][0]);
                }
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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
                string strSelect = "";
                if (Session["role"].Equals("Teacher"))
                {
                    strSelect =
                    "SELECT c.TimetableGUID,Class,convert(VARCHAR(20),c.CreateDate,100) As CreateDate,convert(VARCHAR(20),c.LastUpdateDate,100) As LastUpdateDate " +
                    "FROM Teacher_Classroom a LEFT JOIN Classroom b ON a.ClassroomGUID=b.ClassroomGUID " +
                    "LEFT JOIN Timetable c ON b.TimetableGUID =c.TimetableGUID " +
                    "WHERE b.TimetableGUID IS NOT NULL AND a.TeacherGUID='" + Session["userGUID"] + "'";

                }
                else {
                    strSelect =
                        "SELECT d.TimetableGUID,Class,convert(VARCHAR(20),d.CreateDate,100) As CreateDate,convert(VARCHAR(20),d.LastUpdateDate,100) As LastUpdateDate " +
                        "FROM Parent a LEFT JOIN Student b ON a.ParentGUID=b.ParentGUID " +
                        "LEFT JOIN Classroom c ON b.ClassroomGUID=c.ClassroomGUID " +
                        "LEFT JOIN Timetable d ON c.TimetableGUID=d.TimetableGUID " +
                        "WHERE c.TimetableGUID IS NOT NULL AND a.ParentGUID='" + Session["userGUID"] + "' " +
                        "GROUP BY d.TimetableGUID,Class,d.CreateDate,d.LastUpdateDate";
                }
                
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
                    hyperLink.Attributes["href"] = "TimetableDetails.aspx" + "?TimetableGUID=" + DataBinder.Eval(e.Row.DataItem, "TimetableGUID");
            }
        }

    }
}
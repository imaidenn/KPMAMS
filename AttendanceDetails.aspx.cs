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
    public partial class AttendanceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Session["userGUID"] != null && Session["role"].ToString() == "Student")
                {
                    GetAttendance(Session["userGUID"].ToString());
                }
                else if(Session["userGUID"] != null &&  Session["role"].ToString() == "Parent" && Request.QueryString["StudentGUID"] != null)
                {
                    GetAttendance(Request.QueryString["StudentGUID"].ToString());
                }

            }
        }

        protected void GetAttendance(string studentguid)
        {
            try
            {
                GetDetails(studentguid);
                string userGUID = studentguid;
                string subjectGUID = Request.QueryString["SubjectGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT AttendanceGUID,Status,cast(StartTime as date) AS Date FROM Attendance WHERE StudentGUID = @StudentGUID AND SubjectGUID = @SubjectGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", userGUID);
                cmdSelect.Parameters.AddWithValue("@SubjectGUID", subjectGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {

                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Class = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Class = " + totalQty;

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetDetails(string studentguid)
        {
            try
            {
                string userGUID = studentguid;
                string subjectGUID = Request.QueryString["SubjectGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT COUNT(a.AttendanceGUID) AS TotalClass,SUM(CASE WHEN a.Status = 'Present' THEN 1 ELSE 0 END) AS TotalAttend,a.SubjectGUID,b.SubjectName FROM Attendance a LEFT JOIN Subject b ON a.SubjectGUID = b.SubjectGUID " +
                    "WHERE a.StudentGUID = @StudentGUID AND b.SubjectGUID = @SubjectGUID " +
                    "GROUP BY b.SubjectName,a.SubjectGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", userGUID);
                cmdSelect.Parameters.AddWithValue("@SubjectGUID", subjectGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();


                if (dt.Rows.Count != 0)
                {
                    lblSubject.Text = dt.Rows[0][3].ToString();
                    lblTotalClass.Text = "Total Class Meeting = " + dt.Rows[0][0].ToString();
                    lblTotalAttend.Text = "Total Class Attended = " + dt.Rows[0][1].ToString();
                }
                else
                {

                }

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
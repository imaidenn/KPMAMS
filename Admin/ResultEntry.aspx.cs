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
    public partial class ResultEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["StudentGUID"] != null && Request.QueryString["ExamSemester"] != null)
            {

                if (IsPostBack == false)
                {
                    GetDetails();

                }
            }
        }

        protected void GetResult()
        {
            try
            {

                string StudentGUID = Request.QueryString["StudentGUID"].ToString();
                string ExamSemester = Request.QueryString["ExamSemester"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.ClassroomGUID, b.Class, * FROM Exam a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID LEFT JOIN Subject c ON a.SubjectGUID = c.SubjectGUID " +
                    "WHERE a.StudentGUID = @StudentGUID AND a.ExamSemester = @ExamSemester AND a.Status = 'Confirmed' ORDER BY c.SubjectName";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ExamSemester);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);


                con.Close();

                if (dt.Rows.Count > 0)
                {
                    string classGUID = dt.Rows[0][0].ToString();
                    lblClass.Text = dt.Rows[0][1].ToString();

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    lblNoData.Visible = false;
                    GetSummary(classGUID);
                }
                else
                {
                    lblNoData.Visible = true;
                }


            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }

        }

        protected void GetDetails()
        {
            try
            {

                string StudentGUID = Request.QueryString["StudentGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT FullName,ICNo,Gender FROM Student WHERE StudentGUID = @StudentGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblIC.Text = dt.Rows[0][1].ToString();
                    lblGender.Text = dt.Rows[0][2].ToString();
                    GetResult();
                }

                
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetSummary(string classGUID)
        {
            try
            {
                string StudentGUID = Request.QueryString["StudentGUID"].ToString();
                string ExamSemester = Request.QueryString["ExamSemester"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ROW_NUMBER() OVER(ORDER BY AverageMark DESC) AS PlaceInClass, a.AverageMark, a.GPA, a.CGPA, b.StudentGUID, b.ResultGUID " +
                    "FROM Result a LEFT JOIN Exam b ON b.ResultGUID = a.ResultGUID " +
                    "WHERE b.ExamSemester = @ExamSemester AND b.ClassroomGUID = @ClassroomGUID " +
                    "GROUP BY b.StudentGUID,AverageMark,b.ResultGUID,a.AverageMark,a.GPA,a.CGPA";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ExamSemester);
                cmdSelect.Parameters.AddWithValue("@ClassroomGUID", classGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string studentGuid = row["StudentGUID"].ToString();
                        if (studentGuid == Request.QueryString["StudentGUID"].ToString())
                        {
                            lblplclass.Text = row["PlaceInClass"].ToString() + "/" + (dt.Rows.Count).ToString();
                            lblAvgMark.Text = row["AverageMark"].ToString();
                            lblgpa.Text = row["GPA"].ToString();
                            lblcgpa.Text = row["CGPA"].ToString();
                        }
                    }

                }

                GetClass(classGUID);

                con.Close();


            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetClass(string classguid)
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                string strSelect = "SELECT Form FROM Classroom WHERE ClassroomGUID = @Class";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Class", classguid);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    string form = dt.Rows[0][0].ToString();
                    GetSummary2(form);
                }


                con.Close();


            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetSummary2(string form)
        {
            try
            {
                string StudentGUID = Request.QueryString["StudentGUID"].ToString();
                string ExamSemester = Request.QueryString["ExamSemester"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                string strSelect = "SELECT ROW_NUMBER() OVER(ORDER BY AverageMark DESC) AS PlaceInForm,b.StudentGUID,b.ResultGUID " +
                    "FROM Result a LEFT JOIN Exam b ON b.ResultGUID = a.ResultGUID LEFT JOIN Classroom c ON b.ClassroomGUID = c.ClassroomGUID " +
                    "WHERE b.ExamSemester = @ExamSemester AND c.Form = @form " +
                    "GROUP BY b.StudentGUID,AverageMark,b.ResultGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ExamSemester);
                cmdSelect.Parameters.AddWithValue("@form", form);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string studentGuid = row["StudentGUID"].ToString();
                        if (studentGuid == Request.QueryString["StudentGUID"].ToString())
                        {
                            lblplform.Text = row["PlaceInForm"].ToString() + "/" + (dt.Rows.Count).ToString();

                        }
                    }

                }


                con.Close();


            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }
}
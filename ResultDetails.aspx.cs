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
    public partial class ResultDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userGUID"] != null)
            {
                
                if (IsPostBack == false)
                {
                    GetDetails();
                    GetSemester();

                }
            }
        }


        protected void GetSemester()
        {
            try
            {
                string StudentGUID = Session["userGUID"].ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ExamSemester,SemText FROM Exam WHERE StudentGUID = @StudentGUID AND Status = 'Confirmed' GROUP BY ExamSemester,SemText ORDER BY ExamSemester DESC";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();


                if (dt.Rows.Count > 0)
                {
                    ddlSem.DataTextField = dt.Columns["SemText"].ToString();
                    ddlSem.DataValueField = dt.Columns["ExamSemester"].ToString();
                    ddlSem.DataSource = dt;
                    ddlSem.DataBind();
                    GetResult();
                }




            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetResult()
        {
            try
            {

                string StudentGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.Class, b.Class, a.ExamGUID, * FROM Exam a LEFT JOIN Classroom b ON a.Class = b.ClassroomGUID LEFT JOIN Subject c ON a.SubjectGUID = c.SubjectGUID " +
                    "WHERE a.StudentGUID = @StudentGUID AND a.ExamSemester = @ExamSemester AND a.Status = 'Confirmed' ORDER BY c.SubjectName";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ddlSem.SelectedValue.ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);


                con.Close();

                if (dt.Rows.Count > 0)
                {
                    string classGUID = dt.Rows[0][0].ToString();
                    lblClass.Text = dt.Rows[0][1].ToString();
                    string examGUID = dt.Rows[0][2].ToString();

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
        protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GetResult();
        }

        protected void GetDetails()
        {
            try
            {

                string StudentGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT FullName,ICNo,Gender FROM Student WHERE StudentGUID = @StudentGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblIC.Text = dt.Rows[0][1].ToString();
                    lblGender.Text = dt.Rows[0][2].ToString();

                }


                con.Close();
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
                string StudentGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ROW_NUMBER() OVER(ORDER BY AverageMark DESC) AS PlaceInClass, a.AverageMark, a.GPA, a.CGPA, b.StudentGUID, b.ResultGUID " +
                    "FROM Result a LEFT JOIN Exam b ON b.ResultGUID = a.ResultGUID " +
                    "WHERE b.ExamSemester = @ExamSemester AND b.Class = @ClassroomGUID " +
                    "GROUP BY b.StudentGUID,AverageMark,b.ResultGUID,a.AverageMark,a.GPA,a.CGPA";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ddlSem.SelectedValue);
                cmdSelect.Parameters.AddWithValue("@ClassroomGUID", classGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        string studentGuid = row["StudentGUID"].ToString();
                        if (studentGuid == Session["userGUID"].ToString())
                        {
                            lblplclass.Text = row["PlaceInClass"].ToString() + "/" + (dt.Rows.Count).ToString();
                            lblAvgMark.Text = row["AverageMark"].ToString();
                            lblgpa.Text = row["GPA"].ToString();
                            lblcgpa.Text = row["CGPA"].ToString();
                        }
                    }

                }

                GetSummary2();

                con.Close();

                
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetSummary2()
        {
            try
            {
                string StudentGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                string strSelect = "SELECT ROW_NUMBER() OVER(ORDER BY AverageMark DESC) AS PlaceInForm,b.StudentGUID,b.ResultGUID " +
                    "FROM Result a LEFT JOIN Exam b ON b.ResultGUID = a.ResultGUID " +
                    "WHERE b.ExamSemester = @ExamSemester " +
                    "GROUP BY b.StudentGUID,AverageMark,b.ResultGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ddlSem.SelectedValue);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string studentGuid = row["StudentGUID"].ToString();
                        if (studentGuid == Session["userGUID"].ToString())
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    } 
    
}
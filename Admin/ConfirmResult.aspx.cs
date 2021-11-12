using System;
using System.Collections;
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
    public partial class ConfirmResult : System.Web.UI.Page
    {
        double gpa = 0.0;
        double cgpa = 0.0;
        double a = 4.0;
        double am = 3.67;
        double bp = 3.33;
        double b = 3.0;
        double bm = 2.67;
        double cp = 2.33;
        double c = 2;
        double cm = 1.67;
        double d = 1.33;
        double dm = 1;
        double e = 0.67;
        double f = 0.33;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                
                if (Request.QueryString["StudentGUID"] != null && Request.QueryString["ExamSemester"] != null)
                {
                    GetExistingData();
                }

            }
        }

        protected void GetExistingData()
        {
            try
            {
                string StudentGUID = Request.QueryString["StudentGUID"];
                string ExamSemester = Request.QueryString["ExamSemester"];

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT e.Class,b.FullName,a.ExamSemester,c.FullName,a.CreateDate,a.LastUpdateDate,a.SubjectGUID,d.SubjectName,a.Mark,a.Grade,SUM(Mark) over (partition by a.StudentGUID) as TotalMark, a.ExamGUID " +
                    "FROM Exam a LEFT JOIN Student b ON a.StudentGUID = b.StudentGUID LEFT JOIN Teacher c ON a.CreatedBy = c.TeacherGUID LEFT JOIN Subject d ON a.SubjectGUID = d.SubjectGUID LEFT JOIN Classroom e ON b.ClassroomGUID = e.ClassroomGUID " +
                    "WHERE a.StudentGUID = @StudentGUID AND a.ExamSemester = @ExamSemester";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                cmdSelect.Parameters.AddWithValue("@ExamSemester", ExamSemester);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    txtClass.Text = dt.Rows[0][0].ToString();
                    txtName.Text = dt.Rows[0][1].ToString();
                    txtSem.Text = dt.Rows[0][2].ToString();
                    txtCreatedBy.Text = dt.Rows[0][3].ToString();
                    txtDate.Text = dt.Rows[0][4].ToString();
                    txtUpdate.Text = dt.Rows[0][5].ToString();

                    txtAvgMark.Text = (double.Parse(dt.Rows[0][10].ToString()) / dt.Rows.Count).ToString();

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }


                con.Close();
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (NewResult())
                {
                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(strCon);

                    con.Open();

                    String strUpdate = "UPDATE Exam SET LastUpdateDate = @LastUpdateDate, Status = 'Confirmed' WHERE StudentGUID='" + Guid.Parse(Request.QueryString["StudentGUID"]) + "' AND ExamSemester = '" + Request.QueryString["ExamSemester"] + "'";

                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                    cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                    cmdUpdate.ExecuteNonQuery();

                    con.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Exam Mark Confirmed');window.location ='ApproveExam.aspx';", true);
                }
                

            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Exam SET LastUpdateDate = @LastUpdateDate, Status = 'Rejected' WHERE StudentGUID='" + Guid.Parse(Request.QueryString["StudentGUID"]) + "' AND ExamSemester = '" + Request.QueryString["ExamSemester"] + "'";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New Exam Mark Rejected');window.location ='ApproveExam.aspx';", true);

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetGPA()
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    string grade = row.Cells[4].Text.Trim();

                    if(grade == "A" || grade == "A+")
                    {
                        gpa += a;
                    }
                    if(grade == "A-")
                    {
                        gpa += am;
                    }
                    if(grade == "B+")
                    {
                        gpa += bp;
                    }
                    if(grade == "B")
                    {
                        gpa += b;
                    }
                    if(grade == "B-")
                    {
                        gpa += bm;
                    }
                    if(grade == "C+")
                    {
                        gpa += cp;
                    }
                    if(grade == "C")
                    {
                        gpa += c;
                    }
                    if(grade == "C-")
                    {
                        gpa += cm;
                    }
                    if(grade == "D")
                    {
                        gpa += d;
                    }
                    if(grade == "D-")
                    {
                        gpa += dm;
                    }
                    if(grade == "E")
                    {
                        gpa += e;
                    }
                    if(grade == "F")
                    {
                        gpa += f;
                    }
                    

                }
            }

            gpa = gpa / GridView1.Rows.Count;

            DataTable dt = new DataTable();

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            con.Open();

            String strSelect = "SELECT TOP 1 PERCENT a.CGPA FROM Result a LEFT JOIN Exam b ON b.ResultGUID = a.ResultGUID WHERE b.StudentGUID = @StudentGUID ORDER BY b.LastUpdateDate DESC";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@StudentGUID", Request.QueryString["StudentGUID"].ToString());

            SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

            dt.Load(dtrSelect);

            if (dt.Rows.Count > 0)
            {
                cgpa = (double.Parse(dt.Rows[0][0].ToString()) + gpa) / 2;
            }
            else
            {
                cgpa = gpa;
            }

            con.Close();
        }


        protected bool NewResult()
        {
            bool addBool = false;
            try
            {
                Guid ResultGuid = Guid.NewGuid();
                GetGPA();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                

                String strInsert = "INSERT INTO Result VALUES (@ResultGUID,@AvgMark,@GPA,@CGPA,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@ResultGUID", ResultGuid);
                cmdInsert.Parameters.AddWithValue("@AvgMark", Decimal.Parse(txtAvgMark.Text));
                cmdInsert.Parameters.AddWithValue("@GPA", gpa);
                cmdInsert.Parameters.AddWithValue("@CGPA", cgpa);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);


                cmdInsert.ExecuteNonQuery();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string ExamGuid = row.Cells[0].Text;
                        strInsert = "UPDATE Exam SET ResultGUID = @ResultGUID WHERE ExamGUID = @ExamGUID";

                        cmdInsert = new SqlCommand(strInsert, con);

                        cmdInsert.Parameters.AddWithValue("@ResultGUID", ResultGuid);
                        cmdInsert.Parameters.AddWithValue("@ExamGUID", ExamGuid);

                        cmdInsert.ExecuteNonQuery();


                    }
                }

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }
    }
}
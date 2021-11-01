using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    
    public partial class MarkAttendance : System.Web.UI.Page
    {
        string classroomGUID = "";
        string subjectGUID = "";
        ArrayList student = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != false)
            {
                if (Session["userGUID"] != null && Session["role"] != null)
                {
                    if (Session["role"].ToString() == "Teacher")
                    {

                    }
                }
            }
        }

        protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClassStudent();

        }

        protected void GetMeeting()
        {
            try
            {
                string teacherGUID = Session["userGUID"].ToString();
                DateTime date = DateTime.Parse(txtDate.Text);
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.MeetingGUID,b.Class,b.ClassroomGUID,c.SubjectTeach,d.SubjectName FROM Meeting a " +
                    "LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID LEFT JOIN Teacher_Classroom c ON b.ClassroomGUID = c.ClassroomGUID LEFT JOIN Subject d ON c.SubjectTeach = d.SubjectGUID " +
                    "WHERE a.TeacherGUID = @TeacherGUID AND CONVERT(date,a.MeetingTime) = @Date";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherGUID", teacherGUID);
                cmdSelect.Parameters.AddWithValue("@Date", (date).ToString("dd-MMM-yyyy"));

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count != 0)
                {
                    classroomGUID = dt.Rows[0][2].ToString();
                    subjectGUID = dt.Rows[0][3].ToString();
                    ddlMeeting.DataTextField = dt.Columns["Class"].ToString();
                    ddlMeeting.DataValueField = dt.Columns["MeetingGUID"].ToString();
                    ddlMeeting.DataSource = dt;
                    ddlMeeting.DataBind();
                    GetClassStudent();
                }


            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetClassStudent()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT StudentGUID,FullName FROM Student WHERE ClassroomGUID = @ClassroomGUID";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ClassroomGUID", classroomGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);



                if (dt.Rows.Count != 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        DataTable dt2 = new DataTable();
                        strSelect = "SELECT AttendanceGUID FROM Attendance WHERE StudentGUID = @StudentGUID AND MeetingGUID = @MeetingGUID";


                        cmdSelect = new SqlCommand(strSelect, con);
                        cmdSelect.Parameters.AddWithValue("@StudentGUID", row["StudentGUID"].ToString());
                        cmdSelect.Parameters.AddWithValue("@MeetingGUID", ddlMeeting.SelectedValue);

                        dtrSelect = cmdSelect.ExecuteReader();

                        dt2.Load(dtrSelect);
                        if (dt2.Rows.Count == 0)
                        {
                            String strInsert = "INSERT INTO Attendance VALUES(@AttendanceGUID,@StudentGUID,@SubjectGUID,@MeetingGUID,@StartTime,@EndTime,@Total,'Absent',@createDate,@lastUpdateDate)";

                            SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                            cmdInsert.Parameters.AddWithValue("@AttendanceGUID", Guid.NewGuid());
                            cmdInsert.Parameters.AddWithValue("@StudentGUID", row["StudentGUID"].ToString());
                            cmdInsert.Parameters.AddWithValue("@SubjectGUID", subjectGUID);
                            cmdInsert.Parameters.AddWithValue("@MeetingGUID", ddlMeeting.SelectedValue);
                            cmdInsert.Parameters.AddWithValue("@StartTime", DateTime.Now);
                            cmdInsert.Parameters.AddWithValue("@EndTime", DateTime.Now);
                            cmdInsert.Parameters.AddWithValue("@Total", 0);
                            cmdInsert.Parameters.AddWithValue("@createDate", DateTime.Now);
                            cmdInsert.Parameters.AddWithValue("@lastUpdateDate", DateTime.Now);


                            cmdInsert.ExecuteNonQuery();
                        }
                        //student.Add(dt.Rows[0][0].ToString());
                    }
                    con.Close();

                    LoadStudentAttendance();
                    //CheckStudentAttendance();
                }

            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }


        protected void LoadStudentAttendance()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.AttendanceGUID,a.StudentGUID,b.FullName,a.Status FROM Attendance a LEFT JOIN Student b ON a.StudentGUID = b.StudentGUID" +
                    " WHERE MeetingGUID = @MeetingGUID";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@MeetingGUID", ddlMeeting.SelectedValue);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count != 0)
                {
                    lblInfo.Visible = true;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if(row.Cells[3].Text == "Present")
                        {
                            ((HtmlInputCheckBox)row.FindControl("cbAttendance")).Checked = true;
                        }
                        

                    }
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (UpdateAttendance())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance update successful');window.location ='Homepage.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Attendance update failed');window.location ='Homepage.aspx';", true);
            }
            

        }

        protected bool UpdateAttendance()
        {
            bool update = false;
            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);
                string status = "";
                con.Open();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string AttendanceGUID = row.Cells[0].Text.ToString();
                        if (((HtmlInputCheckBox)row.FindControl("cbAttendance")).Checked == true)
                        {
                            status = "Present";
                        }
                        else
                        {
                            status = "Absent";
                        }

                        String strUpdate = "UPDATE Attendance SET Status = @Status,LastUpdateDate = @lastUpdateDate WHERE AttendanceGUID = @AttendanceGUID";

                        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                        cmdUpdate.Parameters.AddWithValue("@AttendanceGUID", AttendanceGUID);
                        cmdUpdate.Parameters.AddWithValue("@Status", status);
                        cmdUpdate.Parameters.AddWithValue("@lastUpdateDate", DateTime.Now);


                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                con.Close();
                update = true;
                return update;
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return update;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ddlMeeting.Items.Clear();
            GetMeeting();
        }
    }
}
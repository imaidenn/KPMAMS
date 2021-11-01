using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace KPMAMS
{
    public partial class test1 : System.Web.UI.Page
    {
        string subjectGUID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userGUID"] != null)
            {
                if (Session["role"].ToString() == "Student")
                {
                    FindMeeting();

                }
            }


        }

        protected void FindMeeting()
        {
            try
            {
                string meetGUID = Session["MeetingGUID"].ToString();
                string studentGUID = Session["userGUID"].ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT AttendanceGUID FROM Attendance WHERE MeetingGUID = @MeetingGUID AND StudentGUID = @StudentGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@MeetingGUID", meetGUID);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", studentGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);
                string attendanceGUID = dt.Rows[0][0].ToString();

                con.Close();

                if (dt.Rows.Count != 0)
                {
                    UpdateTime(attendanceGUID);
                }
                else
                {
                    InsertAttendance();
                }
                
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void UpdateTime(string attendanceGUID)
        {
            try
            {
                GetMeetingDetails();

                string studentGUID = Session["userGUID"].ToString();
                DateTime startTime = DateTime.Now;

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Attendance SET StartTime = @StartTime " +
                    "WHERE AttendanceGUID = @AttendanceGUID";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@AttendanceGUID", attendanceGUID);
                cmdUpdate.Parameters.AddWithValue("@StartTime", startTime);
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                Session["AttendanceGUID"] = attendanceGUID.ToString();
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetMeetingDetails()
        {
            string meetGUID = Session["MeetingGUID"].ToString();
            DataTable dt = new DataTable();

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            con.Open();
            String strSelect = "SELECT a.SubjectGUID,a.SubjectName,c.Duration FROM Subject a LEFT JOIN Teacher_Classroom b ON a.SubjectGUID = b.SubjectTeach LEFT JOIN Meeting c ON b.ClassroomGUID = c.ClassroomGUID " +
                "WHERE c.MeetingGUID = @MeetingGUID";

            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@MeetingGUID", meetGUID);
            SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

            dt.Load(dtrSelect);


            con.Close();

            if (dt.Rows.Count != 0)
            {
                subjectGUID = dt.Rows[0][0].ToString();
            }
        }

        protected void InsertAttendance()
        {
            try
            {
                GetMeetingDetails();
                Guid attendanceGUID = Guid.NewGuid();
                string meetGUID = Session["MeetingGUID"].ToString();

                DateTime startTime = DateTime.Now;
                string studentGUID = Session["userGUID"].ToString();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Attendance(AttendanceGUID,StudentGUID,SubjectGUID,MeetingGUID,StartTime,CreateDate,LastUpdateDate) " +
                    "VALUES (@AttendanceGUID,@StudentGUID,@SubjectGUID,@MeetingGUID,@StartTime,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@AttendanceGUID", attendanceGUID);
                cmdInsert.Parameters.AddWithValue("@StudentGUID", studentGUID);
                cmdInsert.Parameters.AddWithValue("@SubjectGUID", subjectGUID);
                cmdInsert.Parameters.AddWithValue("@MeetingGUID", meetGUID);
                cmdInsert.Parameters.AddWithValue("@StartTime", startTime);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();

                Session["AttendanceGUID"] = attendanceGUID.ToString();
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

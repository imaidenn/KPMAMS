using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class LeaveMeeting : System.Web.UI.Page
    {
        int meetDuration = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["AttendanceGUID"] != null && Session["MeetingGUID"] != null)
            {
       
                if(Session["Role"].ToString() == "Student")
                {
                    GetAttendance();
                    //UpdateMeeting();
                }

                
            }
        }

        //protected void UpdateMeeting()
        //{
        //    try
        //    {
        //        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //        SqlConnection con = new SqlConnection(strCon);

        //        con.Open();
        //        String strUpdate = "UPDATE Meeting SET Status = 'Inactive', LastUpdateDate = @LastUpdateDate WHERE DATEDIFF(MINUTE, CONVERT(nvarchar, MeetingTime, 8) , CONVERT(nvarchar, GETDATE(), 8)) > Duration";

        //        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
        //        cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
        //        SqlDataReader dtrSelect = cmdUpdate.ExecuteReader();

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayAlertMsg(ex.Message);
        //    }
        //}

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
                meetDuration = int.Parse(dt.Rows[0][2].ToString());
            }
        }

        protected void GetAttendance()
        {
            try
            {
                string AttendanceGUID = Session["AttendanceGUID"].ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT StartTime,EndTime,TotalTime FROM Attendance WHERE AttendanceGUID = @AttendanceGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@AttendanceGUID", AttendanceGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                DateTime startTime = DateTime.Parse(dt.Rows[0][0].ToString());
                double total = double.Parse(dt.Rows[0][2].ToString());

                con.Close();

                if(dt.Rows[0][1] == null)
                {
                    InsertAttendance(startTime);
                }
                else
                {
                    UpdateAttendance(startTime,total);
                }

            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void UpdateAttendance(DateTime startTime,double total)
        {
            try
            {
                string AttendanceGUID = Session["AttendanceGUID"].ToString();
                GetMeetingDetails();
                double time = meetDuration * 0.5;

                string studentGUID = Session["userGUID"].ToString();
                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                total += ts.TotalMinutes;
                string status = "";

                if (total > time)
                {
                    status = "Present";
                }
                else
                {
                    status = "Absent";
                }


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Attendance SET EndTime = @EndTime, TotalTime = @Total, Status = @Status, LastUpdateDate = @LastUpdateDate " +
                    "WHERE AttendanceGUID = @AttendanceGUID";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@AttendanceGUID", AttendanceGUID);
                cmdUpdate.Parameters.AddWithValue("@EndTime", endTime);
                cmdUpdate.Parameters.AddWithValue("@Total", total);
                cmdUpdate.Parameters.AddWithValue("@Status", status);
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                Session["MeetingGUID"] = null;
                Session["AttendanceGUID"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Back to Homepage');window.location ='Homepage.aspx';", true);
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void InsertAttendance(DateTime startTime)
        {
            try
            {
                string AttendanceGUID = Session["AttendanceGUID"].ToString();
                GetMeetingDetails();
                double time = meetDuration * 0.5;

                string studentGUID = Session["userGUID"].ToString();
                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                int Total = int.Parse(ts.TotalMinutes.ToString());
                string status = "";

                if(Total > time)
                {
                    status = "Present";
                }
                else
                {
                    status = "Absent";
                }


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Attendance SET EndTime = @EndTime, TotalTime = @Total, Status = @Status, LastUpdateDate = @LastUpdateDate " +
                    "WHERE AttendanceGUID = @AttendanceGUID";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@AttendanceGUID", AttendanceGUID);
                cmdUpdate.Parameters.AddWithValue("@EndTime", endTime);
                cmdUpdate.Parameters.AddWithValue("@Total", Total);
                cmdUpdate.Parameters.AddWithValue("@Status", status);
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                Session["MeetingGUID"] = null;
                Session["AttendanceGUID"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Back to Homepage');window.location ='Homepage.aspx';", true);
            }
            catch(Exception ex)
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
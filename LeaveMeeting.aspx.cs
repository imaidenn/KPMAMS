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
                GetAttendance();
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
                String strSelect = "SELECT StartTime FROM Attendance WHERE AttendanceGUID = @AttendanceGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@AttendanceGUID", AttendanceGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                DateTime startTime = DateTime.Parse(dt.Rows[0][0].ToString());
                con.Close();

                InsertAttendance(startTime);
            }
            catch(Exception ex)
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
                double time = meetDuration * 0.8;

                string studentGUID = Session["userGUID"].ToString();
                DateTime endTime = DateTime.Now;
                TimeSpan ts = endTime - startTime;
                double Total = ts.TotalMinutes;
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
                Response.Redirect("Homepage.aspx");
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
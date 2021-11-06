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
    public partial class MeetingInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["MeetingGUID"] != null)
            {
                Session["MeetingGUID"] = Request.QueryString["MeetingGUID"];
                GetMeetingListing();
                if(IsPostBack != false)
                {
                    //Request.Form["meeting_number"] = txtMeetingID.Text;
                    //string meetingpass = txtMeetingPass.Text;
                }
            }

        }

        protected void GetMeetingListing()
        {
            try
            {
                string usertype = Session["role"].ToString();
                string userGUID = Session["userGUID"].ToString();
                string meeting = Request.QueryString["MeetingGUID"];

                if (usertype == "Teacher")
                {
                    txtRole.Text = "1";
                }
                else
                {
                    txtRole.Text = "0";
                }

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "";

                if (usertype == "Teacher")
                {
                    strSelect = "SELECT a.MeetingRoomID,a.MeetingRoomPass,a.MeetingTopic,a.MeetingTime,a.Duration,b.Class,c.Fullname FROM Meeting a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID LEFT JOIN Teacher c ON a.TeacherGUID = c.TeacherGUID " +
                        "WHERE MeetingGUID = @MeetingGUID";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@MeetingGUID", meeting);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                }
                else if (usertype == "Student")
                {
                    strSelect = "SELECT a.MeetingRoomID,a.MeetingRoomPass,a.MeetingTopic,a.MeetingTime,a.Duration,b.Class,c.Fullname FROM Meeting a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID LEFT JOIN Student c ON a.ClassroomGUID = c.ClassroomGUID " +
                        "WHERE a.MeetingGUID = @MeetingGUID AND c.StudentGUID = @StudentGUID";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@MeetingGUID", meeting);
                    cmdSelect.Parameters.AddWithValue("@StudentGUID", userGUID);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                }


                con.Close();

                if (dt.Rows.Count != 0)
                {
                    txtClass.Text = dt.Rows[0][5].ToString();
                    txtDate.Text = dt.Rows[0][3].ToString();
                    txtDuration.Text = dt.Rows[0][4].ToString();
                    txtMeetingID.Text = dt.Rows[0][0].ToString();
                    txtMeetingPass.Text = dt.Rows[0][1].ToString();
                    txtTopic.Text = dt.Rows[0][2].ToString();

                    txtName.Text = dt.Rows[0][6].ToString();
                    
                    //meeting_email.Value = "";
                }


            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                //DisplayAlertMsg("Please fill in the blank");
                DisplayAlertMsg(msg);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MeetingList.aspx");
        }


        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }
}
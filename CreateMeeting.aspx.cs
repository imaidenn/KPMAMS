using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class CreateMeeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userGUID"] != null)
            {
                if(IsPostBack == false)
                {
                    GetClass();
                }
                
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateMeeting())
                {
                    if (NewMeeting())
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Meeting created succcess');window.location ='Homepage.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Meeting created fail');window.location ='Homepage.aspx';", true);
                    }
                }



            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected bool NewMeeting()
        {
            try
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.Now;
                var apiSecret = "Np51SIl5XKW9BS1nRkzVHicsMFkWNZfDuGHS";
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "YQ05G1iATHGTD2iFOMV1Kg",
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/users/me/meetings");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { topic = txtTopic.Text, duration = txtDuration.Text, start_time = txtStart.Text, type = "2" });
                request.AddHeader("authorization", "Bearer" + tokenString);

                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                var jObject = JObject.Parse(restResponse.Content);
                Host.Text = (string)jObject["start_url"];
                Join.Text = (string)jObject["join_url"];
                Code.Text = Convert.ToString(numericStatusCode);

                string meetID = Join.Text.Substring(26);
                meetID = meetID.Substring(0,11);
                string meetCode = Join.Text.Substring(42);


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Meeting VALUES ('" + Guid.NewGuid() + "',@TeacherGUID,@ClassroomGUID,@Topic,@RoomID,@RoomPass,@MeetingTime,@Duration,'Active',@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                CultureInfo culture = new CultureInfo("ms-MY");
                cmdInsert.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"].ToString());
                cmdInsert.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@Topic", txtTopic.Text);
                cmdInsert.Parameters.AddWithValue("@RoomID", meetID);
                cmdInsert.Parameters.AddWithValue("@RoomPass", meetCode);
                cmdInsert.Parameters.AddWithValue("@MeetingTime", Convert.ToDateTime(txtStart.Text, culture));
                cmdInsert.Parameters.AddWithValue("@Duration", txtDuration.Text);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return false;
            }
        }

        protected bool ValidateMeeting()
        {
            int i;
            bool isNumeric = int.TryParse(txtDuration.Text, out i);

            if(txtTopic.Text == "")
            {
                DisplayAlertMsg("Please enter Meeting Topic");
                return false;
            }

            if (txtDuration.Text == "")
            {
                DisplayAlertMsg("Please enter Meeting Duration");
                return false;
            }

            if (txtStart.Text == "")
            {
                DisplayAlertMsg("Please enter Meeting Date");
                return false;
            }

            if (!isNumeric)
            {
                DisplayAlertMsg("Please enter only digit for the duration minute");
                return false;
            }

            if (int.Parse(txtDuration.Text) > 120)
            {
                DisplayAlertMsg("Duration of the meeting cannot more than 120 minutes");
                return false;
            }

            if (int.Parse(txtDuration.Text) < 5)
            {
                DisplayAlertMsg("Duration of the meeting cannot less than 10 minutes");
                return false;
            }
            return true;
        }

        protected void GetClass()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT b.Class, a.ClassroomGUID FROM Teacher_Classroom a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                        "WHERE a.TeacherGUID = @TeacherGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"].ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlClass.DataTextField = dt.Columns["Class"].ToString();   
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString();
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
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
    }
}
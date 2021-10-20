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
    public partial class user : System.Web.UI.MasterPage
    {
        String userGUID = "";
        String fullname = "";
        String role = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(Session["userGUID"] != null || Session["fullName"] != null || Session["role"] != null)
                {
                    userGUID = Session["userGUID"].ToString();
                    fullname = Session["fullName"].ToString();
                    role = Session["role"].ToString();
                    LoadData();
                }
                else
                {
                    Response.Write("<script>alert('Please login first!');</script>");
                    Response.Redirect("Login.aspx");
                }

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.ToString());
            }

            String activePage = Request.RawUrl;
            if (activePage.Contains("ForumList.aspx"))
            {
                //hlForum.Attributes.Add("class", "nav-link active");
            }
        }

        protected void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "";

                if (role == "Student")
                {
                    strSelect = "SELECT StudentGUID, ProfilePic FROM Student WHERE StudentGUID = @userGUID";
                    uploadResult.Visible = false;
                }
                else if (role == "Teacher")
                {
                    strSelect = "SELECT TeacherGUID, ProfilePic FROM Teacher WHERE TeacherGUID = @userGUID";
                    resultDetails.Visible = false;  
                }
                else
                {
                    strSelect = "SELECT ParentGUID, ProfilePic FROM Parent WHERE ParentGUID = @userGUID";
                }



                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@userGUID", userGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {

                    String image = "";
                    if (dt.Rows[0][1] != null)
                    {
                        image = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dt.Rows[0][1].ToString();
                    }
                    imgAvatar.ImageUrl = image;
                    lblUser.Text = "Welcome, " + fullname;
                }
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.ToString());
            }
            
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            try
            {
                string strRedirect;
                strRedirect = Request["ReturnUrl"];
                if (role == "Student")
                {
                    strRedirect = "StudentDetails.aspx";
                }
                else if (role == "Teacher")
                {
                    strRedirect = "TeacherDetails.aspx";
                }
                else
                {
                    strRedirect = "ParentDetails.aspx";
                }
                string content = strRedirect + "?userGUID=" + userGUID;
                Response.Redirect(content);
                
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.ToString());
            }
        }
        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string strRedirect = "ForgotPassword.aspx";
                string content = strRedirect + "?UserGUID=" + userGUID + "&UserType=" + role + "&ResetPasswordGUID=00000000-0000-0000-0000-000000000000";
                Response.Redirect(content);

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.ToString());
            }
        }
    }

    
}
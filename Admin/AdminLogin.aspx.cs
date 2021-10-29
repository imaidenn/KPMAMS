using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtUserID.Text != "" || txtPassword.Text != "")
            {
                if (txtUserID.Text == "admin" && txtPassword.Text == "asdf1234")
                {

                    //string strRedirect;
                    //strRedirect = Request["ReturnUrl"];
                    //if (strRedirect == null)
                    //{
                    //    if (userType.Equals("Teacher"))
                    //        strRedirect = "TeacherHomepage.aspx";
                    //    else if (userType.Equals("Parent"))
                    //        strRedirect = "ParentHomepage.aspx";
                    //    else if (userType.Equals("Student"))
                    //        strRedirect = "StudentHomepage.aspx";
                    //    else
                    //        strRedirect = "Login.aspx";

                    //}
                    Session["role"] = "Admin";
                    System.Threading.Thread.Sleep(2000);
                    Response.Redirect("AdminHomepage.aspx", true);
                }
                else
                {
                    DisplayAlertMsg("Wrong ID or password.");
                }

            }
            else
            {
                DisplayAlertMsg("Please enter your ID and password.");
            }


        }

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }
    }
}
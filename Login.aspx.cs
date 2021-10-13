using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class Login : System.Web.UI.Page
    {
        String userType = "";
        Guid userGuid = new Guid();
        String username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userGUID"] != null || Session["fullName"] != null || Session["role"] != null)
            {
                Session["userGUID"] = null;
                Session["fullName"] = null;
                Session["role"] = null;
            }
            
        }

        protected bool ValidateUser(String userID, String password)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            String userPassword = "";
            String encryptedPassword = "";

            try
            {

                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    con = new SqlConnection(strCon);
                    con.Open();

                    if (userID.Substring(0, 1) == "t")
                    {
                        cmd = new SqlCommand("SELECT * from Teacher where TeacherUserID= @UserID", con);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        userType = "Teacher";
                    }
                    else if (userID.Substring(0, 1) == "p")
                    {
                        cmd = new SqlCommand("SELECT * from Parent where ParentUserID= @UserID", con);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        userType = "Parent";

                    }
                    else if (userID.Substring(0, 1) == "s")
                    {
                        cmd = new SqlCommand("SELECT * from Student where StudentUserID= @UserID", con);
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        userType = "Student";
 
                    }
                else
                {
                    return false;
                }


                    SqlDataReader dtrSelect = cmd.ExecuteReader();

                    dt.Load(dtrSelect);

                    //get password
                    encryptedPassword = dt.Rows[0]["Password"].ToString();

                if (userType == "Teacher")
                    userGuid = (Guid)dt.Rows[0]["TeacherGUID"];
                else if (userType == "Parent")
                    userGuid = (Guid)dt.Rows[0]["ParentGUID"];
                else
                    userGuid = (Guid)dt.Rows[0]["StudentGUID"];

                username = dt.Rows[0]["Fullname"].ToString();


                cmd.Dispose();
                    con.Close();
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

            if (encryptedPassword != null)
            {
                if (!(encryptedPassword.Equals("")))
                {
                    userPassword = Decrypt(encryptedPassword);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return (String.Compare(userPassword, password, false) == 0);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

                if (txtUserID.Text != "" || txtPassword.Text != "")
                {
                    if (ValidateUser(txtUserID.Text, txtPassword.Text))
                    {

                    string strRedirect;
                    strRedirect = Request["ReturnUrl"];
                    if (strRedirect == null)
                    {
                        if (userType.Equals("Teacher") || userType.Equals("Student") || userType.Equals("Parent"))
                            strRedirect = "Homepage.aspx";
                       
                        else
                            strRedirect = "Login.aspx";

                    }
                    Session["userGUID"] = userGuid.ToString();
                    Session["fullName"] = username;
                    Session["role"] = userType;
                    System.Threading.Thread.Sleep(2000);
                    Response.Redirect(strRedirect, true);
                    }
                else
                {
                    DisplayAlertMsg("User cannot found. Please enter correct ID and Password.");
                }

                }
            else
            {
                DisplayAlertMsg("Please enter your ID and password.");
            }

             
            }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        protected void DisplayAlertMsg(string msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }


    }


}

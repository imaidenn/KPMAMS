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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        Guid ResetPasswordGUID = new Guid();
        Guid UserGUID = new Guid();
        string UserType = "";
        string alertMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserGUID"] == null || Request.QueryString["ResetPasswordGUID"] == null || Request.QueryString["UserType"] == null)
            {
                Response.Redirect("Login.aspx");

            }
            else
            {
                UserType = Request.QueryString["UserType"];
                if (!Guid.TryParse(Request.QueryString["ResetPasswordGUID"], out ResetPasswordGUID) || !Guid.TryParse(Request.QueryString["UserGUID"], out UserGUID))
                {
                    Response.Redirect("Login.aspx");
                }
            }
            if (!IsPostBack)
            {

                if (!CheckResetPasswordStatus())
                {
                    Response.Write(alertMsg);
                    //Response.Redirect("Homepage.aspx");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ResetPassword() == true)
            {
                Response.Write("<script language='javascript'>window.alert('Password change successful!');window.location='Login.aspx';</script>");
            }
        }

        protected bool CheckResetPasswordStatus()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();
                String strSelect = "";

                if (UserType == "Student")
                {
                    strSelect = "SELECT ResetPasswordGUID, StudentGUID, Status FROM ResetPassword WHERE ResetPasswordGUID = @ResetPasswordGUID";
                }
                else if (UserType == "Teacher")
                {
                    strSelect = "SELECT ResetPasswordGUID, TeacherGUID, Status FROM ResetPassword WHERE ResetPasswordGUID = @ResetPasswordGUID";
                }
                else if (UserType == "Parent")
                {
                    strSelect = "SELECT ResetPasswordGUID, ParentGUID, Status FROM ResetPassword WHERE ResetPasswordGUID = @ResetPasswordGUID";
                }

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                cmdSelect.Parameters.AddWithValue("@ResetPasswordGUID", ResetPasswordGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();
                dt.Load(dtrSelect);

                if (dt.Rows.Count == 0)
                {
                    //DisplayAlertMsg("Invalid reset password request, please request for reset password at the login page");
                    alertMsg = "<script language='javascript'>window.alert('Invalid reset password request, please request for reset password at the login page');window.location='Login.aspx';</script>";
                    return false;
                }
                else if (dt.Rows[0]["Status"].Equals("Active") == false)
                {
                    //DisplayAlertMsg("The link has been used for reset password");
                    alertMsg = "<script language='javascript'>window.alert('The link has been used for reset password');window.location='Login.aspx';</script>";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return false;
            }
        }

        protected bool ResetPassword()
        {
            bool ResetBool = false;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            String encryptedPassword = "";
            try
            {
                String password = txtNewPassword.Text;
                String confirmPassword = txtConfirmPassword.Text;

                if (!password.Equals("") || !confirmPassword.Equals(""))
                {
                    if (password.Equals(confirmPassword))
                    {

                        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        con = new SqlConnection(strCon);
                        con.Open();
                        encryptedPassword = Encrypt(password);

                        if (UserType == "Student")
                        {
                            cmd = new SqlCommand("UPDATE Student SET Password = @Password WHERE StudentGUID = @StudentGUID", con);
                            cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                            cmd.Parameters.AddWithValue("@StudentGUID", UserGUID);
                        }
                        else if (UserType == "Teacher")
                        {
                            cmd = new SqlCommand("UPDATE Teacher SET Password = @Password WHERE TeacherGUID = @TeacherGUID", con);
                            cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                            cmd.Parameters.AddWithValue("@TeacherGUID", UserGUID);
                        }
                        else if (UserType == "Parent")
                        {
                            cmd = new SqlCommand("UPDATE Parent SET Password = @Password WHERE ParentGUID = @ParentGUID", con);
                            cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                            cmd.Parameters.AddWithValue("@ParentGUID", UserGUID);
                        }
                        

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ResetBool = true;
                        }

                        cmd.Dispose();

                        cmd = new SqlCommand("UPDATE ResetPassword SET Status = 'Expired' WHERE ResetPasswordGUID = @ResetPasswordGUID", con);
                        cmd.Parameters.AddWithValue("ResetPasswordGUID", ResetPasswordGUID);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        con.Close();
                    }
                    else
                    {
                        DisplayAlertMsg("Password does not match!");
                    }
                }
                else
                {
                    DisplayAlertMsg("Please fill in all the blank!");
                }
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
            return ResetBool;
        }

        protected void DisplayAlertMsg(String msg)
        {
            string myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
        }

        protected string Encrypt(string cipherText)
        {
            string EncryptionKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            byte[] clearBytes = Encoding.Unicode.GetBytes(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cipherText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}

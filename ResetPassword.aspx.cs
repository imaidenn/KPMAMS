using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
	public partial class ResetPassword : System.Web.UI.Page
	{
		String userType = "";
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnReset_Click(object sender, EventArgs e)
		{
			try
			{
				if (!txtEmail.Text.Equals("") && !txtUserID.Text.Equals(""))
				{
					if (SendEmail(txtUserID.Text, txtEmail.Text))
					{

						DisplayAlertMsg("Success! If your email address exists in our database, you will receive a password recovery link at your email address in a few minutes.");
					}
					else
					{
						DisplayAlertMsg("Please enter correct user ID and email to reset the password");
					}
				}
                else
                {
					DisplayAlertMsg("Please enter your user ID and email before proceed.");
				}
			}
			catch (Exception ex)
			{

				DisplayAlertMsg(ex.Message);

			}
		}

		protected bool SendEmail(String userID, String email)
		{
			bool sentBoolean = false;
			DataTable dt = new DataTable();
			SqlConnection con = new SqlConnection();
			SqlCommand cmd = new SqlCommand();
			try
			{
				SmtpClient client = new SmtpClient();
				client.Port = 587;
				client.Host = "smtp.gmail.com";
				client.EnableSsl = true;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential("kpmnoreply@gmail.com", "kpm12345");

				MailMessage mail = new MailMessage();
				mail.From = new MailAddress("kpmnoreply@gmail.com");

				string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
				con = new SqlConnection(strCon);
				con.Open();

				if (userID.Substring(0, 1) == "t")
				{
					cmd = new SqlCommand("SELECT TeacherGUID, Fullname, Email FROM Teacher WHERE Email = @Email AND TeacherUserID = @UserID", con);
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@UserID", userID);
					userType = "Teacher";
				}
				else if (userID.Substring(0, 1) == "p")
				{
					cmd = new SqlCommand("SELECT ParentGUID, Fullname, Email FROM Parent WHERE Email = @Email AND ParentUserID = @UserID", con);
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@UserID", userID);
					userType = "Parent";

				}
				else if (userID.Substring(0, 1) == "s")
				{
					cmd = new SqlCommand("SELECT StudentGUID, Fullname, Email FROM Student WHERE Email = @Email AND StudentUserID = @UserID", con);
					cmd.Parameters.AddWithValue("@Email", email);
					cmd.Parameters.AddWithValue("@UserID", userID);
					userType = "Student";

				}




				SqlDataReader dtrSelect = cmd.ExecuteReader();

				dt.Load(dtrSelect);

				if (dt.Rows.Count != 0)
				{
					String UserGUID = dt.Rows[0][0].ToString();
					String Username = dt.Rows[0][1].ToString();

					Guid ResetPasswordGUID = Guid.NewGuid();
					String strInsert = "";

						if (userType.Equals("Teacher"))
							strInsert = "INSERT INTO ResetPassword (ResetPasswordGUID, TeacherGUID, Status) VALUES (@ResetPasswordGUID,@UserGUID,@Status)";
						else if (userType.Equals("Parent"))
							strInsert = "INSERT INTO ResetPassword (ResetPasswordGUID, ParentGUID, Status) VALUES (@ResetPasswordGUID,@UserGUID,@Status)";
						else if (userType.Equals("Student"))
							strInsert = "INSERT INTO ResetPassword (ResetPasswordGUID, StudentGUID, Status) VALUES (@ResetPasswordGUID,@UserGUID,@Status)";
						else
							return false;

						SqlCommand cmdInsert = new SqlCommand(strInsert, con);
						cmdInsert.Parameters.AddWithValue("@ResetPasswordGUID", ResetPasswordGUID);
						cmdInsert.Parameters.AddWithValue("@UserGUID", Guid.Parse(UserGUID));
						cmdInsert.Parameters.AddWithValue("@Status", "Active");

						cmdInsert.ExecuteNonQuery();
						cmdInsert.Dispose();

						String url = ConfigurationManager.AppSettings["ResetPasswordURL"].ToString() + UserGUID + "&ResetPasswordGUID=" + ResetPasswordGUID.ToString() + "&UserType=" + userType;
						String body = "<b>Hello " + Username + "</b><br />You recently requested to reset your password for your KPM account. Use the URL link below to change it.<br /><br />URL link: <a href= '" + url + "'><b>Reset Your Password<b></a><br />If you didn't request this, please ignore this email. Your password won't change until you access the link above and create a new one.";
						mail.To.Add(email);
						mail.Subject = "Reset Password";
						mail.IsBodyHtml = true;
						mail.Body = body;

						client.Send(mail);

						sentBoolean = true;


					con.Close();
					return sentBoolean;
				}
			}
			catch (Exception ex)
			{
				DisplayAlertMsg(ex.Message);
				return false;
			}
			return false;
		}

		protected void DisplayAlertMsg(string msg)
		{
			string myScript = String.Format("alert('{0}');", msg);
			ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS.Admin
{
    public partial class TeacherEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["TeacherGUID"] != null)
                {
                    LoadExistingData();
                }
                else
                {
                    GetUserID();

                }

            }
        }

        protected void LoadExistingData()
        {
            try
            {
                String TeacherGUID = Request.QueryString["TeacherGUID"];
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, ICNo, ProfilePic, Gender, PhoneNo, Email, Address, Status, CONVERT(varchar,DateOfBirth) as BirthDate, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher WHERE TeacherGUID = @TeacherGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherGUID", TeacherGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    imageUpload.Enabled = false;
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    txtJoinDate.Enabled = false;
                    txtBirthDate.Enabled = false;
                    ddlGender.Enabled = false;
                    txtICno.Enabled = false;

                    txtTeacherID.Text = dt.Rows[0][1].ToString();
                    txtName.Text = dt.Rows[0][2].ToString();
                    txtICno.Text = dt.Rows[0][3].ToString();
                    txtEmail.Text = dt.Rows[0][7].ToString();
                    txtAddress.Text = dt.Rows[0][8].ToString();
                    txtBirthDate.Text = dt.Rows[0][10].ToString();
                    txtJoinDate.Text = dt.Rows[0][11].ToString();
                    txtPhoneNo.Text = dt.Rows[0][6].ToString();

                    String gender = dt.Rows[0][5].ToString();
                    ddlGender.SelectedValue = gender;


                    String status = dt.Rows[0][9].ToString();
                    ddlStatus.SelectedValue = status;

                    String image = "";
                    if (dt.Rows[0][4] != null)
                    {
                        image = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dt.Rows[0][4].ToString();
                    }
                    imgProfile.ImageUrl = image;
                }
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                //DisplayAlertMsg("Please fill in the blank");
                DisplayAlertMsg(msg);
            }
        }


        protected void GetUserID()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT TeacherUserID FROM Teacher ORDER BY TeacherUserID DESC ";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if(dt.Rows.Count > 0)
                {
                    String newTeacherID = dt.Rows[0][0].ToString();
                    String number = "";
                    foreach (char c in newTeacherID)
                    {
                        if (Char.IsNumber(c))
                        {
                            number += c;
                        }
                    }
                    txtTeacherID.Text = "t" + (int.Parse(number) + 1).ToString();
                }
                else
                {
                    txtTeacherID.Text = "t1001";
                }
                

                con.Close();
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }

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

        protected bool NewTeacher()
        {
            bool addBool = false;
            try
            {
                String password = Encrypt(txtICno.Text);
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime birthDate = DateTime.ParseExact(txtBirthDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Guid imgGuid = Guid.NewGuid();
                string ProfileImgName = "Profile_" + imgGuid.ToString() + Path.GetExtension(imageUpload.FileName);
                string ProfileImgUploadPath = ConfigurationManager.AppSettings.Get("ProfileUploadPath");
                string ProfileImgSavePath = Server.MapPath(ProfileImgUploadPath);

                string ProfileFullSavePath = ProfileImgSavePath + ProfileImgName;

                imageUpload.PostedFile.SaveAs(ProfileFullSavePath);


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Teacher(TeacherGUID,Password,TeacherUserID,FullName,ICNo,Email,PhoneNo,Address,JoinDate,DateOfBirth,Gender,Status,ProfilePic,CreateDate,LastUpdateDate) " +
                    "VALUES ('" + Guid.NewGuid() + "',@Password,@TeacherID,@TeacherName,@TeacherIC,@Email,@PhoneNo,@Address,@JoinDate,@BirthDate,@Gender,@status,@ProfilePic,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@Password", password);
                cmdInsert.Parameters.AddWithValue("@TeacherID", txtTeacherID.Text);
                cmdInsert.Parameters.AddWithValue("@TeacherName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@TeacherIC", txtICno.Text);
                cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmdInsert.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmdInsert.Parameters.AddWithValue("@JoinDate", joinDate);
                cmdInsert.Parameters.AddWithValue("@BirthDate", birthDate);
                cmdInsert.Parameters.AddWithValue("@Gender", ddlGender.SelectedItem.Text);
                cmdInsert.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text);
                cmdInsert.Parameters.AddWithValue("@ProfilePic", ProfileImgName);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }


        protected bool ValidateAdd()
        {
            if (txtName.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher name");
                return false;
            }
            if (txtAddress.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher address");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher email");
                return false;
            }
            if (txtICno.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher IC number");
                return false;
            }
            if (txtPhoneNo.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher phone number");
                return false;
            }

            if (!(imageUpload.HasFile))
            {
                DisplayAlertMsg("Please upload an image of Teacher profile");
                return false;
            }

            int n;
            bool isNumeric = int.TryParse(txtICno.Text, out n);

            if (!isNumeric)
            {
                DisplayAlertMsg("Please enter only number of the IC");
                return false;
            }

            int i;
            bool isNumeric2 = int.TryParse(txtPhoneNo.Text, out i);

            if (!isNumeric2)
            {
                DisplayAlertMsg("Please enter only number of the phone");
                return false;
            }

            if(txtPhoneNo.Text.Length > 11)
            {
                DisplayAlertMsg("Invalid phone number");
                return false;
            }

            if (txtICno.Text.Length > 12)
            {
                DisplayAlertMsg("Invalid IC number");
                return false;
            }

            if (!(Validateemail(txtEmail.Text)))
            {
                DisplayAlertMsg("Invalid email");
                return false;
            }


            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAdd())
            {
                if (NewTeacher())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Teacher added succcess');window.location ='TeacherListing.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Teacher added failed');window.location ='TeacherListing.aspx';", true);
                }
            }
        }

        protected bool UpdateTeacher()
        {
            bool updateBool = false;

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Teacher SET FullName=@FullName,Email=@Email,PhoneNo=@PhoneNo,Address=@Address,Status=@Status WHERE TeacherGUID='" + Guid.Parse(Request.QueryString["TeacherGUID"]) + "'";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@FullName", txtName.Text);
                cmdUpdate.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdUpdate.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmdUpdate.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmdUpdate.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Text);

                cmdUpdate.ExecuteNonQuery();

                con.Close();
                updateBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }

            return updateBool;
        }

        protected bool ValidateUpdate()
        {
            if (txtName.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher name");
                return false;
            }
            if (txtAddress.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher address");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher email");
                return false;
            }
            if (txtPhoneNo.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Teacher phone number");
                return false;
            }

            int i;
            bool isNumeric2 = int.TryParse(txtPhoneNo.Text, out i);

            if (!isNumeric2)
            {
                DisplayAlertMsg("Please enter only number of the phone");
                return false;
            }

            if (txtPhoneNo.Text.Length > 11)
            {
                DisplayAlertMsg("Invalid phone number");
                return false;
            }

            if (!(Validateemail(txtEmail.Text)))
            {
                DisplayAlertMsg("Invalid email");
                return false;
            }

            return true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate())
            {
                if (UpdateTeacher())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Teacher succcessful');window.location ='TeacherListing.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Teacher failed');window.location ='TeacherListing.aspx';", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherListing.aspx");
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected bool Validateemail(String email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                bool IsValid = (address.Address == email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
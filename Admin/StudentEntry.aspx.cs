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
    public partial class StudentEntry : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            GetClass();
            if (IsPostBack == false)
            {
                if (Request.QueryString["StudentGUID"] != null)
                {
                    lbCheckParent.Visible = false;
                    LoadExistingData();
                }
                else
                {
                    GetUserID();
                    Session["checkParent"] = false;
                    
                }

            }
        }

        protected void LoadExistingData()
        {
            try
            {
                String StudentGUID = Request.QueryString["StudentGUID"];
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = 
                    "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.ICNo, a.ProfilePic, a.Gender, a.PhoneNo, a.Email, a.Address, b.Class, a.Status, CONVERT(varchar,a.DateOfBirth) as BirthDate, CONVERT(varchar,a.JoinDate,1) as JoinDate,c.icNo,c.fullName " +
                    "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                    "LEFT JOIN Parent c ON a.ParentGUID=c.ParentGUID " +
                    "WHERE a.StudentGUID = @StudentGUID";
                           
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", StudentGUID);

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
                    txtParentIC.Enabled = false;
                    txtParentName.Enabled = false;

                    txtStudentID.Text = dt.Rows[0][1].ToString();
                    txtName.Text = dt.Rows[0][2].ToString();
                    txtICno.Text = dt.Rows[0][3].ToString();
                    txtEmail.Text = dt.Rows[0][7].ToString();
                    txtAddress.Text = dt.Rows[0][8].ToString();
                    txtBirthDate.Text = dt.Rows[0][11].ToString();
                    txtJoinDate.Text = dt.Rows[0][12].ToString();
                    txtPhoneNo.Text = dt.Rows[0][6].ToString();
                    txtParentIC.Text = dt.Rows[0][13].ToString();
                    txtParentName.Text = dt.Rows[0][14].ToString();


                    String gender = dt.Rows[0][5].ToString();
                    ddlGender.SelectedValue = gender;

                    String classroom = dt.Rows[0][9].ToString();

                    String status = dt.Rows[0][10].ToString();
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
                DisplayAlertMsg("Error when displaying the existing data");
            }
        }

        protected void GetClass()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ClassroomGUID, Class FROM Classroom";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlClass.DataTextField = dt.Columns["Class"].ToString(); // text field name of table dispalyed in dropdown       
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString();
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
                }


                con.Close();
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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

                String strSelect = "SELECT StudentUserID FROM Student ORDER BY StudentUserID DESC ";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    String newstudentID = dt.Rows[0][0].ToString();
                    String number = "";
                    foreach (char c in newstudentID)
                    {
                        if (Char.IsNumber(c))
                        {
                            number += c;
                        }
                    }
                    txtStudentID.Text = "s" + (int.Parse(number) + 1).ToString();
                }
                else
                {
                    txtStudentID.Text = "s1001";
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            
        }


        protected void GetParentUserID()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ParentUserID FROM Parent ORDER BY ParentUserID DESC ";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    String newparentID = dt.Rows[0][0].ToString();
                    String number = "";
                    foreach (char c in newparentID)
                    {
                        if (Char.IsNumber(c))
                        {
                            number += c;
                        }
                    }
                    Session["ParentUserID"] = "p" + (int.Parse(number) + 1).ToString();
                }
                else
                {
                    Session["ParentUserID"] = "p1001";
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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

        protected bool NewStudent()
        {
            bool addBool = false;
            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);
                con.Open();
                String strInsert = "";
                if (Session["ParentGUID"].Equals(""))
                {
                    GetParentUserID();
                    Session["ParentGUID"] = Guid.NewGuid();
                    strInsert = 
                        "INSERT INTO Parent(ParentGUID,ParentUserID,Password,FullName,Status,CreateDate,ICNo) " +
                        "VALUES (@ParentGUID,@ParentUserID,@Password,@FullName,@Status,@CreateDate,@ICNo)";
                    SqlCommand cmd = new SqlCommand(strInsert, con);
                    cmd.Parameters.AddWithValue("@ParentGUID",Session["ParentGUID"]);
                    cmd.Parameters.AddWithValue("@ParentUserID", Session["ParentUserID"]);
                    cmd.Parameters.AddWithValue("@Password", Encrypt(txtParentIC.Text.Trim()));
                    cmd.Parameters.AddWithValue("@FullName", txtParentName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ICNo", txtParentIC.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                String password = Encrypt(txtICno.Text);
                DateTime joinDate = DateTime.ParseExact(txtJoinDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime birthDate = DateTime.ParseExact(txtBirthDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Guid imgGuid = Guid.NewGuid();
                string ProfileImgName = "Profile_" + imgGuid.ToString() + Path.GetExtension(imageUpload.FileName);
                string ProfileImgUploadPath = ConfigurationManager.AppSettings.Get("ProfileUploadPath");
                string ProfileImgSavePath = Server.MapPath(ProfileImgUploadPath);

                string ProfileFullSavePath = ProfileImgSavePath + ProfileImgName;

                imageUpload.PostedFile.SaveAs(ProfileFullSavePath);


                strInsert = "INSERT INTO Student(StudentGUID,Password,StudentUserID,FullName,ICNo,Email,PhoneNo,Address,JoinDate,DateOfBirth,Gender,Status,ProfilePic,ClassroomGUID,CreateDate,LastUpdateDate,ParentGUID) " +
                    "VALUES ('" + Guid.NewGuid() + "',@Password,@StudentID,@StudentName,@StudentIC,@Email,@PhoneNo,@Address,@JoinDate,@BirthDate,@Gender,@status,@ProfilePic,@ClassroomGUID,@CreateDate,@LastUpdateDate,@ParentGUID)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@Password", password);
                cmdInsert.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
                cmdInsert.Parameters.AddWithValue("@StudentName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@StudentIC", txtICno.Text);
                cmdInsert.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmdInsert.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmdInsert.Parameters.AddWithValue("@JoinDate", joinDate);
                cmdInsert.Parameters.AddWithValue("@BirthDate", birthDate);
                cmdInsert.Parameters.AddWithValue("@Gender", ddlGender.SelectedItem.Text);
                cmdInsert.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Text);
                cmdInsert.Parameters.AddWithValue("@ProfilePic", ProfileImgName);
                cmdInsert.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue.ToString());
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@ParentGUID", Session["ParentGUID"]);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return addBool;
        }


        protected bool ValidateAdd()
        {
            if (txtName.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student name");
                return false;
            }
            if (txtAddress.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student address");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student email");
                return false;
            }
            if (txtICno.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student IC number");
                return false;
            }
            if (txtPhoneNo.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student phone number");
                return false;
            }

            if (!(imageUpload.HasFile))
            {
                DisplayAlertMsg("Please upload an image of student profile");
                return false;
            }

            if (Session["checkParent"].Equals(false)) {
                DisplayAlertMsg("Please check the parent ic first");
                return false;
            }

            if (txtParentName.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter parent name");
                return false;
            }

            if (!(checkNo(txtICno.Text)))
            {
                DisplayAlertMsg("Plases enter number only IC");
                return false;
            }

            int i;
            bool isNumeric2 = int.TryParse(txtPhoneNo.Text, out i);

            if (!isNumeric2)
            {
                DisplayAlertMsg("Please enter only number of the phone");
                return false;
            }

            if (!(ValidatephoneNo(txtPhoneNo.Text)))
            {
                DisplayAlertMsg("Invalid phone number");
                return false;
            }

            if (txtICno.Text.Length != 12)
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

        private bool ValidatephoneNo(string phoneNo)
        {
            string pattern = @"^(\+?6?01)[02-46-9]-*[0-9]{7}$|^(\+?6?01)[1]-*[0-9]{8}$";
            Match m = Regex.Match(phoneNo, pattern, RegexOptions.IgnoreCase);
            if (m.Success)
            {
                return true;
            }
            return false;
        }

        private bool checkNo(string number)
        {
            string pattern = @"^[0-9]+$";
            Match m = Regex.Match(number, pattern, RegexOptions.IgnoreCase);
            if (m.Success)
            {
                return true;
            }
            return false;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAdd())
            {
                if (NewStudent())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Student added succcess');window.location ='StudentListing.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Student added failed');window.location ='StudentListing.aspx';", true);
                }
            }
        }

        protected bool UpdateStudent()
        {
            bool updateBool = false;

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Student SET FullName=@FullName,Email=@Email,PhoneNo=@PhoneNo,Address=@Address,Status=@Status,ClassroomGUID=@ClassroomGUID,LastUpdateDate=@LastUpdateDate WHERE StudentGUID='" + Guid.Parse(Request.QueryString["StudentGUID"]) + "'";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@FullName", txtName.Text);
                cmdUpdate.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdUpdate.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmdUpdate.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmdUpdate.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Text);
                cmdUpdate.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue.ToString());
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

                con.Close();
                updateBool = true;
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            return updateBool;
        }

        protected bool ValidateUpdate()
        {
            if (txtName.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student name");
                return false;
            }
            if (txtAddress.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student address");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student email");
                return false;
            }
            if (txtPhoneNo.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the student phone number");
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
                if (UpdateStudent())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update student succcessful');window.location ='StudentListing.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update student failed');window.location ='StudentListing.aspx';", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentListing.aspx");
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

        protected bool checkParent()
        {
            if (txtParentIC.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Parent IC number");
                return false;
            }
            else if (txtParentIC.Text.Length != 12) {
                DisplayAlertMsg("Please enter valid ic number");
                return false;
            }
            else {
                if (!(checkNo(txtParentIC.Text)))
                {
                    DisplayAlertMsg("Plases enter number only IC");
                    return false;
                }
            }
            return true;
        }




        protected void lbCheckParent_Click(object sender, EventArgs e)
        {
            Session["ParentGUID"] = "";
            txtParentName.Text = "";
            Session["checkParent"] = false;
            if (lbCheckParent.Text == "Cancel")
            {
                txtParentIC.Text = "";
                lbCheckParent.Text = "Check";
                txtParentIC.Enabled = true;
                txtParentName.Enabled = false;
            }
            else {
                if (checkParent())
                {
                    try
                    {
                        DataTable dt = new DataTable();

                        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(strCon);

                        con.Open();

                        String strSelect =
                            "SELECT fullName,ParentGUID " +
                            "FROM Parent " +
                            "WHERE IcNo=@IcNO";

                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        cmdSelect.Parameters.AddWithValue("@IcNO", txtParentIC.Text);

                        SqlDataReader dr = cmdSelect.ExecuteReader();

                        dt.Load(dr);

                        con.Close();

                        if (dt.Rows.Count > 0)
                        {
                            txtParentName.Text = dt.Rows[0][0].ToString();
                            DisplayAlertMsg("Parent is exist in the database");
                            Session["ParentGUID"] = dt.Rows[0][1].ToString();
                        }
                        else
                        {
                            DisplayAlertMsg("Parent not exist in the database, please enter parent name");
                            txtParentName.Enabled = true;

                        }
                        txtParentIC.Enabled = false;
                        Session["checkParent"] = true;
                        lbCheckParent.Text = "Cancel";
                    }
                    catch (SqlException ex)
                    {
                        string msg = ex.Message;
                        DisplayAlertMsg("Error to check parent");
                    }
                }
            }
            
        }
    }
}
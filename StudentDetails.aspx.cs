using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["userGUID"] != null)
                {
                    LoadExistingData();
                }

            }
        }

        protected void LoadExistingData()
        {
            try
            {
                String StudentGUID = Request.QueryString["userGUID"];
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = 
                    "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.ICNo, a.ProfilePic, a.Gender, a.PhoneNo, a.Email, a.Address, b.Class, CONVERT(varchar,a.DateOfBirth) as BirthDate, CONVERT(varchar,a.JoinDate,1) as JoinDate,c.ParentUserID,c.fullName " +
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
                    txtStudentID.Text = dt.Rows[0][1].ToString();
                    txtName.Text = dt.Rows[0][2].ToString();
                    txtICno.Text = dt.Rows[0][3].ToString();
                    txtEmail.Text = dt.Rows[0][7].ToString();
                    txtAddress.Text = dt.Rows[0][8].ToString();
                    txtBirthDate.Text = dt.Rows[0][10].ToString();
                    txtJoinDate.Text = dt.Rows[0][11].ToString();
                    txtPhoneNo.Text = dt.Rows[0][6].ToString();
                    txtParentID.Text = dt.Rows[0][12].ToString();
                    txtParentName.Text = dt.Rows[0][13].ToString();
                    txtGender.Text = dt.Rows[0][5].ToString();

                    txtClass.Text = dt.Rows[0][9].ToString();

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

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate())
            {
                if (UpdateStudent())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update succcessful');window.location ='Homepage.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update failed');window.location ='Homepage.aspx';", true);
                }
            }
        }

        protected bool ValidateUpdate()
        {
            if (txtAddress.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the address");
                return false;
            }
            if (txtEmail.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the email");
                return false;
            }
            if (txtPhoneNo.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the phone number");
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

        protected bool UpdateStudent()
        {
            bool updateBool = false;

            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Student SET Email=@Email,PhoneNo=@PhoneNo,Address=@Address,LastUpdateDate=@LastUpdateDate WHERE StudentGUID='" + Guid.Parse(Request.QueryString["UserGUID"]) + "'";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmdUpdate.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmdUpdate.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

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

    }
}
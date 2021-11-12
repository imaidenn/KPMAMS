using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

namespace KPMAMS
{
    public partial class CreateAssessment : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["userGUID"] != null))
            {
                Response.Redirect("Login.aspx");
            }
            if (IsPostBack == false)
            {

                BindClasses();
            }
        }

        protected void BindClasses()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(
                    "SELECT tc.ClassroomGUID, Class " +
                    "FROM Teacher_Classroom tc LEFT JOIN Classroom c ON c.ClassroomGUID = tc.ClassroomGUID " +
                    "WHERE TeacherGUID=@TeacherGUID", con);
                cmd.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    dt.Load(dr);
                    dlClassList.DataTextField = "Class";
                    dlClassList.DataValueField = "ClassroomGUID";
                    dlClassList.DataSource = dt;
                    dlClassList.DataBind();
                    dlClassList.Items.Insert(0, new ListItem("---Please Select---", String.Empty));

                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Error:No Class, can't create forum');</script>");
                    Server.Transfer("AssessmentList.aspx", true);
                }
                con.Close();

            }
            catch (Exception ex)
            {
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Validation();
        }

        protected void Validation()
        {

            if (dlClassList.SelectedValue != String.Empty && tbTitle.Text.Trim() != String.Empty && tbDesc.Text.Trim() != String.Empty)
            {
                createAssessment();
            }
            else
            {
                Response.Write("<script>alert('Please fill all require field');</script>");
            }
        }

        protected void createAssessment()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Guid AssessmentGUID = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Assessment(AssessmentGUID,ClassroomGUID,TeacherGUID,Title,Description,CreateDate,LastUpdateDate,DueDate,[File]) " +
                    "values (@AssessmentGUID,@ClassroomGUID,@TeacherGUID,@Title,@Description,@CreateDate,@LastUpdateDate,@DueDate,@File)", con);
                cmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);
                cmd.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"].ToString());
                cmd.Parameters.AddWithValue("@Title", tbTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", tbDesc.Text.Trim());
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                if (tbDueDate.Text != "")
                {
                    /*DateTime dueDate = DateTime.ParseExact(tbDueDate.Text, "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);*/
                    //DateTime dueDate = DateTime.Parse(tbDueDate.Text);
                    CultureInfo culture = new CultureInfo("ms-MY");
                    DateTime dueDate = Convert.ToDateTime(tbDueDate.Text, culture);

                    cmd.Parameters.AddWithValue("@DueDate", dueDate);
                }
                else {
                    cmd.Parameters.AddWithValue("@DueDate","");
                }

                string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                
                cmd.Parameters.AddWithValue("@File", filename);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                string folderName = "~/Assessment/" + AssessmentGUID + "/";
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }
                if (filename != "")
                {
                    AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename);
                }
                createSubmission(AssessmentGUID);
                Response.Write("<script language='javascript'>alert('Assessment created successfully');</script>");
                Server.Transfer("AssessmentList.aspx", true);
            }
            catch (Exception ex)
            {

            }
        }

        private void createSubmission(Guid assessmentGUID)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Submission(SubmissionGUID, AssessmentGUID, StudentGUID,Status,LastUpdateDate) " +
                    "SELECT newid(),@AssessmentGUID,StudentGUID,'Pending',@LastUpdateDate " +
                    "FROM Student " +
                    "WHERE ClassroomGUID=@ClassroomGUID", con);
                cmd.Parameters.AddWithValue("@AssessmentGUID", assessmentGUID);
                cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void lbClear_Click(object sender, EventArgs e)
        {
            tbDueDate.Text = "";
        }

    }
}
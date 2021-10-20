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
    public partial class AssessmentDetails : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        DateTime lastUpdateDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["userGUID"] != null))
            {
                Response.Redirect("Login.aspx");
            }
            if (IsPostBack == false)
            {
                LoadAssessment();
            }
        }

        private void LoadAssessment()
        {
            try
            {
                if (!(Request.QueryString["AssessmentGUID"] == null))
                {
                    SqlCommand cmd = new SqlCommand();
                    DataTable dt = new DataTable();
                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(strCon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    String select =
                        "SELECT FullName, ProfilePic, cl.Class, Title, Description, convert(VARCHAR(20),a.CreateDate,100), convert(VARCHAR(20),a.LastUpdateDate,100), a.TeacherGUID, convert(VARCHAR(20),DueDate,100),[File] " +
                        "FROM Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID = tc.TeacherGUID " +
                        "LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID " +
                        "LEFT JOIN Assessment a ON cl.ClassroomGUID = a.ClassroomGUID " +
                        "WHERE t.TeacherGUID = a.TeacherGUID AND AssessmentGUID = @AssessmentGUID";
                    cmd = new SqlCommand(select, con);
                    cmd.Parameters.AddWithValue("@AssessmentGUID", Request.QueryString["AssessmentGUID"]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                        con.Close();
                        //Stored to label here
                        lbTitle.Text = dt.Rows[0][3].ToString();
                        lbDesc.Text = dt.Rows[0][4].ToString();
                        lbUserName.Text = "Created by " + dt.Rows[0][0].ToString();
                        if (dt.Rows[0][8].ToString() == "Jan  1 1900 12:00AM")
                        {
                            lbDueDate.Text = "No due date";
                        }
                        else {
                            lbDueDate.Text = "Due date is " +dt.Rows[0][8].ToString();
                        }
                        DateTime createDate = Convert.ToDateTime(dt.Rows[0][5].ToString());
                        DateTime dateNow = DateTime.Now;
                        int totalTime = Convert.ToInt32((dateNow - createDate).TotalDays);
                        String dateFormat = "";
                        if (totalTime >= 365)
                        {
                            totalTime = totalTime / 365;
                            dateFormat = "years";
                        }
                        else if (totalTime >= 30)
                        {
                            totalTime = totalTime / 30;
                            dateFormat = "months";
                        }
                        else if (totalTime >= 7)
                        {
                            totalTime = totalTime / 7;
                            dateFormat = "weeks";
                        }
                        else
                        {
                            if (totalTime <= 0)
                            {
                                totalTime = Convert.ToInt32((dateNow - createDate).TotalMinutes);
                                if (totalTime >= 60)
                                {
                                    totalTime = totalTime / 60;
                                    dateFormat = "hours";
                                }
                                else
                                {
                                    dateFormat = "minutes";
                                }
                            }
                            else
                            {
                                dateFormat = "days";
                            }
                        }
                        divDueDate.Visible = false;
                        lbCreated.Text = "Created " + totalTime + " " + dateFormat + " ago";
                        lbClass.Text = "Class " + dt.Rows[0][2].ToString();
                        lbCreatedDate.Text = "Created " + dt.Rows[0][5].ToString();
                        lbLastUpdate.Text = "Last Update " + dt.Rows[0][6].ToString();
                        lastUpdateDate = Convert.ToDateTime(dt.Rows[0][6].ToString());
                        if (dt.Rows[0][9].ToString() == "")
                        {
                            divFile.Visible = false;
                        }
                        else {
                            hlFile.Attributes["href"] = "*";
                            hlFile.Text = dt.Rows[0][9].ToString();
                            divFile.Visible = true;
                            lbClearFile.Visible = false;
                        }
                        if (dt.Rows[0][1] != null)
                        {
                            String image = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dt.Rows[0][1].ToString();
                            ImgProfilePic.ImageUrl = image;
                        }
                        if (Session["userGUID"].ToString() == dt.Rows[0][7].ToString())
                        {
                            lbMenu.Visible = true;
                        }
                    }
                    else
                    {
                        Response.Redirect("AssessmentList.aspx");
                    }
                }
                else
                {
                    Response.Redirect("AssessmentList.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void lbModify_Click(object sender, EventArgs e)
        {

        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void lbClearFile_Click(object sender, EventArgs e)
        {
        }

        protected void lbClear_Click(object sender, EventArgs e)
        {
            tbDueDate.Text = "";
        }
    }
}
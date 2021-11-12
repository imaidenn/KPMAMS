using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
                ckeckRole();
            }
        }

        private void ckeckRole()
        {
            if (Session["role"].Equals("Teacher"))
            {
                divAssessmentDetails.Attributes["class"] = "col mx-auto";
                divSubmission.Visible = false;
                BindGrivView();
                
            }
            else {
                divSubmissionList.Visible = false;
                LoadSubmission();
                //hide submission list
            
            }
        }

        private void BindGrivView()
        {
            try
            {
                if (lbDueDate.Text == "No due date") {
                    dlStatus.Items.FindByText("Submitted(On time)").Attributes.Add("style", "display:none");
                    dlStatus.Items.FindByText("Late submit").Attributes.Add("style", "display:none");
                }
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String strSelect = "";
                if (dlStatus.SelectedValue == "submitOnTime")
                {
                    strSelect =
                    "SELECT SubmissionGUID, a.FullName, convert(VARCHAR(20),d.LastUpdateDate,100) As LastUpdateDate,d.[File] " +
                    "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID =b.ClassroomGUID " +
                    "LEFT JOIN Assessment c ON b.ClassroomGUID = c.ClassroomGUID " +
                    "LEFT JOIN Submission d ON c.AssessmentGUID = d.AssessmentGUID " +
                    "WHERE d.AssessmentGUID=@AssessmentGUID AND d.Status='Submitted' AND DueDate > d.LastUpdateDate AND a.StudentGUID=d.StudentGUID";
                }else if (dlStatus.SelectedValue == "Submitted")
                {
                    strSelect =
                   "SELECT SubmissionGUID, a.FullName, convert(VARCHAR(20),d.LastUpdateDate,100) As LastUpdateDate,d.[File] " +
                   "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID =b.ClassroomGUID " +
                   "LEFT JOIN Assessment c ON b.ClassroomGUID = c.ClassroomGUID " +
                   "LEFT JOIN Submission d ON c.AssessmentGUID = d.AssessmentGUID " +
                   "WHERE d.AssessmentGUID=@AssessmentGUID AND d.Status='Submitted' AND a.StudentGUID=d.StudentGUID";
                } 
                else if (dlStatus.SelectedValue == "lateSubmit") {

                    strSelect =
                    "SELECT SubmissionGUID, a.FullName, convert(VARCHAR(20),d.LastUpdateDate,100) As LastUpdateDate,d.[File] " +
                    "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID =b.ClassroomGUID " +
                    "LEFT JOIN Assessment c ON b.ClassroomGUID = c.ClassroomGUID " +
                    "LEFT JOIN Submission d ON c.AssessmentGUID = d.AssessmentGUID " +
                    "WHERE d.AssessmentGUID=@AssessmentGUID AND d.Status='Submitted' AND DueDate < d.LastUpdateDate AND a.StudentGUID=d.StudentGUID";
                }
                else 
                {
                    strSelect =
                    "SELECT SubmissionGUID, a.FullName,d.[File], " +
                    "CASE " +
                    "WHEN d.LastUpdateDate!='' THEN '' " +
                    "END AS LastUpdateDate " +
                    "FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID =b.ClassroomGUID " +
                    "LEFT JOIN Assessment c ON b.ClassroomGUID = c.ClassroomGUID " +
                    "LEFT JOIN Submission d ON c.AssessmentGUID = d.AssessmentGUID " +
                    "WHERE d.AssessmentGUID=@AssessmentGUID AND d.Status='Pending' AND a.StudentGUID=d.StudentGUID";
                }
                
                SqlCommand cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@AssessmentGUID", Request.QueryString["AssessmentGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    GvSubmissionList.DataSource = dt;
                    GvSubmissionList.DataBind();
                }
                else
                {
                    lblNoData.Visible = false;
                    GvSubmissionList.DataSource = dt;
                    GvSubmissionList.DataBind();
                }

                con.Close();
            }
            catch (SqlException ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void LoadSubmission()
        {
            try
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
                    "SELECT [File],Status,convert(VARCHAR(20),LastUpdateDate,100),SubmissionGUID " +
                    "FROM Submission " +
                    "WHERE AssessmentGUID=@AssessmentGUID AND StudentGUID=@StudentGUID";
                cmd = new SqlCommand(select, con);
                cmd.Parameters.AddWithValue("@AssessmentGUID", Request.QueryString["AssessmentGUID"]);
                cmd.Parameters.AddWithValue("@StudentGUID", Session["userGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    con.Close();
                    Session["submitGUID"] = dt.Rows[0][3].ToString();
                    //Stored to textbox here
                    if (dt.Rows[0][1].ToString() != "Pending")
                    {
                        string submitFilePath = "~/Assessment/"+ Request.QueryString["AssessmentGUID"] + "/Submission/" + dt.Rows[0][3].ToString() + "/" + dt.Rows[0][0].ToString();
                        hlSubmitFile.Text = dt.Rows[0][0].ToString();
                        hlSubmitFile.Attributes["href"] = ResolveUrl(submitFilePath);
                        btnSubmit.Text = "Unsumit";
                        btnSubmit.Attributes["class"] = "btn btn-block btn-danger btn-sm";
                        lbSubmitDate.Text = "Submitted " + dt.Rows[0][2].ToString();
                    }
                    else {
                        //show insert file
                        hlSubmitFile.Visible = false;
                        AsyncFileUpload2.Visible = true;
                    }
                }
                else
                {
                    Response.Write("Error to load submission");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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
                        Session["currentDueDate"] = dt.Rows[0][8].ToString();
                        if (dt.Rows[0][8].ToString() == "Jan  1 1900 12:00AM")
                        {
                            lbDueDate.Text = "No due date";
                            dlStatus.Items.FindByText("Submitted(On time)").Attributes.Add("style", "display:none");
                            dlStatus.Items.FindByText("Late submit").Attributes.Add("style", "display:none");
                        }
                        else {
                            lbDueDate.Text = "Due date is " + dt.Rows[0][8].ToString() ;
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
                        lbClass.Text = "Class(FORM) " + dt.Rows[0][2].ToString();
                        lbCreatedDate.Text = "Created " + dt.Rows[0][5].ToString();
                        lbLastUpdate.Text = "Last Update " + dt.Rows[0][6].ToString();
                        lastUpdateDate = Convert.ToDateTime(dt.Rows[0][6].ToString());
                        if (dt.Rows[0][9].ToString() == "")
                        {
                            divFile.Visible = false;
                            Session["hasFile"] = false;
                        }
                        else {
                            Session["hasFile"] = true;
                            string assessmentFilePath = "~/Assessment/"+ Request.QueryString["AssessmentGUID"] +"/" + dt.Rows[0][9].ToString();
                            hlFile.Text = dt.Rows[0][9].ToString();
                            hlFile.Attributes["href"] = ResolveUrl(assessmentFilePath);
                            lbDownload.Attributes["href"] = ResolveUrl(assessmentFilePath);
                            lbDownload.Attributes["download"] = dt.Rows[0][9].ToString();
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
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void lbModify_Click(object sender, EventArgs e)
        {
            tbDesc.Visible = true;
            tbTitle.Visible = true;
            lbTitle.Visible = false;
            lbDesc.Visible = false;
            lbMenu.Visible = false;
            divSubmissionList.Visible = false;
            tbTitle.Text = lbTitle.Text;
            tbDesc.Text = lbDesc.Text;

            if (!(Session["currentDueDate"].Equals("Jan  1 1900 12:00AM"))) {
                DateTime dueDate = DateTime.Parse((string)(Session["currentDueDate"]));
                tbDueDate.Text = dueDate.ToString("dd/MM/yyyy HH: mm tt");
            }
            lbDueDate.Visible = false;
            divDueDate.Visible = true;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            lbDownload.Visible = false;
            if (Session["hasFile"].Equals(true))
            {
                lbClearFile.Visible = true;
                Session["updateFile"] = false;
            }
            else {
                spanlb.Visible = false;
                divFile.Visible = true;
                AsyncFileUpload1.Visible = true;
                Session["updateFile"] = true;
            }
        }

        protected void lbDelete_Click(object sender, EventArgs e)
        {
            DeleteAssessment();
        }

        private void DeleteAssessment()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String strDelete = "";
                SqlCommand cmd = new SqlCommand(strDelete, con);
                if (CheckSubmission() == true)
                {
                    cmd = new SqlCommand("DELETE FROM Submission WHERE AssessmentGUID='" + Request.QueryString["AssessmentGUID"] + "';", con);
                    cmd.ExecuteNonQuery();
                    
                }
                cmd = new SqlCommand("DELETE FROM Assessment WHERE AssessmentGUID='" + Request.QueryString["AssessmentGUID"] + "';", con);
                cmd.ExecuteNonQuery();
                con.Close();

                string folderName = "~/Assessment/" + Request.QueryString["AssessmentGUID"];

                //delete student submit file and assessment file
                System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(folderName + "/"));
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
                //delete assessment folder
                if (!Directory.Exists(folderName+"/"))
                {
                    System.IO.Directory.Delete(Server.MapPath(folderName));
                }

                
                Response.Write("<script language='javascript'>alert('Assessment deleted successfully');</script>");
                Server.Transfer("AssessmentList.aspx", true);
            }
            catch (Exception ex)
            {
                
            }
        }

        private bool CheckSubmission()
        {

            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM Submission WHERE AssessmentGUID='" + Request.QueryString["AssessmentGUID"] + "';", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAssessment();
        }

        private void UpdateAssessment()
        {
            if (tbDesc.Text.Trim() != "" & tbTitle.Text.Trim() != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Assessment SET LastUpdateDate=@LastUpdateDate, Title=@Title, Description=@Desc, [File]=@File, DueDate=@DueDate " +
                        "WHERE AssessmentGUID=@AssessmentGUID", con);
                    cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Title", tbTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Desc", tbDesc.Text.Trim());

                    if (Session["updateFile"].Equals(true))
                    {
                        string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                        cmd.Parameters.AddWithValue("@File", filename);
                    }
                    else {
                        cmd.Parameters.AddWithValue("@File", hlFile.Text);
                    }

                    if (tbDueDate.Text != "")
                    {
                        CultureInfo culture = new CultureInfo("ms-MY");
                        DateTime dueDate = Convert.ToDateTime(tbDueDate.Text, culture);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DueDate","");
                    }

                    cmd.Parameters.AddWithValue("@AssessmentGUID", Request.QueryString["AssessmentGUID"]);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Session["updateFile"].Equals(true))
                    {
                        string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
                        string folderName = "~/Assessment/" + Request.QueryString["AssessmentGUID"] + "/";

                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(Server.MapPath(folderName));
                        }

                        if (hlFile.Text!="") {
                            System.IO.File.Delete(Server.MapPath(folderName + "/" + hlFile.Text));
                        }
                        
                        if (filename!="")
                        {
                            AsyncFileUpload1.SaveAs(Server.MapPath(folderName) + filename );
                        }

                    }
                    Response.Write("<script language='javascript'>alert('Assessment updated successfully');</script>");
                    Server.Transfer(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                Response.Write("<script>alert('Title and description of assessment cannot be blank');</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }

        protected void lbClearFile_Click(object sender, EventArgs e)
        {
            Session["updateFile"]= true;
            spanlb.Visible = false;
            hlFile.Visible = false;
            AsyncFileUpload1.Visible = true;
        }

        protected void lbClear_Click(object sender, EventArgs e)
        {
            tbDueDate.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitFile();
        }

        private void SubmitFile()
        {
            if (AsyncFileUpload2.FileName != "" || btnSubmit.Text !="Submit")
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Submission SET LastUpdateDate=@LastUpdateDate,[File]=@File,Status=@Status " +
                        "WHERE AssessmentGUID=@AssessmentGUID AND StudentGUID=@StudentGUID AND SubmissionGUID=@SubmissionGUID", con);
                    cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@AssessmentGUID", Request.QueryString["AssessmentGUID"]);
                    cmd.Parameters.AddWithValue("@StudentGUID", Session["userGUID"]);
                    cmd.Parameters.AddWithValue("@SubmissionGUID",Session["submitGUID"]);

                    string filename = System.IO.Path.GetFileName(AsyncFileUpload2.FileName);
                    cmd.Parameters.AddWithValue("@File", filename);

                    if (btnSubmit.Text == "Submit")
                    {
                        cmd.Parameters.AddWithValue("@Status", "Submitted");
                    }
                    else {
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                    }

                    cmd.ExecuteNonQuery();
                    con.Close();
                    string folderName = "~/Assessment/" + Request.QueryString["AssessmentGUID"] + "/Submission/" +Session["submitGUID"]+"/";

                    if (btnSubmit.Text == "Submit")
                    {
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(Server.MapPath(folderName));
                        }

                        AsyncFileUpload2.SaveAs(Server.MapPath(folderName) + filename);
                    }
                    else {
                        //delete submited file
                        System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(folderName));
                        foreach (FileInfo file in di.EnumerateFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.EnumerateDirectories())
                        {
                            dir.Delete(true);
                        }
                    }

                    Response.Write("<script language='javascript'>alert('File submited');</script>");
                    Server.Transfer(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                Response.Write("<script>alert('Must submit a file!');</script>");
            }
        }

        protected void GvSubmissionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Button btnView = e.Row.FindControl("btnView") as Button;
                //Button btnDownload = e.Row.FindControl("btnDownload") as Button;

                //HyperLink hlView = e.Row.FindControl("hlView") as HyperLink;
                //HyperLink hlDownload = e.Row.FindControl("hlDownload") as HyperLink;

                LinkButton lbView = e.Row.FindControl("lbView") as LinkButton;
                LinkButton lbDownload = e.Row.FindControl("lbDownload") as LinkButton;

                if (lbView != null && lbDownload != null) {
                    string submitFilePath = "~/Assessment/"+Request.QueryString["AssessmentGUID"]+"/Submission/" + DataBinder.Eval(e.Row.DataItem, "SubmissionGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "File");
                    lbView.Attributes["href"] = ResolveUrl(submitFilePath);
                    lbDownload.Attributes["href"] = ResolveUrl(submitFilePath);
                    lbDownload.Attributes["download"] = DataBinder.Eval(e.Row.DataItem, "File").ToString();
                    if (DataBinder.Eval(e.Row.DataItem, "File").ToString() == "") {
                        lbView.Visible = false;
                        lbDownload.Visible = false;
                    }
                }
            }
        }

        protected void dlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrivView();
        }
    }
}
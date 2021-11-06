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
    public partial class AssessmentList : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (!(Session["userGUID"] != null))
                {
                    Response.Redirect("Login.aspx");
                }
                CheckRole();
            }
        }

        protected void CheckRole()
        {
            if (Session["role"].Equals("Teacher"))
            {
                btnCreateAssessment.Visible = true;
                dlClassList.Visible = true;
                dlStatus.Visible = false;
            }
            BindClasses();
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
                SqlCommand cmd = new SqlCommand("SELECT * from Forum where ForumGUID =''", con);
                if (Session["role"].Equals("Student"))
                {
                    cmd = new SqlCommand(
                        "SELECT s.ClassroomGUID, Class " +
                        "FROM Student s LEFT JOIN Classroom c ON c.ClassroomGUID = s.ClassroomGUID " +
                        "WHERE StudentGUID=@StudentGUID", con);
                    cmd.Parameters.AddWithValue("@StudentGUID", Session["userGUID"]);
                }
                else
                {
                    cmd = new SqlCommand(
                        "SELECT tc.ClassroomGUID, Class " +
                        "FROM Teacher_Classroom tc LEFT JOIN Classroom c ON c.ClassroomGUID = tc.ClassroomGUID " +
                        "WHERE TeacherGUID=@TeacherGUID", con);
                    cmd.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"]);
                }
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dt.Load(dr);
                    dlClassList.DataTextField = "Class";
                    dlClassList.DataValueField = "ClassroomGUID";
                    dlClassList.DataSource = dt;
                    dlClassList.DataBind();
                    BindGridView();
                }
                else
                {
                    Response.Write("<script>alert('Error:No class assigned yet');</script>");
                    Server.Transfer("Homepage.aspx", true);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void BindGridView()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String strSelect = "";
                if (Session["role"].Equals("Student"))
                {
                    if (dlStatus.SelectedValue.Equals("Assign")) {

                        strSelect =
                            "SELECT a.AssessmentGUID, '<b>'+t.FullName+'</b>'+' On '+convert(VARCHAR(20),a.CreateDate,100) as CreateBy, convert(VARCHAR(20),a.LastUpdateDate,100) as LastUpdateDate, a.ClassroomGUID, Title, " +
                            "CASE " +
                            "WHEN DueDate ='1/1/1900 12:00:00 AM' THEN 'No due date' " +
                            "ELSE convert(VARCHAR(20),a.DueDate,100) " +
                            "END AS DueDate " +
                            "FROM Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID =tc.TeacherGUID " +
                            "LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID " +
                            "LEFT JOIN Assessment a ON cl.ClassroomGUID = a.ClassroomGUID " +
                            "LEFT JOIN Submission s ON a.AssessmentGUID = s.AssessmentGUID " +
                            "LEFT JOIN Student st ON cl.ClassroomGUID = st.ClassroomGUID " +
                            "WHERE a.ClassroomGUID=@ClassroomGUID AND (DueDate > @CurrentDateTime OR DueDate ='1/1/1900 12:00:00 AM') AND s.StudentGUID=@userGUID AND s.Status='Pending' AND s.StudentGUID=@UserGUID AND st.StudentGUID=s.StudentGUID AND a.TeacherGUID=t.TeacherGUID";

                    } else if (dlStatus.SelectedValue.Equals("Submitted")) {
                        strSelect =
                            "SELECT a.AssessmentGUID, '<b>'+t.FullName+'</b>'+' On '+convert(VARCHAR(20),a.CreateDate,100) as CreateBy, convert(VARCHAR(20),a.LastUpdateDate,100) as LastUpdateDate, a.ClassroomGUID, Title, " +
                            "CASE " +
                            "WHEN DueDate ='1/1/1900 12:00:00 AM' THEN 'No due date' " +
                            "ELSE convert(VARCHAR(20),a.DueDate,100) " +
                            "END AS DueDate " +
                            "FROM Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID =tc.TeacherGUID " +
                            "LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID " +
                            "LEFT JOIN Assessment a ON cl.ClassroomGUID = a.ClassroomGUID " +
                            "LEFT JOIN Submission s ON a.AssessmentGUID = s.AssessmentGUID " +
                            "LEFT JOIN Student st ON cl.ClassroomGUID = st.ClassroomGUID " +
                            "WHERE a.ClassroomGUID=@ClassroomGUID AND s.StudentGUID = @UserGUID AND s.Status='Submitted' AND s.StudentGUID=@UserGUID AND st.StudentGUID=s.StudentGUID AND a.TeacherGUID=t.TeacherGUID";
                    }
                    else {
                        strSelect =
                            "SELECT a.AssessmentGUID, '<b>'+t.FullName+'</b>'+' On '+convert(VARCHAR(20),a.CreateDate,100) as CreateBy, convert(VARCHAR(20),a.LastUpdateDate,100) as LastUpdateDate, a.ClassroomGUID, Title, convert(VARCHAR(20),a.DueDate,100) as DueDate " +
                            "FROM Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID =tc.TeacherGUID " +
                            "LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID " +
                            "LEFT JOIN Assessment a ON cl.ClassroomGUID = a.ClassroomGUID " +
                            "LEFT JOIN Submission s ON a.AssessmentGUID = s.AssessmentGUID " +
                            "LEFT JOIN Student st ON cl.ClassroomGUID = st.ClassroomGUID " +
                            "WHERE a.ClassroomGUID=@ClassroomGUID AND DueDate < @CurrentDateTime AND DueDate != '1/1/1900 12:00:00 AM' AND s.Status='Pending' AND s.StudentGUID=@UserGUID AND st.StudentGUID=s.StudentGUID AND a.TeacherGUID=t.TeacherGUID";
                    }
                }
                else {

                    strSelect =
                        "SELECT a.AssessmentGUID, '<b>'+FullName+'</b>'+' On '+convert(VARCHAR(20),a.CreateDate,100) as CreateBy, convert(VARCHAR(20),a.LastUpdateDate,100) as LastUpdateDate, a.ClassroomGUID, Title, " +
                        "CASE " +
                        "WHEN DueDate ='1/1/1900 12:00:00 AM' THEN 'No due date' " +
                        "ELSE convert(VARCHAR(20),a.DueDate,100) " +
                        "END AS DueDate " +
                        "FROM Teacher t LEFT JOIN Teacher_Classroom tc ON t.TeacherGUID =tc.TeacherGUID " +
                        "LEFT JOIN Classroom cl ON tc.ClassroomGUID = cl.ClassroomGUID " +
                        "LEFT JOIN Assessment a ON cl.ClassroomGUID = a.ClassroomGUID " +
                        "WHERE a.ClassroomGUID=@ClassroomGUID AND a.TeacherGUID = @UserGUID AND a.TeacherGUID=t.TeacherGUID";
                }

                SqlCommand cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);
                cmd.Parameters.AddWithValue("@CurrentDateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserGUID", Session["userGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                lbClass.Text = "Class(FORM) : " + dlClassList.SelectedItem;
                dt.Load(dr);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    GvAssessmentList.DataSource = dt;
                    GvAssessmentList.DataBind();
                }
                else
                {
                    lblNoData.Visible = false;
                    GvAssessmentList.DataSource = dt;
                    GvAssessmentList.DataBind();
                }

                con.Close();
            }
            catch (SqlException ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>"); ;
            }
        }


        protected void dlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void dlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btnCreateAssessment_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAssessment.aspx");
        }

        protected void GvAssessmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "AssessmentDetails.aspx" + "?AssessmentGUID=" + DataBinder.Eval(e.Row.DataItem, "AssessmentGUID");
            }
        }
    }
}
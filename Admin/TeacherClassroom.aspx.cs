using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS.Admin
{
    public partial class TeacherClassroom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false)
            {
                LoadClass();
                LoadTeacher();
                LoadGrid();
            }
        }

        protected void LoadClass()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT ClassroomGUID, Class FROM Classroom ORDER BY Class";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlClass.DataTextField = dt.Columns["Class"].ToString();     
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString();
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
                }
                LoadSubject();

                con.Close();
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void LoadTeacher()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT TeacherGUID,FullName FROM Teacher ORDER BY FullName";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlName.DataTextField = dt.Columns["FullName"].ToString();
                    ddlName.DataValueField = dt.Columns["TeacherGUID"].ToString();
                    ddlName.DataSource = dt;
                    ddlName.DataBind();
                }


                con.Close();
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void LoadSubject()
        {
            try
            {
                string ClassroomGUID = ddlClass.SelectedValue.ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.SubjectGUID,b.SubjectName FROM Subject_Classroom a LEFT JOIN Subject b ON a.SubjectGUID = b.SubjectGUID WHERE a.ClassroomGUID = @Class ORDER BY b.SubjectName";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Class", ClassroomGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlSubject.DataTextField = dt.Columns["SubjectName"].ToString();
                    ddlSubject.DataValueField = dt.Columns["SubjectGUID"].ToString();
                    ddlSubject.DataSource = dt;
                    ddlSubject.DataBind();
                }


                con.Close();
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void LoadGrid()
        {
            try
            {
                string ClassroomGUID = ddlClass.SelectedValue.ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "select a.ClassroomGUID,a.SubjectGUID,ISNULL(b.TeacherGUID,'00000000-0000-0000-0000-000000000000') AS TeacherGUID,ISNULL(c.FullName,'Not Assign') AS TeacherName,d.SubjectName " +
                    "FROM Subject_Classroom a LEFT JOIN Teacher_Classroom b ON a.ClassroomGUID = b.ClassroomGUID AND a.SubjectGUID = b.SubjectTeach " +
                    "LEFT JOIN Teacher c ON b.TeacherGUID = c.TeacherGUID LEFT JOIN Subject d ON a.SubjectGUID = d.SubjectGUID WHERE a.ClassroomGUID = @Class";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Class", ClassroomGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Subjects = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Subjects = " + totalQty;

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                if (NewSubject())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Success');window.location ='TeacherClassroom.aspx';", true);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadGrid();
        }


        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            LoadSubject();
            LoadGrid();
        }

        protected bool ValidateSave()
        {
            bool save = false;
            try
            {
                string ClassroomGUID = ddlClass.SelectedValue.ToString();
                string TeacherGUID = ddlName.SelectedValue.ToString();
                string SubjectGUID = ddlSubject.SelectedValue.ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT * FROM Teacher_Classroom WHERE TeacherGUID = @TeacherGUID AND ClassroomGUID = @ClassroomGUID AND SubjectTeach = @SubjectGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherGUID", TeacherGUID);
                cmdSelect.Parameters.AddWithValue("@ClassroomGUID", ClassroomGUID);
                cmdSelect.Parameters.AddWithValue("@SubjectGUID", SubjectGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    DisplayAlertMsg("One teacher only can teach a subject in a class");
                    return save;
                }

                if (ddlClass.SelectedValue == null)
                {
                    DisplayAlertMsg("Please Select Class");
                    return save;
                }
                if (ddlName.SelectedValue == null)
                {
                    DisplayAlertMsg("Please Select Teacher");
                    return save;
                }
                if (ddlSubject.SelectedValue == null)
                {
                    DisplayAlertMsg("Please Select Subject");
                    return save;
                }
                save = true;
                return save;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return save;
            }
        }

        protected bool NewSubject()
        {
            bool save = false;
            try
            {
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Teacher_Classroom VALUES (@TeacherGUID,@ClassroomGUID,@SubjectGUID)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@TeacherGUID", ddlName.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@SubjectGUID", ddlSubject.SelectedValue);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                save = true;
                return save;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return save;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTeacher")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[i];
                string teacherguid = row.Cells[1].Text;

                if (teacherguid != "00000000-0000-0000-0000-000000000000")
                {
                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(strCon);

                    con.Open();

                    String strDelete = "DELETE Teacher_Classroom WHERE TeacherGUID = @TeacherGUID AND ClassroomGUID = @ClassroomGUID";

                    SqlCommand cmdDelete = new SqlCommand(strDelete, con);

                    cmdDelete.Parameters.AddWithValue("@TeacherGUID", teacherguid);
                    cmdDelete.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue);

                    cmdDelete.ExecuteNonQuery();

                    con.Close();
                    LoadGrid();
                }
                else
                {
                    DisplayAlertMsg("No Data to delete");
                }

            }
        }

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
        //    {
        //        (e.Row.Cells[5].Controls[1] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to unassign this teacher of this subject?');";
        //    }
        //}
    }
}
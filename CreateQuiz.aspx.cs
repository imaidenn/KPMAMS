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
    public partial class CreateQuiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["role"] != null && Session["userGUID"] != null)
            {
                if (Session["role"].ToString() == "Teacher")
                {
                    if(Session["quiz"] == null)
                    {
                        GetClass();
                    }
                    else
                    {
                        LoadData();
                    }

                }
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

                String strSelect = "SELECT c.SubjectName, b.Class, a.ClassroomGUID FROM Teacher_Classroom a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID LEFT JOIN Subject c ON a.SubjectTeach = c.SubjectGUID " +
                        "WHERE a.TeacherGUID = @TeacherGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"].ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlClass.DataTextField = dt.Columns["Class"].ToString();
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString();
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
                }


                con.Close();
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.QuizTitle,b.Class,b.ClassroomGUID FROM Quiz a LEFT JOIN Classroom b ON a.Class = b.ClassroomGUID WHERE a.QuizGUID = @QuizGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@QuizGUID", Session["quiz"].ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    txtQuizTitle.Text = dt.Rows[0][0].ToString();
                    txtQuizTitle.Enabled = false;

                    ddlClass.DataTextField = dt.Columns["Class"].ToString();
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString();
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
                    ddlClass.Enabled = false;

                }


                con.Close();

                LoadGrid();
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
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT QuestionGUID,Question FROM Question WHERE QuizGUID = @QuizGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@QuizGUID", Session["quiz"].ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Question = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Question = " + totalQty;

                }
                GridView1.DataSource = dt;
                GridView1.DataBind();


                con.Close();

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCreate())
                {
                    if (CheckQuestion())
                    {
                        Session["quiz"] = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Quiz Created');window.location ='QuizListing.aspx';", true);
                    }
                    
                }

                
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(Session["quiz"] == null)
                {
                    if (ValidateCreate())
                    {
                        NewQuiz();
                        Response.Redirect("QuestionEntry.aspx");
                    }
                    
                }
                else
                {
                    Response.Redirect("QuestionEntry.aspx");
                }
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void NewQuiz()
        {
            try
            {
                Guid quizGUID = Guid.NewGuid();
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Quiz VALUES (@QuizGUID,@TeacherGUID,@ClassroomGUID,@QuizTitle,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@QuizGUID", quizGUID.ToString());
                cmdInsert.Parameters.AddWithValue("@TeacherGUID", Session["userGUID"].ToString());
                cmdInsert.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@QuizTitle", txtQuizTitle.Text);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();

                Session["quiz"] = quizGUID.ToString();
            }
            catch(SqlException ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected bool ValidateCreate()
        {
            bool create = false;
            try
            {
                if(txtQuizTitle.Text == "")
                {
                    DisplayAlertMsg("Quiz Title cannot be empty");
                    return create;
                }
                if(ddlClass.SelectedValue == "")
                {
                    DisplayAlertMsg("Please select class");
                    return create;
                }

                create = true;
                return create;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return create;
            }
        }

        protected bool CheckQuestion()
        {
            bool create = false;
            try
            {
                if (GridView1.Rows.Count == 0)
                {
                    DisplayAlertMsg("Please Add a question");
                    return create;
                }

                create = true;
                return create;
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return create;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "QuestionEntry.aspx" + "?QuestionGUID=" + DataBinder.Eval(e.Row.DataItem, "QuestionGUID");
            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

    }
}
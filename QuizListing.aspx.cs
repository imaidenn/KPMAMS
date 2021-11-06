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
    public partial class QuizListing : System.Web.UI.Page
    {
        string classGUID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userGUID"] != null)
            {
                if(Session["role"].ToString() == "Student")
                {
                    GetClass();
                }
                GetQuizList();
            }
        }

        protected void GetClass()
        {
            try
            {
                string userGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT ClassroomGUID FROM Student WHERE StudentGUID = @userGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@userGUID", userGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                classGUID = dt.Rows[0][0].ToString();

                con.Close();
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetQuizList()
        {
            try
            {
                string userGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "";

                if(Session["role"].ToString() == "Teacher")
                {
                    strSelect = "SELECT a.QuizGUID,a.QuizTitle,c.Class,COUNT(b.QuestionGUID) As TotalQuestion " +
                       "FROM Quiz a LEFT JOIN Question b ON a.QuizGUID = b.QuizGUID LEFT JOIN Classroom c ON a.Class = c.ClassroomGUID " +
                       "WHERE a.CreateBy = @TeacherGUID GROUP BY a.QuizGUID,a.QuizTitle,c.Class";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@TeacherGUID", userGUID);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);

                }
                else if(Session["role"].ToString() == "Student")
                {
                    strSelect = "SELECT a.QuizGUID,a.QuizTitle,c.Class,COUNT(b.QuestionGUID) As TotalQuestion " +
                    "FROM Quiz a LEFT JOIN Question b ON a.QuizGUID = b.QuizGUID LEFT JOIN Classroom c ON a.Class = c.ClassroomGUID " +
                    "WHERE a.Class = @ClassroomGUID GROUP BY a.QuizGUID,a.QuizTitle,c.Class";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@ClassroomGUID", classGUID);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);

                    btnCreate.Visible = false;
                    btnBack.Visible = false;
                }
                

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Quiz = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Quiz = " + totalQty;

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (Session["role"].ToString() == "Teacher")
                {
                    if (hyperLink != null)
                        hyperLink.Attributes["href"] = "QuizScore.aspx" + "?QuizGUID=" + DataBinder.Eval(e.Row.DataItem, "QuizGUID");
                }
                else if(Session["role"].ToString() == "Student")
                {

                    if (hyperLink != null)
                        hyperLink.Attributes["href"] = "QuizAnswer.aspx" + "?QuizGUID=" + DataBinder.Eval(e.Row.DataItem, "QuizGUID");

                    
                }


            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateQuiz.aspx");
        }
    }
}
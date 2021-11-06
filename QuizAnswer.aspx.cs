using System;
using System.Collections;
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
    public partial class QuizAnswer : System.Web.UI.Page
    {
        //private static ArrayList questionguid = new ArrayList();
        DateTime start = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userGUID"] != null)
            {
                if(Session["role"].ToString() == "Student")
                {
                    if(Request.QueryString["QuizGUID"] != null)
                    {
                        if(IsPostBack == false)
                        {
                            CheckAnswer();
                            
                        }
                        
                    }
                }
            }
        }

        protected void CheckAnswer()
        {
            try
            {
                string userGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT AnswerGUID FROM Answer WHERE StudentGUID = @StudentGUID AND QuizGUID = @QuizGUID";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", userGUID);
                cmdSelect.Parameters.AddWithValue("@QuizGUID", Request.QueryString["QuizGUID"].ToString());
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    LoadQuestion();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have already answered the quiz');window.location ='QuizScore.aspx?QuizGUID=" + Request.QueryString["QuizGUID"].ToString() + "';", true);
                }

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void LoadQuestion()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT QuestionGUID,Question,Option1,Option2,Option3,Option4 FROM Question WHERE QuizGUID = @QuizGUID";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@QuizGUID", Request.QueryString["QuizGUID"].ToString());
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();

                }



                con.Close();
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                //if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
                //{


                //}
                Label qGUID = e.Item.FindControl("lblQuestionGUID") as Label;
                //questionguid.Add(qGUID.Text);

                RadioButtonList rBtnList = e.Item.FindControl("RadioButtonList1") as RadioButtonList;

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT Option1,Option2,Option3,Option4 FROM Question WHERE QuestionGUID = @QuestionGUID";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@QuestionGUID", qGUID.Text);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            string opt = dr[dc].ToString();
                            ListItem nItem = new ListItem(opt);
                            rBtnList.Items.Add(nItem);
                            nItem.Attributes.CssStyle.Add("margin-left", "25px");
                        }
                    }
                    //DataRow row = dt.Rows[0];
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    string opt = row[i].ToString();

                }


                con.Close();
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InsertAnswer())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Congratulation you answered all');window.location ='QuizScore.aspx?QuizGUID="+Request.QueryString["QuizGUID"].ToString()+"';", true);
            }
        }

        protected bool InsertAnswer()
        {
            bool save = false;
            int correct = 0;
            int wrong = 0;
            int no = 0;
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                foreach (RepeaterItem ritem in Repeater1.Items)
                {
                    Label questionguid = ritem.FindControl("lblQuestionGUID") as Label;
                    String strSelect = "SELECT CorrectAnswer FROM Question WHERE QuestionGUID = @QuestionGUID";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@QuestionGUID", questionguid.Text);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                    RadioButtonList rBtnList = ritem.FindControl("RadioButtonList1") as RadioButtonList;

                    if (rBtnList.SelectedIndex >= 0)
                    {
                        if (dt.Rows[0][0].ToString() == (rBtnList.SelectedIndex + 1).ToString())
                        {
                            correct += 1;
                        }
                        else
                        {
                            wrong += 1;
                        }
                    }
                    else
                    {
                        no += 1;
                    }


                }

                if (no > 0)
                {
                    DisplayAlertMsg("Please fill in all the answer");
                    return save;
                }

                TimeSpan ts = (DateTime.Now).Subtract(start);
                double duration = Double.Parse(ts.TotalSeconds.ToString());
                int score = (correct/(correct+wrong)) * 100;
                Guid AnswerGUID = Guid.NewGuid();

                String strInsert = "INSERT INTO Answer VALUES (@AnswerGUID,@QuizGUID,@StudentGUID,@TotalCorrect,@TotalScore,@Duration,@CreateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@AnswerGUID", AnswerGUID);
                cmdInsert.Parameters.AddWithValue("@QuizGUID", Request.QueryString["QuizGUID"].ToString());
                cmdInsert.Parameters.AddWithValue("@StudentGUID", Session["userGUID"].ToString());
                cmdInsert.Parameters.AddWithValue("@TotalCorrect", correct);
                cmdInsert.Parameters.AddWithValue("@TotalScore", score);
                cmdInsert.Parameters.AddWithValue("@Duration", duration);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);

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

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }
}
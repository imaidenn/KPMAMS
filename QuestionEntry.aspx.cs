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
    public partial class QuestionEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["quiz"] != null)
            {
                if(Request.QueryString["QuestionGUID"] != null)
                {
                    LoadData();
                }
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

                String strSelect = "SELECT Question,Option1,Option2,Option3,Option4,CorrectAnswer FROM Question WHERE QuestionGUID = @QuestionGUID";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@QuestionGUID", Request.QueryString["QuestionGUID"].ToString());
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if(dt.Rows.Count > 0)
                {
                    txtQuestion.Text = dt.Rows[0][0].ToString();
                    txtAnswer1.Text = dt.Rows[0][1].ToString();
                    txtAnswer2.Text = dt.Rows[0][2].ToString();
                    txtAnswer3.Text = dt.Rows[0][3].ToString();
                    txtAnswer4.Text = dt.Rows[0][4].ToString();

                    string correct = dt.Rows[0][5].ToString();

                    if (correct == "1")
                    {
                        rbAns1.Checked = true;
                    }
                    else if (correct == "2")
                    {
                        rbAns2.Checked = true;
                    }
                    else if (correct == "3")
                    {
                        rbAns3.Checked = true;
                    }
                    else if (correct == "4")
                    {
                        rbAns4.Checked = true;
                    }

                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                


                con.Close();
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                if (NewQuestion())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Question Created');window.location ='CreateQuiz.aspx';", true);
                }
            }
        }

        protected bool ValidateSave()
        {
            bool save = false;
            try
            {

                if(txtQuestion.Text == "")
                {
                    DisplayAlertMsg("Please enter question");
                    return save;
                }
                if (txtAnswer1.Text == "")
                {
                    DisplayAlertMsg("Please enter selection");
                    return save;
                }
                if (txtAnswer2.Text == "")
                {
                    DisplayAlertMsg("Please enter selection");
                    return save;
                }
                //if (rbAns1.Checked ==false || rbAns2.Checked == false || rbAns3.Checked == false || rbAns4.Checked == false)
                //{
                //    DisplayAlertMsg("Please select correct answer");
                //    return save;
                //}
                save = true;
                return save;
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
                return save;
            }
        }

        protected bool NewQuestion()
        {
            bool save = false;
            try
            {
                string correct = "";
                if(rbAns1.Checked == true)
                {
                    correct = "1";
                }
                else if (rbAns2.Checked == true)
                {
                    correct = "2";
                }
                else if (rbAns3.Checked == true)
                {
                    correct = "3";
                }
                else if (rbAns4.Checked == true)
                {
                    correct = "4";
                }
                else
                {
                    DisplayAlertMsg("Please select correct answer");
                    return save;
                }

                Guid QuestionGUID = Guid.NewGuid();
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Question VALUES (@QuestionGUID,@QuizGUID,@Question,@Correct,@Option1,@Option2,@Option3,@Option4,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@QuestionGUID", QuestionGUID);
                cmdInsert.Parameters.AddWithValue("@QuizGUID", Session["quiz"].ToString());
                cmdInsert.Parameters.AddWithValue("@Question", txtQuestion.Text);
                cmdInsert.Parameters.AddWithValue("@Correct", correct);
                cmdInsert.Parameters.AddWithValue("@Option1", txtAnswer1.Text);
                cmdInsert.Parameters.AddWithValue("@Option2", txtAnswer2.Text);
                cmdInsert.Parameters.AddWithValue("@Option3", txtAnswer3.Text);
                cmdInsert.Parameters.AddWithValue("@Option4", txtAnswer4.Text);
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

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

        protected bool UpdateQuestion()
        {
            bool save = false;
            try
            {
                string correct = "";
                if (rbAns1.Checked == true)
                {
                    correct = "1";
                }
                else if (rbAns2.Checked == true)
                {
                    correct = "2";
                }
                else if (rbAns3.Checked == true)
                {
                    correct = "3";
                }
                else if (rbAns4.Checked == true)
                {
                    correct = "4";
                }
                else
                {
                    DisplayAlertMsg("Please select correct answer");
                    return save;
                }

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strUpdate = "UPDATE Question SET QuestionTitle = @QuestionTitle, Correct = @Correct, Option1 = @Option1, Option2 = @Option2, Option3 = @Option3, Option4 = @Option4, LastUpdateDate = @LastUpdateDate WHERE QuestionGUID = @QuestionGUID";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@QuestionGUID", Request.QueryString["QuestionGUID"]);
                cmdUpdate.Parameters.AddWithValue("@Question", txtQuestion.Text);
                cmdUpdate.Parameters.AddWithValue("@Correct", correct);
                cmdUpdate.Parameters.AddWithValue("@Option1", txtAnswer1.Text);
                cmdUpdate.Parameters.AddWithValue("@Option2", txtAnswer2.Text);
                cmdUpdate.Parameters.AddWithValue("@Option3", txtAnswer3.Text);
                cmdUpdate.Parameters.AddWithValue("@Option4", txtAnswer4.Text);
                cmdUpdate.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdUpdate.ExecuteNonQuery();

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateQuiz.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                if (UpdateQuestion())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Question Updated');window.location ='CreateQuiz.aspx';", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strDelete = "DELETE Question WHERE QuestionGUID = @QuestionGUID";

                SqlCommand cmdDelete = new SqlCommand(strDelete, con);

                cmdDelete.Parameters.AddWithValue("@QuestionGUID", Request.QueryString["QuestionGUID"]);

                cmdDelete.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);

            }
        }
    }
}
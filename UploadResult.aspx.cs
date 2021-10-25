using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class UploadResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack == false)
            {
                GetClass();
                GetYear();
                GetSemester();

            }


        }

        protected void GetSemester()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Text");
                dt.Columns.Add("Value");
                DataRow a = dt.NewRow();
                DataRow b = dt.NewRow();
                DataRow c = dt.NewRow();
                DataRow d = dt.NewRow();
                a["Text"] = "March";
                a["Value"] = "3March";
                b["Text"] = "Pertengahan Tahun";
                b["Value"] = "6PertengahanTahun";
                c["Text"] = "August";
                c["Value"] = "8August";
                d["Text"] = "Akhir Tahun";
                d["Value"] = "11AkhirTahun";
                dt.Rows.Add(a);
                dt.Rows.Add(b);
                dt.Rows.Add(c);
                dt.Rows.Add(d);
                ddlSem.DataTextField = dt.Columns["Text"].ToString();
                ddlSem.DataValueField = dt.Columns["Value"].ToString();
                ddlSem.DataSource = dt;
                ddlSem.DataBind();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetYear()
        {
            try
            {
                int year = DateTime.Now.Year;
                for (int i = year; i <= year + 7; i++)
                {
                    ListItem li = new ListItem(i.ToString());
                    ddlYear.Items.Add(li);
                }
            
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
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

                String strSelect = "SELECT ClassroomGUID, Class FROM Classroom ORDER BY Class ASC";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlClass.DataTextField = dt.Columns["Class"].ToString(); ;   
                    ddlClass.DataValueField = dt.Columns["ClassroomGUID"].ToString(); ;
                    ddlClass.DataSource = dt;
                    ddlClass.DataBind();
                }



                con.Close();

                GetStudent(ddlClass.SelectedValue);

            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetStudent(String classGUID)
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT StudentGUID,Fullname FROM Student WHERE ClassroomGUID= @class";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@class", classGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if (dt.Rows.Count > 0)
                {
                    ddlStudent.DataTextField = dt.Columns["Fullname"].ToString(); // text field name of table dispalyed in dropdown       
                    ddlStudent.DataValueField = dt.Columns["StudentGUID"].ToString();
                    ddlStudent.DataSource = dt;
                    ddlStudent.DataBind();
                }


                con.Close();

                if(dt.Rows.Count > 0)
                {
                    GetSubject();
                }
                
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GetSubject()
        {
            try
            {
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.SubjectGUID,b.SubjectName FROM Subject_Classroom a LEFT JOIN SUbject b ON a.SubjectGUID = b.SubjectGUID WHERE a.ClassroomGUID = @ClassroomGUID";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ClassroomGUID", ddlClass.SelectedValue.ToString());

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;

                }
                else
                {
                    lblNoData.Visible = false;

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();
                
            }
            catch(Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
                


            //}
            //catch(Exception ex)
            //{
            //    DisplayAlertMsg(ex.Message);
            //}
            
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected bool NewMark()
        {
            bool addBool = false;
            try
            {

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Guid examGuid = Guid.NewGuid();
                        string subjectGuid = row.Cells[0].Text;
                        int mark;
                        string textMark = ((HtmlInputText)row.FindControl("txtMark")).Value;
                        mark = int.Parse(textMark);


                        string grade = "";
                        string textGrade = ((HtmlInputText)row.FindControl("txtGrade")).Value;
                        if (textGrade.Length > 1)
                        {
                            grade = textGrade.Substring(0, 1).ToUpper().ToString() + textGrade.Substring(1, 1).ToString();
                        }
                        else
                        {
                            grade = textGrade.Substring(0, 1).ToUpper().ToString();
                        }

                        string sem = ddlSem.SelectedValue;
                        string createBy = Session["userGUID"].ToString();


                        String strInsert = "INSERT INTO Exam(ExamGUID,SubjectGUID,StudentGUID,Class,Mark,Grade,ExamSemester,Status,CreatedBy,CreateDate,LastUpdateDate) VALUES(@examGUID,@subjectGUID,@studentGUID,@class,@mark,@grade,@examSem,@status,@createdby,@createDate,@lastUpdateDate)";

                        SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                        cmdInsert.Parameters.AddWithValue("@examGUID", examGuid);
                        cmdInsert.Parameters.AddWithValue("@subjectGUID", subjectGuid);
                        cmdInsert.Parameters.AddWithValue("@studentGUID", ddlStudent.SelectedValue.ToString());
                        cmdInsert.Parameters.AddWithValue("@class", ddlClass.SelectedValue.ToString());
                        cmdInsert.Parameters.AddWithValue("@mark", mark);
                        cmdInsert.Parameters.AddWithValue("@grade", grade);
                        cmdInsert.Parameters.AddWithValue("@examSem", ddlSem.SelectedValue.ToString()+ddlYear.SelectedValue);
                        cmdInsert.Parameters.AddWithValue("@status", "Pending");
                        cmdInsert.Parameters.AddWithValue("@createdby", createBy);
                        cmdInsert.Parameters.AddWithValue("@createDate", DateTime.Now);
                        cmdInsert.Parameters.AddWithValue("@lastUpdateDate", DateTime.Now);
                 
                        cmdInsert.ExecuteNonQuery();
                    }
                }
    

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }

        protected bool ValidateSave()
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int mark;
                    string textMark = ((HtmlInputText)row.FindControl("txtMark")).Value;
                    if(!(int.TryParse(textMark, out mark)))
                    {
                        DisplayAlertMsg("Please enter only digit");
                        return false;
                    }
                    mark = int.Parse(textMark);
                    if(mark < 0 || mark > 100)
                    {
                        DisplayAlertMsg("Invalid mark");
                        return false;
                    }

                    string grade = "";
                    string textGrade = ((HtmlInputText)row.FindControl("txtGrade")).Value;

                    if (textGrade.Length > 1)
                    {
                        grade = textGrade.Substring(0, 1).ToUpper().ToString() + textGrade.Substring(1, 1).ToString();
                    }
                    else
                    {
                        grade = textGrade.Substring(0, 1).ToUpper().ToString();
                    }



                    if (grade != "A+")
                    {
                        if(grade != "A")
                        {
                            if(grade != "A-")
                            {
                                if(grade != "B+")
                                {
                                    if(grade != "B")
                                    {
                                        if(grade != "B-")
                                        {
                                            if(grade != "C+")
                                            {
                                                if(grade != "C")
                                                {
                                                    if(grade != "C-")
                                                    {
                                                        if(grade != "D")
                                                        {
                                                            if(grade != "E")
                                                            {
                                                                if(grade != "F")
                                                                {
                                                                    DisplayAlertMsg("Invalid grade");
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

            DataTable dt = new DataTable();

            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            con.Open();

            String strSelect = "SELECT * FROM Exam WHERE StudentGUID = @Student AND ExamSemester = @Sem AND Status <> @Status";


            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@Student", ddlStudent.SelectedValue.ToString());
            cmdSelect.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue+ddlYear.SelectedValue);
            cmdSelect.Parameters.AddWithValue("@Status", "Rejected");

            SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

            dt.Load(dtrSelect);

            con.Close();


            if (dt.Rows.Count > 0)
            {
                DisplayAlertMsg("There is a record with same details is pending or accepted");
                return false;

            }

            

            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                
                if (NewMark())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New student mark submitted');window.location ='Homepage.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New student mark submit failed');window.location ='Homepage.aspx';", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GetStudent(ddlClass.SelectedValue.ToString());
            GetSubject();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS.Admin
{
    public partial class ApproveExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSemester();
            GetYear();
            if (IsPostBack == false)
            {
                GetExamListing();
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
            catch (Exception ex)
            {
                DisplayAlertMsg(ex.Message);
            }
        }

        protected void GetExamListing()
        {
            try
            {

                DateTime DateFrom = DateTime.Now;
                DateTime DateTo = DateTime.Now;
                if (Calendar1.Text != "" && Calendar2.Text != "")
                {
                    DateFrom = DateTime.ParseExact(Calendar1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTo = DateTime.ParseExact(Calendar2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "";
                if (Calendar1.Text == "" && Calendar2.Text == "")
                {
                    strSelect = "SELECT a.StudentGUID,b.FullName AS StudentName,a.ExamSemester,c.FullName AS TeacherName FROM Exam a LEFT JOIN Student b ON a.StudentGUID = b.StudentGUID " +
                        "LEFT JOIN Teacher c ON a.CreatedBy = c.TeacherGUID LEFT JOIN Classroom d ON b.ClassroomGUID = d.ClassroomGUID WHERE a.Status = 'Pending' AND a.ExamSemester = @ExamSem " +
                        "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE b.FullName END LIKE '%'+@StudentName+'%' " +
                        "AND CASE WHEN @Class = '' THEN @Class ELSE d.Class END LIKE '%'+@Class+'%'GROUP BY b.FullName,a.StudentGUID,ExamSemester,c.FullName ORDER BY b.FullName";
                }
                else if (Calendar1.Text != "" && Calendar2.Text != "")
                {
                    strSelect = "SELECT a.StudentGUID,b.FullName AS StudentName,a.ExamSemester,c.FullName AS TeacherName FROM Exam a LEFT JOIN Student b ON a.StudentGUID = b.StudentGUID " +
                        "LEFT JOIN Teacher c ON a.CreatedBy = c.TeacherGUID LEFT JOIN Classroom d ON b.ClassroomGUID = d.ClassroomGUID WHERE a.Status = 'Pending' AND a.ExamSemester = @ExamSem " +
                        "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE b.FullName END LIKE '%'+@StudentName+'%' " +
                        "AND CASE WHEN @Class = '' THEN @Class ELSE d.Class END LIKE '%'+@Class+'%' "+
                        "AND (a.CreateDate BETWEEN @DateFrom AND @DateTo) GROUP BY b.FullName,a.StudentGUID,ExamSemester,c.FullName ORDER BY b.FullName";
                }


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ExamSem", ddlSem.SelectedValue+ddlYear.SelectedItem.Text);
                cmdSelect.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmdSelect.Parameters.AddWithValue("@Class", txtClass.Text);
                cmdSelect.Parameters.AddWithValue("@DateFrom", DateFrom);
                cmdSelect.Parameters.AddWithValue("@DateTo", DateTo);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total exam records = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total exam records = " + totalQty;

                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                //DisplayAlertMsg("Please fill in the blank");
                DisplayAlertMsg(msg);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "ConfirmResult.aspx" + "?StudentGUID=" + DataBinder.Eval(e.Row.DataItem, "StudentGUID") + "&ExamSemester=" + DataBinder.Eval(e.Row.DataItem,"ExamSemester");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetExamListing();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }
}
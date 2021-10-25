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
    public partial class ResultListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack == false)
            {
                GetYear();
                GetSemester();
                LoadData();
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
                string msg = ex.Message;
            }
        }

        protected void LoadData()
        {
            try
            {
                string temp = ddlSem.SelectedValue + ddlYear.SelectedValue;
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT a.ExamSemester, a.StudentGUID, c.FullName, d.Class, b.AverageMark, b.GPA, b.CGPA FROM Exam a " +
                    "LEFT JOIN Result b ON a.ResultGUID = b.ResultGUID LEFT JOIN Student c ON a.StudentGUID = c.StudentGUID " +
                    "LEFT JOIN Classroom d ON a.Class = d.ClassroomGUID WHERE a.ExamSemester = @Sem AND a.Status = 'Confirmed' " +
                    "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE c.FullName END LIKE '%'+@StudentName+'%' " +
                    "AND CASE WHEN @Class = '' THEN @Class ELSE d.Class END LIKE '%'+@Class+'%' " +
                    "GROUP BY a.ExamSemester,a.StudentGUID,c.FullName,d.Class,b.AverageMark,b.GPA,b.CGPA ORDER BY FullName";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Sem", ddlSem.SelectedValue+ddlYear.SelectedValue);
                cmdSelect.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmdSelect.Parameters.AddWithValue("@Class", txtClass.Text);


                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total result records = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total result records = " + totalQty;

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

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "ResultEntry.aspx" + "?StudentGUID=" + DataBinder.Eval(e.Row.DataItem, "StudentGUID") + "&ExamSemester=" + DataBinder.Eval(e.Row.DataItem, "ExamSemester");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}
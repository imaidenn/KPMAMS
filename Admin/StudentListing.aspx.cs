using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS.Admin
{
    public partial class StudentListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GetStudentListing();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentEntry.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "StudentEntry.aspx" + "?StudentGUID=" + DataBinder.Eval(e.Row.DataItem, "StudentGUID");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetStudentListing();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GetStudentListing()
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
                if (Calendar1.Text == "" && Calendar2.Text == "" && txtStudentID.Text == "" && txtStudentName.Text =="" && txtClass.Text == "")
                {
                    if(ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID ORDER BY FullName";
                    }
                    else if(ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID WHERE a.Status = 'Active' ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID WHERE a.Status = 'Inactive' ORDER BY FullName";
                    }
                    
                }
                else if (Calendar1.Text == "" && Calendar2.Text == "")
                {
                    if (ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%'" +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%'" +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' ORDER BY FullName";
                    }
                    else if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE a.Status = 'Active'" +
                            "AND CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%'" +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%'" +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE a.Status = 'Inactive'" +
                            "AND CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%'" +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%'" +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' ORDER BY FullName";
                    }

                }
               
                else if(Calendar1.Text != "" && Calendar2.Text != "")
                {
                    if (ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%' " +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%' " +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' " +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }
                    else if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE a.Status = 'Active'" +
                            "AND CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%'" +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%'" +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' " +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT a.StudentGUID, a.StudentUserID, a.FullName, a.Gender, b.Class, a.Status, CONVERT(varchar,a.JoinDate,1) as JoinDate FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                            "WHERE a.Status = 'Inactive'" +
                            "AND CASE WHEN @StudentID = '' THEN @StudentID ELSE a.StudentUserID END LIKE '%'+@StudentID+'%'" +
                            "AND CASE WHEN @StudentName = '' THEN @StudentName ELSE a.FullName END LIKE '%'+@StudentName+'%'" +
                            "AND CASE WHEN @Class = '' THEN @Class ELSE b.Class END LIKE '%'+@Class+'%' " +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }
                    
                }

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentID", txtStudentID.Text);
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
                    lblTotalQty.Text = "Total student records = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total student records = " + totalQty;

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
    }
}
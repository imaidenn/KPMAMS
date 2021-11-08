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
    public partial class TeacherListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GetTeacherListing();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherEntry.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "TeacherEntry.aspx" + "?TeacherGUID=" + DataBinder.Eval(e.Row.DataItem, "TeacherGUID");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetTeacherListing();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void GetTeacherListing()
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
                if (Calendar1.Text == "" && Calendar2.Text == "" && txtTeacherID.Text == "" && txtTeacherName.Text == "" )
                {
                    if (ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender, Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher ORDER BY FullName";
                    }
                    else if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender, Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher WHERE Status = 'Active' ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender, Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher WHERE Status = 'Inactive' ORDER BY FullName";
                    }

                }
                else if (Calendar1.Text == "" && Calendar2.Text == "")
                {
                    if (ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%'" +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%'" +
                            "ORDER BY FullName";
                    }
                    else if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE Status = 'Active'" +
                            "AND CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%'" +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%'" +
                            "ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE Status = 'Inactive'" +
                            "AND CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%'" +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%'" +
                            "ORDER BY FullName";
                    }

                }

                else if (Calendar1.Text != "" && Calendar2.Text != "")
                {
                    if (ddlStatus.SelectedItem.Text == "All")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%' " +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%' " +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }
                    else if (ddlStatus.SelectedItem.Text == "Active")
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE Status = 'Active'" +
                            "AND CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%'" +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%'" +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }
                    else
                    {
                        strSelect = "SELECT TeacherGUID, TeacherUserID, FullName, Gender,  Status, CONVERT(varchar,JoinDate,1) as JoinDate FROM Teacher " +
                            "WHERE Status = 'Inactive'" +
                            "AND CASE WHEN @TeacherID = '' THEN @TeacherID ELSE TeacherUserID END LIKE '%'+@TeacherID+'%'" +
                            "AND CASE WHEN @TeacherName = '' THEN @TeacherName ELSE FullName END LIKE '%'+@TeacherName+'%'" +
                            "AND (JoinDate BETWEEN @DateFrom AND @DateTo) ORDER BY FullName";
                    }

                }

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@TeacherID", txtTeacherID.Text);
                cmdSelect.Parameters.AddWithValue("@TeacherName", txtTeacherName.Text);
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
                    lblTotalQty.Text = "Total Teacher records = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Teacher records = " + totalQty;

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

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GetTeacherListing();
        
        //}

        //protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    GetTeacherListing(e.SortExpression);
        //}
    }
}
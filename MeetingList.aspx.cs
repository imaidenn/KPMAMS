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
    public partial class MeetingList : System.Web.UI.Page
    {
        string classGUID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if(Session["userGUID"] != null && Session["role"] != null)
                {
                    GetClass();

                    GetMeetingListing();
                }
                
            }
        }

        protected void GetClass()
        {
            try
            {
                string usertype = Session["role"].ToString();
                string userGUID = Session["userGUID"].ToString();

                if (usertype == "Student")
                {
                    DataTable dt = new DataTable();

                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection con = new SqlConnection(strCon);

                    con.Open();
                    String strSelect = "SELECT ClassroomGUID FROM Student WHERE StudentGUID = @UserGUID";


                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@UserGUID", userGUID);
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                    classGUID = dt.Rows[0][0].ToString();

                    con.Close();
                }
                    
            }
            

            catch (SqlException ex)
            {
                string msg = ex.Message;
                //DisplayAlertMsg("Please fill in the blank");
                DisplayAlertMsg(msg);
            }

        }


        protected void GetMeetingListing()
        {
            try
            {
                string usertype = Session["role"].ToString();
                string userGUID = Session["userGUID"].ToString();

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "";

                if (usertype == "Teacher")
                {
                    strSelect = "SELECT MeetingGUID,MeetingTopic,MeetingTime,Duration,Class FROM Meeting a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID WHERE a.TeacherGUID = @UserGUID AND a.Status = 'Active' AND CONVERT(DATE, MeetingTime) = @Date";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@UserGUID", userGUID);
                    cmdSelect.Parameters.AddWithValue("@Date", (DateTime.Now).ToString("dd-MMM-yyyy"));
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                }
                else if (usertype == "Student")
                {
                    strSelect = "SELECT MeetingGUID,MeetingTopic,MeetingTime,Duration,Class FROM Meeting a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID WHERE a.Status = 'Active' AND CONVERT(DATE, MeetingTime) = @Date";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@Class", classGUID);
                    cmdSelect.Parameters.AddWithValue("@Date", (DateTime.Now).ToString("dd-MMM-yyyy"));
                    SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                    dt.Load(dtrSelect);
                }


                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;


                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Meeting = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Meeting = " + totalQty;

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
                    hyperLink.Attributes["href"] = "MeetingInfo.aspx" + "?MeetingGUID=" + DataBinder.Eval(e.Row.DataItem, "MeetingGUID");
            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }

}

        

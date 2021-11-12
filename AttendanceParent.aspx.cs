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
    public partial class AttendanceParent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userGUID"] != null && Session["role"].ToString() == "Parent")
            {
                GetStudent();
            }
        }

        protected void GetStudent()
        {
            try
            {
                string userGUID = Session["userGUID"].ToString();
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();
                String strSelect = "SELECT StudentGUID,FullName,ICNo,b.Class FROM Student a LEFT JOIN Classroom b ON a.ClassroomGUID = b.ClassroomGUID " +
                    "WHERE ParentGUID = @ParentGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ParentGUID", userGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                object totalQty;
                totalQty = dt.Rows.Count;

                if (dt.Rows.Count == 0)
                {
                    lblNoData.Visible = true;
                    lblTotalQty.Text = "Total Child = 0";
                }
                else
                {
                    lblNoData.Visible = false;
                    lblTotalQty.Text = "Total Child = " + totalQty;
                }

                   GridView1.DataSource = dt;
                   GridView1.DataBind();

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "AttendanceList.aspx" + "?StudentGUID=" + DataBinder.Eval(e.Row.DataItem, "StudentGUID");
            }
        }
    }
}
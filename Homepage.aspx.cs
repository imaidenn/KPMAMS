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
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GetAnnouncementListing();

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                if (hyperLink != null)
                    hyperLink.Attributes["href"] = "AnnouncementDetails.aspx" + "?AnnouncementGUID=" + DataBinder.Eval(e.Row.DataItem, "AnnouncementGUID");
            }
        }

        protected void GetAnnouncementListing()
        {
            try
            {

                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT * FROM Announcement WHERE Status = @Status ORDER BY CreateDate";


                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@Status", "Active");


                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                //object totalQty;
                //totalQty = dt.Rows.Count;


                //if (dt.Rows.Count == 0)
                //{
                //    lblNoData.Visible = true;
                //    lblTotalQty.Text = "Total Announcement records = 0";
                //}
                //else
                //{
                //    lblNoData.Visible = false;
                //    lblTotalQty.Text = "Total Announcement records = " + totalQty;

                //}

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
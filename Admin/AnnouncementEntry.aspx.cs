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
    public partial class AnnouncementEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["AnnouncementGUID"] != null)
                {
                    LoadExistingData();
                }

            }
        }

        protected void LoadExistingData()
        {
            try
            {
                String AnnouncementGUID = Request.QueryString["AnnouncementGUID"];
                DataTable dt = new DataTable();

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT * FROM Announcement WHERE AnnouncementGUID = @AnnouncementGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@AnnouncementGUID", AnnouncementGUID);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    txtTitle.Enabled = false;
                    btnUpdate.Visible = true;
                    btnRemove.Visible = true;
                    btnSave.Visible = false;

                    txtTitle.Text = dt.Rows[0][1].ToString();
                    txtSummernote.Text = dt.Rows[0][2].ToString();

                }
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                //DisplayAlertMsg("Please fill in the blank");
                DisplayAlertMsg("Error when displaying the existing data");
            }
        }

        protected bool NewAnnouncement()
        {
            bool addBool = false;
            try
            {
                String title = txtTitle.Text;
                String desc = txtSummernote.Text;


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "INSERT INTO Announcement " +
                    "VALUES ('" + Guid.NewGuid() + "',@Title,@Desc,@Status,@CreateDate,@LastUpdateDate)";

                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@Title", title);
                cmdInsert.Parameters.AddWithValue("@Desc", desc);
                cmdInsert.Parameters.AddWithValue("@Status", "Active");
                cmdInsert.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }


        protected bool ValidateAdd()
        {
            if (txtTitle.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Title");
                return false;
            }
            if (txtSummernote.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Description");
                return false;
            }


            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateAdd())
            {
                if (NewAnnouncement())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New announcement added succcessful');window.location ='AdminHomepage.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New announcement added failed');window.location ='AdminHomepage.aspx';", true);
                }
            }


        }

        protected bool UpdateAnnouncement()
        {
            bool addBool = false;
            try
            {
                String desc = txtSummernote.Text;


                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "UPDATE Announcement SET AnnouncementDesc = @Desc, LastUpdateDate = @LastUpdateDate WHERE AnnouncementGUID = '" + Guid.Parse(Request.QueryString["AnnouncementGUID"]) + "' ";


                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@Desc", desc);
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }


        protected bool ValidateUpdate()
        {
            if (txtSummernote.Text.Equals(""))
            {
                DisplayAlertMsg("Please enter the Description");
                return false;
            }


            return true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateUpdate())
            {
                if (UpdateAnnouncement())
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Announcement updated succcessful');window.location ='AdminHomepage.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Announcement updated failed');window.location ='AdminHomepage.aspx';", true);
                }
            }
        }

        protected bool RemoveAnnouncement()
        {
            bool addBool = false;
            try
            {

                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strInsert = "UPDATE Announcement SET Status = @Status ,LastUpdateDate = @LastUpdateDate WHERE AnnouncementGUID = '" + Guid.Parse(Request.QueryString["AnnouncementGUID"]) + "' ";


                SqlCommand cmdInsert = new SqlCommand(strInsert, con);

                cmdInsert.Parameters.AddWithValue("@Status", "Inactive");
                cmdInsert.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmdInsert.ExecuteNonQuery();

                con.Close();
                addBool = true;
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
            }
            return addBool;
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (RemoveAnnouncement())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Announcement remove succcessful');window.location ='AdminHomepage.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Announcement remove failed');window.location ='AdminHomepage.aspx';", true);
            }
        }

        protected void DisplayAlertMsg(String msg)
        {
            String myScript = String.Format("alert('{0}');", msg);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", myScript, true);
        }
    }
}
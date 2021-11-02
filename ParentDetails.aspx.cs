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
    public partial class ParentDetails : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["userGUID"] != null)
                {
                    LoadExistingData();
                    LoadStudent();
                }

            }
        }

        private void LoadStudent()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                String strSelect =
                    "SELECT StudentUserID,b.fullName,b.ICNo,class " +
                    "FROM Parent a LEFT JOIN Student b ON a.ParentGUID=b.ParentGUID " +
                    "LEFT JOIN Classroom c ON b.ClassroomGUID=c.ClassroomGUID " +
                    "WHERE a.ParentGUID=@ParentGUID";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@ParentGUID",Session["userGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

                GvStudentList.DataSource = dt;
                GvStudentList.DataBind();

                con.Close();
            }
            catch (SqlException ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void LoadExistingData()
        {
            try
            {
                String StudentGUID = Request.QueryString["userGUID"];
                DataTable dt = new DataTable();

                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect =
                    "SELECT ParentUserID,fullName,ICNo " +
                    "FROM Parent " +
                    "WHERE ParentGUID = @ParentGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@ParentGUID", Session["userGUID"]);

                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                con.Close();

                if (dt.Rows.Count > 0)
                {
                    txtParentID.Text = dt.Rows[0][0].ToString();
                    txtName.Text = dt.Rows[0][1].ToString();
                    txtICno.Text = dt.Rows[0][2].ToString();
                    
                }
            }
            catch (SqlException ex)
            {
                string msg = ex.Message;
                Response.Write("<script language='javascript'>alert('Error to get profile details');</script>");
                Server.Transfer("Homepage.aspx", true);
            }
        }

    }
}
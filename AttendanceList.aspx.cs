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
    public partial class AttendanceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false)
            {
                if(Session["userGUID"] != null)
                {
                    GetAttendance();
                }
                
            }
        }

        protected void GetAttendance()
        {
            try
            {
                string userGUID = Session["userGUID"].ToString();
                string strPanels = "";

                DataTable dt = new DataTable();
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                con.Open();

                String strSelect = "SELECT COUNT(a.AttendanceGUID) AS TotalClass,SUM(CASE WHEN a.Status = 'Present' THEN 1 ELSE 0 END) AS TotalAttend,a.SubjectGUID,b.SubjectName FROM Attendance a LEFT JOIN Subject b ON a.SubjectGUID = b.SubjectGUID WHERE a.StudentGUID = @StudentGUID " +
                    "GROUP BY b.SubjectName,a.SubjectGUID";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@StudentGUID", userGUID);
                SqlDataReader dtrSelect = cmdSelect.ExecuteReader();

                dt.Load(dtrSelect);

                if(dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {   
                        string subjectName = row["SubjectName"].ToString();
                        string subjectGUID = row["SubjectGUID"].ToString();
                        int TotalClass = int.Parse(row["TotalClass"].ToString());
                        int TotalAttend = int.Parse(row["TotalAttend"].ToString());

                        int percent = (TotalAttend / TotalClass) * 100;
                        double radialPercent = Math.Round(double.Parse((percent / 5).ToString())) * 5;
                        string strRadialType = "";

                        if(radialPercent <= 25)
                        {
                            strRadialType = "radial-bar-danger";
                        }
                        else if(radialPercent <= 50)
                        {
                            strRadialType = "radial-bar-warning";
                        }
                        else if (radialPercent <= 75)
                        {
                            strRadialType = "radial-bar-info";
                        }
                        else
                        {
                            strRadialType = "radial-bar-success";
                        }

                        strPanels += "  <div class=\"col-xs-6\">";
                        strPanels += "      <div class=\"panel panel-default\">";
                        strPanels += "          <div class=\"panel-heading\">" + subjectName + "<a class=\"pull-right\" href=\"AttendanceDetails.aspx?SubjectGUID=" + subjectGUID + "\">To attendance details</a></div>";
                        strPanels += "          <div class=\"panel-body text-center\">";
                        strPanels += "              <div class=\"row d-flex justify-content-center\">";
                        strPanels += "                  <div class=\"col-md-3\">";
                        strPanels += "                      <div data-label=\"" + percent.ToString() + "\" Class=\"radial-bar radial-bar-" + radialPercent.ToString() + " radial-bar-lg " + strRadialType + "\"></div>";
                        strPanels += "                  </div>";
                        strPanels += "                  <div class=\"col-md-9\">";
                        strPanels += "                      <div class=\"col-md-6\">";
                        strPanels += "                          <h3> <span class=\"small text-gray\">Total classes</span></h3>";
                        strPanels += "                          <div class=\"font-weight-bold mb-0\">" + TotalClass.ToString() + "</div>";
                        strPanels += "                      </div>";
                        strPanels += "                      <div class=\"col-md-6\">";
                        strPanels += "                          <h3> <span class=\"small text-gray\">Attended classes</span></h3>";
                        strPanels += "                          <div class=\"font-weight-bold mb-0\">" + TotalAttend.ToString() + "</div>";
                        strPanels += "                      </div>";
                        strPanels += "                  </div>";
                        strPanels += "              </div>";
                        strPanels += "          </div>";
                        strPanels += "      </div>";
                        strPanels += "  </div>";
                    }
                }

                con.Close();


                litAttendancePanels.Text = strPanels;

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }


    }
}
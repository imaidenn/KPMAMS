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
    public partial class TimetableDetails : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                if (!(Session["userGUID"] != null))
                {
                    Response.Redirect("Login.aspx");
                }
                BindTimetable();
                BindGridView();

            }
        }
        private void BindTimetable()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                String strSelect =
                        "SELECT Class,convert(VARCHAR(20),b.CreateDate,100), convert(VARCHAR(20),b.LastUpdateDate,100) " +
                        "FROM Classroom a LEFT JOIN Timetable b ON a.TimetableGUID=b.TimetableGUID " +
                        "WHERE b.TimetableGUID=@TimetableGUID";
                cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    con.Close();
                    lbClass.Text = "Class: " + dt.Rows[0][0].ToString();
                    lbCreateDate.Text = "Create on " + dt.Rows[0][1].ToString();
                    lbLastUpdateDate.Text = "Last update " + dt.Rows[0][2].ToString();
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Error');</script>");
                    Server.Transfer("Homepage.aspx", true);
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dt = new DataTable();
                strSelect =
                    "SELECT TimetableGUID," +
                    "CASE " +
                    "WHEN Day ='1' THEN 'Monday' " +
                    "WHEN Day ='2' THEN 'Tuesday' " +
                    "WHEN Day ='3' THEN 'Wenesday' " +
                    "WHEN Day ='4' THEN 'Thursday' " +
                    "WHEN Day ='5' THEN 'Friday' " +
                    "END AS Day2," +
                    "Timeslot1,Timeslot2,Timeslot3,Timeslot4,Timeslot5,Timeslot6,Timeslot7,Timeslot8,Timeslot9,Timeslot10 " +
                    "FROM TimetableSubject " +
                    "WHERE TimetableGUID=@TimetableGUID " +
                    "ORDER BY Day";
                cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = CheckSubject(dt.Rows[i][2].ToString());
                    dt.Rows[i][3] = CheckSubject(dt.Rows[i][3].ToString());
                    dt.Rows[i][4] = CheckSubject(dt.Rows[i][4].ToString());
                    dt.Rows[i][5] = CheckSubject(dt.Rows[i][5].ToString());
                    dt.Rows[i][6] = CheckSubject(dt.Rows[i][6].ToString());
                    dt.Rows[i][7] = CheckSubject(dt.Rows[i][7].ToString());
                    dt.Rows[i][8] = CheckSubject(dt.Rows[i][8].ToString());
                    dt.Rows[i][9] = CheckSubject(dt.Rows[i][9].ToString());
                    dt.Rows[i][10] = CheckSubject(dt.Rows[i][10].ToString());
                    dt.Rows[i][11] = CheckSubject(dt.Rows[i][11].ToString());

                }

                GvTimetable.DataSource = dt;
                GvTimetable.DataBind();
                GridRow(GvTimetable);

            }
            catch (SqlException ex)
            {

                string msg = ex.Message;
                Response.Write(msg);
            }
        }

        private string CheckSubject(String subjectGuid)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strCon);
            con.Open();
            String strSelect =
                "SELECT SubjectGUID, SubjectName, " +
                "CASE " +
                "WHEN SubjectName ='English' THEN 'BI' " +
                "WHEN SubjectName ='Kimia' THEN 'KM' " +
                "WHEN SubjectName ='Matematik' THEN 'MM' " +
                "WHEN SubjectName ='Pendidikan Seni' THEN 'PS' " +
                "WHEN SubjectName ='Pendidikan Moral' THEN 'PM' " +
                "WHEN SubjectName ='Sejarah' THEN 'SJ' " +
                "WHEN SubjectName ='Biologi' THEN 'BIO' " +
                "WHEN SubjectName ='Prinsip Akaun' THEN 'PA' " +
                "WHEN SubjectName ='Ekonomi' THEN 'EA' " +
                "WHEN SubjectName ='Kemahiran Hidup bersepadu' THEN 'KH' " +
                "WHEN SubjectName ='Bahasa Malaysia' THEN 'BM' " +
                "WHEN SubjectName ='Sains' THEN 'SC' " +
                "WHEN SubjectName ='Matematik Tambahan' THEN 'MT' " +
                "WHEN SubjectName ='Fizik' THEN 'FZ' " +
                "WHEN SubjectName ='Geografi' THEN 'GEO' " +
                "WHEN SubjectName ='Pendidikan Islam' THEN 'PI' " +
                "WHEN SubjectName ='Bahasa Cina' THEN 'BC' " +
                "END AS Shortform " +
                "FROM Subject";
            SqlCommand cmd = new SqlCommand(strSelect, con);

            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == subjectGuid)
                {
                    return dt.Rows[i][2].ToString();
                }
            }
            return "";

        }

        public void GridRow(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 1; rowIndex >= 0; rowIndex--)
            {
                for (int i = 0; i < gridView.Rows[rowIndex].Cells.Count - 1; i++)
                {
                    TableCell oldTc = gridView.Rows[rowIndex].Cells[i];
                    TableCell tc = gridView.Rows[rowIndex].Cells[i + 1];
                    if (oldTc.Text == tc.Text)
                    {
                        oldTc.ColumnSpan = tc.ColumnSpan < 2 ? 2 : tc.ColumnSpan + 1;
                        tc.Visible = false;
                        oldTc.HorizontalAlign = HorizontalAlign.Center;

                    }

                }
            }
        }

        private void BindGridView()
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
                    "SELECT SubjectGUID, SubjectName, " +
                    "CASE " +
                    "WHEN SubjectName ='English' THEN 'BI' " +
                    "WHEN SubjectName ='Kimia' THEN 'KM' " +
                    "WHEN SubjectName ='Matematik' THEN 'MM' " +
                    "WHEN SubjectName ='Pendidikan Seni' THEN 'PS' " +
                    "WHEN SubjectName ='Pendidikan Moral' THEN 'PM' " +
                    "WHEN SubjectName ='Sejarah' THEN 'SJ' " +
                    "WHEN SubjectName ='Biologi' THEN 'BIO' " +
                    "WHEN SubjectName ='Prinsip Akaun' THEN 'PA' " +
                    "WHEN SubjectName ='Ekonomi' THEN 'EA' " +
                    "WHEN SubjectName ='Kemahiran Hidup bersepadu' THEN 'KH' " +
                    "WHEN SubjectName ='Bahasa Malaysia' THEN 'BM' " +
                    "WHEN SubjectName ='Sains' THEN 'SC' " +
                    "WHEN SubjectName ='Matematik Tambahan' THEN 'MT' " +
                    "WHEN SubjectName ='Fizik' THEN 'FZ' " +
                    "WHEN SubjectName ='Geografi' THEN 'GEO' " +
                    "WHEN SubjectName ='Pendidikan Islam' THEN 'PI' " +
                    "WHEN SubjectName ='Bahasa Cina' THEN 'BC' " +
                    "END AS Shortform " +
                    "FROM Subject";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                GvSubject.DataSource = dt;
                GvSubject.DataBind();
                dt = new DataTable();

                strSelect =
                    "SELECT FullName,SubjectName " +
                    "FROM Teacher a LEFT JOIN Teacher_Classroom b ON a.TeacherGUID=b.TeacherGUID " +
                    "LEFT JOIN Classroom c ON b.ClassroomGUID=c.ClassroomGUID " +
                    "LEFT JOIN Subject_Classroom d ON c.ClassroomGUID=d.ClassroomGUID " +
                    "LEFT JOIN Subject e ON d.SubjectGUID=e.SubjectGUID " +
                    "WHERE SubjectTeach=d.SubjectGUID AND c.TimetableGUID=@TimetableGUID";

                cmd = new SqlCommand(strSelect, con);
                cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();
                GvSubjectTeach.DataSource = dt;
                GvSubjectTeach.DataBind();
                con.Close();
            }
            catch (SqlException ex)
            {

                string msg = ex.Message;
                Response.Write(msg);
            }
        }

    }
}
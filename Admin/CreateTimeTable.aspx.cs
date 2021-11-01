using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class CreateTimeTable : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["userGUID"] == null))
            {
                Response.Redirect("Login.aspx");
            }
            if (IsPostBack == false)
            {
                BindGridView();
                BindClasses();
                if (Request.QueryString["TimetableGUID"] == null)
                {
                    btnCreate.Attributes["class"] = "btn btn-primary disabled";
                    btnReset.Attributes["class"] = "btn btn-warning disabled";
                }
                else {
                    BindSubject();
                    btnCreate.Text = "Update";
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

        private void BindClasses()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string strSelect = "";

                if (Request.QueryString["TimetableGUID"] == null)
                {
                    strSelect = "SELECT ClassroomGUID, Class " +
                        "FROM Classroom " +
                        "WHERE TimeTableGUID IS NULL";
                }
                else {
                    strSelect = "SELECT ClassroomGUID, Class " +
                            "FROM Classroom " +
                            "WHERE TimeTableGUID='" + Request.QueryString["TimetableGUID"] + "';";
                }

                SqlCommand cmd = new SqlCommand(
                    strSelect, con);
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    dt.Load(dr);
                    dlClassList.DataTextField = "Class";
                    dlClassList.DataValueField = "ClassroomGUID";
                    dlClassList.DataSource = dt;
                    dlClassList.DataBind();
                    if (Request.QueryString["TimetableGUID"] == null)
                        dlClassList.Items.Insert(0, new ListItem("---Please Select---", String.Empty));
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Error:No Class, can't create forum');</script>");
                    Server.Transfer("AssessmentList.aspx", true);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void dlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlClassList.SelectedValue != "")
            {
                BindSubject();
                btnCreate.Attributes["class"] = "btn btn-primary";
                btnReset.Attributes["class"] = "btn btn-warning";
            }
            else {
                btnCreate.Attributes["class"] = "btn btn-primary disabled";
                btnReset.Attributes["class"] = "btn btn-warning disabled";
            }
        }

        private void BindSubject()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(
                    "SELECT a.SubjectGUID, " +
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
                    "END AS SubjectName " +
                    "FROM Subject a " +
                    "LEFT JOIN Subject_Classroom b ON a.SubjectGUID=b.SubjectGUID " +
                    "WHERE ClassroomGUID=@ClassroomGUID", con);

                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue) ;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                DataTable dt2 = new DataTable();
                if (Request.QueryString["TimetableGUID"] != null) {
                    string strSelect =
                        "SELECT " +
                        "Timeslot1,Timeslot2,Timeslot3,Timeslot4,Timeslot5,Timeslot6,Timeslot7,Timeslot8,Timeslot9,Timeslot10,Day " +
                        "FROM TimetableSubject " +
                        "WHERE TimetableGUID=@TimetableGUID " +
                        "ORDER BY Day";
                    cmd = new SqlCommand(strSelect, con);
                    cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                    dr = cmd.ExecuteReader();
                    dt2.Load(dr);
                }
                


                int d = 1;
                int t = 1;
                int r = 0;
                int c = 0;
                for (int i = 0; i < 50; i++)
                {
                    if (i == 10 || i == 20 || i == 30 || i == 40) {
                        d++;
                    }
                    if (t > 10) {
                        t = 1;
                    }

                    string a = "d" + d + "t" + t;
                    //DropDownList dropdown = FindControl(a) as DropDownList;
                    DropDownList dropdown = this.Master.FindControl("BodyContent").FindControl(a) as DropDownList;
                    dropdown.DataTextField = "SubjectName";
                    dropdown.DataValueField = "SubjectGUID";
                    dropdown.DataSource = dt;
                    dropdown.DataBind();
                    dropdown.Items.Insert(0, new ListItem("", String.Empty));
                    t++;
                    if (Request.QueryString["TimetableGUID"] != null)
                    {
                        if (c == 10) {
                            c = 0;
                            r++;
                        }
                        dropdown.SelectedValue = dt2.Rows[r][c].ToString();
                        c++;
                    }

                }
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (btnCreate.Text == "Update")
            {
                UpdateTimetable();
            }
            else {

                CreateTimetable();
            }
        }

        private void getEmail()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(strCon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(
                "SELECT fullName,email " +
                "FROM Student " +
                "WHERE ClassroomGUID=@ClassroomGUID", con);

            cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            DataTable dt2 = new DataTable();

            string strSelect =
                "SELECT fullName,email " +
                "FROM Teacher a LEFT JOIN Teacher_Classroom b ON a.TeacherGUID=b.TeacherGUID " +
                "WHERE ClassroomGUID=@ClassroomGUID";
            cmd = new SqlCommand(strSelect, con);
            cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);
            dr = cmd.ExecuteReader();
            dt2.Load(dr);

            if (dt.Rows.Count != 0) {
                for (int i = 0; i < dt.Rows.Count; i++) {
                    sendEmail(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }
            }
            if (dt2.Rows.Count != 0) {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sendEmail(dt2.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }
            }

            con.Close();

        }

        private void sendEmail(string name, string email)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("kpmnoreply@gmail.com", "kpm12345");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("kpmnoreply@gmail.com");

            String body = 
                "<b>Hello " + name + "</b><br />" +
                "Timetable for class "+dlClassList.SelectedItem+" have been "+btnCreate.Text.ToLower()+"d, now you can view the timetable at the system <br /><br />" +
                "KPM Academic Mamagement System.";
            mail.To.Add(email);
            mail.Subject = "Timetable for " +dlClassList.SelectedItem+"";
            mail.IsBodyHtml = true;
            mail.Body = body;

            client.Send(mail);
        }

        private void UpdateTimetable()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);

                int d = 1;
                for (int i = 0; i < 5; i++)
                {

                    DropDownList dropdown1 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t1") as DropDownList;
                    DropDownList dropdown2 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t2") as DropDownList;
                    DropDownList dropdown3 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t3") as DropDownList;
                    DropDownList dropdown4 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t4") as DropDownList;
                    DropDownList dropdown5 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t5") as DropDownList;
                    DropDownList dropdown6 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t6") as DropDownList;
                    DropDownList dropdown7 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t7") as DropDownList;
                    DropDownList dropdown8 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t8") as DropDownList;
                    DropDownList dropdown9 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t9") as DropDownList;
                    DropDownList dropdown10 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t10") as DropDownList;

                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                      "UPDATE Timetable " +
                      "SET " +
                      "LastUpdateDate=@LastUpdateDate " +
                      "WHERE TimetableGUID=@TimetableGUID", con);
                    cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                    cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(
                       "UPDATE TimetableSubject " +
                       "SET " +
                       "TimeSlot1=@TimeSlot1," +
                       "TimeSlot2=@TimeSlot2," +
                       "TimeSlot3=@TimeSlot3," +
                       "TimeSlot4=@TimeSlot4," +
                       "TimeSlot5=@TimeSlot5," +
                       "TimeSlot6=@TimeSlot6," +
                       "TimeSlot7=@TimeSlot7," +
                       "TimeSlot8=@TimeSlot8," +
                       "TimeSlot9=@TimeSlot9," +
                       "TimeSlot10=@TimeSlot10 " +
                   "WHERE TimetableGUID=@TimetableGUID AND Day=@Day", con);
                    cmd.Parameters.AddWithValue("@TimetableGUID", Request.QueryString["TimetableGUID"]);
                    cmd.Parameters.AddWithValue("@Day", d);
                    cmd.Parameters.AddWithValue("@TimeSlot1", dropdown1.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot2", dropdown2.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot3", dropdown3.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot4", dropdown4.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot5", dropdown5.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot6", dropdown6.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot7", dropdown7.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot8", dropdown8.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot9", dropdown9.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot10", dropdown10.SelectedValue);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    d++;
                }

                getEmail();
                Response.Write("<script language='javascript'>alert('Timetable update successfully');</script>");
                Server.Transfer("TimetableList_Admin.aspx", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void CreateTimetable()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Guid TimetableGUID = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Timetable(TimetableGUID,CreateDate,LastUpdateDate) " +
                    "values (@TimetableGUID,@CreateDate,@LastUpdateDate)", con);
                cmd.Parameters.AddWithValue("@TimetableGUID", TimetableGUID);
                cmd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@LastUpdateDate", DateTime.Now);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                con.Open();

                cmd = new SqlCommand("" +
                                "UPDATE Classroom SET TimetableGUID=@TimetableGUID " +
                                "WHERE ClassroomGUID=@ClassroomGUID", con);
                cmd.Parameters.AddWithValue("@TimetableGUID", TimetableGUID);
                cmd.Parameters.AddWithValue("@ClassroomGUID", dlClassList.SelectedValue);

                cmd.ExecuteNonQuery();
                con.Close();
                
                int d = 1;
                for (int i = 0; i < 5; i++)
                {

                    DropDownList dropdown1 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t1") as DropDownList;
                    DropDownList dropdown2 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t2") as DropDownList;
                    DropDownList dropdown3 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t3") as DropDownList;
                    DropDownList dropdown4 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t4") as DropDownList;
                    DropDownList dropdown5 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t5") as DropDownList;
                    DropDownList dropdown6 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t6") as DropDownList;
                    DropDownList dropdown7 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t7") as DropDownList;
                    DropDownList dropdown8 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t8") as DropDownList;
                    DropDownList dropdown9 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t9") as DropDownList;
                    DropDownList dropdown10 = this.Master.FindControl("BodyContent").FindControl("d" + d + "t10") as DropDownList;

                    con.Open();

                    cmd = new SqlCommand(
                    "INSERT INTO TimetableSubject(TimetableSubjectGUID,TimetableGUID,Day,TimeSlot1,TimeSlot2,TimeSlot3,TimeSlot4,TimeSlot5,TimeSlot6,TimeSlot7,TimeSlot8,TimeSlot9,TimeSlot10) " +
                    "values (newid(),@TimetableGUID,@Day,@TimeSlot1,@TimeSlot2,@TimeSlot3,@TimeSlot4,@TimeSlot5,@TimeSlot6,@TimeSlot7,@TimeSlot8,@TimeSlot9,@TimeSlot10)", con);
                    cmd.Parameters.AddWithValue("@TimetableGUID", TimetableGUID);
                    cmd.Parameters.AddWithValue("@Day", d);
                    cmd.Parameters.AddWithValue("@TimeSlot1", dropdown1.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot2", dropdown2.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot3", dropdown3.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot4", dropdown4.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot5", dropdown5.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot6", dropdown6.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot7", dropdown7.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot8", dropdown8.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot9", dropdown9.SelectedValue);
                    cmd.Parameters.AddWithValue("@TimeSlot10", dropdown10.SelectedValue);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    d++;
                }

                getEmail();
                Response.Write("<script language='javascript'>alert('Timetable created successfully');</script>");
                Server.Transfer("TimetableList_Admin.aspx", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TimetableList_Admin.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            int d = 1;
            int t = 1;
            for (int i = 0; i < 50; i++)
            {
                if (i == 10 || i == 20 || i == 30 || i == 40)
                {
                    d++;
                }
                if (t > 10)
                {
                    t = 1;
                }
                DropDownList dropdown = this.Master.FindControl("BodyContent").FindControl("d" + d + "t" + t) as DropDownList;
                dropdown.SelectedValue = "";
                t++;
            }
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;



namespace KPMAMS
{
    public partial class LiveChat : System.Web.UI.Page
    {
        public string UserName = "admin";
        public string UserImage = "img/logoKPM.png";
        public string UploadFolderPath = "~/ChatFile/";
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["userGUID"] != null))
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["role"].Equals("Student")) {
                Response.Redirect("Homepage.aspx");
            }
            if (Session["fullName"] != null)
            {
                //string filepath = Server.MapPath(UploadFolderPath);
                //System.IO.DirectoryInfo di = new DirectoryInfo(filepath);

                //foreach (FileInfo file in di.EnumerateFiles())
                //{
                //    file.Delete();
                //}
                //foreach (DirectoryInfo dir in di.EnumerateDirectories())
                //{
                //    dir.Delete(true);
                //}

                UserName = Session["fullName"].ToString();
                if (Session["role"].Equals("Teacher"))
                {
                    GetUserImage(UserName);
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
        public void GetUserImage(string Username)
        {
            try {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT ProfilePic from Teacher where TeacherGUID='" + Session["userGUID"] + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserImage = ConfigurationManager.AppSettings["ProfileUploadPath"].ToString() + dr.GetValue(0).ToString();
                    }
                }
                con.Close();
            } catch (Exception ex) {

                Response.Write(ex.Message);

            }
        }

        private MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
        }

    }

}
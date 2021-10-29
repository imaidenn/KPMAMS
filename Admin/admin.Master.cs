﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(Session["role"].Equals("Admin")))
                {
                    Response.Write("<script language='javascript'>alert('This page is available for admin only');</script>");
                    Server.Transfer("Login.aspx", true);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["role"] = "";
            Response.Redirect("AdminLogin.aspx");
        }
    }
}
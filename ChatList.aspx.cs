using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPMAMS
{
    public partial class ChatList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GvCurrentChat_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnGroupChat_Click(object sender, EventArgs e)
        {
            Response.Redirect("LiveChat.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE_PROPOSAL
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = Request.QueryString["s"];
                Session.Remove("s");
                Session.Add("s", s);
                Response.Redirect("Welcome.aspx");
            }
        }
    }
}
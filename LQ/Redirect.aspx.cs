using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LQ
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

                if (Request.Browser.IsMobileDevice)
                    Response.Redirect("Mobile/MobileDashboard.aspx");
                else
                    Response.Redirect("Welcome.aspx");
            }
        }
    }
}
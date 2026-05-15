using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGR
{
    public partial class Frame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                IF.Src = Crypto.DecryptStringAES(Request.QueryString["URL"]);
            }
        }
    }
}
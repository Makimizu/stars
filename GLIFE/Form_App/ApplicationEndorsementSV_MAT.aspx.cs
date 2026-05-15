using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE.Form_App
{
    public partial class ApplicationEndorsementSV_MAT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationEndorsementGeneric.aspx?REGNO=" + Request.QueryString["REGNO"].ToString() + "&SEQ=" + Request.QueryString["SEQ"].ToString() + "&TYPE=SV_MAT");
        }
    }
}
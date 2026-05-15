using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GLIFE_PROPOSAL
{
    public partial class SubmissionMode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99" || GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "25")
                {
                    trow1.Visible = false;
                    trow2.Visible = false;
                    trow3.Visible = false;
                    trow4.Visible = false;
                }
            }
        }
    }
}
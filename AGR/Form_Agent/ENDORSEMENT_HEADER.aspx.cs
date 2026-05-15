using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGR.Form_Agent
{
    public partial class ENDORSEMENT_HEADER : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mode = "";
            mode = Request.QueryString["mode"].ToString();
            switch (mode)
            {
                case "0": LB_TITLE.Text = "ENDORSEMENT SUBMISSION"; break;
                case "1": LB_TITLE.Text = "ENDORSEMENT APPROVAL"; break;
                case "2": LB_TITLE.Text = "ENDORSEMENT HISTORY"; break;
            }
        }
    }
}
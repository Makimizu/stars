using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE.Form_App
{
    public partial class ApplicationEndorsementSV_CAN : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_SAVING_CANCELATION_ENDORSEMENT " +
                                "'" + Request.QueryString["REGNO"].ToString() + "'," +
                                Request.QueryString["SEQ"].ToString() + "," +
                                "'SV_CAN'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            Response.Redirect("ApplicationEndorsementGeneric.aspx?REGNO=" + Request.QueryString["REGNO"].ToString() + "&SEQ=" + Request.QueryString["SEQ"].ToString() + "&TYPE=SV_CAN");
        }
    }
}
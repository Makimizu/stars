using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class Endorsement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                bDone = TrackDone();

                CheckRedirect();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_TYPE.Text + "-" + LB_SEQ.Text + "')";
            conn.ExecuteQuery();

            if (int.Parse(conn.GetFieldValue("TRACK").ToString()) < 3)
                result = false;

            return result;
        }

        protected void CheckRedirect()
        {
            conn.QueryString = "select CODE from V_LINK_UB_PARAM_ENDORSEMENT where CODE = '" + LB_TYPE.Text + "' and GENERAL_PROCESS = 1";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                Response.Redirect("Endorsement" + LB_TYPE.Text + ".aspx?regno=" + LB_REGNO.Text + "&type=" + LB_TYPE.Text + "&seq=" + LB_SEQ.Text);
            }
        }
    }
}
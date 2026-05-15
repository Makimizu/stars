using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace REAS.Form_Tools
{
    public partial class MultiScreensHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["mode"];
                Setup();
                if (DDL_SCREENS.Items.Count > 0)
                    ShowScreen();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select URL, DESCR = UPPER(DESCR) from REINSURANCE.dbo.V_MULTI_SCREEN where MODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SCREENS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            ShowScreen();
        }

        protected void ShowScreen()
        {
            string URL = "default.html";
            if (DDL_SCREENS.SelectedValue.Trim() != "")
            {
                URL = DDL_SCREENS.SelectedValue.Trim();
                LBL_TITLE.Text = DDL_SCREENS.SelectedItem.Text;
            }
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.multiscreensbody.location.href = '" + URL + "';</script>");
        }

        protected void DDL_SCREENS_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ShowScreen();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;


namespace GO
{
    public partial class MultiScreensHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"];
                Setup();
                if ( DDL_SCREENS.Items.Count > 0)
                    ShowScreen();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CAPTION from M_MULTISCREENS where CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            LB_CAPTION.Text = conn.GetFieldValue("CAPTION").ToString() + " :";

            conn.QueryString = "select URL,DESCR = UPPER(DESCR) from V_M_MULTISCREENS_LIST where CODE = '" + LB_CODE.Text + "' order by SEQ";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SCREENS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
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
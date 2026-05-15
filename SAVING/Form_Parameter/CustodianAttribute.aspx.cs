using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace SAVING.Form_Parameter
{
    public partial class CustodianAttribute : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                LoadRecord();
            }
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "ATTRIBUTE01, " +
                                "ATTRIBUTE02, " +
                                "ATTRIBUTE03, " +
                                "ATTRIBUTE04, " +
                                "ATTRIBUTE05, " +
                                "ATTRIBUTE06, " +
                                "ATTRIBUTE07, " +
                                "ATTRIBUTE08, " +
                                "ATTRIBUTE09, " +
                                "ATTRIBUTE10 " +
                                "from V_CUSTODIAN_MASTER " +
                                "where " +
                                "COMPANY_CODE = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT1.Text = conn.GetFieldValue("ATTRIBUTE01").ToString();
                TXT2.Text = conn.GetFieldValue("ATTRIBUTE02").ToString();
                TXT3.Text = conn.GetFieldValue("ATTRIBUTE03").ToString();
                TXT4.Text = conn.GetFieldValue("ATTRIBUTE04").ToString();
                TXT5.Text = conn.GetFieldValue("ATTRIBUTE05").ToString();
                TXT6.Text = conn.GetFieldValue("ATTRIBUTE06").ToString();
                TXT7.Text = conn.GetFieldValue("ATTRIBUTE07").ToString();
                TXT8.Text = conn.GetFieldValue("ATTRIBUTE08").ToString();
                TXT9.Text = conn.GetFieldValue("ATTRIBUTE09").ToString();
                TXT10.Text = conn.GetFieldValue("ATTRIBUTE10").ToString();
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_CUSTODIAN_MASTER_UPDATE " +
                                "'" + LB_ID.Text + "',"+
                                "'" + TXT1.Text.Trim() + "'," +
                                "'" + TXT2.Text.Trim() + "'," +
                                "'" + TXT3.Text.Trim() + "'," +
                                "'" + TXT4.Text.Trim() + "'," +
                                "'" + TXT5.Text.Trim() + "'," +
                                "'" + TXT6.Text.Trim() + "'," +
                                "'" + TXT7.Text.Trim() + "'," +
                                "'" + TXT8.Text.Trim() + "'," +
                                "'" + TXT9.Text.Trim() + "'," +
                                "'" + TXT10.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            LoadRecord();
        }

    }
}
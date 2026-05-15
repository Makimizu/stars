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
    public partial class InvestmentRateHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                ShowRate();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where PAYDI = 1 and UNITIZE = 0";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR = replace(CODE, '1', 'I') + ' - ' + UPPER(DESCR) from PR_CURRENCY";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void ShowRate()
        {
            conn.QueryString = "select CODE from SECURITY.dbo.REPORT_LIST where APP_ID = 'SV' and REPORT_NAME = 'RPT_INVESTMENT_RATE'";
            conn.ExecuteQuery();

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.InvRateBody.location.href = '../../ReportViewer/Viewer.aspx?APPID=SV&CODE=" + conn.GetFieldValue("CODE").ToString() + "';</script>");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_INVESTMENT_RATE_UPSERT " +
                                    "'" + DDL_GROUP.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_CURRENCY.SelectedValue + "'," +
                                    "'" + TXT_RATE.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }
            ShowRate();
        }
    }
}
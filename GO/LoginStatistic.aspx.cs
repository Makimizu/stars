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
    public partial class LoginStatistic : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }

        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_LOGIN_STATISTIC_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                            "FIRSTDATE = convert(varchar(20), dateadd(day, -(DAY(GETDATE()))+1, GETDATE()), 103), " +
                                            "TODAY = convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_DATE1.Text = conn.GetFieldValue("FIRSTDATE").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("TODAY").ToString();            
        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RESULT.Text = "";


            conn.QueryString = "exec SSP_LOG_STATISTIC " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + DDL_STAT.SelectedValue + "'";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            if(conn.GetRowCount() > 0)
                BT_XLS.Visible = true;

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }
    }
}
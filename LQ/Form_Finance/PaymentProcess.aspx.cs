using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Finance
{
    public partial class PaymentProcess : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Setup();
            }
        }

        protected void Setup()
        {
            TXT_STARTDATE1.Text = "1/1/" + System.DateTime.Now.Year.ToString();
            TXT_STARTDATE2.Text = System.DateTime.Now.Day.ToString() + "/" + System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Year.ToString();

            conn.QueryString = "select " +
                                "CODE		= a.APP_ID + a.CODE, " +
                                "DESCR " +
                                "from		FINANCE.dbo.PARAM_TIPE_SETTLEMENT a " +
                                "where " +
                                "a.APP_ID in ('LF','AGR') " +
                                "order by " +
                                "a.APP_ID desc, 2";
            conn.ExecuteQuery();
            DDL_TRANSTYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANSTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string startdate = "null";
            string enddate = "null";
            string stat = "null";

            if (TXT_STARTDATE1.Text.Trim() != "")
                startdate = "'" + TXT_STARTDATE1.Text + "'";

            if (TXT_STARTDATE2.Text.Trim() != "")
                enddate = "'" + TXT_STARTDATE2.Text + "'";

            if (DDL_STAT.SelectedValue != "")
                stat = DDL_STAT.SelectedValue;

            conn.QueryString = "exec SP_LINK_FINANCE_SETTLEMENT_DETAIL " +
                                "'" + Session["s"].ToString() + "'," +
                                startdate + "," +
                                enddate + "," +
                                stat + "," +
                                "'" + DDL_TRANSTYPE.SelectedValue + "'," +
                                "'" + TXT_DESCR.Text.Trim().Replace("'", "`") + "'";

            conn.ExecuteQuery(500000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
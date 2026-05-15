using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Data
{
    public partial class DataProduction : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string userid, roleid;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_YEAR.Text = Request.QueryString["YEAR"].ToString();
                LB_MONTH.Text = Request.QueryString["MONTH"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select MON = DATENAME(MONTH,'" + LB_MONTH.Text + "/1/" + LB_YEAR.Text + "')";
            conn.ExecuteQuery();
            LB_TITLE.Text = "PRODUCTION DATA : <B>" + conn.GetFieldValue("MON").ToString() + " " + LB_YEAR.Text + "</B>";

            conn.QueryString = "select " +
                                "CODE, DESCR " +
                                "from		UWBOX.dbo.PARAM_PRODUCT_GROUP " +
                                "where " +
                                "SEGMENT = 0 " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_PRODUCTGROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCTGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select " +
                                "CODE, DESCR " +
                                "from		UWBOX.dbo.PR_TRANSACTION_TYPE " +
                                "where " +
                                "CODE not in ('CONNON','CONREG','PRM','TOPNON','TOPREG','YRT') " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_TRANSTYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANSTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            userid = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            roleid = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");

            string where = "";

            if (TXT_PRODUCTNAME.Text.Trim() != "")
                where = where + " and a.PRODUCT_NAME like '%" + TXT_PRODUCTNAME.Text.Trim() + "%' ";

            if (DDL_PRODUCTGROUP.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP = '" + DDL_PRODUCTGROUP.SelectedValue + "' ";

            if (DDL_TRANSTYPE.SelectedValue != "")
                where = where + " and a.TRANS_TYPE = '" + DDL_TRANSTYPE.SelectedValue + "' ";

            if (TXT_YEARFROM.Text.Trim() != "")
                where = where + " and a.YEAR >= " + TXT_YEARFROM.Text.Trim() + " ";

            if (TXT_YEARTO.Text.Trim() != "")
                where = where + " and a.YEAR <= " + TXT_YEARTO.Text.Trim() + " ";

            conn.QueryString = "select " +
                                "STATUS             = (case when a.PREMIUM < 0 then 'CLAWBACK' else 'PRODUCTION' end), " +
                                "[DATE]				= convert(varchar(20), SETTLEDATE, 106), " +
                                "PREMIUM			= replace(convert(varchar(100), convert(money, PREMIUM), 1), '.00', ''), " +
                                "[POLICY NO]		= POLICY_NO, " +
                                "[AGENT NAME]       = AGENT_NAME, " +
                                "CHANNEL			= CHANNEL_DESCR, " +
                                "LEVEL				= LEVEL_DESCR, " +
                                "PRODUCT			= PRODUCT_NAME, " +
                                "[PRODUCT GROUP]	= PRODUCT_GROUP_DESCR, " +
                                "[TRANS. TYPE]		= TRANS_TYPE_DESCR, " +
                                "YEAR				= YEAR " +
                                "from		        V_LINK_MARKETING_DATA_PRODUCTION a " +
                                "where " +
                                "YEAR(a.SETTLEDATE)=" + LB_YEAR.Text + " and MONTH(a.SETTLEDATE)=" + LB_MONTH.Text + " " +
                                DDL_STAT.SelectedValue + " " +
                                "and a.AGENT_CODE = (case when '" + roleid + "' = '99' then '" + userid + "' else a.AGENT_CODE end) " + where + " " +
                                "order by " +
                                "a.SETTLEDATE";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

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
            //try
            //{
                DGR.CurrentPageIndex = 0;
                FillDGR();
            //}
            //catch { }
        }

    }
}
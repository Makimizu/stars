using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class DATA_PRODUCTION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            conn.QueryString = "select START_DATE = '1/' + convert(varchar(4), MONTH(GETDATE())) + '/' + convert(varchar(4), YEAR(GETDATE()))";
            conn.ExecuteQuery();
            TXT_START_DATE.Text = conn.GetFieldValue("START_DATE").ToString();

            conn.QueryString = "select CODE, DESCR from PR_TRANS_TYPE order by 1";
            conn.ExecuteQuery();
            DDL_TRANSTYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANSTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from V_LINK_UB_PR_PRODUCT_GROUP order by 2 desc";
            conn.ExecuteQuery();
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION order by 2";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLLevel();


        }

        protected void FillDDLLevel()
        {
            DDL_LEVEL.Items.Clear();
            conn.QueryString = "select CD_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            DDL_LEVEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP = '" + DDL_PRODUCT_GROUP.SelectedValue + "' ";

            if (TXT_PRODUCT_NAME.Text.Trim() != "")
                where = where + " and a.PRODUCT_NAME like '%" + TXT_PRODUCT_NAME.Text.Trim() + "%' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.CHANNEL = '" + DDL_CHANNEL.SelectedValue + "' ";

            if (DDL_LEVEL.SelectedValue != "")
                where = where + " and a.LEVEL = '" + DDL_LEVEL.SelectedValue + "' ";

            if (DDL_TRANSTYPE.SelectedValue != "")
                where = where + " and a.TRANS_TYPE = '" + DDL_TRANSTYPE.SelectedValue + "' ";

            if (TXT_AGENT.Text.Trim() != "")
                //where = where + " and (a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' or a.AGENT_CODE like '%" + TXT_AGENT.Text.Trim() + "%')";
                where = where + " and (a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' or a.AGENT_CODE like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER1 like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER1_NAME like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER2 like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER2_NAME like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER3 like '%" + TXT_AGENT.Text.Trim() + "%' or a.UPLINER3_NAME like '%" + TXT_AGENT.Text.Trim() + "%')";

            if (TXT_START_DATE.Text.Trim() != "")
                where = where + " and convert(date, a.SETTLE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_END_DATE.Text.Trim() != "")
                where = where + " and convert(date, a.SETTLE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text.Trim(), "d/M/yyyy") + "' ";

            //conn.QueryString = "select " +
            //                    "ID					= a.ID_SETTLEMENT, " +
            //                    "PRODUCT_NAME		= a.PRODUCT_NAME, " +
            //                    "PRODUCT_GROUP		= a.PRODUCT_GROUP_DESCR, " +
            //                    "TRANS_TYPE			= a.TRANS_TYPE_DESCR, " +
            //                    "AGENT				= a.AGENT_CODE + ' - ' + a.AGENT_NAME, " +
            //                    "REFERENCE_NO		= REFERENCE_NO, " +
            //                    "YEAR				= a.YEAR, " +
            //                    "SETTLE_DATE			= convert(varchar(20), a.SETTLE_DATE, 106), " +
            //                    "PAID_AMOUNT			= replace(convert(varchar(100), convert(money, a.PAID_AMOUNT),1), '.00', '') " +
            //                    "from		V_DATA_PRODUCTION a " +
            //                    "where " +
            //                    "1=1 " + where + " " +
            //                    "order by " +
            //                    "a.SETTLE_DATE, " +
            //                    "a.PRODUCT_GROUP_DESCR, " +
            //                    "a.PRODUCT_NAME";
            conn.QueryString = "select " +
                                "ID					= a.ID_SETTLEMENT, " +
                                "PRODUCT_NAME		= a.PRODUCT_NAME, " +
                                "PRODUCT_GROUP		= a.PRODUCT_GROUP_DESCR, " +
                                "TRANS_TYPE			= a.TRANS_TYPE, " +
                                "AGENT				= a.AGENT_CODE + ' - ' + a.AGENT_NAME + ' - ' + dbo.ufn_lvlagent(a.M_AGENT_LEVEL), " +
                                "BASIC_COMM         = a.BASIC_COMM, " +
                                "UPLINER1			= a.UPLINER1 + ' - ' + a.UPLINER1_NAME + ' - ' + dbo.ufn_lvlagent(a.UPLINER1_LEVEL), " +
                                "OR1                = a.OR1, " + 
                                "UPLINER2			= a.UPLINER2 + ' - ' + a.UPLINER2_NAME + ' - ' + dbo.ufn_lvlagent(a.UPLINER2_LEVEL), " +
                                "OR2                = a.OR2, " +
                                "UPLINER3			= a.UPLINER3 + ' - ' + a.UPLINER3_NAME + ' - ' + dbo.ufn_lvlagent(a.UPLINER3_LEVEL), " +
                                "OR3                = a.OR3, " +
                                "REFERENCE_NO		= REFERENCE_NO, " +
                                "YEAR				= a.YEAR, " +
                                "SETTLE_DATE		= convert(varchar(20), a.SETTLE_DATE, 106), " +
                                "DUE_DATE			= convert(varchar(20), a.DUE_DATE, 106), " +
                                "PAID_AMOUNT		= replace(convert(varchar(100), convert(money, a.PAID_AMOUNT),1), '.00', '') " +
                                "from		V_DATA_PRODUCTION_UPLINER a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by " +
                                "a.SETTLE_DATE, " +
                                "a.PRODUCT_GROUP_DESCR, " +
                                "a.PRODUCT_NAME";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
        }
    }
}
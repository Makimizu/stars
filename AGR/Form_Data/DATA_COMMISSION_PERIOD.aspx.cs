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
    public partial class DATA_COMMISSION_PERIOD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REMUN_TYPE.Text = Request.QueryString["TYPE"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_REMUN_TYPE where CODE = '" + LB_REMUN_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = "PERIOD : " + conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select " +
                                "THISYEAR = YEAR(GETDATE()) - SEQ + 1 " +
                                "from SC_SEQ  " +
                                "where " +
                                "SEQ < 3 " +
                                "order by " +
                                "1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "START_DATE		= convert(varchar(20), START_DATE, 106), " +
                                "END_DATE		= convert(varchar(20), END_DATE, 106), " +
                                "DATA_PRODUCTION, " +
                                "DATA_REMUN, " +
                                "DATA_REMUN_PROCESSED " +
                                "from V_PERIOD_MASTER a " +
                                "where " +
                                "YEAR(START_DATE) = " + DDL_YEAR.SelectedValue + " " +
                                "and a.REMUN_TYPE = '" + LB_REMUN_TYPE.Text + "' " +
                                "and a.CD = '" + DDL_CHANNEL.SelectedValue + "' " +
                                "order by " +
                                "a.START_DATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_X");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }

        protected void DDL_REMUN_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_INSERT_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_PERIOD_MASTER_INSERT " +
                                    "'" + DDL_CHANNEL.SelectedValue + "'," +
                                    "'" + LB_REMUN_TYPE.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
            catch { }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PERIOD_MASTER where " +
                                    "REMUN_TYPE = '" + LB_REMUN_TYPE.Text + "' " +
                                    "and START_DATE = '" + e.Item.Cells[0].Text + "' " +
                                    "and END_DATE = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
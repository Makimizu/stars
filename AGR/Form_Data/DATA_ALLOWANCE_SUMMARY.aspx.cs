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
    public partial class DATA_ALLOWANCE_SUMMARY : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION order by 2";
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
                                "ID             = convert(varchar(20), a.START_DATE, 112) + convert(varchar(20), a.END_DATE, 112), " +
                                "START_DATE		= convert(varchar(20), a.START_DATE, 106), " +
                                "END_DATE		= convert(varchar(20), a.END_DATE, 106) " +
                                "from ( select " +
                                "       a.START_DATE, " +
                                "       a.END_DATE " +
                                "       from V_PERIOD_MASTER a  " +
                                "       inner join	(select SETTLEDATE from DATA_PRODUCTION group by SETTLEDATE) b on convert(date, b.SETTLEDATE) between convert(date, a.START_DATE) and convert(date, a.END_DATE) " +
                                "       where " +
                                "       a.REMUN_TYPE = '2' " +
                                "       and a.CD = '" + DDL_CHANNEL.SelectedValue + "' " +
                                "       group by " +
                                "       a.START_DATE, " +
                                "       a.END_DATE ) a " +
                                "where " +
                                "YEAR(START_DATE) = " + DDL_YEAR.SelectedValue + " " +
                                "order by " +
                                "a.START_DATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();

            for (int i = 0; i < DGR_PERIOD.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_PERIOD.Items[i].FindControl("LB_PERIOD");
                lb.Text = DGR_PERIOD.Items[i].Cells[1].Text + " - " + DGR_PERIOD.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ShowSummary(e.Item.Cells[1].Text, e.Item.Cells[2].Text);
            }
        }

        protected void ShowSummary(string startdate, string enddate)
        {
            DV_SUMMARY.Visible = true;
            LB_STARTDATE.Text = startdate;
            LB_ENDDATE.Text = enddate;

            FillDGRUnbooked();
            FillDGRBooked();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void TXT_LEVEL_TextChanged(object sender, EventArgs e)
        {
            FillDGRUnbooked();
            FillDGRBooked();
        }

        protected void TXT_AGENTNAME_TextChanged(object sender, EventArgs e)
        {
            FillDGRUnbooked();
            FillDGRBooked();
        }

        protected void FillDGRUnbooked()
        {
            LB_RECORD_UNBOOK.Text = "";

            conn.QueryString = "exec SP_DATA_ALLOWANCE_PERIOD " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "0,1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNBOOKED.DataSource = dt;
            DGR_UNBOOKED.DataBind();

            conn.QueryString = "exec SP_DATA_ALLOWANCE_PERIOD " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "0,2";
            conn.ExecuteQuery();
            LB_RECORD_UNBOOK.Text = conn.GetFieldValue("RECORDS").ToString() + " Records";
            LB_AMOUNT_UNBOOK.Text = conn.GetFieldValue("AMOUNT").ToString();
        }

        protected void FillDGRBooked()
        {
            LB_RECORD_BOOK.Text = "";

            conn.QueryString = "exec SP_DATA_ALLOWANCE_PERIOD " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "1,1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BOOKED.DataSource = dt;
            DGR_BOOKED.DataBind();

            conn.QueryString = "exec SP_DATA_ALLOWANCE_PERIOD " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "1,2";
            conn.ExecuteQuery();
            LB_RECORD_BOOK.Text = conn.GetFieldValue("RECORDS").ToString() + " Records";
            LB_AMOUNT_BOOK.Text = conn.GetFieldValue("AMOUNT").ToString();
        }

        protected void BT_BOOKED_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_DATA_ALLOWANCE_BOOK " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            FillDGRUnbooked();
            FillDGRBooked();
        }

        protected void BT_UNBOOK_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_DATA_ALLOWANCE_UNBOOK " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'";
            conn.ExecuteNonQuery();

            FillDGRUnbooked();
            FillDGRBooked();
        }

        protected void DGR_UNBOOKED_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_UNBOOKED.CurrentPageIndex = e.NewPageIndex;
            FillDGRUnbooked();
        }

        protected void DGR_BOOKED_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_BOOKED.CurrentPageIndex = e.NewPageIndex;
            FillDGRBooked();
        }

        protected void BT_FINANCE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_LINK_FINANCE_SETTLEMENT_DETAIL_INSERT " +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'2'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            DGR_BOOKED.CurrentPageIndex = 0;
            FillDGRBooked();
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Finance
{
    public partial class MemoRemuneration : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            LB_APP.Text = "AGR";
            LB_TIPE.Text = Request.QueryString["tipe"];

            conn.QueryString = "select DESCR from PR_MARKET_SEGMENT	where CODE = '" + LB_TIPE.Text.Trim() + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + LB_TIPE.Text + "' order by SEQ";
            conn.ExecuteQuery();
            DDL_LEVEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLYear();
            FillDDLPeriod();
        }

        protected void FillDDLYear()
        {
            DDL_YEAR.Items.Clear();

            conn.QueryString = "select THEYEAR=YEAR(GETDATE()) union all select THEYEAR=YEAR(GETDATE())-1 union all select THEYEAR=YEAR(GETDATE())-2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLPeriod()
        {
            DDL_PERIOD.Items.Clear();

            conn.QueryString = "select distinct " +
                                "CODE = convert(varchar(20), START_DATE, 112) + convert(varchar(20), END_DATE, 112), " +
                                "DESCR = convert(varchar(20), START_DATE, 106) + ' - ' + convert(varchar(20), END_DATE, 106) " +
                                "from PERIOD_MASTER " +
                                "where  " +
                                "CD = '" + LB_TIPE.Text + "' " +
                                "and YEAR(START_DATE) = '" + DDL_YEAR.SelectedValue + "'" +
                                "and START_DATE <= GETDATE() " +
                                "order by " +
                                "1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            LB_TOTAL.Text = "";
            conn.QueryString = "exec SP_LINK_FINANCE_MEMO_REMUN " +
                                        "'" + LB_TIPE.Text + "'," +
                                        "'" + DDL_YEAR.SelectedValue + "'," +
                                        "'" + DDL_PERIOD.SelectedValue + "'," +
                                        "'" + TXT_DOCNO.Text.Trim() + "'," +
                                        "'" + TXT_AGENT_CODE.Text.Trim() + "'," +
                                        "'" + TXT_AGENT_NAME.Text.Trim() + "'," +
                                        "'" + DDL_LEVEL.SelectedValue + "'," +
                                        "'" + DDL_APPROVAL_RANGE.SelectedValue + "'," +
                                        "1," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            //LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbDOCNO = (LinkButton)DGR.Items[i].FindControl("LB_DOCNO");

                lbDOCNO.Text = DGR.Items[i].Cells[2].Text;
                lbDOCNO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[4].Text == "0")
                    cb.Visible = false;
            }

            conn.QueryString = "exec SP_LINK_FINANCE_MEMO_REMUN " +
                                        "'" + LB_TIPE.Text + "'," +
                                        "'" + DDL_YEAR.SelectedValue + "'," +
                                        "'" + DDL_PERIOD.SelectedValue + "'," +
                                        "'" + TXT_DOCNO.Text.Trim() + "'," +
                                        "'" + TXT_AGENT_CODE.Text.Trim() + "'," +
                                        "'" + TXT_AGENT_NAME.Text.Trim() + "'," +
                                        "'" + DDL_LEVEL.SelectedValue + "'," +
                                        "'" + DDL_APPROVAL_RANGE.SelectedValue + "'," +
                                        "2," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(500000);

            LB_RESULT.Text = "Total Records : " + conn.GetFieldValue("TOTAL_RECORDS").ToString() + "<BR>";
            LB_TOTAL.Text = "Total Amount : " + conn.GetFieldValue("TOTAL_AMOUNT").ToString() + "<BR>";
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPeriod();
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "update FINANCE.dbo.SETTLEMENT_MASTER set " +
                                            "APPROVALBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "APPROVALDATE = GETDATE() " +
                                            "where " +
                                            "REKAPID = '" + DGR.Items[i].Cells[2].Text + "'";
                        conn.ExecuteNonQuery();

                        //SendEmail(DGR.Items[i]);

                    }
                    catch { }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_APPROVAL_RANGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
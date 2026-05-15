using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Data
{
    public partial class ProcessFollowUp : System.Web.UI.Page
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

        protected void FillDGR_CD()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CD.DataSource = dt;
            DGR_CD.DataBind();

            for (int i = 0; i < DGR_CD.Items.Count; i++)
            {
                Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                bt.Text = DGR_CD.Items[i].Cells[1].Text;
            }
        }

        protected void Setup()
        {
            FillDGR_CD();

            conn.QueryString = "select CODE, DESCR from PR_COMMISSION_TYPE order by(case when CODE = 'COMM' then 0 when CODE like 'OR%' then 1 else 99 end), CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COMM.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
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

            conn.QueryString = "select SEQ, MON = UPPER(DateName( month , DateAdd( month , SEQ , -1 ))), MTD = MONTH(GETDATE()) from SC_SEQ where SEQ <= 12 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            DDL_MONTH.SelectedValue = System.DateTime.Now.Month.ToString();

            FillDDL_Product();

            LB_TITLE.Text = DGR_CD.Items[0].Cells[1].Text;
            LB_CD.Text = DGR_CD.Items[0].Cells[0].Text;
        }

        protected void FillDDL_Product()
        {
            conn.QueryString = "select PRODUCT_CODE, PRODUCT_DESCR = PRODUCT_CODE + ' - ' + PRODUCT_DESCR from UWBOX.dbo.V_PARAM_PRODUCT_MASTER where SEGMENT = " + DDL_SEGMENT.SelectedValue + " order by 1";
            conn.ExecuteQuery();
            DDL_PRODUCT.Items.Clear();
            DDL_PRODUCT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DGR_CD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Button bt = (Button)e.Item.FindControl("BT_CD");
                LB_TITLE.Text = bt.Text;
                LB_CD.Text = e.Item.Cells[0].Text;
                FillDGR();
            }
        }


        protected void FillDGR()
        {
            string where = "";
            if (TXT_AGENT_CODE.Text.Trim() != "")
                where = where + " and a.AGENT_CODE like '%" + TXT_AGENT_CODE.Text.Trim() + "%' ";
            if (TXT_AGENT_NAME.Text.Trim() != "")
                where = where + " and a.AGENT_NAME like '%" + TXT_AGENT_NAME.Text.Trim() + "%' ";
            if (DDL_PRODUCT.SelectedValue != "")
                where = where + " and a.PRODUCT_CODE = '" + DDL_PRODUCT.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "ID_SETTLEMENT              = ID_SETTLEMENT, " +
                                "NEW_SETTLEDATE             = LEFT(CURRENT_PERIOD, 8), " +
                                "AGENT                      = AGENT_CODE + ' - ' + AGENT_NAME, " +
                                "AGENT_LEVEL                = AGENT_LEVEL, " +
                                "COMM_TYPE_DESCR            = COMM_TYPE, " +
                                "YEARSEQ                    = YEARSEQ, " +
                                "PRODUCT                    = PRODUCT_CODE + ' - ' + PRODUCT_DESCR, " +
                                "PRODUCT_SEGMENT            = SEGMENT_DESCR, " +
                                "SETTLEDATE                 = convert(varchar(20), SETTLEDATE, 106), " +
                                "DUE_DATE                   = convert(varchar(20), DUE_DATE, 106), " +
                                "TRANS_TYPE                 = TRANS_TYPE, " +
                                "PAID_AMOUNT                = replace(convert(varchar(100), convert(money, PAID_AMOUNT), 1), '.00', ''), " +
                                "POLICY_NO                  = REFERENCE_NO, " +
                                "FOLLOWUP_PERIOD            = LEFT(CURRENT_PERIOD, 8) + ' - ' + RIGHT(CURRENT_PERIOD, 8) " +
                                "from                       V_DATA_PRODUCTION_UNPROCESS a " +
                                "where " +
                                "a.MARKET_SEGMENT           = '" + LB_CD.Text + "' " +
                                "and a.COMM_TYPE            = '" + DDL_COMM.SelectedValue + "' " +
                                "and YEAR(a.SETTLEDATE)     = '" + DDL_YEAR.SelectedValue + "' " +
                                "and MONTH(a.SETTLEDATE)    = '" + DDL_MONTH.SelectedValue + "' " +
                                "and a.SEGMENT              = '" + DDL_SEGMENT.SelectedValue + "' " +
                                "and a.YEARSEQ              " + DDL_PRODYEAR.SelectedValue + " " + where + " " +
                                "order by " +
                                "a.SETTLEDATE";
            conn.ExecuteQuery();


            LB_CNT.Text = conn.GetRowCount().ToString() + " Records";
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();
        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "FollowUp")
            {
                for (int i = 0; i < DGR_PERIOD.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PERIOD.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        conn.QueryString = "insert into DATA_PRODUCTION_FOLLOWUP select " +
                                            "'" + DGR_PERIOD.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR_PERIOD.Items[i].Cells[1].Text + "'," +
                                            "'" + DGR_PERIOD.Items[i].Cells[2].Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGR();
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PERIOD_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_SEGMENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_Product();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PERIOD.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PERIOD.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}
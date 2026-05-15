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
    public partial class ExternalRevenue : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 1";
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

            FillDDLPeriod();
            FillDDLLevel();
            FillDGR();
        }

        protected void FillDDLPeriod()
        {
            DDL_PERIOD.Items.Clear();
            conn.QueryString = "select " +
                                "CODE = convert(varchar(20), START_DATE, 112) + convert(varchar(20), END_DATE, 112), " +
                                "DESCR = convert(varchar(20), START_DATE, 106) + ' - ' + convert(varchar(20), END_DATE, 106) " +
                                "from PERIOD_MASTER " +
                                "where " +
                                "YEAR(START_DATE) =  " + DDL_YEAR.SelectedValue + " " +
                                "and CD = '" + DDL_CHANNEL.SelectedValue + "' " +
                                "and REMUN_TYPE = '9' " +
                                "order by " +
                                "START_DATE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLLevel()
        {
            DDL_LEVEL.Items.Clear();
            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' order by isnull(SEQ, 0)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select top 200 " +
                                "AGENT_CODE          = a.CODE, " +
                                "AGENT_NAME          = a.FULLNAME, " +
                                "LEVEL               = a.SUBCD_DESCR, " +
                                "AMOUNT              = replace(convert(varchar(100), convert(money, isnull(b.AMOUNT, 0)), 1), '.00', '') " +
                                "from                V_M_AGENTS a " +
                                "left join           PERIOD_DETAIL b on a.CODE = b.AGENT_CODE and convert(varchar(20), b.START_DATE, 112) + convert(varchar(20), b.END_DATE, 112) = '" + DDL_PERIOD.SelectedValue + "' and b.REMUN_TYPE = '9' " +
                                "where " +
                                "a.SUBCD             = '" + DDL_LEVEL.SelectedValue + "' " +
                                "and a.FULLNAME      like '%" + TXT_AGENTNAME.Text.Trim() + "%' " +
                                "order by " +
                                "isnull(b.AMOUNT, 0) desc, " +
                                "a.FULLNAME";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                txtAMOUNT.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[1].Text != "0")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPeriod();
            FillDDLLevel();
            FillDGR();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPeriod();
            FillDGR();
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void TXT_AGENTNAME_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");

                conn.QueryString = "exec SP_PERIOD_DETAIL_OTHER_INSERT " +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + DDL_PERIOD.SelectedValue.Substring(0, 8) + "'," +
                                        "'" + DDL_PERIOD.SelectedValue.Substring(DDL_PERIOD.SelectedValue.Length - 8, 8) + "'," +
                                        "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

            }

            FillDGR();
        }
    }
}
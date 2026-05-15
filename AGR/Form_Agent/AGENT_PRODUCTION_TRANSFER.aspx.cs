using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class AGENT_PRODUCTION_TRANSFER : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "1=1 ";


            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE collate database_default in (select a.Item from SECURITY.dbo.SplitStrings_XML('" + TXT_CODE.Text.Trim().Replace(" ", "") + "',',') a)";
            //where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_AGENCY.Text.Trim() != "")
                where = where + " and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.MARKET_SEGMENT = " + DDL_CHANNEL.SelectedValue + " ";




            conn.QueryString = "select " +
                                "CODE           = a.CODE, " +
                                "FULLNAME        = a.FULLNAME, " +
                                "DOB             = convert(varchar(20), a.DOB, 106), " +
                                "UPLINER         = isnull(a.UPLINER_NAME, ''), " +
                                "BRANCH          = isnull(a.BRANCH_DESCR, ''), " +
                                "LEVEL           = isnull(a.SUBCD_DESCR, ''), " +
                                "YEAR            = convert(varchar(10), YEAR(dateadd(month, -1, GETDATE()))), " +
                                "CASES           = convert(varchar(10), isnull(c.CASES, 0)), " +
                                "ANP             = replace(convert(varchar(100), convert(money, isnull(b.ANP_BASIC, 0) + isnull(b.ANP_DL1, 0) + isnull(b.ANP_DL2, 0) + isnull(b.ANP_DL3, 0)), 1), '.00', '') " +
                                "from           V_M_AGENTS a " +
                                "left join      (select AGENT_CODE, CASES = count(CUSTOMER_ID) from V_M_AGENTS_PORTFOLIO group by AGENT_CODE) c on a.CODE = c.AGENT_CODE " +
                                "left join	    M_AGENTS_ANP b on a.CODE = b.CODE and b.YEAR = YEAR(dateadd(month, -1, GETDATE())) " +
                                "where " + where +
                                "order by " +
                                "a.FULLNAME";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");

                Label lbFULLNAME = (Label)DGR.Items[i].FindControl("LB_FULLNAME");
                Label lbAGENTCODE = (Label)DGR.Items[i].FindControl("LB_AGENT_CODE");
                Label lbBRANCH = (Label)DGR.Items[i].FindControl("LB_BRANCH");
                Label lbUPLINER = (Label)DGR.Items[i].FindControl("LB_UPLINER");
                Label lbYEAR = (Label)DGR.Items[i].FindControl("LB_YEAR");
                Label lbLEVEL = (Label)DGR.Items[i].FindControl("LB_LEVEL");
                Label lbCASES = (Label)DGR.Items[i].FindControl("LB_CASES");
                Label lbANP = (Label)DGR.Items[i].FindControl("LB_ANP");

                lb.Text = DGR.Items[i].Cells[1].Text;

                lbFULLNAME.Text = DGR.Items[i].Cells[2].Text;
                lbAGENTCODE.Text = DGR.Items[i].Cells[1].Text;
                lbBRANCH.Text = DGR.Items[i].Cells[5].Text;
                lbUPLINER.Text = DGR.Items[i].Cells[4].Text;
                lbYEAR.Text = DGR.Items[i].Cells[7].Text;
                lbLEVEL.Text = DGR.Items[i].Cells[6].Text;
                lbCASES.Text = DGR.Items[i].Cells[8].Text;
                lbANP.Text = DGR.Items[i].Cells[9].Text;
            }
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("AGENT_PRODUCTION_TRANSFER_DETAIL_FRAME.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }
        }

        protected void BT_REPORT_Click(object sender, EventArgs e)
        {
            string AGENT_CODE = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if(cb.Checked)
                {
                    AGENT_CODE = AGENT_CODE + DGR.Items[i].Cells[1].Text + ",";
                }
            }

            if(AGENT_CODE.Trim()!="")
            {
                Response.Redirect("AGENT_PRODUCTION_TRANSFER_DETAIL_FRAME.aspx?AGENTCODE=" + AGENT_CODE);
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = CB_ALL.Checked;
            }
        }
    }
}
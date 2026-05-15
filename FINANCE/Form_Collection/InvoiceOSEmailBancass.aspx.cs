using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class InvoiceOSEmailBancass : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, SUBCD_DESCR from (select '' CODE, 'SELECT' SUBCD_DESCR UNION ALL select distinct SUBCD_DESCR CODE, SUBCD_DESCR from MARKETING.dbo.V_M_AGENTS where ACTIVE = 1) a order by CODE";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLAgent();
        }
        
        protected void FillDDLAgent()
        {

            string whereagent = "";

            if (DDL_CD.SelectedValue != "")
                whereagent = whereagent + " and SUBCD = '" + DDL_CD.SelectedValue + "' ";

            conn.QueryString = "select CODE, FULLNAME from MARKETING.dbo.V_M_AGENTS where ACTIVE = 1 "+ whereagent +" order by FULLNAME";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLAgent();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_CD.SelectedValue != "")
                where = where + " and a.CHANNEL_DISTRIBUTION = '" + DDL_CD.SelectedValue + "' ";

            if (DDL_AGENT.SelectedValue != "")
                where = where + " and a.AGENT_CODE = '" + DDL_AGENT.SelectedValue + "' ";

            if (DDL_MONTHLYAGING.SelectedValue != "")
                where = where + " and a.MONTHLY_AGING = '" + DDL_MONTHLYAGING.SelectedValue + "' ";

            //if (TXT_COMPANY.Text.Trim() != "")
            //    where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            //if (TXT_CLAIMDATE1.Text.Trim() != "")
            //    where = where + " and convert(date,a.CLAIM_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            //if (TXT_CLAIMDATE2.Text.Trim() != "")
            //    where = where + " and convert(date,a.CLAIM_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select AGENT_CODE = a.AGENT_CODE, " +
                                "      AGENT_NAME = a.AGENT_NAME, " +
                                "      CHANNEL_DISTRIBUTION = a.CHANNEL_DISTRIBUTION, " +
                                "	   MONTHLY_AGING = a.MONTHLY_AGING, " +
                                "	   TOT_OS = count(a.AGENT_CODE), " +
                                "	   JML_OS = replace(convert(varchar(100), convert(money, SUM(a.OUTSTANDING)),1), '.00',''), " +
                                "	   AGENT_EMAIL = isnull(a.AGENT_EMAIL, ''), " +
                                "      EMAIL_CC = 'creditcontrol-atk@takaful.com;bancassurance@takaful.com;bancas.newbusiness@takaful.com;uwgroup_atk@takaful.com;achmad.sucipto@takaful.com', " +
                                "      MONTH_PARAM = case a.MONTHLY_AGING when '1 Month' then '1' when '2 Month' then '2' when '3 Month' then '3' else '4' end, " +
                                "      SEND_DATE = isnull(LEFT(convert(varchar(20), b.SEND_DATE, 113),20), 'NEVER SENT') " +
                                "from [V_INVOICE_OUTSTANDING_REMINDER_BANCASS] a " +
                                "left join (select AGENT_CODE, MONTHLY_AGING, max(SEND_DATE) SEND_DATE from FINANCE.dbo.EMAIL_INVOICE_OUTSTANDING_REMINDER_BANCASS group by AGENT_CODE, MONTHLY_AGING) b " +
                                "     on a.AGENT_CODE = b.AGENT_CODE and case a.MONTHLY_AGING when '1 Month' then '1' when '2 Month' then '2' when '3 Month' then '3' else '4' end = b.MONTHLY_AGING " +
                                "where 1 = 1 AND a.AGENT_CODE not in ('MIGRASIATK2') " + where + " " +
                                "group by  a.AGENT_CODE, a.AGENT_NAME,a.CHANNEL_DISTRIBUTION, a.MONTHLY_AGING,  isnull(a.AGENT_EMAIL, ''), " +
                                "          case a.MONTHLY_AGING when '1 Month' then '1' when '2 Month' then '2' when '3 Month' then '3' else '4' end, " +
                                "          isnull(LEFT(convert(varchar(20), b.SEND_DATE, 113),20), 'NEVER SENT') " +
                                "order by a.AGENT_NAME, a.MONTHLY_AGING";
            conn.ExecuteQuery(500000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btEmail = (Button)DGR.Items[i].FindControl("BT_EMAIL");
                Button btHST = (Button)DGR.Items[i].FindControl("BT_HST");
                btHST.Attributes.Add("onclick", "window.open('InvoiceOSEmailBancass_Hist.aspx?AGENTCODE=" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "&MONTHLYAGING=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','SEND EMAIL HISTORY','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                txtEMAIL.Text = DGR.Items[i].Cells[0].Text.Trim().Replace("&nbsp;", "");
           }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }

            if (e.CommandName == "Process")
            {
                try
                {
                    TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                    string userlog = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                    if (txtEMAIL.Text.Trim() == "")
                        throw new Exception("Email tidak boleh kosong!");
                    conn.QueryString = "exec FINANCE.dbo.[SP_UPSERT_EMAIL_INVOICE_OUTSTANDING_REMINDER_BANCASS] '" + e.Item.Cells[2].Text + "','" + e.Item.Cells[1].Text + "','" + txtEMAIL.Text.Trim() + "','" + userlog + "'";
                    conn.ExecuteNonQuery();
                    //FillDGR();
                }
                catch { }
            }
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
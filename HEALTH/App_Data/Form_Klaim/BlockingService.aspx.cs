using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Threading.Tasks;

namespace HEALTH.Form_Klaim
{
    public partial class BlockingService : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadFindingList();
            }
        }

        protected void Setup()
        {
            BT_EXCLUDE_SAVE.Attributes.Add("onclick", "if(!confirm('Are you sure to EXCLUDE ?')){return false;};");
        }

        protected void LoadFindingList()
        {
            LB_TITLE.Text = BT1.Text;
            LB_RESULT.Text = "";

            conn.QueryString = "select " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TOTAL_OUTSTANDING = replace(convert(varchar(100), convert(money,TOTAL_OUTSTANDING),1), '.00', ''), " +
                                "MAX_AGING, " +
                                "INVOICE_COUNT, " +
                                "URL_REPORT " +
                                "from V_POLICY_PERIOD_INVOICE_OUTSTANDING a " +
                                "where " +
                                "a.POLICY_ID not in (select POLICY_ID from POLICY_BLOCKING_SERVICE where BLOCKED_DATE is not null and UNBLOCKED_BY is null)";
            conn.ExecuteQuery(50000);

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FINDING.DataSource = dt;
            DGR_FINDING.DataBind();

            for (int i = 0; i < DGR_FINDING.Items.Count; i++)
            {
                Button btDetail = (Button)DGR_FINDING.Items[i].FindControl("BT_D");
                btDetail.Attributes.Add("onclick", "window.open('" + DGR_FINDING.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

            DGR_FINDING.Visible = true;
            DGR_MARKED.Visible = false;
            DGR_BLOCKED.Visible = false;
        }

        protected void LoadMarkedList()
        {
            LB_TITLE.Text = BT2.Text;
            LB_RESULT.Text = "";

            conn.QueryString = "select " +
                                "POLICY_ID, " +
                                "MARK_DATE = convert(varchar(20), a.MARK_DATE, 106), " +
                                "EXCLUDED = isnull(EXCLUDED, 0), " +
                                "EXCLUDED_REASON, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TOTAL_OUTSTANDING = replace(convert(varchar(100), convert(money,TOTAL_OUTSTANDING),1), '.00', ''), " +
                                "INVOICE_COUNT, " +
                                "MAX_AGING, " +
                                "URL_REPORT " +
                                "from V_POLICY_BLOCKING_SERVICE a " +
                                "where isnull(EXCLUDED, 0) = 0 and BLOCKED_DATE is null";
            conn.ExecuteQuery(50000);

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MARKED.DataSource = dt;
            DGR_MARKED.DataBind();

            for (int i = 0; i < DGR_MARKED.Items.Count; i++)
            {
                Button btDetail = (Button)DGR_MARKED.Items[i].FindControl("BT_D_MARKED");
                btDetail.Attributes.Add("onclick", "window.open('" + DGR_MARKED.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

            DGR_FINDING.Visible = false;
            DGR_MARKED.Visible = true;
            DGR_BLOCKED.Visible = false;
        }

        protected void LoadBlockedList()
        {
            LB_TITLE.Text = BT3.Text;
            LB_RESULT.Text = "";

            conn.QueryString = "select " +
                                "POLICY_ID, " +
                                "BLOCKED_DATE = convert(varchar(20), a.BLOCKED_DATE, 106), " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TOTAL_OUTSTANDING = replace(convert(varchar(100), convert(money,TOTAL_OUTSTANDING),1), '.00', ''), " +
                                "INVOICE_COUNT, " +
                                "MAX_AGING, " +
                                "URL_REPORT " +
                                "from V_POLICY_BLOCKING_SERVICE a " +
                                "where BLOCKED_DATE is not null";
            conn.ExecuteQuery(50000);

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BLOCKED.DataSource = dt;
            DGR_BLOCKED.DataBind();

            for (int i = 0; i < DGR_BLOCKED.Items.Count; i++)
            {
                Button btUnblock = (Button)DGR_BLOCKED.Items[i].FindControl("BT_UNBLOCK");
                btUnblock.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk UNBLOCK ?')){return false;};");

                Button btDetail = (Button)DGR_BLOCKED.Items[i].FindControl("BT_D_BLOCKED");
                btDetail.Attributes.Add("onclick", "window.open('" + DGR_BLOCKED.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

            DGR_FINDING.Visible = false;
            DGR_MARKED.Visible = false;
            DGR_BLOCKED.Visible = true;
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            LoadFindingList();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LoadMarkedList();
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LoadBlockedList();
        }

        protected void DGR_FINDING_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {

            }
        }

        protected void DGR_MARKED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {

            }

            if (e.CommandName == "Exclude")
            {
                LB_POLICY_ID.Text = e.Item.Cells[0].Text;
                LB_MARKED_DATE.Text = e.Item.Cells[3].Text;
                LB_POLICYNO.Text = e.Item.Cells[4].Text;
                LB_COMPANY.Text = e.Item.Cells[5].Text;
                LB_OUTSTANDING.Text = e.Item.Cells[6].Text;
                LB_AGING.Text = e.Item.Cells[8].Text;
                TXT_EXCLUDE_REASON.Text = "";

                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void DGR_BLOCKED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {

            }

            if (e.CommandName == "Unblock")
            {
                conn.QueryString = "update POLICY_BLOCKING_SERVICE set " +
                                    "UNBLOCKED_BY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "UNBLOCKED_DATE = GETDATE() " +
                                    "where " +
                                    "POLICY_ID = " + e.Item.Cells[0].Text + " " +
                                    "and datediff(day, convert(date, BLOCKED_DATE), '" + e.Item.Cells[1].Text + "') = 0";
                conn.ExecuteNonQuery();

                conn.QueryString = "exec SP_JOB_EMAIL_POLICY_UNBLOCKING_SERVICE_TO_INTERNAL " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();

                conn.QueryString = "exec SP_JOB_EMAIL_POLICY_UNBLOCKING_SERVICE_TO_CUSTOMER " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();  

                //Task.Run(() => SendEmailUnblock(e.Item.Cells[0].Text));                
                LoadBlockedList();
            }
        }

        protected void SendEmailUnblock(string policyID)
        {
            conn.QueryString = "exec SP_JOB_EMAIL_POLICY_UNBLOCKING_SERVICE_TO_INTERNAL " + policyID;
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_JOB_EMAIL_POLICY_UNBLOCKING_SERVICE_TO_CUSTOMER " + policyID;
            conn.ExecuteNonQuery();            
        }

        protected void BT_EXCLUDE_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_EXCLUDE_REASON.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
                return;
            }

            conn.QueryString = "update POLICY_BLOCKING_SERVICE set " +
                                    "EXCLUDED = 1," +
                                    "EXCLUDED_REASON = '" + TXT_EXCLUDE_REASON.Text.Trim() + "'," +
                                    "EXCLUDED_BY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "EXCLUDED_DATE = GETDATE() " +
                                    "where " +
                                    "POLICY_ID = " + LB_POLICY_ID.Text + " " +
                                    "and convert(date, MARK_DATE) = '" + LB_MARKED_DATE.Text + "'";
            conn.ExecuteNonQuery();
            LoadMarkedList();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'none';", true);
        }
    }
}
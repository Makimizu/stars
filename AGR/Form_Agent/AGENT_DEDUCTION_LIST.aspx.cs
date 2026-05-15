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
    public partial class AGENT_DEDUCTION_LIST : System.Web.UI.Page
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

            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];
            if (IsPostBack && eventTarget == "lnkClickMe")
            {
                LB_DEDUCTIONID.Text = eventArgument;
                LB_DEDUCTION_TITLE.Text = "Deduction Payment History";
                FillDGRDEDUCTIONHISTORY(LB_DEDUCTIONID.Text.ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_REMUN_DEDUCTION where CODE not in ('CLAWBACK','TAX') order by 2";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = DDL_STATUS.SelectedValue + " ";

            if (TXT_CODE.Text.Trim() != "")
                where = where + "and a.AGENT_CODE = '" + TXT_CODE.Text.Trim() + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + "and a.AGENT_NAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + "and a.DEDUCTION_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            if (DDL_STATUS.SelectedIndex > 0 && DDL_OUTSTANDING.SelectedValue != "")
                where = where + DDL_OUTSTANDING.SelectedValue;

            if (DDL_STATUS.SelectedIndex > 0 && TXT_START_DATE.Text.Trim() != "")
                where = where + "convert(date, a.SETTLE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "' ";

            if (DDL_STATUS.SelectedIndex > 0 && TXT_END_DATE.Text.Trim() != "")
                where = where + "convert(date, a.SETTLE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "' ";

            /*conn.QueryString = "select " +
                                "A.DEDUCTION_ID, " +
                                "A.AGENT_CODE, " +
                                "A.AGENT_NAME, " +
                                "A.DEDUCTION_TYPE_DESCR, " +
                                "DEDUCTION_AMOUNT		= replace(convert(varchar(100), convert(money, A.DEDUCTION_AMOUNT), 1), '.00', ''), " +
                                //"DEDUCTION_PAYMENT		= replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', ''), " +
                                "DEDUCTION_PAYMENT = CASE WHEN ISNULL(A.DEDUCTION_PAYMENT,0) > 0 THEN '<a href=''''javascript:__doPostBack(''lnkClickMe'',''' + isnull(CONVERT(VARCHAR(250),A.DEDUCTION_ID), '') + ''')''''>' + replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', '') + '</a>' ELSE replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', '') END, " +
                                "DEDUCTION_OUTSTANDING	= replace(convert(varchar(100), convert(money, A.DEDUCTION_OUTSTANDING), 1), '.00', ''), " +
                                "DEDUCTION_SETTLE_DATE	= convert(varchar(20), A.DEDUCTION_SETTLE_DATE, 106), " +
                                "REQUESTBY				= A.REQUESTBY + ' - ' + convert(varchar(100), A.REQUESTDATE), " +
                                "APPROVEBY				= CASE WHEN ISNULL(A.APPROVEBY,'') <> '' THEN A.APPROVEBY + ' - ' + convert(varchar(100), A.APPROVEDATE) ELSE '' END, " +
                                "B.NAMAFILE, " +
                                "B.CODE " +
                                "from		V_M_AGENTS_DEDUCTION A " +
                                "LEFT JOIN ARCHIEVE.dbo.AGR_ARSIP B ON CONVERT(VARCHAR(200), A.DEDUCTION_ID) = B.OWNER1 " +
                                "where " + where + " " +
                                "order by " +
                                "a.REQUESTDATE desc";*/

            string strQuery = string.Format(@"
                                        SELECT A.DEDUCTION_ID, A.AGENT_CODE, A.AGENT_NAME, A.DEDUCTION_TYPE_DESCR, 
                                        DEDUCTION_AMOUNT		= replace(convert(varchar(100), convert(money, A.DEDUCTION_AMOUNT), 1), '.00', ''), 
                                        --DEDUCTION_PAYMENT		= replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', ''), 
                                        DEDUCTION_PAYMENT = CASE WHEN ISNULL(A.DEDUCTION_PAYMENT,0) > 0 THEN '<a style=""text-decoration: none;"" href=""javascript:__doPostBack(''lnkClickMe'',''' + isnull(CONVERT(VARCHAR(250),A.DEDUCTION_ID), '') + ''')"">' + replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', '') + '</a>' ELSE replace(convert(varchar(100), convert(money, A.DEDUCTION_PAYMENT), 1), '.00', '') END,
                                        DEDUCTION_OUTSTANDING	= replace(convert(varchar(100), convert(money, A.DEDUCTION_OUTSTANDING), 1), '.00', ''), 
                                        DEDUCTION_SETTLE_DATE	= convert(varchar(20), A.DEDUCTION_SETTLE_DATE, 106), 
                                        REQUESTBY				= A.REQUESTBY + ' - ' + convert(varchar(100), A.REQUESTDATE), 
                                        APPROVEBY				= CASE WHEN ISNULL(A.APPROVEBY,'') <> '' THEN A.APPROVEBY + ' - ' + convert(varchar(100), A.APPROVEDATE) ELSE '' END, 
                                        B.NAMAFILE, B.CODE 
                                        FROM		
                                        V_M_AGENTS_DEDUCTION A 
                                        LEFT JOIN ARCHIEVE.dbo.AGR_ARSIP B ON CONVERT(VARCHAR(200), A.DEDUCTION_ID) = B.OWNER1 
                                        where {0}  
                                        order by a.REQUESTDATE desc", where);
            conn.QueryString = strQuery;

            conn.ExecuteQuery(500000);

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lb.Text = DGR.Items[i].Cells[2].Text;

                Label lbDESCR = (Label)DGR.Items[i].FindControl("LB_DESCR");
                LinkButton lbtDESCR = (LinkButton)DGR.Items[i].FindControl("LBT_DESCR");

                if (!string.IsNullOrEmpty(DGR.Items[i].Cells[13].Text) && !DGR.Items[i].Cells[13].Text.Contains("&nbsp;"))
                {
                    lbtDESCR.Visible = true;
                    lbtDESCR.Text = "Download";
                }
                else
                {
                    lbDESCR.Visible = true;
                    lbDESCR.Text = "";
                }    
            }
        }

        protected void FillDGRDEDUCTIONHISTORY(string deductionId)
        {
            conn.QueryString = "exec SP_DEDUCTION_HISTORY " +
                                "'" + deductionId + "' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DUDUCTION_HISTORY.DataSource = dt;
            DGR_DUDUCTION_HISTORY.DataBind();
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_STATUS.SelectedIndex == 0)
            {
                TR_OUTSTANDING.Visible = false;
                TR_SETTLE_DATE.Visible = false;
            }
            else
            {
                TR_OUTSTANDING.Visible = true;
                TR_SETTLE_DATE.Visible = true;
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("AGENT_DEDUCTION.aspx?ID=");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("AGENT_DEDUCTION.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[12].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.AGR_ARSIP where CODE='" + e.Item.Cells[13].Text + "'" + " AND OWNER1 = '" + e.Item.Cells[1].Text + "'",
                                    Page);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}
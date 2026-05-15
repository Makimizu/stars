using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Net;
using System.Data.SqlClient;

namespace HEALTH.Form_Klien
{
    public partial class Polis_PreRenewal : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected SqlConnection connection = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            string where = "";

            if (TXT_DATE1.Text.Trim() == "")
                where = where + "and convert(date,a.END_DATE) >= convert(date, DATEADD(month,-2, GETDATE())) ";

            if (TXT_DATE2.Text.Trim() == "")
                where = where + "and convert(date,a.END_DATE) <= convert(date, GETDATE()) ";

            if (TXT_DATE1.Text.Trim() != "")
                where = where + "and convert(date,a.END_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + "and convert(date,a.END_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "ID, " +
                                "POLICY_ID, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "END_DATE = convert(varchar(20),END_DATE,106), " +
                                "MEMBER, " +
                                "RESERVE_BILLED = replace(convert(varchar(100),convert(money,RESERVE_BILLED),1),'.00',''), " +
                                "RESERVE_EARNED = replace(convert(varchar(100),convert(money,RESERVE_EARNED),1),'.00',''), " +
                                "LOADING = replace(convert(varchar(100),convert(money,LOADING),1),'.00',''), " +
                                "TABARRU = replace(convert(varchar(100),convert(money,TABARRU),1),'.00',''), " +
                                "LOADING_AMT = replace(convert(varchar(100),convert(money,LOADING_AMT),1),'.00',''), " +
                                "TABBARU_AMT = replace(convert(varchar(100),convert(money,TABBARU_AMT),1),'.00',''), " +
                                "CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull(CLAIM,0)),1),'.00',''), " +
                                "REFUND_PREMIUM = replace(convert(varchar(100),convert(money,isnull(REFUND_PREMIUM,0)),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,isnull(BALANCE,0)),1),'.00',''), " +
                                "CLAIM_RATIO = convert(varchar(100),convert(money,a.CLAIM_RATIO * 100),1), " +
                                "CLAIM_RATIO_CALC, " +
                                "TIPE, " +
                                "MOP, " +
                                "PRODUCT, " +
                                "TPA, " +
                                "SURPLUS = (case when isnull(BALANCE,0) >= 0 then 1 else 0 end) " +
                                "from V_POLICY_PERIOD_PRERENEWAL_NEW a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.END_DATE";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton bt = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                Button btR = (Button)DGR.Items[i].FindControl("BT_RESPOND");
                Button btQ = (Button)DGR.Items[i].FindControl("BT_QUOTATION");
                bt.Text = DGR.Items[i].Cells[3].Text;
                

                if (DGR.Items[i].Cells[22].Text == "0")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                    DGR.Items[i].Cells[17].BackColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[17].ForeColor = System.Drawing.Color.White;
                }

                if (DGR.Items[i].Cells[23].Text == "0")
                {
                    btQ.BackColor = System.Drawing.Color.Red;                    
                }

                if (DGR.Items[i].Cells[24].Text == "0")
                {
                    btQ.Visible = false;
                }
                else
                {
                    btQ.Attributes.Add("onclick", "window.open('Polis_PreRenewal_Report_Frame.aspx?CODE=005&ID=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','REPORT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("PolisFrame.aspx?ID=" + e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Respon")
            {
                ShowRespond(e.Item.Cells[1].Text);
            }
        }

        protected void ShowRespond(string periodid)
        {
            LB_PERIOD.Text = periodid;

            conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK '" + periodid + "'";
            conn.ExecuteQuery();
            DGR_RESPOND.DataSource = conn.GetDataTable().Copy();
            DGR_RESPOND.DataBind();

            conn.QueryString = "select CODE, DESCR from PR_POLICY_RENEWAL_DECISION";
            conn.ExecuteQuery();
            

            for (int i = 0; i < DGR_RESPOND.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RESPOND.Items[i].FindControl("DDL_DECISION");
                TextBox txt = (TextBox)DGR_RESPOND.Items[i].FindControl("TXT_REMARK");

                if (DGR_RESPOND.Items[i].Cells[4].Text.Replace("&nbsp;", "").Trim() != "")
                {
                    System.Drawing.Color Clr = System.Drawing.Color.FromName(DGR_RESPOND.Items[i].Cells[4].Text.Replace("&nbsp;", "").Trim());
                    System.Drawing.Color ClInvert = System.Drawing.Color.FromArgb(Clr.ToArgb() ^ 0xffffff);

                    ddl.BackColor = Clr;
                    txt.BackColor = Clr;

                    ddl.ForeColor = ClInvert;
                    txt.ForeColor = ClInvert;
                }

                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddl.SelectedValue = DGR_RESPOND.Items[i].Cells[2].Text;
                }
                catch { }
                txt.Text = DGR_RESPOND.Items[i].Cells[3].Text.Replace("&nbsp;", "");
            }


            conn.QueryString = "select " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "PERIOD = convert(varchar(20),START_DATE,106) + ' - ' + convert(varchar(20),END_DATE,106), " +
                                "TABBARU_AMT = replace(convert(varchar(100),convert(money,TABBARU_AMT),1),'.00',''), " +
                                "CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull(CLAIM,0)),1),'.00',''), " +
                                "CR_DEFAULT = convert(varchar(100),convert(money, a.CLAIM_RATIO * 100),1), " +
                                "CR = convert(varchar(100),convert(money, a.CLAIM_RATIO_CALC * 100),1) " +
                                "from V_POLICY_PERIOD_PRERENEWAL_NEW a " +
                                "left join POLICY_PERIOD_CLAIM_RATIO b on a.ID = b.POLICY_PERIOD_ID " +
                                "where ID = '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_PERIODDATE.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_TABBARU.Text = conn.GetFieldValue("TABBARU_AMT").ToString();
            LB_CLAIM.Text = conn.GetFieldValue("CLAIM_PAID").ToString();
            LB_CRVAL.Text = conn.GetFieldValue("CR_DEFAULT").ToString();
            TXT_CRVAL.Text = conn.GetFieldValue("CR").ToString();

            /*
            conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK_NOTDONE '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            BT_CRSAVE.Visible = true;
            if (conn.GetRowCount() > 0)
                BT_CRSAVE.Visible = false;
            */

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }

        protected void DGR_RESPOND_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_DECISION");
                TextBox txt = (TextBox)e.Item.FindControl("TXT_REMARK");

                try
                {
                    conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK_UPSERT " +
                                        "'" + LB_PERIOD.Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + txt.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    ShowRespond(e.Item.Cells[0].Text);
                }
                catch { }
            }
        }

        protected void BT_CRSAVE_Click(object sender, EventArgs e)
        {

            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_CLAIM_RATIO_UPSERT " +
                                     "'" + LB_PERIOD.Text + "'," +
                                     TXT_CRVAL.Text.Trim() + ", " +
                                     "null, " +
                                     "null, " +
                                     "null, " +
                                     "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR();
                ShowRespond(LB_PERIOD.Text);
            }
            catch { }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Deposit : System.Web.UI.Page
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
        }

        protected void Setup()
        {
            BT_TOPUP_SAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TOPUP ?')){return false;};");
            BT_REFUND_SAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk REFUND ?')){return false;};");
            BT_ACCTRNS_SAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ACCOUNT TRANSFER ?')){return false;};");

            conn.QueryString = "select CODE, BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REFUND_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            string where = "";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.TIPE_DEPOSIT = '" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_COMPANY.TemplateSourceDirectory.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.TemplateSourceDirectory.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "POLICY_PERIOD_ID, " +
                                "TIPE_DEPOSIT, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "END_DATE = convert(varchar(20),END_DATE,106), " +
                                "DEPOSIT_AMOUNT = replace(convert(varchar(100),convert(money,DEPOSIT_AMOUNT),1),'.00',''), " +
                                "TOPUP = replace(convert(varchar(100),convert(money,TOPUP),1),'.00',''), " +
                                "USED = replace(convert(varchar(100),convert(money,USED),1),'.00',''), " +
                                "ACCTRNS = replace(convert(varchar(100),convert(money,ACCTRNS),1),'.00',''), " +
                                "REFUND = replace(convert(varchar(100),convert(money,REFUND),1),'.00',''), " +
                                "SISA = replace(convert(varchar(100),convert(money,SISA),1),'.00',''), " +
                                "PCT_SISA = replace(convert(varchar(100),convert(money,PCT_SISA),1),'.00',''), " +
                                "DEPOSIT_MIN, " +
                                "COLOR = (case when PCT_SISA <= DEPOSIT_MIN then 'Pink' else '' end), " +
                                "DETAIL_URL = b.URLAPP + '&POLICY_PERIOD_ID=' + POLICY_PERIOD_ID + '&TIPE=' + TIPE_DEPOSIT, " +
                                "ENABLE_REFUND = (case when isnull(a.SISA,0) > 0 then 1 else 0 end) " +
                                "from V_POLICY_PERIOD_DEPOSIT_VALUE a " +
                                "left join V_LINK_SC_REPORT_LIST b on b.CODE='299' " +
                                "where " + DDL_INFORCE.SelectedValue + " " + where +
                                "order by  " +
                                "a.COMPANY_NAME, a.START_DATE";

            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                Button btTRNS = (Button)DGR.Items[i].FindControl("BT_TRNS");
                Button btREFUND = (Button)DGR.Items[i].FindControl("BT_REFUND");

                btDETAIL.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[15].Text + "','DETAIL','height=600px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.FromName(DGR.Items[i].Cells[14].Text.Replace("&nbsp;", ""));
                }

                if (DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "").Substring(0,1) == "-")
                {
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Red;
                }

                if (DGR.Items[i].Cells[16].Text.Replace("&nbsp;", "") == "1")
                {
                    btTRNS.Visible = true;
                    btREFUND.Visible = true;
                }
            }
        }

        protected void FillDGRACCTRNS()
        {
            conn.QueryString = "select " +
                                    "a.POLICY_PERIOD_ID, " +
                                    "a.TIPE_DEPOSIT, " +
                                    "a.POLICY_NO, " +
                                    "a.COMPANY_NAME, " +
                                    "PERIOD = convert(varchar(20),a.START_DATE,106) + ' - ' + convert(varchar(20),a.END_DATE,106), " +
                                    "SISA = replace(convert(varchar(100), convert(money, a.SISA),1), '.00', '') " +
                                    "from V_POLICY_PERIOD_DEPOSIT_VALUE a " +
                                    "where " +
                                    "a.POLICY_PERIOD_ID collate database_default + a.TIPE_DEPOSIT <> '" + LB_POLICY_PERIOD_ID.Text + LB_TIPE.Text + "' " +
                                    "and a.COMPANY_NAME like '%" + TXT_ACCTRANS_SEARCH.Text.Trim() + "%' " +
                                    "order by " +
                                    "a.COMPANY_NAME, " +
                                    "a.START_DATE, " +
                                    "a.TIPE_DEPOSIT";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ACCTRNS.DataSource = dt;
            DGR_ACCTRNS.DataBind();

            for (int i = 0; i < DGR_ACCTRNS.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_ACCTRNS.Items[i].FindControl("LB_ACCTRANS_SELECT");

                lb.Text = DGR_ACCTRNS.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                LB_POLICY_PERIOD_ID.Text = e.Item.Cells[0].Text;
                LB_COMPANY.Text = e.Item.Cells[3].Text;
                LB_PERIOD.Text = e.Item.Cells[4].Text + " - " + e.Item.Cells[5].Text;
                LB_POLICYNO.Text = e.Item.Cells[1].Text;
                LB_TIPE.Text = e.Item.Cells[2].Text;
            }
            catch { }

            if (e.CommandName == "TOPUP")
            {   
                TR_TOPUP.Visible = true;
                TR_ACCTRNS.Visible = false;
                TR_REFUND.Visible = false;
                LB_TITLE.Text = e.CommandName;
                
                LB_AWAL.Text = e.Item.Cells[6].Text;
                LB_SISA.Text = e.Item.Cells[9].Text;

                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }


            if (e.CommandName == "ACCOUNT TRANSFER")
            {
                TR_TOPUP.Visible = false;
                TR_ACCTRNS.Visible = true;
                TR_REFUND.Visible = false;
                LB_TITLE.Text = e.CommandName;

                TXT_ACCTRNS_AMT.Text = e.Item.Cells[11].Text;
                TXT_ACCTRANS_SEARCH.Text = "";
                DGR_ACCTRNS.CurrentPageIndex = 0;
                FillDGRACCTRNS();
                
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
            
            if (e.CommandName == "REFUND")
            {
                TR_TOPUP.Visible = false;
                TR_ACCTRNS.Visible = false;
                TR_REFUND.Visible = true;
                LB_TITLE.Text = e.CommandName;

                TXT_REFUND_AMT.Text = e.Item.Cells[11].Text;
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_TOPUP_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_DEPOSIT_MUTASI_INSERT " +
                                    "'" + LB_POLICY_PERIOD_ID.Text + "'," +
                                    "'" + LB_TIPE.Text + "'," +
                                    "'C'," +
                                    "null, " +
                                    "null, " +
                                    TXT_TOPUP_AMOUNT.Text.Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch
            { }            
        }

        protected void BT_REFUND_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_REFUND_ACCNO.Text.Trim() == "" || TXT_REFUND_ACCNAME.Text.Trim() == "" || DDL_REFUND_ACCBANK.SelectedValue == "")
                return;

            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_DEPOSIT_MUTASI_INSERT " +
                                    "'" + LB_POLICY_PERIOD_ID.Text + "'," +
                                    "'" + LB_TIPE.Text + "'," +
                                    "'D'," +
                                    "null, " +
                                    "null, " +
                                    TXT_REFUND_AMT.Text.Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue(0, 0).ToString() == "1")
                {
                    string settlement_type = "12";
                    if(LB_TIPE.Text == "EXC")
                        settlement_type = "12a";

                    conn.QueryString = "exec SP_LINK_FINANCE_STL_DETAIL " +
                                        "'" + settlement_type + "'," +
                                        "'" + TXT_REFUND_AMT.Text.Replace(",", "") + "'," +
                                        "'" + LB_POLICY_PERIOD_ID.Text + "-" + LB_TIPE.Text + "'," +
                                        "'" + LB_POLICYNO.Text + "'," +
                                        "'REFUND DEPOSIT " + LB_TIPE.Text + " " + LB_COMPANY.Text + " PERIODE " + LB_PERIOD.Text + "'," +
                                        "'" + LB_COMPANY.Text + "'," +
                                        "null," +
                                        "null," +
                                        "'" + TXT_REFUND_ACCNO.Text.Trim() + "'," +
                                        "'" + DDL_REFUND_ACCBANK.SelectedValue + "'," +
                                        "'" + TXT_REFUND_ACCNAME.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                FillDGR();
            }
            catch
            { } 
        }


        protected void BT_ACCTRNS_SAVE_Click(object sender, EventArgs e)
        {
            string period = "";
            string tipe = "";

            try
            {
                period = DGR_ACCTRNS.SelectedItem.Cells[0].Text;
                tipe = DGR_ACCTRNS.SelectedItem.Cells[3].Text;
            }
            catch { }

            if (period == "" || tipe == "")
            {
                GlobalTools.popMessage(this, "Belum ada tujuan yang dipilih !");
                return;
            }

            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_DEPOSIT_MUTASI_INSERT " +
                                    "'" + LB_POLICY_PERIOD_ID.Text + "'," +
                                    "'" + LB_TIPE.Text + "'," +
                                    "'D'," +
                                    "'" + period + "', " +
                                    "'" + tipe + "', " +
                                    TXT_ACCTRNS_AMT.Text.Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
            catch
            { }  
        }

        protected void DGR_ACCTRNS_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_ACCTRNS.CurrentPageIndex = e.NewPageIndex;
            FillDGRACCTRNS();
        }

        protected void TXT_ACCTRANS_SEARCH_TextChanged(object sender, EventArgs e)
        {
            DGR_ACCTRNS.CurrentPageIndex = 0;
            FillDGRACCTRNS();
        }

        protected void BT_ACCTRANS_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_ACCTRNS.CurrentPageIndex = 0;
            FillDGRACCTRNS();
        }

        protected void RB_ACCTRNS_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_ACCTRNS.Items.Count; i++)
            {
                RadioButton rb = (RadioButton)DGR_ACCTRNS.Items[i].FindControl("RB_ACCTRNS");                
                if (rb == (RadioButton)sender)
                    rb.Checked = true;
                else
                    rb.Checked = false;
            }
        }
    }
}
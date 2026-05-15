using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class TPA_Deposit : System.Web.UI.Page
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

            conn.QueryString = "select CODE, BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from V_TPA_DEPOSIT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from V_TPA_TIPE_DEPOSIT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

        }

        protected void FillDGR()
        {
            string where = "where 1=1 ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and TIPE_DEPOSIT = '" + DDL_TIPE.SelectedValue + "' ";

            if (DDL_TPA.SelectedValue != "")
                where = where + " and TPA = '" + DDL_TPA.SelectedValue + "' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO = '" + TXT_POLICYNO.Text.Trim() + "' ";

            conn.QueryString = "SELECT * FROM V_CLM_DEPOSIT_TPA " + where + " ORDER BY START_DATE DESC";
            //conn.QueryString = "SELECT " +
            //                        "a.ID, " +
            //                        "a.POLICY_ID, " +
            //                        "START_DATE = convert(varchar(20),a.START_DATE,106), " +
            //                        "END_DATE = convert(varchar(20),a.END_DATE,106), " +
            //                        "b.POLICY_NO, " +
            //                        "d.TIPE_DEPOSIT, " +
            //                        "c.COMPANY_NAME, " +
            //                        "AMOUNT = replace(convert(varchar(100),convert(money, isnull(d.AMOUNT_TOPUP,0)),1),'.00',''), " +
            //                        "USED = replace(convert(varchar(100),convert(money,isnull(e.BYR,0)),1),'.00',''), " +
            //                        "SISA = replace(convert(varchar(100),convert(money,isnull(d.AMOUNT_TOPUP,0) - isnull(e.BYR,0)),1),'.00',''), " +
            //                        "DETAIL_URL = f.URLAPP + '&POLICY_PERIOD_ID=' + a.ID + '&TIPE=' + d.TIPE_DEPOSIT, " +
            //                        "PCT_SISA = 100 * (isnull(d.AMOUNT_TOPUP,0) - isnull(e.BYR,0)) / (case when isnull(d.AMOUNT_TOPUP,0) > 0 then isnull(d.AMOUNT_TOPUP,0) else 1 end), " +
            //                        "TPA = tpa.DESCR " +
            //                    "FROM " +
            //                        "POLICY_PERIOD a " +
            //                    "INNER JOIN " +
            //                        "POLICY b ON b.ID = a.POLICY_ID " +
            //                    "INNER JOIN " +
            //                        "COMPANY c ON c.COMPANY_CODE = b.COMPANY_CODE " +
            //                    "INNER JOIN PARAM_ACT_TPA tpa ON tpa.CODE = a.TPA " +
            //                    "LEFT JOIN " +
            //                        "( " +
            //                            "SELECT " +
            //                                "POLICY_PERIOD_ID, TIPE_DEPOSIT, AMOUNT_TOPUP = SUM(AMOUNT_TOPUP) " +
            //                            "FROM TPA_POLICY_PERIOD_DEPOSIT a " +
            //                            "LEFT JOIN FINANCE.dbo.REKENING_JURNAL_CREDIT b ON a.REKAPID = b.REKAPID " +
            //                            "WHERE b.REKAPID IS NOT NULL " +
            //                            "GROUP BY POLICY_PERIOD_ID, TIPE_DEPOSIT " +
            //                        ") d ON d.POLICY_PERIOD_ID = a.ID " +
            //                    "OUTER APPLY " +
            //                        "dbo.UFT_AMOUNT_BAYAR_DEPOSIT (d.POLICY_PERIOD_ID) e " +
            //                    "LEFT JOIN V_LINK_SC_REPORT_LIST f on f.CODE='380' " +
            //                   "WHERE " +
            //                        "(a.TPA = '09' or a.ID in (select POLICY_PERIOD_ID from ASKES_MIGRASI.dbo.POLICY_PERIOD_TC where tc_id = '9' and benefit_id = '0' and TC_VAL = '5')) " + DDL_TIPE.SelectedValue + where +
            //                    "ORDER BY " +
            //                        "a.ID";

            //conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            //for (int i = 0; i < DGR.Items.Count; i++)
            //{
            //    Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");

            //    btDETAIL.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text + "','DETAIL','height=600px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
            //}
        }
        
        protected void FillDGRHISTORY()
        {
            conn.QueryString = "SELECT * FROM V_CLM_DEPOSIT_TPA_HISTORY WHERE POLICY_PERIOD_ID = '" + LB_POLICY_PERIOD_ID.Text + "' ORDER BY CODE DESC";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_HISTORY.DataSource = dt;
            DGR_HISTORY.DataBind();
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
                TR_HISTORY.Visible = false;
                LB_TITLE.Text = e.CommandName;

                LB_AWAL.Text = e.Item.Cells[6].Text;
                LB_SISA.Text = e.Item.Cells[8].Text;

                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }

            if (e.CommandName == "HISTORY")
            {
                TR_TOPUP.Visible = false;
                TR_HISTORY.Visible = true;
                LB_TITLE.Text = e.CommandName;

                DGR_HISTORY.CurrentPageIndex = 0;
                FillDGRHISTORY();

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
            if (TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAME.Text.Trim() == "" || DDL_ACCBANK.SelectedValue == "")
                return;

            //try
            //{
                conn.QueryString = "select CODE = replace(convert(varchar(30),GETDATE(),112) + convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string CODE = conn.GetFieldValue("CODE").ToString();

                conn.QueryString = "exec SP_TPA_POLICY_PERIOD_DEPOSIT_INSERT " +
                                    "'" + CODE + "'," +
                                    "'" + LB_POLICY_PERIOD_ID.Text + "'," +
                                    "'TPAD'," +
                                    TXT_TOPUP_AMOUNT.Text.Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "'" + LB_POLICYNO.Text + "'," +
                                    "'" + LB_COMPANY.Text + "'," +
                                    "'" + LB_PERIOD.Text + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                    "'" + DDL_ACCBANK.SelectedValue + "'";
                                        
                conn.ExecuteNonQuery();

                FillDGR();
                Response.Write("<script>alert('BERHASIL, MENUNGGU PEMBAYARAN OLEH TIM FINANCE');</script>");

            //}
            //catch
            //{
            //    FillDGR();
            //    Response.Write("<script>alert('ERaROR');</script>");
            //}
        }
        
        protected void DGR_HISTORY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_HISTORY.CurrentPageIndex = e.NewPageIndex;
            FillDGRHISTORY();
        }
    }
}
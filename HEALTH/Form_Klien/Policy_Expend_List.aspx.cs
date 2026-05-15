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
    public partial class Policy_Expend_List : System.Web.UI.Page
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

            if (TXT_DATE1.Text.Trim() != "")
                where = where + "and convert(date,c.START_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + "and convert(date,c.START_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "') ";
            
            if (TXT_PROCESSDATE1.Text.Trim() != "")
                where = where + "and convert(date,a.USERDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_PROCESSDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_PROCESSDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.USERDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_PROCESSDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + "and c.POLICY_NO like '%" + TXT_POLICYNO.Text + "%' ";

            if (TXT_EXPENDNO.Text.Trim() != "")
                where = where + "and a.EXPENDNO like '%" + TXT_EXPENDNO.Text + "%' ";

            if (DDL_STATUS.SelectedValue == "REG")
                where = where + "and a.REJECTBY IS NULL and a.APPROVEBY is null ";

            if (DDL_STATUS.SelectedValue == "REJ")
                where = where + "and a.REJECTBY IS NOT NULL ";

            if (DDL_STATUS.SelectedValue == "APP")
                where = where + "and a.APPROVEBY is NOT NULL ";

            conn.QueryString = "select a.ID, a.POLICY_PERIOD_ID, a.EXPENDNO, c.POLICY_ID, " +
                               "	   c.POLICY_NO, c.COMPANY_NAME, convert(varchar(20),c.START_DATE,106) + ' - ' + convert(varchar(20),c.END_DATE,106) as PERIOD, " +
                               "	   replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00','') AMOUNT, " +
                               "	   a.ACC_NO, a.ACC_NAME, b.BANK, a.USERBY, convert(varchar(20),a.USERDATE,106) PROCESS_DATE,  " +
                               "	   case when a.REJECTBY is not null then 'REJECTED by ' + a.REJECTBY + ' on ' + convert(varchar(20),a.REJECTDATE,106) " +
                               "			when a.APPROVEBY is not null then 'APPROVED by ' + a.APPROVEBY + ' on ' + convert(varchar(20),a.APPROVEDATE,106) " +
                               "	   else 'REGISTERED' end status, " +
                               "       case when a.REJECTBY is null and a.APPROVEBY is null then 'Y' else 'T' end ROLBCK, " +
                               "	   case when a.REJECTBY is not null then 'REJECTED'" +
                               "			when a.APPROVEBY is not null then 'APPROVED'" +
                               "	   else 'REGISTERED' end STAT " +
                               "from POLICY_PERIOD_EXPEND a " +
                               "inner join FINANCE.dbo.PARAM_TBL_BANK b on a.BANK = b.CODE collate database_default " +
                               "inner join V_POLICY_PERIOD c on a.POLICY_PERIOD_ID = c.ID collate database_default " +
                               "where 1 = 1 " + where + " " +
                               "order by a.USERDATE";
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
                bt.Text = DGR.Items[i].Cells[14].Text;
                
                btQ.Attributes.Add("onclick", "window.open('Policy_Expend_history.aspx?CODE=UJR&ID=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','REPORT','height=200px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");


                if (DGR.Items[i].Cells[15].Text == "APPROVED")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.LightGreen;
                    bt.ForeColor = System.Drawing.Color.Black;
                    btR.Text = "VIEW";
                }
                if (DGR.Items[i].Cells[15].Text == "REJECTED")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.LightPink;
                    bt.ForeColor = System.Drawing.Color.Black;
                    btR.Text = "VIEW";
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
                ShowRespond(e.Item.Cells[1].Text, e.Item.Cells[3].Text, e.Item.Cells[13].Text);
            }
        }

        protected void ShowRespond(string periodid, string expendno, string rolbck)
        {
            LB_PERIOD.Text = periodid;
            LB_EXPENDNO.Text = expendno;
            LB_ROLBCK.Text = rolbck;

            TXT_CAIR.Enabled = true;
            TXT_ACCNO.Enabled = true;
            TXT_ACCNAME.Enabled = true;
            DDL_BANK.Enabled = true;
            TXT_REASON.Enabled = true;
            BT_REJECT.Visible = true;
            BT_CRSAVE.Visible = true;
            BT_APPROVE.Visible = true;

            conn.QueryString = "select CODE, BANK from FINANCE.dbo.PARAM_TBL_BANK where BANK not in ('','BANK','BANK -') order by BANK";
            conn.ExecuteQuery();
            DDL_BANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_PERIOD.Text != LB_PERIOD2.Text)
            {
                LB_ERROR.Text = "";
                TXT_CAIR.Text = "";
                TXT_ACCNO.Text = "";
                TXT_ACCNAME.Text = "";
                TXT_REASON.Text = "";
                DDL_BANK.SelectedValue = "";
            }            

            conn.QueryString = "select a.POLICY_PERIOD_ID, b.POLICY_ID, b.POLICY_NO, b.COMPANY_NAME, " +
                                       "convert(varchar(20),b.START_DATE,106) + ' - ' + convert(varchar(20),b.END_DATE,106) as PERIOD, " +
                                       "b.TIPE_DESCR NBRN, a.VAL*100 PERSENCADUJROH, " +
                                       "replace(convert(varchar(100),convert(money,SUM(c.AMOUNT)),1),'.00','') KONTRIBUSI, " +
                                       "replace(convert(varchar(100),convert(money,a.VAL * SUM(c.AMOUNT)),1),'.00','') as CADANGAN_UJROH, " +
                                       "replace(convert(varchar(100),convert(money,ISNULL(f.JMLAMOUNT,0)),1),'.00','') USED_CADANGAN_UJROH, " +
                                       "replace(convert(varchar(100),convert(money,(a.VAL * SUM(c.AMOUNT))-ISNULL(f.JMLAMOUNT,0)),1),'.00','') as SISA_CADANGAN_UJROH " +
                                "from POLICY_PERIOD_LOADING a "+
                                "inner join V_POLICY_PERIOD b on a.POLICY_PERIOD_ID = b.ID collate database_default /*and b.END_DATE >= GETDATE()*/ " +
                                "inner join FINANCE.dbo.V_INVOICE_MASTER c on b.POLICY_NO = c.CUSTOMER_CODE collate database_default and INVOICE_TYPE in ('001','002') " +
                                "inner join V_POLICY_PERIOD_INVOICE d on b.ID = d.POLICY_PERIOD_ID and c.INVOICENO = d.INVOICENO " +
                                "inner join (select POLICY_PERIOD_ID, SUM(isnull(AMOUNT,0)) JMLAMOUNT from POLICY_PERIOD_EXPEND group by POLICY_PERIOD_ID) e on b.ID = e.POLICY_PERIOD_ID " +
                                "left join (select POLICY_PERIOD_ID, SUM(isnull(AMOUNT,0)) JMLAMOUNT from POLICY_PERIOD_EXPEND where APPROVEBY IS not null group by POLICY_PERIOD_ID) f on b.ID = f.POLICY_PERIOD_ID " +
                                "where a.LOADING_CODE = '009' and a.VAL > 0 and a.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                "group by a.POLICY_PERIOD_ID, b.POLICY_ID, b.POLICY_NO, b.COMPANY_NAME, b.START_DATE, b.END_DATE, b.TIPE_DESCR, a.VAL, e.JMLAMOUNT, f.JMLAMOUNT ";
            conn.ExecuteQuery();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_PERIODDATE.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_CUVAL.Text = conn.GetFieldValue("PERSENCADUJROH").ToString();
            LB_CADUJROH.Text = conn.GetFieldValue("CADANGAN_UJROH").ToString();
            LB_SISACADUJROH.Text = conn.GetFieldValue("SISA_CADANGAN_UJROH").ToString();

            conn.QueryString = "select a.AMOUNT, a.ACC_NO, a.ACC_NAME, a.BANK, a.REASON from POLICY_PERIOD_EXPEND a where a.EXPENDNO = '" + LB_EXPENDNO.Text + "' ";
            conn.ExecuteQuery();
            TXT_CAIR.Text = conn.GetFieldValue("AMOUNT").ToString();
            TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            TXT_ACCNAME.Text = conn.GetFieldValue("ACC_NAME").ToString();
            TXT_REASON.Text = conn.GetFieldValue("REASON").ToString();
            DDL_BANK.SelectedValue = conn.GetFieldValue("BANK").ToString();

            if (LB_ROLBCK.Text == "T")
            {
                TXT_CAIR.Enabled = false;
                TXT_ACCNO.Enabled = false;
                TXT_ACCNAME.Enabled = false;
                DDL_BANK.Enabled = false;
                TXT_REASON.Enabled = false;
                BT_REJECT.Visible = false;
                BT_CRSAVE.Visible = false;
                BT_APPROVE.Visible = false;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }

        protected void BT_CRSAVE_Click(object sender, EventArgs e)
        {

            try
            {

                if (TXT_CAIR.Text.Trim() == "" || TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAME.Text.Trim() == "" || DDL_BANK.SelectedValue == "")
                {
                    showMessage("Silahkan lengkapi data pencairan terlebih dahulu", "ERROR");
                    LB_ERROR.Text = "Silahkan lengkapi data pencairan terlebih dahulu";
                }
                else
                {
                    conn.QueryString = "select b.ID " +
                                       "from POLICY_PERIOD_LOADING a " +
                                       "inner join V_POLICY_PERIOD b on a.POLICY_PERIOD_ID = b.ID collate database_default  " +
                                       "inner join FINANCE.dbo.V_INVOICE_MASTER c on b.POLICY_NO = c.CUSTOMER_CODE collate database_default and INVOICE_TYPE in ('001','002')  " +
                                       "inner join V_POLICY_PERIOD_INVOICE d on b.ID = d.POLICY_PERIOD_ID and c.INVOICENO = d.INVOICENO " +
                                       " left join (select POLICY_PERIOD_ID, SUM(isnull(AMOUNT,0)) JMLAMOUNT from POLICY_PERIOD_EXPEND where APPROVEBY IS not null group by POLICY_PERIOD_ID) e " +
                                       "		   on b.ID = e.POLICY_PERIOD_ID " +
                                       "where a.LOADING_CODE = '009' and a.VAL > 0  and b.ID = '" + LB_PERIOD.Text + "' " +
                                       "group by b.ID, a.VAL, e.JMLAMOUNT " +
                                       "having (a.VAL * SUM(c.AMOUNT))-ISNULL(e.JMLAMOUNT,0) < " + TXT_CAIR.Text.Trim();
                    conn.ExecuteQuery(120);
                    if (conn.GetRowCount() > 0)
                    {
                        showMessage("Pengajuan pencairan tidak boleh lebih besar daripada Sisa Cadangan Ujroh", "ERROR");
                        LB_ERROR.Text = "Pengajuan pencairan tidak boleh lebih besar daripada Sisa Cadangan Ujroh";
                    }
                    else
                    {
                        conn.QueryString = "exec SP_POLICY_PERIOD_EXPEND_UPSERT " +
                                             "'" + LB_PERIOD.Text + "', " +
                                             "'" + LB_EXPENDNO.Text + "', " +
                                             "'UJR', " +
                                             TXT_CAIR.Text.Trim() + ", " +
                                             "'" + TXT_ACCNO.Text + "', " +
                                             "'" + TXT_ACCNAME.Text + "', " +
                                             "'" + DDL_BANK.SelectedValue + "', " +
                                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                        FillDGR();
                    }
                }
                LB_PERIOD2.Text = LB_PERIOD.Text;
            }

            catch (Exception ex)
            {
                showMessage(ex.Message, "error");
                LB_ERROR.Text = ex.Message;
                LB_PERIOD2.Text = LB_PERIOD.Text;
            }
        }

        protected void AppRjct(string expendno, string reason, string nextstatus)
        {

            string wherestatus = "";
            try
            {

                if (TXT_CAIR.Text.Trim() == "" || TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAME.Text.Trim() == "" || DDL_BANK.SelectedValue == "")
                {
                    showMessage("Silahkan lengkapi data pencairan terlebih dahulu", "ERROR");
                    LB_ERROR.Text = "Silahkan lengkapi data pencairan terlebih dahulu";
                }
                else if (reason == "")
                {
                    showMessage("Silahkan isi alasan terlebih dahulu", "ERROR");
                    LB_ERROR.Text = "Silahkan isi alasan terlebih dahulu";
                }
                else
                {
                    conn.QueryString = "select b.ID " +
                                       "from POLICY_PERIOD_LOADING a " +
                                       "inner join V_POLICY_PERIOD b on a.POLICY_PERIOD_ID = b.ID collate database_default  " +
                                       "inner join FINANCE.dbo.V_INVOICE_MASTER c on b.POLICY_NO = c.CUSTOMER_CODE collate database_default and INVOICE_TYPE in ('001','002')  " +
                                       "inner join V_POLICY_PERIOD_INVOICE d on b.ID = d.POLICY_PERIOD_ID and c.INVOICENO = d.INVOICENO " +
                                       " left join (select POLICY_PERIOD_ID, SUM(isnull(AMOUNT,0)) JMLAMOUNT from POLICY_PERIOD_EXPEND where APPROVEBY IS not null group by POLICY_PERIOD_ID) e " +
                                       "		   on b.ID = e.POLICY_PERIOD_ID " +
                                       "where a.LOADING_CODE = '009' and a.VAL > 0  and b.ID = '" + LB_PERIOD.Text + "' " +
                                       "group by b.ID, a.VAL, e.JMLAMOUNT " +
                                       "having (a.VAL * SUM(c.AMOUNT))-ISNULL(e.JMLAMOUNT,0) < " + TXT_CAIR.Text.Trim();
                    conn.ExecuteQuery(120);
                    if (conn.GetRowCount() > 0)
                    {
                        showMessage("Pengajuan pencairan tidak boleh lebih besar daripada Sisa Cadangan Ujroh", "ERROR");
                        LB_ERROR.Text = "Pengajuan pencairan tidak boleh lebih besar daripada Sisa Cadangan Ujroh";
                    }
                    else
                    {
                        conn.QueryString = "exec SP_POLICY_PERIOD_EXPEND_APPROVE " +
                                             "'" + LB_EXPENDNO.Text + "', " +
                                             "'" + TXT_REASON.Text + "', " +
                                             "'" + nextstatus + "', " +
                                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                        FillDGR();
                    }
                }
                LB_ERROR.Text = wherestatus;
                LB_PERIOD2.Text = LB_PERIOD.Text;
            }

            catch (Exception ex)
            {
                showMessage(ex.Message, "error");
                LB_ERROR.Text = ex.Message;
                LB_PERIOD2.Text = LB_PERIOD.Text;
            }
        }

        protected void BT_Approve_Click(object sender, EventArgs e)
        {
            AppRjct(LB_EXPENDNO.Text, TXT_REASON.Text, "APPROVE");
        }

        protected void BT_Reject_Click(object sender, EventArgs e)
        {
            AppRjct(LB_EXPENDNO.Text, TXT_REASON.Text, "REJECT");
        }

        private void showMessage(string sMessage, string sType)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertmesg", "<script language=javascript> Swal.fire({position: 'top-end',type: '" + sType + "',title: '" + sMessage.Replace("'", "") + "',showConfirmButton: false,timer: 3000});</script>");
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace UWBOX.Form_Finance
{
    public partial class Memo : System.Web.UI.Page
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
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            if (LB_APP.Text != "")
                BT_REKAP.Visible = false;

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }

            FillDDLRange();
        }

        protected void FillDDLTipe()
        {
            conn.QueryString = "select CODE,DESCR from FINANCE.dbo.PARAM_TIPE_SETTLEMENT where APP_ID = '" + LB_APP.Text + "' order by 1";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLRange()
        {
            DDL_RANGE.Items.Clear();

            conn.QueryString = "select " +
                                "CODE = 'and (b.AMOUNT between ' + convert(varchar(100),convert(money,MIN_AMOUNT)) + ' and ' + convert(varchar(100),convert(money,MAX_AMOUNT)) + ')', " +
                                "DESCR = replace(convert(varchar(100),convert(money,MIN_AMOUNT),1),'.00','') + ' - ' + replace(convert(varchar(100),convert(money,MAX_AMOUNT),1),'.00','') " +
                                "from FINANCE.dbo.PARAM_SETTLEMENT_APPROVAL_LEVEL " +
                                "where " +
                                "APP_ID = '" + LB_APP.Text + "' " +
                                "and CODE = '" + DDL_TIPE.SelectedValue + "'  " +
                                "order by MIN_AMOUNT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_RANGE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            FillDDLRange();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and a.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (DDL_RANGE.SelectedValue != "")
            {
                where = where + DDL_RANGE.SelectedValue + " ";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAPID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DESCR.Text.Trim() != "")
            {
                where = where + " and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and a.USERDATE >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and a.USERDATE <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }


            conn.QueryString = "select " +
                                "REKAPID				= a.REKAPID,  " +
                                "DESCR					= a.DESCR,  " +
                                "AMOUNT					= replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''),  " +
                                "OUTSTANDING			= replace(convert(varchar(100),convert(money,isnull(d.OUTSTANDING,0)),1),'.00',''),  " +
                                "CNT					= b.CNT, " +
                                "DESTINATION_BANK_DESCR	= c.BANK,  " +
                                "AGING					= datediff(day, a.USERDATE, GETDATE()),  " +
                                "GENERATE				= b.USERBY +  ' (' + convert(varchar(50), a.USERDATE) + ')',  " +
                                "URL					= f.URLAPP + '&REKAPID=' + a.REKAPID,  " +
                                "ENABLE_REFUND			= aa.ENABLE_REFUND,  " +
                                "AUTH					= (case when cc.USERID is not null and a.DESTINATION_BANK <> 'X' then 1 else 0 end) " +
                                "from		FINANCE.dbo.SETTLEMENT_MASTER a " +
                                "inner join	FINANCE.dbo.PARAM_TIPE_SETTLEMENT aa on a.APP_ID = aa.APP_ID and a.TIPE_SETTLEMENT = aa.CODE " +
                                "inner join	(	select " +
                                "                REKAPID, " +
                                "                CUSTOMER_CODE	= MIN(CUSTOMER_CODE), " +
                                "                USERBY			= MIN(USERBY), " +
                                "                AMOUNT			= SUM(AMOUNT), " +
                                "                CNT				= count(TRXID) " +
                                "                from FINANCE.dbo.SETTLEMENT_DETAIL " +
                                "                group by " +
                                "                REKAPID " +
                                "                ) b on a.REKAPID = b.REKAPID " +
                                "inner join	FINANCE.dbo.PARAM_TBL_BANK c on a.DESTINATION_BANK = c.CODE " +
                                "left join	(	select " +
                                "                APP_ID, " +
                                "                CUSTOMER_CODE, " +
                                "                OUTSTANDING		= SUM(OUTSTANDING) " +
                                "                from FINANCE.dbo.V_INVOICE_MASTER " +
                                "                group by " +
                                "                APP_ID, " +
                                "                CUSTOMER_CODE " +
                                "                ) d on a.APP_ID = d.APP_ID and b.CUSTOMER_CODE = d.CUSTOMER_CODE " +
                                "inner join	FINANCE.dbo.V_LINK_SC_REPORT_LIST f on f.CODE='2' " +
                                "left join	FINANCE.dbo.PARAM_SETTLEMENT_APPROVAL_LEVEL bb on a.APP_ID=bb.APP_ID and a.TIPE_SETTLEMENT=bb.CODE and (b.AMOUNT between bb.MIN_AMOUNT and bb.MAX_AMOUNT)  " +
                                "left join	FINANCE.dbo.PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL cc on bb.APP_ID=cc.APP_ID and bb.CODE=cc.CODE and bb.MIN_AMOUNT=cc.MIN_AMOUNT and cc.USERID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                "where " +
                                "a.APP_ID='" + LB_APP.Text + "'" +
                                "and a.APPROVALBY is null " + where + " " +
                                "order by a.USERDATE ";
            conn.ExecuteQuery(500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                Button btRFD = (Button)DGR.Items[i].FindControl("BT_RFD");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[11].Text == "0")
                    btRFD.Visible = false;


                if (DGR.Items[i].Cells[12].Text == "0")
                    cb.Visible = false;

            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }

            if (e.CommandName == "Refund")
            {
                TR_STL1.Visible = false;
                TR_STL2.Visible = false;
                TR_RFD.Visible = true;

                conn.QueryString = "select " +
                                    "a.APP_ID, " +
                                    "a.TRXID, " +
                                    "a.TIPE_SETTLEMENT, " +
                                    "a.CUSTOMER_CODE, " +
                                    "a.DOCNO, " +
                                    "a.DESCR, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT - isnull(d.AMOUNT,0)),1),'.00',''), " +
                                    "COMPANY = c.COMPANY_NAME " +
                                    "from FINANCE.dbo.SETTLEMENT_DETAIL a " +
                                    "left join (select STL_TRXID, AMOUNT = SUM(AMOUNT) from FINANCE.dbo.SETTLEMENT_REFUND group by STL_TRXID) d on a.TRXID = d.STL_TRXID " +
                                    "inner join FINANCE.dbo.V_LINK_CB_POLICY_GROUP_MASTER b on a.CUSTOMER_CODE=b.POLICY_NO and a.APP_ID=b.APP_ID " +
                                    "inner join FINANCE.dbo.V_LINK_CB_COMPANY c on b.COMPANY_CODE=c.COMPANY_CODE " +
                                    "where " +
                                    "a.REKAPID = '" + e.Item.Cells[2].Text + "' " +
                                    "order by " +
                                    "c.COMPANY_NAME, " +
                                    "a.DESCR";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_RFD.DataSource = dt;
                DGR_RFD.DataBind();

                for (int i = 0; i < DGR_RFD.Items.Count; i++)
                {
                    TextBox txtAMOUNT = (TextBox)DGR_RFD.Items[i].FindControl("TXT_AMOUNT");
                    txtAMOUNT.Text = DGR_RFD.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_APPROCE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "update FINANCE.dbo.SETTLEMENT_MASTER set " +
                                            "APPROVALBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "APPROVALDATE = GETDATE() " +
                                            "where " +
                                            "REKAPID = '" + DGR.Items[i].Cells[2].Text + "'";
                        conn.ExecuteNonQuery();

                        SendEmail(DGR.Items[i]);

                    }
                    catch { }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void SendEmail(System.Web.UI.WebControls.DataGridItem e)
        {
            conn.QueryString = "select * from FINANCE.dbo.V_SETTLEMENT_MASTER where REKAPID='" + e.Cells[2].Text + "'";
            conn.ExecuteQuery();

            string aprby = conn.GetFieldValue("APPROVALBY").ToString();
            string aprdate = conn.GetFieldValue("APPROVALDATE").ToString();

            conn.QueryString = "select " +
                                "a.BODY, a.CC, RECIPIENT = b.EMAIL " +
                                "from FINANCE.dbo.V_LINK_SC_PARAM_EMAIL a " +
                                "left join FINANCE.dbo.V_LINK_SC_M_CONTACT_INFO b on b.UNIT_ID='5' " +
                                "where a.CODE=1";
            conn.ExecuteQuery();

            string sender = GlobalUse.GetUserMgmt(Session["s"].ToString(), "Email");
            string recipient = conn.GetFieldValue("RECIPIENT").ToString();
            string body = conn.GetFieldValue("BODY").ToString().Replace("@DOCNO", e.Cells[2].Text).Replace("@DESCR", e.Cells[3].Text).Replace("@AMOUNT", e.Cells[4].Text).Replace("@APPBY", aprby).Replace("@APPDATE", aprdate).Replace("@REPORT", e.Cells[10].Text);
            string subject = "PAYMENT REQUEST: " + e.Cells[3].Text;
            string CC = conn.GetFieldValue("CC").ToString();
            string BCC = GlobalUse.GetUserMgmt(Session["s"].ToString(), "Email");

            string[] attachment = new string[0];
            GlobalUse.SendEmail(sender, recipient, CC, BCC, subject, body, attachment);
        }

        protected void BT_REKAP_Click(object sender, EventArgs e)
        {
            string appid = "null";
            string tipe = "null";

            if (LB_APP.Text != "")
                appid = "'" + LB_APP.Text + "'";
            if (LB_TIPE.Text != "")
                tipe = "'" + LB_TIPE.Text + "'";

            //try
            //{
            conn.QueryString = "exec FINANCE.dbo.SP_JOB_SETTLEMENT_MASTER " +
                                appid + "," +
                                tipe + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            Setup();
            DGR.CurrentPageIndex = 0;
            FillDGR();
            //}
            //catch { }
        }

        protected void BT_PROCESS_RFD_Click(object sender, EventArgs e)
        {
            if (TXT_RFD_DESCR.Text.Trim() == "")
                return;

            for (int i = 0; i < DGR_RFD.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_RFD.Items[i].FindControl("CB0");
                TextBox txtAMOUNT = (TextBox)DGR_RFD.Items[i].FindControl("TXT_AMOUNT");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec FINANCE.dbo.SP_SETTLEMENT_REFUND_INSERT " +
                                    "'" + DGR_RFD.Items[i].Cells[1].Text + "'," +
                                    "'" + DGR_RFD.Items[i].Cells[2].Text + "'," +
                                    "'" + TXT_RFD_DESCR.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            try
            {
                conn.QueryString = "exec FINANCE.dbo.SP_SETTLEMENT_REFUND_REKAP";
                conn.ExecuteNonQuery();
            }
            catch { }

            DGR_RFD.CurrentPageIndex = 0;
            FillDGR();

            TR_STL1.Visible = true;
            TR_STL2.Visible = true;
            TR_RFD.Visible = false;
        }

        protected void DGR_RFD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR_RFD.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_RFD.Items[i].FindControl("CB0");
                    cb.Checked = true;
                }
            }
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLRange();
        }

        protected void DDL_RANGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Tool
{
    public partial class Memo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("FN"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APP.Text = Request.QueryString["APPID"];
                LB_TIPE.Text = Request.QueryString["TIPE"];

                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            FillDDLRange();

            if (LB_TIPE.Text != "")
            {
                LB_TIPE.Text = LB_TIPE.Text;
            }
        }

        protected void FillDDLRange()
        {
            DDL_RANGE.Items.Clear();

            conn.QueryString = "select " +
                                "CODE = 'and (b.AMOUNT between ' + convert(varchar(100),convert(money,MIN_AMOUNT)) + ' and ' + convert(varchar(100),convert(money,MAX_AMOUNT)) + ')', " +
                                "DESCR = replace(convert(varchar(100),convert(money,MIN_AMOUNT),1),'.00','') + ' - ' + replace(convert(varchar(100),convert(money,MAX_AMOUNT),1),'.00','') " +
                                "from PARAM_SETTLEMENT_APPROVAL_LEVEL " +
                                "where " +
                                "APP_ID = '" + LB_APP.Text + "' " +
                                "and CODE = '" + LB_TIPE.Text + "'  " +
                                "order by MIN_AMOUNT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_RANGE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                                "REKAPID				= a.REKAPID, " +
                                "DESCR					= b.DESCR, " +
                                "AMOUNT					= replace(convert(varchar(100),convert(money, b.AMOUNT),1),'.00',''),   " +
                                "CNT					= b.CNT, " +
                                "DESTINATION_BANK_DESCR	= c.BANK, " +
                                "AGING					= datediff(day, a.USERDATE, GETDATE()), " +
                                "URL					= f.URLAPP + '&REKAPID=' + a.REKAPID, " +
                                "AUTH					= (case when cc.USERID is not null and a.DESTINATION_BANK <> 'X' then 1 else 0 end) " +
                                "from		SETTLEMENT_MASTER a " +
                                "left join	V_LINK_SC_REPORT_LIST f on f.CODE='2' " +
                                "left join	PARAM_TBL_BANK c on a.DESTINATION_BANK = c.CODE " +
                                "inner join	(	select " +
                                "                REKAPID, " +
                                "                DESCR		= MIN(DESCR), " +
                                "                AMOUNT		= SUM(AMOUNT), " +
                                "                CNT			= count(TRXID) " +
                                "                from SETTLEMENT_DETAIL  " +
                                "                group by " +
                                "                REKAPID " +
                                "                ) b on a.REKAPID = b.REKAPID " +
                                "left join	PARAM_SETTLEMENT_APPROVAL_LEVEL bb on a.APP_ID=bb.APP_ID and a.TIPE_SETTLEMENT=bb.CODE and (b.AMOUNT between bb.MIN_AMOUNT and bb.MAX_AMOUNT) " +
                                "left join	PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL cc on bb.APP_ID=cc.APP_ID and bb.CODE=cc.CODE and bb.MIN_AMOUNT=cc.MIN_AMOUNT and cc.USERID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                "where " +
                                "a.APP_ID = '" + LB_APP.Text + "' " +
                                "and a.TIPE_SETTLEMENT = '" + LB_TIPE.Text + "' " +
                                "and a.APPROVALBY is null " + where + " " +
                                "order by datediff(day, a.USERDATE, GETDATE())";


            /*
            conn.QueryString = "select " +
                                "REKAPID				= a.REKAPID,  " +
                                "DESCR					= a.DESCR,  " +
                                "AMOUNT					= replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''),  " +
                                "CNT					= b.CNT, " +
                                "DESTINATION_BANK_DESCR	= c.BANK,  " +
                                "AGING					= datediff(day, a.USERDATE, GETDATE()),  " +
                                "GENERATE				= b.USERBY +  ' (' + convert(varchar(50), a.USERDATE) + ')',  " +
                                "URL					= f.URLAPP + '&REKAPID=' + a.REKAPID,  " +
                                "AUTH					= (case when cc.USERID is not null and a.DESTINATION_BANK <> 'X' then 1 else 0 end) " +
                                "from		SETTLEMENT_MASTER a " +
                                "inner join	PARAM_TIPE_SETTLEMENT aa on a.APP_ID = aa.APP_ID and a.TIPE_SETTLEMENT = aa.CODE " +
                                "inner join	(	select " +
                                "                REKAPID, " +
                                "                USERBY			= MIN(USERBY), " +
                                "                AMOUNT			= SUM(AMOUNT), " +
                                "                CNT				= count(TRXID) " +
                                "                from SETTLEMENT_DETAIL " +
                                "                group by " +
                                "                REKAPID " +
                                "                ) b on a.REKAPID = b.REKAPID " +
                                "left join	PARAM_TBL_BANK c on a.DESTINATION_BANK = c.CODE " +
                                "left join	V_LINK_SC_REPORT_LIST f on f.CODE='2' " +
                                "left join	PARAM_SETTLEMENT_APPROVAL_LEVEL bb on a.APP_ID=bb.APP_ID and a.TIPE_SETTLEMENT=bb.CODE and (b.AMOUNT between bb.MIN_AMOUNT and bb.MAX_AMOUNT)  " +
                                "left join	PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL cc on bb.APP_ID=cc.APP_ID and bb.CODE=cc.CODE and bb.MIN_AMOUNT=cc.MIN_AMOUNT and cc.USERID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                "where " +
                                "a.APPROVALBY is null " + where + " " +
                                "order by a.USERDATE ";
            */
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

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");


                if (DGR.Items[i].Cells[9].Text == "0")
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

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from SETTLEMENT_MASTER where REKAPID = '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
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
                        conn.QueryString = "update SETTLEMENT_MASTER set " +
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
            conn.QueryString = "select * from V_SETTLEMENT_MASTER where REKAPID='" + e.Cells[2].Text + "'";
            conn.ExecuteQuery();

            string aprby = conn.GetFieldValue("APPROVALBY").ToString();
            string aprdate = conn.GetFieldValue("APPROVALDATE").ToString();

            conn.QueryString = "select " +
                                "a.BODY, a.CC, RECIPIENT = b.EMAIL " +
                                "from V_LINK_SC_PARAM_EMAIL a " +
                                "left join V_LINK_SC_M_CONTACT_INFO b on b.UNIT_ID='5' " +
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
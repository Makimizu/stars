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
    public partial class Invoice_Email : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected Connection connsec = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TIPE.Text = Request.QueryString["TIPE"];
                Setup();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            /*
            if (TXT_REMINDER.Text.Trim() != "")
                where = where + " and a.SEQ='" + TXT_REMINDER.Text.Trim() + "' ";
            */

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.INVOICE_TYPE='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DUEDATE.Text.Trim() != "")
                where = where + " and convert(date,a.DOC_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_DUEDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DUEDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.DOC_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_DUEDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_SENDDATE.Text.Trim() != "")
                where = where + " and a.FIRST_SEND is not null and convert(date,a.FIRST_SEND) >= '" + GlobalUse.GlobalDateFormat(TXT_SENDDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_SENDDATE2.Text.Trim() != "")
                where = where + " and a.FIRST_SEND is not null and convert(date,a.FIRST_SEND) <= '" + GlobalUse.GlobalDateFormat(TXT_SENDDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "SEQ, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "DOC_DATE = convert(varchar(20),DOC_DATE,106), " +
                                "AGING, " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,PAYMENT_AMOUNT),1),'.00',''), " +
                                "INVOICE_TYPE_DESCR, " +
                                "CUSTOMER_NAME, " +
                                "PIC_EMAIL, " +
                                "EMAIL_TIPE, " +
                                "REPORT_CODE, " +
                                "REPORT_URL, " +
                                "FIRST_SEND = convert(varchar(100),FIRST_SEND), " +
                                "LAST_SEND = convert(varchar(100),LAST_SEND) " +
                                "from V_INVOICE_MASTER_EMAIL a " +
                                "where " +
                                "TIPE like '" + LB_TIPE.Text + "%' " + DDL_SENDED.SelectedValue + where +
                                "order by a.INVOICE_DATE desc";
            try
            {
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            if (LB_TIPE.Text == "R")
                DGR.Columns[1].Visible = true;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                txtEMAIL.Text = DGR.Items[i].Cells[9].Text.Trim().Replace("&nbsp;", "");
                lbID.Text = DGR.Items[i].Cells[0].Text.Trim().Replace("&nbsp;", "");

                lbID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[15].Text.Replace("&nbsp;", "") + "','RECEIPT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void Setup()
        {
            /*
            if (LB_TIPE.Text == "R")
                TR_REMINDER.Visible = true;
            */

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from INVOICE_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLInvoice();
        }

        protected void FillDDLInvoice()
        {
            conn.QueryString = "select INVOICE_TYPE,DESCR from PARAM_INVOICE_TYPE where APP_ID = '" + DDL_APP.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";
            if (e.CommandName == "Send")
            {
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

                if (txtEMAIL.Text.Trim() == "")
                    return;

                try
                {
                    SendEmail(e);                    
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "<BR>" + ex.Message;
                    return;
                }

                if (LB_TIPE.Text == "B")
                {                    
                    conn.QueryString = "exec SP_INVOICE_BLOCKING_SERVICE_UPSERT " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "1," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

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
               

        protected void SendEmail(DataGridCommandEventArgs e)
        {
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

            if (txtEMAIL.Text.Trim() == "")
                return;

            connsec.QueryString = "exec SP_PARAM_EMAIL " +
                                "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                e.Item.Cells[13].Text + "," +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "'" + e.Item.Cells[3].Text + "'";
            connsec.ExecuteQuery();

            string sender = connsec.GetFieldValue("DEFAULT_SENDER").ToString();
            string CC = connsec.GetFieldValue("CC").ToString();
            string BCC = connsec.GetFieldValue("BCC").ToString();
            string body = connsec.GetFieldValue("BODY").ToString();
            string subject = "INVOICE " + e.Item.Cells[8].Text + " - " + e.Item.Cells[7].Text + " (" + e.Item.Cells[0].Text + ")";

            if (LB_TIPE.Text == "R")
                subject = "REMINDER " + subject;

            string path = Request.PhysicalApplicationPath + "Upload";
            string[] attachment = new string[1];

            attachment[0] = GlobalUse.RenderReport(e.Item.Cells[14].Text, "PDF", path, e.Item.Cells[0].Text, "", "", "", "", "", "", "", "", "");

            string sendresult = GlobalUse.SendEmail(sender, txtEMAIL.Text.Trim(), CC, BCC, subject, body, attachment);
            if (sendresult.Trim() != "")
            {
                LB_ERROR.Text = sendresult;
            }
            else
            {
                connsec.QueryString = "exec SP_EMAIL_SENDED_LOG_UPSERT " +
                                    "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                    "'" + e.Item.Cells[13].Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[3].Text + "'," +
                                    "'" + sender + "'," +
                                    "'" + txtEMAIL.Text.Trim() + "'";
                connsec.ExecuteNonQuery();
            }

            for (int i = 0; i < attachment.Count(); i++)
            {
                if (File.Exists(attachment[i]))
                    File.Delete(attachment[i]);
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

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLInvoice();
        }
    }
}
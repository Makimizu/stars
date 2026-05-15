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
    public partial class Invoice_Payment_Receipt : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected Connection connsec = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
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
                FillDGR();
            }
        }

        protected void Setup()
        {
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

        protected void FillDGR()
        {
            string where = "";
            
            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_CUSTCODE.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_CUSTCODE.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.INVOICE_TYPE='" + DDL_TIPE.SelectedValue + "' ";

            
            if (TXT_STLDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.DOC_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STLDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STLDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.SETTLE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STLDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select * " +
                                "from V_REKENING_JURNAL_DEBET_EMAIL a " +
                                "where " +
                                "APP_ID = '" + DDL_APP.SelectedValue+ "' " + 
                                "and FIRST_SEND " + DDL_SENDED.SelectedValue + " " +
                                where +
                                "order by SETTLE_DATE desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbdocno = (LinkButton)DGR.Items[i].FindControl("LB_DOCNO");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                txtEMAIL.Text = DGR.Items[i].Cells[7].Text.Trim().Replace("&nbsp;", "");
                lbdocno.Text = DGR.Items[i].Cells[0].Text.Trim().Replace("&nbsp;", "");

                lbdocno.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "") + "','PAYMENT_RECEIPT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLInvoice();
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

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void SendEmail(DataGridCommandEventArgs e)
        {
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

            if (txtEMAIL.Text.Trim() == "")
                return;

            connsec.QueryString = "exec SP_PARAM_EMAIL " +
                                "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                 "'" + e.Item.Cells[12].Text + "'," +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "'" + e.Item.Cells[3].Text + "'";
            connsec.ExecuteQuery();

            string sender = connsec.GetFieldValue("DEFAULT_SENDER").ToString();
            string CC = connsec.GetFieldValue("CC").ToString();
            string BCC = connsec.GetFieldValue("BCC").ToString();
            string body = connsec.GetFieldValue("BODY").ToString();
            string subject = connsec.GetFieldValue("SUBJECT").ToString();


            string path = Request.PhysicalApplicationPath + "Upload";
            string[] attachment = new string[1];
            attachment[0] = "";

            string sendresult = GlobalUse.SendEmail(sender, txtEMAIL.Text.Trim(), CC, BCC, subject, body, attachment);
            if (sendresult.Trim() != "")
            {
                LB_ERROR.Text = sendresult;
            }
            else
            {
                connsec.QueryString = "exec SP_EMAIL_SENDED_LOG_UPSERT " +
                                    "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                    "'" + e.Item.Cells[12].Text + "'," +
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
    }
}
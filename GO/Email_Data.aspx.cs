using System;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GO
{
    public partial class Email_Data : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APP.Text = Request.QueryString["APPID"];
                LB_CODE.Text = Request.QueryString["CODE"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";
            string where = "";


            conn.QueryString = "select * from PARAM_EMAIL_SQL where APP_ID='" + LB_APP.Text + "' and CODE=" + LB_CODE.Text;
            conn.ExecuteQuery();
                        
            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and a." + conn.GetFieldValue("DOCNO").ToString() + "='" + TXT_DOCNO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a." + conn.GetFieldValue("CUSTOMER_NAME").ToString() + " like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_PROCDATE.Text.Trim() != "")
                where = where + " and convert(date, a." + conn.GetFieldValue("DOCDATE").ToString() + ") >= '" + GlobalUse.GlobalDateFormat(TXT_PROCDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_PROCDATE2.Text.Trim() != "")
                where = where + " and convert(date, a." + conn.GetFieldValue("DOCDATE").ToString() + ") <= '" + GlobalUse.GlobalDateFormat(TXT_PROCDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            string SQL = "select " +
                         "DOCNO   = a." + conn.GetFieldValue("DOCNO").ToString() + "," +
                         "DOCDATE = convert(varchar(20), a." + conn.GetFieldValue("DOCDATE").ToString() + ",106)," +
                         "COMPANY = a." + conn.GetFieldValue("CUSTOMER_NAME").ToString() + "," +
                         "EMAIL   = a." + conn.GetFieldValue("CUSTOMER_EMAIL").ToString() + "," +
                         "REPORT  = a." + conn.GetFieldValue("REPORT_FIELD").ToString() + ", " +
                         "b.FIRST_SEND, " +
                         "b.LAST_SEND, " +
                         "b.RECIPIENT " +
                         "from " + conn.GetFieldValue("TABLENAME").ToString() + " a " +
                         "left join EMAIL_SENDED_LOG b on a." + conn.GetFieldValue("DOCNO").ToString() + "=b.DOCNO and datediff(day,a." + conn.GetFieldValue("DOCDATE").ToString() + ",b.DOCDATE)=0 and b.APP_ID='" + LB_APP.Text + "' and b.CODE='" + LB_CODE.Text + "' " +
                         "where b.APP_ID " + DDL_SENDED.SelectedValue + " " +
                         where +
                         "order by a." + conn.GetFieldValue("DOCDATE").ToString();

            try
            {
                conn.QueryString = SQL;
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREPORT = (LinkButton)DGR.Items[i].FindControl("LB_REPORT");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                lbREPORT.Text = DGR.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");
                txtEMAIL.Text = DGR.Items[i].Cells[4].Text.Trim().Replace("&nbsp;", "");

                lbREPORT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[6].Text + "','DOCUMENT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";
            if (e.CommandName == "Send")
            {
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                Send(e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[3].Text, txtEMAIL.Text.Trim());
                FillDGR();
            }
        }

        

        protected void Send(string docno, string docdate, string company, string recipient)
        {
            conn.QueryString = "exec SP_PARAM_EMAIL " +
                                "'" + LB_APP.Text + "'," +
                                LB_CODE.Text + "," +
                                "'" + docno + "'," +
                                "'" + docdate + "'";
            conn.ExecuteQuery();

            string sender = conn.GetFieldValue("DEFAULT_SENDER").ToString();
            string CC = conn.GetFieldValue("CC").ToString();
            string BCC = conn.GetFieldValue("BCC").ToString();
            string body = conn.GetFieldValue("BODY").ToString();
            string subject = conn.GetFieldValue("SUBJECT").ToString();


            /*
            conn.QueryString = "select " +
                                "a.DEFAULT_SENDER, a.BODY, a.DESCR, a.CC, a.BCC, " +
                                "from PARAM_EMAIL a " +
                                "where a.APP_ID='" + LB_APP.Text + "' and a.CODE=" + LB_CODE.Text;
            conn.ExecuteQuery();

            string sender = conn.GetFieldValue("DEFAULT_SENDER").ToString();
            string CC = conn.GetFieldValue("CC").ToString();
            string BCC = conn.GetFieldValue("BCC").ToString();
            string body = conn.GetFieldValue("BODY").ToString();
            string subject = conn.GetFieldValue("DESCR").ToString() + " - " + company + " (" + docdate + ")";

            string tablename = conn.GetFieldValue("TABLENAME").ToString();
            string docno_field = conn.GetFieldValue("DOCNO").ToString();
            string body_field = conn.GetFieldValue("BODY_FIELD").ToString();

            body = body.Replace("@SMALL_LOGO", GlobalUse.GetStringImageURL("select SMALL_LOGO from SC_COMPANY where CODE='1'", "SMALL_LOGO"));

            conn.QueryString = "select NAME, ADDR = ADDRESS1 + ' ' + RTRIM(isnull(ADDRESS2,'')) + ' ' + ZIPCODE + ', ' + COUNTRY + '<BR>Tel: ' + PHONE + ', Fax: ' + FAX + ', Email: ' +EMAIL from SC_COMPANY where CODE='1'";
            conn.ExecuteQuery();

            body = body.Replace("@THISCOMPANY", conn.GetFieldValue("NAME").ToString());
            body = body.Replace("@THISADDR", conn.GetFieldValue("ADDR").ToString());
            */
    
            conn.QueryString = "select a.REPORT_CODE, b.DESCR " +
                                "from PARAM_EMAIL_ATTACHMENT a " +
                                "inner join PR_EXPORT_FORMAT b on a.FORMAT=b.CODE " +
                                "where a.APP_ID='" + LB_APP.Text + "' and a.CODE=" + LB_CODE.Text;
            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable().Copy();
            string path = Request.PhysicalApplicationPath + "Upload";
            string[] attachment = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                attachment[i] = GlobalUse.RenderReport(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), path, docno, "", "", "", "", "", "", "", "", "");
            }

            string sendresult = GlobalUse.SendEmail(sender, recipient, CC, BCC, subject, body, attachment);
            if (sendresult.Trim() != "")
            {
                LB_ERROR.Text = sendresult;
            }
            else
            {
                conn.QueryString = "exec SP_EMAIL_SENDED_LOG_UPSERT " +
                                    "'" + LB_APP.Text + "'," +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + docno + "'," +
                                    "'" + docdate + "'," +
                                    "'" + sender + "'," +
                                    "'" + recipient + "'";
                conn.ExecuteNonQuery();
            }

            for (int i = 0; i < attachment.Count(); i++)
            {
                if (File.Exists(attachment[i]))
                    File.Delete(attachment[i]);
            }
        }
    }
}
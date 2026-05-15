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
    public partial class Invoice_SOA_HEALT : System.Web.UI.Page
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

                //LB_TIPE.Text = Request.QueryString["TIPE"];
                Setup();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";
            string where = "1=1";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_SENDDATE.Text.Trim() != "")
                where = where + " and a.FIRST_SEND is not null and convert(date,a.FIRST_SEND) >= '" + GlobalUse.GlobalDateFormat(TXT_SENDDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_SENDDATE2.Text.Trim() != "")
                where = where + " and a.FIRST_SEND is not null and convert(date,a.FIRST_SEND) <= '" + GlobalUse.GlobalDateFormat(TXT_SENDDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "SELECT " +
                                "a.CUSTOMER_CODE, " +
                                "a.COMPANY_NAME, " +
                                "a.BRANCH, " +
                                "a.TPA, " +
                                "a.OUTSTANDING_AMOUNT, " +
                                "TGL_SEND = a.FIRST_SEND, " +
                                "a.LAST_SEND, " +
                                "COMPANY_EMAIL = a.RECIPIENTS, " +
                                "a.SEQ, " +
                                //"REPORT_URL = CONCAT(ac.URLAPP COLLATE SQL_Latin1_General_CP1_CI_AS,'&CUSTOMER_CODE=',a.CUSTOMER_CODE COLLATE SQL_Latin1_General_CP1_CI_AS,'&SEQ=',a.SEQ) " +
                                "REPORT_URL = CONCAT(ac.URLAPP COLLATE SQL_Latin1_General_CP1_CI_AS,'&CUSTOMER_CODE=',a.CUSTOMER_CODE COLLATE SQL_Latin1_General_CP1_CI_AS) " +
                                "FROM FINANCE.dbo.SEND_EMAIL_HEALT_SOA a " +
                                "LEFT JOIN FINANCE.dbo.V_LINK_SC_REPORT_LIST ac ON ac.CODE = '404' " +
                                "ORDER BY FIRST_SEND DESC ";
            //conn.QueryString = "select " +
            //                    "a.DOCNO, " +
            //                    "a.POLICY_ID, " +
            //                    "TGL_INV = convert(varchar(20),a.FIRST_SEND), " +
            //                    "a.COMPANY_NAME, " +
            //                    "a.PARTICIPANTS, " +
            //                    "a.TYPE, " +
            //                    "a.COMPANY_EMAIL, " +
            //                    "TGL_SEND = a.FIRST_SEND, " +
            //                    "REPORT_URL   = ac.URLAPP COLLATE Latin1_General_CI_AS + '&DOCNO=' + a.DOCNO COLLATE Latin1_General_CI_AS " +
            //                    "from GLIFE.dbo.SEND_EMAIL_GTLR_DOCUMENT_LETTER a " +
            //                    "left join  GLIFE.dbo.V_LINK_SC_REPORT_LIST ac on ac.CODE = '71' " +
            //                    "where " + where +
            //                    "order by FIRST_SEND desc";
            
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
                txtEMAIL.Text = DGR.Items[i].Cells[6].Text.Trim().Replace("&nbsp;", "");
                lbID.Text = DGR.Items[i].Cells[0].Text.Trim().Replace("&nbsp;", "");

                lbID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','RECEIPT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void Setup()
        {
           
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
                    LB_ERROR.Text =  LB_ERROR.Text + "<BR>" + ex.Message;
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
            String SQL = "exec GLIFE..SP_JOB_SEND_EMAIL_GTLR " +
                                    "'" + e.Item.Cells[10].Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + txtEMAIL.Text.Trim() + "'";
            //String SQL = "exec SP_PARAM_EMAIL " +
            //                    "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
            //                    //e.Item.Cells[0].Text + "," +
            //                     "'" + 5 + "'," +
            //                    "'" + e.Item.Cells[0].Text + "'," +
            //                    "'" + e.Item.Cells[3].Text + "'";
            LB_ERROR.Text = SQL;
            conn.QueryString = SQL;
            conn.ExecuteQuery(150000);

            //string sender = connsec.GetFieldValue("DEFAULT_SENDER").ToString();
            //string CC = connsec.GetFieldValue("CC").ToString();
            //string BCC = connsec.GetFieldValue("BCC").ToString();
            //string body = connsec.GetFieldValue("BODY").ToString();
            //string subject = "INVOICE " + e.Item.Cells[8].Text + " - " + e.Item.Cells[7].Text + " (" + e.Item.Cells[0].Text + ")";

            //if (LB_TIPE.Text == "R")
            //    subject = "REMINDER " + subject;

            //string path = Request.PhysicalApplicationPath + "Upload";
            //string[] attachment = new string[1];

            ////attachment[0] = GlobalUse.RenderReport(e.Item.Cells[14].Text, "PDF", path, e.Item.Cells[0].Text, "", "", "", "", "", "", "", "", "");

            //string sendresult = GlobalUse.SendEmail(sender, txtEMAIL.Text.Trim(), CC, BCC, subject, body, attachment);
            //if (sendresult.Trim() != "")
            //{
            //    LB_ERROR.Text = sendresult;
            //}
            //else
            //{
            //    connsec.QueryString = "exec SP_EMAIL_SENDED_LOG_UPSERT " +
            //                        "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
            //                        "'" + e.Item.Cells[13].Text + "'," +
            //                        "'" + e.Item.Cells[0].Text + "'," +
            //                        "'" + e.Item.Cells[3].Text + "'," +
            //                        "'" + sender + "'," +
            //                        "'" + txtEMAIL.Text.Trim() + "'";
            //    connsec.ExecuteNonQuery();
            //}

            //for (int i = 0; i < attachment.Count(); i++)
            //{
            //    if (File.Exists(attachment[i]))
            //        File.Delete(attachment[i]);
            //}
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

        //protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillDDLInvoice();
        //}
    }
}
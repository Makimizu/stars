using DMS.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FINANCE.Form_Collection
{
    public partial class CancellationEmail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]).Replace("STARSBA", "192.168.100.92"));
        protected Connection connsec = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                BindInvoiceType();
                FillDGR();
            }
        }

        private void BindData()
        {
            string dateFormat = DateTime.Now.ToString("dd/MM/yyyy");
            //TXT_INVOICE_DATE_START.Text = dateFormat;
            //TXT_INVOICE_DATE_END.Text = dateFormat;
            TXT_CANCEL_DATE_START.Text = dateFormat;
            TXT_CANCEL_DATE_END.Text = dateFormat;
            //TXT_SEND_DATE_START.Text = dateFormat;
            //TXT_SEND_DATE_END.Text = dateFormat;

        }

        private void BindInvoiceType()
        {
            var _sql = @"select INVOICE_TYPE, DESCR from FINANCE.dbo.PARAM_INVOICE_TYPE where app_id = 'GL'";

            conn.QueryString = _sql;

            conn.ExecuteQuery();

            DDL_INVOICE_TYPE.Items.Clear();
            DDL_INVOICE_TYPE.DataTextField = "DESCR";
            DDL_INVOICE_TYPE.DataValueField = "INVOICE_TYPE";
            DDL_INVOICE_TYPE.DataSource = conn.GetDataTable();
            DDL_INVOICE_TYPE.DataBind();

            DDL_INVOICE_TYPE.Items.Add(new ListItem("SELECT", ""));
            DDL_INVOICE_TYPE.SelectedValue = "";

        }

        protected void FillDGR()
        {
            try
            {
                string _dateFormat = "yyyy-MM-dd";
                string _dateValid = "dd/MM/yyyy";
                string value = "26/11/2021";

                LB_RESULT.Text = "";
                string where = " ";

                if (DDL_STATUS_EMAIL.SelectedValue.Trim() == "1")
                    where = where + " and o.code is not null";

                if (DDL_STATUS_EMAIL.SelectedValue.Trim() == "2")
                    where = where + " and o.code is null";

                if (!string.IsNullOrEmpty(TXT_COMPANY.Text.Trim()))
                    where = where + " and h.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_FULLNAME.Text.Trim()))
                    where = where + " and mb.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_INVOICE_NO.Text.Trim()))
                    where = where + " and j.INVOICENO like '%" + TXT_INVOICE_NO.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_REGNO.Text.Trim()))
                    where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_FULLNAME.Text.Trim()))
                    where = where + " and mb.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_POLICYNO.Text.Trim()))
                    where = where + " and g.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(TXT_COMPANY.Text.Trim()))
                    where = where + " and h.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

                if (!string.IsNullOrEmpty(DDL_INVOICE_TYPE.SelectedValue.Trim()))
                    where = where + " and k.INVOICE_TYPE = '" + DDL_INVOICE_TYPE.SelectedValue.Trim() + "' ";

                if (!string.IsNullOrEmpty(TXT_INVOICE_DATE_START.Text.Trim()))
                    where = where + " and convert(date, k.INVOICE_DATE) >= '" + DateTime.ParseExact(TXT_INVOICE_DATE_START.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_INVOICE_DATE_END.Text.Trim()))
                    where = where + " and convert(date, k.INVOICE_DATE) <= '" + DateTime.ParseExact(TXT_INVOICE_DATE_END.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_CANCEL_DATE_START.Text.Trim()))
                    where = where + " and convert(date, b.USER_ENDDATE) >= '" + DateTime.ParseExact(TXT_CANCEL_DATE_START.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_CANCEL_DATE_END.Text.Trim()))
                    where = where + " and convert(date, b.USER_ENDDATE) <= '" + DateTime.ParseExact(TXT_CANCEL_DATE_END.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_SEND_DATE_START.Text.Trim()))
                    where = where + " and convert(date, o.FIRST_SEND) >= '" + DateTime.ParseExact(TXT_SEND_DATE_START.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_SEND_DATE_END.Text.Trim()))
                    where = where + " and convert(date, o.LAST_SEND) <= '" + DateTime.ParseExact(TXT_SEND_DATE_END.Text.Trim(), _dateValid, CultureInfo.InvariantCulture).ToString(_dateFormat) + "' ";

                if (!string.IsNullOrEmpty(TXT_AGENT_NAME.Text.Trim()))
                {
                    string _whereAgent = string.Format("and (l.FRONT_NAME like '%{0}%' or l.MID_NAME like '%{0}%' or l.LAST_NAME like '%{0}%' or m.FRONT_NAME like '%{0}%' or m.MID_NAME like '%{0}%' or m.LAST_NAME like '%{0}%')", TXT_AGENT_NAME.Text.Trim());
                    where = where + _whereAgent;
                }

                if (!string.IsNullOrEmpty(TXT_BRANCH_NAME.Text.Trim()))
                    where = where + " and n.NAMA_CABANG like '%" + TXT_BRANCH_NAME.Text.Trim() + "%' ";

                string sql = string.Format(@"select 
	                REGNO			= a.REGNO,
	                INVOICENO		= j.INVOICENO,
	                INVOICE_DATE	= k.INVOICE_DATE,
	                INVOICE_TYPE	= k.INVOICE_TYPE_DESCR,
	                AGING			= DATEDIFF(day, k.INVOICE_DATE, b.USER_ENDDATE),
	                FULLNAME		= mb.FULLNAME,   
	                POLICY_NO		= g.POLICY_NO,   
	                COMPANY_NAME	= h.COMPANY_NAME,   
	                UW_CODE			= UW_CODE,   
	                PREMIUM			= (case	when isnull(f.VAL, 0) = 0 then replace(convert(varchar(100), convert(money,e.PREMIUM),1), '.00','') 
							                else (case when isnull(f.VAL, 0) > e.PREMIUM then replace(convert(varchar(100), convert(money,f.VAL),1), '.00','') else replace(convert(varchar(100), convert(money,e.PREMIUM),1), '.00','') end) 
						                end),  
	                SUMINS			= replace(convert(varchar(100), convert(money, d.SUMINS),1), '.00',''),   
					OUTSTANDING		= jj.AMOUNT,
	                REASON			= '<U>' + bb.DESCR collate database_default + ' by ' + b.USER_ENDBY + ' (' + convert(varchar(100),b.USER_ENDDATE) + '):</U><BR><I>' + isnull(b.COMMENT collate database_default,'') + '</I>',  
	                LAST_TRACK		= b.SEQ,
	                LAST_TRACK_DESCR = pt.DESCR,
	                CANCEL_DATE		= b.USER_ENDDATE,
	                AGENT_CODE		= a.USERBY,  
	                AGENT_NAME		= (	case	when l.CODE is not null then LTRIM(RTRIM(replace(isnull(l.FRONT_NAME,'') + ' ' + isnull(l.MID_NAME,'') + ' ' + isnull(l.LAST_NAME,''),'  ',' ')))  
						                else LTRIM(RTRIM(replace(isnull(m.FRONT_NAME,'') + ' ' + isnull(m.MID_NAME,'') + ' ' + isnull(m.LAST_NAME,''),'  ',' ')))  
					                    end),  
	                BRANCH_NAME		= n.NAMA_CABANG,
	                a.BRANCH_CODE,
		            DOCDATE = b.USER_ENDDATE,
		            CUSTOMER_NAME = mb.FULLNAME,
		            CUSTOMER_PIC = h.COMPANY_NAME,
		            o.FIRST_SEND,
		            o.LAST_SEND,
                    SENDED = case when o.code is not null then 'SENDED' else 'NOT SENDED' end,
                    SEND_DATE = isnull(o.LAST_SEND, o.FIRST_SEND),
		            REPORT_CODE = 346,
		            REPORT_URL = q.URL + '&REGNO=' + a.REGNO collate database_default,
		            p.DOCNO,
                    RECIPIENT = case when isnull(o.RECIPIENT, '') != '' then o.RECIPIENT collate database_default --sended
					    when isnull(l.EMAIL, '') != '' then l.EMAIL collate database_default --agent
					    when isnull(mb.EMAIL, '') != '' then mb.EMAIL collate database_default --customer
					    when isnull(m.EMAIL, '') != '' then m.EMAIL collate database_default --master user
				    end,
                    EMAIL_CC = 'bancassurance@takaful.com, claim-atk@takaful.com, creditcontrol-atk@takaful.com'
                from GLIFE.dbo.TRACK_DATA b  
                inner join GLIFE.dbo.PARAM_TRACK bb on b.TIPE_CODE = bb.TIPE_CODE and b.SEQ = bb.SEQ
                inner join GLIFE.dbo.APPLICATION_MASTER a on a.REGNO=b.OWNER collate database_default
                inner join GLIFE.dbo.APPLICATION_MAIN_INFO d on a.REGNO = d.REGNO
                inner join GLIFE.dbo.POLICY g on a.POLICY_ID = g.ID
                inner join GLIFE.dbo.V_LINK_CB_COMPANY h on g.COMPANY_CODE = h.COMPANY_CODE
                left join GLIFE.dbo.V_LINK_UB_TC_MASTER i on a.TC_ID = i.CODE
                left join GLIFE.dbo.V_LINK_CB_MEMBER_MASTER mb on a.MEMBER_ID = mb.ID
                inner join (select REGNO, RATE = SUM(isnull(RATE,0)), PREMIUM = SUM(SUMINS * isnull(RATE,0))/1000 from GLIFE.dbo.APPLICATION_BENEFIT group by REGNO) e on a.REGNO = e.REGNO   
                left join GLIFE.dbo.V_LINK_UB_TC_ITEMS f on f.TC_ITEM = '43' and ISNUMERIC(replace(f.VAL, ',','')) = 1 and a.TC_ID = f.TC_CODE
                left join FINANCE.dbo.INVOICE_DETAIL j on (case when j.DOC_NO like '%-RST' then LEFT(j.DOC_NO, len(j.DOC_NO) - 4) else j.DOC_NO end) = d.REGNO
                inner join ( select * from FINANCE.dbo.INVOICE_NOTA x where x.TIPE_NOTA = '999' and x.TIPE_REASON = '1') jj on j.INVOICENO = jj.INVOICENO and jj.NOTA_NO like j.INVOICENO collate database_default + '-' + j.DOC_NO collate database_default + '-%'
                left join FINANCE.dbo.V_INVOICE_MASTER k on isnull(j.INVOICENO, '') = k.INVOICENO
                left join GLIFE.dbo.V_LINK_MARKETING_M_AGENTS l on a.USERBY = l.CODE  
                left join GLIFE.dbo.V_LINK_SC_M_USERS m on a.USERBY = m.CODE  
                left join GLIFE.dbo.V_LINK_CB_BRANCH n on a.BRANCH_CODE = n.BRANCH_CODE
                left join GLIFE.dbo.PARAM_TRACK pt on pt.TIPE_CODE = 'UW' and pt.SEQ = b.SEQ 
                left join SECURITY.dbo.EMAIL_SENDED_LOG o on a.regno collate database_default = o.docno collate database_default and o.code = '346' and o.APP_ID in ('GL')
	            left join GLIFE.dbo.APPLICATION_DOCUMENT_LETTER p on a.REGNO = p.REGNO
                left join SECURITY.dbo.REPORT_LIST  q on q.APP_ID = 'GL' and q.CODE = 346
                where b.TIPE_CODE = 'UW' and b.SEQ =  '5' and p.CODE = '040' {0}", where);

                conn.QueryString = string.Format(sql, where);
                conn.ExecuteQuery(50000);

                LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
                int MaxCount = DGR.PageSize;

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();
            }
            catch (Exception ex)
            {

                LB_ERROR.Text = ex.Message.ToString();
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }
            if (e.CommandName == "Send")
            {

                try
                {
                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                        if (cb.Checked)
                        {

                            TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                            if (txtEMAIL.Text.Trim() == "")
                                throw new Exception("Email tidak boleh kosong!");

                            //TextBox emailCC = (TextBox)e.Item.FindControl("TXT_EMAIL_CC");
                            TextBox emailCC = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL_CC");
                            string lblRegNo = DGR.Items[i].Cells[22].Text.Trim();
                            string reportCode = DGR.Items[i].Cells[19].Text.Trim();
                            string companyName = DGR.Items[i].Cells[3].Text.Trim();
                            string policyNo = DGR.Items[i].Cells[5].Text.Trim();
                            string docDate = DGR.Items[i].Cells[8].Text.Trim();

                            SendEmail(txtEMAIL.Text.Trim(), emailCC.Text.Trim(), lblRegNo, reportCode, companyName, policyNo, docDate);

                        }
                    }
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

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void SendEmail(string txtEMAIL, string emailCC, string lblRegNo, string reportCode, string companyName, string policyNo, string docDate)
        {
            try
            {

                string _sql = string.Format("exec SP_PARAM_EMAIL '{0}', '{1}', '{2}', '{3}'",
                    //System.Configuration.ConfigurationManager.AppSettings["appid"].ToString(),
                    "GL",
                    reportCode, //code report
                    lblRegNo, //regno
                    null //docdate
                );

                connsec.QueryString = _sql;
                connsec.ExecuteQuery();

                string sender = connsec.GetFieldValue("DEFAULT_SENDER").ToString();
                string CC = connsec.GetFieldValue("CC").ToString();
                string BCC = connsec.GetFieldValue("BCC").ToString();
                string body = connsec.GetFieldValue("BODY").ToString();
                string subject = string.Format("CANCELLATION LETTER - {0} ({1} - {2})", companyName, policyNo, lblRegNo);

                string path = Request.PhysicalApplicationPath + "Upload";
                string[] attachment = new string[1];

                attachment[0] = GlobalUse.RenderReportByAppID("GL", reportCode, "PDF", path, lblRegNo.Trim(), "", "", "", "", "", "", "", "", "");

                string sendresult = GlobalUse.SendEmail(sender, txtEMAIL, emailCC, BCC, subject, body, attachment);
                if (sendresult.Trim() != "")
                {
                    LB_ERROR.Text = sendresult;
                }
                else
                {
                    _sql = string.Format("exec SP_EMAIL_SENDED_LOG_UPSERT '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                        //System.Configuration.ConfigurationManager.AppSettings["appid"], //appid
                                        "GL",
                                        reportCode, //code report
                                        lblRegNo, //docno/regno
                                        docDate, //docdate
                                        sender, //email sender
                                        txtEMAIL); //recipient
                    connsec.QueryString = _sql;
                    connsec.ExecuteNonQuery();
                }

                for (int i = 0; i < attachment.Count(); i++)
                {
                    if (File.Exists(attachment[i]))
                        File.Delete(attachment[i]);
                }
            }
            catch (Exception ex)
            {
                LB_ERROR.Text = ex.Message.ToString();
            }

        }

        protected void BTN_EXPORT_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }


    }
}
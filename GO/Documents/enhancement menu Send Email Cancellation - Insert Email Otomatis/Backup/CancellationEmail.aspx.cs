    using DMS.DBConnection;
    using System;
    using System.Collections.Generic;
    using System.Data;
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
                string dateFormat = DateTime.Now.ToString("yyyy-MM-dd");
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
                        where = where + " and convert(date, k.INVOICE_DATE) >= '" + TXT_INVOICE_DATE_START.Text.Trim() + "' ";

                    if (!string.IsNullOrEmpty(TXT_INVOICE_DATE_END.Text.Trim()))
                        where = where + " and convert(date, k.INVOICE_DATE) <= '" + TXT_INVOICE_DATE_END.Text.Trim() + "' ";

                    if (!string.IsNullOrEmpty(TXT_CANCEL_DATE_START.Text.Trim()))
                        where = where + " and convert(date, b.USER_ENDDATE) >= '" + TXT_CANCEL_DATE_START.Text.Trim() + "' ";

                    if (!string.IsNullOrEmpty(TXT_CANCEL_DATE_END.Text.Trim()))
                        where = where + " and convert(date, b.USER_ENDDATE) <= '" + TXT_CANCEL_DATE_END.Text.Trim() + "' ";

                    if (!string.IsNullOrEmpty(TXT_SEND_DATE_START.Text.Trim()))
                        where = where + " and convert(date, o.FIRST_SEND) >= '" + TXT_SEND_DATE_START.Text.Trim() + "' ";

                    if (!string.IsNullOrEmpty(TXT_SEND_DATE_END.Text.Trim()))
                        where = where + " and convert(date, o.LAST_SEND) <= '" + TXT_SEND_DATE_END.Text.Trim() + "' ";

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
                        RECIPIENT = isnull(o.RECIPIENT, isnull(mb.EMAIL, ''))
                    from  (select * from GLIFE.dbo.APPLICATION_DOCUMENT_LETTER with (nolock) where code = '040') p  
				    inner join (select * from GLIFE.dbo.TRACK_DATA with (nolock) where seq = '5' and tipe_code = 'UW') b on p.regno = b.owner  collate database_default
                    inner join (select * from GLIFE.dbo.PARAM_TRACK with (nolock) where SEQ =  '5' ) bb on b.TIPE_CODE = bb.TIPE_CODE and b.SEQ = bb.SEQ
                    inner join GLIFE.dbo.APPLICATION_MASTER a with (nolock) on a.REGNO=b.OWNER collate database_default
                    inner join GLIFE.dbo.APPLICATION_MAIN_INFO d with (nolock) on a.REGNO = d.REGNO
                    inner join GLIFE.dbo.POLICY g with (nolock) on a.POLICY_ID = g.ID
                    inner join GLIFE.dbo.V_LINK_CB_COMPANY h with (nolock) on g.COMPANY_CODE = h.COMPANY_CODE
                    left join GLIFE.dbo.V_LINK_UB_TC_MASTER i with (nolock) on a.TC_ID = i.CODE
                    left join GLIFE.dbo.V_LINK_CB_MEMBER_MASTER mb with (nolock) on a.MEMBER_ID = mb.ID
                    inner join (select REGNO, RATE = SUM(isnull(RATE,0)), PREMIUM = SUM(SUMINS * isnull(RATE,0))/1000 from GLIFE.dbo.APPLICATION_BENEFIT with (nolock) group by REGNO) e on a.REGNO = e.REGNO   
                    left join GLIFE.dbo.V_LINK_UB_TC_ITEMS f with (nolock) on f.TC_ITEM = '43' and ISNUMERIC(replace(f.VAL, ',','')) = 1 and a.TC_ID = f.TC_CODE
                    left join FINANCE.dbo.INVOICE_DETAIL j with (nolock) on (case when j.DOC_NO like '%-RST' then LEFT(j.DOC_NO, len(j.DOC_NO) - 4) else j.DOC_NO end) = d.REGNO
                    inner join ( select * from FINANCE.dbo.INVOICE_NOTA x with (nolock) where x.TIPE_NOTA = '999' and x.TIPE_REASON = '1') jj on j.INVOICENO = jj.INVOICENO and jj.NOTA_NO like j.INVOICENO collate database_default + '-' + j.DOC_NO collate database_default + '-%'
                    left join FINANCE.dbo.V_INVOICE_MASTER k with (nolock) on isnull(j.INVOICENO, '') = k.INVOICENO
                    left join GLIFE.dbo.V_LINK_MARKETING_M_AGENTS l with (nolock) on a.USERBY = l.CODE  
                    left join GLIFE.dbo.V_LINK_SC_M_USERS m with (nolock) on a.USERBY = m.CODE  
                    left join GLIFE.dbo.V_LINK_CB_BRANCH n with (nolock) on a.BRANCH_CODE = n.BRANCH_CODE
                    left join GLIFE.dbo.PARAM_TRACK pt with (nolock) on pt.TIPE_CODE = 'UW' and pt.SEQ = b.SEQ 
                    left join SECURITY.dbo.EMAIL_SENDED_LOG o with (nolock) on a.regno collate database_default = o.docno collate database_default and o.code = '346' and o.APP_ID in ('GL')
	                --left join GLIFE.dbo.APPLICATION_DOCUMENT_LETTER p with (nolock) on a.REGNO = p.REGNO
                    left join SECURITY.dbo.REPORT_LIST  q with (nolock) on q.APP_ID = 'GL' and q.CODE = 346
                    where p.CODE = '040' {0}", where);

                    conn.QueryString = string.Format(sql, where);
                    conn.ExecuteQuery(50000);

                    LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
                    int MaxCount = DGR.PageSize;
                    if (conn.GetRowCount() <= MaxCount)
                        DGR.AllowPaging = false;

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

                                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                                if (txtEMAIL.Text.Trim() == "")
                                    throw new Exception("Email tidak boleh kosong!");

                                SendEmail(e);

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

            protected void SendEmail(DataGridCommandEventArgs e)
            {
                try
                {
                    TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                    TextBox emailCC = (TextBox)e.Item.FindControl("TXT_EMAIL_CC");
                    string lblRegNo = e.Item.Cells[22].Text.Trim();
                    string reportCode = e.Item.Cells[19].Text.Trim();

                    if (txtEMAIL.Text.Trim() == "")
                        throw new Exception("Email tujuan tidak boleh kosong!");

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
                    string subject = string.Format("CANCELLATION LETTER - {0} ({1} - {2})", e.Item.Cells[3].Text.Trim(), e.Item.Cells[5].Text.Trim(), lblRegNo);

                    string path = Request.PhysicalApplicationPath + "Upload";
                    string[] attachment = new string[1];

                    attachment[0] = GlobalUse.RenderReportByAppID("GL",reportCode, "PDF", path, lblRegNo.Trim(), "", "", "", "", "", "", "", "", "");

                    string sendresult = GlobalUse.SendEmail(sender, txtEMAIL.Text.Trim(), emailCC.Text.Trim(), BCC, subject, body, attachment);
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
                                            e.Item.Cells[8].Text.Trim(), //docdate
                                            sender, //email sender
                                            txtEMAIL.Text.Trim()); //recipient
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
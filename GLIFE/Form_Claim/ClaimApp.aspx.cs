using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization; 

namespace GLIFE.Form_Claim
{
    public partial class ClaimApp : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        private string username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                ShowBenefit();
                ShowTrack();
                string warning = ShowBlacklist();
                if (!string.IsNullOrEmpty(warning))
                {
                    TR_BLACKLIST.Visible = true;
                    TEXT_BLACKLIST.Text = "<p>" + warning + "</p>";
                    TEXT_BLACKLIST.ForeColor = System.Drawing.Color.Red;
                    TBL_BLACKLIST.Style.Add("border", "2px solid red");
                }
                username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());
            }
        }

        protected void FillDDLType()
        {
            string equation = "=";
            if (DDL_SEQ.Items.Count > 0 && DDL_SEQ.SelectedValue != "1")
                equation = "<>";

            DDL_TYPE.Items.Clear();

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_TYPE where CODE " + equation + " '001' order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                LB_READONLY.Text = "&readonly=1";
                BT_SAVE.Visible = false;
                //BT_SAVE.Visible = true;

                DDL_LOCATION.Enabled = false;
                DDL_TYPE.Enabled = false;
                TXT_CLAIMDATE.Enabled = false;
                TXT_OCCUREDDATE.Enabled = false;
                TXT_RECEIVEDATE.Enabled = false;
            }

            if (Request.QueryString["SEQ"].ToString() != "")
            {
                conn.QueryString = "select SEQ from APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' order by 1";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_SEQ.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                try
                {
                    DDL_SEQ.SelectedValue = Request.QueryString["SEQ"].ToString();
                }
                catch { }
            }
            else
            {
                DDL_SEQ.Visible = false;
            }

            FillDDLType();

            LB_TITLE.Text = BT1.Text;

            conn.QueryString = "select " +
                                "REGNO				= a.REGNO,  " +
                                "FULLNAME			= d.FULLNAME,  " +
                                "DOB				= convert(varchar(20),d.DOB,106),  " +
                                "SEX				= (case when SEX='M' then 'MALE' else 'FEMALE' end),  " +
                                "POLICY_NO			= b.POLICY_NO,  " +
                                "COMPANY_NAME		= c.COMPANY_NAME,  " +
                                "TC_ID				= a.TC_ID,  " +
                                "TC_DESCR			= LTRIM(replace(replace(e.DESCR, a.TC_ID, ''), '-', '')),  " +
                                "BRANCH_CODE		= a.BRANCH_CODE,  " +
                                "AGENT_NAME			= LTRIM(RTRIM(replace(isnull(i.FRONT_NAME,'') + ' ' + isnull(i.MID_NAME,'') + ' ' + isnull(i.LAST_NAME,''),'  ',' '))) , " +
                                "SUBCD_DESCR		= SUBCD_DESCR, " +
                                "PERIOD             = convert(varchar(20), f.START_DATE,106) + ' - ' + convert(varchar(20), f.END_DATE,106), " +
                                "SUMINS             = replace(convert(varchar(100), convert(money, f.SUMINS),1), '.00',''), " +
                                "START_AGE          = f.START_AGE, " +
                                "UW_CODE            = (case when UW_CODE='AC' then 'AUTOMATIC COVER' when UW_CODE='NM' then 'NON MEDICAL' else 'MEDICAL - '+UW_CODE end) " +
                                "from APPLICATION_MASTER a " +
                                "inner join POLICY b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_CB_COMPANY c on b.COMPANY_CODE = c.COMPANY_CODE " +
                                "inner join V_LINK_CB_MEMBER_MASTER d on a.MEMBER_ID = d.ID " +
                                "inner join V_LINK_UB_TC_MASTER e on a.TC_ID = e.CODE " +
                                "inner join APPLICATION_MAIN_INFO f on a.REGNO = f.REGNO " +
                                "left join V_LINK_MARKETING_M_AGENTS i on a.USERBY = i.CODE " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_GENDER.Text = conn.GetFieldValue("SEX").ToString();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_TC_ID.Text = conn.GetFieldValue("TC_ID").ToString();
            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_CODE").ToString();

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_LOCATION";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOCATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_REASON_UNAPPROVE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_UNAPPROVE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select TODAY = convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_CLAIMDATE.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_OCCUREDDATE.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_RECEIVEDATE.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_COMPLETEDATE.Text = conn.GetFieldValue("TODAY").ToString();

            conn.QueryString = "select  " +
                               "BLACKLIST = case when b.FULLNAME is not null then  " +
                               "                         case when lower(b.FLAG) = 'black' then '<b>Warning: Main Insured ('+ a.FULLNAME +')<br>is a '+ lower(b.FLAG) +'list participant with remark<br>' + b.REMARK + '</b>' " +
                               "                         else '<b>Warning: Main Insured ('+ a.FULLNAME +')<br>is a '+ lower(b.FLAG) +'list participant with remark<br>' + b.REMARK + '</b>' end " +
                               "                 else '' " +
                               "            end, " +
                               "BLCOL = case when b.FULLNAME is not null then  " +
                               "                         case when lower(b.FLAG) = 'black' then 'Red' " +
                               "                         else 'Yellow' end " +
                               "                 else 'Green' " +
                               "            end " +
                               "from GLIFE.dbo.V_APPLICATION_MASTER a " +
                               "left join CLIENT_BASE.dbo.MEMBER_MASTER_BLACKLIST b on a.FULLNAME = b.FULLNAME and a.DOB = b.DOB " +
                               "where a.regno = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_BLACKLIST.Text = conn.GetFieldValue("BLACKLIST").ToString();
            LB_BLACKLIST.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("BLCOL").ToString());

        }

        protected void LoadRecord()
        {
            LB_REGNO.Visible = true;
            TR_TYPE.Visible = true;

            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER " +
                                "'" + LB_REGNO.Text + "'," + DDL_SEQ.SelectedValue;
            conn.ExecuteQuery();

            TXT_CLAIMDATE.Text = conn.GetFieldValue("CLAIM_DATE").ToString();
            TXT_OCCUREDDATE.Text = conn.GetFieldValue("OCCURED_DATE").ToString();
            TXT_RECEIVEDATE.Text = conn.GetFieldValue("RECEIVED_DATE").ToString();
            TXT_COMPLETEDATE.Text = conn.GetFieldValue("COMPLETE_DATE").ToString();
            LB_LOM.Text = conn.GetFieldValue("LOM").ToString();

            try
            {
                DDL_LOCATION.SelectedValue = conn.GetFieldValue("LOCATION").ToString();
            }
            catch { }

            try
            {
                DDL_TYPE.SelectedValue = conn.GetFieldValue("CLAIM_TYPE").ToString();
            }
            catch { }

            TR_MEMBERSHIP.Visible = true;
        }
         
        protected void ShowTrack()
        {
            if (DDL_SEQ.SelectedValue == "")
                return;

            LoadRecord();

            TR_BUTTONS.Visible = true;
            DGR_TRACK.Visible = false;
            TBL_STAT.Visible = false;

            string seq = DDL_SEQ.SelectedValue;
            conn.QueryString = "select " +
                                "c.SEQ, " +
                                "DESCR = replace(c.DESCR,'ED',''), " +
                                "COMMENT = convert(int,c.IsCOMMENT) " +
                                "from V_APPLICATION_CLAIM_MASTER a " +
                                "inner join PARAM_TRACK_NEXT b on a.LAST_TRACK = b.SEQ and b.TIPE_CODE = 'CLM' " +
                                "inner join PARAM_TRACK c on b.NEXT_SEQ = c.SEQ and b.TIPE_CODE = c.TIPE_CODE " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = " + seq + " " +
                                "order by convert(int,c.IsCOMMENT), c.SEQ";
            conn.ExecuteQuery();
             
            if (conn.GetRowCount() == 0)
            {
                BT_PENDING.Visible = false;
                ShowStat();
                return;
            }

            BT_PENDING.Visible = true;
            DGR_TRACK.Visible = true;

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR_TRACK.Visible = false;
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRACK.DataSource = dt;
            DGR_TRACK.DataBind();

            for (int i = 0; i < DGR_TRACK.Items.Count; i++)
            {
                Button btTRACK = (Button)DGR_TRACK.Items[i].FindControl("BT_TRACK");
                TextBox txtCOMMENT = (TextBox)DGR_TRACK.Items[i].FindControl("TXT_COMMENT");

                btTRACK.Text = DGR_TRACK.Items[i].Cells[1].Text;
                btTRACK.Attributes.Add("onclick", "if(!confirm('Are you sure to " + DGR_TRACK.Items[i].Cells[1].Text + " ?')){return false;};");
                if (DGR_TRACK.Items[i].Cells[2].Text == "1")
                {
                    txtCOMMENT.Visible = true;
                }
            }
        }

        protected void ShowStat()
        {
            TBL_STAT.Visible = true;

            conn.QueryString = "select " +
                                "STAT	= a.LAST_TRACK_DESCR, " +
                                "COMMENT	= b.COMMENT, " +
                                "COLOR	= (case when a.LAST_TRACK = 4 then 'Blue' else 'Red' end) " +
                                "from V_APPLICATION_CLAIM_MASTER a  " +
                                "inner join TRACK_DATA b on b.TIPE_CODE='CLM' and a.REGNO + '-' + convert(varchar(10), a.SEQ) = b.OWNER collate database_default and a.LAST_TRACK = b.SEQ " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = " + DDL_SEQ.SelectedValue;
            conn.ExecuteQuery();

            LB_STAT.Text = conn.GetFieldValue("STAT").ToString();
            LB_STATCOMMENT.Text = conn.GetFieldValue("COMMENT").ToString();

            LB_STAT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());
            LB_STATCOMMENT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());

            Disable();

            conn.QueryString = "select  a.REGNO " +
                                    "from V_APPLICATION_CLAIM_MASTER a " +
                                    "inner join V_APPLICATION_CLAIM_AMOUNT_SUMM b on a.REGNO = b.REGNO and a.SEQ = b.SEQ " +
                                    "inner join (select REPLACE(DOCNO,right(DOCNO,2),'') collate database_default REGNO,a.USERDATE " +
                                                "from FINANCE.dbo.SETTLEMENT_DETAIL a " +
                                                "join FINANCE.dbo.SETTLEMENT_MASTER b on b.REKAPID=a.REKAPID " +
                                                "where a.APP_ID='GL' and a.TIPE_SETTLEMENT='1' and b.APPROVALBY is null) c on c.REGNO = a.REGNO  " +
                                    "where a.REGNO in(select REPLACE(DOCNO,right(DOCNO,2),'') collate database_default " +
                                                        "from FINANCE.dbo.SETTLEMENT_DETAIL a " +
                                                        "join FINANCE.dbo.SETTLEMENT_MASTER b on b.REKAPID=a.REKAPID " +
                                                        "where a.APP_ID='GL' and b.APPROVALBY is null) " +
                                    "and a.REGNO = '" + LB_REGNO.Text + "' " +
                                    "and a.SEQ = " + DDL_SEQ.SelectedValue;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                BTN_UNAPPROVE.Attributes.Add("onclick", "if(!confirm('Are you sure to Unapprove ?')){return false;};");
                BTN_UNAPPROVE.Visible = true;
                DDL_UNAPPROVE.Visible = true;
                UNAPPROVE_REMARK.Visible = true;
            }

        }

        protected void Disable()
        {
            BT_SAVE.Visible = false;
            TXT_CLAIMDATE.ReadOnly = true;
            TXT_OCCUREDDATE.ReadOnly = true;
            TXT_RECEIVEDATE.ReadOnly = true;
            DDL_LOCATION.Enabled = false;
        }

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Next")
            {
                TextBox txtCOMMENT = (TextBox)e.Item.FindControl("TXT_COMMENT");
                if (txtCOMMENT.Visible && txtCOMMENT.Text.Trim() == "")
                {
                    txtCOMMENT.Focus();
                    return;
                }

                if (VerificationBlacklist())
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('Verifkasi tidak dapat dilanjutkan karena member terdeteksi blacklist/fraud.')</script>");
                    return;
                }

                DateTime OccuredDate = DateTime.ParseExact(TXT_OCCUREDDATE.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime DocDate = DateTime.ParseExact(TXT_COMPLETEDATE.Text, @"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string formattedOccuredDate = OccuredDate.ToString("yyyy-MM-dd");
                string formattedDocDateDate = DocDate.ToString("yyyy-MM-dd");


                Session["txtComment"] = txtCOMMENT.Text.Trim();
                Session["e"] = e.Item.Cells[0].Text;

                try
                {
                    conn.QueryString = "EXEC SP_GET_DATE_DIFF '" + formattedOccuredDate + "', '" + formattedDocDateDate + "'";
                    conn.ExecuteQuery();
                    int dateRange = Convert.ToInt32(conn.GetFieldValue("DATERANGE").ToString());

                    if (dateRange >= 90)
                    {
                        string message = "Pengajuan klaim lebih dari 90 Hari Kalender (dihitung dari tanggal risiko s/d tanggal dokumen lengkap), apakah anda akan melanjutkan proses klaim ini?";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
                    }
                    else
                    {
                        conn.QueryString = "select " +
                                    "LAST_TRACK " +
                                    "from V_APPLICATION_CLAIM_MASTER " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = " + DDL_SEQ.SelectedValue;
                        conn.ExecuteQuery();
                        string
                        track = conn.GetFieldValue("LAST_TRACK").ToString();

                        if (e.Item.Cells[0].Text == "4")
                        {
                            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_APV_VALIDATION " +
                                                "'" + LB_REGNO.Text + "'," +
                                                DDL_SEQ.SelectedValue + "," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteQuery();

                            if (conn.GetRowCount() > 0)
                            {
                                LoadPendingItems();
                                return;
                            }
                        }

                        try
                        {
                            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_NEXT_TRACK " +
                                                "'" + LB_REGNO.Text + "-" + DDL_SEQ.SelectedValue + "'," +
                                                "'" + e.Item.Cells[0].Text + "'," +
                                                "'" + txtCOMMENT.Text.Trim() + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();

                            conn.QueryString = "exec SP_LINK_REINS_APPLICATION_CLAIM_MASTER " +
                                                "'" + LB_REGNO.Text + "'," +
                                                DDL_SEQ.SelectedValue + "," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteQuery();

                            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'ClaimReg.aspx?seq=" + track + "';</script>");
                        }
                        catch (Exception ex) { ex.Message.ToString(); }
                    }


                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        private void ProsesData()
        {

            conn.QueryString = "select " +
                                    "LAST_TRACK " +
                                    "from V_APPLICATION_CLAIM_MASTER " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = " + DDL_SEQ.SelectedValue;
            conn.ExecuteQuery();
            string
            track = conn.GetFieldValue("LAST_TRACK").ToString();

            if (Session["e"].ToString() == "4")
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_APV_VALIDATION " +
                                    "'" + LB_REGNO.Text + "'," +
                                    DDL_SEQ.SelectedValue + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    LoadPendingItems();
                    return;
                }
            }

            try
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_NEXT_TRACK " +
                                    "'" + LB_REGNO.Text + "-" + DDL_SEQ.SelectedValue + "'," +
                                    "'" + Session["e"].ToString() + "'," +
                                    "'" + Session["txtComment"].ToString() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                conn.QueryString = "exec SP_LINK_REINS_APPLICATION_CLAIM_MASTER " +
                                    "'" + LB_REGNO.Text + "'," +
                                    DDL_SEQ.SelectedValue + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'ClaimReg.aspx?seq=" + track + "';</script>");
            }
            catch { }
        }



        protected void LoadPendingItems()
        {
            LB_TITLE.Text = "UNCOMPLETED ITEMS";
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimUncompletedItems.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + "';</script>");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            string seq = DDL_SEQ.SelectedValue;
            if (seq == "")
                seq = "0";

            conn.QueryString = "select START_DATE,END_DATE from APPLICATION_MAIN_INFO where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DateTime _startPeriod;
            _startPeriod = DateTime.Parse(conn.GetFieldValue("START_DATE").ToString());

            DateTime _endPeriod;
            _endPeriod = DateTime.Parse(conn.GetFieldValue("END_DATE").ToString());

            DateTime _occuredDate;
            _occuredDate = DateTime.Parse(GlobalUse.GlobalDateFormat(TXT_OCCUREDDATE.Text.Trim(), "d/M/yyyy"));

            if (_endPeriod < _occuredDate && LB_POLICYNO.Text != "31120230000046")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('KLAIM DILUAR MASA PERJANJIAN')", true);
            }
            else if (_startPeriod > _occuredDate && LB_POLICYNO.Text != "31120230000046")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('KLAIM DILUAR MASA PERJANJIAN')", true);
            }
            else
            {

                try
                {
                    string warning = ShowBlacklist(); 
                    if (!string.IsNullOrEmpty(warning)) 
                    {
                        var sValue = Session["s"] as string;
                        username = string.IsNullOrEmpty(sValue) ? (Session["username"] as string) : GlobalUse.GetSession(sValue);
                        conn.QueryString = "exec SP_SEND_EMAIL_KLAIM_NASABAH_BERESIKO_TINGGI2 '" + LB_REGNO.Text + "~" + username + "'";
                        conn.ExecuteQuery();
                    }

                    conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        seq + "," +
                                        "'" + DDL_TYPE.SelectedValue + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_RECEIVEDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_OCCUREDDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + DDL_LOCATION.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                    conn2.QueryString = "EXEC SP_REFF_CLAIM_MASTER_COMPLETE_DATE " +
                                       "'" + LB_REGNO.Text + "'," + seq + "," +
                                       "'" + GlobalUse.GlobalDateFormat(TXT_COMPLETEDATE.Text.Trim(), "d/M/yyyy") + "'";
                    conn2.ExecuteQuery();

                    Response.Redirect("ClaimAppFrame.aspx?REGNO=" + conn.GetFieldValue("REGNO").ToString() + "&SEQ=" + conn.GetFieldValue("SEQ").ToString());
                }
                catch { }
            }
        }

        protected void BT_UNAPPROVE_Click(object sender, EventArgs e)
        {
            string seq = DDL_SEQ.SelectedValue;
            if (seq == "")
                seq = "0";

            string TEXT_DDL_UNAPPROVE = DDL_UNAPPROVE.SelectedItem.Text;

            try
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_UNAPPROVE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    seq + "," +
                                    "'" + TEXT_DDL_UNAPPROVE + " (" + DDL_UNAPPROVE.SelectedValue + ")'," +
                                    "'" + UNAPPROVE_REMARK.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                //Response.Redirect("ClaimAppFrame.aspx?REGNO=" + conn.GetFieldValue("REGNO").ToString() + "&SEQ=" + conn.GetFieldValue("SEQ").ToString());
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Process Unapprove Successfully! Silahkan cek data di menu Claim Verification')", true);
                //Response.Redirect("ClaimReg.aspx?seq=3");
            }
            catch { }
        }

        protected void ShowBenefit()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimBenefitFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowBenefit();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimDocFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimShareFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimUWFrame.aspx?REGNO=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRemarkFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Parameter/TC.aspx?CODE=" + LB_TC_ID.Text + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Track.aspx?tipe=CLM&owner=" + LB_REGNO.Text + "-" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT_PENDING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimPendingFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void DDL_SEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ClaimApp.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue);
        }

        protected void buttonHide_ServerClick(object sender, EventArgs e)
        {
            ProsesData();
        }

        private void showMessage(string sMessage, string sType)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertmesg", "<script language=javascript> Swal.fire({position: 'center',type: '" + sType + "',title: '" + sMessage.Replace("'", "") + "',showConfirmButton: true, showCancelButton: true,});</script>");
        }

        protected string ShowBlacklist()
        {
            string warning = "";
            conn.QueryString = "EXEC SP_APPLICATION_MASTER_CLAIM '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            var MAIN_INSURED_BLOCK_STATUS = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            var POLICY_HOLDER_BLOCK_STATUS = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            var RESIKO_TINGGI = conn.GetFieldValue("RESIKO_TINGGI").ToString();

            if (!string.IsNullOrEmpty(MAIN_INSURED_BLOCK_STATUS))
            {
                warning = MAIN_INSURED_BLOCK_STATUS + "<br/>";
            }

            if (!string.IsNullOrEmpty(POLICY_HOLDER_BLOCK_STATUS))
            {
                warning += POLICY_HOLDER_BLOCK_STATUS + "<br/>";
            }

            //if (!string.IsNullOrEmpty(RESIKO_TINGGI))
            //{
            //    warning += RESIKO_TINGGI + "<br/>";
            //}

            return warning;
        }

        protected bool VerificationBlacklist()
        {
            string warning = "";
            bool result = false;
            TR_BLACKLIST.Visible = false;
            conn.QueryString = "exec SP_APPLICATION_MASTER_CLAIM '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            var MAIN_INSURED_BLOCK_STATUS = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            var POLICY_HOLDER_BLOCK_STATUS = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            var RESIKO_TINGGI = conn.GetFieldValue("RESIKO_TINGGI").ToString();
            var flag = conn.GetFieldValue("FLAG").ToString().ToLower();
            var source = conn.GetFieldValue("SOURCE").ToString().ToLower();
            var flag1 = conn.GetFieldValue("FLAG1").ToString().ToLower();
            var source1 = conn.GetFieldValue("SOURCE1").ToString().ToLower();

            if ((!string.IsNullOrEmpty(flag) && flag == "black") || (!string.IsNullOrEmpty(source) && source == "fraud") ||
                (!string.IsNullOrEmpty(flag1) && flag1 == "black") || (!string.IsNullOrEmpty(source1) && source1 == "fraud"))
            {
                if (!string.IsNullOrEmpty(MAIN_INSURED_BLOCK_STATUS))
                {
                    warning = MAIN_INSURED_BLOCK_STATUS + "<br/>";
                }

                if (!string.IsNullOrEmpty(POLICY_HOLDER_BLOCK_STATUS))
                {
                    warning += POLICY_HOLDER_BLOCK_STATUS + "<br/>";
                }

                if (!string.IsNullOrEmpty(RESIKO_TINGGI))
                {
                    warning += RESIKO_TINGGI + "<br/>";
                }

                if (!string.IsNullOrEmpty(warning))
                {
                    result = true;
                    TR_BLACKLIST.Visible = true;
                    TEXT_BLACKLIST.Text = "<p>" + warning + "</p>";
                    TEXT_BLACKLIST.ForeColor = System.Drawing.Color.Red;
                    TBL_BLACKLIST.Style.Add("border", "2px solid red");
                }

            }
            return result;
        }
    }
}
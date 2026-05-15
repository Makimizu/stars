using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationEndorsement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void ShowTrack()
        {
            conn.QueryString = "select " +
                                "c.SEQ,  " +
                                "DESCR = replace(c.DESCR,'ED',''),  " +
                                "COMMENT = convert(int,c.IsCOMMENT)  " +
                                "from (	select SEQ = MAX(SEQ) from TRACK_DATA a " +
                                "        where TIPE_CODE = 'POS' and OWNER = '" + LB_REGNO.Text + "-" + LB_SEQ.Text + "-" + DDL_TYPE.SelectedValue + "' " +
                                "        ) a " +
                                "inner join PARAM_TRACK_NEXT b on a.SEQ = b.SEQ and b.TIPE_CODE = 'POS'  " +
                                "inner join PARAM_TRACK c on b.NEXT_SEQ = c.SEQ and b.TIPE_CODE = c.TIPE_CODE  " +
                                "order by convert(int,c.IsCOMMENT), c.SEQ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                ShowStat();
                return;
            }

            DGR_TRACK.Visible = true;
            BT_PENDING.Visible = true;

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
                                "STAT	= c.DESCR, " +
                                "COMMENT	= b.COMMENT, " +
                                "COLOR	= (case when a.SEQ = 4 then 'Blue' else 'Red' end) " +
                                 "from (	select OWNER, SEQ = MAX(SEQ) from TRACK_DATA a " +
                                "           where TIPE_CODE = 'POS' and OWNER = '" + LB_REGNO.Text + "-" + LB_SEQ.Text + "-" + DDL_TYPE.SelectedValue + "' " +
                                "           group by OWNER " +
                                "        ) a " +
                                "inner join TRACK_DATA b on b.TIPE_CODE='POS' and a.OWNER = b.OWNER collate database_default and a.SEQ = b.SEQ " +
                                "inner join PARAM_TRACK c on b.TIPE_CODE = c.TIPE_CODE and b.SEQ =  c.SEQ";
            conn.ExecuteQuery();

            LB_STAT.Text = conn.GetFieldValue("STAT").ToString();
            LB_STATCOMMENT.Text = conn.GetFieldValue("COMMENT").ToString();

            LB_STAT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());
            LB_STATCOMMENT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());

        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
            LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
            LB_TYPE.Text = Request.QueryString["TYPE"].ToString();


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
                                "UW_CODE = (case when UW_CODE='AC' then 'AUTOMATIC COVER' when UW_CODE='NM' then 'NON MEDICAL' else 'MEDICAL - '+UW_CODE end) " +
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

            conn.QueryString = "select ENDORSEMENT_TYPE, ENDORSEMENT_TYPE_DESCR from V_LINK_UB_TC_ENDORSEMENT_TYPE where TC_CODE = '" + LB_TC_ID.Text + "'";
            conn.ExecuteQuery();
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            if (LB_SEQ.Text != "")
            {
                LoadRecord();
            }

        }

        protected void LoadRecord()
        {
            BT_SAVE.Visible = false;
            LB_REGNO.Visible = true;
            LB_SEQ.Visible = true;

            try
            {
                DDL_TYPE.SelectedValue = LB_TYPE.Text;
                DDL_TYPE.Enabled = false;
            }
            catch { }

            TR_BUTTONS.Visible = true;
            ShowSubmission();
            ShowTrack();
        }

        protected void ShowSubmission()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSbody.location.href = 'ApplicationEndorsement" + DDL_TYPE.SelectedValue + ".aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowSubmission();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSbody.location.href = 'ApplicationEndorsementRemarkFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + DDL_TYPE.SelectedValue + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSbody.location.href = '../Form_Tools/Track.aspx?tipe=POS&owner=" + LB_REGNO.Text + "-" + LB_SEQ.Text + "-" + DDL_TYPE.SelectedValue + "';</script>");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "0," +
                                    "'" + DDL_TYPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("REMARK").ToString() != "")
                {
                    LB_ERROR.Text = conn.GetFieldValue("REMARK").ToString();
                    return;
                }

                Response.Redirect("AppPOSFrame.aspx?REGNO=" + conn.GetFieldValue("REGNO").ToString() + "&SEQ=" + conn.GetFieldValue("SEQ").ToString() + "&TYPE=" + conn.GetFieldValue("ENDORSEMENT_TYPE").ToString());
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
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


                conn.QueryString = "select " +
                                    "LAST_TRACK " +
                                    "from V_APPLICATION_ENDORSEMENT_MASTER " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = " + LB_SEQ.Text + " " +
                                    "and ENDORSEMENT_TYPE = '" + DDL_TYPE.SelectedValue + "'";
                conn.ExecuteQuery();
                string
                track = conn.GetFieldValue("LAST_TRACK").ToString();

                if (e.Item.Cells[0].Text == "4")
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_APV_VALIDATION " +
                                         "'" + LB_REGNO.Text + "'," +
                                         LB_SEQ.Text + "," +
                                         "'" + LB_TYPE.Text + "'," +
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
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK " +
                                        "'" + LB_REGNO.Text + "-" + LB_SEQ.Text + "-" + DDL_TYPE.SelectedValue + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + txtCOMMENT.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'AppPOS.aspx?seq=" + track + "';</script>");
                }
                catch { }

            }

        }

        protected void LoadPendingItems()
        {
            LB_TITLE.Text = "UNCOMPLETED ITEMS";
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSbody.location.href = 'ApplicationEndorsementUncompletedItems.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + DDL_TYPE.SelectedValue + "';</script>");
        }

        protected void BT_PENDING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSbody.location.href = 'ApplicationEndorsementPendingFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + DDL_TYPE.SelectedValue + "';</script>");
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LQ.Form_Claim
{
    public partial class ClaimApp : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadRecord();

                int track = GetTrack();
                if (track > 0)
                {
                    TR_BUTTONS.Visible = true;
                    ShowBenefit();
                }
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
            conn.QueryString = "select TODAY = convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_CLAIMDATE.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_OCCUREDDATE.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_RECEIVEDATE.Text = conn.GetFieldValue("TODAY").ToString();

            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
            //if (Request.QueryString["readonly"].ToString() == "1")
            //{
            //    LB_READONLY.Text = "&readonly=1";
            //    BT_SAVE.Visible = false;
            //    DDL_LOCATION.Enabled = false;
            //    DDL_TYPE.Enabled = false;
            //    TXT_CLAIMDATE.Enabled = false;
            //    TXT_OCCUREDDATE.Enabled = false;
            //    TXT_RECEIVEDATE.Enabled = false;
            //}

            if (Request.QueryString["SEQ"].ToString() != "")
            {
                conn.QueryString = "select SEQ from APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' order by 1 desc";
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
                BT_PENDING.Visible = false;
            }

            FillDDLType();
            LB_TITLE.Text = BT1.Text;

            conn.QueryString = "select " +
                                "REGNO				= a.REGNO,  " +
                                "POLICY_NO			= a.POLICY_NO,  " +
                                "FULLNAME			= a.FULLNAME,  " +
                                "DOB				= convert(varchar(20),a.DOB,106),  " +
                                "SEX				= (case when a.SEX='M' then 'MALE' else 'FEMALE' end),  " +
                                "PRODUCT_CODE		= a.PRODUCT_CODE,  " +
                                "PRODUCT_NAME		= a.PRODUCT_CODE + ' - ' + a.PRODUCT_NAME,  " +
                                "PERIOD             = convert(varchar(20), a.START_DATE,106) + ' - ' + convert(varchar(20), a.END_DATE,106), " +
                                "SUMINS             = replace(convert(varchar(100), convert(money, a.SUMINS),1), '.00',''), " +
                                "START_AGE          = a.START_AGE, " +
                                "UW_CODE            = a.UW_DESCR " +
                                "from V_APPLICATION_MASTER a " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_GENDER.Text = conn.GetFieldValue("SEX").ToString();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("PRODUCT_NAME").ToString();
            LB_TC_ID.Text = conn.GetFieldValue("PRODUCT_CODE").ToString();
            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_CODE").ToString();

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_LOCATION";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOCATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_CAUSE order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CAUSE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select ENABLE = dbo.UFN_PENDING_ENABLE('" + LB_REGNO.Text + "','" + DDL_SEQ.SelectedValue + "','CLM')";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("ENABLE").ToString() == "0")
            {
                BT_PENDING.Enabled = false;
            }
        }

        protected void LoadRecord()
        {
            LB_REGNO.Visible = true;
            TR_TYPE.Visible = true;

            string SEQ = DDL_SEQ.SelectedValue;
            if (SEQ == "")
                SEQ = "0";

            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER '" + LB_REGNO.Text + "'," + SEQ;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_CLAIMDATE.Text = conn.GetFieldValue("CLAIM_DATE").ToString();
                TXT_OCCUREDDATE.Text = conn.GetFieldValue("OCCURED_DATE").ToString();
                TXT_RECEIVEDATE.Text = conn.GetFieldValue("RECEIVED_DATE").ToString();
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

                try
                {
                    DDL_CAUSE.SelectedValue = conn.GetFieldValue("CLAIM_CAUSE").ToString();
                }
                catch { }

                TR_MEMBERSHIP.Visible = true;
            }
        }

        protected int GetTrack()
        {
            if (DDL_SEQ.SelectedValue == "")
                return -1;

            conn.QueryString = "select SEQ = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'CLM', '" + DDL_SEQ.SelectedValue + "')";
            conn.ExecuteQuery();

            return int.Parse(conn.GetFieldValue("SEQ").ToString());
        }


        protected void Disable()
        {
            TXT_CLAIMDATE.ReadOnly = true;
            TXT_OCCUREDDATE.ReadOnly = true;
            TXT_RECEIVEDATE.ReadOnly = true;
            DDL_LOCATION.Enabled = false;
        }

        //protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    if (e.CommandName == "Next")
        //    {
        //        TextBox txtCOMMENT = (TextBox)e.Item.FindControl("TXT_COMMENT");
        //        if (txtCOMMENT.Visible && txtCOMMENT.Text.Trim() == "")
        //        {
        //            txtCOMMENT.Focus();
        //            return;
        //        }

        //        conn.QueryString = "select " +
        //                            "LAST_TRACK " +
        //                            "from V_APPLICATION_CLAIM_MASTER " +
        //                            "where " +
        //                            "REGNO = '" + LB_REGNO.Text + "' " +
        //                            "and SEQ = " + DDL_SEQ.SelectedValue;
        //        conn.ExecuteQuery();
        //        string
        //        track = conn.GetFieldValue("LAST_TRACK").ToString();

        //        if (e.Item.Cells[0].Text == "4")
        //        {
        //            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_APV_VALIDATION " +
        //                                "'" + LB_REGNO.Text + "'," +
        //                                DDL_SEQ.SelectedValue + "," +
        //                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //            conn.ExecuteQuery();

        //            if (conn.GetRowCount() > 0)
        //            {
        //                LoadPendingItems();
        //                return;
        //            }
        //        }

        //        try
        //        {
        //            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_NEXT_TRACK " +
        //                                "'" + LB_REGNO.Text + "-" + DDL_SEQ.SelectedValue + "'," +
        //                                "'" + e.Item.Cells[0].Text + "'," +
        //                                "'" + txtCOMMENT.Text.Trim() + "'," +
        //                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //            conn.ExecuteNonQuery();

        //            conn.QueryString = "exec SP_LINK_REINS_APPLICATION_CLAIM_MASTER " +
        //                                "'" + LB_REGNO.Text + "'," +
        //                                DDL_SEQ.SelectedValue + "," +
        //                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //            conn.ExecuteQuery();

        //            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'ClaimReg.aspx?seq=" + track + "';</script>");
        //        }
        //        catch { }
        //    }
        //}

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Track")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_TRACK");
                string remark = "";

                if (!txt.Visible)
                {
                    conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_APV_VALIDATION '" + LB_REGNO.Text + "'," + DDL_SEQ.SelectedValue + ",'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        LB_TITLE.Text = "VALIDATION";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimValidationLog.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + "';</script>");
                        return;
                    }
                }
                else
                {
                    if (txt.Text.Trim().Replace("'", "`") == "")
                        return;
                    remark = txt.Text.Trim().Replace("'", "`");
                }

                Task.Run(() => SetNextTrack(e.Item.Cells[0].Text, remark, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")));

                string URL = "../Standard/default.html";
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '" + URL + "';</script>");
            }
        }


        protected void SetNextTrack(string track, string remark, string userby)
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_NEXT_TRACK " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DDL_SEQ.SelectedValue + "'," +
                                        track + "," +
                                        "'" + remark + "'," +
                                        "'" + userby + "'";
            conn.ExecuteQuery(500000);
        }

        protected void LoadPendingItems()
        {
            LB_TITLE.Text = "UNCOMPLETED ITEMS";
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimUncompletedItems.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + DDL_SEQ.SelectedValue + "';</script>");
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
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_App/ApplicationOtherInfo.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
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
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_App/ApplicationLoadingShare.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT_6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_App/ApplicationPremiumTermFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
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


    }
}
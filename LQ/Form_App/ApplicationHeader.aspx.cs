using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LQ.Form_App
{
    public partial class ApplicationHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                LoadRecord();
                ShowTCBenefit();
                track = GetTrack();
            }
        }

        protected int GetTrack()
        {
            conn.QueryString = "select SEQ = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            return int.Parse(conn.GetFieldValue("SEQ").ToString());
        }
        

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_INSPERIOD.Text = conn.GetFieldValue("INSURANCE_PERIOD").ToString();
            LB_PAYMENTPERIOD.Text = conn.GetFieldValue("PAYMENT_PERIOD").ToString();
            LB_PREMIUM.Text = conn.GetFieldValue("BASICPREMIUM").ToString();
            LB_PRODUCTGROUP.Text = conn.GetFieldValue("PRODUCT_GROUP_NAME").ToString();
            LB_PRODUCTNAME.Text = conn.GetFieldValue("PRODUCT_NAME").ToString();
            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_DESCR").ToString();
            LB_FOP.Text = conn.GetFieldValue("FOP_DESCR").ToString();
            LB_VACC.Text = conn.GetFieldValue("VACC").ToString();
            LB_TOPUP_REGULER.Text = conn.GetFieldValue("TOPUP_STANDARD").ToString();
            LB_TOPUP_IRREGULER.Text = conn.GetFieldValue("TOPUP_IREGULER").ToString();
            LB_STAT.Text = conn.GetFieldValue("STAT_DESCR").ToString();
        }

        protected void ShowTCBenefit()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationTCBenefit.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowTCBenefit();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationMemberAgent.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationLoadingShare.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationOtherInfo.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationDocumentFrameChild.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationRemarkFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationPremiumTermFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Track")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_TRACK");
                string remark = "";

                if (!txt.Visible)
                {
                    conn.QueryString = "exec SP_APPLICATION_VALIDATION_LOG " +
                                        "'" + LB_REGNO.Text + "', " +
                                        e.Item.Cells[0].Text + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'"; ;
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        LB_TITLE.Text = "VALIDATION";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationValidationLog.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
                        return;
                    }
                }
                else
                {
                    if (txt.Text.Trim().Replace("'", "`") == "")
                        return;
                    remark = txt.Text.Trim().Replace("'", "`");
                }

                conn.QueryString = "exec SP_APPLICATION_MASTER_NEXT_TRACK " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'UW'," +
                                        e.Item.Cells[0].Text + "," +
                                        "'" + remark + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                string URL = "../Standard/default.html";
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '" + URL + "';</script>");
            }
        }

    }
}
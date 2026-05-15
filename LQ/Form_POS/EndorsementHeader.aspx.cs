using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LQ.Form_POS
{
    public partial class EndorsementHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMode(Request.QueryString["ID"].ToString());
                LoadRecord();
                ShowSubmission();
            }
        }

        protected void SetMode(string ID)
        {
            conn.QueryString = "select " +
                                "REGNO, " +
                                "SEQ " +
                                "from V_APPLICATION_ENDORSEMENT_PARENT " +
                                "where " +
                                "REGNO + '-' + Convert(varchar(10), SEQ) = '" + ID + "'";
            conn.ExecuteQuery();

            LB_REGNO.Text = conn.GetFieldValue("REGNO").ToString();
            LB_SEQ.Text = conn.GetFieldValue("SEQ").ToString();
        }

        protected int GetTrack()
        {
            conn.QueryString = "select SEQ = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_SEQ.Text + "')";
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

        protected void ShowSubmission()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementReportFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowSubmission();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_App/ApplicationPremiumTermFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementDocFrame.aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementRemarkFrame.aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementPendingFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_App/ApplicationLoadingShare.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Track")
            {

                TextBox txt = (TextBox)e.Item.FindControl("TXT_TRACK");
                string remark = "";

                if (!txt.Visible)
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_VALIDATION_LOG " +
                                        "'" + LB_REGNO.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        LB_TITLE.Text = "VALIDATION";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementValidationLog.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
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
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK " +
                                       "'" + LB_REGNO.Text + "'," +
                                       "'" + LB_SEQ.Text + "'," +
                                       track + "," +
                                       "'" + remark + "'," +
                                       "'" + userby + "'";
            conn.ExecuteQuery(500000);
        }
    }
}
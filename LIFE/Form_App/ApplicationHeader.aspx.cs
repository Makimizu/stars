using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LIFE.Form_App
{
    public partial class ApplicationHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected float track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                LoadRecord();
                ShowTCBenefit();
                track = GetTrack();
                ShowTrack();
            }
        }

        protected float GetTrack()
        {
            conn.QueryString = "select SEQ = [LIFE].[dbo].[UFN_GET_APP_TRACK]('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            return float.Parse(conn.GetFieldValue("SEQ").ToString());
        }

        protected void ShowTrack()
        {
            conn.QueryString = "exec [LIFE].[dbo].[SP_APPLICATION_TRACK_BUTTON] '" + LB_REGNO.Text + "','UW', ''";
            conn.ExecuteQuery();
            DGR_TRACK.DataSource = conn.GetDataTable().Copy();
            DGR_TRACK.DataBind();

            for (int i = 0; i < DGR_TRACK.Items.Count; i++)
            {
                Button bt = (Button)DGR_TRACK.Items[i].FindControl("BT_TRACK");
                TextBox txt = (TextBox)DGR_TRACK.Items[i].FindControl("TXT_TRACK");
                Label lb = (Label)DGR_TRACK.Items[i].FindControl("LB_TRACK");
                BT8.Visible = true ; //Add by firman
                if (DGR_TRACK.Items[i].Cells[3].Text == "1")
                {
                    bt.Visible = false;
                    txt.Visible = false;
                    lb.Text = DGR_TRACK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                    //BT8.Visible = false;
                }
                else
                {
                    bt.Text = DGR_TRACK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                    bt.Attributes.Add("onclick", "if(!confirm('Are you sure to go to " + bt.Text + " ?')){return false;};");
                    if (DGR_TRACK.Items[i].Cells[2].Text == "0")
                        txt.Visible = false;
                }
            }
        }


        protected void LoadRecord()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            conn.QueryString = "exec [LIFE].[dbo].[SP_APPLICATION_MASTER] '" + LB_REGNO.Text + "'";
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

            string status1 = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string status2 = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string status3 = conn.GetFieldValue("RESIKO_TINGGI").ToString();
            string status4 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status5 = conn.GetFieldValue("RISK_COUNTRY").ToString();
            string status6 = conn.GetFieldValue("RISK_JOB").ToString();
            string warning = "";

            if (!string.IsNullOrEmpty(status1) && !string.IsNullOrWhiteSpace(status1))
            {
                warning = status1;
            }

            if (!string.IsNullOrEmpty(status2) && !string.IsNullOrWhiteSpace(status2))
            {
                if (warning == "")
                {
                    warning = status2;
                }
                else
                {
                    warning += "<br>" + status2;
                }
            }

            if (!string.IsNullOrEmpty(status3) && !string.IsNullOrWhiteSpace(status3))
            {
                if (warning == "")
                {
                    warning = status3;
                }
                else
                {
                    warning += "<br>" + status3;
                }
            } 
            else if (!string.IsNullOrEmpty(status4) && !string.IsNullOrWhiteSpace(status4))
            {
                if (warning == "")
                {
                    warning = status4;
                }
                else
                {
                    warning += "<br>" + status4;
                }
            }
            
            if (!string.IsNullOrEmpty(status5) && !string.IsNullOrWhiteSpace(status5))
            {
                if (warning == "")
                {
                    warning = status5;
                }
                else
                {
                    warning += "<br>" + status5;
                }
            }
            
            if (!string.IsNullOrEmpty(status6) && !string.IsNullOrWhiteSpace(status6))
            {
                if (warning == "")
                {
                    warning = status6;
                }
                else
                {
                    warning += "<br>" + status6;
                }
            }

            if (warning != "" && warning != "&nbsp;")
            {
                LB_WARNING.Text = warning;
                LB_WARNING.CssClass = "alert";
            }
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

        protected void BT8_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationHistoryPos.aspx?ID=" + LB_REGNO.Text + "';</script>");

            //  LB_TITLE.Text = ((Button)sender).Text;

            //  //conn.QueryString = "select b.STAT_TRACK ,a.ENDORSEMENT_TYPE  from APPLICATION_ENDORSEMENT_MASTER a inner join V_APPLICATION_master b on a.regno= b.regno where a.regno='" + LB_REGNO.Text + "'";
            //  //conn.ExecuteQuery();
            //  conn.QueryString = "SELECT TOP 1 a.SEQ,a.ENDORSEMENT_TYPE FROM    APPLICATION_ENDORSEMENT_MASTER a INNER JOIN  V_APPLICATION_master b ON a.regno = b.regno WHERE  a.regno = '" + LB_REGNO.Text + "' ORDER BY  a.SEQ DESC";
            //  conn.ExecuteQuery();
            //  string strENDORSEMENT_TYPE = conn.GetFieldValue("ENDORSEMENT_TYPE").ToString();
            //  string strSEQ = conn.GetFieldValue("SEQ").ToString();

            //  //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationPremiumTermFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
            ////  ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_POS/EndorsementPayOutFrame.aspx?REGNO=D232579&SEQ=8&TYPE=MBR01&REMARK=1';</script>");
            // ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_POS/EndorsementPayOutFrame.aspx?regno=" + LB_REGNO.Text + "&SEQ=" + strSEQ + "&TYPE=" + strENDORSEMENT_TYPE + "&REMARK=1';</script>");
        }

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Track")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_TRACK");
                string remark = "";

                if (!txt.Visible)
                {
                    conn.QueryString = "exec [LIFE].[dbo].[SP_APPLICATION_VALIDATION_LOG] " +
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

                conn.QueryString = "exec [LIFE].[dbo].[SP_APPLICATION_MASTER_NEXT_TRACK] " +
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
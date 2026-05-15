using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Member
{
    public partial class GPA_Proses : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_URL_REFERRER.Text = Request.UrlReferrer.ToString();
                string BATCH_ID = Request.QueryString["BATCH_ID"];
                LoadRecord(BATCH_ID);
            }
        }

        protected void LoadRecord(string BATCH_ID)
        {
            LB_ID.Text = BATCH_ID;
            BT_GOTO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES ?')){return false;};");
            I2.Attributes.Add("src", "../Form_Tools/Track.aspx?tipe=GPA&owner=" + LB_ID.Text);

            conn.QueryString = "exec SP_GPA_TRACK_GOTO_LIST @BATCH_ID = '" + BATCH_ID + "' ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GOTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select POLICY_PERIOD_ID,TIPE_ENDORS from V_GPA_ENDORSEMENT_BATCH where BATCH_ID='" + BATCH_ID + "'";
            conn.ExecuteQuery();
            LB_TIPE.Text = conn.GetFieldValue("TIPE_ENDORS").ToString();
            LB_POLICY_PERIOD.Text = conn.GetFieldValue("POLICY_PERIOD_ID").ToString();

            conn.QueryString = "exec SP_GPA_ENDORSEMENT_BATCH_INFO '" + BATCH_ID + "'";
            conn.ExecuteQuery();
            DGR_INFO_BATCH.DataSource = conn.GetDataTable();
            DGR_INFO_BATCH.DataBind();

            conn.QueryString = "exec SP_UW_COMPANY_INFO '" + LB_POLICY_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DGR_INFO_COMPANY.DataSource = conn.GetDataTable();
            DGR_INFO_COMPANY.DataBind();

            conn.QueryString = "SELECT REPORT_URL FROM dbo.V_GPA_ENDORSEMENT_BATCH WHERE BATCH_ID='" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            string URL = conn.GetFieldValue(0, 0);

            ShowProses();
        }

        protected void ShowProses()
        {
            LBL_TITLE.Text = BT_PROSES.Text;

            switch (LB_TIPE.Text)
            {
                case "ADD": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_ADD_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "DEL": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_DEL_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "CHG": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_CHG_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "UPD": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_UPD_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "RPC": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_RPC_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "BAN": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_BAN_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
                case "CAN": Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Member/GPA_Proses_CAN_Frame.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
                    break;
            }
        }

        protected void BT_PROSES_Click(object sender, EventArgs e)
        {
            ShowProses();
        }

        protected void BT_ARSIP_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = BT_ARSIP.Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_GPA", LB_ID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            Response.Write("<script language='javascript'>parent.gpabody.location.href = '" + URL + "';</script>");
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = BT_REMARK.Text;
            Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Tools/Remark.aspx?tipe=GPA&owner=" + LB_ID.Text + "';</script>");
        }

        protected void BT_REPORT_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = BT_ARSIP.Text;
            conn.QueryString = "select REPORT_SSRS_URL from V_GPA_ENDORSEMENT_BATCH where BATCH_ID='" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            Response.Write("<script language='javascript'>parent.gpabody.location.href = '" + conn.GetFieldValue("REPORT_SSRS_URL").ToString() + "';</script>");
        }

        protected void BT_GOTO_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_GPA_ENDORSEMENT_BATCH_VALIDATION '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            string result = conn.GetFieldValue("RESULT").ToString();

            if (result.Trim() != "")
            {
                GlobalTools.popMessage(this, result);
                return;
            }

            conn.QueryString = "exec SP_GPA_TRACK_GOTO_PROSES @BATCH_ID='" + LB_ID.Text + "',@NEXT_TRACK = " + DDL_GOTO.SelectedValue + ", @USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            Response.Redirect(LB_URL_REFERRER.Text);
        }

        protected void BT_PLAN_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = BT_PLAN.Text;

            conn.QueryString = "select POLICY_PERIOD_ID from ENDORSEMENT_BATCH where BATCH_ID='" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            Response.Write("<script language='javascript'>parent.gpabody.location.href = '../Form_Klien/Polis_Period_Plan.aspx?PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
        }


    }
}
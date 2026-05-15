using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace UWBOX.Form_Tools
{
    public partial class SurplusUWPreviewHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_BATCH_ID.Text = Request.QueryString["ID"].ToString();
                LB_SURPLUS_GROUP.Text = Request.QueryString["GROUP"].ToString();
                LoadRecord();
                ShowReport();
            }
        }

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS " +
                                "'" + LB_BATCH_ID.Text + "'," +
                                "'" + LB_SURPLUS_GROUP.Text + "'";
            conn.ExecuteQuery();

            LB_USERBY.Text = conn.GetFieldValue("USERBY").ToString();
            LB_USERDATE.Text = conn.GetFieldValue("USERDATE").ToString();

            if (conn.GetFieldValue("APPROVED").ToString() == "1")
            {
                BT_APPROVE.Visible = false;
            }

            BT_APPROVE.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO APPROVE BATCH ?')){return false;};");
            BT_REJECT.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO REJECT BATCH ?')){return false;};");
        }

        protected void ShowReport()
        {
            conn.QueryString = "select APP_ID, CODE from SECURITY.dbo.REPORT_LIST where APP_ID = 'UW' and REPORT_NAME = 'RPT_UW_SURPLUS_DETAIL'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../../ReportViewer/Viewer.aspx?APPID=" + conn.GetFieldValue("APP_ID").ToString() + "&CODE=" + conn.GetFieldValue("CODE").ToString() + "&BATCH_ID=" + LB_BATCH_ID.Text + "&GROUP=" + LB_SURPLUS_GROUP.Text + "';</script>");
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS_PROCESS_VALIDATE " +
                 "'" + LB_BATCH_ID.Text + "'," +
                 "'" + LB_SURPLUS_GROUP.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'SurplusUWValidation.aspx?BATCH_ID=" + LB_BATCH_ID.Text + "&GROUP=" + LB_SURPLUS_GROUP.Text + "';</script>");
                return;
            }

            Task.Run(() => ProcessBatch());
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'SurplusUwList.aspx?MODE=A';</script>");
        }

        protected void ProcessBatch()
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS_PROCESS " +
                "'" + LB_BATCH_ID.Text + "'," +
                "'" + LB_SURPLUS_GROUP.Text + "'," +
                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(500000);
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS_GROUP_DELETE " +
                "'" + LB_BATCH_ID.Text + "'," +
                "'" + LB_SURPLUS_GROUP.Text + "'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'SurplusUwList.aspx?MODE=A';</script>");
        }
    }
}
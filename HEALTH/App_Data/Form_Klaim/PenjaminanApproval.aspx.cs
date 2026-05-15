using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class PenjaminanApproval : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["NOSURAT"].ToString();
                BlockingServiceCheck();
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_APPROVAL " +
                                    "'" + LB_ID.Text + "'," +
                                    "2," +
                                    "'" + TXT_REJECT.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }
            
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'Penjaminan.aspx?NOSURAT=" + LB_ID.Text + "';</script>");

        }

        protected void BlockingServiceCheck()
        {
            conn.QueryString = "select " +
                                "a.NOMOR_SURAT_JAMINAN " +
                                "from CLAIM_SURAT_JAMINAN_MASTER a " +
                                "inner join PESERTA_MASTER b on a.REGNO = b.REGNO " +
                                "inner join POLICY_BLOCKING_SERVICE c on b.POLICY_ID = c.POLICY_ID and c.BLOCKED_DATE is not null and c.UNBLOCKED_BY is null " +
                                "where " +
                                "a.NOMOR_SURAT_JAMINAN = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                BT_APPROVE.Visible = false;
                TXT_REJECT.Text = "Polis sedang dalam status BLOCKING SERVICE";
            }
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            if (TXT_REJECT.Text.Trim() == "")
            {
                LB_ERROR.Text = "Alasan REJECT/CANCEL tidak boleh kosong !";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_APPROVAL " +
                                    "'" + LB_ID.Text + "'," +
                                    "3," +
                                    "'" + TXT_REJECT.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '../Form_Tools/InquiryScreen.aspx?CODE=035b';</script>");
        }
    }
}
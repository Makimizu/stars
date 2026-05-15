using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class PenjaminanMonitoring : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["NOSURAT"].ToString();
                FillDGRMonitoring();
            }

        }

        protected void FillDGRMonitoring()
        {
            conn.QueryString = "select " +
                                "[NO] = b.SEQ, " +
                                "[TGL] = convert(varchar(20),b.TGL_MONITORING,106), " +
                                "[USER] = b.CREATEBY, " +
                                "[WAKTU CATAT] = b.CREATEDATE " +
                                "from CLAIM_SURAT_JAMINAN_MASTER a " +
                                "inner join CLAIM_SURAT_JAMINAN_MONITORING b on a.ID=b.SURAT_JAMINAN_ID " +
                                "where " +
                                "a.NOMOR_SURAT_JAMINAN = '" + LB_ID.Text + "' " +
                                "order by b.SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MON.DataSource = dt;
            DGR_MON.DataBind();

        }

        protected void DGR_MON_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string script = "<script language='javascript'> window.open('PenjaminanMonitoringEntri.aspx?NOSURAT=" + LB_ID.Text + "&SEQ=" + e.Item.Cells[1].Text.ToString() + "','MONITORING','height=600px,width=900px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>";
            Response.Write(script);
        }

        protected void BT_MON_ADDTGL_Click(object sender, EventArgs e)
        {
            if (TXT_MON_ADDTGL.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_ADD " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_MON_ADDTGL.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                return;
            }

            FillDGRMonitoring();
            TXT_MON_ADDTGL.Text = "";
        }
    }
}
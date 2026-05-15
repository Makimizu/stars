using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationHistoryPos : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                FillDGRHistory();
            }
        }

        protected void FillDGRHistory()
        {
            conn.QueryString = "exec [LIFE].[dbo].[SP_DETAIL_POLISNO_POS_HISTORY] '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_HISTORY.DataSource = dt;
            DGR_HISTORY.DataBind();

      
        }

        

        protected void DGR_HISTORY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "ViewReport")
            {
                string[] param = e.CommandArgument.ToString().Split('|');

                string Reg_No = param[0];
                string Seq = param[1];
                string EndormentType = param[2];
                int Code = 0;
                if ( EndormentType == "Surrender" )
                {
                    Code = 3;
                }
                else if (EndormentType == "Freelook") 
                {
                    Code = 5;
                }
                else if (EndormentType == "Withdrawal")
                {
                    Code = 4;
                }
                else if (EndormentType == "Installment/Beasiswa")
                {
                    Code = 6;
                }
                else if (EndormentType == "Payment Method (CC/Cash/Debet)" || EndormentType == "Cetak Ulang Polis" || EndormentType == "Change Payment Frequency"
                         || EndormentType == "Change Premium & Top Up" || EndormentType == "Koreksi tanggal lahir" || EndormentType == "Korespondensi"
                         || EndormentType == "Korespondensi_Address" || EndormentType == "Pegkinian Data" || EndormentType == "Pembatalan peserta"
                         || EndormentType == "Perubahan Ahli Waris" || EndormentType == "Perubahan data pribadi" || EndormentType == "Perubahan Komposisi Wakaf"
                         || EndormentType == "Perubahan Manfaat" || EndormentType == "Perubahan Rekening Bank") 
                {
                    Code = 1;
                }
                else if (EndormentType == "TopUp Irreguler" || EndormentType == "Switching" || EndormentType == "Apportion")
                {
                    Code = 2;
                }
                else if (EndormentType == "Maturiy")
                {
                    Code = 7;
                }
                conn.QueryString = "select TOP 1 URL_APP from V_APPLICATION_ENDORSEMENT_REPORT " +
                                   "where REGNO='" + Reg_No + "' and SEQ=" + Seq + " and CODE= " + Code;

                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    string urlReport = conn.GetFieldValue("URL_APP").Replace("'", "\\'");

                    ClientScript.RegisterStartupScript(
                        GetType(),
                        "OpenReport",
                        "window.location.href='" + urlReport + "';",
                        true
                    );
                }
            }
        }
    }
}
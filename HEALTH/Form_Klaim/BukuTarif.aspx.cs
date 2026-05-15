using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class BukuTarif : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                string provider = Request.QueryString["PROVIDER"] ?? "";
                LB_ID.Text = "Clinical Pathway on progress";
                FillDGRTarif( provider);
            }
        }

        protected void FillDGRTarif( string provider = "")
        {
            string query = "exec SP_CLM_PROVIDER_TARIF '" + provider + "','DRI'";

            conn.QueryString = query;
            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable().Copy();
            DGR_TARIF.DataSource = dt;
            DGR_TARIF.DataBind();
        }


        protected void DGR_TARIF_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtKET = (TextBox)e.Item.FindControl("TXT_TRF_KET");
            TextBox txtTRF = (TextBox)e.Item.FindControl("TXT_TRF_RP");
            TextBox txtTGL = (TextBox)e.Item.FindControl("TXT_TRF_TGL");

            if (e.CommandName == "Edit")
            {
                try
                {
                    conn.QueryString = "update PROVIDER_TARIF set " +
                                        "DESCR = '" + txtKET.Text.Trim() + "'," +
                                        "TARIF = '" + txtTRF.Text.Trim().Replace(",", "") + "'," +
                                        "TGL_BERLAKU = '" + GlobalUse.GlobalDateFormat(txtTGL.Text.Trim(), "d/M/yyyy") + "'," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "ID='" + e.Item.Cells[0].Text.Replace("&nbsp;", "") + "'";
                    conn.ExecuteNonQuery();
                    FillDGRTarif();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PROVIDER_TARIF where " +
                                    "ID='" + e.Item.Cells[0].Text.Replace("&nbsp;", "") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRTarif();
        }


        protected void DGR_TARIF_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_TARIF.CurrentPageIndex = e.NewPageIndex;
            FillDGRTarif();
        }

        protected void BT_TRF_Click(object sender, EventArgs e)
        {
            string provider = Request.QueryString["PROVIDER"] ?? "";

            // Safer way to build query
            //string query = $"SELECT TOP 1 [PATH_DOKUMEN] FROM V_PROVIDER_TARIF_DOKUMEN WHERE KODE_PROVIDER = '{provider}'";
            string query = string.Format("SELECT TOP 1 [PATH_DOKUMEN] FROM V_PROVIDER_TARIF_DOKUMEN WHERE KODE_PROVIDER = '{0}'",provider);


            conn.QueryString = query;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                string filePath = conn.GetFieldValue("PATH_DOKUMEN");
                string fullUrl = ResolveUrl("~/" + filePath);

                // Directly open the document in a new tab
                //string script = $"<script>window.open('{fullUrl}', '_blank');</script>";
                string script = "<script>window.open('" + fullUrl + "', '_blank');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "OpenDocument", script);
            }
            else
            {
                // Optional: show message or handle not found case
                ClientScript.RegisterStartupScript(this.GetType(), "NotFound", "<script>alert('Document tidak ada.');</script>");
            }
        }


        protected void DDL_KIRIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_TARIF.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_TARIF.Items[i].FindControl("DDL_KIRIM");
                TextBox txtemailfax = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_EMAILFAX");

                if (ddl == (DropDownList)sender)
                {
                    if (ddl.SelectedValue == "0")
                        txtemailfax.Text = DGR_TARIF.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (ddl.SelectedValue == "1")
                        txtemailfax.Text = DGR_TARIF.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
            }
        }
    }
}
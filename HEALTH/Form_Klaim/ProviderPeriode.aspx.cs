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
    public partial class ProviderPeriode : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                FillDGR_Periode();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_PROVIDER_RENEWAL_METHOD";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIODERENEW.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR_Periode()
        {
            string code = LB_CODE.Text;

            conn.QueryString = "select " +
                                "ID," +
                                "[AWAL PERIODE] = convert(varchar(20),BEGIN_DATE,106), " +
                                "[AKHIR PERIODE] = convert(varchar(20),END_DATE,106), " +
                                "[NO DOKUMEN] = DOC_NO, " +
                                "[TGL KONTRAK] = convert(varchar(20),TGL_KONTRAK,106),  " +
                                "[RENEWAL METHOD] = b.DESCR " +
                                "from PROVIDER_PERIODE a " +
                                "left join PR_PROVIDER_RENEWAL_METHOD b on a.RENEW_METHOD=b.code " +
                                "where " +
                                "KODE_PROVIDER='" + code + "' " +
                                "order by END_DATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIODE.DataSource = dt;
            DGR_PERIODE.DataBind();

        }

        protected void BT_PERIODEADD_Click(object sender, EventArgs e)
        {
            if (TBL_PERIODE.Visible)
            {
                if (TXT_PERIODEDATE1.Text.Trim() == "" || TXT_PERIODEDATE2.Text.Trim() == "" || TXT_PERIODEDOCNO.Text.Trim() == "" || TXT_PERIODEKONTRAKDATE.Text.Trim() == "")
                    return;

                try
                {
                    conn.QueryString = "exec SP_CLM_PROVIDER_PERIODE_UPSERT " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PERIODEDATE1.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PERIODEDATE2.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + TXT_PERIODEDOCNO.Text.Trim() + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PERIODEKONTRAKDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + DDL_PERIODERENEW.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    TBL_PERIODE.Visible = false;
                    FillDGR_Periode();
                }
                catch { return; }
            }
            else
            {
                TBL_PERIODE.Visible = true;
            }

            TXT_PERIODEDATE1.Text = "";
            TXT_PERIODEDATE2.Text = "";
            TXT_PERIODEDOCNO.Text = "";
            TXT_PERIODEKONTRAKDATE.Text = "";
        }

        protected void DGR_PERIODE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PROVIDER_PERIODE where ID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR_Periode();
                }
                catch { }
            }
        }
    }
}
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
    public partial class ProviderRegistrasi : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();

                if (TXT_CODE.Text.Trim() != "")
                    LoadRecord(TXT_CODE.Text.Trim());
            }
        }

        protected void Setup()
        {
            if (TXT_CODE.Text == "")
            {
                TR_REJECT.Visible = false;
                BT_APPROVE.Visible = false;
                BT_REJECT.Visible = false;
            }
            else
            {
                BT_APPROVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk APPROVE ?')){return false;};");
                BT_REJECT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk REJECT ?')){return false;};");
            }

            conn.QueryString = "select CODE,DESCR from PR_PROPINSI";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TITLE_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TITLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select KODE_PROVIDER,LTRIM(NAMA) from PROVIDER_MASTER where LTRIM(RTRIM(isnull(NAMA,'')))<>'' order by LTRIM(RTRIM(isnull(NAMA,'')))";
            conn.ExecuteQuery();
            DDL_GRUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GRUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TIPE_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_JENIS_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_JENIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,UPPER(DESCR) from PARAM_TBL_KOTA_PROVIDER order by 2";
            conn.ExecuteQuery();
            DDL_KOTA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_KOTA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_KEWARGANEGARAAN";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NEGARA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_PROVIDER_KEPEMILIKAN";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_OWNER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select * from V_PROVIDER where KODE_PROVIDER='" + code + "'";
            conn.ExecuteQuery();

            try
            {
                DDL_TITLE.SelectedValue = conn.GetFieldValue("KODE_TITLE").ToString();
            }
            catch { }
            try
            {
                DDL_TIPE.SelectedValue = conn.GetFieldValue("TIPE_PROVIDER").ToString();
            }
            catch { }
            try
            {
                DDL_NEGARA.SelectedValue = conn.GetFieldValue("NEGARA").ToString();
            }
            catch { }
            try
            {
                DDL_KOTA.SelectedValue = conn.GetFieldValue("KOTA").ToString();
            }
            catch { }
            try
            {
                DDL_JENIS.SelectedValue = conn.GetFieldValue("JENIS_PROVIDER").ToString();
            }
            catch { }
            try
            {
                DDL_GRUP.SelectedValue = conn.GetFieldValue("KODE_PROVIDERGROUP").ToString();
            }
            catch { }
            try
            {
                DDL_OWNER.SelectedValue = conn.GetFieldValue("KEPEMILIKAN_PROVIDER").ToString();
            }
            catch { }

            TXT_ALAMAT1.Text = conn.GetFieldValue("ALAMAT").ToString();
            TXT_ALAMAT2.Text = conn.GetFieldValue("ALAMAT2").ToString();
            TXT_FAX1.Text = conn.GetFieldValue("FAX").ToString();
            TXT_FAX2.Text = conn.GetFieldValue("FAX2").ToString();
            TXT_KODEPOS.Text = conn.GetFieldValue("KODEPOS").ToString();
            TXT_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
            TXT_NPWPALAMAT.Text = conn.GetFieldValue("NPWP_ALAMAT").ToString();
            TXT_NPWPNAMA.Text = conn.GetFieldValue("NPWP_NAMA").ToString();
            TXT_NPWPNO.Text = conn.GetFieldValue("NPWP_NO").ToString();
            TXT_TELEPON1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_TELEPON2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_ADMEDIKA.Text = conn.GetFieldValue("KODE_ADMEDIKA").ToString();
            TXT_ALASAN.Text = conn.GetFieldValue("ALASAN_REGISTRASI").ToString();

            SetPROPINSI();
            //FillDGRSimilarity();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            string kode = "null";
            string group = "null";
            if (TXT_CODE.Text != "")
                kode = "'" + TXT_CODE.Text + "'";
            if (DDL_GRUP.SelectedValue != "")
                group = "'" + DDL_GRUP.SelectedValue + "'";

            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_UPSERT " +
                                    kode + "," +
                                    group + "," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + DDL_JENIS.SelectedValue + "'," +
                                    "'" + DDL_OWNER.SelectedValue + "'," +
                                    "'" + TXT_ADMEDIKA.Text.Trim() + "'," +
                                    "'" + DDL_TITLE.SelectedValue + "'," +
                                    "'" + TXT_NAMA.Text + "'," +
                                    "'" + TXT_ALAMAT1.Text.Trim() + "'," +
                                    "'" + TXT_ALAMAT2.Text.Trim() + "'," +
                                    "'" + TXT_KODEPOS.Text.Trim() + "'," +
                                    "'" + TXT_TELEPON1.Text.Trim() + "'," +
                                    "'" + TXT_TELEPON2.Text.Trim() + "'," +
                                    "'" + TXT_FAX1.Text.Trim() + "'," +
                                    "'" + TXT_FAX2.Text.Trim() + "'," +
                                    "'" + DDL_KOTA.SelectedValue + "'," +
                                    "'" + TXT_NPWPNO.Text.Trim() + "'," +
                                    "'" + TXT_NPWPNAMA.Text.Trim() + "'," +
                                    "'" + TXT_NPWPALAMAT.Text.Trim() + "'," +
                                    "'" + TXT_ALASAN.Text.Trim().Replace("'", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                //LB_ERROR.Text = "<BR>" + conn.QueryString;

                TXT_CODE.Text = conn.GetFieldValue("KODE_PROVIDER").ToString();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }
        }

        protected void FillDGRSimilarity()
        {
            conn.QueryString = "select " +
                                "[NAMA] = UPPER(NAMA), " +
                                "ALAMAT, " +
                                "[STATUS] = b.DESCR, " +
                                "[% SIMILARITY] = convert(decimal(18,2),dbo.f_jaro_winkler('" + TXT_NAMA.Text.Trim() + "',NAMA)*100) " +
                                "from PROVIDER_MASTER a " +
                                "inner join PR_STATUS_PROVIDER b on a.STAT=b.CODE " +
                                "where " +
                                "dbo.f_jaro_winkler('" + TXT_NAMA.Text.Trim() + "',NAMA)>=0.8 " +
                                "and KODE_PROVIDER<>'" + TXT_CODE.Text + "' " +
                                "order by 4 desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SIMILARITY.DataSource = dt;
            DGR_SIMILARITY.DataBind();

            for (int i = 0; i < DGR_SIMILARITY.Items.Count; i++)
            {
                if (DGR_SIMILARITY.Items[i].Cells[3].Text == "NON AKTIF")
                {
                    DGR_SIMILARITY.Items[i].BackColor = System.Drawing.Color.Pink;
                    DGR_SIMILARITY.Items[i].Cells[3].ForeColor = System.Drawing.Color.Red;
                }

                if (DGR_SIMILARITY.Items[i].Cells[0].Text == "100,00")
                {
                    DGR_SIMILARITY.Items[i].Font.Bold = true;
                }
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_APPROVE '" + TXT_CODE.Text.Trim() + "',1,'" + TXT_REJECT.Text.Trim() + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("Provider_Entry.aspx?CODE=" + TXT_CODE.Text);
            }
            catch { }
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            if (TXT_REJECT.Text.Trim() == "")
            {
                GlobalTools.popMessage(this, "alasan REJECT tidak boleh kosong");
                return;
            }

            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_APPROVE '" + TXT_CODE.Text.Trim() + "',1,'" + TXT_REJECT.Text.Trim() + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("ProviderPending.aspx");
            }
            catch { }
        }

        protected void DDL_KOTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPROPINSI();
        }

        protected void SetPROPINSI()
        {
            try
            {
                conn.QueryString = "select PROPINSI from PARAM_TBL_KOTA_PROVIDER where CODE='" + DDL_KOTA.SelectedValue + "'";
                conn.ExecuteQuery();
                DDL_PROPINSI.SelectedValue = conn.GetFieldValue("PROPINSI").ToString();
            }
            catch { }
        }
    }
}
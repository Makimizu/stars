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
    public partial class ProviderEntry : System.Web.UI.Page
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
                {
                    LoadRecord(TXT_CODE.Text.Trim());
                    CallLayanan();
                }
            }

        }

        protected void FillDGRTPA()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR, " +
                                "TPA_VERSION = isnull(b.CODE,'') " +
                                "from PARAM_ACT_TPA a " +
                                "left join PROVIDER_TPA_MAPPING b on a.CODE=b.TPA and b.KODE_PROVIDER='" + TXT_CODE.Text + "' " +
                                "where " +
                                "a.CODE <> '01'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TPA.DataSource = dt;
            DGR_TPA.DataBind();

            for (int i = 0; i < DGR_TPA.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TPA.Items[i].FindControl("TXT_TPAVERSION");
                txt.Text = DGR_TPA.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void SaveDGRTPA()
        {
            string sql = "delete from PROVIDER_TPA_MAPPING where KODE_PROVIDER='" + TXT_CODE.Text + "' ";
            for (int i = 0; i < DGR_TPA.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TPA.Items[i].FindControl("TXT_TPAVERSION");
                if (txt.Text.Trim() != "")
                {
                    sql = sql + "insert into PROVIDER_TPA_MAPPING select " +
                                "'" + TXT_CODE.Text + "'," +
                                "'" + DGR_TPA.Items[i].Cells[0].Text + "'," +
                                "'" + txt.Text.Trim() + "' ";
                }
            }

            try
            {
                conn.QueryString = sql;
                conn.ExecuteNonQuery();
                FillDGRTPA();
            }
            catch { }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_PROPINSI";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TITLE_PROVIDER";
            conn.ExecuteQuery();
            DDL_TITLE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TITLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select KODE_PROVIDER,UPPER(LEFT(NAMA,40)) from PROVIDER_MASTER where LTRIM(RTRIM(isnull(NAMA,'')))<>'' order by LTRIM(RTRIM(isnull(NAMA,'')))";
            conn.ExecuteQuery();
            DDL_GRUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GRUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TIPE_PROVIDER";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_JENIS_PROVIDER";
            conn.ExecuteQuery();
            DDL_JENIS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_JENIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,UPPER(DESCR) from PARAM_TBL_KOTA_PROVIDER order by 2";
            conn.ExecuteQuery();
            DDL_KOTA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_KOTA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_KEWARGANEGARAAN";
            conn.ExecuteQuery();
            DDL_NEGARA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NEGARA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_STATUS_PROVIDER";
            conn.ExecuteQuery();
            DDL_STATUS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

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
                DDL_STATUS.SelectedValue = conn.GetFieldValue("STAT").ToString();
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
            TXT_ALASAN.Text = conn.GetFieldValue("ALASAN_REGISTRASI").ToString();

            SetPROPINSI();
            FillDGRTPA();
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

        protected void DDL_KOTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPROPINSI();
        }

        protected void BT_SAVE_PROV_Click(object sender, EventArgs e)
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
                                    "''," +
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
                conn.ExecuteNonQuery();
                //LB_ERROR.Text = "<BR>" + conn.QueryString;
            }
            catch (System.Exception ex)
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = ex.Message;
                return;
            }

            SaveDGRTPA();

            LB_ERROR.ForeColor = System.Drawing.Color.Blue;
            LB_ERROR.Text = "SUKSES";
        }

        protected void CallLayanan()
        {
            LB_SUB.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderLayanan.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            CallLayanan();
        }
        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderPIC.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }
        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderTarif.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }
        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderDiscount.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }
        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderRekening.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }
        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = 'ProviderPeriode.aspx?CODE=" + TXT_CODE.Text + "';</script>");
        }
        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_SUB.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_PRV", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.provbody.location.href = '" + URL + "';</script>");
        }
    }
}
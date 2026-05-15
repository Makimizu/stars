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
    public partial class ProviderTarif : System.Web.UI.Page
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
                FillDGR_Tarif();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TARIF_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRF_KODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_PROVIDER_KELAS_KAMAR order by 2";
            conn.ExecuteQuery();
            DDL_TRF_KAMAR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRF_KAMAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDL_SubTarif();
        }

        protected void FillDDL_SubTarif()
        {
            conn.QueryString = "select KODE_SUB_TARIF,DESCR from PARAM_TBL_PROVIDER_SUB_TARIF where KODE_TARIF='" + DDL_TRF_KODE.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_TRF_SUB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRF_SUB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_TRF_KODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_SubTarif();
            FillDGR_Tarif();
        }

        protected void FillDGR_Tarif()
        {
            /*
            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.KODE_TARIF, " +
                                "KODE_TARIF_DESCR = b.DESCR, " +
                                "TGL_BERLAKU = convert(varchar(20),a.TGL_BERLAKU,106), " +
                                "TGL_AKHIR_BERLAKU = convert(varchar(20),a.TGL_AKHIR_BERLAKU,106), " +
                                "a.KELAS_KAMAR, " +
                                "a.DESCR, " +
                                "TARIF = replace(convert(varchar(100),convert(money,a.TARIF),1),'.00',''), " +
                                "a.TARIF_P " +
                                "from PROVIDER_TARIF a " +
                                "inner join PR_TARIF_PROVIDER b on a.KODE_TARIF=b.CODE " +
                                "where " +
                                "KODE_PROVIDER='" +LB_CODE.Text+ "' " +
                                "order by  " +
                                "b.DESCR, " +
                                "a.TGL_BERLAKU desc";
            */
            conn.QueryString = "exec SP_CLM_PROVIDER_TARIF '" + LB_CODE.Text + "','" + DDL_TRF_KODE.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TARIF.DataSource = dt;
            DGR_TARIF.DataBind();


            for (int i = 0; i < DGR_TARIF.Items.Count; i++)
            {

                /*
                DropDownList ddlTGL = (DropDownList)DGR_TARIF.Items[i].FindControl("DDL_TARIF_TGL");            
                */
                TextBox txtKET = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_KET");
                TextBox txtTARIF = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_RP");
                TextBox txtTGL = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_TGL");
                Button btDel = (Button)DGR_TARIF.Items[i].FindControl("BT_DELETE");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                txtKET.Text = DGR_TARIF.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtTARIF.Text = DGR_TARIF.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtTGL.Text = DGR_TARIF.Items[i].Cells[6].Text.Replace("&nbsp;", "");

                /*
                conn.QueryString = "select distinct " +
                                    "convert(varchar(20),TGL_BERLAKU,112), " +
                                    "convert(varchar(20),TGL_BERLAKU,106) " +
                                    "from PROVIDER_TARIF a " +
                                    "where " +
                                    "KODE_PROVIDER='" +LB_CODE.Text+ "' " +
                                    "and isnull(KODE_TARIF,'') = '" + DGR_TARIF.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "' " +
                                    "and isnull(KELAS_KAMAR,'') = '" + DGR_TARIF.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "' " +
                                    "and isnull(DESCR,'') = '" + DGR_TARIF.Items[i].Cells[4].Text.Replace("&nbsp;","") + "' " +
                                    "order by " +
                                    "1 desc";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTGL.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddlTGL.SelectedValue = DGR_TARIF.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
                catch { }

                txtTARIF.Text = DGR_TARIF.Items[i].Cells[6].Text;
                */
            }

        }

        protected void BT_TRF_SAVE_Click(object sender, EventArgs e)
        {
            string kelaskamar = "null";
            string subtarif = "null";
            if (DDL_TRF_KAMAR.SelectedValue != "")
                kelaskamar = "'" + DDL_TRF_KAMAR.SelectedValue + "'";
            if (DDL_TRF_SUB.SelectedValue != "")
                subtarif = "'" + DDL_TRF_SUB.SelectedValue + "'";

            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_TARIF_INSERT " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_TRF_KODE.SelectedValue + "'," +
                                    kelaskamar + "," +
                                    subtarif + "," +
                                    "'" + TXT_TRF_DESCR.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_TRF_AMOUNT.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TRF_TGLBERLAKU.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR_Tarif();
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
            }
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
                    FillDGR_Tarif();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PROVIDER_TARIF where " +
                                    "ID='" + e.Item.Cells[0].Text.Replace("&nbsp;", "") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR_Tarif();
        }
    }
}
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
    public partial class PenjaminanMonitoringEntri : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_NOSURAT.Text = Request.QueryString["NOSURAT"];
                LB_SEQ.Text = Request.QueryString["SEQ"];

                Setup();
                LoadRecord();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_GOLONGAN_OPERASI";
            conn.ExecuteQuery();
            DDL_MON_GOL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MON_GOL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_KELAS_RS order by 2";
            conn.ExecuteQuery();
            DDL_MON_KELAS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MON_KELAS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_KETBENEFIT order by 2";
            conn.ExecuteQuery();
            DDL_MON_KELBEN.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MON_KELBEN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_STATUS_MONITORING order by 2";
            conn.ExecuteQuery();
            DDL_MON_STAT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MON_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            BT_MON_ICD.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode1.aspx?field1=CODE&field2=DESCR&tablename=PR_ICD&parent=0&target=TXT_MON_ICD','ICD','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void LoadRecord()
        {
            TR_MON0.Visible = true;
            TR_MON1.Visible = false;
            TR_MON2.Visible = false;
            TR_MON3.Visible = false;
            TR_MON4.Visible = false;

            LB_MON_ERROR.Text = "";
            DDL_HH_TINDAKAN1.SelectedIndex = 0;
            DDL_MM_TINDAKAN1.SelectedIndex = 0;
            DDL_HH_TINDAKAN2.SelectedIndex = 0;
            DDL_MM_TINDAKAN2.SelectedIndex = 0;

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    LB_SEQ.Text;
                conn.ExecuteQuery();

                TXT_MON_AKHIR.Text = conn.GetFieldValue("BIAYA_AKHIR").ToString();
                TXT_MON_KONTAK.Text = conn.GetFieldValue("KONTAK").ToString();
                TXT_MON_LOS.Text = conn.GetFieldValue("LOS").ToString();
                TXT_MON_NOMED.Text = conn.GetFieldValue("MEDICAL_NO").ToString();
                TXT_MON_SEMENTARA.Text = conn.GetFieldValue("BIAYA_SEMENTARA").ToString();
                TXT_MON_TINDAKAN.Text = conn.GetFieldValue("RENCANA_TINDAKAN").ToString();

                TXT_DATE_TINDAKAN1.Text = conn.GetFieldValue("WAKTU_TINDAKAN1").ToString();
                TXT_DATE_TINDAKAN2.Text = conn.GetFieldValue("WAKTU_TINDAKAN2").ToString();

                try
                {
                    DDL_HH_TINDAKAN1.SelectedValue = conn.GetFieldValue("WAKTU_TINDAKAN1_HH").ToString();
                    DDL_MM_TINDAKAN1.SelectedValue = conn.GetFieldValue("WAKTU_TINDAKAN1_MM").ToString();
                }
                catch { }

                try
                {
                    DDL_HH_TINDAKAN2.SelectedValue = conn.GetFieldValue("WAKTU_TINDAKAN2_HH").ToString();
                    DDL_MM_TINDAKAN2.SelectedValue = conn.GetFieldValue("WAKTU_TINDAKAN2_MM").ToString();
                }
                catch { }

                try
                {
                    DDL_MON_KELAS.SelectedValue = conn.GetFieldValue("KELAS_PERAWATAN").ToString();
                }
                catch { }

                try
                {
                    DDL_MON_GOL.SelectedValue = conn.GetFieldValue("GOL_OPERASI").ToString();
                }
                catch { }

                try
                {
                    DDL_MON_KELBEN.SelectedValue = conn.GetFieldValue("KETBENEFIT_CODE").ToString();
                }
                catch { }

                try
                {
                    DDL_MON_STAT.SelectedValue = conn.GetFieldValue("STATUS_MONITORING_KODE").ToString();
                }
                catch { }

                LB_MON_TGL.Text = conn.GetFieldValue("TGL_MONITORING").ToString();
            }
            catch { }

            LoadINFO();
        }

        protected void BT_MON_SAVE_Click(object sender, EventArgs e)
        {
            LB_MON_ERROR.Text = "";

            string kelas = "null";
            string kelben = "null";
            string stat = "null";
            string biayasem = "null";
            string biayaakh = "null";
            string goloperasi = "null";
            string los = "null";
            string waktutindakan1 = "null";
            string waktutindakan2 = "null";

            if (DDL_MON_KELAS.SelectedValue != "")
                kelas = "'" + DDL_MON_KELAS.SelectedValue + "'";
            if (DDL_MON_KELBEN.SelectedValue != "")
                kelben = "'" + DDL_MON_KELBEN.SelectedValue + "'";
            if (DDL_MON_STAT.SelectedValue != "")
                stat = "'" + DDL_MON_STAT.SelectedValue + "'";
            if (DDL_MON_GOL.SelectedValue != "")
                goloperasi = "'" + DDL_MON_GOL.SelectedValue + "'";

            if (TXT_MON_LOS.Text.Trim() != "")
                los = TXT_MON_LOS.Text.Replace(",", "");
            if (TXT_MON_SEMENTARA.Text.Trim() != "")
                biayasem = TXT_MON_SEMENTARA.Text.Replace(",", "");
            if (TXT_MON_AKHIR.Text.Trim() != "")
                biayaakh = TXT_MON_AKHIR.Text.Replace(",", "");
            if (TXT_DATE_TINDAKAN1.Text.Trim() != "")
            {
                waktutindakan1 = "'" + GlobalUse.GlobalDateFormat(TXT_DATE_TINDAKAN1.Text.Trim(), "d/M/yyyy") + " " + DDL_HH_TINDAKAN1.SelectedValue + ":" + DDL_MM_TINDAKAN1.SelectedValue + "'";
            }
            if (TXT_DATE_TINDAKAN2.Text.Trim() != "")
            {
                waktutindakan2 = "'" + GlobalUse.GlobalDateFormat(TXT_DATE_TINDAKAN2.Text.Trim(), "d/M/yyyy") + " " + DDL_HH_TINDAKAN2.SelectedValue + ":" + DDL_MM_TINDAKAN2.SelectedValue + "'";
            }

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_SAVE " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    los + "," +
                                    goloperasi + "," +
                                    "'" + TXT_MON_TINDAKAN.Text.Trim() + "'," +
                                    "'" + TXT_MON_NOMED.Text.Trim() + "'," +
                                    "'" + TXT_MON_KONTAK.Text.Trim() + "'," +
                                    kelas + "," +
                                    kelben + "," +
                                    stat + "," +
                                    biayasem + "," +
                                    biayaakh + "," +
                                    waktutindakan1 + "," +
                                    waktutindakan2 + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_MON_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_MON_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            LB_MON_ERROR.ForeColor = System.Drawing.Color.Blue;
            LB_MON_ERROR.Text = "<BR>SUKSES";
        }

        protected void BT_MON1_Click(object sender, EventArgs e)
        {
            LoadINFO();
        }

        protected void LoadINFO()
        {
            TR_MON1.Visible = true;
            TR_MON2.Visible = false;
            TR_MON3.Visible = false;
            TR_MON4.Visible = false;
            FillDGRMonitoringINFO();
        }

        protected void BT_MON2_Click(object sender, EventArgs e)
        {
            TR_MON1.Visible = false;
            TR_MON2.Visible = true;
            TR_MON3.Visible = false;
            TR_MON4.Visible = false;
            FillDGRMonitoringDiagnosa();
        }

        protected void BT_MON3_Click(object sender, EventArgs e)
        {
            TR_MON1.Visible = false;
            TR_MON2.Visible = false;
            TR_MON3.Visible = true;
            TR_MON4.Visible = false;
            FillDGRMonitoringDokter();
        }

        protected void BT_MON4_Click(object sender, EventArgs e)
        {
            TR_MON1.Visible = false;
            TR_MON2.Visible = false;
            TR_MON3.Visible = false;
            TR_MON4.Visible = true;
            FillDGR_MonitoringKontak();
        }

        protected void FillDGRMonitoringINFO()
        {
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_INFO " +
                                "'" + LB_NOSURAT.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MON_INFO.DataSource = dt;
            DGR_MON_INFO.DataBind();

            for (int i = 0; i < DGR_MON_INFO.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_MON_INFO.Items[i].FindControl("TXT_MON_INFO");
                txt.Text = DGR_MON_INFO.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void FillDGRMonitoringDiagnosa()
        {
            conn.QueryString = "select " +
                                "a.ICD_CODE,  " +
                                "c.DESCR  " +
                                "from CLAIM_SURAT_JAMINAN_MONITORING_ICD a  " +
                                "inner join CLAIM_SURAT_JAMINAN_MONITORING d on a.MONITORING_ID=d.ID " +
                                "inner join CLAIM_SURAT_JAMINAN_MASTER b on d.SURAT_JAMINAN_ID=b.ID  " +
                                "inner join PR_ICD c on a.ICD_CODE=c.CODE  " +
                                "where  " +
                                "b.NOMOR_SURAT_JAMINAN='" + LB_NOSURAT.Text + "'" +
                                "and d.SEQ=" + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MON_DIAG.DataSource = dt;
            DGR_MON_DIAG.DataBind();
        }

        protected void FillDGRMonitoringDokter()
        {
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_DOKTER " +
                                "'" + LB_NOSURAT.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MON_DOKTER.DataSource = dt;
            DGR_MON_DOKTER.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_KETERANGAN_DOKTER order by 1";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_MON_DOKTER.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_MON_DOKTER.Items[i].FindControl("DDL_MON_DOKTER");
                TextBox txt = (TextBox)DGR_MON_DOKTER.Items[i].FindControl("TXT_MON_DOKTER");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                try
                {
                    ddl.SelectedValue = DGR_MON_DOKTER.Items[i].Cells[1].Text;
                }
                catch { }
                txt.Text = DGR_MON_DOKTER.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void FillDGR_MonitoringKontak()
        {
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_KONTAK " +
                                "'" + LB_NOSURAT.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MON_KONTAK.DataSource = dt;
            DGR_MON_KONTAK.DataBind();

            for (int i = 0; i < DGR_MON_KONTAK.Items.Count; i++)
            {
                TextBox txtnama = (TextBox)DGR_MON_KONTAK.Items[i].FindControl("TXT_MON_KON_NAMA");
                TextBox txtnomor = (TextBox)DGR_MON_KONTAK.Items[i].FindControl("TXT_MON_KON_NOMOR");

                txtnama.Text = DGR_MON_KONTAK.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtnomor.Text = DGR_MON_KONTAK.Items[i].Cells[3].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_MON_INFO_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MON_INFO.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_MON_INFO.Items[i].FindControl("TXT_MON_INFO");
                try
                {
                    conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_INFO_SAVE " +
                                        "'" + LB_NOSURAT.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + DGR_MON_INFO.Items[i].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRMonitoringINFO();
        }


        protected void BT_MON_ICD_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_MON_ICD.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_ICD_SAVE " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + TXT_MON_ICD.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRMonitoringDiagnosa();
                TXT_MON_ICD.Text = "";
            }
            catch { }
        }


        protected void DGR_MON_DIAG_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_SURAT_JAMINAN_MONITORING_ICD " +
                                        "where " +
                                        "ICD_CODE='" + e.Item.Cells[0].Text + "' " +
                                        "and MONITORING_ID in " +
                                        "(select a.ID from CLAIM_SURAT_JAMINAN_MONITORING a " +
                                        "inner join CLAIM_SURAT_JAMINAN_MASTER b on a.SURAT_JAMINAN_ID=b.ID  " +
                                        "where b.NOMOR_SURAT_JAMINAN='" + LB_NOSURAT.Text + "' and a.SEQ=" + LB_SEQ.Text + ")";
                    conn.ExecuteNonQuery();
                    FillDGRMonitoringDiagnosa();
                }
                catch { }
            }
        }


        protected void BT_MON_DOKTER_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_DOKTER_ADD " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRMonitoringDokter();
            }
            catch { }
        }


        protected void DGR_MON_DOKTER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_MON_DOKTER");
            TextBox txt = (TextBox)e.Item.FindControl("TXT_MON_DOKTER");

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_SURAT_JAMINAN_MONITORING_DOKTER where ID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRMonitoringDokter();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "update CLAIM_SURAT_JAMINAN_MONITORING_DOKTER set " +
                                        "NAMA_DOKTER='" + txt.Text.Trim() + "', " +
                                        "KET_DOKTER_CODE='" + ddl.SelectedValue + "', " +
                                        "USERBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "USERDATE=GETDATE() " +
                                        "where ID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRMonitoringDokter();
                }
                catch { }
            }
        }


        protected void DGR_MON_KONTAK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtnama = (TextBox)e.Item.FindControl("TXT_MON_KON_NAMA");
            TextBox txtnomor = (TextBox)e.Item.FindControl("TXT_MON_KON_NOMOR");

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_MONITORING_KONTAK_SAVE " +
                                         "'" + LB_NOSURAT.Text + "'," +
                                         LB_SEQ.Text + "," +
                                         "'" + e.Item.Cells[0].Text + "'," +
                                         "'" + txtnama.Text.Replace("&nbsp;", "") + "'," +
                                         "'" + txtnomor.Text.Replace("&nbsp;", "") + "'," +
                                         "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'"; ;
                    conn.ExecuteNonQuery();
                    FillDGR_MonitoringKontak();
                }
                catch (System.Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }
    }
}
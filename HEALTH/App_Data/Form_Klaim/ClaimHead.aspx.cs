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
    public partial class ClaimHead : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();

                try
                {
                    LB_BATCHID.Text = Request.QueryString["BATCH_ID"].ToString();
                }
                catch { }
                try
                {
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                if (LB_CLAIMNO.Text.Trim() != "")
                {
                    LoadRecord(LB_CLAIMNO.Text.Trim());
                }
                else
                {
                    TD_BUTTONS.Visible = false;
                    InfoBatch(LB_BATCHID.Text.Trim());
                    if (DDL_PROV.SelectedValue != "")
                    {
                        SetPROVIDER(DDL_PROV.SelectedValue);
                    }
                    ShowRekening("null", "'" + LB_BATCHID.Text.Trim() + "'");
                }

                if (DDL_PR.SelectedValue == "P")
                {
                    BT_CARI2.Visible = false;
                    DDL_NOREK.Enabled = false;
                    TXT_ACCNO.Enabled = false;
                    TXT_ACCNAMA.Enabled = false;
                    DDL_ACCBANK.Enabled = false;
                }
            }


        }


        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TIPE_CLAIM";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select LTRIM(RTRIM(KODE_PROVIDER)),NAMA from PROVIDER_MASTER where LTRIM(ISNULL(NAMA,''))<>'' order by LTRIM(NAMA)";
            conn.ExecuteQuery();
            DDL_PROV.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROV.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select a.ID,DESCR = LEFT(b.COMPANY_NAME,50) collate database_default + ' - ' + a.POLICY_NO from POLICY a " +
                                "inner join COMPANY b on a.COMPANY_CODE=b.COMPANY_CODE collate database_default " +
                                "order by b.COMPANY_NAME";
            conn.ExecuteQuery();
            DDL_POLIS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select SEQ,DESCR from PARAM_TRACK where TIPE_CODE='CLMMASTER' order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRACK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by BANK";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_ACCBANK_RFD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            BT_MEMBERCLOSE.Attributes.Add("onclick", "document.getElementById('pnlMEMBER').style.display = 'none';");
            BT_PROVIDERCLOSE.Attributes.Add("onclick", "document.getElementById('pnlPROVIDER').style.display = 'none';");
        }

        protected void InfoBatch(string batch_id)
        {
            conn.QueryString = "select " +
                                "DOC_NO, " +
                                "TGL_DOC = CONVERT(varchar(20),TGL_DOC,103), " +
                                "PR, " +
                                "KODE_PROVIDER, " +
                                "POLICY_ID " +
                                "from V_CLM_CLAIM_MASTER_BATCH " +
                                "where BATCH_ID='" + batch_id + "'";
            conn.ExecuteQuery();

            LB_SM.Text = "<a href='ClaimReg.aspx?BATCH_ID=" + batch_id + "' target='content'><span style='color: #0000FF'>" + conn.GetFieldValue("DOC_NO").ToString() + "</span>";
            TXT_TGLSM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            try
            {
                if (Request.QueryString["BATCH_ID"].ToString() != "")
                    TXT_TGLCLAIM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            }
            catch { }
            try
            {
                DDL_PR.SelectedValue = conn.GetFieldValue("PR").ToString();
            }
            catch { }
            try
            {
                DDL_POLIS.SelectedValue = conn.GetFieldValue("POLICY_ID").ToString();
            }
            catch { }
            try
            {
                DDL_PROV.SelectedValue = conn.GetFieldValue("KODE_PROVIDER").ToString();
            }
            catch { }

            if (DDL_PR.SelectedValue == "P")
                TR_DISCOUNT.Visible = true;
        }

        protected void SetPROVIDER(string kodeprovider)
        {
            LB_KODE_PROVIDER.Text = kodeprovider;
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_INFO_PROVIDER '" + kodeprovider + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO_PROVIDER.DataSource = dt;
            DGR_INFO_PROVIDER.DataBind();
        }

        protected void ShowRekening(string claimno, string batchid)
        {
            conn.QueryString = "exec SP_CLM_CLAIM_MASTER_REKENING " + claimno + "," + batchid;
            conn.ExecuteQuery();
            TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            TXT_ACCNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
            try
            {
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
            }
            catch { }

            if (DDL_PR.SelectedValue == "P")
            {
                TR_REFUNDACC.Visible = true;

                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_REKENING_REFUND " + claimno;
                conn.ExecuteQuery();
                TXT_ACCNO_RFD.Text = conn.GetFieldValue("ACC_NO").ToString();
                TXT_ACCNAMA_RFD.Text = conn.GetFieldValue("ACC_NAMA").ToString();
                try
                {
                    DDL_ACCBANK_RFD.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
                }
                catch { }
            }
        }

        protected void ShowRekeningSource(string claimno)
        {
            TR_REK.Visible = true;

            conn.QueryString = "select " +
                                "a.TIPE " +
                                "from V_CLM_CLAIM_MASTER_REKENING_SOURCE a " +
                                "inner join CLAIM_MASTER b on a.CLAIM_NO=b.CLAIM_NO " +
                                "inner join CLAIM_MASTER_BATCH c on c.BATCH_ID=b.BATCH_ID and a.PR=c.PR " +
                                "where " +
                                "a.CLAIM_NO='" + claimno + "' " +
                                "order by a.SEQ";
            conn.ExecuteQuery();
            DDL_NOREK.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void SetREGNO(string regno)
        {
            LB_REGNO.Text = regno;
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_INFO_PESERTA '" + regno + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO_PESERTA.DataSource = dt;
            DGR_INFO_PESERTA.DataBind();
        }

        protected void CekTrack()
        {
            if (DDL_TRACK.SelectedValue == "3" || DDL_TRACK.SelectedValue == "4")
            {
                BT_CARI1.Visible = false;
                BT_CARI2.Visible = false;
                BT_SAVE.Visible = false;
                TR_REK.Visible = false;

                TR_PENDING.Visible = false;
                if (DDL_TRACK.SelectedValue == "4")
                    TR_REJECT.Visible = false;
            }
        }

        protected void LoadRecord(string claimno)
        {
            conn.QueryString = "select " +
                                "CLAIM_NO, " +
                                "BATCH_ID, " +
                                "LAST_TRACK, " +
                                "REGNO, " +
                                "KODE_PROVIDER, " +
                                "TIPE_CLAIM, " +
                                "TGL_KLAIM = CONVERT(varchar(20),TGL_KLAIM,103), " +
                                "TGL_RAWAT_DARI = CONVERT(varchar(20),TGL_RAWAT_DARI,103), " +
                                "TGL_RAWAT_SAMPAI = CONVERT(varchar(20),TGL_RAWAT_SAMPAI,103) " +
                                "from V_CLM_CLAIM_MASTER " +
                                "where " +
                                "CLAIM_NO='" + claimno + "'";
            conn.ExecuteQuery();

            LB_BATCHID.Text = conn.GetFieldValue("BATCH_ID").ToString();
            LB_CLAIMNO.Text = conn.GetFieldValue("CLAIM_NO").ToString();
            LB_REGNO.Text = conn.GetFieldValue("REGNO").ToString();
            LB_KODE_PROVIDER.Text = conn.GetFieldValue("KODE_PROVIDER").ToString();
            TXT_TGLCLAIM.Text = conn.GetFieldValue("TGL_KLAIM").ToString();
            TXT_TGLRAWATDARI.Text = conn.GetFieldValue("TGL_RAWAT_DARI").ToString();
            TXT_TGLRAWATSAMPAI.Text = conn.GetFieldValue("TGL_RAWAT_SAMPAI").ToString();

            try
            {
                DDL_TRACK.SelectedValue = conn.GetFieldValue("LAST_TRACK").ToString();
            }
            catch { }
            try
            {
                DDL_TIPE.SelectedValue = conn.GetFieldValue("TIPE_CLAIM").ToString();
            }
            catch { }

            ShowRekeningSource(claimno);
            InfoBatch(LB_BATCHID.Text.Trim());
            SetREGNO(LB_REGNO.Text);
            SetPROVIDER(LB_KODE_PROVIDER.Text);
            ShowRekening("'" + claimno + "'", "null");
            CekTrack();
            ShowDiagnosa();
            CheckTPExist();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimHeader.aspx?BATCH_ID=" + LB_BATCHID.Text);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            Save();
        }


        protected void Save()
        {
            LB_ERROR.Text = "";

            string claimno = "null";
            string kodeprovider = "null";
            if (LB_CLAIMNO.Text != "")
                claimno = "'" + LB_CLAIMNO.Text + "'";
            if (LB_KODE_PROVIDER.Text != "")
                kodeprovider = "'" + LB_KODE_PROVIDER.Text + "'";

            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT_VALIDATION " +
                                    claimno + "," +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + LB_REGNO.Text + "'," +
                                    kodeprovider + "," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLCLAIM.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATDARI.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATSAMPAI.Text.Trim(), "d/M/yyyy") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString() != "")
                {
                    LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();
                    LB_ERROR.ForeColor = System.Drawing.Color.Red;
                    return;
                }

            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                return;
            }


            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT " +
                                    claimno + "," +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + LB_REGNO.Text + "'," +
                                    kodeprovider + "," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLCLAIM.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATDARI.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATSAMPAI.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                LB_CLAIMNO.Text = conn.GetFieldValue("CLAIM_NO").ToString();


            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                return;
            }

            ShowRekeningSource(LB_CLAIMNO.Text);
            //CopyNOREK();
            SaveNOREK();
            SaveNOREKREFUND();
            TD_BUTTONS.Visible = true;
            ShowDiagnosa();
        }

        protected void CopyNOREK()
        {
            if (DDL_NOREK.Items.Count > 0)
            {
                conn.QueryString = "select " +
                                    "a.ACC_NO, " +
                                    "a.ACC_NAMA, " +
                                    "a.ACC_BANK " +
                                    "from V_CLM_CLAIM_MASTER_REKENING_SOURCE a " +
                                    "where " +
                                    "a.CLAIM_NO='" + LB_CLAIMNO.Text + "' " +
                                    "and a.TIPE='" + DDL_NOREK.SelectedValue + "'";
                conn.ExecuteQuery();
                TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
                TXT_ACCNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
                try
                {
                    DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
                }
                catch { }
            }

            SaveNOREK();
        }

        protected void SaveNOREK()
        {
            if (TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAMA.Text.Trim() == "" || DDL_ACCBANK.SelectedValue == "")
                return;

            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_REKENING_UPSERT " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                    "'" + DDL_ACCBANK.SelectedValue + "'";
                conn.ExecuteQuery();
            }
            catch { }
        }

        protected void SaveNOREKREFUND()
        {
            if (TXT_ACCNO_RFD.Text.Trim() == "" || TXT_ACCNAMA_RFD.Text.Trim() == "" || DDL_ACCBANK_RFD.SelectedValue == "")
                return;

            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_REKENING_REFUND_UPSERT " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + TXT_ACCNO_RFD.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAMA_RFD.Text.Trim() + "'," +
                                    "'" + DDL_ACCBANK_RFD.SelectedValue + "'";
                conn.ExecuteQuery();
            }
            catch { }
        }

        protected void ShowDiagnosa()
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimDiagnosa.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            ShowDiagnosa();
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimBenTrx.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT8_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimSurgery.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT0_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/AdditionalInfo.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT9_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Remark.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_CLMNO", LB_CLAIMNO.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '" + URL + "';</script>");
        }

        protected void BT_ADD_REASON_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Klaim/ClaimAddReason.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Track.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRevision.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT_HP_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo.aspx?REGNO=" + LB_REGNO.Text + "';</script>");            

            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo_CLAIMHISTORY.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");

        }

        protected void BT_TC_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
        }

        protected void BT_LIMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo_CLAIMBENEFITLIMIT.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
        }

        protected void BT_DISCOUNT_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimDiscount.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            if (!CheckTP("2"))
                return;

            BT6.Attributes.Remove("onclick");
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=2';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            if (!CheckTP("3"))
                return;

            BT7.Attributes.Remove("onclick");
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=3';</script>");
        }

        protected void CheckTPExist()
        {
            conn.QueryString = "select CLAIM_NO from CLAIM_TP where CLAIM_NO='" + LB_CLAIMNO.Text + "' and TP='2'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                BT6.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PENDING ?')){return false;};");

            conn.QueryString = "select CLAIM_NO from CLAIM_TP where CLAIM_NO='" + LB_CLAIMNO.Text + "' and TP='3'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                BT7.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TOLAK ?')){return false;};");
        }

        protected bool CheckTP(string TP)
        {
            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_TP " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + TP + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                string result = conn.GetFieldValue("RESULT").ToString();

                if (result == "1")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        protected void DGR_CARI_PESERTA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                SetREGNO(e.Item.Cells[1].Text);
                if (DDL_PR.SelectedValue == "R")
                {
                    Save();
                    CopyNOREK();
                    if (LB_CLAIMNO.Text != "")
                    {
                        ShowDiagnosa();
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimheader.location.href = 'ClaimHeader.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
                    }
                }
            }
        }

        protected void DGR_CARI_PESERTA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PESERTA.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariPeserta();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlMEMBER').style.display = 'block';", true);
        }

        protected void DGR_CARI_PROVIDER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                SetPROVIDER(e.Item.Cells[1].Text);
                Save();
            }
        }

        protected void DGR_CARI_PROVIDER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariProvider();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlPROVIDER').style.display = 'block';", true);
        }

        protected void FillDGRCariPeserta()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.NAMA, " +
                                "c.COMPANY_NAME " +
                                "from PESERTA_MASTER a " +
                                "inner join BRANCH b on a.BRANCH_CODE=b.BRANCH_CODE " +
                                "inner join COMPANY c on b.COMPANY_CODE=c.COMPANY_CODE " +
                                "inner join POLICY d on b.COMPANY_CODE=d.COMPANY_CODE " +
                                "where " +
                                "a.STAT=a.STAT " +
                                "and a.REGNO like '%" + TXT_CARI_REGNO.Text.Trim() + "%' " +
                                "and a.NAMA like '%" + TXT_CARI_PESERTA.Text.Trim() + "%' " +
                                "and c.COMPANY_NAME like '%" + TXT_CARI_COMPANY.Text.Trim() + "%' " +
                                "and d.POLICY_NO like '%" + TXT_CARI_NOPOL.Text.Trim() + "%' " +
                                "order by 2";

            if (DDL_POLIS.SelectedValue != "")
            {
                conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.NAMA, " +
                                "c.COMPANY_NAME " +
                                "from PESERTA_MASTER a " +
                                "inner join BRANCH b on a.BRANCH_CODE=b.BRANCH_CODE " +
                                "inner join COMPANY c on b.COMPANY_CODE=c.COMPANY_CODE " +
                                "inner join POLICY d on c.COMPANY_CODE=d.COMPANY_CODE " +
                                "where " +
                                "a.STAT=a.STAT " +
                                "and a.REGNO like '%" + TXT_CARI_REGNO.Text.Trim() + "%' " +
                                "and a.NAMA like '%" + TXT_CARI_PESERTA.Text.Trim() + "%' " +
                                "and d.ID = '" + DDL_POLIS.SelectedValue + "' " +
                                "order by 2";
            }

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CARI_PESERTA.DataSource = dt;
            DGR_CARI_PESERTA.DataBind();

            DGR_CARI_PESERTA.Visible = true;
        }

        protected void FillDGRCariProvider()
        {
            conn.QueryString = "select " +
                                "KODE_PROVIDER, " +
                                "NAMA = UPPER(NAMA) " +
                                "from PROVIDER_MASTER " +
                                "where " +
                                "STAT='" + DDL_STAT_PROVIDER.SelectedValue + "' " +
                                "and NAMA like '%" + TXT_NAMA_PROVIDER.Text.Trim() + "%' " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CARI_PROVIDER.DataSource = dt;
            DGR_CARI_PROVIDER.DataBind();
            
        }

        protected void BT_NOREK_Click(object sender, EventArgs e)
        {
            CopyNOREK();
        }

        protected void BT_CARI1_Click(object sender, EventArgs e)
        {
            TXT_CARI_PESERTA.Text = "";
            TXT_CARI_COMPANY.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlMEMBER').style.display = 'block';", true);
        }

        protected void BT_CARI2_Click(object sender, EventArgs e)
        {
            TXT_NAMA_PROVIDER.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlPROVIDER').style.display = 'block';", true);
        }

        protected void BT_CARI_REGNO_Click(object sender, EventArgs e)
        {
            DGR_CARI_PESERTA.CurrentPageIndex = 0;
            FillDGRCariPeserta();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlMEMBER').style.display = 'block';", true);
        }

        protected void BT_CARI_PROVIDER_Click(object sender, EventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = 0;
            FillDGRCariProvider();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlPROVIDER').style.display = 'block';", true);
        }
    }
}
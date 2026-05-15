using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Globalization;
using System.Data.SqlClient;
using System.Drawing;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string username = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                TXT_TGLCLAIM.Attributes.Add("readonly", "readonly");
                TXT_TGLRAWATDARI.Attributes.Add("readonly", "readonly");
                TXT_TGLRAWATSAMPAI.Attributes.Add("readonly", "readonly");

                string batchId = Request.QueryString["BATCH_ID"] ?? ""; 
                string claimNo = Request.QueryString["CLAIM_NO"] ?? "";

                LB_BATCHID.Text = batchId;
                LB_CLAIMNO.Text = claimNo;

                if (LB_CLAIMNO.Text.Trim() != "")
                {
                    LoadRecord(LB_CLAIMNO.Text.Trim());
                    CheckCP(claimNo);
                    //ANDEZ ADMEDIKA

                    conn.QueryString = "SELECT CREATEBY = RIGHT(CREATEBY, 4) FROM CLAIM_MASTER WHERE CLAIM_NO = '" + LB_CLAIMNO.Text.Trim() + "'";
                    conn.ExecuteQuery();

                    if (conn.GetFieldValue("CREATEBY").ToString() == "_api")
                    {
                        BT_API.Visible = true;
                    }

                    //ANDEZ ADMEDIKA
                }
                else
                {
                    TD_BUTTONS.Visible = false;
                    InfoBatch(LB_BATCHID.Text.Trim());
                    if (DDL_PROV.SelectedValue != "")
                    {
                        SetPROVIDER(DDL_PROV.SelectedValue);
                    }
                    //ShowRekening("null", "'" + LB_BATCHID.Text.Trim() + "'");
                    ShowRekeningSource2();
                }

                if (DDL_PR.SelectedValue == "P")
                {
                    BT_CARI2.Visible = false;
                    DDL_NOREK.Enabled = false;
                    TXT_ACCNO.Enabled = false;
                    TXT_ACCNAMA.Enabled = false;
                    DDL_ACCBANK.Enabled = false;
                }
                username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());
            }


        }


        protected void Setup()
        {
            //BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");

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

        }

        protected void CheckCP(string nomor)
        {
            string connection = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            string queryString = "exec SP_GET_CP_BY_NO_SURAT @Nomor"; // Use parameterized queries for security

            using (SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Nomor", nomor); // Safe from SQL injection

                DataSet CPData = new DataSet();
                adapter.Fill(CPData, "CPData");

                if (CPData.Tables["CPData"].Rows.Count == 0)
                {
                    CPStatus.ForeColor = Color.Red;
                    CPStatus.Text = "CP Tidak Tersedia";
                    return; // No need to continue
                }

                bool nonCP = true;
                string cpStatus = "Loading CP Status";

                foreach (DataRow row in CPData.Tables["CPData"].Rows)
                {
                    cpStatus = row["STATUS_CP"].ToString();

                    if (cpStatus == "CP")
                    {
                        nonCP = false;
                        break;
                    }
                }

                // Display result based on conditions
                if (nonCP)
                {
                    CPStatus.ForeColor = Color.Red;
                    CPStatus.Text = cpStatus;
                }
                else
                {
                    CPStatus.ForeColor = Color.Green;
                    CPStatus.Text = cpStatus;
                }
            }
        }


        protected void InfoBatch(string batch_id)
        {
            //-- remark GAS
            //conn.QueryString = "select " +
            //                    "DOC_NO, " +
            //                    "TGL_DOC = CONVERT(varchar(20),TGL_DOC,103), " +
            //                    "PR, " +
            //                    "KODE_PROVIDER, " +
            //                    "POLICY_ID " +
            //                    "from V_CLM_CLAIM_MASTER_BATCH " +
            //                    "where BATCH_ID='" + batch_id + "'";

            // ADD GAS
            conn.QueryString = "select " +
                                "DOC_NO, " +
                                //"TGL_DOC = FORMAT(TGL_DOC, 'yyyy-MM-dd'), " +
                                "TGL_DOC = FORMAT(TGL_DOC, 'dd-MM-yyyy'), " +
                                "PR, " +
                                "KODE_PROVIDER, " +
                                "POLICY_ID " +
                                "from V_CLM_CLAIM_MASTER_BATCH " +
                                "where BATCH_ID='" + batch_id + "'";
            conn.ExecuteQuery();

            LB_SM.Text = "<a href='ClaimReg.aspx?BATCH_ID=" + batch_id + "' target='content'><span style='color: #0000FF'>" + conn.GetFieldValue("DOC_NO").ToString() + "</span>";
            TXT_TGLSM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            //try
            //{
            //    if (batch_id != "")
            //        TXT_TGLCLAIM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            //}
            //catch { }
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

        protected void ShowRekeningSource2()
        {
            if (!string.IsNullOrEmpty(Session["sessionSaveClaimReg"] as string))
            {
                ShowRekening("'" + LB_CLAIMNO.Text.Trim() + "'", "null");
                Session["sessionSaveClaimReg"] = null;
            }
            else
            {
                string owner = "";
                if (DDL_PR.SelectedValue == "P")
                {
                    owner = DDL_PROV.SelectedValue;
                    if (CekDataTempClaimBatch(LB_CLAIMNO.Text.Trim()) == true)
                    {
                        conn.QueryString = "USP_GET_REKENING_SOURCE '" + DDL_PR.SelectedValue + "','" + owner + "'";
                        conn.ExecuteQuery();
                        DataTable dt;
                        dt = new DataTable();
                        dt = conn.GetDataTable().Copy();

                        if (dt.Rows.Count != 0)
                        {
                            DDL_NOREK.DataSource = dt;
                            DDL_NOREK.DataTextField = "TIPE";
                            DDL_NOREK.DataValueField = "ACC_NO";
                            TXT_ACCNAMA.Text = dt.Rows[0][5].ToString();
                            TXT_ACCNO.Text = dt.Rows[0][4].ToString();
                            DDL_ACCBANK.SelectedValue = dt.Rows[0][6].ToString();
                            DDL_NOREK.DataBind();
                        }
                        else
                        {
                            DDL_NOREK.Items.Clear();
                        }
                    }
                    else
                    {
                        ShowRekening("'" + LB_CLAIMNO.Text.Trim() + "'", "null");
                    }
                }
                else
                {
                    owner = DDL_POLIS.SelectedValue;
                    ShowRekening("'" + LB_CLAIMNO.Text.Trim() + "'", "null");
                }     
            }
        }

        

        private bool CekDataTempClaimBatch(string claimNo) 
        {
            bool retValue = false;
            try
            {
                conn.QueryString = "select BATCH_ID from V_CLM_CLAIM_MASTER where CLAIM_NO = '" + claimNo + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                string batchID = dt.Rows[0][0].ToString();


                conn.QueryString = "select ACC_NO from TEMP_CLAIM_MASTER_BATCH WHERE BATCH_ID = '" + batchID + "'";
                conn.ExecuteQuery();
                DataTable dtAccNoOld;
                dtAccNoOld = new DataTable();
                dtAccNoOld = conn.GetDataTable().Copy();
                string accNoOLD = dtAccNoOld.Rows[0][0].ToString();

                conn.QueryString = "SELECT ACC_NO FROM V_CLM_CLAIM_MASTER_BATCH WHERE BATCH_ID =  '" + batchID + "'";
                conn.ExecuteQuery();
                DataTable dtAccNoNew;
                dtAccNoNew = new DataTable();
                dtAccNoNew = conn.GetDataTable().Copy();
                string accNoNEW = dtAccNoNew.Rows[0][0].ToString();

                //if (accNoNEW == accNoOLD) 
                //{
                //    retValue = true;
                //}
                //else if (accNoNEW != accNoOLD) 
                //{
                //    retValue = false;
                //}

                if (accNoNEW == accNoOLD)
                {
                    retValue = false;
                }
                else if (accNoNEW != accNoOLD)
                {
                    retValue = true;
                }
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
                
            }

            return retValue;
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
            //-- remark GAS 
            //conn.QueryString = "select " +
            //                    "CLAIM_NO, " +
            //                    "BATCH_ID, " +
            //                    "LAST_TRACK, " +
            //                    "REGNO, " +
            //                    "KODE_PROVIDER, " +
            //                    "TIPE_CLAIM, " +
            //                    "TGL_KLAIM = CONVERT(varchar(20),TGL_KLAIM,103), " +
            //                    "TGL_RAWAT_DARI = CONVERT(varchar(20),TGL_RAWAT_DARI,103), " +
            //                    "TGL_RAWAT_SAMPAI = CONVERT(varchar(20),TGL_RAWAT_SAMPAI,103) " +
            //                    "from V_CLM_CLAIM_MASTER " +
            //                    "where " +
            //                    "CLAIM_NO='" + claimno + "'";

            // ADD GAS
            conn.QueryString = "select " +
                                "CLAIM_NO, " +
                                "BATCH_ID, " +
                                "LAST_TRACK, " +
                                "REGNO, " +
                                "KODE_PROVIDER, " +
                                "TIPE_CLAIM, " +
                                "TGL_KLAIM = FORMAT(TGL_KLAIM, 'dd-MM-yyyy'), " +
                                "TGL_RAWAT_DARI = FORMAT(TGL_RAWAT_DARI, 'dd-MM-yyyy'), " +
                                "TGL_RAWAT_SAMPAI = FORMAT(TGL_RAWAT_SAMPAI, 'dd-MM-yyyy') " +
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

            if (string.IsNullOrEmpty(LB_KODE_PROVIDER.Text))
            {
                BT_CP.Visible = false;
                BT_TRF.Visible = false;
            }

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
            //ShowRekening("'" + claimno + "'", "null");
            ShowRekeningSource2();
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

            if (TXT_TGLRAWATDARI.Text == "" || TXT_TGLRAWATSAMPAI.Text == "" || TXT_TGLCLAIM.Text == "")
            {
                lblError2.Text = "Tanggal Rawat/ Tanggal Klaim Tidak Boleh Kosong!";
            }
            else
            {
                DateTime dateStart = DateTime.Parse(TXT_TGLRAWATDARI.Text, null);
                DateTime dateEnd = DateTime.Parse(TXT_TGLRAWATSAMPAI.Text, null);
                DateTime dateKlaim = DateTime.Parse(TXT_TGLCLAIM.Text, null);

                if (dateStart <= dateEnd)
                {
                    if (dateStart <= dateKlaim && dateEnd <= dateKlaim)
                    {
                        try
                        {
                            // remark GAS
                            //try
                            //{
                            //    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT_VALIDATION " +
                            //                        claimno + "," +
                            //                        "'" + LB_BATCHID.Text + "'," +
                            //                        "'" + LB_REGNO.Text + "'," +
                            //                        kodeprovider + "," +
                            //                        "'" + DDL_TIPE.SelectedValue + "'," +
                            //                        "'" + GlobalUse.GlobalDateFormat(TXT_TGLCLAIM.Text.Trim(), "d/M/yyyy") + "'," +
                            //                        "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATDARI.Text.Trim(), "d/M/yyyy") + "'," +
                            //                        "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATSAMPAI.Text.Trim(), "d/M/yyyy") + "'";
                            //    conn.ExecuteQuery();

                            //    if (conn.GetFieldValue("RESULT").ToString() != "")
                            //    {
                            //        LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();
                            //        LB_ERROR.ForeColor = System.Drawing.Color.Red;
                            //        return;
                            //    }

                            //}
                            //catch (System.Exception ex)
                            //{
                            //    LB_ERROR.Text = ex.Message;
                            //    LB_ERROR.ForeColor = System.Drawing.Color.Red;
                            //    return;
                            //}


                            try
                            {
                                // remark GAS
                                //conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT " +
                                //                    claimno + "," +
                                //                    "'" + LB_BATCHID.Text + "'," +
                                //                    "'" + LB_REGNO.Text + "'," +
                                //                    kodeprovider + "," +
                                //                    "'" + DDL_TIPE.SelectedValue + "'," +
                                //                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLCLAIM.Text.Trim(), "d/M/yyyy") + "'," +
                                //                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATDARI.Text.Trim(), "d/M/yyyy") + "'," +
                                //                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLRAWATSAMPAI.Text.Trim(), "d/M/yyyy") + "'," +
                                //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                                // ADD GAS
                                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT " +
                                                    claimno + "," +
                                                    "'" + LB_BATCHID.Text + "'," +
                                                    "'" + LB_REGNO.Text + "'," +
                                                    kodeprovider + "," +
                                                    "'" + DDL_TIPE.SelectedValue + "','" + dateKlaim.ToString("MM/dd/yyyy") + "','" + dateStart.ToString("MM/dd/yyyy") + "','" + dateEnd.ToString("MM/dd/yyyy") + "','" 
                                                    + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                
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
                            DisplayListBenefitButton(true);
                            string warning = ShowBlacklist(LB_REGNO.Text);
                            if (!string.IsNullOrEmpty(warning))
                            {
                                conn.QueryString = "EXEC SP_SEND_EMAIL_NASABAH_ASKES_BERESIKO_TINGGI '" + LB_REGNO.Text + "-" + username + "'";
                                conn.ExecuteQuery();
                            }
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERROR.Text = ex.Message;
                            LB_ERROR.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                    else
                    {
                        lblError2.Text = "Tanggal Rawat tidak boleh lebih besar dari tanggal klaim!";
                        DisplayListBenefitButton(false);
                    }
                }
                else
                {
                    lblError2.Text = "Tanggal mulai rawat tidak boleh lebih besar dari tanggal selesai rawat!";
                    DisplayListBenefitButton(false);
                }
            }       
        }

        private void DisplayListBenefitButton(bool value) 
        {
            BT3.Visible = value;
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
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimDiagnosa.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            //ShowDiagnosa();
            ShowPopUp(((Button)sender).Text, "ClaimDiagnosa.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimBenTrx.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimBenTrx.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT8_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimSurgery.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimSurgery.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT0_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/AdditionalInfo.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Tools/AdditionalInfo.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text);
        }

        protected void BT9_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Remark.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Tools/Remark.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text);
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_CLMNO", LB_CLAIMNO.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '" + URL + "';</script>");
            ShowPopUp(((Button)sender).Text, URL);
        }

        protected void BT_ADD_REASON_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Klaim/ClaimAddReason.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Klaim/ClaimAddReason.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Track.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Tools/Track.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text);
        }
        //ANDEZ ADMEDIKA
        protected void BT_API_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Tools/Track.aspx?tipe=CLMMASTER&owner=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Tools/TrackApi.aspx?tipe=APIADM&owner=" + LB_CLAIMNO.Text);
        }
        //ANDEZ ADMEDIKA

        protected void BT_SURAT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select NO_SURAT from CLAIM_SURAT_JAMINAN_RJ where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            ShowPopUp(((Button)sender).Text, "PenjaminanSurat.aspx?NOSURAT=" + conn.GetFieldValue("NO_SURAT").ToString());
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Account Bank tidak boleh kosong')", true);
        }
        protected void BT5_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRevision.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimRevision.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT_HP_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo.aspx?REGNO=" + LB_REGNO.Text + "';</script>");            
            
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo_CLAIMHISTORY.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Member/PesertaInfo_CLAIMHISTORY.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString());
            
        }

        protected void BT_TC_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString());
        }

        protected void BT_FINANCE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            ShowPopUp(((Button)sender).Text, "../Form_Klien/Polis_Period_Finance.aspx?PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString());
        }

        protected void BT_LIMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select POLICY_PERIOD_ID from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = '../Form_Member/PesertaInfo_CLAIMBENEFITLIMIT.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString() + "';</script>");
            ShowPopUp(((Button)sender).Text, "../Form_Member/PesertaInfo_CLAIMBENEFITLIMIT.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + conn.GetFieldValue("POLICY_PERIOD_ID").ToString());
        }

        protected void BT_DISCOUNT_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimDiscount.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimDiscount.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void BT_APL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from SECURITY.dbo.REPORT_LIST where APP_ID = 'HO' and CODE = 377";
            conn.ExecuteQuery(1000);
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + conn.GetFieldValue("URL").ToString() + "&rc:Parameters=False&CLAIMNO=" + LB_CLAIMNO.Text + "','APLIKASI KLAIM KESEHATAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            //ShowPopUp(((Button)sender).Text, "ClaimDiscount.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);

            //conn.QueryString = "select URL from SECURITY.dbo.REPORT_LIST where APP_ID = 'GL' and CODE = 63";
            //conn.ExecuteQuery();
            //string URL = conn.GetFieldValue("URL").ToString();
            //string URL1;

            //conn.QueryString = "select REGNO from GLIFE.dbo.V_APPLICATION_MASTER where REGNO in ('20220105132509010','20220105132531537','20220113180425820') order by REGNO";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    URL1 = ""; 
            //    URL1 = URL + "&rs:Format=PDF&REGNO=" + conn.GetFieldValue(i, 0).ToString();
            //    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + URL1 + "','APLIKASI KLAIM KESEHATAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            //    //return URL1;

            //    Response.Write("<script>console.log('" + URL1 + "');</script>");
            //}
        
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            if (!CheckTP("2"))
                return;

            BT6.Attributes.Remove("onclick");
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=2';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=2");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            if (!CheckTP("3"))
                return;

            BT7.Attributes.Remove("onclick");
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbody.location.href = 'ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=3';</script>");
            ShowPopUp(((Button)sender).Text, "ClaimRejectPending.aspx?CLAIM_NO=" + LB_CLAIMNO.Text + "&MODE=3");
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
                BT_CARI1.Visible = true;
                TBL_CARI_PESERTA.Visible = false;
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
                string warning = ShowBlacklist(e.Item.Cells[1].Text);
                if (warning != "")
                {
                    DV_WARNING.Visible = true;
                    LB_WARNING.Text = warning;
                }
            }
        }

        protected void DGR_CARI_PESERTA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PESERTA.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariPeserta();
        }

        protected void DGR_CARI_PROVIDER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                BT_CARI2.Visible = true;
                TBL_PROVIDER.Visible = false;
                SetPROVIDER(e.Item.Cells[1].Text);
                Save();
            }
        }

        protected void DGR_CARI_PROVIDER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariProvider();
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

            DGR_CARI_PROVIDER.Visible = true;
        }

        protected void BT_NOREK_Click(object sender, EventArgs e)
        {
            CopyNOREK();
        }

        protected void BT_CARI1_Click(object sender, EventArgs e)
        {
            BT_CARI1.Visible = false;
            TBL_CARI_PESERTA.Visible = true;

            TXT_CARI_PESERTA.Text = "";
            TXT_CARI_COMPANY.Text = "";
            DGR_CARI_PESERTA.Visible = false;
        }

        protected void BT_CARI2_Click(object sender, EventArgs e)
        {
            BT_CARI2.Visible = false;
            TBL_PROVIDER.Visible = true;

            TXT_NAMA_PROVIDER.Text = "";
            DGR_CARI_PROVIDER.Visible = false;
        }

        protected void BT_CARI_REGNO_Click(object sender, EventArgs e)
        {
            DGR_CARI_PESERTA.CurrentPageIndex = 0;
            FillDGRCariPeserta();
        }

        protected void BT_CARI_PROVIDER_Click(object sender, EventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = 0;
            FillDGRCariProvider();
        }

        protected void ShowPopUp(string title, string url)
        {
            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", url);
        }

        protected void BT_TRF_Click(object sender, EventArgs e)
        {
            string kode = LB_KODE_PROVIDER.Text;
            ShowPopUp(((Button)sender).Text, "BukuTarif.aspx?PROVIDER=" + kode);
        }

        protected void BT_CP_LIST(object sender, EventArgs e)
        {
            string kode = LB_KODE_PROVIDER.Text;
            ShowPopUp(((Button)sender).Text, "CPList.aspx?PROVIDER=" + kode);
        }

        protected void BT_CP_Click(object sender, EventArgs e)
        {
            string kode = LB_CLAIMNO.Text;
            ShowPopUp(((Button)sender).Text, "CPbyNoSurat.aspx?SURAT=" + kode);
        }

        protected string ShowBlacklist(string regno)
        {
            string warning = "";

            conn.QueryString = "exec SP_MASTER_MEMBER_BLACKLIST '" + regno + "'";
            conn.ExecuteQuery();

            string MAIN_INSURED_BLACKLISTED = conn.GetFieldValue("MAIN_INSURED_BLACKLISTED").ToString();
            string POLICY_HOLDER_BLACKLISTED = conn.GetFieldValue("POLICY_HOLDER_BLACKLISTED").ToString();
            string FLAG = conn.GetFieldValue("FLAG").ToString();
            string SOURCE = conn.GetFieldValue("SOURCE").ToString();

            if (!string.IsNullOrEmpty(MAIN_INSURED_BLACKLISTED) && !string.IsNullOrWhiteSpace(MAIN_INSURED_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning += MAIN_INSURED_BLACKLISTED;
                }
                else
                {
                    warning += MAIN_INSURED_BLACKLISTED + "<br/>";
                }
            }

            if (!string.IsNullOrEmpty(POLICY_HOLDER_BLACKLISTED) && !string.IsNullOrWhiteSpace(POLICY_HOLDER_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning += POLICY_HOLDER_BLACKLISTED;
                }
                else
                {
                    warning += POLICY_HOLDER_BLACKLISTED + "<br/>";
                }
            }

            return warning;
        }
    }
}
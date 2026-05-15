using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Drawing;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimReg : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        string _query = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("sweet"))
            {
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "sweet",
                    "<script src='https://cdn.jsdelivr.net/npm/sweetalert2@11'></script>",
                    false
                );
            }

            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (arg == "do" && target != null && target.Contains("BT_DEL"))
            {
                HandleDelete(target);
            }


            if (!IsPostBack)
            {
                TXT_TGLSM.Attributes.Add("readonly", "readonly");
                LB_BATCHID.Text = Request.QueryString["BATCH_ID"].ToString();
                Setup();
                if (LB_BATCHID.Text.Trim() != "")
                {
                    LoadRecord(LB_BATCHID.Text.Trim());
                    //InsertTempClaimBatch(LB_BATCHID.Text.Trim());
                }
                CekProviderReimb();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' and ROLE_CODE in (select code from PR_USER_ACCESS_POLICYNO)";
            conn.ExecuteQuery(120);
            string ROLECODE = conn.GetFieldValue("ROLE_CODE").ToString();
            if (String.IsNullOrEmpty(ROLECODE))
            {
                conn.QueryString = "select CODE, DESCR from PR_BENEFIT_PROVIDER_TYPE";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            else
            {
                conn.QueryString = "select CODE, DESCR from PR_BENEFIT_PROVIDER_TYPE order by code DESC";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_PR.Enabled = false;
            }

            conn.QueryString = "select LTRIM(RTRIM(KODE_PROVIDER)), NAMA from PROVIDER_MASTER where LTRIM(ISNULL(NAMA,''))<>'' order by LTRIM(NAMA)";
            conn.ExecuteQuery();
            DDL_PROV.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROV.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (String.IsNullOrEmpty(ROLECODE))
            {
                conn.QueryString = "select a.ID,DESCR = LEFT(b.COMPANY_NAME,50) + ' - ' + a.POLICY_NO from POLICY a inner join COMPANY b on a.COMPANY_CODE=b.COMPANY_CODE order by b.COMPANY_NAME";
            }
            else
            {
                conn.QueryString = "select a.ID,DESCR = LEFT(b.COMPANY_NAME,50) + ' - ' + a.POLICY_NO from POLICY a inner join COMPANY b on a.COMPANY_CODE=b.COMPANY_CODE " +
                                   "where a.POLICY_NO in (select [DESCR] from [PR_USER_ACCESS_POLICYNO] where CODE = '" + ROLECODE + "') " +
                                   "order by b.COMPANY_NAME";
            }
            conn.ExecuteQuery();
            DDL_POLIS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by BANK";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_DOC_SOURCE order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DOC_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            BT_CARIPROVIDER.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode1.aspx?field1=KODE_PROVIDER&field2=NAMA&field3=ACC_NO&tablename=V_PROVIDER&target=DDL_PROV&parent=0&callback=DDL_PROV.onchange()','PROVIDER','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
            BT_CARIPOLIS.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode1.aspx?field1=ID&field2=COMPANY_NAME&tablename=V_POLICY&target=DDL_POLIS&parent=0&callback=DDL_POLIS.onchange()','POLIS','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void LoadRecord(string batch_id)
        {
            // remark GAS
            //conn.QueryString = "select " +
            //                    "DOC_NO, " +
            //                    "DOC_SOURCE, " +
            //                    "TGL_DOC = CONVERT(varchar(20),TGL_DOC,103), " +
            //                    "PR, " +
            //                    "KODE_PROVIDER, " +
            //                    "POLICY_ID, " +
            //                    "ACC_NO, " +
            //                    "ACC_NAMA, " +
            //                    "ACC_BANK " +
            //                    "from V_CLM_CLAIM_MASTER_BATCH " +
            //                    "where BATCH_ID='" + batch_id + "'";
            //conn.ExecuteQuery(120);

            //-- ADD GAS
            conn.QueryString = "select " +
                                "DOC_NO, " +
                                "DOC_SOURCE, " +
                                //"TGL_DOC = CONVERT(varchar(20),TGL_DOC,103), " +
                                "TGL_DOC = FORMAT(TGL_DOC, 'dd-MM-yyyy'), " +
                                "PR, " +
                                "KODE_PROVIDER, " +
                                "POLICY_ID, " +
                                "ACC_NO, " +
                                "ACC_NAMA, " +
                                "ACC_BANK " +
                                "from V_CLM_CLAIM_MASTER_BATCH " +
                                "where BATCH_ID='" + batch_id + "'";
            conn.ExecuteQuery(12000);


            TXT_SM.Text = conn.GetFieldValue("DOC_NO").ToString();
            TXT_TGLSM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            //TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            //TXT_ACCNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
            DDL_DOC_SOURCE.SelectedValue = conn.GetFieldValue("DOC_SOURCE");

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
            //try
            //{
            //    DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
            //}
            //catch { }

            ShowRekeningSource();
            //string batchid = InsertMasterBatchUpsert();

            TBL_ARSIP.Visible = true;

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], "CLMBATCH", LB_BATCHID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            I1.Attributes.Add("src", URL);
            FillDGR();
        }

        public bool CekUserDelete(string CODE)
        {
            var isUser = false;

            conn.QueryString = "select count(*) as DATA from SECURITY.dbo.M_USERS WHERE CODE = '" + CODE + "' AND ROLE_CODE = 7 ";
            conn.ExecuteQuery(120);
            int ROLECODE = int.Parse(conn.GetFieldValue("DATA"));
            //ROLECODE = 1; //Buat testing doang.
            if (ROLECODE == 1)
            {
                isUser = true;
            }
            return isUser;
        }
        private void InsertTempClaimBatch(string batchID)
        {
            try
            {
                string _query = "USP_INSERT_TEMP_CLAIM_BATCH '" + batchID + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        protected void FillDGR()
        {
            LB_CNT.Text = "";
            string where = "";

            if (TXT_NAMA.Text != "")
                where += " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text != "")
                where += " and COMPANY_NAME like '%" + TXT_COMPANY.Text + "%' ";

            if (DDL_DONE.SelectedValue != "")
                where += " and DONE like '%" + DDL_DONE.SelectedValue + "%' ";

            conn.QueryString = "SELECT * FROM V_CLM_CLAIM_MASTER_AMOUNT_LIST " +
                               "WHERE BATCH_ID='" + LB_BATCHID.Text + "' " + where;
            conn.ExecuteQuery(12000);

            LB_CNT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt = conn.GetDataTable().Copy();
            string claimNo = "";
            //-- ADD GAS
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    claimNo = row["CLAIM_NO"].ToString();
                    string cash = row["AMOUNT_CASH"].ToString().Replace(",", "");
                    string tolak = row["JUMLAH_TOLAK"].ToString().Replace(",", "");

                    if (cash != "0" && tolak != "0")
                    {
                        if (cash == tolak)
                        {
                            string recID = GetRecID(claimNo, cash, tolak);
                            if (recID != "")
                            {
                                //UpdateBenefitUnPaid(recID);
                            }
                            else
                            {
                                DataTable dtc = GetRecID2(claimNo);
                                if (dtc.Rows.Count != 0)
                                {
                                    foreach (DataRow drc in dtc.Rows)
                                    {
                                        //UpdateBenefitUnPaid(drc["ID"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //-- END GAS
            dt.Clear();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            var isAcces = CekUserDelete(GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDelete = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btClone = (Button)DGR.Items[i].FindControl("BT_CLONE");
                CheckBox checkDel = (CheckBox)DGR.Items[i].FindControl("CB");

                Label lb = (Label)DGR.Items[i].FindControl("LB_CLAIMNO");
                //Button btClone = (Button)DGR.Items[i].FindControl("BT_CLONE");
                btDelete = (Button)DGR.Items[i].FindControl("BT_DEL");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (DGR.Items[i].Cells[15].Text == "1")
                {
                    DGR.Items[i].Cells[3].ForeColor = Color.MediumSeaGreen;
                    DGR.Items[i].Cells[3].Font.Bold = true;
                    DGR.Items[i].Cells[4].ForeColor = Color.MediumSeaGreen;
                    DGR.Items[i].Cells[4].Font.Bold = true;
                }

                if (DGR.Items[i].Cells[16].Text != "1" && DGR.Items[i].Cells[16].Text != "2" || isAcces == false)
                    btDelete.Visible = false;
             
                    string Claim_No = "";
                    Claim_No = DGR.Items[i].Cells[1].Text;
                    //Alert By Claim No
                    lb.Text = "<a href='ClaimHeader.aspx?CLAIM_NO=" + DGR.Items[i].Cells[1].Text + "' ><span style='color: #0000FF'>" + DGR.Items[i].Cells[1].Text + "</span></a>";
                    btClone.Attributes["onclick"] = "return confirmSweetAlertPostback('COPY','" + Claim_No + "', this);";
                    //btDelete.Attributes["onclick"] = "return confirmSweetAlertPostback('DELETE','" + Claim_No + "', this);";
                    ////btClone.Attributes["onclick"] = $"return confirmSweetAlertPostback('COPY', '{claimNo}', this);";
                    ////btDelete.Attributes["onclick"] = $"return confirmSweetAlertPostback('DELETE', '{claimNo}', this);";



            }
        }

         protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                        cb.Checked = false;
                    else
                        cb.Checked = true;
                }
            }
            if (e.CommandName == "Clone")
            {
                //try
                //{string reason = txtDeleteReason?.Text ?? "";
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_CLONE '" + e.Item.Cells[1].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                Response.Redirect("ClaimHeader.aspx?CLAIM_NO=" + conn.GetFieldValue("CLAIM_NO").ToString());
                //}
                //catch { }
            }

            if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                DataGridItem item = DGR.Items[rowIndex];

                TextBox txtDeleteReason = (TextBox)item.FindControl("deleteReason");
                CheckBox check = (CheckBox)item.FindControl("CB");

                // ❌ Validasi gagal
                if (txtDeleteReason.Text == "" || !check.Checked)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "alertInvalid",
                        "Swal.fire({ icon: 'warning', title: 'Gagal', text: 'Checklist harus dicentang dan alasan delete harus diisi.' });",
                        true
                    );
                    return;
                }

                // ✔ Validasi OKE → Konfirmasi swal
                string claimNo = item.Cells[1].Text;
                string uniqueID = ((Button)item.FindControl("BT_DEL")).UniqueID;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "confirmDelete",
                    "Swal.fire({" +
                        "title:'Konfirmasi'," +
                        "text:'Yakin hapus " + claimNo + "?'," +
                        "icon:'warning'," +
                        "showCancelButton:true," +
                        "confirmButtonText:'Ya'," +
                        "cancelButtonText:'Tidak'" +
                    "}).then((r)=>{ if(r.isConfirmed){ __doPostBack('" + uniqueID + "', 'do'); } });",
                    true
                );

                return;
            }
        }
        private void HandleDelete(string target)
        {
            foreach (DataGridItem item in DGR.Items)
            {
                Button bt = (Button)item.FindControl("BT_DEL");

                if (bt != null && bt.UniqueID == target)
                {
                    TextBox txtDeleteReason = (TextBox)item.FindControl("deleteReason");
                    string reason = txtDeleteReason.Text;
                    string claimNo = item.Cells[1].Text;

                    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_ROLLBACK '" +
                                        claimNo + "', '" + reason + "', '" +
                                        GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    FillDGR();
                    break;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            string batchid = string.Empty;
            try
            {
                batchid = InsertMasterBatchUpsert();
            }
            catch (Exception ex)
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = ex.Message;
                return;
            }

            if (LB_BATCHID.Text == "")
                Response.Redirect("ClaimReg.aspx?BATCH_ID=" + batchid);
            else
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Blue;
                LB_ERROR.Text = "SUKSES";
            }

            Session["sessionSaveClaimReg"] = "1";

            //LB_ERROR.Text = "";
            //if (TXT_TGLSM.Text.Trim() == "")
            //{
            //    LB_ERROR.ForeColor = System.Drawing.Color.Red;
            //    LB_ERROR.Text = "Nomor dan tanggal dokumen tidak boleh kosong !";
            //    return;
            //}

            //string batchid = "null";
            //string provider = "null";
            //string polis = "null";
            //string bank = "null";

            //if (LB_BATCHID.Text != "")
            //    batchid = "'" + LB_BATCHID.Text + "'";
            //if (DDL_PROV.SelectedValue != "")
            //    provider = "'" + DDL_PROV.SelectedValue + "'";
            //if (DDL_POLIS.SelectedValue != "")
            //    polis = "'" + DDL_POLIS.SelectedValue + "'";
            //if (DDL_ACCBANK.SelectedValue != "")
            //    bank = "'" + DDL_ACCBANK.SelectedValue + "'";

            ////-- ADD GAS
            //DateTime TGLSM = DateTime.Parse(TXT_TGLSM.Text.Trim(), null);

            //try
            //{
            //    // remark GAS
            //    //conn.QueryString = "exec SP_CLM_CLAIM_MASTER_BATCH_UPSERT " +
            //    //                    batchid + "," +
            //    //                    "'" + TXT_SM.Text.Trim() + "'," +
            //    //                    "'" + DDL_DOC_SOURCE.SelectedValue + "'," +
            //    //                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLSM.Text.Trim(), "d/M/yyyy") + "'," +
            //    //                    "'" + DDL_PR.SelectedValue + "'," +
            //    //                    provider + "," +
            //    //                    polis + "," +
            //    //                    "'" + TXT_ACCNO.Text.Trim() + "'," +
            //    //                    "'" + TXT_ACCNAMA.Text.Trim() + "'," +
            //    //                    bank + "," +
            //    //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

            //    //-- ADD GAS
            //    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_BATCH_UPSERT " +
            //                        batchid + "," +
            //                        "'" + TXT_SM.Text.Trim() + "'," +
            //                        "'" + DDL_DOC_SOURCE.SelectedValue + "'," +
            //                        "'" + TGLSM.ToString("MM/dd/yyyy") + "'," +
            //                        "'" + DDL_PR.SelectedValue + "'," +
            //                        provider + "," +
            //                        polis + "," +
            //                        "'" + TXT_ACCNO.Text.Trim() + "'," +
            //                        "'" + TXT_ACCNAMA.Text.Trim() + "'," +
            //                        bank + "," +
            //                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

            //    conn.ExecuteQuery();
            //    batchid = conn.GetFieldValue("BATCH_ID").ToString();
            //}
            //catch (System.Exception ex)
            //{
            //    LB_ERROR.ForeColor = System.Drawing.Color.Red;
            //    LB_ERROR.Text = ex.Message;
            //    return;
            //}

            //if (LB_BATCHID.Text == "")
            //    Response.Redirect("ClaimReg.aspx?BATCH_ID=" + batchid);
            //else
            //{
            //    LB_ERROR.ForeColor = System.Drawing.Color.Blue;
            //    LB_ERROR.Text = "SUKSES";
            //}

            //Session["sessionSaveClaimReg"] = "1";
        }

        private string InsertMasterBatchUpsert()
        {
            string retValue = string.Empty;

            if (TXT_TGLSM.Text.Trim() == "")
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = "Nomor dan tanggal dokumen tidak boleh kosong !";
            }
            else
            {
                string batchid = "null";
                string provider = "null";
                string polis = "null";
                string bank = "null";

                if (LB_BATCHID.Text != "")
                    batchid = "'" + LB_BATCHID.Text + "'";
                if (DDL_PROV.SelectedValue != "")
                    provider = "'" + DDL_PROV.SelectedValue + "'";
                if (DDL_POLIS.SelectedValue != "")
                    polis = "'" + DDL_POLIS.SelectedValue + "'";
                if (DDL_ACCBANK.SelectedValue != "")
                    bank = "'" + DDL_ACCBANK.SelectedValue + "'";

                //-- ADD GAS
                DateTime TGLSM = DateTime.Parse(TXT_TGLSM.Text.Trim(), null);

                try
                {
                    //-- ADD GAS
                    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_BATCH_UPSERT " +
                                        batchid + "," +
                                        "'" + TXT_SM.Text.Trim() + "'," +
                                        "'" + DDL_DOC_SOURCE.SelectedValue + "'," +
                                        "'" + TGLSM.ToString("MM/dd/yyyy") + "'," +
                                        "'" + DDL_PR.SelectedValue + "'," +
                                        provider + "," +
                                        polis + "," +
                                        "'" + TXT_ACCNO.Text.Trim() + "'," +
                                        "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                        bank + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteQuery();
                    retValue = conn.GetFieldValue("BATCH_ID").ToString();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

            return retValue;
        }

       
        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            if (LB_BATCHID.Text == "")
                return;
            Response.Redirect("ClaimHeader.aspx?BATCH_ID=" + LB_BATCHID.Text);
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {
                try
                {
                    string where = "";

                    if (TXT_NAMA.Text != "")
                        where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

                    if (TXT_COMPANY.Text != "")
                        where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text + "%' ";

                    if (DDL_DONE.SelectedValue != "")
                        where = where + " and DONE like '%" + DDL_DONE.SelectedValue + "%' ";
                    //conn.QueryString = "exec SP_CLM_CLAIM_MASTER_AMOUNT_LIST_TOTAL '" + LB_BATCHID.Text + "'";
                    conn.QueryString = "SELECT " +
                                        "AMOUNT_PENGAJUAN = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.AMOUNT_PENGAJUAN,0))),1),'.00',''), " +
                                        "AMOUNT_CASH = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.AMOUNT_CASH,0))),1),'.00',''), " +
                                        "AMOUNT_BAYAR = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.AMOUNT_BAYAR,0))),1),'.00',''), " +
                                        "JUMLAH_TOLAK = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.JUMLAH_TOLAK,0))),1),'.00',''), " +
                                        "EKSES = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.EKSES,0))),1),'.00',''), " +
                                        "REFUND = REPLACE(CONVERT(VARCHAR(100),CONVERT(MONEY,SUM(ISNULL(b.REFUND,0))),1),'.00','') " +
                                        "FROM V_CLM_CLAIM_MASTER a " +
                                        "LEFT JOIN V_CLM_CLAIM_BENEFIT_SUM b ON a.CLAIM_NO=b.CLAIM_NO " +
                                        "WHERE a.BATCH_ID='" + LB_BATCHID.Text + "' " + where;
                    conn.ExecuteQuery();

                    e.Item.Cells[7].Text = "TOTAL";
                    e.Item.Cells[8].Text = conn.GetFieldValue("AMOUNT_PENGAJUAN").ToString();
                    e.Item.Cells[9].Text = conn.GetFieldValue("AMOUNT_CASH").ToString();
                    e.Item.Cells[10].Text = conn.GetFieldValue("AMOUNT_BAYAR").ToString();
                    e.Item.Cells[11].Text = conn.GetFieldValue("JUMLAH_TOLAK").ToString();
                    e.Item.Cells[12].Text = conn.GetFieldValue("EKSES").ToString();
                    e.Item.Cells[13].Text = conn.GetFieldValue("REFUND").ToString();
                }
                catch { }
            }
        }

        protected void ShowRekeningSource()
        {
            string owner = "";
            if (DDL_PR.SelectedValue == "P")
                owner = DDL_PROV.SelectedValue;
            else
                owner = DDL_POLIS.SelectedValue;

            conn.QueryString = "USP_GET_REKENING_SOURCE '" + DDL_PR.SelectedValue + "','" + owner + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            if (dt.Rows.Count >= 2)
            {
                string[] arr = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arr[i] = dt.Rows[i]["CODE"].ToString();
                }

                int counter = 1;
                for (int i = 0; i < counter; i++)
                {
                    if (arr[i].ToString() != arr[counter].ToString())
                    {
                        TXT_ACCNAMA.Text = "";
                        TXT_ACCNO.Text = "";
                        DDL_ACCBANK.SelectedValue = "";
                    }
                    else
                    {
                        TXT_ACCNAMA.Text = dt.Rows[0][5].ToString();
                        TXT_ACCNO.Text = dt.Rows[0][4].ToString();
                        DDL_ACCBANK.SelectedValue = dt.Rows[0][6].ToString();
                        //string batchID = InsertMasterBatchUpsert();
                    }
                }

                DDL_NOREK.DataSource = dt;
                DDL_NOREK.DataTextField = "TIPE";
                DDL_NOREK.DataValueField = "ACC_NO";
                DDL_NOREK.DataBind();
            }
            else if (dt.Rows.Count > 0 && dt.Rows.Count == 1)
            {
                DDL_NOREK.DataSource = dt;
                DDL_NOREK.DataTextField = "TIPE";
                DDL_NOREK.DataValueField = "ACC_NO";
                TXT_ACCNAMA.Text = dt.Rows[0][5].ToString();
                TXT_ACCNO.Text = dt.Rows[0][4].ToString();
                DDL_ACCBANK.SelectedValue = dt.Rows[0][6].ToString();
                DDL_NOREK.DataBind();
                //string batchID = InsertMasterBatchUpsert();
            }
            else
            {
                DDL_NOREK.Items.Clear();
            }



            //// remark GAS
            //conn.QueryString = "select " +
            //                    "ACC_NO, " +
            //                    "TIPE " +
            //                    "from V_CLM_CLAIM_MASTER_BATCH_REKENING_SOURCE " +
            //                    "where " +
            //                    "PR='" + DDL_PR.SelectedValue + "' " +
            //                    "and OWNER='" + owner + "' " +
            //                    "order by TIPE";
            //conn.ExecuteQuery();
            //DDL_NOREK.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}
        }

        protected void BT_NOREK_Click(object sender, EventArgs e)
        {
            TXT_ACCNO.Text = "";
            TXT_ACCNAMA.Text = "";
            DDL_ACCBANK.SelectedValue = "";

            if (DDL_NOREK.Items.Count == 0)
                return;

            conn.QueryString = "select " +
                                "a.ACC_NO, " +
                                "a.ACC_NAMA, " +
                                "a.ACC_BANK " +
                                "from V_CLM_CLAIM_MASTER_BATCH_REKENING_SOURCE a " +
                                "where " +
                                "a.PR='" + DDL_PR.SelectedValue + "' " +
                                "and a.ACC_NO='" + DDL_NOREK.SelectedValue + "' " +
                                "and a.OWNER='" + DDL_PROV.SelectedValue + "' " +
                                "order by a.TIPE";
            conn.ExecuteQuery();
            TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            TXT_ACCNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
            try
            {
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
            }
            catch { }
        }

        protected void DDL_PR_SelectedIndexChanged(object sender, EventArgs e)
        {
            CekProviderReimb();
            ShowRekeningSource();
            BT_NOREK_Click(null, EventArgs.Empty);
        }

        protected void DDL_PROV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRekeningSource();
            BT_NOREK_Click(null, EventArgs.Empty);
        }

        protected void DDL_POLIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRekeningSource();
            BT_NOREK_Click(null, EventArgs.Empty);
        }

        protected void CekProviderReimb()
        {
            if (DDL_PR.SelectedValue == "P")
            {
                DDL_PROV.Enabled = true;
                BT_CARIPROVIDER.Enabled = true;
                DDL_POLIS.Enabled = false;
                BT_CARIPOLIS.Enabled = false;

                DDL_POLIS.SelectedValue = "";
            }
            else
            {
                DDL_PROV.Enabled = false;
                BT_CARIPROVIDER.Enabled = false;
                DDL_POLIS.Enabled = true;
                BT_CARIPOLIS.Enabled = true;

                DDL_PROV.SelectedValue = "";
            }
        }

        protected void DDL_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            BT_NOREK_Click(null, EventArgs.Empty);
        }

        protected void BT_DONE_Click(object sender, EventArgs e)
        {
            try
            {
                string ValueDate = "";
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        if (DGR.Items[i].Cells[15].Text == "1")
                            ValueDate = "USER_STARTDATE";
                        else
                            ValueDate = "GETDATE()";
                        conn.QueryString = "UPDATE dbo.TRACK_DATA  " +
                                            "SET USER_ENDDATE=" + ValueDate + " " +
                                            "WHERE OWNER='" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteQuery();
                    }
                }
                FillDGR();
            }
            catch { }
            ;
        }
        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        private void UpdateBenefitUnPaid(string recID)
        {
            try
            {
                Connection conns = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                conns.QueryString = "USP_UPDATE_UNPAID_BENEFIT '" + recID + "'";
                conns.ExecuteQuery();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private string GetRecID(string claimNo, string cashAmount, string unpaidAmount)
        {
            string retValue = string.Empty;
            try
            {
                Connection connss = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                connss.QueryString = "USP_GET_REC_UD '" + claimNo + "', '" + cashAmount + "', '" + unpaidAmount + "'";
                connss.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = connss.GetDataTable().Copy();

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        retValue = dr["RECID"].ToString();
                    }
                }

                return retValue;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return retValue;
        }

        private DataTable GetRecID2(string claimNo)
        {
            DataTable dt = new DataTable();
            try
            {
                Connection connss2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                connss2.QueryString = "USP_GET_REC_ID_2 '" + claimNo + "'";
                connss2.ExecuteQuery();
                dt = connss2.GetDataTable().Copy();
                return dt;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }

        protected void BT_INQUIRY_Click(object sender, EventArgs e)
        {

            try
            {
                string accountNo = TXT_ACCNO.Text.Trim();
                string accountName = TXT_ACCNAMA.Text.Trim();
                string bankCode = DDL_ACCBANK.SelectedValue;

                conn.QueryString = "select CLEARING_CODE from FINANCE.dbo.PARAM_TBL_BANK where code = '" + bankCode + "'";
                conn.ExecuteQuery(3000);

                string clearingCode = conn.GetFieldValue("CLEARING_CODE").ToString().Substring(0, 3).Trim(); //"009";
                string accounBMITakaful = "3040031803";
                string transferAmount = "0";
                string transferDesc = "-";
                string userBy = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                conn.QueryString = "exec FINANCE.dbo.SP_API_BMI_INQUIRY " +
                                    "'" + clearingCode + "'," +
                                    "'" + accounBMITakaful + "'," +
                                    "'" + accountNo + "'," +
                                    "'" + transferAmount + "'," +
                                    "'" + transferDesc + "'," +
                                    "'" + userBy + "'";


                conn.ExecuteQuery(3000);

                if (conn.GetRowCount() > 0)
                {

                    if (conn.GetFieldValue("errorCode").ToString() == "00")
                    {

                        lblDestName.Text = conn.GetFieldValue("toAccName").ToString();
                        lblDestAccNo.Text = accountNo;
                        lblDestBank.Text = DDL_ACCBANK.SelectedItem.Text;

                        spanInquery.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);

                        spanInquery.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }

        //protected void BT_INQUIRY_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        string accountNo = TXT_ACCNO.Text.Trim();
        //        string accountName = TXT_ACCNAMA.Text.Trim();
        //        string bankCode = DDL_ACCBANK.SelectedValue;

        //        conn.QueryString = "select CLEARING_CODE from FINANCE.dbo.PARAM_TBL_BANK where code = '" + bankCode + "'";
        //        conn.ExecuteQuery(3000);

        //        string clearingCode = conn.GetFieldValue("CLEARING_CODE").ToString().Substring(0, 3).Trim(); //"009";
        //        string accounBMITakaful = "3040031803";
        //        string transferAmount = "0";
        //        string transferDesc = "-";
        //        string userBy = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

        //        conn.QueryString = "exec FINANCE.dbo.SP_API_BMI_INQUIRY " +
        //                            "'" + clearingCode + "'," +
        //                            "'" + accounBMITakaful + "'," +
        //                            "'" + accountNo + "'," +
        //                            "'" + transferAmount + "'," +
        //                            "'" + transferDesc + "'," +
        //                            "'" + userBy + "'";


        //        conn.ExecuteQuery(3000);

        //        if (conn.GetRowCount() > 0)
        //        {

        //            if (conn.GetFieldValue("errorCode").ToString() == "00")
        //            {

        //                lblDestName.Text = conn.GetFieldValue("toAccName").ToString();
        //                lblDestAccNo.Text = accountNo;
        //                lblDestBank.Text = DDL_ACCBANK.SelectedItem.Text;

        //                spanInquery.Visible = true;
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);

        //                spanInquery.Visible = false;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
        //    }

        //}


    }
}
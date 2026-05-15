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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_BATCHID.Text = Request.QueryString["BATCH_ID"].ToString();
                Setup();
                if (LB_BATCHID.Text.Trim() != "")
                {
                    LoadRecord(LB_BATCHID.Text.Trim());
                }
                CekProviderReimb();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select LTRIM(RTRIM(KODE_PROVIDER)),NAMA from PROVIDER_MASTER where LTRIM(ISNULL(NAMA,''))<>'' order by LTRIM(NAMA)";
            conn.ExecuteQuery();
            DDL_PROV.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROV.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select a.ID,DESCR = LEFT(b.COMPANY_NAME,50) + ' - ' + a.POLICY_NO from POLICY a inner join COMPANY b on a.COMPANY_CODE=b.COMPANY_CODE order by b.COMPANY_NAME";
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
            conn.QueryString = "select " +
                                "DOC_NO, " +
                                "DOC_SOURCE, " +
                                "TGL_DOC = CONVERT(varchar(20),TGL_DOC,103), " +
                                "PR, " +
                                "KODE_PROVIDER, " +
                                "POLICY_ID, " +
                                "ACC_NO, " +
                                "ACC_NAMA, " +
                                "ACC_BANK " +
                                "from V_CLM_CLAIM_MASTER_BATCH " +
                                "where BATCH_ID='" + batch_id + "'";
            conn.ExecuteQuery();

            TXT_SM.Text = conn.GetFieldValue("DOC_NO").ToString();
            TXT_TGLSM.Text = conn.GetFieldValue("TGL_DOC").ToString();
            TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            TXT_ACCNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
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
            try
            {
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
            }
            catch { }

            ShowRekeningSource();

            TBL_ARSIP.Visible = true;

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], "CLMBATCH", LB_BATCHID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            I1.Attributes.Add("src", URL);
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_CNT.Text = "";
            string where = "";

            if (TXT_NAMA.Text != "")
                where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text + "%' ";

            if (DDL_DONE.SelectedValue != "")
                where = where + " and DONE like '%" + DDL_DONE.SelectedValue + "%' ";
            /*
            conn.QueryString = "select " +
                                "[NOREG CLAIM] = CLAIM_NO, " +
                                "[NAMA PESERTA] = NAMA, " +
                                "[PERUSAHAAN] = LEFT(COMPANY_NAME,30), " +
                                "[PROVIDER] = PROVIDER, " +
                                "[TIPE] = TIPE_CLAIM_DESCR, " +
                                "[TGL KLAIM] = convert(varchar(20),TGL_KLAIM,106), " +
                                "[TGL RAWAT DARI] = convert(varchar(20),TGL_RAWAT_DARI,106), " +
                                "[TGL RAWAT SAMPAI] = convert(varchar(20),TGL_RAWAT_SAMPAI,106), " +
                                "[LAST TRACK]=LAST_TRACK_DESCR  " +
                                "from V_CLM_CLAIM_MASTER a  " +
                                "where BATCH_ID='" + LB_BATCHID.Text + "' " +
                                "order by NAMA";
            */
            //conn.QueryString = "exec SP_CLM_CLAIM_MASTER_AMOUNT_LIST '" + LB_BATCHID.Text + "'";
            conn.QueryString = "SELECT * FROM V_CLM_CLAIM_MASTER_AMOUNT_LIST " +
                                "WHERE BATCH_ID='" + LB_BATCHID.Text + "' " + where;
            conn.ExecuteQuery();

            LB_CNT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lb = (Label)DGR.Items[i].FindControl("LB_CLAIMNO");
                Button btClone = (Button)DGR.Items[i].FindControl("BT_CLONE");
                Button btDelete = (Button)DGR.Items[i].FindControl("BT_DEL");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[15].Text == "1")
                {
                    DGR.Items[i].Cells[3].ForeColor = Color.MediumSeaGreen;
                    DGR.Items[i].Cells[3].Font.Bold = true;
                    DGR.Items[i].Cells[4].ForeColor = Color.MediumSeaGreen;
                    DGR.Items[i].Cells[4].Font.Bold = true;
                }
                if (DGR.Items[i].Cells[16].Text != "1" && DGR.Items[i].Cells[16].Text != "2")
                    btDelete.Visible = false;
                lb.Text = "<a href='ClaimHeader.aspx?CLAIM_NO=" + DGR.Items[i].Cells[1].Text + "' ><span style='color: #0000FF'>" + DGR.Items[i].Cells[1].Text + "</span></a>";
                btClone.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk COPY " + DGR.Items[i].Cells[1].Text + " ?')){return false;};");
                btDelete.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE " + DGR.Items[i].Cells[1].Text + " ?')){return false;};");
            }

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            if (TXT_TGLSM.Text.Trim() == "")
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = "Nomor dan tanggal dokumen tidak boleh kosong !";
                return;
            }

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

            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_BATCH_UPSERT " +
                                    batchid + "," +
                                    "'" + TXT_SM.Text.Trim() + "'," +
                                    "'" + DDL_DOC_SOURCE.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLSM.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_PR.SelectedValue + "'," +
                                    provider + "," +
                                    polis + "," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                    bank + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                batchid = conn.GetFieldValue("BATCH_ID").ToString();
            }
            catch (System.Exception ex)
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
                //{
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_CLONE '" + e.Item.Cells[1].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                Response.Redirect("ClaimHeader.aspx?CLAIM_NO=" + conn.GetFieldValue("CLAIM_NO").ToString());
                //}
                //catch { }
            }
            if (e.CommandName == "Delete")
            {
                //try
                //{
                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_ROLLBACK '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();
                FillDGR();
                //}
                //catch { }
            }
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


            conn.QueryString = "select " +
                                "ACC_NO, " +
                                "TIPE " +
                                "from V_CLM_CLAIM_MASTER_BATCH_REKENING_SOURCE " +
                                "where " +
                                "PR='" + DDL_PR.SelectedValue + "' " +
                                "and OWNER='" + owner + "' " +
                                "order by TIPE";
            conn.ExecuteQuery();
            DDL_NOREK.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
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
            catch { };
        }
        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
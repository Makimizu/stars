using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Globalization;
using System.Security.Claims;
using System.Diagnostics;

namespace HEALTH.Form_Klaim
{ 
    public partial class ClaimPending : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //<%-- Push by Firman --%>
            if (!IsPostBack)
            {
                LB_TRACK.Text = Request.QueryString["track"];
                Setup();
            }
        }
          
        protected void Setup()
        {
            //DDL CP
            DDL_CP.Items.Add(new ListItem("", ""));
            DDL_CP.Items.Add(new ListItem("CP", "CP"));
            DDL_CP.Items.Add(new ListItem("CP NOT FITS", "CP NOT FITS"));
            DDL_CP.Items.Add(new ListItem("NON-CP", "NON-CP"));

            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery(120);
            DDL_PR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE,DESCR from PR_TIPE_CLAIM";
            conn.ExecuteQuery(120);
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
             
            conn.QueryString = "select " +
                                "b.SEQ, b.DESCR  " +
                                "from PARAM_TRACK_NEXT a " +
                                "inner join PARAM_TRACK b on a.TIPE_CODE=b.TIPE_CODE and a.NEXT_SEQ=b.SEQ " +
                                "where a.TIPE_CODE='CLMMASTER' and a.SEQ='" + LB_TRACK.Text + "' " +
                                "order by b.SEQ desc";
            conn.ExecuteQuery(120);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NEXTTRACK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_DOC_SOURCE order by CODE";
            conn.ExecuteQuery(120);
            DDL_DOC_SOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DOC_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //BT_PROSES.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk SUBMIT ?')){return false;};");
            SetButtonPROSES();
        }
         
        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string date1 = "1 jan 1980";
            string date2 = "31 dec 2090";
            string done = "";
            string view = "";
            string datarekening = "(ISNULL(ACC_NO,'')<>'' and ISNULL(ACC_NAMA,'')<>'' and ISNULL(ACC_BANK,'')<>'')";

            if (TXT_CLAIMNO.Text.Trim() != "")
                where = where + " and CLAIM_NO = '" + TXT_CLAIMNO.Text.Trim() + "' ";

            if (TXT_NAMA.Text.Trim() != "")
                where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (DDL_DOC_SOURCE.SelectedValue != "")
                where = where + " and DOC_SOURCE like '%" + DDL_DOC_SOURCE.SelectedValue + "%' ";

            if (TXT_SM.Text.Trim() != "")
                where = where + " and DOC_NO like '%" + TXT_SM.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (PROVIDER.Text.Trim() != "")
                where = where + " and PROVIDER like '%" + PROVIDER.Text.Trim() + "%' ";

            if (TOT_BAYAR1.Text.Trim() != "")
                where = where + " and AMOUNT_BAYAR >= " + TOT_BAYAR1.Text.Trim() + " ";

            if (TOT_BAYAR2.Text.Trim() != "")
                where = where + " and AMOUNT_BAYAR <= " + TOT_BAYAR2.Text.Trim() + " ";

            if (DDL_PR.SelectedValue != "")
                where = where + " and PR='" + DDL_PR.SelectedValue + "' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and TIPE_CLAIM='" + DDL_TIPE.SelectedValue + "' ";

            if (DDL_CP.SelectedValue != "")
                where = where + " and STATUS_CP='" + DDL_CP.SelectedValue + "' ";

            if (TXT_DATE.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                if (TXT_DATE.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                where = where + " and (a.TGL_KLAIM between '" + date1 + "' and '" + date2 + "') ";
            }

            if (TXT_DOB.Text.Trim() != "")
            {
                if (TXT_DOB.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy");

                where = where + " and a.DOB = '" + date1 + "' ";
            }

            if (DDL_REK.SelectedValue == "0")
            {
                where = where + " and ((ISNULL(ACC_NO,'')='' or ISNULL(ACC_NAMA,'')='' or ISNULL(ACC_BANK,'')='')) ";
            }
            else
            {
                where = where + " and ((ISNULL(ACC_NO,'')<>'' and ISNULL(ACC_NAMA,'')<>'' and ISNULL(ACC_BANK,'')<>'')) ";
            }

            conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' and ROLE_CODE in (select code from PR_USER_ACCESS_POLICYNO)";
            conn.ExecuteQuery(120);
            string ROLECODE = conn.GetFieldValue("ROLE_CODE").ToString();
            if (!String.IsNullOrEmpty(ROLECODE))
            {
                where = where + " and (select x.POLICY_NO from ASKES_MIGRASI.dbo.V_CLM_CLAIM_MASTER x where x.CLAIM_NO = a.CLAIM_NO) in (select [DESCR] from [PR_USER_ACCESS_POLICYNO] where CODE = '" + ROLECODE + "') ";
            }

            switch (LB_TRACK.Text)
            {
                case "1": view = "V_CLM_CLAIM_MASTER_PENDING_VERIFY"; break;
                case "2": view = "V_CLM_CLAIM_MASTER_PENDING_APPROVAL"; break;
            }

            conn.QueryString = "select " +
                                "CLAIM_NO, " +
                                "DOC_NO, " +
                                "NAMA, " +
                                "COMPANY_NAME, " +
                                "PR_DESCR, " +
                                "TIPE_CLAIM_DESCR, " +
                                "PEC, " +
                                "ICD_CODE, " +
                                "PROVIDER, " +
                                "STATUS_CP, " +
                                "DOB = CONVERT(varchar(20),DOB,106), " +
                                "TGL_KLAIM = CONVERT(varchar(20),TGL_KLAIM,106), " +
                                "AMOUNT_PENGAJUAN = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_PENGAJUAN),1),'.00',''), " +
                                "AMOUNT_CASH = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_CASH),1),'.00',''), " +
                                "AMOUNT_BAYAR = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_BAYAR),1),'.00',''), " +
                                "JUMLAH_TOLAK = replace(CONVERT(varchar(100),CONVERT(money,JUMLAH_TOLAK),1),'.00',''), " +
                                "EKSES = replace(CONVERT(varchar(100),CONVERT(money,EKSES),1),'.00',''), " +
                                "REFUND = replace(CONVERT(varchar(100),CONVERT(money,REFUND),1),'.00',''), " +
                                "DONE, " +
                                "TOTAL_BIAYA_CP = replace(CONVERT(varchar(100),CONVERT(money,TOTAL_BIAYA_CP),1),'.00','')" +
                                "from " + view + " a " +
                                "where 1=1 " + where +
                                "order by  " +
                                "a.TGL_KLAIM asc, " +
                                "a.COMPANY_NAME, " +
                                "a.NAMA";

            conn.ExecuteQuery(120);
            LB_CNT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            if (dt.Rows.Count > 0)
            {
                //try
                //{
                //    Connection connUpdateExists = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                //    connUpdateExists.QueryString = "USP_CLAIM_APPROVAL_EXISITING_BANK_INSERT '" + conn.GetFieldValue("DOC_NO").ToString() + "'";
                //    connUpdateExists.ExecuteQuery();
                //}
                //catch (Exception ex)
                //{
                //    ex.Message.ToString();
                //}

                foreach (DataRow row in dt.Rows)
                {
                    string claimNo = row["CLAIM_NO"].ToString();
                    string cash = row["AMOUNT_CASH"].ToString().Replace(",", "");
                    string tolak = row["JUMLAH_TOLAK"].ToString().Replace(",", "");

                    if (cash != "0" && tolak != "0")
                    {
                        if (cash == tolak)
                        {
                            string recID = GetRecID(claimNo, cash, tolak);
                            if (recID != "")
                            {
                                UpdateBenefitUnPaid(recID);
                            }
                            else
                            {
                                DataTable dtc = new DataTable();
                                dtc = GetRecID2(claimNo);

                                if (dtc.Rows.Count != 0)
                                {
                                    foreach (DataRow drc in dtc.Rows)
                                    {
                                        UpdateBenefitUnPaid(drc["ID"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else // Penambahan NUR !! CEK Ketika BATCH CLAIM NO REK KOSONG, INSERT!
            {
                where = "";

                if (TXT_CLAIMNO.Text.Trim() != "")
                    where = where + " and CLAIM_NO = '" + TXT_CLAIMNO.Text.Trim() + "' ";

                if (TXT_NAMA.Text.Trim() != "")
                    where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

                if (DDL_DOC_SOURCE.SelectedValue != "")
                    where = where + " and DOC_SOURCE like '%" + DDL_DOC_SOURCE.SelectedValue + "%' ";

                if (TXT_SM.Text.Trim() != "")
                    where = where + " and DOC_NO like '%" + TXT_SM.Text.Trim() + "%' ";

                if (TXT_COMPANY.Text.Trim() != "")
                    where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

                if (PROVIDER.Text.Trim() != "")
                    where = where + " and PROVIDER like '%" + PROVIDER.Text.Trim() + "%' ";

                if (TOT_BAYAR1.Text.Trim() != "")
                    where = where + " and AMOUNT_BAYAR >= " + TOT_BAYAR1.Text.Trim() + " ";

                if (TOT_BAYAR2.Text.Trim() != "")
                    where = where + " and AMOUNT_BAYAR <= " + TOT_BAYAR2.Text.Trim() + " ";

                if (DDL_PR.SelectedValue != "")
                    where = where + " and PR='" + DDL_PR.SelectedValue + "' ";

                if (DDL_TIPE.SelectedValue != "")
                    where = where + " and TIPE_CLAIM='" + DDL_TIPE.SelectedValue + "' ";

                if (TXT_DATE.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
                {
                    if (TXT_DATE.Text.Trim() != "")
                        date1 = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
                    if (TXT_DATE2.Text.Trim() != "")
                        date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                    where = where + " and (a.TGL_KLAIM between '" + date1 + "' and '" + date2 + "') ";
                }

                if (TXT_DOB.Text.Trim() != "")
                {
                    if (TXT_DOB.Text.Trim() != "")
                        date1 = GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy");

                    where = where + " and a.DOB = '" + date1 + "' ";
                }

                conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' and ROLE_CODE in (select code from PR_USER_ACCESS_POLICYNO)";
                conn.ExecuteQuery(120);
                string ROLECODE2 = conn.GetFieldValue("ROLE_CODE").ToString();
                if (!String.IsNullOrEmpty(ROLECODE2))
                {
                    where = where + " and (select x.POLICY_NO from ASKES_MIGRASI.dbo.V_CLM_CLAIM_MASTER x where x.CLAIM_NO = a.CLAIM_NO) in (select [DESCR] from [PR_USER_ACCESS_POLICYNO] where CODE = '" + ROLECODE2 + "') ";
                }

                where = where + " and ((ISNULL(ACC_NO,'')='' or ISNULL(ACC_NAMA,'')='' or ISNULL(ACC_BANK,'')='')) ";

                switch (LB_TRACK.Text)
                {
                    case "1": view = "V_CLM_CLAIM_MASTER_PENDING_VERIFY"; break;
                    case "2": view = "V_CLM_CLAIM_MASTER_PENDING_APPROVAL"; break;
                }

                Connection connNUR = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

                connNUR.QueryString = "select " +
                               "CLAIM_NO, " +
                               "DOC_NO, " +
                               "NAMA, " +
                               "COMPANY_NAME, " +
                               "PR_DESCR, " +
                               "TIPE_CLAIM_DESCR, " +
                               "PEC, " +
                               "ICD_CODE, " +
                               "PROVIDER, " +
                               "STATUS_CP, " +
                               "DOB = CONVERT(varchar(20),DOB,106), " +
                               "TGL_KLAIM = CONVERT(varchar(20),TGL_KLAIM,106), " +
                               "AMOUNT_PENGAJUAN = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_PENGAJUAN),1),'.00',''), " +
                               "AMOUNT_CASH = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_CASH),1),'.00',''), " +
                               "AMOUNT_BAYAR = replace(CONVERT(varchar(100),CONVERT(money,AMOUNT_BAYAR),1),'.00',''), " +
                               "JUMLAH_TOLAK = replace(CONVERT(varchar(100),CONVERT(money,JUMLAH_TOLAK),1),'.00',''), " +
                               "EKSES = replace(CONVERT(varchar(100),CONVERT(money,EKSES),1),'.00',''), " +
                               "REFUND = replace(CONVERT(varchar(100),CONVERT(money,REFUND),1),'.00',''), " +
                               "DONE, " +
                                "TOTAL_BIAYA_CP = replace(CONVERT(varchar(100),CONVERT(money,TOTAL_BIAYA_CP),1),'.00','')" +
                               "from " + view + " a " +
                               "where 1=1 " + where +
                               "order by  " +
                               "a.TGL_KLAIM asc, " +
                               "a.COMPANY_NAME, " +
                               "a.NAMA";

                connNUR.ExecuteQuery(120);
                dt = connNUR.GetDataTable().Copy();

                string claimNox = connNUR.GetFieldValue("CLAIM_NO").ToString();
                string docNo = connNUR.GetFieldValue("DOC_NO").ToString();

                //Connection connAPP = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                //connAPP.QueryString = "USP_CLAIM_APPROVAL_EMPTY_BANK_INSERT_2 '" + claimNox + "', '" + docNo + "'";
                //connAPP.ExecuteQuery();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string claimNo = row["CLAIM_NO"].ToString();
                        string cash = row["AMOUNT_CASH"].ToString().Replace(",", "");
                        string tolak = row["JUMLAH_TOLAK"].ToString().Replace(",", "");

                        if (cash != "0" && tolak != "0")
                        {
                            if (cash == tolak)
                            {
                                string recID = GetRecID(claimNo, cash, tolak);
                                if (recID != "")
                                {
                                    UpdateBenefitUnPaid(recID);
                                }
                                else
                                {
                                    DataTable dtc = new DataTable();
                                    dtc = GetRecID2(claimNo);

                                    if (dtc.Rows.Count != 0)
                                    {
                                        foreach (DataRow drc in dtc.Rows)
                                        {
                                            UpdateBenefitUnPaid(drc["ID"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            conn.ExecuteQuery(120);
            LB_CNT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            //dt.Clear();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='200'";
            conn.ExecuteQuery(120);
            string URL = conn.GetFieldValue("URL").ToString();

            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='277'";
            conn.ExecuteQuery(120);
            string URL_SM = conn.GetFieldValue("URL").ToString();


            conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(120);
            string role_code = conn.GetFieldValue("ROLE_CODE").ToString();

            bool manager = role_code == "86" || role_code == "00";


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                Label lbClaimNo = (Label)DGR.Items[i].FindControl("LB_CLAIMNO");
                Label lbDocNo = (Label)DGR.Items[i].FindControl("LB_DOC_NO");
                Button BtAppAPL = (Button)DGR.Items[i].FindControl("BT_APPAPL");

                if (DGR.Items[i].Cells[20].Text == "1")
                {

                    //'- CR Approval Claim baik Clinical Pathway maupun Non Clinical Pathway, dapat disetujui oleh team approve.
                    //Untuk kondisi Claim Clinical Pathway dengan nilai pengajuan Rumah Sakit > Nominal Clinical Pathway maka yang dapat disetujui hanya level Manager.

                    bool isCPNotFits = DGR.Items[i].Cells[23].Text == "CP NOT FITS";
                    if (isCPNotFits)
                    {
                        if (!manager)
                        {
                            cb.Visible = false;
                        }
                        else
                        {
                            cb.Visible = true;
                        }
                    }
                    else
                    {
                        cb.Visible = true;
                    }
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Green;
                    DGR.Items[i].Cells[9].Font.Bold = true;
                    DGR.Items[i].Cells[10].ForeColor = System.Drawing.Color.Green;
                    DGR.Items[i].Cells[10].Font.Bold = true;
                }
                else if (DGR.Items[i].Cells[20].Text == "2")
                {
                    conn.QueryString = "select count(CODE) as COUNT from ASKES_MIGRASI.dbo.PR_CLAIM_APROVAL_APL_LIMIT where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();
                    if (Int32.Parse(conn.GetFieldValue("COUNT")) > 0)
                    {
                        BtAppAPL.Visible = true;
                    }
                }
                else
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }

                //lbClaimNo.Text = "<a href='" +URL+ "&rc:Parameters=False&CLAIM_NO=" +DGR.Items[i].Cells[1].Text+ "'  target='Claim'><span style='color: Blue'>" +DGR.Items[i].Cells[1].Text+ "</span></a>";
                lbDocNo.Text = "<a href='" + URL_SM + "&rc:Parameters=False&DOC_NO=" + DGR.Items[i].Cells[2].Text + "'  target='Claim'><span style='color: Blue'>" + DGR.Items[i].Cells[2].Text + "</span></a>";
                lbClaimNo.Text = "<a href='ClaimHeader.aspx?CLAIM_NO=" + DGR.Items[i].Cells[1].Text + "'  target='Claim'><span style='color: Blue'>" + DGR.Items[i].Cells[1].Text + "</span></a>";
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

        private string GetRecIDSim(string claimNo)
        {
            string retValue = string.Empty;
            try
            {
                Connection connss = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                connss.QueryString = "USP_GET_SIMILAR_CLAIM '" + claimNo + "'";
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


        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            try
            {
                DGR.CurrentPageIndex = 0;
                FillDGR();

                // Close Swal loading after processing
                ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
            }
            catch (Exception ex)
            {
                // Show error message using Swal
                //ScriptManager.RegisterStartupScript(this, GetType(), "errorSwal",
                //    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "errorSwal",
                    "Swal.fire('Error', '" + ex.Message + "', 'error');", true);

            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                DGR.CurrentPageIndex = e.NewPageIndex;
                FillDGR();

                // Close Swal loading after processing
                ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
            }
            catch (Exception ex)
            {
                // Show error message using Swal
                //ScriptManager.RegisterStartupScript(this, GetType(), "errorSwal",
                //    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "errorSwal",
                    "Swal.fire('Error', '" + ex.Message + "', 'error');", true);

            }
        }

        protected void BT_PROSES_Click(object sender, EventArgs e)
        {
            int iCount = 0, iCountFiled = 0, iCountChecklist = 0;
            string recidsim = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked && cb.Visible == true)
                {
                    try
                    {
                        //validate limit klaim approve 
                        //var dataBayar1 = DGR.DataKeys[0].ToString();
                        var dataBayar = DGR.Items[i].Cells[16].Text;
                        decimal dcNominal = decimal.Parse(dataBayar, CultureInfo.InvariantCulture);
                        //decimal dcNominal = decimal.Parse(DGR.Items[i].Cells[12].Text.Replace(",", ".").Replace(".", ","));
                        bool validate = validateApprove(dcNominal);
                        if (validate == true)
                        {
                            string recID = GetRecIDSim(DGR.Items[i].Cells[1].Text);
                            if (LB_TRACK.Text == "1" && recID != "")
                            {
                                recidsim = recidsim + ", " + DGR.Items[i].Cells[1].Text + "(" + recID + ")";
                            }
                            else
                            {
                                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_NEXTTRACK " +
                                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                    "'" + DDL_NEXTTRACK.SelectedValue + "'," +
                                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteNonQuery();
                                iCount += 1;
                            }

                        }
                        else
                        {
                            iCountFiled += 1;
                        }
                        iCountChecklist += 1;
                    }
                    catch (Exception ex)
                    {
                        showMessage(ex.Message, "error");
                    }
                }
            }

            string sLimit = string.Empty;
            if (iCountChecklist == 0)
            {
                showMessage("Pilih data yang akan di approve!", "info");
            }
            if (LB_TRACK.Text == "1")
            {
                if (recidsim != "")
                {
                    sLimit = " dan ada data No Claim yang similar, yaitu " + recidsim.Substring(2);
                }

                if (iCount > 0)
                {
                    showMessage(iCount + " Klaim berhasil di verifikasi" + sLimit, "success");
                }
                else
                {
                    if (recidsim != "")
                    {
                        showMessage("Ada data No Claim yang similar, yaitu " + recidsim.Substring(2), "error");
                    }
                }
            }
            else if (LB_TRACK.Text == "2")
            {
                if (iCountFiled > 0)
                {
                    sLimit = ", " + iCountFiled + " Klaim melebihi batas limit approval!";
                }

                if (iCount > 0)
                {
                    showMessage(iCount + " Klaim berhasil di approve" + sLimit, "success");
                }
                else if (iCountFiled > 0)
                {
                    showMessage(sLimit.Replace(",", ""), "error");
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        private bool validateApprove(decimal dcNominal)
        {
            bool bResult = true;
            string sUserID = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            try
            {

                if (LB_TRACK.Text == "2")
                {
                    conn.QueryString = "exec SP_PR_CLAIM_APROVAL_LIMIT_GETVALIDATE " +
                                                "'" + sUserID + "'," +
                                                "'" + dcNominal.ToString() + "'";
                    conn.ExecuteQuery(120);

                    if (conn.GetRowCount() > 0)
                    {
                        bResult = true;
                    }
                    else
                    {
                        bResult = false;
                    }
                }
                else
                {
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                showMessage(ex.Message, "error");
            }
            return bResult;
        }

        private void showMessage(string sMessage, string sType)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alertmesg", "<script language=javascript> alert('" + sMessage.Replace("'", "") + "');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertmesg", "<script language=javascript> Swal.fire({position: 'top-end',type: '" + sType + "',title: '" + sMessage.Replace("'", "") + "',showConfirmButton: false,timer: 3000});</script>");
        }

        private void showConfirmation()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "sweetConfirmation();", true);
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }

            if (e.CommandName == "CetakAPL")
            {
                conn.QueryString = "select URL from SECURITY.dbo.REPORT_LIST where APP_ID = 'HO' and CODE = 377";
                conn.ExecuteQuery();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + conn.GetFieldValue("URL").ToString() + "&rc:Parameters=False&CLAIMNO=" + e.Item.Cells[1].Text + "','APLIKASI KLAIM KESEHATAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "AppAPL")
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_APPROVE_APL " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    showMessage("Aplikasi persetujuan klaim kesehatan untuk No. " + e.Item.Cells[1].Text + " berhasil di-Approve", "success");
                }
                catch (Exception ex)
                {
                    showMessage(ex.Message, "error");
                }
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }
        protected void ShowCPPanel(object sender, EventArgs e)
        {
            LB_TITLE.Text = "Clinical Pathway";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", "~/Form_Klaim/CPList.aspx");
        }
        protected void SetButtonPROSES()
        {
            BT_PROSES.Text = DDL_NEXTTRACK.SelectedItem.Text;
        }

        protected Tuple<string, string> ShowBlacklist(string claimno)
        {
            string warning = "";
            Tuple<string, string> tuple;

            conn.QueryString = "exec SP_CLAIM_MASTER_MEMBER_BLACKLIST '" + claimno + "'";
            conn.ExecuteQuery();

            string MAIN_INSURED_BLACKLISTED = conn.GetFieldValue("MAIN_INSURED_BLACKLISTED").ToString();
            string POLICY_HOLDER_BLACKLISTED = conn.GetFieldValue("POLICY_HOLDER_BLACKLISTED").ToString();
            string FLAG = conn.GetFieldValue("FLAG").ToString().ToLower();
            string SOURCE = conn.GetFieldValue("SOURCE").ToString().ToLower();
            string REGNO = conn.GetFieldValue("REGNO").ToString().ToLower();

            if ((!string.IsNullOrEmpty(FLAG) && FLAG == "black") || (!string.IsNullOrEmpty(SOURCE) && SOURCE == "fraud"))
            {
                if (warning == "")
                {
                    warning = MAIN_INSURED_BLACKLISTED;
                }
                else
                {
                    warning += MAIN_INSURED_BLACKLISTED + "<br/>";
                }
            }
            tuple = Tuple.Create(REGNO, warning);

            return tuple;
        }

        protected void BT_PROSES2_Click(object sender, EventArgs e)
        {
            int iCountBlacklist = 0;
            string warning = "", regno = "";
            string username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());

            #region Warning Blacklist
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked && cb.Visible == true)
                {
                    string claimno = DGR.Items[i].Cells[1].Text;
                    var item = ShowBlacklist(claimno);
                    if (item.Item2 != "")
                    {
                        warning = item.Item2;
                        regno = item.Item1;
                        iCountBlacklist += 1;
                        break;
                    }
                }
            }

            if (iCountBlacklist > 0)
            {
                conn.QueryString = "EXEC SP_SEND_EMAIL_NASABAH_ASKES_BERESIKO_TINGGI '" + regno + "-" + username + "'";
                conn.ExecuteQuery();

                TD_WARNING.Visible = true;
                LB_WARNING.Text = warning;
                DGR.CurrentPageIndex = 0;
                FillDGR();
                return;
            }
            else
            {
                TD_WARNING.Visible = false;
                LB_WARNING.Text = "";
                showConfirmation();
            }
            #endregion
        }
    }
}
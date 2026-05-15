using DMS.DBConnection;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static System.Net.Mime.MediaTypeNames;

namespace LIFE.Form_POS
{
    public partial class EndorsementPayOut : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string noPolis = string.Empty;
        string namaPemegang = string.Empty;
        double nominal = 0;
        decimal totalTransaksi = 0;
        public decimal totalAmounts = 0;
        public string benefitlabel = "";
        #endregion
        private string username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();

                if (Convert.ToString(LB_SEQ.Text) !="")
                   {
                    FillDGR();
                    LoadReasons();
                }
                else
                {
                    BT_SAVE.Enabled = false;
                }
                CheckTrack();
              
                username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());
            }
        }


        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR_LINK.Enabled = false;
                DGR_LINK.Columns[5].Visible = false;
                DGR_LINK.Columns[6].Visible = false;
                DGR.Enabled = false;
                DGR_LINK.Enabled = false;
                BT_SAVE.Visible = false;
                chkReason.Enabled = false;
            }
            else
            {
                chkReason.Enabled = true;
            }
        }

        protected void Setup()
        {
            string vDate = string.Empty;
            conn.QueryString = " select a.val from APPLICATION_OTHER_INFO a inner join PARAM_OTHER_SETTING b on a.CODE = b.CODE and GROUP_CODE = 'POL'  where b.code='POL00' and REGNO ='" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            vDate = conn.GetFieldValue("val").ToString();

            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString() + " - " + vDate;

            conn.QueryString = "select UNITIZE = isnull(b.UNITIZE, 0) from APPLICATION_MASTER a inner join UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_UNITIZE.Text = conn.GetFieldValue("UNITIZE").ToString();
        }

        protected void FillDGR()
        {
            if (LB_UNITIZE.Text != "0")
            {
                FillDGRLink();
                 
                FillDGRNonLink();
            }
            else
                FillDGRNonLink();
        }

        protected void FillDGRLink()
        {
            if (LB_TYPE.Text == "MBR08")
                return;

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_UNIT_LINK " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LINK.DataSource = dt;
            DGR_LINK.DataBind();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn2.QueryString = "select CODE = 'UNIT', DESCR = 'UNIT' " +
                                "union all " +
                                "select " +
                                "CODE		= 'AMOUNT', " +
                                "DESCR		= replace(b.CURRENCY_CODE, '1', 'I') " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn2.ExecuteQuery();

            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");
                txt.Text = DGR_LINK.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "");
                txtUNIT.Text = DGR_LINK.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "");

                if (DGR_LINK.Items[i].Cells[1].Text == "1")
                {
                    txt.Enabled = false;
                    txtUNIT.Enabled = false;
                }

                for (int k = 0; k < conn2.GetRowCount(); k++)
                    ddlTOGGLE.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));

                if (DGR_LINK.Items[i].Cells[2].Text.Replace("&nbsp;", "") == "0" || DGR_LINK.Items[i].Cells[2].Text.Replace("&nbsp;", "") == "")
                {
                    ddlTOGGLE.SelectedValue = "UNIT";
                    txt.Visible = false;
                }
                else
                {
                    ddlTOGGLE.SelectedValue = "AMOUNT";
                    txtUNIT.Visible = false;
                }
            }
        }

        protected void FillDGRNonLink()
        {
            if (Convert.ToInt32(LB_SEQ.Text) < 4)
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_INSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();                 
            }
            else
            {
                chkReason.Enabled = false;
                BT_SAVE.Visible = false;
                DGR_LINK.Enabled = false;
            }
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            foreach (DataRow row in dt.Rows)
            {
                if (row["DC"].ToString().Trim()=="D")
                {

                    if (row["AMOUNT"].ToString().Trim() != "0" && row["CODE"].ToString().Trim() != "501")
                    {
                        string strAmount = "";
                        decimal amounts = decimal.Parse(row["AMOUNT"].ToString().Trim(), CultureInfo.InvariantCulture);
                        if (amounts >= 0)
                        {   strAmount = "-" + row["AMOUNT"].ToString().Trim();     }
                        else
                        { strAmount = row["AMOUNT"].ToString().Trim().Replace("-", "");  }

                          benefitlabel += row["DESCR"].ToString().Trim() + "=" + strAmount + ";";
                    }
                }
            }

            LB_DESCRAMOUNT.Text = benefitlabel;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtUNIT = (TextBox)DGR.Items[i].FindControl("TXT_UNIT");
                Button btURL = (Button)DGR.Items[i].FindControl("BT_URL");

                txt.Text = DGR.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "");

                if (DGR.Items[i].Cells[1].Text == "D")
                {
                    txt.BackColor = System.Drawing.Color.Pink;
                    txt.ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Red;
                     
                }
                 
                if (DGR.Items[i].Cells[4].Text == "1")
                {
                    txt.Enabled = false;
                }

                if (DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    btURL.Visible = true;
                    btURL.Text = DGR.Items[i].Cells[5].Text;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            // === Ambil semua reason yang dicentang ===
            string reasons = "";



            totalTransaksi = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                string text = txt.Text.Replace(",", ""); // hapus koma pemisah ribuan

                if (!string.IsNullOrEmpty(text))
                {
                    totalTransaksi += decimal.Parse(text, CultureInfo.InvariantCulture);
                }
            }

            if (totalTransaksi > 100000000)
            {
                var sValue = Session["s"] as string;
                username = string.IsNullOrEmpty(sValue) ? (Session["username"] as string) : GlobalUse.GetSession(sValue);
                conn.QueryString = "EXEC SP_SEND_EMAIL_POS_NASABAH_INDIVIDU_BERESIKO_TINGGI2 '" + LB_REGNO.Text + "-" + Session["username"].ToString() + "'";
                conn.ExecuteQuery();

                string message = "Peserta utama : " + namaPemegang + " merupakan nasabah dengan transaksi mencurigakan";
                string script = "parent.parent.parent.appheader.showWarning('" + message.Replace("'", "\\'") + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowHeaderWarning", "<script>" + script + "</script>");
            }

            string notes = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    //if (Convert.ToInt32(txt.Text.Trim().Replace(",", "")) > 0)
                    //{
                    //    notes += DGR.Items[i].Cells[7].Text + "=" + txt.Text.Trim() + ";";
                    //}
                }
                catch { }
            }

            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");

                string unit, amount;
                unit = amount = "0";
                if (ddlTOGGLE.SelectedValue == "AMOUNT")
                    amount = txt.Text.Trim().Replace(",", "");
                else
                    unit = txtUNIT.Text.Trim().Replace(",", "");

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_UNIT_LINK_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + DGR_LINK.Items[i].Cells[0].Text + "'," +
                                    amount + "," +
                                    unit + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

            }

            try
            {
                conn.QueryString = "exec SP_TRANSACTION_LIST '" + LB_REGNO.Text + "', '" + LB_SEQ.Text + "'";
                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable();
                foreach (DataRow row in dt.Rows)
                {
                    notes += "_" + row["TRX_TYPE"].ToString() + "_" + row["POLICY_NO"].ToString() + "_" + row["AMOUNT"].ToString();
                }
            }
            catch { }

            totalAmounts = decimal.Parse(LB_TOTALAMOUNT.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (totalAmounts >= 0)
            {

                foreach (ListItem item in chkReason.Items)
                {
                    if (item.Selected)
                    {
                        reasons += item.Value + "_";
                    }

                }
                if (reasons.EndsWith(", "))
                {
                    reasons = reasons.Substring(0, reasons.Length - 2);
               
                }
                conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                noPolis = conn.GetFieldValue("POLICY_NO").ToString();
                namaPemegang = conn.GetFieldValue("FULLNAME").ToString();
                string reason = reasons + LB_DESCRAMOUNT.Text;
                reason = reason.Replace("--", "-");
                reason += notes;
                if (reasons != string.Empty)
                {
                    if (LB_TYPE.Text != "MBR08") // Add by Firman
                    {
                        conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARKS_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + reason +
                                        "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();

                        // === Save reasons ===
                        conn.QueryString = "EXEC SP_ENDORSEMENT_REASON_DELETE '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
                        conn.ExecuteNonQuery();
                    }
                    

                    foreach (ListItem item in chkReason.Items)
                    {
                        if (item.Selected)
                        {
                            conn.QueryString = "EXEC SP_ENDORSEMENT_REASON_UPSERT '" +
                                                LB_REGNO.Text + "'," +
                                                LB_SEQ.Text + ",'" +
                                                item.Value + "','" +
                                                GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                    }

                    //// === Kirim email otomatis ===
                    ////string noPolis = LB_POLIS.Text;
                    ////string namaPemegang = LB_NAMAPEMEGANG.Text;
                    ////string nominal = txtTotalTopup.Text.Replace(",", "");
                    //string circleDate = DateTime.Now.ToString("dd MMM yyyy");
                    //string emailBody = $@"
                    //                    <html>
                    //                    <body style='font-family:Tahoma; font-size:12px;'>
                    //                    Kepada Yth: <b>Bagian Credit Control PT Asuransi Takaful Keluarga</b><br/>
                    //                    Dari: <b>Bagian Policy Owner Service</b><br/>
                    //                    Hal: <b>Informasi Top Up Irregular Polis {noPolis} - {namaPemegang}</b><br/><br/>
                    //                    <p>Assalamu’alaikum Wr Wb,</p>
                    //                    <p>Sehubungan dengan adanya pengajuan proses Top Up Irregular dengan data sebagai berikut:</p>

                    //                    <table style='border-collapse:collapse;'>
                    //                      <tr><td style='width:120px;'>No Polis</td><td>: {noPolis}</td></tr>
                    //                      <tr><td>Nominal</td><td>: Rp {nominal}</td></tr>
                    //                      <tr><td>Circle Date</td><td>: {circleDate}</td></tr>
                    //                      <tr><td valign='top'>Reason</td><td>: {reasons}</td></tr>
                    //                    </table>

                    //                    <br/>
                    //                    <p>Maka dengan ini mohon agar dapat diproses lebih lanjut input kontribusi Top Up Irregular tersebut dengan kelengkapan berkas terlampir.</p>

                    //                    <p>Demikian disampaikan, atas perhatian dan kerjasama yang baik kami ucapkan terima kasih.</p>

                    //                    <p>Wassalamu’alaikum Wr Wb</p>

                    //                    <b>Policy Owner Service</b>
                    //                    </body>
                    //                    </html>";

                    //conn.QueryString = $@"
                    //                        EXEC msdb.dbo.sp_send_dbmail
                    //                            @profile_name = 'EXTERNAL',
                    //                            @recipients = 'endi.octaviano@mitra.takaful.com;sigit.pratomo@mitra.takaful.com;achmad.sucipto@takaful.com',
                    //                            @copy_recipients = 'endi.octaviano@gmail.com',
                    //                            @subject = 'Surrender - {noPolis} ({namaPemegang})',
                    //                            @body = N'{emailBody.Replace("'", "''")}',
                    //                            @body_format = 'HTML';
                    //                              ";
                    //conn.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, GetType(), "swalSuccess",
                    "Swal.fire({ position: 'center', icon: 'success', title: 'Berhasil!', text: 'Data telah tersimpan.', showConfirmButton: true });", true);
                }
            }
              
                else
                { 
                ScriptManager.RegisterStartupScript(this, GetType(), "swalSuccess",
                     "Swal.fire({ position: 'center', icon: 'success', title: 'Tidak Berhasil!', text: 'Data gagal tersimpan Amount minus', showConfirmButton: true });", true);

                }

            FillDGR();
 
        }


        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            totalAmounts = 0;
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_TOTAL " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[DGR.Columns.Count - 1].Text = conn.GetFieldValue("AMOUNT").ToString();
                string AmountTotal = conn.GetFieldValue("AMOUNT").ToString();
                LB_TOTALAMOUNT.Text = AmountTotal;//Convert.ToDecimal(AmountTotal, CultureInfo.InvariantCulture);
            }
        }

        protected void DDL_TOGGLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");

                if (ddlTOGGLE == (DropDownList)sender)
                {
                    TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                    TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");

                    if (ddlTOGGLE.SelectedValue == "AMOUNT")
                    {
                        txt.Visible = true;
                        txtUNIT.Visible = false;
                    }
                    else
                    {
                        txt.Visible = false;
                        txtUNIT.Visible = true;
                    }
                    return;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "URL")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.EndorsementPayoutPreview.location.href = '" + e.Item.Cells[6].Text + "';</script>");
            }
        }


        protected void DGR_LINK_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string TotalAMOUNT = " 0 ";
                for (int i = 0; i < DGR_LINK.Items.Count; i++)
                {
                    try
                    {
                        TotalAMOUNT = TotalAMOUNT + " + " + DGR_LINK.Items[i].Cells[8].Text.Trim().Replace(",", "");
                    }
                    catch { }
                }


                e.Item.Cells[DGR_LINK.Columns.Count - 3].Text = "TOTAL";
                try
                {
                    conn.QueryString = "select convert(varchar(100), convert(money, " + TotalAMOUNT + "),1)";
                    conn.ExecuteQuery();
                    e.Item.Cells[DGR_LINK.Columns.Count - 2].Text = conn.GetFieldValue(0, 0).ToString();
                }
                 
                catch { }
            }
        }

        protected string ShowBlacklist()
        {
            string warning = "";

            conn.QueryString = "exec SP_MEMBER_BLACKLIST '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            string BENEFICIARY_BLACKLISTED = conn.GetFieldValue("BENEFICIARY_BLACKLISTED").ToString();
            string MAIN_INSURED_BLACKLISTED = conn.GetFieldValue("MAIN_INSURED_BLACKLISTED").ToString();
            string POLICY_HOLDER_BLACKLISTED = conn.GetFieldValue("POLICY_HOLDER_BLACKLISTED").ToString();
            string RISKY_CUSTOMER = conn.GetFieldValue("RISKY_CUSTOMER").ToString();

            if (!string.IsNullOrEmpty(BENEFICIARY_BLACKLISTED) && !string.IsNullOrWhiteSpace(BENEFICIARY_BLACKLISTED))
            {
                warning += BENEFICIARY_BLACKLISTED + "<br/>";
            }

            if (!string.IsNullOrEmpty(MAIN_INSURED_BLACKLISTED) && !string.IsNullOrWhiteSpace(MAIN_INSURED_BLACKLISTED))
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

            if (!string.IsNullOrEmpty(POLICY_HOLDER_BLACKLISTED) && !string.IsNullOrWhiteSpace(POLICY_HOLDER_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning = POLICY_HOLDER_BLACKLISTED;
                }
                else
                {
                    warning += POLICY_HOLDER_BLACKLISTED + "<br/>";
                }
            }

            if (!string.IsNullOrEmpty(RISKY_CUSTOMER) && !string.IsNullOrWhiteSpace(RISKY_CUSTOMER))
            {
                if (warning == "")
                {
                    warning = RISKY_CUSTOMER;
                }
                else
                {
                    warning += RISKY_CUSTOMER + "<br/>";
                }
            }

            return warning;
        }

        //private void LoadReasons()
        //{
        //    conn.QueryString = "EXEC SP_ENDORSEMENT_REASON_SELECTED '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
        //    conn.ExecuteQuery();

        //    DataTable dt = conn.GetDataTable().Copy();
        //    chkReason.Items.Clear();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ListItem item = new ListItem(row["REASON_DESCR"].ToString(), row["REASON_CODE"].ToString());
        //        item.Selected = row["SELECTED"].ToString() == "1";
        //        chkReason.Items.Add(item);
        //    }
        //}

        private void LoadReasons()
        {
            // ambil reason dari database
            conn.QueryString = "EXEC SP_ENDORSEMENT_REASON_SELECTED '" + LB_REGNO.Text + "', '" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable().Copy();

            // uncheck semua dulu
            foreach (ListItem item in chkReason.Items)
                item.Selected = false;

            // cek reason yang sudah tersimpan
            foreach (DataRow row in dt.Rows)
            {
                string reason = row["REASON"].ToString().Trim();

                foreach (ListItem item in chkReason.Items)
                {
                    if (item.Value.Equals(reason, StringComparison.OrdinalIgnoreCase))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

    }
}

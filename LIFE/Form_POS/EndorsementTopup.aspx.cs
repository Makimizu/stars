using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
//using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.IO;
using System.Net.Mail;
//using Org.BouncyCastle.Math.Field;
using System.Net;

namespace LIFE.Form_POS
{
    public partial class EndorsementTopup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string noPolis = string.Empty;
        string namaPemegang = string.Empty;
        double nominal = 0;



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
                FillDGR();
                CheckTrack();
                LoadSourceOfFundsData(LB_REGNO.Text, LB_SEQ.Text);
                username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());

                string typeText = LB_TYPE.Text;
                string seqText = LB_SEQ.Text;
                if (typeText == "MBR13" && seqText=="8") 
                {
                    BT_SAVE.Enabled = false;
                }
                else {
                    BT_SAVE.Enabled = true; }
            }


            
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_TOPUP " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
            decimal totalTransaksi = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                txt.Text = DGR.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");

                string text = txt.Text.Replace(",", ""); // hapus koma pemisah ribuan

                if (!string.IsNullOrEmpty(text))
                {
                    totalTransaksi += decimal.Parse(text, CultureInfo.InvariantCulture);
                }

                nominal = nominal + Convert.ToDouble(text); ///200,000,000
            }

        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR.Enabled = false;
                BT_SAVE.Visible = false;
                chkSumberDana.Enabled = false;
                rblPendapatan.Enabled = false;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            decimal totalTransaksi = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                string text = txt.Text.Replace(",", ""); // hapus koma pemisah ribuan


                if (!string.IsNullOrEmpty(text))

                {

                    totalTransaksi += decimal.Parse(text, CultureInfo.InvariantCulture);

                }
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_TOPUP_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            string sumber = "";
            foreach (ListItem item in chkSumberDana.Items)
            {
                if (item.Selected)
                    sumber += item.Value + "_";
            }

            string pendapatan = rblPendapatan.SelectedValue;
            string remarks = sumber + pendapatan;
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

            try
            {
                if (remarks != string.Empty)
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARKS_UPSERT " +
                                       "'" + LB_REGNO.Text + "'," +
                                       "'" + LB_SEQ.Text + "'," +
                                       "'" + remarks + "'," +
                                       "'" + user + "'";
                    conn.ExecuteNonQuery();

                    if (totalTransaksi > 100000000)
                    {
                        var sValue = Session["s"] as string;
                        username = string.IsNullOrEmpty(sValue) ? (Session["username"] as string) : GlobalUse.GetSession(sValue);
                        conn.QueryString = "EXEC SP_SEND_EMAIL_POS_NASABAH_INDIVIDU_BERESIKO_TINGGI '" + LB_REGNO.Text + "-" + username + "'";
                        conn.ExecuteQuery();

                        conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
                        conn.ExecuteQuery();
                        namaPemegang = conn.GetFieldValue("FULLNAME").ToString();

                        string message = "Peserta utama : " + namaPemegang + " merupakan nasabah dengan transaksi mencurigakan";
                        string script = "parent.parent.parent.appheader.showWarning('" + message.Replace("'", "\\'") + "');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowHeaderWarning", "<script>" + script + "</script>");
                    }

                    ///////////////////////////////
                    string regno = LB_REGNO.Text;
                    int seq;
                    bool isGaji = chkSumberDana.Items.Cast<ListItem>().Any(li => li.Selected && li.Value == "Gaji");
                    bool isBonusKomisi = chkSumberDana.Items.Cast<ListItem>().Any(li => li.Selected && li.Value == "Bonus/Insentif/Komisi");
                    bool isTabungan = chkSumberDana.Items.Cast<ListItem>().Any(li => li.Selected && li.Value == "Tabungan");
                    bool isBisnisPribadi = chkSumberDana.Items.Cast<ListItem>().Any(li => li.Selected && li.Value == "Bisnis Pribadi");
                    bool isLainnya = chkSumberDana.Items.Cast<ListItem>().Any(li => li.Selected && li.Value == "Lainnya");

                    string pendapatanRange = rblPendapatan.SelectedValue;

                    conn.QueryString = "exec SP_SaveOrUpdateTopupSourceOfFunds " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + isGaji + "'," +
                                "'" + isBonusKomisi + "'," +
                                "'" + isTabungan + "'," +
                                "'" + isBisnisPribadi + "'," +
                                "'" + isLainnya + "'," +
                                "'" + pendapatanRange + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                                          
                    lblMessage.Text = "Data Top-Up dan Sumber Dana berhasil disimpan.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "swalSuccess",
                    "Swal.fire({ position: 'center', icon: 'success', title: 'Berhasil!', text: 'Data telah tersimpan.', showConfirmButton: true });", true);
                    /*
                    // penyiapan file Bukti Bayar untuk di-attach dalam mail : By Endi O
                    string fullPath = "";
                    conn.QueryString = "SELECT * FROM ARCHIEVE.dbo.LF_ARSIP where REMARK = 'Bukti Bayar' AND OWNER1 = '" + LB_REGNO.Text + "'";
                    conn.ExecuteQuery();
                    if (conn.GetRowCount() > 0)
                    {
                        fullPath = Server.MapPath("~/Upload/") + conn.GetFieldValue("NAMAFILE");
                        GlobalUse.SQLToFilePath(fullPath.Trim(), 
                            "SELECT THEFILE FROM ARCHIEVE.dbo.LF_ARSIP WHERE REMARK = 'Bukti Bayar' AND OWNER1='" + LB_REGNO.Text + "'");
                    }

                    ///put email using store procedure
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
                        conn.ExecuteQuery();

                        noPolis = conn.GetFieldValue("POLICY_NO").ToString();
                        namaPemegang = conn.GetFieldValue("FULLNAME").ToString();
                 
                        string circleDate = DateTime.Now.ToString("dd MMM yyyy");

                        // isi body HTML surat
                        string emailBody = @"
                        <html>
                        <body style='font-family:Tahoma; font-size:12px;'>
                        Kepada Yth: <b>Bagian Credit Control PT Asuransi Takaful Keluarga</b><br/>
                        Dari: <b>Bagian Policy Owner Service</b><br/>
                        Hal: <b>Informasi Top Up Irregular Polis " + noPolis + @" - " + namaPemegang + @"</b><br/><br/>
                        <p>Assalamu’alaikum Wr Wb,</p>
                        <p>Sehubungan dengan adanya pengajuan proses Top Up Irregular dengan data sebagai berikut:</p>

                        <table style='border-collapse:collapse;'>
                        <tr><td style='width:120px;'>No Polis</td><td>: " + noPolis + @"</td></tr>
                        <tr><td>Nominal</td><td>: Rp " + totalTransaksi + @"</td></tr>
                        <tr><td>Cycle Date</td><td>: " + circleDate + @"</td></tr>
                        </table>

                        <br/>
                        <p>Maka dengan ini mohon agar dapat diproses lebih lanjut input kontribusi Top Up Irregular tersebut dengan kelengkapan berkas terlampir.</p>

                        <p>Demikian disampaikan, atas perhatian dan kerjasama yang baik kami ucapkan terima kasih.</p>

                        <p>Wassalamu’alaikum Wr Wb</p>

                        <b>Policy Owner Service</b>
                        </body>
                        </html>";

                        //// panggil Database Mail
                        ////conn.QueryString = $@"
                        ////EXEC msdb.dbo.sp_send_dbmail
                        ////@profile_name = 'EXTERNAL',
                        ////@recipients = 'fardian.yusuf@mitra.takaful.com;sigit.pratomo@mitra.takaful.com;achmad.sucipto@takaful.com',
                        ////@copy_recipients = 'fardian_y@yahoo.com',
                        ////@subject = 'TOP UP IRREGULAR - {noPolis} ({namaPemegang})',
                        ////@body = N'{emailBody.Replace("'", "''")}',
                        ////@body_format = 'HTML';
                        ////";
                        //conn.QueryString = $@"
                        //EXEC msdb.dbo.sp_send_dbmail
                        //@profile_name = 'EXTERNAL',
                        //@recipients = 'endi.octaviano@mitra.takaful.com',
                        //@copy_recipients = 'endi.octaviano@gmail.com',
                        //@subject = 'TOP UP IRREGULAR - {noPolis} ({namaPemegang})',
                        //@body = N'{emailBody.Replace("'", "''")}',
                        //@body_format = 'HTML',
                        //@file_attachments = N'{fullPath}'
                        //";

                        string dari = "no-reply@takaful.com";
                        string password = "@Tk2017";
                        //string tujuan = "endi.octaviano@mitra.takaful.com,ferdiansyah.pratama@mitra.takaful.com,achmad.sucipto@takaful.com";
                        string tujuan = "endi.octaviano@mitra.takaful.com";
                        string pathFile = fullPath;
                        string smtpServer = "smtpatk.takaful.com";
                        int smtpPort = 587;

                        using (MailMessage mail = new MailMessage(dari, tujuan))
                        {
                            mail.Subject = "TOP UP IRREGULAR - " + noPolis + " (" + namaPemegang + ")";
                            mail.Body = emailBody;
                            mail.IsBodyHtml = true;
                            //mail.CC.Add(new MailAddress("blabla"));

                            if (!string.IsNullOrEmpty(pathFile))
                            {
                                Attachment att = new Attachment(pathFile);
                                mail.Attachments.Add(att);
                            }

                            using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                            {
                                smtp.Credentials = new NetworkCredential(dari, password);
                                smtp.EnableSsl = true;

                                try
                                {
                                    smtp.Send(mail);
                                    Console.WriteLine("Email berhasil dikirim.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Gagal mengirim email: " + ex.Message);
                                }
                            }
                        }

                        //conn.ExecuteNonQuery();
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    }
                    catch (Exception exMail)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "swalMail",
                            "Swal.fire({ position: 'center', icon: 'warning', title: 'Email tidak terkirim', text: '" + exMail.Message.Replace("'", "\\'") + "', showConfirmButton: true });", true);
                    }
                    */

                    ///

                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error simpan sumber dana: " + ex.Message;
            }

            FillDGR();
        }

        private string ShowBlacklist(string regno)
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER '" + regno + "'";
            conn.ExecuteQuery();

            string status1 = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string status2 = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string status3 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status4 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status5 = conn.GetFieldValue("RISK_COUNTRY").ToString();
            string status6 = conn.GetFieldValue("RISK_JOB").ToString();
            string warning = "";

            if (!string.IsNullOrEmpty(status1) && !string.IsNullOrWhiteSpace(status1))
            {
                warning = status1;
            }

            if (!string.IsNullOrEmpty(status2) && !string.IsNullOrWhiteSpace(status2))
            {
                if (warning == "")
                {
                    warning = status2;
                }
                else
                {
                    warning += "<br>" + status2;
                }
            }

            if (!string.IsNullOrEmpty(status3) && !string.IsNullOrWhiteSpace(status3))
            {
                if (warning == "")
                {
                    warning = status3;
                }
                else
                {
                    warning += "<br>" + status3;
                }
            }
            else if (!string.IsNullOrEmpty(status4) && !string.IsNullOrWhiteSpace(status4))
            {
                if (warning == "")
                {
                    warning = status4;
                }
                else
                {
                    warning += "<br>" + status4;
                }
            }

            if (!string.IsNullOrEmpty(status5) && !string.IsNullOrWhiteSpace(status5))
            {
                if (warning == "")
                {
                    warning = status5;
                }
                else
                {
                    warning += "<br>" + status5;
                }
            }

            if (!string.IsNullOrEmpty(status6) && !string.IsNullOrWhiteSpace(status6))
            {
                if (warning == "")
                {
                    warning = status6;
                }
                else
                {
                    warning += "<br>" + status6;
                }
            }

            return warning;
        }

        private void LoadSourceOfFundsData(string regno, string seq)
        { 
           
            conn.QueryString = "SP_GetTopupSourceOfFunds '" + regno + "','" + seq + "'";
            conn.ExecuteQuery(); 
            chkSumberDana.ClearSelection();
            if (conn.GetRowCount() > 0)
            {
                bool isGaji = Convert.ToBoolean(conn.GetFieldValue("isGaji"));
                bool isBonusKomisi = Convert.ToBoolean(conn.GetFieldValue("isBonusKomisi"));
                bool isTabungan = Convert.ToBoolean(conn.GetFieldValue("isTabungan"));
                bool isBisnisPribadi = Convert.ToBoolean(conn.GetFieldValue("isBisnisPribadi"));
                bool isLainnya = Convert.ToBoolean(conn.GetFieldValue("isLainnya"));

                if (isGaji == true)
                    chkSumberDana.Items.FindByValue("Gaji").Selected = true;
                if (isBonusKomisi == true)
                    chkSumberDana.Items.FindByValue("Bonus/Insentif/Komisi").Selected = true;
                if (isTabungan == true)
                    chkSumberDana.Items.FindByValue("Tabungan").Selected = true;
                if (isBisnisPribadi == true)
                    chkSumberDana.Items.FindByValue("Bisnis Pribadi").Selected = true;
                if (isLainnya == true)
                    chkSumberDana.Items.FindByValue("Lainnya").Selected = true;

                // 2. Radio Button List (rblPendapatan)
                string selectedRange = conn.GetFieldValue("PendapatanRange").ToString();
                rblPendapatan.ClearSelection();
                ListItem rangeItem = rblPendapatan.Items.FindByValue(selectedRange);
                if (rangeItem != null)
                {
                    rangeItem.Selected = true;
                }
            }
        }      
    }
}
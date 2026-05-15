using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization;

namespace LIFE.Form_POS
{
    public partial class EndorsementBenefitCyclePayout : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                FillDGRBenefit();
                CheckTrack();

                decimal saldoBalance = GetSaldoBalance(LB_REGNO.Text);
                ViewState["saldoBalance"] = saldoBalance;

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

        protected void FillDGRBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_PAYOUT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
                if (DGR_BENEFIT.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }

            try
            {
                LB_ALERT.Text = DGR_BENEFIT.Items[0].Cells[3].Text.Replace("&nbsp;", "");
            }
            catch { }
        }
        private decimal GetSaldoBalance(string regno)
        {
            conn.QueryString = "exec SP_GET_SALDO_BALANCE_BALANCE '" + regno + "'";
            conn.ExecuteQuery();

            decimal saldoBalance = 0;

            if (conn.GetRowCount() > 0)
            {
                object val = conn.GetFieldValue(0, "BALANCE");

                if (val != null && val != DBNull.Value)
                {
                    saldoBalance = Convert.ToDecimal(val);
                }
            }

            return Math.Round(saldoBalance, 2);
        }


        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR_BENEFIT.Enabled = false;
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            decimal saldoAwal = GetSaldoBalance(LB_REGNO.Text);
            decimal sisaSaldo = saldoAwal;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "debugSaldo",
                "console.log('SaldoAwal=" + saldoAwal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "');",
                true
            );

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
                string tobepaid = cb.Checked ? "1" : "0";

                string desc = DGR_BENEFIT.Items[i].Cells[4].Text.ToUpper();

                decimal persen = 0;
                decimal.TryParse(DGR_BENEFIT.Items[i].Cells[5].Text.Trim(), out persen);

                decimal payout = 0;

                bool isTahapan = //DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "10" ||
                                 //DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "15" ||
                                 //DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "20" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "25" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "35" ||
                                 //DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "40" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "50" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "100";

                bool isTahapanParent = DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "10" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "15" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "20" ||
                                 DGR_BENEFIT.Items[i].Cells[5].Text.Trim() == "40";



                // =========================
                // JIKA TAHAPAN (KECUALI MASUK PT)
                // =========================
                if (isTahapan)
                {
                    tobepaid = "0";

                    decimal parentValue = 0;
                    string debugMsg = "";

                    // ===============================
                    // VALIDASI PARENT
                    // ===============================
                    if (cb.Checked && i > 0)
                    {
                        string persenParent = DGR_BENEFIT.Items[i - 1].Cells[5].Text.Trim();

                        bool parentIsTahapan =
                            //persenParent == "10" ||
                            //persenParent == "15" ||
                            //persenParent == "20" ||
                            persenParent == "25" ||
                            persenParent == "35" ||
                            //persenParent == "40" ||
                            persenParent == "50" ||
                            persenParent == "100";

                        if (parentIsTahapan)
                        {
                            string rawValue = DGR_BENEFIT.Items[i - 1].Cells[6].Text.Trim();

                            rawValue = rawValue
                                .Replace(".", "")
                                .Replace(",", ".");

                            decimal.TryParse(rawValue, NumberStyles.Any, CultureInfo.InvariantCulture, out parentValue);

                            // 🔥 LOG PARENT
                            debugMsg += "ROW " + i +
                                        " | ParentRaw=" + rawValue +
                                        " | ParentParsed=" + parentValue + "\\n";

                            if (parentValue == 0)
                            {
                                cb.Checked = false;
                                DGR_BENEFIT.Items[i].Cells[6].Text = "0";

                                debugMsg += "BLOCKED karena parent = 0\\n";

                                ScriptManager.RegisterStartupScript(
                                    this,
                                    GetType(),
                                    "alert",
                                    "alert('Tahapan sebelumnya harus dibayar dulu!');",
                                    true
                                );

                                continue;
                            }
                        }
                    }

                    // ===============================
                    // HITUNG PAYOUT
                    // ===============================
                    if (cb.Checked)
                    {
                        if (persen == 100)
                            payout = sisaSaldo;
                        else
                            payout = sisaSaldo * (persen / 100m);

                        payout = Math.Round(payout, 2);

                        DGR_BENEFIT.Items[i].Cells[6].Text =
                            payout.ToString("N2", new CultureInfo("id-ID"));

                        sisaSaldo -= payout;

                        tobepaid = "1";
                    }
                    else
                    {
                        payout = 0;
                        DGR_BENEFIT.Items[i].Cells[6].Text = "0";
                    }

                    // 🔥 LOG PAYOUT
                    debugMsg += "ROW " + i +
                                " | Persen=" + persen +
                                " | Payout=" + payout +
                                " | SisaSaldo=" + sisaSaldo + "\\n";

                    // ===============================
                    // EXECUTE QUERY
                    // ===============================
                    conn.QueryString =
                        "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_PAYOUT_UPSERT_BALANCE " +
                        "'" + LB_REGNO.Text + "'," +
                        LB_SEQ.Text + "," +
                        "'" + LB_TYPE.Text + "'," +
                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                        tobepaid + "," +
                        "'" + DGR_BENEFIT.Items[i].Cells[2].Text + "'," +
                        payout.ToString(CultureInfo.InvariantCulture) + "," +
                        "'" + persen + "'," +
                        "'" + desc.Replace("<B>", "")
                                 .Replace("</B>", "")
                                 .Replace("TAHAPAN :", "")
                                 .Trim() + "'," +
                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    // ===============================
                    // 🔥 KIRIM LOG KE BROWSER (INSPECT)
                    // ===============================
                    ScriptManager.RegisterStartupScript(
                        this,
                        GetType(),
                        "consoleLog" + i,
                        "console.log('" + debugMsg.Replace("'", "\\'") + "');",
                        true
                    );
                }


                // =========================
                // Tahapan Parent
                // =========================
                else if (isTahapanParent)
                {
                    string parent = "Parent";
                    Console.WriteLine(parent);

                    if (cb.Checked)
                    {
                      
                        tobepaid = "1";
                    }
                    else
                    {

                        tobepaid = "0";
                    }

                    conn.QueryString =
                       "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_PAYOUT_UPSERT_PARENT " +
                        "'" + LB_REGNO.Text + "'," +
                        LB_SEQ.Text + "," +
                        "'" + LB_TYPE.Text + "'," +
                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                        tobepaid + "," +
                        "'" + DGR_BENEFIT.Items[i].Cells[2].Text + "'," +
                        payout.ToString(CultureInfo.InvariantCulture) + "," +
                        "'" + persen + "'," +
                        "'" + desc.Replace("<B>", "")
                                 .Replace("</B>", "")
                                 .Replace("TAHAPAN :", "")
                                 .Trim() + "'," +
                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                }
                    
                else
                {
                    conn.QueryString =
                    "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_PAYOUT_UPSERT " +
                    "'" + LB_REGNO.Text + "'," +
                    LB_SEQ.Text + "," +
                    "'" + LB_TYPE.Text + "'," +
                    "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                    tobepaid + "," +
                    "'" + DGR_BENEFIT.Items[i].Cells[2].Text + "'," +
                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                }

                conn.ExecuteNonQuery();
            }

            FillDGRBenefit();
        }




        //protected void CB_CheckedChanged(object sender, EventArgs e)
        //{

        //    for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
        //    {
        //        CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
        //        if (cb == (CheckBox)sender)
        //        {
        //            string tobepaid = "0";
        //            if (cb.Checked)
        //                tobepaid = "1";

        //            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_PAYOUT_UPSERT " +
        //                                "'" + LB_REGNO.Text + "'," +
        //                                LB_SEQ.Text + "," +
        //                                "'" + LB_TYPE.Text + "'," +
        //                                "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
        //                                tobepaid + "," +
        //                                "'" + DGR_BENEFIT.Items[i].Cells[2].Text + "'," +
        //                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //            conn.ExecuteNonQuery();
        //            FillDGRBenefit();
        //            return;
        //        }
        //    }
        //}
    }
}
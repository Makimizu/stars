using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace HLP.Form_Company
{
    public partial class CompanyBranch : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_PROPINSI order by 1";
            conn.ExecuteQuery();
            DDL_PROPCAB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPCAB.Items.Add(new ListItem(conn.GetFieldValue(i, "DESCR").ToString(), conn.GetFieldValue(i, "CODE").ToString()));

            FillDGRCabang(LB_CODE.Text);

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_NEWCAB.Visible = false;
                BT_SAVE_CAB.Visible = false;
                TXT_NAMACAB.Enabled = false;
                TXT_KODECAB2.Enabled = false;
                TXT_ALAMATCAB1.Enabled = false;
                TXT_ALAMATCAB2.Enabled = false;
                TXT_PHNCAB.Enabled = false;
                TXT_FAXCAB.Enabled = false;
                TXT_EMAILCAB.Enabled = false;
                TXT_PICCAB.Enabled = false;
                TXT_PICTITLECAB.Enabled = false;
                TXT_PICEMAILCAB.Enabled = false;
                TXT_KOTAMADYACAB.Enabled = false;
                DDL_PROPCAB.Enabled = false;
                TXT_KODEWIL.Enabled = false;
                TXT_KODEPOSCAB.Enabled = false;
            }
        }

        protected void FillDGRCabang(string code)
        {
            conn.QueryString = "select BRANCH_CODE,NAMA_CABANG from BRANCH where COMPANY_CODE='" + code + "' and NAMA_CABANG like '%" + TXT_BRANCH_SEARCH.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();

            LB_BRANCH.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString().ToUpper(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void LoadCabangRecord(string code)
        {
            conn.QueryString = "select * from BRANCH where BRANCH_CODE='" + code + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TXT_KDCAB.Text = conn.GetFieldValue(0, "BRANCH_CODE").ToString();
            TXT_KODECAB2.Text = conn.GetFieldValue(0, "KODE_CAB").ToString();
            TXT_NAMACAB.Text = conn.GetFieldValue(0, "NAMA_CABANG").ToString();
            TXT_ALAMATCAB1.Text = conn.GetFieldValue(0, "BRANCH_ADDRESS1").ToString();
            TXT_ALAMATCAB2.Text = conn.GetFieldValue(0, "BRANCH_ADDRESS2").ToString();
            TXT_PHNCAB.Text = conn.GetFieldValue(0, "BRANCH_PHONE").ToString();
            TXT_FAXCAB.Text = conn.GetFieldValue(0, "BRANCH_FAX").ToString();
            TXT_EMAILCAB.Text = conn.GetFieldValue(0, "BRANCH_EMAIL").ToString();
            TXT_PICCAB.Text = conn.GetFieldValue(0, "BRANCH_PIC").ToString();
            TXT_PICTITLECAB.Text = conn.GetFieldValue(0, "BRANCH_PIC_TITLE").ToString();
            TXT_PICEMAILCAB.Text = conn.GetFieldValue(0, "BRANCH_PIC_EMAIL").ToString();
            TXT_KODEPOSCAB.Text = conn.GetFieldValue(0, "BRANCH_ZIP").ToString();
            TXT_KOTAMADYACAB.Text = conn.GetFieldValue(0, "KOTAMADYA").ToString();
            DDL_PROPCAB.SelectedValue = conn.GetFieldValue(0, "PROPINSI").ToString();
            TXT_KODEWIL.Text = conn.GetFieldValue(0, "KODE_WIL").ToString();

            FillBranchACC();
            FillBranchCorresponden();
        }

        protected void FillBranchACC()
        {
            conn.QueryString = "select * from V_BRANCH_BANK_ACCOUNT where BRANCH_CODE='" + TXT_KDCAB.Text + "' order by convert(int,TIPE)";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ACCOUNT.DataSource = dt;
            DGR_ACCOUNT.DataBind();

            conn.QueryString = "select CODE,BANK from FINANCE.dbo.PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_ACCOUNT.Items.Count; i++)
            {
                TextBox txtaccno = (TextBox)DGR_ACCOUNT.Items[i].FindControl("TXT_ACCNO0");
                TextBox txtaccname = (TextBox)DGR_ACCOUNT.Items[i].FindControl("TXT_NAMAREK0");
                DropDownList ddlaccbank = (DropDownList)DGR_ACCOUNT.Items[i].FindControl("DDL_BANK0");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtaccno.Enabled = false;
                    txtaccname.Enabled = false;
                    ddlaccbank.Enabled = false;
                }

                ddlaccbank.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlaccbank.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                txtaccno.Text = DGR_ACCOUNT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtaccname.Text = DGR_ACCOUNT.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                try
                {
                    ddlaccbank.SelectedValue = DGR_ACCOUNT.Items[i].Cells[4].Text;
                }
                catch { }
            }
        }


        private void FillBranchCorresponden()
        {

            conn.QueryString = "select * from V_BRANCH_CORRESPONDENCE where BRANCH_CODE='" + TXT_KDCAB.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_KORESPONDEN.DataSource = dt;
            DGR_KORESPONDEN.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_SALUTATION";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_KORESPONDEN.Items.Count; i++)
            {
                DropDownList ddlsalut = (DropDownList)DGR_KORESPONDEN.Items[i].FindControl("DDL_PICSALUTATION");
                TextBox txtname = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICNAMA");
                TextBox txttitle = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICTITLE");
                TextBox txtphone = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICPHONE");
                TextBox txtemail = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICEMAIL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtname.Enabled = false;
                    txttitle.Enabled = false;
                    txtphone.Enabled = false;
                    txtemail.Enabled = false;
                    ddlsalut.Enabled = false;
                }


                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlsalut.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }


                try
                {
                    ddlsalut.SelectedValue = DGR_KORESPONDEN.Items[i].Cells[3].Text;
                }
                catch { }

                txtname.Text = DGR_KORESPONDEN.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txttitle.Text = DGR_KORESPONDEN.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtphone.Text = DGR_KORESPONDEN.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtemail.Text = DGR_KORESPONDEN.Items[i].Cells[6].Text.Replace("&nbsp;", "");
            }

        }

        protected void LB_BRANCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCabangRecord(LB_BRANCH.SelectedValue);
        }

        protected void TXT_BRANCH_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGRCabang(LB_CODE.Text);
        }

        protected void BT_SAVE_CAB_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_BRANCH_UPSERT " +
                                    "'" + TXT_KDCAB.Text + "'," +
                                    "'" + LB_CODE.Text.Trim() + "'," +
                                    "'" + TXT_KODECAB2.Text.Trim() + "'," +
                                    "'" + TXT_KODEWIL.Text.Trim() + "'," +
                                    "'" + TXT_NAMACAB.Text.Trim() + "'," +
                                    "'" + TXT_ALAMATCAB1.Text.Trim() + "'," +
                                    "'" + TXT_ALAMATCAB2.Text.Trim() + "'," +
                                    "'" + TXT_PHNCAB.Text.Trim() + "'," +
                                    "'" + TXT_FAXCAB.Text.Trim() + "'," +
                                    "'" + TXT_EMAILCAB.Text.Trim() + "'," +
                                    "'" + TXT_PICCAB.Text.Trim() + "'," +
                                    "'" + TXT_PICTITLECAB.Text.Trim() + "'," +
                                    "'" + TXT_PICEMAILCAB.Text.Trim() + "'," +
                                    "'" + TXT_KODEPOSCAB.Text.Trim() + "'," +
                                    "'" + TXT_KOTAMADYACAB.Text.Trim() + "'," +
                                    "'" + DDL_PROPCAB.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                TXT_KDCAB.Text = conn.GetFieldValue("BRANCH_CODE").ToString();

                SaveDGRACCOUNT();
                SaveDGRKORESPONDEN();
                LoadCabangRecord(TXT_KDCAB.Text);
                FillDGRCabang(LB_CODE.Text);

            }
            catch (SystemException ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void SaveDGRACCOUNT()
        {
            for (int i = 0; i < DGR_ACCOUNT.Items.Count; i++)
            {
                TextBox txtaccno = (TextBox)DGR_ACCOUNT.Items[i].FindControl("TXT_ACCNO0");
                TextBox txtaccnama = (TextBox)DGR_ACCOUNT.Items[i].FindControl("TXT_NAMAREK0");
                DropDownList ddlaccbank = (DropDownList)DGR_ACCOUNT.Items[i].FindControl("DDL_BANK0");

                if (txtaccno.Text.Trim() != "")
                {
                    try
                    {
                        conn.QueryString = "exec SP_BRANCH_BANK_ACCOUNT_UPSERT " +
                                            "'" + TXT_KDCAB.Text.Trim() + "'," +
                                            "'" + DGR_ACCOUNT.Items[i].Cells[0].Text + "'," +
                                            "'" + txtaccno.Text.Trim() + "'," +
                                            "'" + txtaccnama.Text.Trim().Replace("'", "`") + "'," +
                                            "'" + ddlaccbank.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
        }

        protected void SaveDGRKORESPONDEN()
        {
            for (int i = 0; i < DGR_KORESPONDEN.Items.Count; i++)
            {
                TextBox txtnama = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICNAMA");
                TextBox txttitle = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICTITLE");
                TextBox txtphone = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICPHONE");
                TextBox txtemail = (TextBox)DGR_KORESPONDEN.Items[i].FindControl("TXT_PICEMAIL");
                DropDownList ddlsalut = (DropDownList)DGR_KORESPONDEN.Items[i].FindControl("DDL_PICSALUTATION");

                if (txtnama.Text.Trim() != "")
                {
                    try
                    {
                        conn.QueryString = "exec SP_BRANCH_CORRESPONDENCE_UPSERT " +
                                            "'" + TXT_KDCAB.Text.Trim() + "'," +
                                            "'" + DGR_KORESPONDEN.Items[i].Cells[0].Text + "'," +
                                            "'" + txtnama.Text.Trim() + "'," +
                                            "'" + ddlsalut.SelectedValue + "'," +
                                            "'" + txttitle.Text.Trim() + "'," +
                                            "'" + txtphone.Text.Trim() + "'," +
                                            "'" + txtemail.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
        }

        protected void BT_NEWCAB_Click(object sender, EventArgs e)
        {
            TXT_KDCAB.Text = "";
            TXT_KODECAB2.Text = "";
            TXT_NAMACAB.Text = "";
            TXT_ALAMATCAB1.Text = "";
            TXT_ALAMATCAB2.Text = "";
            TXT_PHNCAB.Text = "";
            TXT_FAXCAB.Text = "";
            TXT_EMAILCAB.Text = "";
            TXT_PICCAB.Text = "";
            TXT_PICTITLECAB.Text = "";
            TXT_PICEMAILCAB.Text = "";
            TXT_KODEPOSCAB.Text = "";
            TXT_KOTAMADYACAB.Text = "";
            TXT_KODEWIL.Text = "";
            DDL_PROPCAB.SelectedIndex = 0;


            FillBranchACC();
            FillBranchCorresponden();
        }

        protected void DGR_ACCOUNT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtacc = (TextBox)e.Item.FindControl("TXT_ACCNO0");
            TextBox txtaccnama = (TextBox)e.Item.FindControl("TXT_NAMAREK0");
            DropDownList ddlaccbank = (DropDownList)e.Item.FindControl("DDL_BANK");

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "update BRANCH_BANK_ACCOUNT set " +
                                        "ACCNO = '" + txtacc.Text.Trim() + "'," +
                                        "ACCNAME = '" + txtaccnama.Text.Trim() + "'," +
                                        "ACCBANK = '" + ddlaccbank.SelectedValue + "' " +
                                        "where " +
                                        "BRANCH_CODE='" + TXT_KDCAB.Text + "' " +
                                        "and ROWID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from BRANCH_BANK_ACCOUNT where " +
                                        "BRANCH_CODE='" + TXT_KDCAB.Text + "' " +
                                        "and ROWID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { return; }
                FillBranchACC();
            }

            if (e.CommandName == "Add")
            {
                try
                {
                    conn.QueryString = "insert into BRANCH_BANK_ACCOUNT values (" +
                                        "NEWID()," +
                                        "'" + TXT_KDCAB.Text + "'," +
                                        "''," +
                                        "''," +
                                        "'')";
                    conn.ExecuteNonQuery();
                }
                catch { return; }
                FillBranchACC();
            }
        }
    }
}
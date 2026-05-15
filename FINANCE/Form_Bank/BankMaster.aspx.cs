using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;


namespace FINANCE.Form_Bank
{
    public partial class BankMaster : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string sql;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string s = Session["s"].ToString();
            }
            catch
            {
                Response.Redirect("../Standard/FailedSession.aspx");
            }

            LB_ERROR.Text = "";
            FillRecordGrid();
            if (!IsPostBack)
            {
                Setup();
                GlobalUse.SetReadOnly(Page, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"), "MasterBank.aspx.cs");
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_BANK order by 2";
            conn.ExecuteQuery();
            DDL_GROUPBANK.Items.Clear();
            DDL_GROUPBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GROUPBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillRecordGrid();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            if (TXT_CODE.Text == "")
            {
                InsertBank(TXT_NAMA_BANK.Text, TXT_KOTA.Text, TXT_CABANG.Text, TXT_KODE_CABANG.Text, TXT_CLEARING_CODE.Text, TXT_RTGS_CODE.Text);
            }
            else
            {
                UpdateBank(TXT_CODE.Text, TXT_NAMA_BANK.Text, TXT_KOTA.Text, TXT_CABANG.Text, TXT_KODE_CABANG.Text, TXT_CLEARING_CODE.Text, TXT_RTGS_CODE.Text);
            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        protected void FillRecordGrid()
        {
            string query = "SELECT A.*,[GROUP BANK]=ISNULL(B.DESCR,'') FROM PARAM_TBL_BANK A " +
                            "LEFT JOIN dbo.PR_BANK B ON A.BANK_GROUP=B.CODE " +
                            "WHERE " +
                            "BANK like '%" +TXT_SEARCH.Text.Trim()+ "%' " +
                            "ORDER BY A.BANK_GROUP,BANK";

            conn.QueryString = query;
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
        }

        protected void InsertBank(string bank, string city, string branch, string branchCode, string clearingCode, string rtgsCode)
        {
            try
            {
                string bank_group = DDL_GROUPBANK.SelectedValue == string.Empty ? "NULL" : "'" + DDL_GROUPBANK.SelectedValue + "'";

                conn.QueryString = "EXEC SP_MASTER_BANK_INSERT '" + bank + "','" + city + "','" + branch + "','" + branchCode + "','" + clearingCode + "','" + rtgsCode + "'," + bank_group;
                conn.ExecuteQuery();
                DataTable dt = new DataTable();
                dt = conn.GetDataTable();

                if (dt.Rows.Count > 0)
                {
                    string x = dt.Rows[0]["ReturnValue"].ToString();

                    if (x == "1")
                    {
                        LB_ERROR.Text = "Insert Sukses";
                        LB_ERROR.ForeColor = System.Drawing.Color.Blue;

                        FillRecordGrid();
                    }
                    else
                    {
                        LB_ERROR.Text = "Insert Gagal, Nama Bank Sudah Ada";
                        LB_ERROR.ForeColor = System.Drawing.Color.Red;
                    }
                }


            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }

        protected void UpdateBank(string code, string bank, string city, string branch, string branchCode, string clearingCode, string rtgsCode)
        {
            try
            {
                if (code == "" && bank == "" && city == "" && branch == "" && branchCode == "" && clearingCode == "" && rtgsCode == "")
                {
                    LB_ERROR.Text = "Insert Gagal";
                    LB_ERROR.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string bank_group = DDL_GROUPBANK.SelectedValue == string.Empty ? "NULL" : "'" + DDL_GROUPBANK.SelectedValue + "'";
                    sql = "UPDATE PARAM_TBL_BANK SET BANK = '" + bank.ToUpper() +
                        "', CITY = '" + city.ToUpper() +
                        "', BRANCH = '" + branch.ToUpper() +
                        "', BRANCH_CODE = '" + branchCode.ToUpper() +
                        "', CLEARING_CODE = '" + clearingCode.ToUpper() +
                        "', RTGS_CODE = '" + rtgsCode.ToUpper() +
                        "', BANK_GROUP = " + bank_group +
                        " WHERE CODE = '" + code + "'";

                    conn.QueryString = sql;
                    conn.ExecuteNonQuery();

                    LB_ERROR.Text = "Update Sukses";
                    LB_ERROR.ForeColor = System.Drawing.Color.Blue;

                    FillRecordGrid();
                }
            }
            catch
            {
                LB_ERROR.Text = "Insert Gagal";
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ResetValue()
        {
            TXT_NAMA_BANK.Text = "";
            TXT_KOTA.Text = "";
            TXT_CABANG.Text = "";
            TXT_KODE_CABANG.Text = "";
            TXT_CLEARING_CODE.Text = "";
            TXT_RTGS_CODE.Text = "";
            TXT_CODE.Text = "";
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                conn.QueryString = "SELECT CODE,BANK,CITY,BRANCH,BRANCH_CODE,CLEARING_CODE,RTGS_CODE,BANK_GROUP = ISNULL(BANK_GROUP,'') FROM PARAM_TBL_BANK WHERE CODE='" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();
                DDL_GROUPBANK.SelectedValue = conn.GetFieldValue("BANK_GROUP").ToString();
                TXT_NAMA_BANK.Text = conn.GetFieldValue("BANK").ToString();
                TXT_KOTA.Text = conn.GetFieldValue("CITY").ToString();
                TXT_CABANG.Text = conn.GetFieldValue("BRANCH").ToString();
                TXT_KODE_CABANG.Text = conn.GetFieldValue("BRANCH_CODE").ToString();
                TXT_CLEARING_CODE.Text = conn.GetFieldValue("CLEARING_CODE").ToString();
                TXT_RTGS_CODE.Text = conn.GetFieldValue("RTGS_CODE").ToString();
                TXT_CODE.Text = conn.GetFieldValue("CODE").ToString();
            }
        }

        protected void BTN_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillRecordGrid();
        }
    }
}
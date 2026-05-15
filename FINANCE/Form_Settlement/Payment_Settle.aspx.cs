using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class Payment_Settle : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_MODE.Text = Request.QueryString["MODE"];
                LB_CODE.Text = Request.QueryString["CODE"];
                LB_AMOUNT.Text = Request.QueryString["AMOUNT"];
                Setup();
                LoadRecords();
            }
        }

        protected void Setup()
        {
            if (LB_MODE.Text == "PAY")
            {
                FillDDLBeneficiary();
                ShowBenefDetail();
            }
            FillDGRInfo();
        }

        protected void FillDDLBeneficiary()
        {
            conn.QueryString = "select distinct ACC_NO, ACC_NAME from V_SETTLEMENT_MASTER_BANK_CHARGE_UNSETTLE " +
                                "where " +
                                "REKAPID = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEF.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGRInfo()
        {
            string ext = "";
            if (LB_MODE.Text == "PAY")
                ext = DDL_BENEF.SelectedValue;

            conn.QueryString = "exec SP_PAYMENT_RK " +
                                "'" + LB_MODE.Text + "'," +
                                "'" + LB_CODE.Text + ext + "'," +
                                "'" + LB_AMOUNT.Text.Replace(",","") + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO.DataSource = dt;
            DGR_INFO.DataBind();
        }

        protected void FillDGRRK()
        {
            string where = "";

            if (TXT_RK_DESCR.Text.Trim() != "")
                where = where + "and DESCR like '%" + TXT_RK_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "a.NOREK, " +
                                "a.TRXID, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.DEBET),1),'.00',''), " +
                                "POST_DATE = convert(varchar(20),a.POST_DATE,106), " +
                                "a.DESCR " +
                                "from V_REKENING_JURNAL a " +
                                "left join REKENING_JURNAL_CREDIT b on a.NOREK=b.NOREK and a.TRXID=b.TRXID " +
                                "where " +
                                "a.DEBET > 0 and b.NOREK is null " + where +
                                " order by a.POST_DATE";
            conn.ExecuteQuery();

            string records = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_RK.DataSource = dt;
            DGR_RK.DataBind();

            for (int i = 0; i < DGR_RK.Items.Count; i++)
            {
                Button btSTL = (Button)DGR_RK.Items[i].FindControl("BT_STL");
                btSTL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SETTLE ?')){return false;};");
            }

            string SQL = "select " +
                            "AMOUNT = replace(convert(varchar(100),convert(money,SUM(DEBET)),1),'.00','') " +
                            "from V_REKENING_JURNAL a " +
                            "left join REKENING_JURNAL_CREDIT b on a.NOREK=b.NOREK and a.TRXID=b.TRXID " +
                            "where " +
                            "a.DEBET > 0 and b.NOREK is null " + where;

            ShowResult(records, SQL);
        }

        protected void ShowResult(string records, string SQL_Amount)
        {
            conn.QueryString = SQL_Amount;
            conn.ExecuteQuery();

            LB_RECORDS.Text = records;
            LB_AMOUNTS.Text = conn.GetFieldValue(0, 0).ToString();
        }

        protected void LoadRecords()
        {
            TR_RK.Visible = false;

            if (LB_MODE.Text == "PAY")
            {
                TR_RK.Visible = true;
                //FillDGRRK();
            }
        }

        protected void BT_RK_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGRRK();
        }

        protected void DGR_RK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                LB_ERROR.Text = "";

                if (LB_MODE.Text == "PAY")
                {
                    try
                    {
                        conn.QueryString = "exec SP_REKENING_JURNAL_CREDIT_INSERT " +
                                            "'" + e.Item.Cells[1].Text + "'," +
                                            "'" + e.Item.Cells[2].Text + "'," +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + DDL_BENEF.SelectedValue + "'," +
                                            "'" + LB_ACCBANKCODE.Text + "'," +
                                            "'" + DDL_BENEF.SelectedItem.Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery(500000);
                    }
                    catch(System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }

                    try
                    {
                        DDL_BENEF.Items.RemoveAt(DDL_BENEF.SelectedIndex);
                        FillDGRInfo();
                    }
                    catch { }
                    FillDGRRK();
                }
            }
        }

        protected void DDL_BENEF_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowBenefDetail();
            FillDGRInfo();
        }

        protected void ShowBenefDetail()
        {
            conn.QueryString = "select " +
                                "b.CODE, " +
                                "b.BANK " +
                                "from SETTLEMENT_MASTER_BANK_CHARGE a " +
                                "inner join PARAM_TBL_BANK b on a.ACC_BANK=b.CODE collate database_default " +
                                "where " +
                                "a.REKAPID = '" + LB_CODE.Text + "' " +
                                "and a.ACC_NO = '" + DDL_BENEF.SelectedValue + "'";
            conn.ExecuteQuery();

            LB_ACCNO.Text = DDL_BENEF.SelectedValue;
            LB_ACCBANK.Text = conn.GetFieldValue("BANK").ToString();
            LB_ACCBANKCODE.Text = conn.GetFieldValue("CODE").ToString();
        }
    }
}
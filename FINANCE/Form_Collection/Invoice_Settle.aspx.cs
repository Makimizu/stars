using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Invoice_Settle : System.Web.UI.Page
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
                Setup();
                LoadRecords();
            }
        }

        protected void FillDGRInfo()
        {
            conn.QueryString = "exec SP_INVOICE_RK " +
                                "'" + LB_MODE.Text + "'," +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO.DataSource = dt;
            DGR_INFO.DataBind();
        }

        protected void Setup()
        {
            FillDGRInfo();

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from INVOICE_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLInvoice();
        }

        protected void FillDDLInvoice()
        {
            conn.QueryString = "select INVOICE_TYPE,DESCR from PARAM_INVOICE_TYPE where APP_ID = '" + DDL_APP.SelectedValue + "'";
            conn.ExecuteQuery(150000);
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void ShowResult(string records, string SQL_Amount)
        {
            conn.QueryString = SQL_Amount;
            conn.ExecuteQuery(150000);

            LB_RECORDS.Text = records;
            LB_AMOUNTS.Text = conn.GetFieldValue(0, 0).ToString();

            TBL_RESULT.Visible = true;
        }

        protected void FillDGRINV()
        {
            TBL_RESULT.Visible = false;
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_VA.Text.Trim() != "")
                where = where + " and a.VA='" + TXT_VA.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.INVOICE_TYPE='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "VA, " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AGING, " +
                                "INVOICE_TYPE_DESCR, " +
                                "ENABLE_DETAIL " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "OUTSTANDING > 0 " + where +
                                " order by a.CUSTOMER_NAME, a.INVOICE_DATE";
            conn.ExecuteQuery(150000);

            string records = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INV.DataSource = dt;
            DGR_INV.DataBind();

            for (int i = 0; i < DGR_INV.Items.Count; i++)
            {
                Button btSTL = (Button)DGR_INV.Items[i].FindControl("BT_STL");
                btSTL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SETTLE ?')){return false;};");

                if (DGR_INV.Items[i].Cells[2].Text == "1")
                {
                    btSTL.Text = "D";
                    btSTL.BackColor = System.Drawing.Color.Gray;
                }
            }

            string SQL = "select " +
                            "AMOUNT = replace(convert(varchar(100),convert(money,SUM(OUTSTANDING)),1),'.00','') " +
                            "from V_INVOICE_MASTER a " +
                            "where " +
                            "OUTSTANDING > 0 " + where;

            ShowResult(records, SQL);
        }

        protected void FillDGRRK()
        {
            TBL_RESULT.Visible = false;
            string where = "";

            if (TXT_RK_DESCR.Text.Trim() != "")
                where = where + "and DESCR like '%" + TXT_RK_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_BALANCE1.Text.Trim() != "")
                where = where + " and a.BALANCE >= " + TXT_BALANCE1.Text.Trim().Replace(",", "") + " ";

            if (TXT_BALANCE2.Text.Trim() != "")
                where = where + " and a.BALANCE <= " + TXT_BALANCE2.Text.Trim().Replace(",", "") + " ";


            conn.QueryString = "select " +
                                "TRXID, " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "BOOK_NAME " +
                                "from V_REKENING_JURNAL a " +
                                "where " +
                                "BALANCE > 0 and COL = 1 and TIPE_VALIDASI = '001' and DEBET = 0 " + where +
                                " order by a.POST_DATE";
            conn.ExecuteQuery(150000);

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
                            "AMOUNT = replace(convert(varchar(100),convert(money,SUM(BALANCE)),1),'.00','') " +
                            "from V_REKENING_JURNAL a " +
                            "where " +
                            "BALANCE > 0 " + where;

            ShowResult(records, SQL);
        }

        protected void LoadRecords()
        {
            TR_INV.Visible = false;
            TR_RK.Visible = false;

            if (LB_MODE.Text == "INV")
            {
                TR_RK.Visible = true;
                FillDGRRK();
            }

            if (LB_MODE.Text == "RK")
            {
                TR_INV.Visible = true;
                FillDGRINV();
            }
        }

        protected void DGR_INV_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                if (e.Item.Cells[2].Text != "1")
                {
                    LB_ERROR.Text = "";
                    try
                    {
                        string invoiceno = "";
                        string trxid = "";
                        if (LB_MODE.Text == "INV")
                        {
                            invoiceno = LB_CODE.Text;
                            trxid = e.Item.Cells[1].Text;
                        }
                        else
                        {
                            invoiceno = e.Item.Cells[1].Text;
                            trxid = LB_CODE.Text;
                        }

                        conn.QueryString = "exec SP_COL_SETTLE_PREMI_RK " +
                                            "'" + invoiceno + "'," +
                                            "'" + trxid + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                        Setup();
                        LoadRecords();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = "<BR>" + ex.Message;
                    }
                }
                else
                {
                    ShowInvoiceDetail(e.Item);
                }
            }
        }

        protected void ShowInvoiceDetail(DataGridItem item)
        {
            LB_INVDET_INVOICENO.Text = item.Cells[1].Text;
            LB_INVDET_COMPANY.Text = item.Cells[6].Text;
            LB_INVDET_INVOICEDATE.Text = item.Cells[7].Text;
            LB_INVDET_INVOICETYPE.Text = item.Cells[9].Text;
            LB_INVDET_OUTSTANDING.Text = item.Cells[3].Text;
            LB_INVDET_POLICYNO.Text = item.Cells[5].Text;

            TR_INVOCIE_DETAIL.Visible = true;
            TR_INVOICE_MASTER.Visible = false;
            TBL_RESULT.Visible = false;

            FillDGRInvoiceDetail();
        }

        protected void FillDGRInvoiceDetail()
        {
            conn.QueryString = "select  " +
                                "INVOICENO,  " +
                                "APP_ID,  " +
                                "INVOICE_TYPE,  " +
                                "DOC_NO,  " +
                                "MEMBER_NAME,  " +
                                "OUTSTANDING		= replace(convert(varchar(100), convert(money, OUTSTANDING),1), '.00', '')  " +
                                "from V_INVOICE_DETAIL_SUMMARY  " +
                                "where  " +
                                "INVOICENO = '" + LB_INVDET_INVOICENO.Text + "' " +
                                "order by DOC_NO";
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INV_DETAIL.DataSource = dt;
            DGR_INV_DETAIL.DataBind();

            for (int i = 0; i < DGR_INV_DETAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_INV_DETAIL.Items[i].FindControl("CB_INVDET");
                if (DGR_INV_DETAIL.Items[i].Cells[5].Text == "0")
                    cb.Visible = false;
            }
        }

        protected void DGR_RK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                conn.QueryString = "select ENABLE_DETAIL from V_INVOICE_MASTER where INVOICENO = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery(150000);

                if (conn.GetFieldValue(0, 0).ToString() != "1")
                {
                    LB_ERROR.Text = "";
                    try
                    {
                        conn.QueryString = "exec SP_COL_SETTLE_PREMI_RK " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + e.Item.Cells[1].Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                        Setup();
                        LoadRecords();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = "<BR>" + ex.Message;
                    }
                }
                else
                {
                    string invoiceno = LB_CODE.Text;

                    LB_CODE.Text = e.Item.Cells[1].Text;
                    LB_MODE.Text = "RK";
                    FillDGRInfo();

                    TR_INV.Visible = true;
                    TR_RK.Visible = false;
                    TR_INVOCIE_DETAIL.Visible = true;
                    TR_INVOICE_MASTER.Visible = false;
                    TBL_RESULT.Visible = false;

                    conn.QueryString = "select " +
                                        "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                        "CUSTOMER_CODE, " +
                                        "CUSTOMER_NAME, " +
                                        "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                        "AGING, " +
                                        "INVOICE_TYPE_DESCR, " +
                                        "ENABLE_DETAIL " +
                                        "from V_INVOICE_MASTER a " +
                                        "where " +
                                        "INVOICENO = '" + invoiceno + "'";
                    conn.ExecuteQuery(150000);

                    LB_INVDET_INVOICENO.Text = invoiceno;
                    LB_INVDET_COMPANY.Text = conn.GetFieldValue("CUSTOMER_NAME").ToString();
                    LB_INVDET_INVOICEDATE.Text = conn.GetFieldValue("INVOICE_DATE").ToString();
                    LB_INVDET_INVOICETYPE.Text = conn.GetFieldValue("INVOICE_TYPE_DESCR").ToString();
                    LB_INVDET_OUTSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
                    LB_INVDET_POLICYNO.Text = conn.GetFieldValue("CUSTOMER_CODE").ToString();                    

                    FillDGRInvoiceDetail();
                }

            }
        }

        protected void BT_RK_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGRRK();
        }

        protected void BT_INV_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGRINV();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLInvoice();
        }

        protected void LBT_BACK_Click(object sender, EventArgs e)
        {
            TR_INVOCIE_DETAIL.Visible = false;
            TR_INVOICE_MASTER.Visible = true;
            TBL_RESULT.Visible = true;
            FillDGRINV();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_INV_DETAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_INV_DETAIL.Items[i].FindControl("CB_INVDET");
                cb.Checked = ((CheckBox)sender).Checked;
            }

            InvoiceDetailTobePaid();
        }

        protected void InvoiceDetailTobePaid()
        {
            LB_INVDET_TOBEPAID.Text = "0";
            float tobepaid = 0;

            for (int i = 0; i < DGR_INV_DETAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_INV_DETAIL.Items[i].FindControl("CB_INVDET");

                if (cb.Visible && cb.Checked)
                {
                    try
                    {
                        tobepaid = tobepaid + float.Parse(DGR_INV_DETAIL.Items[i].Cells[5].Text.Replace(",", ""));
                    }
                    catch { }
                }
            }

            LB_INVDET_TOBEPAID.Text = tobepaid.ToString("#,##0.00");

            conn.QueryString = "select " +
                                "OUTSTANDING		= replace(convert(varchar(100), convert(money, OUTSTANDING),1), '.00', '') " +
                                "from V_INVOICE_MASTER where INVOICENO = '" + LB_INVDET_INVOICENO.Text + "'";
            conn.ExecuteQuery(150000);
            LB_INVDET_OUTSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
        }

        protected void CB_INVDET_CheckedChanged(object sender, EventArgs e)
        {
            InvoiceDetailTobePaid();
        }

        protected void DGR_INV_DETAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                for (int i = 0; i < DGR_INV_DETAIL.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_INV_DETAIL.Items[i].FindControl("CB_INVDET");

                    if (cb.Visible && cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_COL_SETTLE_PREMI_RK_DETAIL " +
                                                     "'" + DGR_INV_DETAIL.Items[i].Cells[0].Text + "'," +
                                                     "'" + LB_CODE.Text + "'," +
                                                     "'" + DGR_INV_DETAIL.Items[i].Cells[3].Text + "'," +
                                                     "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                FillDGRInvoiceDetail();
                InvoiceDetailTobePaid();
                FillDGRInfo();
            }
        }
    }
}
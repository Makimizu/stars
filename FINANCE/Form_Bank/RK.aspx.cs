using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class RK : System.Web.UI.Page
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

                Setup();
                DGR.CurrentPageIndex = 0;
                //FillDGR();
            }
        }

        protected void Setup()
        {
            DDL_NOREK.Items.Clear();
            conn.QueryString = "select NOREK, BANK from REKENING_MASTER order by 2";
            conn.ExecuteQuery(150000);

            DDL_NOREK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select FIRST_DOM = convert(varchar(2),day(GETDATE())) + '/' + convert(varchar(2),month(GETDATE())) + '/' +  + convert(varchar(4),year(GETDATE()))";
            conn.ExecuteQuery(150000);
            TXT_POSTDATE.Text = conn.GetFieldValue("FIRST_DOM").ToString();
        }

        protected void FillDGR()
        {
            LB_ERR.Text = "";
            BT_XLS.Visible = false;

            try
            {
                LB_RECORDS.Text = "";
                LB_BALANCE.Text = "";
                LB_CREDIT.Text = "";
                LB_DEBET.Text = "";
                string where = "";

                if (TXT_VALIDATION.Text.Trim() != "")
                    where = where + "and TIPE_VALIDASI_DESCR like '%" + TXT_VALIDATION.Text.Trim() + "%' ";

                if (TXT_DESCR.Text.Trim() != "")
                    where = where + "and DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

                if (TXT_BATCH.Text.Trim() != "")
                    where = where + "and BATCH_ID = '" + TXT_BATCH.Text.Trim() + "' ";

                if (TXT_POSTDATE.Text.Trim() != "")
                    where = where + "and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

                if (TXT_POSTDATE2.Text.Trim() != "")
                    where = where + "and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

                if (TXT_DEBET1.Text.Trim() != "")
                    where = where + "and DEBET >= " + TXT_DEBET1.Text.Trim().Replace(",", "") + " ";

                if (TXT_DEBET2.Text.Trim() != "")
                    where = where + "and DEBET <= " + TXT_DEBET2.Text.Trim().Replace(",", "") + " ";

                if (TXT_CREDIT1.Text.Trim() != "")
                    where = where + "and CREDIT >= " + TXT_CREDIT1.Text.Trim().Replace(",", "") + " ";

                if (TXT_CREDIT2.Text.Trim() != "")
                    where = where + "and CREDIT <= " + TXT_CREDIT2.Text.Trim().Replace(",", "") + " ";

                if (DDL_NOREK.SelectedValue != "")
                    where = where + "and NOREK = '" + DDL_NOREK.SelectedValue + "' ";

                conn.QueryString = "select " +
                                    "TRXID, " +
                                    "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                    "DESCR, " +
                                    "DEBET = replace(convert(varchar(100),convert(money,DEBET),1),'.00',''), " +
                                    "CREDIT = replace(convert(varchar(100),convert(money,CREDIT),1),'.00',''), " +
                                    "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                    "TIPE_VALIDASI_DESCR, " +
                                    "ENABLE_DEBET, " +
                                    "ENABLE_REFUND, " +
                                    "NOREK, " +
                                    "BANK_DESCR, " +
                                    "BALANCE_AMOUNT = BALANCE, " +
                                    "FLAG_DATE = convert(varchar(20),FLAG_DATE,106) " +
                                    "from V_REKENING_JURNAL a  " +
                                    "where 1=1 " + where +
                                    "order by " +
                                    DDL_ORDERBY.SelectedValue + " " +DDL_ORDERSHORT.SelectedValue;
                conn.ExecuteQuery(150000);

                if (conn.GetRowCount() > 0)
                    BT_XLS.Visible = true;

                LB_RECORDS.Text = " : " + conn.GetRowCount().ToString();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();

                conn.QueryString = "select " +
                                    "DEBET = replace(convert(varchar(100),convert(money,SUM(DEBET)),1),'.00',''), " +
                                    "CREDIT = replace(convert(varchar(100),convert(money,SUM(CREDIT)),1),'.00',''), " +
                                    "BALANCE = replace(convert(varchar(100),convert(money,SUM(BALANCE)),1),'.00','') " +
                                    "from V_REKENING_JURNAL a  " +
                                    "where 1=1 " + where;
                conn.ExecuteQuery(150000);
                LB_DEBET.Text = " : " + conn.GetFieldValue("DEBET").ToString();
                LB_CREDIT.Text = " : " + conn.GetFieldValue("CREDIT").ToString();
                LB_BALANCE.Text = " : " + conn.GetFieldValue("BALANCE").ToString();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    Button btHST = (Button)DGR.Items[i].FindControl("BT_HST");
                    Button btSTL = (Button)DGR.Items[i].FindControl("BT_STL");
                    Button btRFD = (Button)DGR.Items[i].FindControl("BT_RFD");
                    Button btFLAG = (Button)DGR.Items[i].FindControl("BT_FLAG");
                    Button btPindahDana = (Button)DGR.Items[i].FindControl("BT_PINDAH_DANA");

                    btHST.Attributes.Add("onclick", "window.open('RK_History.aspx?TRXID=" + DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                    if (DGR.Items[i].Cells[1].Text == "1")
                        btSTL.Visible = true;
                    if (DGR.Items[i].Cells[2].Text == "1")
                        btRFD.Visible = true;
                    if (DGR.Items[i].Cells[1].Text == "1" && DGR.Items[i].Cells[6].Text != "0" && DGR.Items[i].Cells[9].Text == "TITIPAN KONTRIBUSI")
                        btFLAG.Visible = true;
                    if (DGR.Items[i].Cells[9].Text == "BREAKDOWN")
                        btPindahDana.Visible = true;
                }
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message + "<BR>";
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                Response.Redirect("../Form_Collection/Invoice_Settle.aspx?MODE=RK&CODE=" + e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Refund")
            {
                Response.Redirect("RK_Refund.aspx?&CODE=" + e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Flag")
            {
                string norek = e.Item.Cells[10].Text;

                FillDDLFlag(norek);

                LB_TRXID.Text = e.Item.Cells[0].Text;
                LB_POST_DATE.Text = e.Item.Cells[3].Text;
                LB_DESCRIPTION.Text = e.Item.Cells[5].Text;
                LB_AMOUNT.Text = e.Item.Cells[8].Text;
                LB_BALANCE_AMOUNT.Text = e.Item.Cells[12].Text;
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('FlagPopup').style.display = 'block';", true);
            }

            if (e.CommandName == "Transfer")
            {
                Response.Redirect("RK_Transfer.aspx?&CODE=" + e.Item.Cells[0].Text);
            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";

            if (TXT_VALIDATION.Text.Trim() != "")
                where = where + "and TIPE_VALIDASI_DESCR like '%" + TXT_VALIDATION.Text.Trim() + "%' ";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + "and DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_BATCH.Text.Trim() != "")
                where = where + "and BATCH_ID = '" + TXT_BATCH.Text.Trim() + "' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DEBET1.Text.Trim() != "")
                where = where + "and DEBET >= " + TXT_DEBET1.Text.Trim().Replace(",", "") + " ";

            if (TXT_DEBET2.Text.Trim() != "")
                where = where + "and DEBET <= " + TXT_DEBET2.Text.Trim().Replace(",", "") + " ";

            if (TXT_CREDIT1.Text.Trim() != "")
                where = where + "and CREDIT >= " + TXT_CREDIT1.Text.Trim().Replace(",", "") + " ";

            if (TXT_CREDIT2.Text.Trim() != "")
                where = where + "and CREDIT <= " + TXT_CREDIT2.Text.Trim().Replace(",", "") + " ";

            if (DDL_NOREK.SelectedValue != "")
                where = where + "and NOREK = '" + DDL_NOREK.SelectedValue + "' ";

            /*
            conn.QueryString = "select " +
                                "TRXID, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "DEBET = replace(convert(varchar(100),convert(money,DEBET),1),'.00',''), " +
                                "CREDIT = replace(convert(varchar(100),convert(money,CREDIT),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                "FLAG = TIPE_VALIDASI_DESCR, " +
                                "ACCNO = NOREK, " +
                                "BANK = BANK_DESCR " +
                                "from V_REKENING_JURNAL a  " +
                                "where 1=1 " + where +
                                "order by a.POST_DATE desc";
            conn.ExecuteQuery(150000);
            */

            conn.QueryString = "select " +
                                "TRXID, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "DEBET, " +
                                "CREDIT, " +
                                "BALANCE, " +
                                "FLAG = TIPE_VALIDASI_DESCR, " +
                                "ACCNO = NOREK, " +
                                "BANK = BANK_DESCR, " +
                                "AGING = AGING, " +
                                "USERID = USERID, " +
                                "FLAG_DATE = convert(varchar(20),FLAG_DATE,106) " +
                                "from V_REKENING_JURNAL a  " +
                                "where 1=1 " + where +
                                "order by a.POST_DATE desc";
            conn.ExecuteQuery(150000);

            LB_RECORDS.Text = " : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Clone();

            dt.Columns[0].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(Int64);
            dt.Columns[4].DataType = typeof(Int64);
            dt.Columns[5].DataType = typeof(Int64);
            dt.Columns[7].DataType = typeof(string);

            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "BANK_STATEMENT", true);
        }

        protected void FillDDLFlag(string norek)
        {
            conn.QueryString = "exec SP_PARAM_RK_VALIDASI_BANK_STATEMENT " +
                                "'" + norek + "'," +
                                "'C'";
            conn.ExecuteQuery(150000);
            DDL_FLAG.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FLAG.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void BT_SET_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_REKENING_JURNAL_FLAG_BANK_STATEMENT_INSERT " +
                                    "'" + LB_TRXID.Text + "'," +
                                    "'" + DDL_FLAG.SelectedValue + "'," +
                                    "'" + LB_BALANCE_AMOUNT.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
            catch (System.Exception ex)
            {
                //LB_ERR.Text = LB_ERR.Text + ex.Message + "<BR>";
            }
        }
    }
}
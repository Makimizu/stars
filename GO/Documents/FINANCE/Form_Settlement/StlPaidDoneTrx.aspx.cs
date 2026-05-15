using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlPaidDoneTrx : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            //DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            FillDDLTipe();
        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            //DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "";

            if (DDL_APP.SelectedValue != "")
                where = where + "and a.APP_ID='" + DDL_APP.SelectedValue + "' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + "and a.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_DOCNO.Text.Trim() != "")
            {
                where = where + "and a.DOCNO like '%" + TXT_DOCNO.Text.Trim() + "%' ";
            }

            if (TXT_EXTDOCNO.Text.Trim() != "")
            {
                where = where + "and b.EXTDOC like '%" + TXT_EXTDOCNO.Text.Trim() + "%' ";
            }

            if (TXT_NAMA.Text.Trim() != "")
            {
                where = where + "and b.NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";
            }

            if (TXT_ACCNO.Text.Trim() != "")
            {
                where = where + "and a.ACC_NO like '%" + TXT_ACCNO.Text.Trim() + "%' ";
            }

            if (TXT_COMPANY.Text.Trim() != "")
            {
                where = where + "and b.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            }

            if (TXT_BENEF.Text.Trim() != "")
            {
                where = where + "and a.BENEFICIARY like '%" + TXT_BENEF.Text.Trim() + "%' ";
            }

            if (TXT_POLICYNO.Text.Trim() != "")
            {
                where = where + "and a.CUSTOMER_CODE like '%" + TXT_POLICYNO.Text.Trim() + "%' ";
            }

            if (TXT_DATE.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select  " +
                                "a.DOCNO, " +
                                "b.EXTDOC, " +
                                "b.NAMA, " +
                                "b.COMPANY_NAME, " +
                                "POST_DATE	= convert(varchar(20),a.POST_DATE,106), " +
                                "AMOUNT		= replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "a.BENEFICIARY, " +
                                "a.ACC_NO, " +
                                "a.BANK, " +
                                "a.RK_DESCR, " +
                                "REPORT_URL = b.URL, " +
                                "a.REKAPID " +
                                "from V_SETTLEMENT_DETAIL_PAID a " +
                                "inner join V_LINK_SETTLEMENT_DETAIL_REPORT_URL b on a.APP_ID = b.APP_ID and a.TIPE_SETTLEMENT = b.TIPE and a.DOCNO = b.DOCNO collate database_default " +
                                "where 1=1 " + where + " " +
                                "order by a.POST_DATE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString();

            conn.QueryString = "select " +
                                "AMOUNT		= replace(convert(varchar(100),convert(money,SUM(a.AMOUNT)),1),'.00','') " +
                                "from V_SETTLEMENT_DETAIL_PAID a " +
                                "inner join V_LINK_SETTLEMENT_DETAIL_REPORT_URL b on a.APP_ID = b.APP_ID and a.TIPE_SETTLEMENT = b.TIPE and a.DOCNO = b.DOCNO collate database_default " +
                                "where 1=1 " + where;
            conn.ExecuteQuery();

            LB_RECORD.Text = "<table>" +
                                "<tr><td>Records</td><td>:</td><td>" +LB_RECORD.Text+ "</td></tr>" +
                                "<tr><td>Total Amount</td><td>:</td><td>" + conn.GetFieldValue("AMOUNT").ToString() + "</td></tr>" +
                                "</table>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton btID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                btID.Text = DGR.Items[i].Cells[1].Text;
                btID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[11].Text + "','PAID_TRX','height=400,width=800,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
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

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }
    }
}
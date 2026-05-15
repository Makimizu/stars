using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class PaidTrx : System.Web.UI.Page
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
                                "a.CODE, " +
                                "a.DESCR  " +
                                "from FINANCE.dbo.PARAM_TIPE_SETTLEMENT a " +
                                "inner join V_LINK_SC_M_APPS b on b.APP_DBNAME=DB_NAME() and a.APP_ID=b.CODE collate database_default " +
                                "inner join V_LINK_FN_SETTLEMENT_DETAIL_PAID c on a.CODE = c.TIPE_SETTLEMENT " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "";

            if (TXT_DOCNO.Text.Trim() != "")
            {
                where = where + "and DOCNO like '%" + TXT_DOCNO.Text.Trim() + "%' ";
            }

            if (TXT_EXTDOCNO.Text.Trim() != "")
            {
                where = where + "and EXTDOC like '%" + TXT_EXTDOCNO.Text.Trim() + "%' ";
            }

            if (TXT_NAMA.Text.Trim() != "")
            {
                where = where + "and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";
            }

            if (TXT_COMPANY.Text.Trim() != "")
            {
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            }

            if (TXT_BENEF.Text.Trim() != "")
            {
                where = where + "and BENEFICIARY like '%" + TXT_BENEF.Text.Trim() + "%' ";
            }

            if (TXT_POLICYNO.Text.Trim() != "")
            {
                where = where + "and CUSTOMER_CODE like '%" + TXT_POLICYNO.Text.Trim() + "%' ";
            }

            if (TXT_DATE.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + " and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select  " +
                                "DOCNO, " +
                                "EXTDOC, " +
                                "NAMA, " +
                                "COMPANY_NAME, " +
                                "POST_DATE	= convert(varchar(20),POST_DATE,106), " +
                                "AMOUNT		= replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "BENEFICIARY, " +
                                "ACC_NO, " +
                                "BANK, " +
                                "RK_DESCR, " +
                                "REPORT_URL, " +
                                "REKAPID " +
                                "from V_LINK_FN_SETTLEMENT_DETAIL_PAID a " +
                                "where " +
                                "TIPE_SETTLEMENT = '" + DDL_TIPE.SelectedValue + "' " +
                                where + " order by a.POST_DATE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

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
    }
}
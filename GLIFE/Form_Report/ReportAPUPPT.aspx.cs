using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Report
{
    public partial class ReportAPUPPT : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from [UWBOX].[dbo].[PARAM_PRODUCT_GROUP] where SEGMENT = 0";
            conn.ExecuteQuery(150000);
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //conn.QueryString = "select SEQ, DESCR from [LIFE].[dbo].[PARAM_TRACK] where TIPE_CODE = 'UW' and SEQ not in (99)";
            //conn.ExecuteQuery();
            //DDL_STAT.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //    DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string nama = "", produk = "", regno = "", group = "", start = "", end = "", track = "";

            if (TXT_FULLNAME.Text.Trim() != "")
                nama = TXT_FULLNAME.Text.Trim();

            if (TXT_PRODUCT.Text.Trim() != "")
                produk = TXT_PRODUCT.Text.Trim();

            //if (TXT_REGNO.Text.Trim() != "")
            //    regno = TXT_REGNO.Text.Trim();

            //if (cc.SelectedValue != "")
            //    where = where + DDL_PREMIUM.SelectedValue;

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                group = DDL_PRODUCT_GROUP.SelectedValue;

            if (TXT_STARTDATE1.Text.Trim() != "")
                start = TXT_STARTDATE1.Text.Trim();

            if (TXT_STARTDATE2.Text.Trim() != "")
                end = TXT_STARTDATE2.Text.Trim();

            //if (DDL_STAT.SelectedValue != "")
            //    track = DDL_STAT.SelectedValue;

            conn.QueryString = "EXEC [GLIFE].[dbo].[SP_APU_PPT] '" + nama + "', '" + produk + "', '" + group + "', '" + start + "', '" + end + "'";
            conn.ExecuteQuery(150000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            Session["datatable"] = dt;

            BT_EXCEL.Visible = true;
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            BT_EXCEL.Visible = false;
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }


        protected void BT_EXCEL_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["datatable"];

            DateTime dte = DateTime.UtcNow;
            DateTime utc = dte.ToUniversalTime();
            long unixTime = (long)(utc - new DateTime(1970, 1, 1)).TotalSeconds;
            //long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();
            string filename = "report_apuppt_korporasi_" + unixTime.ToString();

            //ClientScript.RegisterStartupScript(this.GetType(), "hideProgress", "HideProgress();", true);

            GlobalUse.ExportDataSetToExcel(dt, this, filename, true);

        }
    }
}
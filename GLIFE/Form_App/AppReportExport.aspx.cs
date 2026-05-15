using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.IO;

namespace GLIFE.Form_App
{
    public partial class AppReportExport : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dateFormat = DateTime.Now.ToString("yyyy-MM-dd");
                TXT_CONFDATE1.Text = dateFormat;
                TXT_CONFDATE2.Text = dateFormat;

                BindInvoiceType();
            }
        }

        private void BindInvoiceType()
        {
            var _sql = @"select * from param_track where tipe_code = 'UW' and seq != '98'";

            conn.QueryString = _sql;

            conn.ExecuteQuery();

            DDL_STATUS.Items.Clear();
            DDL_STATUS.DataTextField = "DESCR";
            DDL_STATUS.DataValueField = "SEQ";
            DDL_STATUS.DataSource = conn.GetDataTable();
            DDL_STATUS.DataBind();

        }

        protected void Export()
        {
            LB_RESULT.Text = "";

            if (string.IsNullOrEmpty(TXT_CONFDATE1.Text.Trim()))
            {
                LB_RESULT.Text = "Tanggal Mulai tidak boleh kosong!";
                return;
            }
            if (string.IsNullOrEmpty(TXT_CONFDATE2.Text.Trim()))
            {
                LB_RESULT.Text = "Tanggal Akhir tidak boleh kosong!";
                return;
            }

            List<ListItem> items = DDL_STATUS.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
            if (items.Count == 0)
            {
                LB_RESULT.Text = "Pilih status report!";
                return;
            }

            string combindedString = string.Join(",", items.Select(x => x.Value));
            var xxx = combindedString;

            conn.QueryString = string.Format("exec RPT_PRODUCTION_DAILY_PERIOD '{0}', '{1}', '{2}'", TXT_CONFDATE1.Text.Trim(), TXT_CONFDATE2.Text.Trim(), combindedString);
            conn.ExecuteQuery(50000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DataGrid oDg = new DataGrid();
            oDg.DataSource = dt;
            oDg.DataBind();

            //export to excel
            this.Response.Clear();
            var fileName = string.Format("{0:yyyyMMddhhmmsstt}.xls", DateTime.Now);
            this.Response.Buffer = true;
            this.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.Charset = "";
            var oStringWriter = new StringWriter();
            var oHtmlTextWriter = new HtmlTextWriter(oStringWriter);
            oDg.RenderControl(oHtmlTextWriter);
            this.Response.Write(oStringWriter.ToString());
            this.Response.End();
            //GlobalUse.DataGridToExcel(this, oDg);

        }

        protected void BTN_EXPORT_Click(object sender, EventArgs e)
        {
            Export();
        }


    }
}
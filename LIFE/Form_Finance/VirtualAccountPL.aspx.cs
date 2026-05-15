using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Text;
using System.Net.Mime;

namespace LIFE.Form_Finance
{
    public partial class VirtualAccountPL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "YEAR		= YEAR(GETDATE()) - SEQ + 1 " +
                                "from		SC_SEQ " +
                                "where " +
                                "SEQ			<= 3 " +
                                "order by 1 desc";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select " +
                                "SEQ, " +
                                "DESCR		= dateName(month,DateAdd(month , SEQ, 0 ) - 1) " +
                                "from		SC_SEQ  " +
                                "where  " +
                                "SEQ			<= 12 " +
                                "order by " +
                                "SEQ desc";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select MONTH(GETDATE())";
            conn.ExecuteQuery(150000);
            DDL_MONTH.SelectedValue = conn.GetFieldValue(0, 0).ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ACTIVE_DATE " + DDL_YEAR.SelectedValue + "," + DDL_MONTH.SelectedValue;
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_BANK");

                conn.QueryString = DGR.Items[i].Cells[0].Text;
                conn.ExecuteQuery(150000);
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, "BANK").ToString(), conn.GetFieldValue(j, "CODE").ToString()));
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Export")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_BANK");
                conn.QueryString = "select " + 
                                    "FORMAT			= a.FILEFORMAT, "+
                                    "URL_REPORT		= REPLACE(REPLACE(a.URL_REPORT,'@REPORT_CODE',convert(varchar(10),b.CODE)),'@START_DATE','" + e.Item.Cells[1].Text.Replace("&nbsp;","") + "'), "+
                                    "SQL			= 'exec RPT_APPLICATION_GENERATE_REGNO_VA '''+ a.CODE +''',''' + '" + e.Item.Cells[1].Text.Replace("&nbsp;", "") + "' + '''', " +
                                    "FILENAME		= 'APPLICATION_GENERATE_REGNO_VA_' + convert(varchar(30),'" + e.Item.Cells[1].Text.Replace("&nbsp;", "") + "',112) + '.' + a.FILEFORMAT " +
                                    "from			V_BANK_VIRTUALACC_EXPORT_FORMAT a  " +
                                    "inner join		SECURITY.dbo.REPORT_LIST b on a.REPORT_CODE = b.CODE and APP_ID	= 'LF' and b.CODE in (44,45)" + 
                                    "where  " +
                                    "a.CODE = '" + ddl.SelectedValue + "'";
                conn.ExecuteQuery(150000);

                string FORMAT = conn.GetFieldValue("FORMAT").ToString();
                string URL_REPORT = conn.GetFieldValue("URL_REPORT").ToString();
                string SQL = conn.GetFieldValue("SQL").ToString();
                string FILENAME = conn.GetFieldValue("FILENAME").ToString();

                if (FORMAT == "TXT" || FORMAT == "CSV")
                {
                    conn.QueryString = SQL;
                    conn.ExecuteQuery(150000);
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    ExportDataTabletoFile(dt, this, FILENAME, false);
                }
                else
                {
                    Response.Redirect(URL_REPORT);
                    //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>window.open('" + URL_REPORT + "');';</script>");
                }
            }
        }

        protected void ExportDataTabletoFile(DataTable table, Page page, string filename, bool header)
        {
            var result = new StringBuilder();
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(table.Columns[i].ColumnName);
                    result.Append(i == table.Columns.Count - 1 ? "\r\n" : ";");
                }
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    var rowValue = row[i].ToString();
                    result.Append(rowValue);
                    result.Append(i == table.Columns.Count - 1 ? "\r\n" : ";");
                }
            }

            page.Response.Clear();
            page.Response.Buffer = true;

            page.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            page.Response.Charset = "";
            page.Response.ContentType = "application/text";
            page.Response.Output.Write(result.ToString());
            page.Response.Flush();
            page.Response.End();
        }
    }
}
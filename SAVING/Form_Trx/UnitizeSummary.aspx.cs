using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Text;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SAVING.Form_Trx
{
    public partial class UnitizeSummary : System.Web.UI.Page
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
            Page.Server.ScriptTimeout = 300;

            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from V_CUSTODIAN_MASTER order by 2";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select YEAR	= YEAR(GETDATE()) - a.SEQ + 1 from	SC_SEQ a where a.SEQ <= 5 order by 1 desc";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "CODE	= SEQ, " +
                                "DESCR	= UPPER(DateName( month , DateAdd( month , SEQ , -1 ))) " +
                                "from	SC_SEQ  " +
                                "where  " +
                                "SEQ		<= 12";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "THISYEAR	= YEAR(GETDATE()), " +
                                "THISMONTH	= MONTH(GETDATE())";
            conn.ExecuteQuery(150000);

            try
            {
                DDL_YEAR.SelectedValue = conn.GetFieldValue("THISYEAR").ToString();
            }
            catch { }

            try
            {
                DDL_MONTH.SelectedValue = conn.GetFieldValue("THISMONTH").ToString();
            }
            catch { }
        }

        protected void DDL_COMPANY_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_CUSTODIAN_UNIT_LINK_SCHEDULE " +
                                "'" + DDL_COMPANY.SelectedValue + "'," +
                                DDL_YEAR.SelectedValue + "," +
                                DDL_MONTH.SelectedValue;
            conn.ExecuteQuery(150000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "CODE		= a.APP_ID + '-' + convert(varchar(10), a.REPORT_CODE), " +
                                "DESCR		= b.DESCR " +
                                "from		CUSTODIAN_MASTER_REPORT a " +
                                "inner join	SECURITY.dbo.REPORT_LIST b on a.APP_ID = b.APP_ID collate database_default and a.REPORT_CODE = b.CODE " +
                                "where " +
                                "a.COMPANY_CODE = '" + DDL_COMPANY.SelectedValue + "' " +
                                "order by 2";
            conn.ExecuteQuery(150000);

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlREPORT = (DropDownList)DGR.Items[i].FindControl("DDL_REPORT");
                Button btEMAIL = (Button)DGR.Items[i].FindControl("BT_EMAIL");

                if (DGR.Items[i].Cells[0].Text != "1")
                {
                    btEMAIL.Enabled = false;
                }

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlREPORT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                conn.QueryString = "exec SP_GENERATE_CUSTODIAN_TRANSFER '" + e.Item.Cells[1].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(150000);

                DropDownList ddlREPORT = (DropDownList)e.Item.FindControl("DDL_REPORT");
                conn.QueryString = "select " +
                                    "FORMAT     = a.FORMAT, " +
                                    "URL_REPORT = '../../ReportViewer/Viewer.aspx?APPID=' + a.APP_ID + '&CODE=' + convert(varchar(10), a.REPORT_CODE) + '&TRXDATE=" + e.Item.Cells[1].Text.Replace(" ", "-") + "', " +
                                    "SQL		= 'exec SAVING.dbo.' + b.REPORT_NAME + ' ''" + e.Item.Cells[1].Text + "''', " +
                                    "FILENAME	= case a.REPORT_CODE    when 38 then 'SUBSCRIPTION_' + convert(varchar(15),convert(date,'" + e.Item.Cells[2].Text + "'),112) + '.' + a.FORMAT " +
                                    "                                   when 39 then 'REDEMPTION_' + convert(varchar(15),convert(date,'" + e.Item.Cells[2].Text + "'),112) + '.' + a.FORMAT " +
                                    "                                   when 40 then 'ACCOUNT_' + convert(varchar(15),convert(date,'" + e.Item.Cells[2].Text + "'),112) + '.' + a.FORMAT " +
                                    "                                   when 41 then 'KYC_Details_' + convert(varchar(15),convert(date,'" + e.Item.Cells[2].Text + "'),112) + '.' + a.FORMAT " +
                                    "                                   else '" + ddlREPORT.SelectedItem.Text.Replace(" ", "_") + "-' + a.COMPANY_CODE + '-" + e.Item.Cells[1].Text.Replace(" ", "-") + ".' + a.FORMAT " +
                                    "                                   end " +
                                    "from		CUSTODIAN_MASTER_REPORT a " +
                                    "inner join	SECURITY.dbo.REPORT_LIST b on a.APP_ID = b.APP_ID collate database_default and a.REPORT_CODE = b.CODE " +
                                    "where " +
                                    "a.COMPANY_CODE = '" + DDL_COMPANY.SelectedValue + "' " +
                                    "and a.APP_ID + '-' + convert(varchar(10), a.REPORT_CODE) = '" + ddlREPORT.SelectedValue + "'";
                conn.ExecuteQuery(150000);

                string FORMAT = conn.GetFieldValue("FORMAT").ToString();
                string URL_REPORT = conn.GetFieldValue("URL_REPORT").ToString();
                string SQL = conn.GetFieldValue("SQL").ToString();
                string FILENAME = conn.GetFieldValue("FILENAME").ToString();

                switch (FORMAT)
                {
                    case "PDF":
                        Response.Redirect(URL_REPORT);
                        break;
                    case "EXCEL":
                        Response.Redirect(URL_REPORT);
                        break;
                    default:
                        conn.QueryString = SQL;
                        conn.ExecuteQuery(150000);
                        DataTable dt;
                        dt = new DataTable();
                        dt = conn.GetDataTable().Copy();
                        ExportDataTabletoFile(dt, this, FILENAME, false);
                        //Response.Redirect("UnitizeSummary.aspx");
                        break;
                }


            }

            if (e.CommandName == "Email")
            {
                Task.Run(() => SendDailyMail(e.Item.Cells[1].Text, DDL_COMPANY.SelectedValue));
                //SendDailyMail(e.Item.Cells[1].Text, DDL_COMPANY.SelectedValue);
            }
        }

        protected void SendDailyMail(string trxdate, string companyCode)
        {
            conn.QueryString = "exec SP_GENERATE_CUSTODIAN_TRANSFER '" + trxdate + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(5000);

            conn.QueryString = "exec SP_UNIT_LINK_DAILY_EMAIL " +
                                    "'" + trxdate + "', " +
                                    "'" + companyCode + "'";
            conn.ExecuteQuery(5000);
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
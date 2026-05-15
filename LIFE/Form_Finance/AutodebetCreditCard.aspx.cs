using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Text;


namespace LIFE.Form_Finance
{
    public partial class AutodebetCreditCard : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                //FillDGR();

                string script = "$(document).ready(function () { $('[id*=BT_LOAD]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void Setup()
        {
            //conn.QueryString = "select BANK from FINANCE.dbo.PARAM_TBL_BANK where CODE = '" + LB_CODE.Text + "'";
            //conn.ExecuteQuery();
            //LB_TITLE.Text = conn.GetFieldValue(0, 0).ToString();

            conn.QueryString = "select THEYEAR = YEAR(GETDATE()) union all select YEAR(GETDATE()) - 1 order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_AUTODEBET_CC '" + DDL_YEAR.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            //for (int i = 0; i < DGR.Items.Count; i++)
            //{
            //    LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_LINK");

            //    conn.QueryString = DGR.Items[i].Cells[0].Text;
            //    conn.ExecuteQuery();
            //    for (int j = 0; j < conn.GetRowCount(); j++)
            //    {
            //        lb.Text = lb.Text + conn.GetFieldValue(j, 0).ToString() + "<BR>";
            //    }
            //}
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_LOAD_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                String SQL = "", FILENAME = "";

                FILENAME = "RPT_AUTODEBET_CC_" + e.Item.Cells[1].Text.Replace(" ", "-") + ".TXT";
                SQL = "exec RPT_AUTODEBET_CC " +
                        "'" + e.Item.Cells[1].Text + "'";

                conn.QueryString = SQL;
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                ExportDataTabletoFile(dt, this, FILENAME, false);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace SAVING.Form_Parameter
{
    public partial class CustodianReport : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillAvailableReport();
                FillSelectedReport();
            }
        }

        protected void FillAvailableReport()
        {
            BT_ADD.Enabled = true;
            DDL_REPORT.Items.Clear();

            conn.QueryString = "select REPORT_CODE, REPORT_NAME from V_CUSTODIAN_MASTER_REPORT where TAKEN = 0 order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REPORT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (DDL_REPORT.Items.Count == 0)
                BT_ADD.Enabled = false;
        }

        protected void FillSelectedReport()
        {
            conn.QueryString = "select " +
                                "REPORT_CODE, " +
                                "REPORT_NAME, " +
                                "FORMAT, " +
                                "DELIMITER, " +
                                "FILENAME " +
                                "from			V_CUSTODIAN_MASTER_REPORT  " +
                                "where  " +
                                "COMPANY_CODE	= '" + LB_ID.Text + "' " +
                                "order by  " +
                                "REPORT_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                TextBox txtFORMAT = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_FORMAT");
                TextBox txtDELIMITER = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_DELIMITER");
                TextBox txtFILENAME = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_FILENAME");
                Button btX = (Button)DGR_SELECTED.Items[i].FindControl("BT_X");

                txtFORMAT.Text = DGR_SELECTED.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtDELIMITER.Text = DGR_SELECTED.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtFILENAME.Text = DGR_SELECTED.Items[i].Cells[4].Text.Replace("&nbsp;", "");

                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE?')){return false;};");
            }
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "insert into CUSTODIAN_MASTER_REPORT select " +
                                "'" + LB_ID.Text + "'," +
                                "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                "'" + DDL_REPORT.SelectedValue + "'," +
                                "''," +
                                "''," +
                                "''," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE()," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE()";
            conn.ExecuteNonQuery();

            FillAvailableReport();
            FillSelectedReport();
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CUSTODIAN_MASTER_REPORT where " +
                                    "COMPANY_CODE   = '" + LB_ID.Text + "' " +
                                    "and APP_ID     ='" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                    "and REPORT_CODE    = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillAvailableReport();
                FillSelectedReport();
            }
        }
    }
}
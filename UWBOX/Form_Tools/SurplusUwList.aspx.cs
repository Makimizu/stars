using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace UWBOX.Form_Tools
{
    public partial class SurplusUwList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            DateTime dt = DateTime.Now;
            var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);

            if (TXT_DATE_START.Text.Trim() == "")
            {
                TXT_DATE_START.Text = firstDayOfMonth.ToString("dd/MM/yyyy");
            }

            if (TXT_DATE_END.Text.Trim() == "")
            {
                TXT_DATE_END.Text = lastDayOfMonth.ToString("dd/MM/yyyy");
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_BATCH_MASTER_UW_SURPLUS " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE_START.Text.Trim(), "d/M/yyyy") + "', " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE_END.Text.Trim(), "d/M/yyyy") + "', " +
                                "'" + LB_MODE.Text + "'";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>location.href = 'SurplusUWPreviewFrame.aspx?ID=" + e.Item.Cells[0].Text + "&GROUP=" + e.Item.Cells[1].Text + "';</script>");
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
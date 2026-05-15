using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace CORPORATE_PORTAL
{
    public partial class Provider : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                Setup();
            }
        }

        protected void Setup()
        {
            DGR.CurrentPageIndex = 0;
            FillGrid();
        }

        protected void FillGrid()
        {
            LB_RESULT.Text = "";
            conn.QueryString = "select " +
                                "NAMA = UPPER(ltrim(NAMA)),  " +
                                "TITLE,  " +
                                "a.ALAMAT, " +
                                "KOTA_DESCR, " +
                                "a.PHONE,  " +
                                "RI = (case when RI = 1 then 'X' else '' end),  " +
                                "RJ = (case when RJ = 1 then 'X' else '' end),  " +
                                "MCU = (case when MCU = 1 then 'X' else '' end),  " +
                                "OPT = (case when OPT = 1 then 'X' else '' end),  " +
                                "DRB = (case when DRB = 1 then 'X' else '' end),  " +
                                "LAB = (case when LAB = 1 then 'X' else '' end)  " +
                                "from V_LINK_HO_CLM_PROVIDER a " +
                                "where " +
                                "NAMA like '%" + TXT_NAME.Text.Trim() + "%' " +
                                "and TITLE like '%" + TXT_TITLE.Text.Trim() + "%' " +
                                "and ALAMAT like '%" + TXT_ADDRESS.Text.Trim() + "%' " +
                                "and KOTA_DESCR like '%" + TXT_CITY.Text.Trim() + "%' " +
                                "order by KOTA_DESCR, ltrim(NAMA)";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void LB_PROV_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillGrid();
        }

        protected void LB_EXCEL_EXPORT_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillGrid();
        }
    }
}
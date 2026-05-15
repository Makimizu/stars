using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Data.OleDb;

namespace LQ.Form_App
{
    public partial class ApplicationBenefitSeries : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                CheckFinancingTerm();
                LoadSeries();
            }
        }

        protected void CheckFinancingTerm()
        {
            conn.QueryString = "select distinct " +
                                "a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM b on a.PRODUCT_CODE = b.PRODUCT_CODE " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                Response.Redirect("ApplicationFinancingTerm.aspx?REGNO=" + LB_REGNO.Text);
        }

        protected void LoadSeries()
        {
            conn.QueryString = "select " +
                                "SEQ, " +
                                "VALUEDATE = convert(varchar(20),VALUEDATE,106), " +
                                "START_BALANCE = replace(convert(varchar(100),convert(money,START_BALANCE),1),'.00',''), " +
                                "PRINCIPAL = replace(convert(varchar(100),convert(money,PRINCIPAL),1),'.00',''), " +
                                "MARGIN = replace(convert(varchar(100),convert(money,MARGIN),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                "OWNRETENTION = replace(convert(varchar(100),convert(money,OWNRETENTION),1),'.00','') " +
                                "from APPLICATION_SUMINS_SERIES a " +
                                "where REGNO='" + LB_REGNO.Text + "' " +
                                "order by " +
                                "a.REGNO, a.SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select " +
                                "SEQ, " +
                                "VALUEDATE = convert(varchar(20), VALUEDATE, 103), " +
                                "PRINCIPAL, " +
                                "MARGIN " +
                                "from APPLICATION_SUMINS_SERIES " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' order by SEQ";
            conn.ExecuteQuery();
            GlobalUse.ExportDataSetToExcel(conn.GetDataTable().Copy(), this, LB_REGNO.Text + ".xls", true);
        }

    }
}
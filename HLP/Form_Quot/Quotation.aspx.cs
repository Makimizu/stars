using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class Quotation : System.Web.UI.Page
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
            DDL_LOB.Items.Add(new ListItem("-- ALL --", ""));
            conn.QueryString = "select CODE,DESCR from PR_COMPANY_LOB";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_PRODUCT.Items.Add(new ListItem("-- ALL --", ""));
            conn.QueryString = "select CODE,DESCR from PARAM_PRODUCT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            string where = "where 1=1 ";
            string date1 = "'1 jan 1980'";
            string date2 = "GETDATE()";

            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                where = where + " and CREATEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
            {
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            }

            if (TXT_QUOTNO.Text.Trim() != "")
            {
                where = where + "and QUOTNO = '" + TXT_QUOTNO.Text.Trim() + "' ";
            }

            if (DDL_LOB.SelectedValue != "")
            {
                where = where + "and COMPANY_LOB = '" + DDL_LOB.SelectedValue + "' ";
            }

            if (DDL_PRODUCT.SelectedValue != "")
            {
                where = where + "and PRODUCT_CODE = '" + DDL_PRODUCT.SelectedValue + "' ";
            }

            if (TXT_TGLCREATE1.Text.Trim() != "" || TXT_TGLCREATE2.Text.Trim() != "")
            {
                if (TXT_TGLCREATE1.Text.Trim() != "")
                    date1 = "'" + GlobalUse.GlobalDateFormat(TXT_TGLCREATE1.Text.Trim(), "d/M/yyyy") + "'";
                if (TXT_TGLCREATE2.Text.Trim() != "")
                    date2 = "'" + GlobalUse.GlobalDateFormat(TXT_TGLCREATE2.Text.Trim(), "d/M/yyyy") + "'";

                where = where + "and (convert(date,CREATEDATE) between convert(date," + date1 + ") and convert(date," + date2 + ")) ";
            }

            conn.QueryString = "select " +
                                "[QUOT NO] = QUOTNO, " +
                                "[COMPANY] = COMPANY_NAME, " +
                                "[PRODUCT] = PRODUCT_NAME, " +
                                "[LOB] = LOB_DESCR, " +
                                "[TOT VERSION] = TOTVER, " +
                                "[CREATE BY] = CREATEBY, " +
                                "[CREATE DATE] = CREATEDATE " +
                                "from V_QUOTATION " + where  + 
                                "order by CREATEDATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("QuotationFrame.aspx?code=" + e.Item.Cells[1].Text + ReadOnly);
            }
        }
    }
}

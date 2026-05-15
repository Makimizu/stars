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
    public partial class CustodianProduct : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillAvailableProduct();
                FillSelectedProduct();
            }
        }

        protected void FillAvailableProduct()
        {
            conn.QueryString = "select PRODUCT_CODE, PRODUCT_NAME = PRODUCT_CODE + ' - ' + PRODUCT_NAME from V_CUSTODIAN_MASTER_PRODUCT a where TAKEN = 0 order by a.PRODUCT_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SEARCH.DataSource = dt;
            DGR_SEARCH.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LinkButton bt = (LinkButton)DGR_SEARCH.Items[i].FindControl("LBT");
                bt.Text = DGR_SEARCH.Items[i].Cells[1].Text;
            }
        }

        protected void FillSelectedProduct()
        {
            conn.QueryString = "select PRODUCT_CODE, PRODUCT_NAME = PRODUCT_CODE + ' - ' + PRODUCT_NAME from V_CUSTODIAN_MASTER_PRODUCT a where COMPANY_CODE = '" + LB_ID.Text + "' and TAKEN = 1 order by a.PRODUCT_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LinkButton bt = (LinkButton)DGR_SELECTED.Items[i].FindControl("LBT");
                bt.Text = DGR_SELECTED.Items[i].Cells[1].Text;
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillAvailableProduct();
        }

        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                conn.QueryString = "insert into CUSTODIAN_MASTER_PRODUCT " +
                                    "select " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + LB_ID.Text + "'";
                conn.ExecuteNonQuery();

                FillAvailableProduct();
                FillSelectedProduct();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CUSTODIAN_MASTER_PRODUCT " +
                                    "where " +
                                    "PRODUCT_CODE   = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillAvailableProduct();
                FillSelectedProduct();
            }
        }
    }
}
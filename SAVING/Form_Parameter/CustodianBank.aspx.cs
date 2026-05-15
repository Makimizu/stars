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
    public partial class CustodianBank : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillAvailableAccNo();
                FillSelectedAccNo();
            }
        }

        protected void FillAvailableAccNo()
        {
            conn.QueryString = "select NOREK, BANK, BANK_DESCR from	V_CUSTODIAN_BANK_ACCOUNT where TAKEN = 0 order by 2";
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

        protected void FillSelectedAccNo()
        {
            conn.QueryString = "select NOREK, BANK, BANK_DESCR from	V_CUSTODIAN_BANK_ACCOUNT where COMPANY_CODE	= '" + LB_ID.Text + "' order by 2";
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


        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                conn.QueryString = "insert into CUSTODIAN_BANK_ACCOUNT " +
                                    "select " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + LB_ID.Text + "'," +                                    
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();

                FillAvailableAccNo();
                FillSelectedAccNo();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CUSTODIAN_BANK_ACCOUNT " +
                                    "where " +
                                    "ACCNO      = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillAvailableAccNo();
                FillSelectedAccNo();
            }
        }
    }
}
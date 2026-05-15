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
    public partial class CustodianSelect : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAvailableList();
                FillSelectedList();
                Setup();
            }
        }

        protected void Setup()
        {
            if (DGR_SELECTED.Items.Count > 0)
            {
                SetSelected(DGR_SELECTED.Items[0].Cells[0].Text, DGR_SELECTED.Items[0].Cells[1].Text);
            }
        }

        protected void FillAvailableList()
        {
            conn.QueryString = "exec SP_CUSTODIAN_AVAILABLE '" + TXT_SEARCH.Text.Trim() + "'";
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

        protected void FillSelectedList()
        {
            conn.QueryString = "exec SP_CUSTODIAN_SELECTED";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                Button btX = (Button)DGR_SELECTED.Items[i].FindControl("BT_X");
                LinkButton bt = (LinkButton)DGR_SELECTED.Items[i].FindControl("LBT");
                bt.Text = DGR_SELECTED.Items[i].Cells[1].Text;

                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE?')){return false;};");
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillAvailableList();
        }

        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                conn.QueryString = "exec SP_CUSTODIAN_MASTER_INSERT " +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillAvailableList();
                FillSelectedList();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                SetSelected(e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CUSTODIAN_MASTER where " +
                                    "COMPANY_CODE = '" + e.Item.Cells[0].Text + "'";                                    
                conn.ExecuteNonQuery();

                FillAvailableList();
                FillSelectedList();

                if (DGR_SELECTED.Items.Count > 0)
                {
                    SetSelected(DGR_SELECTED.Items[0].Cells[0].Text, DGR_SELECTED.Items[0].Cells[1].Text);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustSelected.location.href = '../Standard/default.html';</script>");
                }
            }
        }

        protected void SetSelected(string CODE, string DESCR)
        {
            LB_TITLE.Text = DESCR.ToUpper();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustSelected.location.href = 'CustodianSelectedFrame.aspx?ID=" + CODE + "';</script>");
        }
    }
}
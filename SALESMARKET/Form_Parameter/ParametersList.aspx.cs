using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace SALESMARKET.Form_Parameter
{
    public partial class ParametersList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PREFIX.Text = Request.QueryString["prefix"];
                LB_PAGE.Text = Request.QueryString["page"];
                Setup();
            }
        }

        protected void Setup()
        {
            FillLBX();
        }

        protected void TXT_PARAM_TextChanged(object sender, EventArgs e)
        {
            FillLBX();
        }

        protected void FillLBX()
        {
            conn.QueryString = "select name,alias=replace(replace(name,'" + LB_PREFIX.Text + "',''),'_',' ') from sysobjects where LEFT(name," + LB_PREFIX.Text.Length.ToString() + ") = '" + LB_PREFIX.Text + "' and xtype='U' and replace(replace(name,'" + LB_PREFIX.Text + "',''),'_',' ') like '%" + TXT_PARAM.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR0.DataSource = dt;
            DGR0.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LinkButton bt = (LinkButton)DGR0.Items[i].FindControl("LBT");
                bt.Text = DGR0.Items[i].Cells[1].Text;
            }
        }

        protected void DGR0_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.parambody.location.href = '" + LB_PAGE.Text + ".aspx?prefix=" + LB_PREFIX.Text + "&code=" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}
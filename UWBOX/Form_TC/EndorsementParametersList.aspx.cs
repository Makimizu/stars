using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace UWBOX.Form_TC
{
    public partial class EndorsementParametersList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            conn.QueryString = "select " +
                                "CODE		= a.CODE, " +
                                "DESCR		= b.DESCR + ' - ' + a.DESCR " +
                                "from		PARAM_ENDORSEMENT a " +
                                "inner join	PR_ENDORSEMENT_GROUP b on a.GROUP_CODE = b.CODE " +
                                "order by " +
                                "b.DESCR, a.DESCR";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR0.DataSource = dt;
            DGR0.DataBind();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LinkButton bt = (LinkButton)DGR0.Items[i].FindControl("LBT");
                bt.Text = DGR0.Items[i].Cells[1].Text.ToUpper();
            }
        }

        protected void DGR0_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.parambody.location.href = 'EndorsementParameters.aspx?prefix=" + e.Item.Cells[1].Text + "&code=" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}
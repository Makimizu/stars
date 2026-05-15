using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class PolicyTC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {

            LoadLastTC();
        }

        protected void LoadLastTC()
        {
            conn.QueryString = "select top 1 TC_ID from GLIFE.dbo.POLICY_TC " +
                                "where " +
                                "POLICY_ID = " + LB_ID.Text + " " +
                                "order by START_DATE desc";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                LoadTC(conn.GetFieldValue("TC_ID").ToString());
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "CODE = a.TC_ID, " +
                                "START_DATE = convert(varchar(20),a.START_DATE,106), " +
                                "DESCR = a.TC_ID + ' - ' + b.DESCR " +
                                "from GLIFE.dbo.POLICY_TC a " +
                                "left join V_LINK_UW_TC_MASTER b on a.TC_ID = b.CODE " +
                                "where " +
                                "a.POLICY_ID = " + LB_ID.Text + " " +
                                "order by " +
                                "a.START_DATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LBT_DATE");
                lbt.Text = DGR.Items[i].Cells[2].Text;
            }

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void LoadTC(string code)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policyTCbody.location.href = 'TC.aspx?CODE=" + code + "';</script>");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadTC(e.Item.Cells[1].Text);
            }
        }
    }
}
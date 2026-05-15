using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Agents
{
    public partial class Agent_Approval_Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "[KODE] = CODE, " +
                                "[NAMA] = FRONT_NAME + ' ' + LAST_NAME, " +
                                "[CHANNEL] = CD_DESCR, " +
                                "[SUB CHANNEL] = SUBCD_DESCR " +
                                "from V_M_AGENTS where TRACK='1' order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCode = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lbCode.Text = DGR.Items[i].Cells[0].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Write("<script language='javascript'>parent.pagebutton.location.href = 'Agent_Approval_Button.aspx?code=" + e.Item.Cells[0].Text + "';</script>");
                Response.Write("<script language='javascript'>parent.pagebody.location.href = 'Agent_Entry.aspx?code=" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}
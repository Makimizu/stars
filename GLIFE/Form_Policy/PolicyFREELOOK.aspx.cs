using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyFREELOOK : System.Web.UI.Page
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
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.POLICY_ID, " +
                                "a.SEQ, " +
                                "a.ENDORSEMENT_TYPE, " +
                                "c.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "a.ENDORSEMENT_DESCR, " +
                                "BALANCE = replace(convert(varchar(100), convert(money, b.BALANCE), 1), '.00', ''), " +
                                "USERBY	= LAST_TRACK_BY + ' - (' + convert(varchar(100),LAST_TRACK_DATE) + ')' " +
                                "from V_POLICY_ENDORSEMENT_MASTER a " +
                                "inner join V_POLICY_SAVING_BALANCE b on a.POLICY_ID = b.POLICY_ID " +
                                "inner join POLICY c on a.POLICY_ID = c.ID " +
                                "where " +
                                "LAST_TRACK = 1 " +
                                "order by a.LAST_TRACK_DATE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LBT_ID");
                lbt.Text = DGR.Items[i].Cells[3].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("PolicyFREELOOKFrame.aspx?ID=" + e.Item.Cells[0].Text + "-" + e.Item.Cells[1].Text + "-" + e.Item.Cells[2].Text);
            }
        }
    }
}
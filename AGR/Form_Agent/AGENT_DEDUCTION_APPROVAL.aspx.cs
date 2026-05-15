using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class AGENT_DEDUCTION_APPROVAL : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PARAM_REMUN_DEDUCTION where CODE not in ('CLAWBACK','TAX') order by 2";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "a.APPROVEBY is null ";

            if (TXT_CODE.Text.Trim() != "")
                where = where + "and a.AGENT_CODE = '" + TXT_CODE.Text.Trim() + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + "and a.AGENT_NAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + "and a.DEDUCTION_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "DEDUCTION_ID, " +
                                "APPROVE                = (case when a.DEDUCTION_TERM>0 and a.DEDUCTION_TERM_AMOUNT=a.DEDUCTION_AMOUNT then 1 else 0 end), " +
                                "AGENT_CODE, " +
                                "AGENT_NAME, " +
                                "DEDUCTION_TYPE_DESCR, " +
                                "DEDUCTION_AMOUNT		= replace(convert(varchar(100), convert(money, DEDUCTION_AMOUNT), 1), '.00', ''), " +
                                "DEDUCTION_TERM_AMOUNT	= replace(convert(varchar(100), convert(money, DEDUCTION_TERM_AMOUNT), 1), '.00', ''), " +
                                "a.DEDUCTION_TERM, " +
                                "REQUESTBY				= REQUESTBY + ' - ' + convert(varchar(100), REQUESTDATE) " +
                                "from		V_M_AGENTS_DEDUCTION a " +
                                "where " + where + " " +
                                "order by " +
                                "a.REQUESTDATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                Button btAPPROVE = (Button)DGR.Items[i].FindControl("BT_APPROVE");
                lb.Text = DGR.Items[i].Cells[2].Text;

                if (DGR.Items[i].Cells[3].Text != "1")
                    btAPPROVE.Visible = false;
            }
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("AGENT_DEDUCTION.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Approve")
            {
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_DEDUCTION_APPROVAL " +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "1," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(500000);
                }
                catch (Exception ex)
                {

                    System.Threading.Thread.Sleep(2000);
                    //string message = ex.Message.Contains("Total payment term > dari amount yang diajukan") ? "Total payment term > dari amount yang diajukan" : ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + ex.Message + "');", true);
                }
                
                FillDGR();
            }

            if (e.CommandName == "Reject")
            {
                conn.QueryString = "exec SP_M_AGENT_DEDUCTION_APPROVAL " +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "0," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(500000);
                FillDGR();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}
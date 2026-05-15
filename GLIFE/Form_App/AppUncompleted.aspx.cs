using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class AppUncompleted : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                //FillDGR();

                //string script = "$(document).ready(function () { $('[id*=BT_SEARCH]').click(); });";
                //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";
                        
            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (DDL_NOAGENT.SelectedValue != "")
                where = where + " and a.NOAGENT = " + DDL_NOAGENT.SelectedValue + " ";

            if (DDL_NOBRANCH.SelectedValue != "")
                where = where + " and a.NOBRANCH = " + DDL_NOBRANCH.SelectedValue + " ";

            if (DDL_NOPREMIUM.SelectedValue != "")
                where = where + " and a.NOPREMIUM = " + DDL_NOPREMIUM.SelectedValue + " ";

            if (DDL_NOUWCODE.SelectedValue != "")
                where = where + " and a.NOUWCODE = " + DDL_NOUWCODE.SelectedValue + " ";

            conn.QueryString = "select " +
                                "REGNO, " +
                                "FULLNAME, " +
                                "POLICY_NO, " +
                                "COMPANY, " +
                                "BRANCH_SQL, " +
                                "AGENT_SQL, " +
                                "NOUWCODE	= (case when NOUWCODE = 1 then 'YES' else '' end), " +
                                "NOPREMIUM	= (case when NOPREMIUM = 1 then 'YES' else '' end) " +
                                "from V_APPLICATION_MASTER_UNCOMPLETE a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.POLICY_NO, a.FULLNAME";
            conn.ExecuteQuery(10000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                DropDownList ddlBRANCH = (DropDownList)DGR.Items[i].FindControl("DDL_BRANCH");
                DropDownList ddlAGENT = (DropDownList)DGR.Items[i].FindControl("DDL_AGENT");

                lbCODE.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    conn.QueryString = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    ddlBRANCH.Items.Add(new ListItem("", ""));
                    for (int j = 0; j < conn.GetRowCount(); j++)
                        ddlBRANCH.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                    ddlBRANCH.Visible = true;
                }

                if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    conn.QueryString = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    ddlAGENT.Items.Add(new ListItem("", ""));
                    for (int j = 0; j < conn.GetRowCount(); j++)
                        ddlAGENT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                    ddlAGENT.Visible = true;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_UPDATE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlBRANCH = (DropDownList)DGR.Items[i].FindControl("DDL_BRANCH");
                DropDownList ddlAGENT = (DropDownList)DGR.Items[i].FindControl("DDL_AGENT");

                string update = "";
                if (ddlBRANCH.Visible && ddlBRANCH.SelectedValue != "")
                    update = update + "BRANCH_CODE = '" + ddlBRANCH.SelectedValue + "', ";
                if (ddlAGENT.Visible && ddlAGENT.SelectedValue != "")
                    update = update + "USERBY = '" + ddlAGENT.SelectedValue + "', ";

                if (update.Length > 0)
                {
                    conn.QueryString = "update APPLICATION_MASTER set " +
                                        update +
                                        "REGNO = REGNO " +
                                        "where REGNO = '" + DGR.Items[i].Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
            }

            FillDGR();
        }
    }
}
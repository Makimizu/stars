using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace SALESMARKET.Form_Agents
{
    public partial class Agent_List : System.Web.UI.Page
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
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" +  TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and REPLACE(LTRIM(RTRIM(isnull(FRONT_NAME,'') + ' ' + isnull(MID_NAME,'') + ' ' + isnull(LAST_NAME,''))),'  ',' ') like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.ACTIVE = " + DDL_STAT.SelectedValue + " ";

            conn.QueryString = "select " +
                                "CODE, " +
                                "SUBCD_DESCR, " +
                                "NAMA = REPLACE(LTRIM(RTRIM(isnull(FRONT_NAME,'') + ' ' + isnull(MID_NAME,'') + ' ' + isnull(LAST_NAME,''))),'  ',' '), " +
                                "DOB = convert(varchar(20),a.DOB,106), " +
                                "UPLINER, " +
                                "UPLINER_NAME, " +
                                "BRANCH_DESCR, " +
                                "ACTIVE " +
                                "from V_M_AGENTS a " +
                                "where " +
                                "1=1 " + where +
                                "order by " +
                                "NAMA";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                lb.Text = DGR.Items[i].Cells[1].Text;
                if (DGR.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Entry.aspx?code=" + e.Item.Cells[1].Text);
            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and REPLACE(LTRIM(RTRIM(isnull(FRONT_NAME,'') + ' ' + isnull(MID_NAME,'') + ' ' + isnull(LAST_NAME,''))),'  ',' ') like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.ACTIVE = " + DDL_STAT.SelectedValue + " ";

            conn.QueryString = "select " +
                                "CODE, " +
                                "SUBCD_DESCR, " +
                                "NAMA = REPLACE(LTRIM(RTRIM(isnull(FRONT_NAME,'') + ' ' + isnull(MID_NAME,'') + ' ' + isnull(LAST_NAME,''))),'  ',' '), " +
                                "DOB = convert(varchar(20),a.DOB,106), " +
                                "UPLINER, " +
                                "UPLINER_NAME, " +
                                "BRANCH_DESCR, " +
                                "ACTIVE_DESCR " +
                                "from V_M_AGENTS a " +
                                "where " +
                                "1=1 " + where +
                                "order by " +
                                "NAMA";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "AGENTS", true);
        }

        protected void DB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if ((CheckBox)sender == cb)
                {
                    string stat = "0";
                    if (cb.Checked)
                        stat = "1";

                    try
                    {
                        conn.QueryString = "update M_AGENTS set ACTIVE = " + stat + " where CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteNonQuery();
                        FillDGR();
                        return;
                    }
                    catch { }
                }
            }
        }
    }
}
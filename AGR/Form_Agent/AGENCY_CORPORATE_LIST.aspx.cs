using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;

namespace AGR
{
    public partial class AGENCY_CORPORATE_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FILL_DGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI";
            conn.ExecuteQuery();
            DDL_PROVINCE.Items.Clear();
            DDL_PROVINCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FILL_DGR();
        }

        protected void FILL_DGR()
        {


            string where = "";


            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                where = where + " and AGENT = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";

            if (TXT_COMPANY_CODE.Text.Trim() != "")
                where = where + " and COMPANY_CODE = '" + TXT_COMPANY_CODE.Text.Trim() + "' ";

            if (TXT_COMPANY_NAME.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY_NAME.Text.Trim() + "%' ";

            if (DDL_PROVINCE.SelectedValue != "")
                where = where + " and PROPINSI = '" + DDL_PROVINCE.SelectedValue + "' ";

            if (DDL_AGENT.SelectedValue != "")
                where = where + " " + DDL_AGENT.SelectedValue + " ";

            if (DDL_BRANCH.SelectedValue != "")
                where = where + " " + DDL_BRANCH.SelectedValue + " ";

            conn.QueryString = "select " +
                                "COMPANY_CODE, " +
                                "COMPANY_NAME, " +
                                "COMPANY_ADDRESS, " +
                                "PROPINSI_DESCR, " +
                                "AGENTS, " +
                                "BRANCHS " +
                                "from V_LINK_CB_COMPANY a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbCode = (Label)DGR.Items[i].FindControl("LB_CODE");
                lbCode.Text = DGR.Items[i].Cells[0].Text;

                lbCode.Text = "<a href='AGENCY_CORPORATE_FRAME.aspx?ID=" + DGR.Items[i].Cells[0].Text + "'>" + DGR.Items[i].Cells[0].Text + "</a>";
            }

            if (Request.Browser.IsMobileDevice)
            {
                for (int i = 0; i < DGR.Columns.Count; i++)
                {
                    DGR.Columns[i].Visible = false;
                    switch (i)
                    {
                        case 1: DGR.Columns[i].Visible = true; break;
                        case 2: DGR.Columns[i].Visible = true; break;
                    }
                }
            }

            LB_RECORD.Text = conn.GetRowCount().ToString() + " records";
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FILL_DGR();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";


            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                where = where + " and AGENT = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";

            if (TXT_COMPANY_CODE.Text.Trim() != "")
                where = where + " and COMPANY_CODE = '" + TXT_COMPANY_CODE.Text.Trim() + "' ";

            if (TXT_COMPANY_NAME.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY_NAME.Text.Trim() + "%' ";

            if (DDL_PROVINCE.SelectedValue != "")
                where = where + " and PROPINSI = '" + DDL_PROVINCE.SelectedValue + "' ";

            if (DDL_AGENT.SelectedValue != "")
                where = where + " " + DDL_AGENT.SelectedValue + " ";

            if (DDL_BRANCH.SelectedValue != "")
                where = where + " " + DDL_BRANCH.SelectedValue + " ";

            conn.QueryString = "select " +
                                "COMPANY_CODE, " +
                                "COMPANY_NAME, " +
                                "COMPANY_ADDRESS, " +
                                "PROPINSI_DESCR, " +
                                "AGENTS, " +
                                "BRANCHS " +
                                "from V_LINK_CB_COMPANY a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "AGENCY_CORPORATE_LIST", true);
        }
    }
}
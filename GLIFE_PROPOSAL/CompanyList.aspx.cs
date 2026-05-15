using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace GLIFE_PROPOSAL
{
    public partial class CompanyList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                Setup();
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


            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_LINE_OF_BUSINESS";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            DDL_LOB.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

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

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO = '" + TXT_POLICYNO.Text.Trim() + "' ";

            if (TXT_COMPANY_NAME.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY_NAME.Text.Trim() + "%' ";


            if (DDL_PROVINCE.SelectedValue != "")
                where = where + " and PROPINSI = '" + DDL_PROVINCE.SelectedValue + "' ";

            if (DDL_LOB.SelectedValue != "")
                where = where + " and COMPANY_LOB = '" + DDL_LOB.SelectedValue + "' ";


            conn.QueryString = "select " +
                                "a.COMPANY_CODE, " +
                                "a.POLICY_NO, " +
                                "COMPANY_NAME = COMPANY_NAME + ' ' + isnull(COMPANY_TYPE_DESCR,''), " +
                                "COMPANY_LOB_DESCR, " +
                                "PROPINSI_DESCR " +
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

                lbCode.Text = "<a href='Company.aspx?ID=" + DGR.Items[i].Cells[0].Text + "'>" + DGR.Items[i].Cells[0].Text + "</a>";
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
    }
}
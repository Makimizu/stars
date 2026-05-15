using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CUSTOMERS.Form_Client
{
    public partial class Company : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                try
                {
                    LB_APP_ID.Text = Request.QueryString["APP_ID"];
                }
                catch { }
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_COMPANY_CATEGORY";
            conn.ExecuteQuery();
            DDL_CATEGORY.Items.Clear();
            DDL_CATEGORY.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CATEGORY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select SUB_CODE,DESCR from V_LINK_PARAM_SUB_CHANNEL_DISTRIBUTION order by 2";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Clear();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_LOB";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            DDL_LOB.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_STATUS";
            conn.ExecuteQuery();
            DDL_COMPANY_STATUS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COMPANY_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }




        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FILL_DGR();
        }

        protected void FILL_DGR()
        {
            string sdate = "'1 jan 1980'";
            string edate = "GETDATE()";

            string where = "";
            string joinapp = "";

            if (LB_APP_ID.Text.Trim() != "")
            {
                joinapp = "inner join (select COMPANY_CODE from POLICY_GROUP_MASTER where APP_ID='" +LB_APP_ID.Text+ "') b on a.COMPANY_CODE=b.COMPANY_CODE ";
            }

            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                where = where + " and AGENT = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";

            if (TXT_COMPANY_CODE.Text.Trim() != "")
                where = where + " and COMPANY_CODE = '" + TXT_COMPANY_CODE.Text.Trim() + "' ";

            if (TXT_COMPANY_NAME.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY_NAME.Text.Trim() + "%' ";

            if (TXT_AGENT.Text.Trim() != "")
                where = where + " and AGENT like '%" + TXT_AGENT.Text.Trim() + "%' ";

            if (TXT_AGENT_NAME.Text.Trim() != "")
                where = where + " and AGENT_NAME like '%" + TXT_AGENT_NAME.Text.Trim() + "%' ";

            if (DDL_CATEGORY.SelectedValue != "")
                where = where + " and COMPANY_CATEGORY = '" + DDL_CATEGORY.SelectedValue + "' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and SUB_CHANEL_DIST = '" + DDL_CHANNEL.SelectedValue + "' ";

            if (DDL_LOB.SelectedValue != "")
                where = where + " and COMPANY_LOB = '" + DDL_LOB.SelectedValue + "' ";

            if (TXT_REGDATEFROM.Text.Trim() != "" || TXT_REGDATETO.Text.Trim() != "")
            {
                if (TXT_REGDATEFROM.Text.Trim() != "")
                    sdate = "'" + GlobalUse.GlobalDateFormat(TXT_REGDATEFROM.Text.Trim(), "d/M/yyyy") + "'";

                if (TXT_REGDATETO.Text.Trim() != "")
                    edate = "'" + GlobalUse.GlobalDateFormat(TXT_REGDATETO.Text.Trim(), "d/M/yyyy") + "'";

                where = where + " and (convert(date,a.COMPANY_REGISTERED_DATE) between convert(date," + sdate + ") and convert(date," + edate + ")) ";
            }

            conn.QueryString = "select " +
                                "a.COMPANY_CODE, " +
                                "COMPANY_NAME = COMPANY_NAME + ' ' + isnull(COMPANY_TYPE_DESCR,''), " +
                                "COMPANY_CATEGORY_DESCR, " +
                                "COMPANY_LOB_DESCR, " +
                                "PROPINSI_DESCR, " +
                                "COMPANY_REGISTERED_DATE = convert(varchar(20),COMPANY_REGISTERED_DATE,106), " +
                                "STAT_DESCR, " +
                                "AGENT, " +
                                "AGENT_NAME, " +
                                "SUB_CHANEL_DIST_DECSR " +
                                "from V_COMPANY a " + joinapp +
                                "where " +
                                "STAT like '%" + DDL_COMPANY_STATUS.SelectedValue + "%' " + 
                                where + 
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbCode = (Label)DGR.Items[i].FindControl("LB_CODE");
                lbCode.Text = DGR.Items[i].Cells[0].Text;

                lbCode.Text = "<a href='CompanyFrame.aspx?CODE=" + DGR.Items[i].Cells[0].Text + "'>" + DGR.Items[i].Cells[0].Text + "</a>";
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
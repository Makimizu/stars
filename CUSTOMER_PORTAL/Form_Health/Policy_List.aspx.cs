using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace CUSTOMER_PORTAL.Form_Health
{
    public partial class Policy_List : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_TIPE_POLIS order by 1";
            conn.ExecuteQuery();

            DDL_NBRN.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NBRN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PARAM_ACT_MOP order by 1";
            conn.ExecuteQuery();

            DDL_MOP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MOP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PARAM_ACT_TPA order by 1";
            conn.ExecuteQuery();

            DDL_TPA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RECORD.Text = "";
            string where = "";

            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
            {
                string downliners = "";

                conn.QueryString = "exec SP_LINK_MR_AGENT_DOWNLINER '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    downliners = downliners + ",'" + conn.GetFieldValue(i, 0).ToString() + "'";
                }

                where = where + " and a.AGENT_CODE in ('" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'" + downliners + ") ";
            }

            if (TXT_POLICYNO.Text.Trim() != "")
            {
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";
            }

            if (TXT_COMPANY.Text.Trim() != "")
            {
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            }

            if (TXT_CHANNEL.Text.Trim() != "")
            {
                where = where + " and a.SUB_CODE_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";
            }

            if (TXT_AGENT.Text.Trim() != "")
            {
                where = where + " and a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' ";
            }

            if (DDL_NBRN.SelectedValue != "")
            {
                where = where + " and a.TIPE_DESCR = '" + DDL_NBRN.SelectedValue + "' ";
            }

            if (DDL_MOP.SelectedValue != "")
            {
                where = where + " and a.MOP = '" + DDL_MOP.SelectedValue + "' ";
            }

            if (DDL_TPA.SelectedValue != "")
            {
                where = where + " and a.TPA = '" + DDL_TPA.SelectedValue + "' ";
            }

            if (DDL_STAT.SelectedValue != "")
            {
                where = where + " and a.STAT = '" + DDL_STAT.SelectedValue + "' ";
            }

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_ENDDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.END_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_ENDDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_ENDDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.END_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_ENDDATE2.Text.Trim(), "d/M/yyyy") + "') ";


            conn.QueryString = "select " +
                                "ID, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "END_DATE = convert(varchar(20),END_DATE,106), " +
                                "IS_CAPTIVE = (case when isnull(IS_CAPTIVE ,0) > 0 then 'YES' else 'NO' end), " +
                                "TIPE_DESCR, " +
                                "MOP_DESCR, " +
                                "PRODUCT, " +
                                "TPA_DESCR, " +
                                "AGENT_NAME, " +
                                "SUB_CODE_DESCR, " +
                                "STAT, " +
                                "PASSWORD " +
                                "from V_LINK_HO_POLICY a " +
                                "where " +
                                "1 = 1 " + where +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                BT_XLS.Visible = true;

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                Label lbPWD = (Label)DGR.Items[i].FindControl("LB_PWD");

                lbID.Text = DGR.Items[i].Cells[4].Text;
                try
                {
                    lbPWD.Text = Crypto.DecryptStringAES(DGR.Items[i].Cells[2].Text.Replace("&nbsp;", ""));
                }
                catch { }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Policy_Frame.aspx?ID=" + e.Item.Cells[1].Text);
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

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }
    }
}
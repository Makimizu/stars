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
    public partial class Member_List : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_GENDER order by 1";
            conn.ExecuteQuery();

            DDL_SEX.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_SEX.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_FAMILY_GROUP order by 1";
            conn.ExecuteQuery();

            DDL_FAMREL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FAMREL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_STATUS_PESERTA order by 1";
            conn.ExecuteQuery();

            DDL_STAT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "", agentjoin = "";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_BRANCH.Text.Trim() != "")
                where = where + " and a.NAMA_CABANG like '%" + TXT_BRANCH.Text.Trim() + "%' ";

            if (DDL_SEX.SelectedValue != "")
                where = where + " and a.SEX = '" + DDL_SEX.SelectedValue + "' ";

            if (DDL_FAMREL.SelectedValue != "")
                where = where + " and a.FAMILY_GROUP = '" + DDL_FAMREL.SelectedValue + "' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.STAT = '" + DDL_STAT.SelectedValue + "' ";

            if (TXT_DOB1.Text.Trim() != "")
                where = where + " and convert(date,a.DOB) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOB1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DOB2.Text.Trim() != "")
                where = where + " and convert(date,a.DOB) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOB2.Text.Trim(), "d/M/yyyy") + "') ";


            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
            {
                string downliners = "";

                conn.QueryString = "exec SP_LINK_MR_AGENT_DOWNLINER '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    downliners = downliners + ",'" + conn.GetFieldValue(i, 0).ToString() + "'";
                }

                agentjoin = "inner join (select distinct POLICY_ID from V_LINK_HO_POLICY_PERIOD where AGENT_CODE in ('" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'" + downliners + ")) b on a.POLICY_ID=b.POLICY_ID ";
            }

            conn.QueryString = "select " +
                                "REGNO, " +
                                "NAMA = LTRIM(NAMA), " +
                                "POLICY_NO, " +
                                "COMPANY_NAME = left(COMPANY_NAME,30), " +
                                "BRANCH = left(NAMA_CABANG,30), " +
                                "SEX, " +
                                "FAMILY_GROUP_DESCR, " +
                                "DOB = convert(varchar(20),DOB,106), " +
                                "TGL_MASUK = convert(varchar(20),TGL_MASUK,106), " +
                                "STATUS = (case when isnull(STAT,0)=1 then 'AKTIF' else 'NON AKTIF' end)  " +
                                "from V_LINK_HO_PESERTA_MASTER a " + agentjoin + " " +
                                "where " +
                                "1 = 1 " + where +
                                "order by a.COMPANY_NAME, a.REGNO";
            conn.ExecuteQuery();

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                lbID.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Member_Frame.aspx?REGNO=" + e.Item.Cells[1].Text);
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
    }
}
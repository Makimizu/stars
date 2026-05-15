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
    public partial class Claim_PendingReject : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_CLAIM_TP order by 1";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRACK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "";

            if (TXT_CLAIMNO.Text.Trim() != "")
                where = where + " and a.DOCNO = '" + TXT_CLAIMNO.Text.Trim() + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAME.Text.Trim() + "%' ";
            
            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";
            
            if (DDL_PR.SelectedValue != "")
                where = where + " and a.PR = '" + DDL_PR.SelectedValue + "' ";

            if (DDL_TRACK.SelectedValue != "")
                where = where + " and a.TP = '" + DDL_TRACK.SelectedValue + "' ";

            if (TXT_DOCDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.DOCDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOCDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DOCDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.DOCDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOCDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            
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

            conn.QueryString = "select " +
                                "DOCNO, " +
                                "DOCDATE = convert(varchar(20),a.DOCDATE,106), " +
                                "COMPANY, " +
                                "POLICY_NO, " +
                                "AGENT_CODE, " +
                                "NAMA, " +
                                "PR_DESCR, " +
                                "REPORT " +
                                "from V_LINK_HO_CLAIM_TP a " + 
                                "where " +
                                "1 = 1 " + where +
                                "order by a.DOCDATE";
            conn.ExecuteQuery();

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                lbID.Text = DGR.Items[i].Cells[1].Text;
                lbID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text + "','LETTER','height=400,width=800,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
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

        protected void DDL_TRACK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
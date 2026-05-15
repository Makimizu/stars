using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace LIFE.Form_Claim
{
    public partial class ClaimOrgBeneficiary : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_MODE.Text = Request.QueryString["MODE"].ToString();
                try
                {
                    LB_ORGTYPE.Text = Request.QueryString["ORGTYPE"].ToString();
                }
                catch
                {
                    LB_ORGTYPE.Text = "1";
                }

                FillClaimOrganization();
            }

        }

        protected void FillClaimOrganization()
        {
            DDL_ACCBANK.Items.Clear();
            conn.QueryString = "select KODE, BANK = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by 1";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select * from V_APPLICATION_ORG_BENEFICIARY where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_ORGTYPE.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_ORG.Text = conn.GetFieldValue("ORG_NAME").ToString();
                TXT_CERNO.Text = conn.GetFieldValue("CERNO").ToString();
                TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
                TXT_ACCNAME.Text = conn.GetFieldValue("ACCNAME").ToString();

                try
                {
                    DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
                }
                catch { }

                string PCT_CLM = conn.GetFieldValue("PCT_RISK").ToString();
                string PCT_CLM_SQL = conn.GetFieldValue("PCT_RISK_SQL").ToString();
                string PCT_INV = conn.GetFieldValue("PCT_NONRISK").ToString();
                string PCT_INV_SQL = conn.GetFieldValue("PCT_NONRISK_SQL").ToString();

                DDL_PCTCLM.Items.Clear();
                conn.QueryString = PCT_CLM_SQL;
                conn.ExecuteQuery();
                DDL_PCTCLM.Items.Add(new ListItem("0", "0"));
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PCTCLM.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

                try
                {
                    DDL_PCTCLM.SelectedValue = PCT_CLM;
                }
                catch { }

                DDL_PCTINV.Items.Clear();
                conn.QueryString = PCT_INV_SQL;
                conn.ExecuteQuery();
                DDL_PCTINV.Items.Add(new ListItem("0", "0"));
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PCTINV.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

                try
                {
                    DDL_PCTINV.SelectedValue = PCT_INV;
                }
                catch { }
            }
        }

        protected void BT_ORG_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_ORG_BENEFICIARY_UPSERT" +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + TXT_ORG.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_CERNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_ACCNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_ACCBANK.SelectedValue + "'," +
                                "'" + TXT_ACCNAME.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_PCTCLM.SelectedValue + "'," +
                                "'" + DDL_PCTINV.SelectedValue + "'," +
                                "'" + LB_ORGTYPE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillClaimOrganization();

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitheader.location.href = 'ClaimBenefit.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&ORGTYPE=" + LB_ORGTYPE.Text + "&MODE=" + LB_MODE.Text + "';</script>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementPayoutOrg : System.Web.UI.Page
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
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();
                LB_MODE.Text = Request.QueryString["MODE"].ToString();

                Setup();
                FillClaimOrganization();
            }
        }

        protected void Setup()
        {
            try
            {
                conn.QueryString = "select DESCR from V_CHARITY_MODE where CODE = " + LB_MODE.Text;
                conn.ExecuteQuery();
                LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
            }
            catch { }
        }

        protected void FillClaimOrganization()
        {
            DDL_ACCBANK.Items.Clear();
            conn.QueryString = "select KODE, BANK = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by 1";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select * from V_APPLICATION_ORG_BENEFICIARY where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_MODE.Text;
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

                string PCT_INV = conn.GetFieldValue("PCT_NONRISK").ToString();
                string PCT_INV_SQL = conn.GetFieldValue("PCT_NONRISK_SQL").ToString();


                DDL_PCTINV.Items.Clear();
                try
                {
                    conn.QueryString = PCT_INV_SQL;
                    conn.ExecuteQuery();
                    for (int i = 0; i < conn.GetRowCount(); i++)
                        DDL_PCTINV.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

                    DDL_PCTINV.SelectedValue = PCT_INV;
                }
                catch { }
            }
            else
            {
                if (LB_MODE.Text == "1")
                {
                    BT_ORG.Visible = false;
                    TXT_ORG.Enabled = false;
                    TXT_CERNO.Enabled = false;
                    TXT_ACCNO.Enabled = false;
                    TXT_ACCNAME.Enabled = false;
                    DDL_ACCBANK.Enabled = false;
                    DDL_PCTINV.Enabled = false;
                }
            }

            if (LB_MODE.Text != "1")
            {
                TR_PCT.Visible = false;
            }
        }

        protected void BT_ORG_Click(object sender, EventArgs e)
        {
            string INV_PCT = "0";
            if (DDL_PCTINV.SelectedValue != "")
                INV_PCT = DDL_PCTINV.SelectedValue;

            conn.QueryString = "exec SP_APPLICATION_ORG_BENEFICIARY_UPSERT" +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + TXT_ORG.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_CERNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_ACCNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_ACCBANK.SelectedValue + "'," +
                                "'" + TXT_ACCNAME.Text.Trim().Replace("'", "`") + "'," +
                                "0," +
                                INV_PCT + "," +
                                LB_MODE.Text + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();


            if (LB_MODE.Text == "1")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PAYOUT_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'ORG'," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                }
                catch { }
            }

            FillClaimOrganization();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.EndorsementPayoutheader.location.href = 'EndorsementPayOut.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + LB_TYPE.Text + "';</script>");
        }
    }
}
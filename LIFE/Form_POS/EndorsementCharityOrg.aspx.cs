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
    public partial class EndorsementCharityOrg : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                LoadRecord();
                CheckTrack();
            }
        }

        protected void Setup()
        {
            DDL_ACCBANK.Items.Clear();
            conn.QueryString = "select KODE, BANK = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by 1";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_CHARITY_ORG " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            TXT_ORG.Text = conn.GetFieldValue("ORG_NAME").ToString();
            TXT_CERNO.Text = conn.GetFieldValue("CERNO").ToString();
            TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
            TXT_ACCNAME.Text = conn.GetFieldValue("ACCNAME").ToString();

            DDL_ACCBANK.SelectedIndex = 0;
            try
            {
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
            }
            catch { }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                TXT_ORG.Enabled = false;
                TXT_CERNO.Enabled = false;
                TXT_ACCNO.Enabled = false;
                TXT_ACCNAME.Enabled = false;
                DDL_ACCBANK.Enabled = false;
                BT_ORG.Visible = false;
            }
        }

        protected void BT_ORG_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_CHARITY_ORG_UPDATE " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + TXT_ORG.Text.Trim() + "'," +
                                "'" + TXT_CERNO.Text.Trim() + "'," +
                                "'" + TXT_ACCNO.Text.Trim() + "'," +
                                "'" + DDL_ACCBANK.SelectedValue + "'," +
                                "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            LoadRecord();
        }
    }
}
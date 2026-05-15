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
    public partial class EndorsementReinstatement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();

                Setup();
                CheckTrack();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR = UPPER(DESCR) from UWBOX.dbo.PARAM_ENDORSEMENT where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TYPE_DESCR.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select REMARK from APPLICATION_ENDORSEMENT_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            TXT_REASON.Text = conn.GetFieldValue("REMARK").ToString();
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                TXT_REASON.ReadOnly = true;
                BT_SAVE.Visible = false;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "update APPLICATION_ENDORSEMENT_MASTER set " +
                                "REMARK         = '" + TXT_REASON.Text.Trim().Replace("'", "`") + "'," +
                                "LASTCHANGEBY   = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "LASTCHANGEDATE = GETDATE() " +
                                "where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "'";
            conn.ExecuteNonQuery();
            Setup();
        }
    }
}
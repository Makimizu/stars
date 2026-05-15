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
    public partial class ClaimShare : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                bDone = TrackDone();
                LoadShare();
                LoadPaymentAcc();
            }
        }

        protected void LoadPaymentAcc()
        {
            string disable = "0";
            if (bDone || Request.QueryString["readonly"] == "1")
            {
                disable = "1";
            }
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimsharebody.location.href = '../Form_Tools/PaymentAcc.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&CODE=CLM&DISABLE=" + disable + "';</script>");
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void LoadShare()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_SHARE " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SHARE.DataSource = dt;
            DGR_SHARE.DataBind();

            for (int i = 0; i < DGR_SHARE.Items.Count; i++)
            {
                if (DGR_SHARE.Items[i].Cells[0].Text == "TOTAL")
                {
                    DGR_SHARE.Items[i].BackColor = System.Drawing.Color.Gainsboro;
                    DGR_SHARE.Items[i].Font.Bold = true;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace LQ.Form_Claim
{
    public partial class ClaimShare : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
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
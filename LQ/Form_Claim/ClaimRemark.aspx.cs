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
    public partial class ClaimRemark : System.Web.UI.Page
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
                LoadRemark();
            }
        }

        protected void LoadRemark()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_REMARK " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();

            for (int i = 0; i < DGR_REMARK.Items.Count; i++)
            {
                Button btSAVE = (Button)DGR_REMARK.Items[i].FindControl("BT_SAVE");
                Label lbDESCR = (Label)DGR_REMARK.Items[i].FindControl("LB_DESCR");
                TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_REMARK");

                lbDESCR.Text = DGR_REMARK.Items[i].Cells[1].Text;
                txtREMARK.Text = DGR_REMARK.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (bDone || Request.QueryString["readonly"] == "1")
                {
                    btSAVE.Visible = false;
                    txtREMARK.ReadOnly = true;
                }
            }
        }
    }
}
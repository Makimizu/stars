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
    public partial class ClaimBenefitICD : System.Web.UI.Page
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
                LoadDGRSelected();
            }
        }


        protected void LoadDGRSelected()
        {
            TR_SELECTED.Visible = false;
            conn.QueryString = "exec SP_APPLICATION_CLAIM_ICD " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TR_SELECTED.Visible = true;
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ICD_SELECTED.DataSource = dt;
            DGR_ICD_SELECTED.DataBind();
        }

        
    }
}
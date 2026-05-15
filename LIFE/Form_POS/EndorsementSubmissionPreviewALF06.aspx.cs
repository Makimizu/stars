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
    public partial class EndorsementSubmissionPreviewALF06 : System.Web.UI.Page
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

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_FOP_CHANGE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for(int i = 0;i<DGR.Items.Count;i++)
            {
                if (DGR.Items[i].Cells[0].Text == "NEW")
                    DGR.Items[i].ForeColor = System.Drawing.Color.Blue;
            }
        }
    }
}
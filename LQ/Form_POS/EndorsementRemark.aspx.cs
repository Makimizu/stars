using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_POS
{
    public partial class EndorsementRemark : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadRemark();
            }
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
            LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
            bDone = TrackDone();
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_SEQ.Text + "')";
            conn.ExecuteQuery();

            if (int.Parse(conn.GetFieldValue("TRACK").ToString()) < 3)
                result = false;

            return result;
        }


        protected void LoadRemark()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARK " +
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
                Label lbDESCR = (Label)DGR_REMARK.Items[i].FindControl("LB_DESCR");
                TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_REMARK");

                lbDESCR.Text = DGR_REMARK.Items[i].Cells[1].Text;
                txtREMARK.Text = DGR_REMARK.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }
    }
}
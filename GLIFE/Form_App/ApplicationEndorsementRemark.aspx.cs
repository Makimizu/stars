using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationEndorsementRemark : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadRemark();
                ShowArchieve();
            }
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
            LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
            LB_TYPE.Text = Request.QueryString["TYPE"].ToString();
            bDone = TrackDone();
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_ENDORSEMENT_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "' and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void ShowArchieve()
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_06", LB_REGNO.Text + "-" + LB_SEQ.Text + "-" + LB_TYPE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSremarkbody.location.href = '" + URL + "';</script>");
        }

        protected void LoadRemark()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARK " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'";
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

                if (bDone)
                {
                    btSAVE.Visible = false;
                    txtREMARK.ReadOnly = true;
                }
            }
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtREMARK = (TextBox)e.Item.FindControl("TXT_REMARK");

                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARK_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + txtREMARK.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                LoadRemark();
            }
        }
    }
}
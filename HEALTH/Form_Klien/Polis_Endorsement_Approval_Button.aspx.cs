using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement_Approval_Button : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                LB_SEQ.Text = Request.QueryString["SEQ"];
                Setup();
            }
        }

        protected void Setup()
        {
            BT_APPROVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk APPROVE ?')){return false;};");
            BT_ROLLBACK.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_APPROVE '" + LB_ID.Text + "'," + LB_SEQ.Text + ",'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                                
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'Polis_Endorsement_Approval.aspx';</script>");
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }
        }

        protected void BT_ROLLBACK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_ROLLBACK '" + LB_ID.Text + "'," + LB_SEQ.Text;
                conn.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'Polis_Endorsement_Approval.aspx';</script>");
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }
        }
    }
}
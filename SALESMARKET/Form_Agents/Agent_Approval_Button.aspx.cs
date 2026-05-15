using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace SALESMARKET.Form_Agents
{
    public partial class Agent_Approval_Button : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                BT_APPROVAL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk APPROVE ?')){return false;};");
                BT_REJECT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk REJECT ?')){return false;};");
            }
        }

        protected void BT_APPROVAL_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_M_AGENTS_APPROVAL_VALIDATION " +
                                    "'" + LB_CODE.Text.Trim() + "'," +
                                    "'" + TXT_REASON.Text.Trim() + "'," +
                                    "2";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString() != "")
                {
                    LB_ERR.Text = conn.GetFieldValue("RESULT").ToString();
                    return;
                }

                conn.QueryString = "exec SP_M_AGENTS_APPROVAL " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'2'," +
                                    "'" + TXT_REASON.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Write("<script language='javascript'>parent.pageheader.location.href = 'Agent_Approval_Header.aspx';</script>");
                Response.Write("<script language='javascript'>parent.pagebody.location.href = '../Standard/default.html';</script>");
                Response.Write("<script language='javascript'>parent.pagebutton.location.href = '../Standard/default.html';</script>");
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
                return;
            }
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_M_AGENTS_APPROVAL_VALIDATION " +
                                    "'" + LB_CODE.Text.Trim() + "'," +
                                    "'" + TXT_REASON.Text.Trim() + "'," +
                                    "3";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString() != "")
                {
                    LB_ERR.Text = conn.GetFieldValue("RESULT").ToString();
                    return;
                }

                conn.QueryString = "exec SP_M_AGENTS_APPROVAL " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'3'," +
                                    "'" + TXT_REASON.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Write("<script language='javascript'>parent.pageheader.location.href = 'Agent_Approval_Header.aspx';</script>");
                Response.Write("<script language='javascript'>parent.pagebody.location.href = '../Standard/default.html';</script>");
                Response.Write("<script language='javascript'>parent.pagebutton.location.href = '../Standard/default.html';</script>");
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
                return;
            }
        }
    }
}
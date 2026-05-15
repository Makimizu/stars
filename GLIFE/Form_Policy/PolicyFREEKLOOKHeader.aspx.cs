using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyFREEKLOOKHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                LoadPolicy();
            }
        }

        protected void LoadPolicy()
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "a.TC_DESCR, " +
                                "BALANCE	= replace(convert(varchar(100), convert(money, b.BALANCE),1), '.00',''), " +
                                "STAT = (case when isnull(a.STAT,0) = 1 then 'ACTIVE' else 'NOT ACTIVE' end), " +
                                "REPORT_URL = d.URLAPP + '&POLICY_ID=' + convert(varchar(20), a.ID) " +
                                "from		V_POLICY a " +
                                "inner join	V_POLICY_SAVING_BALANCE b on a.ID = b.POLICY_ID " +
                                "inner join	POLICY_ENDORSEMENT_MASTER c on c.POLICY_ID = a.ID " +
                                "left join	V_LINK_SC_REPORT_LIST d on d.CODE = 24 " +
                                "where " +
                                "convert(varchar(10), c.POLICY_ID) + '-' + convert(varchar(10),c.SEQ) + '-' + c.ENDORSEMENT_TYPE collate database_default = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            LB_BALANCE.Text = conn.GetFieldValue("BALANCE").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_STAT.Text = conn.GetFieldValue("STAT").ToString();

            BT_APPROVAL.Attributes.Add("onclick", "if(!confirm('Are you sure to Approve FREELOOK ?')){return false;};");
            BT_REJECT.Attributes.Add("onclick", "if(!confirm('Are you sure to Reject FREELOOK ?')){return false;};");

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.freelookreport.location.href = '" + conn.GetFieldValue("REPORT_URL").ToString() + "';parent.freelookbody.location.href = '../Form_Tools/PaymentAcc.aspx?REGNO=" + LB_ID.Text + "&SEQ=1&CODE=PED';</script>");

        }

        protected void BT_APPROVAL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_POLICY_ENDORSEMENT_MASTER_VALIDATION " +
                                "'" + LB_ID.Text + "'," +
                                "2," +
                                "'" + TXT_REMARK.Text.Trim() + "'";
            conn.ExecuteQuery();

            LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();

            if (LB_ERROR.Text != "")
                return;

            try
            {
                conn.QueryString = "exec SP_POLICY_ENDORSEMENT_MASTER_APPROVAL " +
                                    "'" + LB_ID.Text + "'," +
                                    "2," +
                                    "'" + TXT_REMARK.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'PolicyFREELOOK.aspx';</script>");
            //Response.Redirect("PolicyFREELOOK.aspx");
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_POLICY_ENDORSEMENT_MASTER_VALIDATION " +
                                "'" + LB_ID.Text + "'," +
                                "3," +
                                "'" + TXT_REMARK.Text.Trim() + "'";
            conn.ExecuteQuery();

            LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();

            if (LB_ERROR.Text != "")
                return;


            try
            {
                conn.QueryString = "exec SP_POLICY_ENDORSEMENT_MASTER_APPROVAL " +
                                    "'" + LB_ID.Text + "'," +
                                    "3," +
                                    "'" + TXT_REMARK.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'PolicyFREELOOK.aspx';</script>");
            //Response.Redirect("PolicyFREELOOK.aspx");
        }
    }
}
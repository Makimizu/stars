using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;
namespace CUSTOMERS.Form_Client
{
    public partial class CompanySimilarity : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_CODE.Text = Request.QueryString["code"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            BT_APPROVAL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk APPROVE ?')){return false;};");
            BT_REJECT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk REJECT ?')){return false;};");
            BT_DELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_COMPANY_SIMILARITY '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
        }

        protected void BT_APPROVAL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_COMPANY_APPROVAL " +
                                "'" + LB_CODE.Text + "'," +
                                "'002'," +
                                "'" + TXT_REASON.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            conn.QueryString = "select STAT from COMPANY where COMPANY_CODE='" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("STAT").ToString() != "001")
            {
                Response.Write("<script language='javascript'>parent.location.href = 'CompanyApproval.aspx';</script>");
            }
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_COMPANY_APPROVAL " +
                                "'" + LB_CODE.Text + "'," +
                                "'003'," +
                                "'" + TXT_REASON.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("STAT").ToString() != "001")
            {
                Response.Redirect("CompanyApproval.aspx");
            }
        }

        protected void BT_DELETE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_COMPANY_ROLLBACK " +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            Response.Write("<script language='javascript'>parent.location.href = 'CompanyApproval.aspx';</script>");
        }
    }
}
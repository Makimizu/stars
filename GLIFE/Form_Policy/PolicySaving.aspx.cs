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
    public partial class PolicySaving : System.Web.UI.Page
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
                                "ENABLE_CANCEL = (case	when ENABLE_CANCEL = 1 and c.ROW_ID is not null then 0 " +
                                "                        else ENABLE_CANCEL end), " +
                                "STAT = (case when isnull(a.STAT,0) = 1 then 'ACTIVE' else 'NOT ACTIVE' end), " +
                                "REPORT_URL = d.URLAPP + '&POLICY_ID=' + convert(varchar(20), a.ID) " +
                                "from		V_POLICY a " +
                                "inner join	V_POLICY_SAVING_BALANCE b on a.ID = b.POLICY_ID " +
                                "left join	TRACK_DATA c on c.TIPE_CODE = 'PED' and c.SEQ = 1 and c.OWNER = convert(varchar(20), a.ID) " +
                                "left join	V_LINK_SC_REPORT_LIST d on d.CODE = 24 " +
                                "where " +
                                "a.ID = " + LB_ID.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            LB_BALANCE.Text = conn.GetFieldValue("BALANCE").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_STAT.Text = conn.GetFieldValue("STAT").ToString();

            if (conn.GetFieldValue("ENABLE_CANCEL").ToString() == "0")
            {
                BT_FREELOOK.Visible = true;
            }
            else
            {
                BT_FREELOOK.Attributes.Add("onclick", "if(!confirm('Are you sure to submit policy FREELOOK ?')){return false;};");
            }

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policysavingbody.location.href = '" + conn.GetFieldValue("REPORT_URL").ToString() + "';</script>");
        }

        protected void BT_FREELOOK_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_POLICY_ENDORSEMENT_INSERT " +
                                    LB_ID.Text + "," +
                                    "'SV_FRE'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();

                if (LB_ERROR.Text == "")
                {
                    BT_FREELOOK.Visible = false;
                    LB_ERROR.Text = "FREELOOK is REGISTERED";
                    LB_ERROR.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }
    }
}
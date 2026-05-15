using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimPending004 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_READONLY.Text = Request.QueryString["readonly"].ToString();

                Setup();

                if (LB_READONLY.Text == "1")
                {
                    TBL_REMARK.Visible = false;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PARAM_PENDING_TYPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select " +
                                "b.EMAIL, " +
                                "a.REMARK " +
                                "from       APPLICATION_MASTER_PENDING a " +
                                "inner join APPLICATION_ADDRESS b on a.REGNO = b.REGNO and ADDRESS_TYPE = 'COR' " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
                TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();
            }
        }

        protected void BT_EMAIL_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + LB_TYPE.Text + "'";
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            //try
            //{
            conn.QueryString = "update APPLICATION_MASTER_PENDING set " +
                                "REMARK = '" + TXT_REMARK.Text.Trim().Replace("'", "`") + "' " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text + " " +
                                "and PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteNonQuery();
            //}
            //catch { }

            Setup();
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_04b", LB_REGNO.Text, LB_SEQ.Text, LB_TYPE.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", URL);
        }
    }
}
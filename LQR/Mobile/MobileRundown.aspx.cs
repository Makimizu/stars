using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LQR.Mobile
{
    public partial class MobileRundown : System.Web.UI.Page
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
                    Response.Redirect("../SessionExpired.aspx");
                }

                LB_REGNO.Text = Session["s"].ToString();
                ShowRundDown();
            }
        }

        protected void ShowRundDown()
        {
            conn.QueryString = "exec SP_APPLICATION_QUESTION_GROUP '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                string URL = "ApplicationQuestion.aspx?" +
                                "REGNO=" + LB_REGNO.Text +
                                "&GROUP=" + conn.GetFieldValue(i, "CODE").ToString() +
                                "&MEMBERID=" + conn.GetFieldValue(i, "MEMBER_ID").ToString() +
                                "&URL=";
                LB_CONTENT.Text = LB_CONTENT.Text + "<iframe src=\"" + URL + "\" style=\"width: 100%;border:0;\" scrolling=\"no\" onload=\"resizeIframe(this)\"></iframe>";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LQR
{
    public partial class ApplicationQuestionRundown : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                LB_CONTENT.Text = LB_CONTENT.Text + "<iframe src=\"" + URL + "\" style=\"width: 98%;\" scrolling=\"no\" onload=\"resizeIframe(this)\"></iframe>";
            }

            conn.QueryString = "exec SP_REPORT_LIST '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_CONTENT.Text = LB_CONTENT.Text + "<iframe src=\"" + conn.GetFieldValue("URL").ToString() + "\" style=\"width: 98%;height:1000px;\" ></iframe>";
        }
    }
}
using DMS.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace REAS.Form_Reports
{
    public partial class GLIFERefundContribution : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string StartDateUpdate;
        protected string EndDateUpdate;
        protected string SelectedStatus;
        protected string ReasName;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            DGR.AllowPaging = true;
            try
            {
                string s = Session["s"].ToString();
            }
            catch
            {
                Response.Redirect("../Standard/FailedSession.aspx");
            }

            DGR.CurrentPageIndex = 0;
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_REAS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {

        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }
    }
}
using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CUSTOMERS.Form_Client
{
    public partial class CompanyGroup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FILL_DGR();
            }
        }


        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FILL_DGR();
        }

        protected void FILL_DGR()
        {
            
            conn.QueryString = "SELECT * FROM PR_COMPANY_GROUP";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " records";
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FILL_DGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                HID_ID.Value = e.Item.Cells[1].Text;
                TXT_NAME.Text = e.Item.Cells[2].Text;
            }
        }

        private void Save() {
            var sql = string.Format("exec SP_PR_COMPANY_GROUP_UPSERT '{0}', '{1}', '{2}'", (string.IsNullOrEmpty(HID_ID.Value.Trim()) ? "0" : HID_ID.Value.Trim()) , TXT_NAME.Text.Trim(),
                GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            conn.QueryString = sql;
            conn.ExecuteQuery();
        }

        protected void BTN_SAVE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TXT_NAME.Text.Trim()))
            {
                Response.Write("<script>alert('Data inserted successfully')</script>");
            }
            else {
                Save();
                Clear();
                FILL_DGR();
            }
        }

        private void Clear() {
            TXT_NAME.Text = string.Empty;
            HID_ID.Value = string.Empty;
        }

        protected void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}
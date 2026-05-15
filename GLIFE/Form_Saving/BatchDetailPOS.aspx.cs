using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Saving
{
    public partial class BatchDetailPOS : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "a.USERDATE, " +
                                "c.POLICY_NO, " +
                                "c.COMPANY_NAME, " +
                                "c.TC_DESCR " +
                                "from BATCH_MASTER a " +
                                "inner join APPLICATION_DATA_RAW b on a.ID = b.BATCH_ID " +
                                "inner join V_POLICY c on b.POLICY_ID = c.ID " +
                                "where " +
                                "a.ID = '" + LB_ID.Text + "' " +
                                "group by " +
                                "a.USERDATE, " +
                                "c.POLICY_NO, " +
                                "c.COMPANY_NAME, " +
                                "c.TC_DESCR";
            conn.ExecuteQuery();

            LB_BATCHTIME.Text = conn.GetFieldValue("USERDATE").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_BATCH_DETAIL_SAVING '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_COUNT.Text = conn.GetRowCount().ToString();

            if (conn.GetRowCount() > 0)
            {
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();
            }
        }
    }
}
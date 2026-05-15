using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Temp
{
    public partial class Admedika_Benefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_TPA_PLAN_IMPORT_Click(object sender, EventArgs e)
        {

            conn.QueryString = "exec SP_MIGRASI_ADMEDIKA_BENEFIT";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ToCSV(dt, this, "ADMEDIKA_TAKAFUL_Benefit.txt", false, "\"", ",");
        }
    }
}
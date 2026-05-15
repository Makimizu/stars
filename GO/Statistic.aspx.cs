using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GO
{
    public partial class Statistic : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGRDBSize();
                FillDGRJob();
            }
        }

        protected void FillDGRDBSize()
        {
            conn.QueryString = "select " +
                                "DBNAME, " +
                                "DB_SIZE = replace(convert(varchar(100),convert(money,DB_SIZE),1),'.00',''), " +
                                "LOG_SIZE = replace(convert(varchar(100),convert(money,LOG_SIZE),1),'.00',''), " +
                                "DEVICE_NAME, " +
                                "LOG_NAME " +
                                "from V_DB_FILE_SIZE a " +
                                "order by a.LOG_SIZE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DBSIZE.DataSource = dt;
            DGR_DBSIZE.DataBind();
        }

        protected void FillDGRJob()
        {
            conn.QueryString = "select " +
                                "[JOB NAME], " +
                                "[DESCRIPTION], " +
                                "[LAST RUN TIME] = convert(varchar(50),[START TIME]), " +
                                "[MESSAGE] " +
                                "from V_SYSJOBS " +
                                "order by " +
                                "[START TIME] desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_JOB.DataSource = dt;
            DGR_JOB.DataBind();
        }
    }
}
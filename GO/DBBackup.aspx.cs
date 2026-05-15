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
    public partial class DBBackup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BT_START.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO BACKUP ?')){return false;};");
                BT_START_RESTORE.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO RESTORE ?')){return false;};");
            }
        }

        protected void BT_START_Click(object sender, EventArgs e)
        {
            LB_STATUS.Text = "";
            LB_STATUS_RESTORE.Text = "";

            try
            {
                conn.QueryString = "exec SP_JOB_BACKUP_DB '" + TXT_FOLDER.Text.Trim() + "'";
                conn.ExecuteQuery(50000);

                LB_STATUS.ForeColor = System.Drawing.Color.Blue;
                LB_STATUS.Text = "BACKUP IS SUCCED";
            }
            catch (System.Exception ex)
            {
                LB_STATUS.ForeColor = System.Drawing.Color.Red;
                LB_STATUS.Text = ex.Message;
            }
        }

        protected void BT_START_RESORE_Click(object sender, EventArgs e)
        {
            LB_STATUS.Text = "";
            LB_STATUS_RESTORE.Text = "";

            try
            {
                conn.QueryString = "exec SP_JOB_RESTORE_DB '" + TXT_SOURCE_FOLDER.Text.Trim() + "'";
                conn.ExecuteQuery(50000);

                LB_STATUS_RESTORE.ForeColor = System.Drawing.Color.Blue;
                LB_STATUS_RESTORE.Text = "RESTORE IS SUCCED";
            }
            catch (System.Exception ex)
            {
                LB_STATUS_RESTORE.ForeColor = System.Drawing.Color.Red;
                LB_STATUS_RESTORE.Text = ex.Message;
            }
        }
    }
}
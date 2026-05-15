using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class PARAMETER_MASTER_ENTRY : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                Setup();
            }

        }

        protected void Setup()
        {
           
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_UPSERT " +
                             "@ID = null, " +
                            "@TYPE = '" + LB_MODE.Text.Trim() + "', " +
                            "@DESCRIPTION = '" + TXT_DESCRIPTION.Text.Trim() + "'," +
                            "@STARTDATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "'," +
                            "@ENDDATE  = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "'," +
                            "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

            conn.ExecuteQuery();
        }

      
    }
}
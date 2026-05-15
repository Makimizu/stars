using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Finance
{
    public partial class MemoFrame : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string sysCode = string.Empty;
            string sysVal = string.Empty;
            if (!IsPostBack)
            {
                sysCode = !string.IsNullOrEmpty(Session["SysCode"] as string) ? Session["SysCode"].ToString() : ""; ;
                Session.Remove("SysCode");

                if (!string.IsNullOrEmpty(sysCode)) {
                    /*conn.QueryString = $@"SELECT SystemValue FROM MasterSystemConfig 
                                    WHERE SystemCategory = 'AGR'
                                    AND SystemSubCategory = 'MAILLINK'
                                    AND SystemCode = '{sysCode}'";*/
                    //fixing interpolated string in visual studio 2012
                    conn.QueryString = string.Format(@"
                                                    SELECT SystemValue 
                                                    FROM MasterSystemConfig 
                                                    WHERE SystemCategory = 'AGR' 
                                                    AND SystemSubCategory = 'MAILLINK' 
                                                    AND SystemCode = '{0}'", sysCode);

                    conn.ExecuteQuery();
                    if (conn.GetRowCount() > 0)
                    {
                        sysVal = conn.GetFieldValue("SystemValue").ToString();
                    }
                }

                FrameSrc.Text = sysVal;
            }
        }
    }
}
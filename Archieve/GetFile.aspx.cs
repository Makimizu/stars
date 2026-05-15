using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace Archieve
{
    public partial class GetFile : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    conn.QueryString = "select NAMAFILE from " + Request.QueryString["APPID"] + "_ARSIP where CODE='" + Request.QueryString["CODE"] + "'";
                    conn.ExecuteQuery();

                    GlobalUse.SQLToFile(conn.GetFieldValue("NAMAFILE").ToString(),
                                        "select THEFILE from " + Request.QueryString["APPID"] + "_ARSIP where CODE='" + Request.QueryString["CODE"] + "'",
                                        this);
                }
                catch { }
            }
        }
    }
}
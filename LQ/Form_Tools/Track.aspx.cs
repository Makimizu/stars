using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
namespace LQ.Form_Tools
{
    public partial class Track : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR(Request.QueryString["REGNO"].ToString(), Request.QueryString["TRACK_TYPE"].ToString(), Request.QueryString["PARAM_VALUE"].ToString());
            }
        }

        protected void FillDGR(string REGNO, string TRACK_TYPE, string PARAM_VALUE)
        {
            conn.QueryString = "exec SP_APPLICATION_TRACK '" + REGNO + "','" + TRACK_TYPE + "','" + PARAM_VALUE + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

    }
}
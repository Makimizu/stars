using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_App
{
    public partial class ApplicationRemark : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                LoadRemark();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            if (int.Parse(conn.GetFieldValue("TRACK").ToString()) < 3)
                result = false;

            return result;
        }

        protected void LoadRemark()
        {
            conn.QueryString = "select " +
                                "a.SEQ, " +
                                "REMARK = '<B>' + UPPER(a.CREATEBY) + ' - [' + convert(varchar(50),a.CREATEDATE) + ']</B><BR><BR>' + a.REMARK " +
                                "from APPLICATION_REMARK a " +
                                "where a.REGNO = '" + LB_REGNO.Text + "' order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();
        }

    }
}
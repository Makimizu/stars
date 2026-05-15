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
    public partial class AGENT_CASES : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();

                conn.QueryString = "select " +
                                    "a.FULLNAME, " +
                                    "a.CD_DESCR, " +
                                    "a.SUBCD_DESCR " +
                                    "from	V_M_AGENTS a " +
                                    "where " +
                                    "a.CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();
                LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
                LB_CHANNEL.Text = conn.GetFieldValue("CD_DESCR").ToString();
                LB_LEVEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_M_AGENT_CASES '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}
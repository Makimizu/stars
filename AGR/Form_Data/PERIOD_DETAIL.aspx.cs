using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Data
{
    public partial class PERIOD_DETAIL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string cd, remuntype, startdate, enddate;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CHANNEL.Text = Request.QueryString["CD"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                conn.QueryString = "select " +
                                    "DESCR, " +
                                    "START_DATE = convert(varchar(20), convert(date, '" + Request.QueryString["START_DATE"].ToString() + "'), 106), " +
                                    "END_DATE = convert(varchar(20), convert(date, '" + Request.QueryString["END_DATE"].ToString() + "'), 106) " +
                                    "from PR_CHANNEL_DISTRIBUTION  " +
                                    "where " +
                                    "CODE = '" + LB_CHANNEL.Text + "'";
                conn.ExecuteQuery();

                LB_CHANNEL_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
                LB_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
                LB_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PERIOD_DETAIL '" + LB_CHANNEL.Text + "','" + LB_STARTDATE.Text + "','" + LB_ENDDATE.Text + "','" + LB_TYPE.Text + "','" + TXT_AGENT.Text.Trim() + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();
        }


        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void TXT_AGENT_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
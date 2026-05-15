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
    public partial class DATA_COMMISSION_DETAIL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select " +
                                "THISYEAR = YEAR(GETDATE()) - SEQ + 1 " +
                                "from SC_SEQ  " +
                                "where " +
                                "SEQ < 3 " +
                                "order by " +
                                "1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                //"ID = '<a href=\"../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=2&CD=" + DDL_CHANNEL.SelectedValue + "&PERIOD=' + convert(varchar(20), a.START_DATE, 112) + convert(varchar(20), a.END_DATE, 112) + '\" target=\"CommDetailBody\">' + convert(varchar(20), a.START_DATE, 106) + ' - ' + convert(varchar(20), a.END_DATE, 106)  + '</a>' " +
                                "ID = '<a href=\"PERIOD_DETAIL.aspx?CD=" + DDL_CHANNEL.SelectedValue + "&START_DATE=' + convert(varchar(20), a.START_DATE, 112) + '&END_DATE=' + convert(varchar(20), a.END_DATE, 112) + '&TYPE=1\" target=\"CommDetailBody\">' + convert(varchar(20), a.START_DATE, 106) + ' - ' + convert(varchar(20), a.END_DATE, 106)  + '</a>' " +
                                "from V_PERIOD_MASTER a " +
                                "inner join	(select START_DATE from PERIOD_DETAIL where REMUN_TYPE = '1' group by START_DATE) b on a.START_DATE = b.START_DATE " +
                                "where " +
                                "YEAR(a.START_DATE) = " + DDL_YEAR.SelectedValue + " " +
                                "and a.REMUN_TYPE = '1' " +
                                "and a.CD = '" + DDL_CHANNEL.SelectedValue + "' " +
                                "order by " +
                                "a.START_DATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
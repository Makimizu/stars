using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Globalization;

namespace AGR.Form_Data
{
    public partial class DATA_COMMISSION_PERPERIOD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();
                LB_CD.Text = Request.QueryString["CD"].ToString();
                LB_STARTDATE.Text = Request.QueryString["START_DATE"].ToString();
                LB_ENDDATE.Text = Request.QueryString["END_DATE"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_REMUN_TYPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select " +
                                "DESCR, " +
                                "START_DATE = convert(varchar(20), convert(date,'" + LB_STARTDATE.Text + "'), 106), " +
                                "END_DATE = convert(varchar(20), convert(date,'" + LB_ENDDATE.Text + "'), 106) " +
                                "from PR_MARKET_SEGMENT where CODE = '" + LB_CD.Text + "'";
            conn.ExecuteQuery();
            LB_MARKET_SEGMENT.Text = conn.GetFieldValue("DESCR").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            LB_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();

            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";

            conn.QueryString = "exec SP_PROCESS_REMUN_RESULT_PERPERIOD " +
                                "'" + LB_CD.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + LB_STARTDATE.Text + "'," +
                                "'" + LB_ENDDATE.Text + "'," +
                                "'" + TXT_FULLNAME.Text.Trim() + "'," +
                                "'" + TXT_AGENCY.Text.Trim() + "'," +
                                "'" + TXT_LEVEL.Text.Trim() + "'";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";
            var startDate = GenerateDate(Request.QueryString["START_DATE"].ToString());
            var endDate = GenerateDate(Request.QueryString["END_DATE"].ToString());
            string baseUrl = ExportLink.NavigateUrl.ToString();
            //string url = $"&CD={LB_CD.Text}&STARTDATE={startDate}&ENDDATE={endDate}&REMUN_TYPE={LB_TYPE.Text}";
            //fixing interpolated string in visual studio 2012
            string url = string.Format(
                                "&CD={0}&STARTDATE={1}&ENDDATE={2}&REMUN_TYPE={3}",
                                LB_CD.Text,
                                startDate,
                                endDate,
                                LB_TYPE.Text
                            );

            baseUrl = baseUrl + url;
            ExportLink.NavigateUrl = baseUrl;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();
        }

        protected string GenerateDate(string value)
        {
            string y = value.Substring(0, 4);
            string m = value.Substring(4, 2);
            string d = value.Substring(6, 2);
            string res = y + "-" + m + "-" + d;
            return res;
        }

        protected void DGR_PERIOD_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void TXT_FULLNAME_TextChanged(object sender, EventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_LEVEL_TextChanged(object sender, EventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_AGENCY_TextChanged(object sender, EventArgs e)
        {
            DGR_PERIOD.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
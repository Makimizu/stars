using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Web.Services.Description;
using System.Runtime.InteropServices.ComTypes;

namespace AGR.Form_Data
{
    public partial class DATA_COMMISSION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_REMUN_TYPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2";
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
            string RemunType = LB_TYPE.Text;
            if (RemunType.ToUpper() != "ALL")
            {
                TBL_EXPORTALL.Visible = false;
                DGR_PERIOD_ALL.Visible = false;
                DGR_PERIOD.Visible = true;
                conn.QueryString = "exec SP_PROCESS_REMUN_RESULT " +
                                DDL_YEAR.SelectedValue + "," +
                                "'" + DDL_CHANNEL.SelectedValue + "'," +
                                "'" + LB_TYPE.Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_PERIOD.DataSource = dt;
                DGR_PERIOD.DataBind();
            }
            else {
                //System.Threading.Thread.Sleep(3000);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowProgress();", true);
                DGR_PERIOD.Visible = false;
                DGR_PERIOD_ALL.Visible = true;
                TBL_EXPORTALL.Visible = true;

                string baseUrl = "../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=50";
                //string url = $"&YEAR={DDL_YEAR.SelectedValue}&MARKET_SEGMENT={DDL_CHANNEL.SelectedValue}";
                //fixing interpolated string in visual studio 2012
                string url = string.Format("&YEAR={0}&MARKET_SEGMENT={1}", DDL_YEAR.SelectedValue, DDL_CHANNEL.SelectedValue);
                baseUrl = baseUrl + url;
                ExportLink.NavigateUrl = baseUrl;

                conn.QueryString = "exec SP_PROCESS_REMUN_RESULT_ALL " +
                                DDL_YEAR.SelectedValue + "," +
                                "'" + DDL_CHANNEL.SelectedValue + "'";
                conn.ExecuteQuery(100000);
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_PERIOD_ALL.DataSource = dt;
                DGR_PERIOD_ALL.DataBind();
            }
            
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Summary")
            {
                if (e.Item.Cells[0].Text.Replace("&nbsp;", "") != "")
                    Response.Redirect(e.Item.Cells[0].Text.Replace("&nbsp;", ""));
            }

            if (e.CommandName == "Detail")
            {
                if (e.Item.Cells[1].Text.Replace("&nbsp;", "") != "")
                    Response.Redirect(e.Item.Cells[1].Text.Replace("&nbsp;", ""));
            }
        }
    }
}
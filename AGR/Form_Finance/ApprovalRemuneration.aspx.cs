using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
using AGR.Apps.Core.UnitOfWorks;
using System.Net.Http.Headers;
using System.Net.Http;
using AGR.Apps.Core.Interfaces;

namespace AGR.Form_Finance
{
    public partial class ApprovalRemuneration : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        
        static string sharedSecret = System.Configuration.ConfigurationManager.AppSettings["enckey"];
        static string connString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
        private readonly IUnitOfWork unitOfWork = new UnitOfWork(connString, sharedSecret);
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MARKET_SEGMENT.Text = Request.QueryString["tipe"].ToString();
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR(DDL_STAT.SelectedValue, DDL_YEAR.SelectedValue);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_MARKET_SEGMENT	where CODE = '" + LB_MARKET_SEGMENT.Text.Trim() + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select THEYEAR = YEAR(GETDATE()) - SEQ + 1 from SC_SEQ where SEQ <= 5";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR(string STAT, string YEAR)
        {
            string where = "and DONE = " + STAT + " ";
            if (STAT == "1")
                where = where + " and YEAR(a.START_DATE) = " + YEAR + " ";

            string MarketSegment = LB_MARKET_SEGMENT.Text;

            /*string strQuery = $@"SELECT MARKET_SEGMENT = a.MARKET_SEGMENT, MIN_AMOUNT = a.MIN_AMOUNT, 
                                START_DATE = a.START_DATE, 
                                END_DATE = a.END_DATE, 
                                REMUN_PERIOD = UPPER(convert(VARCHAR(15), a.START_DATE, 106)) + ' - ' + UPPER(convert(VARCHAR(15), a.END_DATE, 106)), 
                                TOTAL_AMOUNT = REPLACE(convert(VARCHAR(100), convert(money, a.TOTAL_AMOUNT), 1), '.00', ''), 
                                APPROVE1 = (CASE WHEN a.APPROVEBY1 is null THEN '' ELSE a.APPROVEBY1 + '<BR>' + convert(VARCHAR(150), a.APPROVEDATE1) END), 
                                APPROVE2 = (CASE WHEN a.APPROVEBY2 is null THEN '' ELSE a.APPROVEBY2 + '<BR>' + convert(VARCHAR(150), a.APPROVEDATE2) END), 
                                APPROVE3 = (CASE WHEN a.APPROVEBY3 is null THEN '' ELSE a.APPROVEBY3 + '<BR>' + convert(VARCHAR(150), a.APPROVEDATE3) END),
                                APPROVE4 = (CASE WHEN a.APPROVEBY4 is null THEN '' ELSE a.APPROVEBY4 + '<BR>' + convert(VARCHAR(150), a.APPROVEDATE4) END),
                                APPROVE5 = (CASE WHEN a.APPROVEBY5 is null THEN '' ELSE a.APPROVEBY5 + '<BR>' + convert(VARCHAR(150), a.APPROVEDATE5) END)
                                FROM   V_REMUN_APPROVAL_MASTER a 
                                WHERE a.MARKET_SEGMENT = '{MarketSegment}'
                                {where}
                                ORDER BY a.END_DATE";*/
            //fixing interpolated string
            string strQuery = string.Format(@"
                                        SELECT 
                                            MARKET_SEGMENT = a.MARKET_SEGMENT, 
                                            MIN_AMOUNT = a.MIN_AMOUNT, 
                                            START_DATE = a.START_DATE, 
                                            END_DATE = a.END_DATE, 
                                            REMUN_PERIOD = UPPER(CONVERT(VARCHAR(15), a.START_DATE, 106)) + ' - ' + UPPER(CONVERT(VARCHAR(15), a.END_DATE, 106)), 
                                            TOTAL_AMOUNT = REPLACE(CONVERT(VARCHAR(100), CONVERT(MONEY, a.TOTAL_AMOUNT), 1), '.00', ''), 
                                            APPROVE1 = (CASE WHEN a.APPROVEBY1 IS NULL THEN '' ELSE a.APPROVEBY1 + '<BR>' + CONVERT(VARCHAR(150), a.APPROVEDATE1) END), 
                                            APPROVE2 = (CASE WHEN a.APPROVEBY2 IS NULL THEN '' ELSE a.APPROVEBY2 + '<BR>' + CONVERT(VARCHAR(150), a.APPROVEDATE2) END), 
                                            APPROVE3 = (CASE WHEN a.APPROVEBY3 IS NULL THEN '' ELSE a.APPROVEBY3 + '<BR>' + CONVERT(VARCHAR(150), a.APPROVEDATE3) END),
                                            APPROVE4 = (CASE WHEN a.APPROVEBY4 IS NULL THEN '' ELSE a.APPROVEBY4 + '<BR>' + CONVERT(VARCHAR(150), a.APPROVEDATE4) END),
                                            APPROVE5 = (CASE WHEN a.APPROVEBY5 IS NULL THEN '' ELSE a.APPROVEBY5 + '<BR>' + CONVERT(VARCHAR(150), a.APPROVEDATE5) END)
                                        FROM V_REMUN_APPROVAL_MASTER a 
                                        WHERE a.MARKET_SEGMENT = '{0}'
                                        {1}
                                        ORDER BY a.END_DATE", MarketSegment, where);


            conn.QueryString = strQuery;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                bt.Attributes.Add("onclick", "{ShowProgress();");
            }

            //LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";
            /*
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbDOCNO = (LinkButton)DGR.Items[i].FindControl("LB_DOCNO");

                lbDOCNO.Text = DGR.Items[i].Cells[2].Text;
                lbDOCNO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[4].Text == "0")
                    cb.Visible = false;
            }
            */
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                LB_APPROVAL_TITLE.Text = e.Item.Cells[4].Text;
                FillDGRApproval(e.Item.Cells[0].Text, e.Item.Cells[1].Text, e.Item.Cells[2].Text, e.Item.Cells[3].Text);
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }

            if (e.CommandName == "Detail")
            {
                //string srtUrl = $"../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=39&MARKET_SEGMENT={LB_MARKET_SEGMENT.Text.Trim()}&START_DATE={e.Item.Cells[2].Text}&END_DATE={e.Item.Cells[3].Text}";
                //fixing interpolated string in visual studio 2012
                string srtUrl = string.Format(
                                        "../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=39&MARKET_SEGMENT={0}&START_DATE={1}&END_DATE={2}",
                                        LB_MARKET_SEGMENT.Text.Trim(),
                                        e.Item.Cells[2].Text,
                                        e.Item.Cells[3].Text
                                    );

                Response.Redirect(srtUrl);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR(DDL_STAT.SelectedValue, DDL_YEAR.SelectedValue);
        }

        protected void FillDGRApproval(string marketSegment, string minAmount, string startDate, string endDate)
        {
            conn.QueryString = "exec SP_REMUN_APPROVAL_MASTER_BUTTON " +
                                "'" + marketSegment + "', " +
                                "'" + minAmount + "', " +
                                "'" + startDate + "', " +
                                "'" + endDate + "', " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_APPROVAL.DataSource = dt;
            DGR_APPROVAL.DataBind();

            for (int i = 0; i < DGR_APPROVAL.Items.Count; i++)
            {
                Button bt = (Button)DGR_APPROVAL.Items[i].FindControl("BT_APPROVE");
                //bt.Attributes.Add("onclick", "if(!confirm('Are you sure you want to APPROVE ?')){return false;};");
                bt.Attributes.Add("onclick", "if(!confirm('Are you sure you want to APPROVE ?')){return false;}else{ShowProgress();}");

                if (DGR_APPROVAL.Items[i].Cells[5].Text == "0")
                    bt.Visible = false;
            }
        }

        protected void DGR_APPROVAL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                try
                {
                    System.Threading.Thread.Sleep(3000);
                    conn.QueryString = "exec SP_REMUN_APPROVAL_MASTER_UPDATE " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[2].Text + "'," +
                                    "'" + e.Item.Cells[3].Text + "'," +
                                    "'" + e.Item.Cells[4].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(2000);
                    ProcessMail();
                }
                catch (Exception ex)
                {
                    MessageBox(this, ex.Message);
                }
                
                //conn.ExecuteNonQuery();
                FillDGR(DDL_STAT.SelectedValue, DDL_YEAR.SelectedValue);
            }
        }

        public void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }

        protected void DDL_STAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_STAT.SelectedValue == "1")
                TR_YEAR.Visible = true;
            else
                TR_YEAR.Visible = false;

            FillDGR(DDL_STAT.SelectedValue, DDL_YEAR.SelectedValue);
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR(DDL_STAT.SelectedValue, DDL_YEAR.SelectedValue);
        }

        private void ProcessMail()
        {
            using (var client = new HttpClient())
            {
                var urlService = unitOfWork.SystemConfigurationService.GetSysconfigValue("EMAIL", "JOBSMAIL", "URLMAIL");
                var request = new HttpRequestMessage(HttpMethod.Get, urlService);
                string token = Session["validtoken"].ToString();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }
        }
    }
}
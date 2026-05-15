using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using AGR.Apps.Core.UnitOfWorks;
using System.Net.Http.Headers;
using System.Net.Http;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Services;

namespace AGR.Form_Data
{
    public partial class Process : System.Web.UI.Page
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
                Setup();
                //ProcessMail(); test hit email
            }
        }

        protected void FillDGR_CD()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CD.DataSource = dt;
            DGR_CD.DataBind();

            for (int i = 0; i < DGR_CD.Items.Count; i++)
            {
                Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                bt.Text = DGR_CD.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGR_APPROVAL()
        {
            DGR_TO_APPROVAL.Visible = false;

            conn.QueryString = "exec SP_REMUN_PROCESS_TO_APPROVAL '" + LB_CD.Text + "'," + DDL_YEAR.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TO_APPROVAL.DataSource = dt;
            DGR_TO_APPROVAL.DataBind();

            for (int i = 0; i < DGR_TO_APPROVAL.Items.Count; i++)
            {
                Button bt = (Button)DGR_TO_APPROVAL.Items[i].FindControl("BT_TO_APPROVAL");
                bt.Text = DGR_TO_APPROVAL.Items[i].Cells[1].Text;

                bt.Attributes.Add("onclick", "if(!confirm('Are you sure the " + LB_TITLE.Text + " period remuneration has COMPLETED ?')){return false;}else{ShowProgress();}");
            }

            if (DGR_TO_APPROVAL.Items.Count > 0)
            {
                DGR_TO_APPROVAL.Visible = true;
            }
        }

        protected void Setup()
        {
            FillDGR_CD();


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

            conn.QueryString = "select SEQ, MON = UPPER(DateName( month , DateAdd( month , SEQ , -1 ))), MTD = MONTH(GETDATE()) from SC_SEQ where SEQ <= 12 order by SEQ";
            conn.ExecuteQuery();
            DDL_MONTH.Items.Add(new ListItem("", "null"));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            LB_TITLE.Text = DGR_CD.Items[0].Cells[1].Text;
            LB_CD.Text = DGR_CD.Items[0].Cells[0].Text;
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PROCESS_REMUN_SCHEDULE " + DDL_YEAR.SelectedValue + "," + DDL_MONTH.SelectedValue + ",'" + LB_CD.Text + "'";
            conn.ExecuteQuery(500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();

            for (int i = 0; i < DGR_PERIOD.Items.Count; i++)
            {
                Button btPROCESS = (Button)DGR_PERIOD.Items[i].FindControl("BT_PROCESS");
                Button btUNPROCESS = (Button)DGR_PERIOD.Items[i].FindControl("BT_DEL");
                Button btFINANCE = (Button)DGR_PERIOD.Items[i].FindControl("BT_FINANCE");

                if (DGR_PERIOD.Items[i].Cells[3].Text == "0")
                {
                    btPROCESS.BackColor = System.Drawing.Color.Gray;
                    btPROCESS.Enabled = false;
                }
                if (DGR_PERIOD.Items[i].Cells[4].Text == "0")
                {
                    btUNPROCESS.BackColor = System.Drawing.Color.Gray;
                    btUNPROCESS.Enabled = false;
                }
                if (DGR_PERIOD.Items[i].Cells[5].Text == "0")
                {
                    btFINANCE.BackColor = System.Drawing.Color.Gray;
                    btFINANCE.Enabled = false;
                }

                btPROCESS.Attributes.Add("onclick", "if(!confirm('Are you sure to PROCESS ?')){return false;}else{ShowProgress();}");
                btUNPROCESS.Attributes.Add("onclick", "if(!confirm('Are you sure to UNPROCESS ?')){return false;}else{ShowProgress();}");
                btFINANCE.Attributes.Add("onclick", "if(!confirm('Are you sure to SEND TO FINANCE ?')){return false;}else{ShowProgress();}");
            }

            FillDGR_APPROVAL();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                System.Threading.Thread.Sleep(3000);

                try
                {
                    conn.QueryString = "exec SP_REMUN_PROCESS " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[6].Text + "'," +
                                    "'" + e.Item.Cells[7].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(500000);
                }
                catch (Exception ex)
                {
                    //Please process basic commision first
                    string message = ex.Message.Contains("Please process basic commision first") ? "Silahkan proses BASIC COMMISSION & OVERRIDING terlebih dahulu" : ex.Message;
                    //Response.Write($"<script>alert('{message}');</script>");
                    MessageBox(this, message);
                }
                
                FillDGR();
            }

            if (e.CommandName == "Unprocess")
            {
                System.Threading.Thread.Sleep(3000);
                conn.QueryString = "exec SP_REMUN_UNPROCESS " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[6].Text + "'," +
                                    "'" + e.Item.Cells[7].Text + "'";
                conn.ExecuteQuery(500000);
                FillDGR();
            }

            if (e.CommandName == "Finance")
            {
                System.Threading.Thread.Sleep(3000);
                conn.QueryString = "exec SP_LINK_FINANCE_SETTLEMENT_DETAIL_INSERT " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[6].Text + "'," +
                                    "'" + e.Item.Cells[7].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(500000);
                FillDGR();
            }
        }

        public void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }

        protected void DGR_CD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Button bt = (Button)e.Item.FindControl("BT_CD");
                LB_TITLE.Text = bt.Text;
                LB_CD.Text = e.Item.Cells[0].Text;
                FillDGR();
            }
        }

        protected void DGR_TO_APPROVAL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    conn.QueryString = e.Item.Cells[0].Text;
                    conn.ExecuteQuery(2000);
                    ProcessMail();
                    //conn.ExecuteNonQuery();
                    FillDGR_APPROVAL();
                }
                catch (Exception ex) {
                    MessageBox(this, ex.Message);
                    FillDGR_APPROVAL();
                }
            }
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
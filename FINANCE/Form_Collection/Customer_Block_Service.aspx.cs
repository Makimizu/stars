using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Customer_Block_Service : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected Connection connsec = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_MODE.Text = Request.QueryString["MODE"];
                LB_PRIVILEDGE.Text = Request.QueryString["FC"];
                Setup();
                ShowResults();                
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from INVOICE_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                        
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";
            string where = "";

            
            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            
            conn.QueryString = "select " +
                                "a.CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "PIC_NAMA, " +
                                "PIC_EMAIL, " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "INVOICE, " +
                                "MAX_AGING, " +
                                "START_AGING = convert(varchar(20),START_AGING,106) " +
                                "from V_INVOICE_CUSTOMER_OVERDUE a " +
                                "left join INVOICE_BLOCKING_SERVICE b on a.APP_ID=b.APP_ID and a.CUSTOMER_CODE=b.CUSTOMER_CODE and isnull(b.UNBLOCK_BY,'') = '' " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' and b.CUSTOMER_CODE is null " + where +
                                "order by  " +
                                "a.MAX_AGING desc";
            try
            {
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            if (LB_PRIVILEDGE.Text != "1")
                DGR.Columns[8].Visible = false;
            else
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    LinkButton lbNO = (LinkButton)DGR.Items[i].FindControl("LB_NO");
                    TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                    lbNO.Text = DGR.Items[i].Cells[1].Text;
                    lbNO.Attributes.Add("onclick", "window.open('Customer_Block_Service_Detail.aspx?APPID=" +DDL_APP.SelectedValue+ "&NO=" +DGR.Items[i].Cells[1].Text+ "','DETAIL','height=300px,width=600px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                    txtEMAIL.Text = DGR.Items[i].Cells[7].Text.Trim().Replace("&nbsp;", "");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";
            if (e.CommandName == "Send")
            {
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

                if (txtEMAIL.Text.Trim() == "")
                    return;

                try
                {
                    //SendEmail(e);
                    
                    conn.QueryString = "exec SP_INVOICE_BLOCKING_SERVICE_UPSERT " +
                                        "'" + DDL_APP.SelectedValue + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "1," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "<BR>" + ex.Message;
                    return;
                }

                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }
        }

        protected void SendEmail(DataGridCommandEventArgs e)
        {
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

            if (txtEMAIL.Text.Trim() == "")
                return;

            connsec.QueryString = "exec SP_PARAM_EMAIL " +
                                "'" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," +
                                e.Item.Cells[13].Text + "," +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "'" + e.Item.Cells[3].Text + "'";
            connsec.ExecuteQuery();

            string sender = connsec.GetFieldValue("DEFAULT_SENDER").ToString();
            string CC = connsec.GetFieldValue("CC").ToString();
            string BCC = connsec.GetFieldValue("BCC").ToString();
            string body = connsec.GetFieldValue("BODY").ToString();
            string subject = connsec.GetFieldValue("SUBJECT").ToString();


            string path = Request.PhysicalApplicationPath + "Upload";
            string[] attachment = new string[1];
            attachment[0] = "";
            string sendresult = GlobalUse.SendEmail(sender, txtEMAIL.Text.Trim(), CC, BCC, subject, body, attachment);
            
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            ShowResults();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowResults();
        }

        protected void ShowResults()
        {
            switch (LB_MODE.Text)
            {
                case "BLOCK": TR_BLOCK.Visible = true;
                    TR_UNBLOCK.Visible = false;
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                    break;
                case "UNBLOCK": TR_BLOCK.Visible = false;
                    TR_UNBLOCK.Visible = true;
                    break;
            }
        }
    }
}
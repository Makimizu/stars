using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Invoice_History : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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

                FillDGR(Request.QueryString["INVOICENO"]);
            }
        }

        protected void FillDGR(string invoiceno)
        {
            conn.QueryString = "exec SP_INVOICE_MASTER_HISTORY '" + invoiceno + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
                //if (DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "") == "")
                //{
                //    btDEL.Visible = false;
                //}
                if (DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "PREMIUM CREDIT LIFE - BATCH" || DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "PREMIUM CREDIT LIFE" || DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "PREMIUM PERTAMA TOPUP STANDARD" || DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "PREMI TOP UP IRREGULER" || DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "PREMI TOP UP REGULER")
                    {
                        btDEL.Visible = false;
                    }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = e.Item.Cells[7].Text.Replace("&nbsp;", "");
                    conn.ExecuteNonQuery();
                    FillDGR(Request.QueryString["INVOICENO"]);
                }
                catch { }
            }
        }
    }
}
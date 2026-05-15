using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class SuspendTRXID : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TRXID.Text = Request.QueryString["TRXID"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            string descr = "(case when a.REGNO is null then DESCR " +
                                "                        else DESCR +  " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + POLICY_NO + ' - ' + VACC + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + FULLNAME + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + PRODUCT_NAME + '</span>' " +
                                "                        end)";

            conn.QueryString = "select " +
                                "ACCNO			= BANK, " +
                                "TRXID			= TRXID, " +
                                "POST_DATE		= convert(varchar(20), POST_DATE, 106), " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, BALANCE), 1), '.00', ''), " +
                                "DESCR			= " + descr + ", " +
                                "REGNO			= REGNO " +
                                "from			V_LINK_FINANCE_REKENING_JURNAL_SUSPEND a " +
                                "where " +
                                "TRXID          = '" + LB_TRXID.Text + "'";
            conn.ExecuteQuery(150000);

            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_BALANCE.Text = conn.GetFieldValue("BALANCE").ToString();
            LB_POSTDATE.Text = conn.GetFieldValue("POST_DATE").ToString();
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGRSearch();
        }

        protected void FillDGRSearch()
        {
            conn.QueryString = "exec SP_APPLICATION_PREMIUM_PERFORMANCE_SEARCH '" + TXT_SEARCH.Text.Trim() + "',''";
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SEARCH.DataSource = dt;
            DGR_SEARCH.DataBind();

            for (int i = 0; i < DGR_SEARCH.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_SEARCH.Items[i].FindControl("LB_SELECT");
                lb.Text = DGR_SEARCH.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationOutstanding.aspx?TRXID=" + LB_TRXID.Text + "&REGNO=" + e.Item.Cells[0].Text);
            }
        }
    }
}
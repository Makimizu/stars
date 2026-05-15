using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Policy_Expend_History : System.Web.UI.Page
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

                FillDGR(Request.QueryString["ID"]);
            }
        }

        protected void FillDGR(string policyperiodid)
        {
            conn.QueryString = "select (ROW_NUMBER() over (order by a.USERDATE)) NBR,  a.ID, a.EXPENDNO, replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00','') AMOUNT, " +
	                           "       convert(varchar(20),a.USERDATE,106) DATE, a.ACC_NO, a.ACC_NAME, b.BANK, a.USERBY, " +
	                           "       case when a.REJECTBY is not null then 'REJECTED by ' + a.REJECTBY + ' on ' + convert(varchar(20),a.REJECTDATE,106) " +
			                   "            when a.APPROVEBY is not null then 'APPROVED by ' + a.APPROVEBY + ' on ' + convert(varchar(20),a.APPROVEDATE,106) " +
	                           "       else 'REGISTERED' end status, " +
	                           "       case when a.REJECTBY is not null or a.APPROVEBY is not null then '' else 'DELETE FROM POLICY_PERIOD_EXPEND where EXPENDNO = ''' + a.EXPENDNO + '''' end ROLBCK " +
                               "from POLICY_PERIOD_EXPEND a " +
                               "inner join FINANCE.dbo.PARAM_TBL_BANK b on a.BANK = b.CODE collate database_default " +
                               "where a.POLICY_PERIOD_ID = '" + policyperiodid + "' order by a.USERDATE";
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
                if (DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "") == "")
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
                    FillDGR(Request.QueryString["ID"]);
                }
                catch { }
            }
        }
    }
}
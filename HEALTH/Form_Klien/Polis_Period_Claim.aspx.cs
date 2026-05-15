using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Claim : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGRClaim();
                FillDGRICD();
            }
        }

        protected void FillDGRICD()
        {
            conn.QueryString = "exec SP_CLAIM_ICD_PERIOD '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_ICD.DataSource = dt;
            DGR_ICD.DataBind();
        }

        protected void FillDGRClaim()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.NAMA, " +
                                "a.FAMILY_GROUP_DESCR, " +
                                "INCURRED = replace(convert(varchar(100),convert(money,a.INCURRED),1),'.00',''), " +
                                "REJECTED = replace(convert(varchar(100),convert(money,a.REJECTED),1),'.00',''), " +
                                "EXCESS = replace(convert(varchar(100),convert(money,a.EXCESS),1),'.00',''), " +
                                "CASH = replace(convert(varchar(100),convert(money,a.CASH),1),'.00',''), " +
                                "REFUND = replace(convert(varchar(100),convert(money,a.REFUND),1),'.00',''), " +
                                "PAID = replace(convert(varchar(100),convert(money,a.PAID),1),'.00',''), " +
                                "a.CASECNT " +
                                "from V_PESERTA_MASTER_CLAIM_PERIOD a " +
                                "where " +
                                "a.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                "order by " +
                                "a.REGNO_EMP, " +
                                "a.FAMILY_GROUP, " +
                                "a.REGNO";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREGNO = (LinkButton)DGR.Items[i].FindControl("LB_REGNO");
                lbREGNO.Text = DGR.Items[i].Cells[1].Text;

                lbREGNO.Attributes.Add("onclick", "window.open('Polis_Period_Claim_Regno.aspx?PolicyPeriod=" +LB_PERIOD.Text+"&Regno=" +DGR.Items[i].Cells[1].Text+ "','Claim','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

            conn.QueryString = "select " +
                                "INCURRED = replace(convert(varchar(100),convert(money,SUM(a.INCURRED)),1),'.00',''), " +
                                "REJECTED = replace(convert(varchar(100),convert(money,SUM(a.REJECTED)),1),'.00',''), " +
                                "EXCESS = replace(convert(varchar(100),convert(money,SUM(a.EXCESS)),1),'.00',''), " +
                                "CASH = replace(convert(varchar(100),convert(money,SUM(a.CASH)),1),'.00',''), " +
                                "REFUND = replace(convert(varchar(100),convert(money,SUM(a.REFUND)),1),'.00',''), " +
                                "PAID = replace(convert(varchar(100),convert(money,SUM(a.PAID)),1),'.00','') " +
                                "from V_PESERTA_MASTER_CLAIM_PERIOD a " +
                                "where " +
                                "a.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            LB_APPROVED.Text = conn.GetFieldValue("PAID").ToString();
            LB_CASH.Text = conn.GetFieldValue("CASH").ToString();
            LB_EXCESS.Text = conn.GetFieldValue("EXCESS").ToString();
            LB_INCURRED.Text = conn.GetFieldValue("INCURRED").ToString();
            LB_REFUND.Text = conn.GetFieldValue("REFUND").ToString();
            LB_REJECTED.Text = conn.GetFieldValue("REJECTED").ToString();
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rs:Format=EXCEL&POLICY_PERIOD_ID=" + LB_PERIOD.Text + "' from V_LINK_SC_REPORT_LIST where CODE = '312'";
            conn.ExecuteQuery();

            Response.Redirect(conn.GetFieldValue("URL").ToString());
        }
    }
}
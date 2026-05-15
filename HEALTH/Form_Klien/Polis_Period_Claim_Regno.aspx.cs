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
    public partial class Polis_Period_Claim_Regno : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                LB_REGNO.Text = Request.QueryString["Regno"];
                FillDGRClaim();
            }
        }

        protected void FillDGRClaim()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.NAMA, " +
                                "DOB = convert(varchar(20),a.DOB,106), " +
                                "FAMREL = c.DESCR, " +
                                "b.COMPANY_NAME, " +
                                "b.POLICY_NO, " +
                                "PERIOD = convert(varchar(20),b.START_DATE,106) + ' - ' + convert(varchar(20),b.END_DATE,106) " +
                                "from PESERTA_MASTER a " +
                                "inner join PR_FAMILY_GROUP c on a.FAMILY_GROUP=c.CODE " +
                                "inner join V_POLICY_PERIOD b on a.POLICY_ID=b.POLICY_ID " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and b.ID = '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_FAMREL.Text = conn.GetFieldValue("FAMREL").ToString();
            LB_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
            LB_PERIODDATE.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();

            conn.QueryString = "exec SP_PESERTA_MASTER_CLAIM_PERIOD '" + LB_REGNO.Text + "','" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            
            
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace GLIFE.Form_Claim
{
    public partial class ClaimInstallmentSchedule : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                FillDGRRange();
            }
        }


        protected void FillDGRRange()
        {

            conn.QueryString = "select " +
                                "a.PAID_DATE, " +
                                "a.AMOUNT " +
                                "from APPICATION_CLAIM_INSTALLMENT_SCHEDULE a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = '" + LB_SEQ.Text + "' " +
                                "order by " +
                                "a.PAID_DATE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRQUERY.DataSource = dt;
            DGRQUERY.DataBind();

            for (int i = 0; i < DGRQUERY.Items.Count; i++)
            {
                Button btDEL = (Button)DGRQUERY.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into APPICATION_CLAIM_INSTALLMENT_SCHEDULE select " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PAIDDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + TXT_AMOUNT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                conn.ExecuteNonQuery();
            }
            catch { }
            FillDGRRange();
        }

        protected void DGRQUERY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APLPLICATION_CLAIM_INSTALLMENT_SCHEDULE where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and SEQ = '" + LB_SEQ.Text + "' " +
                                        "and PAID_DATE = '" + e.Item.Cells[0].Text + "' " +
                                        "and AMOUNT = " + e.Item.Cells[1].Text.Replace(",", "");
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRRange();
            }
        }
    }
}
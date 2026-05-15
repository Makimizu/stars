using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LQ.Form_Tools
{
    public partial class PaymentAcc : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                try
                {
                    LB_DISABLE.Text = Request.QueryString["DISABLE"].ToString();
                }
                catch { }

                Setup();
                LoadAcc();
            }
        }

        protected void CheckTrack()
        {
            string regno, seq;
            switch (LB_CODE.Text)
            {
                case "POS": conn.connString = "select REGNO, SEQ from APPLICATION_ENDORSEMENT_MASTER where REGNO + '-' + convert(varchar(10), SEQ) + '-' + ENDORSEMENT_TYPE = '" + LB_REGNO.Text + "'"; break;
                case "CLM": conn.connString = "select REGNO, SEQ from APPLICATION_CLAIM_MASTER where REGNO + '-' + convert(varchar(10), SEQ) = '" + LB_REGNO.Text + "'"; break;
            }

            conn.ExecuteQuery();
            regno = conn.GetFieldValue("REGNO").ToString();
            seq = conn.GetFieldValue("SEQ").ToString();
        }

        protected void Setup()
        {
            conn.QueryString = "select KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by KODE";
            conn.ExecuteQuery();
            DDL_BANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadAcc()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_PAYMENT_ACC " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TXT_ACCNO.Text = conn.GetFieldValue("ACC_NO").ToString();
            TXT_ACCNAME.Text = conn.GetFieldValue("ACC_NAME").ToString();

            try
            {
                DDL_BANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
            }
            catch { }
        }

    }
}
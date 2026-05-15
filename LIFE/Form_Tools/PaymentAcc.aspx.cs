using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LIFE.Form_Tools
{
    public partial class PaymentAcc : System.Web.UI.Page
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
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                try
                {
                    //LB_DISABLE.Text = Request.QueryString["DISABLE"].ToString();
                    if (Session["DISABLE"] != null)
                    {
                        LB_DISABLE.Text = Session["DISABLE"].ToString();
                    }
                    else if (Request.QueryString["DISABLE"] != null)
                    {
                        LB_DISABLE.Text = Request.QueryString["DISABLE"];
                    }
                    else
                    {
                        LB_DISABLE.Text = ""; // atau default value
                    }
                }
                catch { }

                Setup();
                LoadAcc();
                //CheckTrack();
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

            if (GlobalUse.GetTrack(regno, LB_CODE.Text, seq) > 3)
            {
                TXT_ACCNO.Enabled = false;
                TXT_ACCNAME.Enabled = false;
                DDL_BANK.Enabled = false;
                BT_SAVE.Visible = false;
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by KODE";
            //conn.QueryString = "select CODE, DESCR = CODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by CODE";
            
            conn.ExecuteQuery();
            DDL_BANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadAcc()
        {
            if (LB_DISABLE.Text == "1")
            {
                BT_SAVE.Visible = false;
                TXT_ACCNO.ReadOnly = true;
                TXT_ACCNAME.ReadOnly = true;
                DDL_BANK.Enabled = false;
            }

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

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAME.Text.Trim() == "" || DDL_BANK.SelectedValue == "")
            {
                LB_ERR.Text = "NOMOR, NAMA REKENING & BANK TUJUAN TIDAK BOLEH KOSONG.";
                return;
            }
            try
            {
                LB_ERR.Text = "";
                conn.QueryString = "exec SP_APPLICATION_MASTER_PAYMENT_ACC_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + DDL_BANK.SelectedValue + "'," +
                                    "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            LoadAcc();
        }

        protected void BT_INQUIRY_Click(object sender, EventArgs e)
        {

            try
            {
                string accountNo = TXT_ACCNO.Text.Trim();
                string accountName = TXT_ACCNAME.Text.Trim();
                string bankCode = DDL_BANK.SelectedValue;

                conn.QueryString = "select CLEARING_CODE from FINANCE.dbo.PARAM_TBL_BANK where KODE = '" + bankCode + "'";
                conn.ExecuteQuery(3000);

                string clearingCode = conn.GetFieldValue("CLEARING_CODE").ToString().Substring(0, 3).Trim(); //"009";
                string accounBMITakaful = "3040031803";
                string transferAmount = "0";
                string transferDesc = "-";
                string userBy = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                conn.QueryString = "exec FINANCE.dbo.SP_API_BMI_INQUIRY " +
                                    "'" + clearingCode + "'," +
                                    "'" + accounBMITakaful + "'," +
                                    "'" + accountNo + "'," +
                                    "'" + transferAmount + "'," +
                                    "'" + transferDesc + "'," +
                                    "'" + userBy + "'";


                conn.ExecuteQuery(3000);

                if (conn.GetRowCount() > 0)
                {

                    if (conn.GetFieldValue("errorCode").ToString() == "00")
                    {

                        lblDestName.Text = conn.GetFieldValue("toAccName").ToString();
                        lblDestAccNo.Text = accountNo;
                        lblDestBank.Text = DDL_BANK.SelectedItem.Text;

                        spanInquery.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);

                        spanInquery.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }

    }
}
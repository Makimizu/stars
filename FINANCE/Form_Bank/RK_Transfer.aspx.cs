using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class RK_Transfer : System.Web.UI.Page
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

                
                LB_CODE.Text = Request.QueryString["CODE"];
                Setup();
            }
        }

        protected void FillDGRInfo()
        {
            conn.QueryString = "exec SP_INVOICE_RK " +
                                "'RK'," +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO.DataSource = dt;
            DGR_INFO.DataBind();
        }

        protected void Setup()
        {
            FillDGRInfo();

            BT_SUBMIT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk Pindah Dana ?')){return false;};");

            conn.QueryString = "SELECT CODE, DESCR FROM PARAM_TIPE_SETTLEMENT WHERE APP_ID = 'FN' AND ACCOUNT_TYPE = 'INV' AND ENABLE_REFUND = 1 ORDER BY 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SETTLEMENTTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,BANK from PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT NOREK = '', BANK = ''" +
                               " UNION ALL " +
                               " SELECT NOREK, BANK FROM REKENING_MASTER rm";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ACCMASTER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT '' AS CODE,'' AS DESCR " +
                                   "UNION ALL " +
                                   "SELECT CODE, DESCR FROM PR_TCODE_T00 " +
                                   "ORDER BY 2";
            conn.ExecuteQuery();
            DDL_T00.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_T00.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "SELECT * FROM V_REKENING_JURNAL_TRANSFER " +
                                "WHERE TRXID = '" + LB_CODE.Text + "' " +
                                "ORDER BY USERDATE DESC";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;

            conn.QueryString = "SELECT TOTAL_AMOUNT = CONVERT(VARCHAR(1000), CONVERT(MONEY, SUM(a.AMOUNT)), 1) " +
                                "FROM V_REKENING_JURNAL_TRANSFER a " +
                                "WHERE TRXID = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DGR.Columns[6].FooterText = conn.GetFieldValue("TOTAL_AMOUNT").ToString();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btEDIT = (Button)DGR.Items[i].FindControl("BT_EDIT");

                if (DGR.Items[i].Cells[9].Text == "NEW")
                {
                    btDEL.Visible = true;
                    btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk Hapus data ?')){return false;};");
                    btEDIT.Visible = true;

                }
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            if (TXT_ACCNO.Text.Trim() == "")
            {
                LB_ERROR.Text = "ACC NO is Empty";
                return;
            }

            if (TXT_ACCNAME.Text.Trim() == "")
            {
                LB_ERROR.Text = "ACC NAME is Empty";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_REKENING_JURNAL_TRANSFER_INSERT " +
                                    "'" + TXT_ID.Text + "'," + 
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                    "'" + DDL_ACCBANK.SelectedValue + "'," +
                                    "'" + TXT_AMOUNT.Text.Replace(",", "") + "'," +
                                    "'" + TXT_DESCR.Text.Trim() + "'," +
                                    "'" + DDL_SETTLEMENTTYPE.SelectedValue + "'," +
                                    "'" + DDL_T00.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                TXT_ACCNO.Text = "";
                TXT_ACCNAME.Text = "";
                TXT_AMOUNT.Text = "";
                TXT_DESCR.Text = "";
                DDL_ACCMASTER.SelectedValue = "";
                TXT_ID.Text = "";

                FillDGRInfo();
                FillDGR();

                //Response.Redirect("RK.aspx");
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void BT_INQUIRY_Click(object sender, EventArgs e)
        {
            try
            {
                string accountNo = TXT_ACCNO.Text.Trim();
                string accountName = TXT_ACCNAME.Text.Trim();
                string bankCode = DDL_ACCBANK.SelectedValue;

                conn.QueryString = "select CLEARING_CODE from FINANCE.dbo.PARAM_TBL_BANK where Code = '" + bankCode + "'";
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
                        lblDestBank.Text = DDL_ACCBANK.SelectedItem.Text;

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

        protected void DDL_ACCMASTER_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.QueryString = "select * from REKENING_MASTER where NOREK = '" + DDL_ACCMASTER.SelectedValue + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_ACCNO.Text = conn.GetFieldValue("NOREK").ToString();
                TXT_ACCNAME.Text = conn.GetFieldValue("NAMA").ToString();
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("BANK_CODE").ToString();
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_REKENING_JURNAL_TRANSFER_ROLLBACK " +
                                        "'" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteQuery();
                    FillDGR();

                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }

            if (e.CommandName == "Edit")
            {
                try
                {
                    conn.QueryString = "SELECT * FROM REKENING_JURNAL_TRANSFER WHERE ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        TXT_ID.Text = e.Item.Cells[0].Text;
                        TXT_ACCNO.Text = conn.GetFieldValue("NOREK").ToString();
                        TXT_ACCNAME.Text = conn.GetFieldValue("ACC_NAME").ToString();
                        DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACC_BANK").ToString();
                        TXT_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();
                        TXT_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
                        DDL_SETTLEMENTTYPE.SelectedValue = conn.GetFieldValue("TIPE_SETTLEMENT").ToString();
                        DDL_T00.SelectedValue = conn.GetFieldValue("T00").ToString();
                    }
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class Validasi_BMI : System.Web.UI.Page
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

                LB_MODE.Text = Request.QueryString["MODE"];
                LB_CODE.Text = Request.QueryString["CODE"];
                LB_ACCT_NO.Text = Request.QueryString["ACC_NO"];
                LB_ACCT_NAME.Text = Request.QueryString["ACC_NAME"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                    "pass = PASSWORD " +
                                    "from security..M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            string
            pass = conn.GetFieldValue("pass").ToString();
            string
            password = Crypto.DecryptStringAES(pass.Trim());

            BT_REGISTRATION.Attributes.Add("onclick", "if(!confirm('Are you sure to REGISTRATION ?')){return false;};");
            //BT_VERIFICATION.Attributes.Add("onclick", "let pass = prompt('Please enter your password');if (pass != '" + password + "'){alert('salah'); return false};");
            //BT_APPROVED.Attributes.Add("onclick", "let pass = prompt('Please enter your password');if (pass != '" + password + "'){alert('salah'); return false};");
            BT_REJECT.Attributes.Add("onclick", "if(!confirm('Are you sure to REJECT ?')){return false;};");

            conn.QueryString = "select " +
                                    "rekapid, status " +
                                    "from LOGGER.dbo.VALIDASI_BMI where rekapid = '" + LB_CODE.Text + "'  and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
            conn.ExecuteQuery();
            string
            status = conn.GetFieldValue("STATUS").ToString();

            if (status == "1")
            {
                FillDGRInfo();
                TB_TRANSFER.Visible = false;
                TB_INQUIRY.Visible = false;

                conn.QueryString = "select " +
                                    "CODE " +
                                    "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'VERIFICATION' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                conn.ExecuteQuery();
                string
                CODE = conn.GetFieldValue("CODE").ToString();

                if (CODE == "VERIFICATION") //15
                {
                    conn.QueryString = "select " +
                                    "registerby " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    maker = conn.GetFieldValue("registerby").ToString();

                    if (maker != GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"))
                    {
                        BT_VERIFICATION.Visible = true;
                        BT_REJECT.Visible = true;
                    }
                    LB_STATUS.Text = "REGISTRATION";
                }
                else
                {
                    conn.QueryString = "select " +
                                    "statusReject " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    reject = conn.GetFieldValue("statusReject").ToString();

                    if (reject == "1")
                    {
                        LB_STATUS.Text = "REJECT";
                    }
                    else
                    {
                        LB_STATUS.Text = "REGISTRATION";
                    }
                }
            }
            else if (status == "2")
            {
                FillDGRInfo();
                TB_TRANSFER.Visible = false;
                TB_INQUIRY.Visible = false;

                conn.QueryString = "select " +
                                    "CODE " +
                                    "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'APPROVED' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                conn.ExecuteQuery();
                string
                CODE = conn.GetFieldValue("CODE").ToString();

                if (CODE == "APPROVED") //35
                {
                    conn.QueryString = "select " +
                                    "verifby " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    verif = conn.GetFieldValue("verifby").ToString();

                    if (verif != GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"))
                    {
                        BT_REJECT.Visible = true;
                        BT_APPROVED.Visible = true;
                    }

                    LB_STATUS.Text = "VERIFICATION";
                }
                else
                {
                    conn.QueryString = "select " +
                                    "statusReject " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    reject = conn.GetFieldValue("statusReject").ToString();

                    if (reject == "1")
                    {
                        LB_STATUS.Text = "REJECT";
                    }
                    else
                    {
                        LB_STATUS.Text = "VERIFICATION";
                    }
                }
            }
            else
            {
                FillDDLTransfer();
                TB_INFO.Visible = false;
                TB_INQUIRY.Visible = false;

                conn.QueryString = "select " +
                                    "CODE " +
                                    "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'REGISTRATION' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                conn.ExecuteQuery();
                string
                CODE = conn.GetFieldValue("CODE").ToString();

                if (CODE == "REGISTRATION") //36
                {
                    BT_REGISTRATION.Visible = true;

                    conn.QueryString = "select " +
                                    "statusReject " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    reject = conn.GetFieldValue("statusReject").ToString();

                    if (reject == "1")
                    {
                        LB_STATUS.Text = "REJECT";
                    }
                    else
                    {
                        LB_STATUS.Text = "";
                    }
                }
                else
                {
                    conn.QueryString = "select " +
                                    "statusReject " +
                                    "from LOGGER.dbo.VALIDASI_BMI WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                    conn.ExecuteQuery();
                    string
                    reject = conn.GetFieldValue("statusReject").ToString();

                    if (reject == "1")
                    {
                        LB_STATUS.Text = "REJECT";
                    }
                    else
                    {
                        LB_STATUS.Text = "";
                    }

                }
            }
        }

        protected void FillDDLTransfer()
        {

            conn.QueryString = "SELECT CODE = '1', DESCR = 'Transfer BMI' UNION ALL SELECT CODE = '2', DESCR = 'Transfer Online' UNION ALL SELECT CODE = '3', DESCR = 'Transfer SKN' UNION ALL SELECT CODE = '4', DESCR = 'Transfer RTGS'";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRANSFER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGRInquiry()
        {
            conn.QueryString = "SELECT toAcctNo = a.ACC_NO, toAcctName = a.ACC_NAME, toAcctBankCode = LEFT(d.CLEARING_CODE, 3), toAcctBank = d.BANK, swiftCode = d.RTGS_CODE " +
                                "FROM V_SETTLEMENT_MASTER_REKAP a " +
                                "inner join PARAM_TBL_BANK d ON d.CODE collate database_default = a.ACC_BANK_CODE " +
                                "where a.REKAPID = '" + LB_CODE.Text + "' and a.ACC_NO = '" + LB_ACCT_NO.Text + "' and a.ACC_NAME = '" + LB_ACCT_NAME.Text + "'";
            conn.ExecuteQuery();

            LB_ACCNAME.Text = conn.GetFieldValue("toAcctName").ToString();
            LB_ACCNO.Text = conn.GetFieldValue("toAcctNo").ToString();
            LB_ACCBANK.Text = conn.GetFieldValue("toAcctBankCode").ToString();
            LB_ACCBANKDESC.Text = conn.GetFieldValue("toAcctBank").ToString();
            LB_SWIFTCODE.Text = conn.GetFieldValue("swiftCode").ToString();

        }

        protected void BT_INQUIRY_Click(object sender, EventArgs e)
        {
            FillDGRInfo();
        }

        protected void FillDGRInfo()
        {
            conn.QueryString = "select " +
                                    "rekapid, status " +
                                    "from LOGGER.dbo.VALIDASI_BMI where rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
            conn.ExecuteQuery();
            string
            status = conn.GetFieldValue("STATUS").ToString();

            if (status == "1" || status == "2")
            {
                conn.QueryString = "select " +
                                    "toAcctName, toAcctNo, fromAcctNo, fromAcctName, transferAmount = replace(convert(varchar(100),convert(money,transferAmount),1),'.00',''), transferDescription, " +
                                    "toBank = CASE WHEN b.BANK IS NULL THEN c.BANK_NAME ELSE b.BANK END " +
                                    "from LOGGER.dbo.VALIDASI_BMI a " +
                                    "left join FINANCE.dbo.PARAM_TBL_BANK b on b.KODE = a.toBankCode " +
                                    "left join FINANCE.dbo.BANK_SKN_RTGS c on c.BANK_CODE = a.toBankCode " +
                                    "where a.rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and a.nameStars = '" + LB_ACCT_NAME.Text + "'";
                conn.ExecuteQuery();

                LB_ACCNAME_INFO.Text = conn.GetFieldValue("toAcctName").ToString();
                LB_ACCNO_INFO.Text = conn.GetFieldValue("toAcctNo").ToString();
                LB_BANK_INFO.Text = conn.GetFieldValue("toBank").ToString();
                LB_SRNAME_INFO.Text = conn.GetFieldValue("fromAcctName").ToString();
                LB_SRACC_INFO.Text = conn.GetFieldValue("fromAcctNo").ToString();
                LB_AMOUNT_INFO.Text = conn.GetFieldValue("transferAmount").ToString();
                LB_TFDESC_INFO.Text = conn.GetFieldValue("transferDescription").ToString();
                TB_INFO.Visible = true;
            }
            else
            {

                conn.QueryString = "select " +
                                   "SOURCE_NAME = 'PT ASURANSI TAKAFUL KELUARGA', " +
                                   "SOURCE_ACC = a.ACC_SOURCE, " +
                                   "TF_DESC = REPLACE(a.REKAPID, '-', ' '), " +
                                   "AMOUNT = FLOOR(isnull(a.AMOUNT,0)), " +
                                   "AMOUNTS = convert(varchar(1000),convert(money,FLOOR(isnull(a.AMOUNT,0))),1) collate database_default " +
                                   "from V_SETTLEMENT_MASTER_REKAP a " +
                                   "where a.REKAPID = '" + LB_CODE.Text + "' and a.ACC_NO = '" + LB_ACCT_NO.Text + "' and a.ACC_NAME = '" + LB_ACCT_NAME.Text + "'";
                conn.ExecuteQuery();

                SOURCE_NAME.Text = conn.GetFieldValue("SOURCE_NAME").ToString();
                SOURCE_ACC.Text = conn.GetFieldValue("SOURCE_ACC").ToString();
                TF_DESC.Text = conn.GetFieldValue("TF_DESC").ToString();
                AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();
                AMOUNTS.Text = conn.GetFieldValue("AMOUNTS").ToString();

                if (DDL_TRANSFER.SelectedValue == "3")
                {
                    if (Int32.Parse(AMOUNT.Text) > 50000000 && Int32.Parse(AMOUNT.Text) < 100000000)
                    {
                        conn.QueryString = "exec SP_API_BMI_INQUIRY " +
                                    "'" + LB_ACCBANK.Text + "'," +
                                    "'" + SOURCE_ACC.Text + "'," +
                                    "'" + LB_ACCNO.Text + "'," +
                                    "'10000'," +
                                    "'" + LB_CODE.Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery(3000);
                    }
                    else
                    {
                        conn.QueryString = "exec SP_API_BMI_INQUIRY " +
                                    "'" + LB_ACCBANK.Text + "'," +
                                    "'" + SOURCE_ACC.Text + "'," +
                                    "'" + LB_ACCNO.Text + "'," +
                                    "'" + AMOUNT.Text + "'," +
                                    "'" + LB_CODE.Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery(3000);
                    }
                }
                else
                {
                    conn.QueryString = "exec SP_API_BMI_INQUIRY " +
                                        "'" + LB_ACCBANK.Text + "'," +
                                        "'" + SOURCE_ACC.Text + "'," +
                                        "'" + LB_ACCNO.Text + "'," +
                                        "'" + AMOUNT.Text + "'," +
                                        "'" + LB_CODE.Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(3000);
                }

                if (conn.GetRowCount() > 0)
                {
                    if (conn.GetFieldValue("errorCode").ToString() == "00")
                    {
                        LB_ACCNAME_INFO.Text = conn.GetFieldValue("toAccName").ToString();
                        LB_BANK_INFO.Text = LB_ACCBANKDESC.Text;
                        LB_SRNAME_INFO.Text = SOURCE_NAME.Text;
                        LB_SRACC_INFO.Text = SOURCE_ACC.Text;
                        LB_AMOUNT_INFO.Text = AMOUNTS.Text;
                        LB_TFDESC_INFO.Text = TF_DESC.Text;
                        LB_ACCNO_INFO.Text = LB_ACCNO.Text;
                        LB_TRANSACTIONID_INFO.Text = conn.GetFieldValue("transactionId").ToString();
                        TB_INFO.Visible = true;

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                        TB_INFO.Visible = false;
                    }
                }
            }
        }

        protected void BT_PROCESS_Click(object sender, EventArgs e)
        {
            FillDGRInquiry();
            TB_INQUIRY.Visible = true;
        }

        protected void BT_REGISTRATION_Click(object sender, EventArgs e)
        {
            try
            {
                if (DDL_TRANSFER.SelectedValue == "2" || DDL_TRANSFER.SelectedValue == "3")
                {
                    conn.QueryString = "select " +
                                   "CODE = a.BANK_CODE " +
                                   "from BANK_SKN_RTGS a " +
                                   "where a.SWIFT_CODE = '" + LB_SWIFTCODE.Text + "' ";
                    conn.ExecuteQuery();

                    KODEBANK.Text = conn.GetFieldValue("CODE").ToString();
                }
                else
                {
                    KODEBANK.Text = LB_ACCBANK.Text;
                }

                conn.QueryString = "exec SP_API_BMI_VALIDASI_INSERT " +
                                    "'" + LB_TRANSACTIONID_INFO.Text + "'," +
                                    "'" + LB_SRNAME_INFO.Text + "'," +
                                    "'" + LB_SRACC_INFO.Text + "'," +
                                    "'" + LB_ACCNO_INFO.Text + "'," +
                                    "'" + LB_ACCNAME_INFO.Text + "'," +
                                    "'" + KODEBANK.Text + "'," +
                                    "'" + DDL_TRANSFER.SelectedValue + "'," +
                                    "'" + AMOUNT.Text + "'," +
                                    "'" + LB_TFDESC_INFO.Text + "'," +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + LB_ACCT_NO.Text + "'," +
                                    "'" + LB_ACCT_NAME.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteNonQuery();
                Response.Redirect("StlPost.aspx?");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select " +
                                    "pass = PASSWORD " +
                                    "from security..M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            string
            pass = conn.GetFieldValue("pass").ToString();
            string
            password = Crypto.DecryptStringAES(pass.Trim());

            if (TXT_PASS.Text == password)
            {
                if (TXT_TYPE.Text == "VERIF")
                {
                    conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET status = '2', verifby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', verifdate = GETDATE() WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";

                    try
                    {
                        conn.ExecuteNonQuery();
                        Response.Redirect("StlPost.aspx?");
                    }
                    catch (System.Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
                    }
                }
                else
                {
                    try
                    {
                        conn.QueryString = "select " +
                                            "toAcctName, typetransfer, toAcctNo, toBankCode, fromAcctNo, fromAcctName, transferAmount, transferDescription, nameStars, toAcctNoStars " +
                                            "from LOGGER.dbo.VALIDASI_BMI where rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                        conn.ExecuteQuery();

                        string
                            fromAcctNo = conn.GetFieldValue("fromAcctNo").ToString();
                        string
                            toBankCode = conn.GetFieldValue("toBankCode").ToString();
                        string
                            toAcctNo = conn.GetFieldValue("toAcctNo").ToString();
                        string
                            transferAmount = conn.GetFieldValue("transferAmount").ToString();
                        string
                            toAcctName = conn.GetFieldValue("toAcctName").ToString();
                        string
                            transferDescription = conn.GetFieldValue("transferDescription").ToString();
                        string
                            fromAcctName = conn.GetFieldValue("fromAcctName").ToString();
                        string
                            typetransfer = conn.GetFieldValue("typetransfer").ToString();
                        string
                            nameStars = conn.GetFieldValue("nameStars").ToString();
                        string
                            toAcctNoStars = conn.GetFieldValue("toAcctNoStars").ToString();

                        conn.QueryString = "exec SP_API_BMI_TRANSFER " +
                                        "'" + toBankCode + "'," +
                                        "'" + fromAcctNo + "'," +
                                        "'" + toAcctNo + "'," +
                                        "'" + transferAmount + "'," +
                                        "'" + transferDescription + "'," +
                                        "'" + toAcctName + "'," +
                                        "'" + typetransfer + "'," +
                                        "'" + fromAcctName + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "'" + nameStars + "'," +
                                        "'" + toAcctNoStars + "'";
                        conn.ExecuteQuery(3000);

                        if (conn.GetRowCount() > 0)
                        {
                            conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                            conn.ExecuteNonQuery();
                            if (conn.GetFieldValue("errorCode").ToString() == "00")
                            {
                                conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', status = '00' , approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";
                                conn.ExecuteNonQuery();
                                Response.Redirect("StlPost.aspx?");
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                            }
                        }

                    }
                    catch (System.Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('password anda salah')", true);
            }
        }

        protected void BT_VERIFICATION_Click(object sender, EventArgs e)
        {
            TXT_TYPE.Text = "VERIF";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            
        }

        protected void BT_APPROVED_Click(object sender, EventArgs e)
        {
            TXT_TYPE.Text = "APPROVE";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET statusReject = '1', status = '0' WHERE rekapid = '" + LB_CODE.Text + "' and toAcctNoStars = '" + LB_ACCT_NO.Text + "' and nameStars = '" + LB_ACCT_NAME.Text + "'";

            try
            {
                conn.ExecuteNonQuery();
                Response.Redirect("StlPost.aspx?");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
    }
}
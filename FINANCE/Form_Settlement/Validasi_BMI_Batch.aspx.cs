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
    public partial class Validasi_BMI_Batch : System.Web.UI.Page
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
                LB_ACCT_NO.Text = Request.QueryString["ACC_NO"];
                LB_ACCT_NAME.Text = Request.QueryString["ACC_NAME"];
                LB_STATUS.Text = Request.QueryString["VALIDATION"];

                Setup();
            }
        }

        protected void Setup()
        {
            FillDDLTransfer();
            FillBTInfo();
        }

        protected void FillDDLTransfer()
        {
            string Query = "SELECT CODE = '1', " +
                            "DESCR = 'Transfer BMI' " +
                            "UNION ALL " +
                            "SELECT CODE = '2', " +
                            "DESCR = 'Transfer Online' " +
                            "UNION ALL " +
                            "SELECT CODE = '3', " +
                            "DESCR = 'Transfer SKN' " +
                            "UNION ALL " +
                            "SELECT CODE = '4', " +
                            "DESCR = 'Transfer RTGS'";

            if (Request.QueryString["INHOUSE"] == "and a.ACC_SOURCE_BANK_CODE <> a.ACC_BANK_CODE")
            {
                Query = "SELECT CODE = '3', " +
                        "DESCR = 'Transfer SKN' " +
                        "UNION ALL " +
                        "SELECT CODE = '1', " +
                        "DESCR = 'Transfer BMI' " +
                        "UNION ALL " +
                        "SELECT CODE = '2', " +
                        "DESCR = 'Transfer Online' " +
                        "UNION ALL " +
                        "SELECT CODE = '4', " +
                        "DESCR = 'Transfer RTGS'";
            }
            conn.QueryString = Query;
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRANSFER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillBTInfo()
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

            //conn.QueryString = "EXEC SP_SETTLEMENT_VALIDATION_LOGGER " +
            //                        "'" + LB_CODE.Text + "'" +
            //                        ",'" + LB_ACCT_NO.Text + "'" +
            //                        ",'" + LB_ACCT_NAME.Text + "'";
            //conn.ExecuteQuery();

            //ANDEZ CR 100 DATA H2H START
            conn.QueryString = "EXEC SP_SETTLEMENT_VALIDATION_LOGGER " +
                                    "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            //ANDEZ CR 100 DATA H2H END

            conn.ExecuteQuery();
            string status = conn.GetFieldValue("status").ToString();
            string registerby = conn.GetFieldValue("registerby").ToString();
            string verif = conn.GetFieldValue("verifby").ToString();
            string reject = conn.GetFieldValue("statusReject").ToString();

            if (status == "1")
            {
                FillDGR();

                conn.QueryString = "select " +
                                    "CODE " +
                                    "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'VERIFICATION' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                conn.ExecuteQuery();
                string
                CODE = conn.GetFieldValue("CODE").ToString();

                if (CODE == "VERIFICATION") //15
                {
                    if (registerby != GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"))
                    {
                        BT_VERIFICATION.Visible = true;
                        BT_REJECT.Visible = true;
                    }
                }
            }
            else if (status == "2")
            {
                TB_TRANSFER.Visible = false;
                FillDGR();
                conn.QueryString = "select " +
                                    "CODE " +
                                    "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'APPROVED' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                conn.ExecuteQuery();
                string
                CODE = conn.GetFieldValue("CODE").ToString();

                if (CODE == "APPROVED") //35
                {
                    if (verif != GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"))
                    {
                        BT_REJECT.Visible = true;
                        BT_APPROVED.Visible = true;
                    }
                }
            }
            else
            {
                FillDGR();
                if (status != "00")
                {
                    conn.QueryString = "select " +
                                        "CODE " +
                                        "from SECURITY..PR_EXECUTE_PAYMENT where CODE = 'REGISTRATION' and DESCR LIKE '%#" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "#%'";
                    conn.ExecuteQuery();
                    string
                    CODE = conn.GetFieldValue("CODE").ToString();

                    if (CODE == "REGISTRATION") //36
                    {
                        BT_REGISTRATION.Visible = true;
                    }
                }
            }
        }

        protected void FillDGR()
        {
            //conn.QueryString = "exec SP_SETTLEMENT_VALIDATION " +
            //                        "'" + DDL_TRANSFER.SelectedValue + "'" +
            //                        ",'" + LB_CODE.Text + "'" +
            //                        ",'" + LB_ACCT_NO.Text + "'" +
            //                        ",'" + LB_ACCT_NAME.Text + "'";
            //conn.ExecuteQuery(3000);

            //ANDEZ CR 100 DATA H2H START
            conn.QueryString = "exec SP_SETTLEMENT_VALIDATION " +
                                    "'" + DDL_TRANSFER.SelectedValue + "'" +
                                    ",'" + LB_CODE.Text + "'";
            conn.ExecuteQuery(3000);
            //ANDEZ CR 100 DATA H2H END

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            string status = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAccNo = (TextBox)DGR.Items[i].FindControl("TXT_ACCNO");
                Label txtRowNumber = (Label)DGR.Items[i].FindControl("ROW_NUMBER");
                Button btnInquiry = (Button)DGR.Items[i].FindControl("BT_INQUIRY");

                txtRowNumber.Text = (i + 1).ToString();
                txtAccNo.Text = DGR.Items[i].Cells[3].Text;
                status = DGR.Items[i].Cells[24].Text;

                if (status == "1" || status == "2")
                {
                    txtAccNo.ReadOnly = true;
                    btnInquiry.Visible = false;
                }
            }

            conn.QueryString = "SELECT CODE, BANK FROM PARAM_TBL_BANK WHERE COALESCE(BANK, '') <> '' AND CODE <> 'X' ORDER BY BANK";
            conn.ExecuteQuery();

            if (status == "1" || status == "2")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_ACC_BANK_DESC");
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        if (DGR.Items[i].Cells[25].Text == conn.GetFieldValue(j, 0).ToString())
                        {
                            ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_ACC_BANK_DESC");
                    ddl.Items.Add(new ListItem("", ""));
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }

                    ddl.SelectedValue = DGR.Items[i].Cells[25].Text;
                }
            }

            conn.QueryString = "SELECT CODE = '1', " +
                                "DESCR = 'Transfer BMI' " +
                                "UNION ALL " +
                                "SELECT CODE = '2', " +
                                "DESCR = 'Transfer Online' " +
                                "UNION ALL " +
                                "SELECT CODE = '3', " +
                                "DESCR = 'Transfer SKN' " +
                                "UNION ALL " +
                                "SELECT CODE = '4', " +
                                "DESCR = 'Transfer RTGS'";
            conn.ExecuteQuery();
            if (status == "1" || status == "2")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DropDownList DDLTransferType = (DropDownList)DGR.Items[i].FindControl("DDL_TRANSFER_TYPE");
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        if (DGR.Items[i].Cells[26].Text == conn.GetFieldValue(j, 0).ToString())
                        {

                            DDLTransferType.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DropDownList DDLTransferType = (DropDownList)DGR.Items[i].FindControl("DDL_TRANSFER_TYPE");
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        DDLTransferType.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }

                    DDLTransferType.SelectedValue = DGR.Items[i].Cells[26].Text;
                }
            }

            //INQUIRY
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList DDLTransferType = (DropDownList)DGR.Items[i].FindControl("DDL_TRANSFER_TYPE");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (status == "0")
                {
                    int AMOUNT_TRANSFER = Int32.Parse(DGR.Items[i].Cells[21].Text);

                    if (DDLTransferType.SelectedValue == "3" && AMOUNT_TRANSFER > 50000000 && AMOUNT_TRANSFER < 100000000)
                    {
                        AMOUNT_TRANSFER = 10000;
                    }

                    conn.QueryString = "exec SP_API_BMI_INQUIRY " +
                                        "'" + DGR.Items[i].Cells[7].Text + "'," +
                                        "'" + DGR.Items[i].Cells[19].Text + "'," +
                                        "'" + DGR.Items[i].Cells[12].Text + "'," +
                                        "'" + AMOUNT_TRANSFER + "'," +
                                        "'" + DGR.Items[i].Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(3000);

                    if (conn.GetRowCount() > 0)
                    {
                        if (conn.GetFieldValue("errorCode").ToString() == "00")
                        {
                            DGR.Items[i].Cells[10].Text = conn.GetFieldValue("toAccName").ToString();
                            DGR.Items[i].Cells[11].Text = conn.GetFieldValue("transactionId").ToString();
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                            DGR.Items[i].Cells[10].Text = "";
                            DGR.Items[i].Cells[11].Text = "";
                            DGR.Items[i].Cells[12].Text = "";
                            DGR.Items[i].Cells[13].Text = "";
                            cb.Enabled = false;
                            cb.Checked = false;
                            Recalculate();
                        }
                    }
                }
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            int totalAmount = 0;
            int totalData = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Enabled)
                    cb.Checked = ((CheckBox)sender).Checked;

                if (cb.Checked)
                {
                    totalAmount += Convert.ToInt32(DGR.Items[i].Cells[21].Text);
                    totalData += 1;
                }
            }

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                 "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + totalData.ToString("##,#") + "</TD></TR>" +
                                 "<TR><TD>Total Amount</TD><TD>:</TD><TD>" + totalAmount.ToString("##,#") + "</TD></TR>" +
                                 "</TABLE>";
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            int totalAmount = 0;
            int totalData = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (cb.Checked)
                {
                    totalAmount += Convert.ToInt32(DGR.Items[i].Cells[21].Text);
                    totalData += 1;
                }
            }

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + totalData.ToString("##,#") + "</TD></TR>" +
                                "<TR><TD>Total Amount</TD><TD>:</TD><TD>" + totalAmount.ToString("##,#") + "</TD></TR>" +
                                "</TABLE>";
        }

        protected void BT_PROCESS_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_REGISTRATION_Click(object sender, EventArgs e)
        {
            try
            {
                string KodeBank = "";
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    DropDownList DDLTransferType = (DropDownList)DGR.Items[i].FindControl("DDL_TRANSFER_TYPE");

                    if (cb.Checked)
                    {
                        KodeBank = DGR.Items[i].Cells[7].Text;

                        if (DDLTransferType.SelectedValue == "2" || DDLTransferType.SelectedValue == "3")
                        {
                            conn.QueryString = "select " +
                                           "CODE = a.BANK_CODE " +
                                           "from BANK_SKN_RTGS a " +
                                           "where a.SWIFT_CODE = '" + DGR.Items[i].Cells[8].Text + "' ";
                            conn.ExecuteQuery();
                            KodeBank = conn.GetFieldValue("CODE").ToString();
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + DGR.Items[i].Cells[1].Text + "')", true);

                        conn.QueryString = "exec SP_API_BMI_VALIDASI_INSERT " +
                                    "'" + DGR.Items[i].Cells[11].Text + "'," + //TRANSACTIONID_INFO
                                    "'" + DGR.Items[i].Cells[14].Text + "'," + //SRNAME_INFO
                                    "'" + DGR.Items[i].Cells[15].Text + "'," + //SRACC_INFO
                                    "'" + DGR.Items[i].Cells[12].Text + "'," + //ACCNO_INFO
                                    "'" + DGR.Items[i].Cells[10].Text + "'," + //ACCNAME_INFO
                                    "'" + KodeBank + "'," + //KODEBANK
                                    "'" + DDLTransferType.SelectedValue + "'," + //DDL_TRANSFER
                                    "'" + DGR.Items[i].Cells[21].Text + "'," + //AMOUNT
                                    "'" + DGR.Items[i].Cells[20].Text + "'," + //TFDESC_INFO
                                    "'" + DGR.Items[i].Cells[1].Text + "'," + //CODE
                                    "'" + DGR.Items[i].Cells[3].Text + "'," + //ACCT_NO
                                    "'" + DGR.Items[i].Cells[2].Text + "'," + //ACCT_NAME
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                        conn.ExecuteNonQuery();
                    }
                }


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
                    try
                    {
                        for (int i = 0; i < DGR.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                            if (cb.Checked)
                            {
                                conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET " +
                                                    "status = '2', " +
                                                    "verifby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                                    "verifdate = GETDATE() " +
                                                    "WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' " +
                                                    "and toAcctNoStars = '" + DGR.Items[i].Cells[3].Text + "' " +
                                                    "and nameStars = '" + DGR.Items[i].Cells[2].Text + "'";

                                conn.ExecuteNonQuery();
                            }
                        }
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
                        for (int i = 0; i < DGR.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                            if (cb.Checked)
                            {
                                conn.QueryString = "select " +
                                            "toAcctName, typetransfer, toAcctNo, toBankCode, fromAcctNo, fromAcctName, transferAmount, transferDescription, nameStars, toAcctNoStars " +
                                            "from LOGGER.dbo.VALIDASI_BMI " +
                                            "where rekapid = '" + DGR.Items[i].Cells[1].Text + "' " +
                                            "and toAcctNoStars = '" + DGR.Items[i].Cells[3].Text + "' " +
                                            "and nameStars = '" + DGR.Items[i].Cells[2].Text + "' " +
                                            "and status <> '00' ";
                                conn.ExecuteQuery();

                                if (conn.GetRowCount() > 0)
                                {
                                    string fromAcctNo = conn.GetFieldValue("fromAcctNo").ToString();
                                    string toBankCode = conn.GetFieldValue("toBankCode").ToString();
                                    string toAcctNo = conn.GetFieldValue("toAcctNo").ToString();
                                    string transferAmount = conn.GetFieldValue("transferAmount").ToString();
                                    string toAcctName = conn.GetFieldValue("toAcctName").ToString();
                                    string transferDescription = conn.GetFieldValue("transferDescription").ToString();
                                    string fromAcctName = conn.GetFieldValue("fromAcctName").ToString();
                                    string typetransfer = conn.GetFieldValue("typetransfer").ToString();
                                    string nameStars = conn.GetFieldValue("nameStars").ToString();
                                    string toAcctNoStars = conn.GetFieldValue("toAcctNoStars").ToString();

                                    conn.QueryString = "exec SP_API_BMI_TRANSFER " +
                                            "'" + toBankCode + "'," +
                                            "'" + fromAcctNo + "'," +
                                            "'" + toAcctNo + "'," +
                                            "'" + transferAmount + "'," +
                                            "'" + transferDescription + "'," +
                                            "'" + toAcctName + "'," +
                                            "'" + typetransfer + "'," +
                                            "'" + fromAcctName + "'," +
                                            "'" + nameStars + "'," +
                                            "'" + toAcctNoStars + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                    conn.ExecuteQuery(3000);

                                    //if (conn.GetRowCount() > 0)
                                    //{
                                    //    //conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
                                    //    //conn.ExecuteNonQuery();
                                    //    //if (conn.GetFieldValue("errorCode").ToString() == "00")
                                    //    //{
                                    //    //    conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET status = '00', transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
                                    //    //    conn.ExecuteNonQuery();
                                    //    //}
                                    //    //else
                                    //    //{
                                    //    //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                                    //    //}

                                    //    if (conn.GetFieldValue("errorCode").ToString() != "00")
                                    //    {
                                    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                                    //    }
                                    //}
                                }

                                //conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET status = '00', transactionIDTransfer = 'yulia test approved', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
                                //conn.ExecuteNonQuery();

                            }
                        }
                        Response.Redirect("StlPost.aspx?");
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
            //try
            //{
            //    for (int i = 0; i < DGR.Items.Count; i++)
            //    {
            //        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

            //        if (cb.Checked)
            //        {
            //            conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET " +
            //                                "status = '2', " +
            //                                "verifby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
            //                                "verifdate = GETDATE() " +
            //                                "WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' " +
            //                                "and toAcctNoStars = '" + DGR.Items[i].Cells[3].Text + "' " +
            //                                "and nameStars = '" + DGR.Items[i].Cells[2].Text + "'";

            //            conn.ExecuteNonQuery();
            //        }
            //    }
            //    Response.Redirect("StlPost.aspx?");
            //}
            //catch (System.Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            //}
        }

        protected void BT_APPROVED_Click(object sender, EventArgs e)
        {
            TXT_TYPE.Text = "APPROVE";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            //try
            //{
            //    for (int i = 0; i < DGR.Items.Count; i++)
            //    {
            //        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

            //        if (cb.Checked)
            //        {
            //            conn.QueryString = "select " +
            //                        "toAcctName, typetransfer, toAcctNo, toBankCode, fromAcctNo, fromAcctName, transferAmount, transferDescription, nameStars, toAcctNoStars " +
            //                        "from LOGGER.dbo.VALIDASI_BMI " +
            //                        "where rekapid = '" + DGR.Items[i].Cells[1].Text + "' " +
            //                        "and toAcctNoStars = '" + DGR.Items[i].Cells[3].Text + "' " +
            //                        "and nameStars = '" + DGR.Items[i].Cells[2].Text + "'";
            //            conn.ExecuteQuery();

            //            string fromAcctNo = conn.GetFieldValue("fromAcctNo").ToString();
            //            string toBankCode = conn.GetFieldValue("toBankCode").ToString();
            //            string toAcctNo = conn.GetFieldValue("toAcctNo").ToString();
            //            string transferAmount = conn.GetFieldValue("transferAmount").ToString();
            //            string toAcctName = conn.GetFieldValue("toAcctName").ToString();
            //            string transferDescription = conn.GetFieldValue("transferDescription").ToString();
            //            string fromAcctName = conn.GetFieldValue("fromAcctName").ToString();
            //            string typetransfer = conn.GetFieldValue("typetransfer").ToString();
            //            string nameStars = conn.GetFieldValue("nameStars").ToString();
            //            string toAcctNoStars = conn.GetFieldValue("toAcctNoStars").ToString();

            //            conn.QueryString = "exec SP_API_BMI_TRANSFER " +
            //                    "'" + toBankCode + "'," +
            //                    "'" + fromAcctNo + "'," +
            //                    "'" + toAcctNo + "'," +
            //                    "'" + transferAmount + "'," +
            //                    "'" + transferDescription + "'," +
            //                    "'" + toAcctName + "'," +
            //                    "'" + typetransfer + "'," +
            //                    "'" + fromAcctName + "'," +
            //                    "'" + nameStars + "'," +
            //                    "'" + toAcctNoStars + "'," +
            //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            //            conn.ExecuteQuery(3000);

            //            if (conn.GetRowCount() > 0)
            //            {
            //                //conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
            //                //conn.ExecuteNonQuery();
            //                if (conn.GetFieldValue("errorCode").ToString() == "00")
            //                {
            //                    //conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET status = '00', transactionIDTransfer = '" + conn.GetFieldValue("transactionID").ToString() + "', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
            //                    //conn.ExecuteNonQuery();
            //                }
            //                else
            //                {
            //                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('" + conn.GetFieldValue("errorDesc").ToString() + "')", true);
            //                }
            //            }

            //            //conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET status = '00', transactionIDTransfer = 'yulia test approved', approveby = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', approvedate = GETDATE() WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' and toAcctNoStars = '" + toAcctNoStars + "' and nameStars = '" + nameStars + "'";
            //            //conn.ExecuteNonQuery();

            //        }
            //    }
            //    Response.Redirect("StlPost.aspx?");
            //}
            //catch (System.Exception ex)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            //}
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    if (cb.Checked)
                    {
                        conn.QueryString = "UPDATE LOGGER.dbo.VALIDASI_BMI SET " +
                                            "statusReject = '1', status = '0' " +
                                            "WHERE rekapid = '" + DGR.Items[i].Cells[1].Text + "' " +
                                            "and toAcctNoStars = '" + DGR.Items[i].Cells[3].Text + "' " +
                                            "and nameStars = '" + DGR.Items[i].Cells[2].Text + "'";

                        conn.ExecuteNonQuery();
                    }
                }
                Response.Redirect("StlPost.aspx?");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Inquiry")
            {
                try
                {
                    TextBox txtAccNo = (TextBox)e.Item.FindControl("TXT_ACCNO");
                    DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_ACC_BANK_DESC");
                    CheckBox cb = (CheckBox)e.Item.FindControl("CB");

                    int AMOUNT_TRANSFER = Int32.Parse(e.Item.Cells[21].Text);

                    if (DDL_TRANSFER.SelectedValue == "3" && AMOUNT_TRANSFER > 50000000 && AMOUNT_TRANSFER < 100000000)
                    {
                        AMOUNT_TRANSFER = 10000;
                    }

                    conn.QueryString = "exec SP_API_BMI_INQUIRY " +
                                        "'" + e.Item.Cells[7].Text + "'," +
                                        "'" + e.Item.Cells[19].Text + "'," +
                                        "'" + txtAccNo.Text + "'," +
                                        "'" + AMOUNT_TRANSFER + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(3000);

                    if (conn.GetRowCount() > 0)
                    {
                        if (conn.GetFieldValue("errorCode").ToString() == "00")
                        {
                            e.Item.Cells[10].Text = conn.GetFieldValue("toAccName").ToString();
                            e.Item.Cells[11].Text = conn.GetFieldValue("transactionId").ToString();
                            e.Item.Cells[12].Text = txtAccNo.Text;
                            e.Item.Cells[13].Text = ddl.SelectedItem.Text;
                            cb.Enabled = true;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);
                            e.Item.Cells[10].Text = "";
                            e.Item.Cells[11].Text = "";
                            e.Item.Cells[12].Text = "";
                            e.Item.Cells[13].Text = "";
                            cb.Enabled = false;
                            cb.Checked = false;
                            Recalculate();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
                }


            }
        }

        protected void DDL_ACC_BANK_DESC_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_ACC_BANK_DESC");

                if ((DropDownList)sender == ddl)
                {
                    conn.QueryString = "SELECT ACCBANK = LEFT(CLEARING_CODE, 3), SWIFTCODE = RTGS_CODE FROM PARAM_TBL_BANK WHERE CODE = '" + ddl.SelectedValue + "'";
                    conn.ExecuteQuery(3000);

                    if (conn.GetRowCount() > 0)
                    {
                        DGR.Items[i].Cells[7].Text = conn.GetFieldValue("ACCBANK").ToString();
                        DGR.Items[i].Cells[8].Text = conn.GetFieldValue("SWIFTCODE").ToString();
                    }
                }
            }
        }
    }
}
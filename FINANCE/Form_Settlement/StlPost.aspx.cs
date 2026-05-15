using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlPost : System.Web.UI.Page
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

                Setup();
                DGR.CurrentPageIndex = 0;
                //FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DEFAULTDAY = convert(varchar(20),dateadd(day,-1,GETDATE()),103)";
            conn.ExecuteQuery(150000);
            TXT_DATE1.Text = conn.GetFieldValue("DEFAULTDAY").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("DEFAULTDAY").ToString();

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery(150000);
            DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //conn.QueryString = "select distinct " +
            //                    "a.NOREK_SOURCE, " +
            //                    "b.BANK " +
            //                    "from SETTLEMENT_MASTER a " +
            //                    "inner join REKENING_MASTER b on a.NOREK_SOURCE=b.NOREK " +
            //                    "where a.NOREK_SOURCE is not null " +
            //                    "order by 2";
            conn.QueryString = "select NOREK, BANK from V_REKENING_MASTER where STL = 1 order by 2";
            conn.ExecuteQuery(150000);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLTipe();


            BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");
        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery(150000);
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string start_paid_date = "";
            string end_paid_date = "";

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and a.APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and a.ACC_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
            }

            if (DDL_BANKCONN.SelectedValue != "")
            {
                where = where + " " + DDL_BANKCONN.SelectedValue + " ";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and a.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAPID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.APPROVALDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.APPROVALDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE3.Text.Trim() != "")
            {
                start_paid_date = " and d.ID is not null and convert(date,d.USER_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE3.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE4.Text.Trim() != "")
            {
                end_paid_date = " and convert(date,d.USER_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE4.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_BENEF.Text.Trim() != "")
            {
                where = where + " and a.BENEFICIARY like '%" + TXT_BENEF.Text.Trim() + "%'";
            }

            //conn.QueryString = "select " +
            //                    "a.REKAPID, " +
            //                    "a.ACC_NO, " +
            //                    "a.ACC_BANK, " +
            //                    "a.ACC_NAME, " +
            //                    "a.BENEFICIARY, " +
            //                    "a.TIPE_SETTLEMENT_DESCR, " +
            //                    "APPROVALBY = UPPER(a.APPROVALBY), " +
            //                    "APPROVALDATE = convert(varchar(20),a.APPROVALDATE,106), " +
            //                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
            //                    "CHARGE = replace(convert(varchar(100),convert(money,isnull(b.CHARGE,0)),1),'.00',''), " +
            //                    "TOTAL = replace(convert(varchar(100),convert(money,a.AMOUNT + isnull(b.CHARGE,0)),1),'.00',''), " +
            //                    "a.CNT, " +
            //                    "b.PAYMENT_METHOD " +
            //                    "from V_SETTLEMENT_MASTER_REKAP a " +
            //                    "left join SETTLEMENT_MASTER_BANK_CHARGE b on a.REKAPID = b.REKAPID and a.ACC_NO = b.ACC_NO and a.ACC_NAME = b.ACC_NAME collate database_default and a.ACC_BANK_CODE = b.ACC_BANK collate database_default " +
            //                    "where " +
            //                    "ACC_SOURCE is not null " + where +
            //                    "order by " +
            //                    "a.APPROVALDATE, " +
            //                    "a.REKAPID";
            conn.QueryString = "exec SP_STL_POST " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE3.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE4.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + TXT_BENEF.Text.Trim() + "'," +
                                "'" + TXT_ID.Text.Trim() + "'," +
                                "'" + DDL_TIPE.SelectedValue + "'," +
                                "'" + DDL_ACCSOURCE.SelectedValue + "'," +
                                "'" + DDL_APP.SelectedValue + "'," +
                                "'" + DDL_BANKCONN.SelectedValue + "'," +
                                "'" + DDL_VALIDATION.SelectedValue + "'";
            conn.ExecuteQuery(150000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            //conn.QueryString = "select " +
            //                    "TOTAL = replace(convert(varchar(100),convert(money,SUM(a.AMOUNT + isnull(b.CHARGE,0))),1),'.00','') " +
            //                    "from V_SETTLEMENT_MASTER_REKAP a " +
            //                    "left join SETTLEMENT_MASTER_BANK_CHARGE b on a.REKAPID = b.REKAPID and a.ACC_NO = b.ACC_NO and a.ACC_NAME = b.ACC_NAME collate database_default and a.ACC_BANK_CODE = b.ACC_BANK collate database_default " +
            //                    "where " +
            //                    "ACC_SOURCE is not null " + where;
            conn.QueryString = "exec SP_STL_POST_TOTAL " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE3.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DATE4.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + TXT_BENEF.Text.Trim() + "'," +
                                "'" + TXT_ID.Text.Trim() + "'," +
                                "'" + DDL_TIPE.SelectedValue + "'," +
                                "'" + DDL_ACCSOURCE.SelectedValue + "'," +
                                "'" + DDL_APP.SelectedValue + "'," +
                                "'" + DDL_BANKCONN.SelectedValue + "'," +
                                "'" + DDL_VALIDATION.SelectedValue + "'";
            conn.ExecuteQuery(150000);

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + LB_RESULT.Text + "</TD></TR>" +
                                "<TR><TD>Total Amount</TD><TD>:</TD><TD>" + conn.GetFieldValue("TOTAL").ToString() + "</TD></TR>" +
                                "</TABLE>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                Button btIB = (Button)DGR.Items[i].FindControl("BT_FILE");
                Button btV = (Button)DGR.Items[i].FindControl("BT_VALIDASI");
                Button btTL = (Button)DGR.Items[i].FindControl("BT_SLIP");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                TextBox txtCHARGE = (TextBox)DGR.Items[i].FindControl("TXT_CHARGE");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                txtCHARGE.Text = DGR.Items[i].Cells[4].Text;

                btTL.Visible = false;
                if (DGR.Items[i].Cells[3].Text == "TL")
                {
                    cb.Visible = false;
                    btTL.Visible = true;
                }

                conn.QueryString = "exec SP_API_BMI_VALIDASI " +
                                "'" + DDL_ACCSOURCE.SelectedValue + "'," +
                                "'" + DGR.Items[i].Cells[2].Text + "'";
                conn.ExecuteQuery(1000);

                string
                status = conn.GetFieldValue("status").ToString();

                if (status == "OFF")
                {
                    btV.Visible = false;
                }

                lbREKAPID.Attributes.Add("onclick", "displayFloatingDiv('divDETAIL', 'DETAIL', 400, 200, 100, 50);");
                //lbREKAPID.Attributes.Add("onclick", "window.open('StlPostDetail.aspx','DETAIL','height=300px,width=500px,left=0,top=0,border=0,resizable=no,status=no,toolbar=no,scrollbars=no,fullscreen = { yes | no | 1 | 0 },menubar=no,location=no,dependent=yes');");
                //lbREKAPID.Attributes.Add("onclick", "<script language='javascript'>window.showModalDialog('StlPostDetail.aspx',dialogWidth:400px;dialogHeight: 200px;center: yes;resize: no;status: no;help:no');</Script>");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGrid dgrDETAIL = (DataGrid)e.Item.FindControl("DGR_DETAIL");
            Button btDETAIL = (Button)e.Item.FindControl("BT_DETAIL");
            Button btFILE = (Button)e.Item.FindControl("BT_FILE");
            Button btSLIP = (Button)e.Item.FindControl("BT_SLIP");
            Button btV = (Button)e.Item.FindControl("BT_VALIDASI");
            Button btRetur = (Button)e.Item.FindControl("BT_RETUR");


            if (e.CommandName == "Settle")
            {
                Response.Redirect("Payment_Settle.aspx?MODE=PAY&CODE=" + e.Item.Cells[2].Text + "&AMOUNT=" + e.Item.Cells[14].Text);
            }

            if (e.CommandName == "Validasi")
            {
                conn.QueryString = "select " +
                                    "rekapid, status, toAcctNo " +
                                    "from LOGGER.dbo.VALIDASI_BMI where rekapid = '" + e.Item.Cells[2].Text + "' and toAcctNoStars = '" + e.Item.Cells[6].Text + "' and nameStars = '" + e.Item.Cells[7].Text + "'";
                conn.ExecuteQuery(150000);
                string
                status = conn.GetFieldValue("STATUS").ToString();
                string
                rekapid = conn.GetFieldValue("rekapid").ToString();
                string
                toAcctNo = conn.GetFieldValue("toAcctNo").ToString();

                if (status == "00")
                {
                    btV.Attributes.Add("onclick", "alert('Transfer ke Rekening " + toAcctNo + ", Dengan Nomor RekapID : " + rekapid + " Berhasil');");
                }
                else
                {
                    Response.Redirect("Validasi_BMI.aspx?MODE=PAY&CODE=" + e.Item.Cells[2].Text + "&ACC_NO=" + e.Item.Cells[6].Text + "&ACC_NAME=" + e.Item.Cells[7].Text);
                }
            }

            if (e.CommandName == "Retur")
            {
                conn.QueryString = "SELECT * FROM SECURITY..MENU_ROLE WHERE ROLE_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "' AND MENU_CODE = '943'";
                conn.ExecuteQuery(150000);

                if (conn.GetFieldValue("AUTH_TYPE").ToString() == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User tidak memiliki hak akses')", true);
                    return;
                }

                ToRetur();
            }

            if (e.CommandName == "IBALL")
            {
                if (DDL_VALIDATION.SelectedValue == "UNREGISTERED")
                {
                    string
                    AMOUNT = "", CNT = "";
                    int
                        count = 0;
                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                        if (cb.Checked)
                        {
                            count++;
                            AMOUNT = AMOUNT + "+" + DGR.Items[i].Cells[14].Text;

                        }
                    }
                    AMOUNT = AMOUNT.Replace(",", "");

                    conn.QueryString = "SELECT AMOUNT = replace(convert(varchar(100),convert(money, 0" + AMOUNT + "),1),'.00','')";
                    conn.ExecuteQuery(150000);
                    CNT = conn.GetFieldValue("AMOUNT").ToString();

                    TXT_DATA.Text = count.ToString();
                    TXT_NOMINAL.Text = CNT;

                    ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlib').style.display = 'block';", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please select validation status UNREGISTERED!')", true);
                }
            }

            if (e.CommandName == "IB")
            {
                /*
                if (e.Item.Cells[15].Text.Replace("&nbsp;", "") != "")
                    Response.Redirect(e.Item.Cells[15].Text.Replace("&nbsp;", ""));
                else
                    ToCSV(e.Item.Cells[2].Text);
                */
            }

            if (e.CommandName == "TL")
            {

            }

            if (e.CommandName == "Detail")
            {
                ShowDetail(e.Item.Cells[2].Text, e.Item.Cells[6].Text, e.Item.Cells[7].Text, e.Item.Cells[8].Text, e.Item.Cells[10].Text, 0);
            }

            if (e.CommandName == "SaveCharge")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtCHARGE = (TextBox)DGR.Items[i].FindControl("TXT_CHARGE");

                    try
                    {
                        conn.QueryString = "update SETTLEMENT_MASTER_BANK_CHARGE set " +
                                            "CHARGE = " + txtCHARGE.Text.Trim().Replace(",", "") + " " +
                                            "where " +
                                            "REKAPID = '" + DGR.Items[i].Cells[2].Text + "' " +
                                            "and ACC_NO = '" + DGR.Items[i].Cells[6].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                FillDGR();
            }

            if (e.CommandName == "ValidateBatch")
            {
                //conn.QueryString = "SELECT * FROM SECURITY..MENU_ROLE WHERE ROLE_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "' AND MENU_CODE = '943'";
                //conn.ExecuteQuery(150000);

                //if (conn.GetFieldValue("AUTH_TYPE").ToString() == "0")
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User tidak memiliki hak akses')", true);
                //    return;
                //}

                ToValidate();
            }
        }

        protected void ShowDetail(string rekapid, string accno, string accname, string accbank, string type, int pageindex)
        {
            LB_DOCNO.Text = rekapid;
            LB_ACCNO.Text = accno;
            LB_ACCNAME.Text = accname;
            LB_ACCBANK.Text = accbank;
            LB_TRANSTYPE.Text = type;

            DGR_DETAIL.CurrentPageIndex = pageindex;
            FillDGRDetail();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {

        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec SP_TRANSFER_CMS '" + DGR.Items[i].Cells[2].Text + "', '" + DGR.Items[i].Cells[6].Text + "', '" + DGR.Items[i].Cells[7].Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(150000);
                }
            }

            FillDGR();
        }

        protected void FillDGRDetail()
        {
            conn.QueryString = "select " +
                                    "NBR = ROW_NUMBER() over (order by a.USERDATE), " +
                                    "a.DOCNO, " +
                                    "a.RESERVED_PARAM1, " +
                                    "a.DESCR, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00','') " +
                                    "from SETTLEMENT_DETAIL a " +
                                    "where " +
                                    "a.REKAPID = '" + LB_DOCNO.Text + "' " +
                                    "and a.ACC_NO = '" + LB_ACCNO.Text + "' " +
                                    "order by a.USERDATE";
            conn.ExecuteQuery(150000);
            DGR_DETAIL.DataSource = conn.GetDataTable().Copy();
            DGR_DETAIL.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_ACCSOURCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void DDL_BANKCONN_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Visible)
                    cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_DETAIL_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ShowDetail(LB_DOCNO.Text, LB_ACCNO.Text, LB_ACCNAME.Text, LB_ACCBANK.Text, LB_TRANSTYPE.Text, e.NewPageIndex);
        }

        protected void FillDGRRetur(string IDGROUP)
        {
            conn.QueryString = "select RETUR_ID, RETUR_ID_GROUP, REKAP_ID, ACC_NO, ACC_NAME, AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00','') " +
                                " from RETUR_MASTER where RETUR_ID_GROUP = '" + IDGROUP + "'";
            conn.ExecuteQuery(150000);
            DGR_RETUR.DataSource = conn.GetDataTable().Copy();
            DGR_RETUR.DataBind();

            for (int i = 0; i < DGR_RETUR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RETUR.Items[i].FindControl("DDL_REASON");

                conn.QueryString = "SELECT CODE, DESCR FROM PR_REASON_RETUR ORDER BY CODE";
                conn.ExecuteQuery(150000);
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
            }
        }

        protected void DGR_RETUR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                ToUpdateRetur();
            }
        }

        protected void ToUpdateRetur()
        {
            for (int i = 0; i < DGR_RETUR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RETUR.Items[i].FindControl("DDL_REASON");

                if (ddl.SelectedValue != "0")
                {
                    conn.QueryString = "UPDATE RETUR_MASTER " +
                    "SET REASON = '" + ddl.SelectedValue + "' " +
                    "WHERE RETUR_ID = '" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery(150000);

                    //conn.QueryString = "exec SP_SEND_EMAIL_RETUR " +
                    //            "'" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    //conn.ExecuteQuery(1000);

                    FillDGR();
                }
                else
                {
                    conn.QueryString = "DELETE FROM RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery(150000);

                    Response.Write("<script>alert('REKAP_ID " + DGR_RETUR.Items[i].Cells[2].Text + " BELUM DI ISI');</script>");

                    FillDGR();
                }
            }

        }

        protected void DGR_RETUR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ToRetur();
        }

        protected void ToRetur()
        {
            var guid = Guid.NewGuid().ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec SP_RETUR '" + DGR.Items[i].Cells[2].Text + "', '" + DGR.Items[i].Cells[6].Text + "', '" + DGR.Items[i].Cells[7].Text + "', '" + DGR.Items[i].Cells[14].Text + "', NULL, '-', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', 'OTOMATIS', '" + guid + "',''";
                    conn.ExecuteQuery(150000);
                }
            }

            IDGROUP.Text = conn.GetFieldValue("IDGROUP").ToString();

            DGR_RETUR.CurrentPageIndex = 0;
            FillDGRRetur(IDGROUP.Text);

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('ReturPopup').style.display = 'block';", true);
        }
        //ANDEZ

        protected void ToCMS()
        {
            string
            AMOUNT = "", CNT = "";
            int
                count = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec SP_TRANSFER_CMS '" + DGR.Items[i].Cells[2].Text + "', '" + DGR.Items[i].Cells[6].Text + "', '" + DGR.Items[i].Cells[7].Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(150000);

                    count++;
                    AMOUNT = AMOUNT + "+" + DGR.Items[i].Cells[14].Text;

                }
            }
            AMOUNT = AMOUNT.Replace(",", "");

            conn.QueryString = "SELECT AMOUNT = replace(convert(varchar(100),convert(money, 0" + AMOUNT + "),'.00','')";
            conn.ExecuteQuery(150000);
            CNT = conn.GetFieldValue("AMOUNT").ToString();

            FillDGR();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Jumlah Data: " + count + " ; Jumlah Nominal : " + CNT + "')", true);
        }

        protected void ToCSVALL()
        {
            string
            REKAPID = "",
            ACC_SOURCE = DDL_ACCSOURCE.SelectedValue;

            bool bFound = false;

            var result = new StringBuilder();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    REKAPID = REKAPID + DGR.Items[i].Cells[2].Text + DGR.Items[i].Cells[6].Text + ",";
                    bFound = true;
                }
            }

            if (!bFound)
                return;

            int trxcount = 0;
            decimal trxtotal = 0;

            conn.QueryString = "exec RPT_INTERNET_BANKING_HEX '" + REKAPID + "','" + ACC_SOURCE + "'";
            conn.ExecuteQuery(150000);

            foreach (DataRow row in conn.GetDataTable().Copy().Rows)
            {
                for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                {
                    var rowValue = row[i].ToString();
                    result.Append(rowValue.Replace("\\r\\n", "\r\n"));
                    result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            for (int j = 0; j < DGR.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR.Items[j].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec RPT_INTERNET_BANKING_HDEX '" + DGR.Items[j].Cells[2].Text + DGR.Items[j].Cells[6].Text + "','" + DDL_ACCSOURCE.SelectedValue + "','D'";
                    conn.ExecuteQuery(150000);

                    foreach (DataRow row in conn.GetDataTable().Copy().Rows)
                    {
                        for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                        {
                            var rowValue = row[i].ToString();
                            result.Append(rowValue);
                            result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                        }
                        trxcount++;
                        try
                        {
                            trxtotal = trxtotal + System.Convert.ToDecimal(row[38].ToString());
                        }
                        catch { }
                    }
                }
            }

            conn.QueryString = "exec RPT_INTERNET_BANKING_HDEX_FOOTER '" + REKAPID + "','" + ACC_SOURCE + "'," + trxcount.ToString() + "," + trxtotal.ToString();
            conn.ExecuteQuery(150000);

            foreach (DataRow row in conn.GetDataTable().Copy().Rows)
            {
                for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                {
                    var rowValue = row[i].ToString();
                    result.Append(rowValue.Replace("\\r\\n", "\r\n"));
                    result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            if (result != null)
            {
                conn.QueryString = "select convert(varchar(20),GETDATE(),112)";
                conn.ExecuteQuery(150000);

                this.Response.Clear();
                this.Response.Buffer = true;

                this.Response.AddHeader("content-disposition", "attachment;filename=" + DDL_ACCSOURCE.SelectedItem.Text.Replace(" ", "") + "_" + conn.GetFieldValue(0, 0).ToString() + "_" + DDL_BANKCONN.SelectedItem.Text + ".csv");
                this.Response.Charset = "";
                this.Response.ContentType = "application/text";
                this.Response.Output.Write(result.ToString());
                this.Response.Flush();
                this.Response.End();
            }
        }

        protected void DDL_VALIDATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void ToValidate()
        {
            if (DDL_VALIDATION.SelectedValue != "")
            {

                //string Code = "";
                //string AccNo = "";
                //string AccName = "";
                //int j = 0;

                //ANDEZ CR 100 DATA H2H START
                var guid = Guid.NewGuid().ToString();

                conn.QueryString = "DELETE FROM BATCH_TEMPORARY_H2H WHERE PROCESSBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(150000);
                //ANDEZ CR 100 DATA H2H END

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        //ANDEZ CR 100 DATA H2H START
                        conn.QueryString = "INSERT INTO BATCH_TEMPORARY_H2H SELECT BATCH_ID = '" + guid + "', REKAPID = '" + DGR.Items[i].Cells[2].Text + "', ACC_NO = '" + DGR.Items[i].Cells[6].Text + "', ACC_NAME = '" + DGR.Items[i].Cells[7].Text + "', PROCESSBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', PROCESSDATE = GETDATE()";
                        conn.ExecuteQuery(150000);
                        //ANDEZ CR 100 DATA H2H END

                        //Code += (j == 0) ? DGR.Items[i].Cells[2].Text : ";" + DGR.Items[i].Cells[2].Text;
                        //AccNo += (j == 0) ? DGR.Items[i].Cells[6].Text : ";" + DGR.Items[i].Cells[6].Text;
                        //AccName += (j == 0) ? DGR.Items[i].Cells[7].Text : ";" + DGR.Items[i].Cells[7].Text;

                        //j++;
                    }
                }
                DDL_VALIDATION.Visible = true;
                //Response.Redirect("Validasi_BMI_Batch.aspx?MODE=PAY&CODE=" + Code + "&ACC_NO=" + AccNo + "&ACC_NAME=" + AccName + "&INHOUSE=" + DDL_BANKCONN.SelectedValue + "&VALIDATION=" + DDL_VALIDATION.SelectedValue);

                //ANDEZ CR 100 DATA H2H START
                Response.Redirect("Validasi_BMI_Batch.aspx?MODE=PAY&CODE=" + guid + "&ACC_NO=&ACC_NAME=&INHOUSE=" + DDL_BANKCONN.SelectedValue + "&VALIDATION=" + DDL_VALIDATION.SelectedValue);
                //ANDEZ CR 100 DATA H2H END
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please select validation status!')", true);

            }
        }
    }
}
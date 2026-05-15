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
    public partial class Retur : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DEFAULTDAY = convert(varchar(20),dateadd(day,-1,GETDATE()),103)";
            conn.ExecuteQuery();

            conn.QueryString = "SELECT ID = 1, DESCR = 'PENDING' " +
                                    "UNION " +
                                    "SELECT ID = 2, DESCR = 'RETUR' " +
                                    "UNION " +
                                    "SELECT ID = 3, DESCR = 'READY TO PAID' " +
                                    "UNION " +
                                    "SELECT ID = 4, DESCR = 'PAID'";

            conn.ExecuteQuery();
            DDL_STATUS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select NOREK, BANK from V_REKENING_MASTER where STL = 1 order by 2";
            conn.ExecuteQuery();
            DDL_ACCSOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReturDate();
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillReturDate()
        {
            TXT_DATE1.Text = string.Empty;
            TXT_DATE2.Text = string.Empty;
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and c.APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and c.NOREK_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
            }

            if (DDL_STATUS.SelectedValue != "")
            {
                where = where + " and a.STATUS='" + DDL_STATUS.SelectedValue + "'";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and c.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAP_ID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DESTACC.Text.Trim() != "")
            {
                where = where + " and (a.ACC_NO collate database_default + ' - ' + d.BANK collate database_default + ' - ' + a.ACC_NAME collate database_default) like '%" + TXT_DESTACC.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts2.USER_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts2.USER_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE3.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts3.USER_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE3.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE4.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts3.USER_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE4.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            conn.QueryString = "SELECT ACCESS = CASE WHEN ROLE_CODE IN ('36', '15', '35', '39', '0', '37', '13') THEN '1' ELSE '0' END FROM SECURITY..M_USERS WHERE CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("ACCESS").ToString() != "1")
            {
                where = where + " and a.APPROVALBY IN (SELECT DISTINCT c.APPROVALBY FROM SECURITY.dbo.M_USERS a " +
									"INNER JOIN SECURITY.dbo.EMAIL_ROLE b ON b.ROLE_CODE = a.ROLE_CODE " +
                                    "INNER JOIN (SELECT a.APPROVALBY, c.EMAIL FROM RETUR_MASTER a " +
									"INNER JOIN SECURITY.dbo.M_USERS b ON b.CODE = a.APPROVALBY COLLATE DATABASE_DEFAULT " +
									"INNER JOIN SECURITY.dbo.EMAIL_ROLE c ON c.ROLE_CODE = b.ROLE_CODE) c ON c.EMAIL = b.EMAIL " +
									"WHERE a.CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "')";
            }

            conn.QueryString = "SELECT " +
                                    "a.RETUR_ID, a.REKAP_ID, a.ACC_NO, a.ACC_NAME, ACC_BANK = d.BANK, ACC_BANK_CODE = a.ACC_BANK, ACC_BANK_CODE_BARU = a.ACC_BANK_BARU, a.ACC_NO_BARU, a.ACC_NAME_BARU,  " +
	                                "ACC_BANK_BARU = db.BANK, " +
                                    "TIPE_CODE = a.TIPE, " +
	                                "TIPE = CASE WHEN a.TIPE = 'OTOMATIS' THEN 'RET OTOM (RETUR FROM EXECUTE)' ELSE 'RET SISTEM (RETUR FROM FLAGING)' END, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''),  " +
                                    "AMOUNT_F = a.AMOUNT," +
	                                "a.BENEFICIARY,  " +
	                                "a.TRANS_TYPE,  " +
	                                "REASON = b.DESCR, " +
	                                "STATUS = CASE WHEN a.STATUS = 2 THEN 'RETUR' WHEN a.STATUS = 1 THEN 'PENDING' WHEN a.STATUS = 3 THEN 'READY TO PAID' ELSE 'PAID' END,  " +
	                                "STATUS_BY = a.USER_BY, " +
                                    "STATUS_DATE = convert(varchar, a.USER_DATE, 103), " +
                                    "RETUR_DATE = case when a.RETUR_DATE is null then convert(varchar, sts2.USER_DATE, 103) else convert(varchar, a.RETUR_DATE, 103) end, " +
                                    "RETUR_BY = case when a.RETUR_DATE is null then sts2.USER_BY else 'SYSTEM' end, " +
                                    "PAID_DATE = convert(varchar, sts4.USER_DATE, 103), " +
                                    "PAID_BY = sts4.USER_BY, " +
                                    "AGING = (case when sts4.USER_BY is null then datediff(dd, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end), GETDATE()) - (select count(HOLIDAY) from SECURITY.dbo.PARAM_HOLIDAY where convert(date,HOLIDAY) between convert(date, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end)) and convert(date,GETDATE())) " +
                                            "else datediff(dd, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end), sts4.USER_DATE) - (select count(HOLIDAY) from SECURITY.dbo.PARAM_HOLIDAY where convert(date,HOLIDAY) between convert(date, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end)) and convert(date,sts4.USER_DATE)) end) " +
                                "FROM (SELECT a.*, b.STATUS, c.USER_DATE, c.USER_BY FROM RETUR_MASTER a " +
                                        "INNER JOIN ( select a.RETUR_ID, STATUS = MAX(a.STATUS) from TRACK_RETUR_MASTER a group by a.RETUR_ID ) b ON a.RETUR_ID = b.RETUR_ID " +
                                        "INNER JOIN TRACK_RETUR_MASTER c ON c.STATUS = b.STATUS AND c.RETUR_ID = b.RETUR_ID) a " +
                                "INNER JOIN PARAM_TBL_BANK d on a.ACC_BANK = d.CODE collate database_default " +
                                "INNER JOIN SETTLEMENT_MASTER c on c.REKAPID = a.REKAP_ID COLLATE DATABASE_DEFAULT " +
                                "INNER JOIN PR_REASON_RETUR b ON b.CODE = a.REASON " +
                                "LEFT JOIN PARAM_TBL_BANK db on a.ACC_BANK_BARU = db.CODE collate database_default " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts2 ON sts2.RETUR_ID = a.RETUR_ID AND sts2.STATUS = 2 " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts3 ON sts3.RETUR_ID = a.RETUR_ID AND sts3.STATUS = 3 " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts4 ON sts4.RETUR_ID = a.RETUR_ID AND sts4.STATUS = 4 " +
                                "WHERE 1 = 1 " + where +
                                "ORDER BY a.STATUS";
            conn.ExecuteQuery(1000);

            if (conn.GetRowCount() > 0)
                BT_XLS.Visible = true;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + LB_RESULT.Text + "</TD></TR>" +
                                "</TABLE>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                Button btRetur = (Button)DGR.Items[i].FindControl("BT_RETUR");
                Button btSave = (Button)DGR.Items[i].FindControl("BT_SAVE");
                Button btPending = (Button)DGR.Items[i].FindControl("BT_PENDING");
                Button btRollback = (Button)DGR.Items[i].FindControl("BT_ROLLBACK");
                TextBox txtACCNO = (TextBox)DGR.Items[i].FindControl("TXT_ACC_NO");
                Label lbACCNO = (Label)DGR.Items[i].FindControl("LB_ACC_NO");
                TextBox txtACCNAME = (TextBox)DGR.Items[i].FindControl("TXT_ACC_NAME");
                Label lbACCNAME = (Label)DGR.Items[i].FindControl("LB_ACC_NAME");
                Label lbACCBANK = (Label)DGR.Items[i].FindControl("LB_ACC_BANK");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;

                DropDownList ddlBank = (DropDownList)DGR.Items[i].FindControl("DDL_ACC_BANK");
                conn.QueryString = "SELECT CODE, BANK FROM PARAM_TBL_BANK WHERE CODE NOT IN ('X', '65', '196') UNION SELECT CODE = '', BANK = '' ORDER BY BANK";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlBank.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                }

                try
                {
                    ddlBank.SelectedValue = DGR.Items[i].Cells[6].Text;
                }
                catch { }

                lbACCNO.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtACCNO.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                lbACCNAME.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtACCNAME.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                lbACCBANK.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");

                conn.QueryString = "SELECT ACCESS = CASE WHEN ROLE_CODE IN ('36', '15', '35', '39', '0', '37') THEN '1' ELSE '0' END FROM SECURITY..M_USERS WHERE CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("ACCESS").ToString() == "1")
                {
                    txtACCNO.Visible = false;
                    txtACCNAME.Visible = false;
                    ddlBank.Visible = false;
                    if (DGR.Items[i].Cells[19].Text == "PENDING")
                    {
                        btPending.Visible = true;
                        btSave.Visible = true;
                        btRetur.Visible = false;
                    }
                    else if (DGR.Items[i].Cells[19].Text == "RETUR")
                    {
                        btRetur.Visible = false;
                        btSave.Visible = false;
                        btPending.Visible = false;
                    }
                    else if (DGR.Items[i].Cells[19].Text == "READY TO PAID")
                    {
                        btRollback.Visible = true;
                        btRetur.Visible = false;
                        btSave.Visible = false;
                        btPending.Visible = false;
                    }
                    else
                    {
                        btSave.Visible = false;
                        btRetur.Visible = false;
                        btPending.Visible = false;
                    }
                }
                else
                {
                    if (DGR.Items[i].Cells[19].Text == "PENDING")
                    {
                        txtACCNO.Visible = false;
                        txtACCNAME.Visible = false;
                        ddlBank.Visible = false;
                        btPending.Visible = false;
                        btSave.Visible = false;
                        btRetur.Visible = false;
                    }
                    else if (DGR.Items[i].Cells[19].Text == "RETUR")
                    {

                        lbACCNO.Visible = false;
                        lbACCNAME.Visible = false;
                        lbACCBANK.Visible = false;
                        btRetur.Visible = true;
                        btSave.Visible = false;
                        btPending.Visible = false;
                    }
                    else
                    {
                        txtACCNO.Visible = false;
                        txtACCNAME.Visible = false;
                        ddlBank.Visible = false;
                        btSave.Visible = false;
                        btRetur.Visible = false;
                        btPending.Visible = false;
                    }
                }

                
                
                
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Button btPending = (Button)e.Item.FindControl("BT_PENDING");
            Button btSave = (Button)e.Item.FindControl("BT_SAVE");
            Button btRetur = (Button)e.Item.FindControl("BT_RETUR");
            Button btRollback = (Button)e.Item.FindControl("BT_ROLLBACK");

            if (e.CommandName == "Rollback")
            {
                conn.QueryString = "DELETE FROM TRACK_RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + e.Item.Cells[1].Text + "' AND STATUS = 3";
                conn.ExecuteQuery();

                conn.QueryString = "UPDATE SETTLEMENT_MASTER SET PROCESSBY = 'retur' " +
                    "WHERE REKAPID = '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteQuery();

                FillDGR();
            }

            if (e.CommandName == "Pending")
            {
                conn.QueryString = "DELETE FROM RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();

                conn.QueryString = "DELETE FROM TRACK_RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();

                FillDGR();
            }

            if (e.CommandName == "Retur")
            {
                TextBox txtACCNO = (TextBox)e.Item.FindControl("TXT_ACC_NO");
                TextBox txtACCNAME = (TextBox)e.Item.FindControl("TXT_ACC_NAME");
                DropDownList ddlBank = (DropDownList)e.Item.FindControl("DDL_ACC_BANK");

                if (txtACCNO.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Account Number tidak boleh kosong')", true);
                }
                else if (txtACCNAME.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Account Name tidak boleh kosong')", true);
                }
                else if (ddlBank.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Account Bank tidak boleh kosong')", true);
                }
                else
                {

                    conn.QueryString = "UPDATE SETTLEMENT_MASTER " +
                                        "SET PROCESSBY = null, NOREK_SOURCE = null " +
                                        "WHERE REKAPID = '" + e.Item.Cells[2].Text + "'";
                    conn.ExecuteQuery();

                    if (e.Item.Cells[11].Text == "" || e.Item.Cells[11].Text  == "&nbsp;")
                    {
                        conn.QueryString = "UPDATE SETTLEMENT_DETAIL " +
                                        "SET ACC_NO = '" + txtACCNO.Text.Trim() + "' " +
                                        ", ACC_BANK = '" + ddlBank.SelectedValue + "' " +
                                        ", ACC_NAME = '" + txtACCNAME.Text.Trim() + "' " +
                                        "WHERE REKAPID = '" + e.Item.Cells[2].Text + "' and ACC_NO = '" + e.Item.Cells[4].Text + "' and ACC_NAME = '" + e.Item.Cells[5].Text + "' and ACC_BANK = '" + e.Item.Cells[7].Text + "'";
                        conn.ExecuteQuery();
                    }
                    else
                    {

                        conn.QueryString = "UPDATE SETTLEMENT_DETAIL " +
                                        "SET ACC_NO = '" + txtACCNO.Text.Trim() + "' " +
                                        ", ACC_BANK = '" + ddlBank.SelectedValue + "' " +
                                        ", ACC_NAME = '" + txtACCNAME.Text.Trim() + "' " +
                                        "WHERE REKAPID = '" + e.Item.Cells[2].Text + "' and ACC_NO = '" + e.Item.Cells[11].Text + "' and ACC_NAME = '" + e.Item.Cells[12].Text + "' and ACC_BANK = '" + e.Item.Cells[28].Text + "'";
                        conn.ExecuteQuery();
                    }

                    conn.QueryString = "UPDATE RETUR_MASTER " +
                                        "SET ACC_NO_BARU = '" + txtACCNO.Text.Trim() + "' " +
                                        ", ACC_NAME_BARU = '" + txtACCNAME.Text.Trim() + "' " +
                                        ", ACC_BANK_BARU = '" + ddlBank.SelectedValue + "' " +
                                        "WHERE RETUR_ID = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteQuery();

                    conn.QueryString = "SP_TRACK_RETUR_MASTER_UPSERT '" + e.Item.Cells[1].Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', 3";
                    conn.ExecuteQuery();

                    conn.QueryString = "exec SP_SEND_EMAIL_NOTIFIKASI_RETUR " +
                                "'" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteQuery(1000);

                    FillDGR();
                }
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "SP_TRACK_RETUR_MASTER_UPSERT '" + e.Item.Cells[1].Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', 2";
                    conn.ExecuteQuery();

                    if (e.Item.Cells[27].Text == "MANUAL")
                    {
                        conn.QueryString = "insert into SETTLEMENT_DETAIL_RETUR " +
                                            "select a.STL_TRXID, a.REKAPID, a.APP_ID, a.TIPE_SETTLEMENT, a.CUSTOMER_CODE, a.AMOUNT, a.DOCNO, a.STL_DESCR, a.BENEFICIARY, a.ACC_NO, a.BANK, a.NOREK, a.TRXID, a.POST_DATE, a.RK_DESCR " +
                                            "from V_SETTLEMENT_DETAIL_PAID a " +
                                            "WHERE REKAPID = '" + e.Item.Cells[2].Text + "' and ACC_NO = '" + e.Item.Cells[4].Text + "'";
                        conn.ExecuteQuery();

                        conn.QueryString = "insert into SETTLEMENT_DETAIL_RETUR " +
                                                "select a.STL_TRXID, a.REKAPID, a.APP_ID, a.TIPE_SETTLEMENT, a.CUSTOMER_CODE, AMOUNT = - a.AMOUNT, a.DOCNO, a.STL_DESCR, a.BENEFICIARY, a.ACC_NO, a.BANK, a.NOREK, a.TRXID, POST_DATE = c.RETUR_DATE, RK_DESCR = b.DESCR " +
                                                "from RETUR_MASTER c " +
                                                "inner join V_SETTLEMENT_DETAIL_PAID a on c.REKAP_ID = a.REKAPID COLLATE DATABASE_DEFAULT and c.ACC_NO = a.ACC_NO COLLATE DATABASE_DEFAULT " +
                                                "inner join REKENING_JURNAL b on b.DESCR COLLATE DATABASE_DEFAULT like '%'+a.REKAPID+'%' and b.DEBET = 0 and b.CREDIT = c.AMOUNT " +
                                                "WHERE c.REKAP_ID = '" + e.Item.Cells[2].Text + "' and c.ACC_NO = '" + e.Item.Cells[4].Text + "'";
                        conn.ExecuteQuery();
                    }

                    conn.QueryString = "exec SP_SEND_EMAIL_RETUR " +
                                "'" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteQuery(1000);

                    conn.QueryString = "update SETTLEMENT_MASTER_BANK_CHARGE set ACC_NO = '" + e.Item.Cells[4].Text + "-R' where REKAPID = '" + e.Item.Cells[2].Text + "' and ACC_NO = '" + e.Item.Cells[4].Text + "' and ACC_BANK = '" + e.Item.Cells[7].Text + "' and ACC_NAME = '" + e.Item.Cells[5].Text + "'";

                    conn.ExecuteQuery();

                    
                }
                catch {
                    
                }
                FillDGR();
            }

            if (e.CommandName == "Detail")
            {
                ShowDetail(e.Item.Cells[1].Text, e.Item.Cells[2].Text, 0);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_ACCSOURCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void ShowDetail(string returid, string rekapid, int pageindex)
        {
            LB_RETURID.Text = returid;
            LB_DOCNO.Text = rekapid;

            DGR_DETAIL.CurrentPageIndex = pageindex;
            FillDGRDetail();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void FillDGRDetail()
        {
            conn.QueryString = "SELECT " +
                                    "a.RETUR_ID, " +
                                    "a.REKAP_ID, " +
                                    "a.ACC_NO, " +
                                    "a.ACC_NAME, " +
                                    "ACC_BANK = d.BANK, " +
                                    "a.ACC_NO_BARU, " +
                                    "a.ACC_NAME_BARU, " +
                                    "ACC_BANK_BARU = db.BANK, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                    "a.BENEFICIARY, " +
                                    "a.TRANS_TYPE, " +
                                    "REASON = b.DESCR, " +
                                    "TIPE = CASE WHEN a.TIPE = 'OTOMATIS' THEN 'RET OTOM (RETUR FROM EXECUTE)' ELSE 'RET SISTEM (RETUR FROM FLAGING)' END, " +
                                    "STATUS = CASE WHEN e.STATUS = 2 THEN 'RETUR' WHEN e.STATUS = 1 THEN 'PENDING' WHEN e.STATUS = 3 THEN 'READY TO PAID' ELSE 'PAID' END, " +
                                    "e.USER_BY, " +
                                    "e.USER_DATE " +

                                "FROM (SELECT a.*, b.STATUS, c.USER_DATE, c.USER_BY FROM RETUR_MASTER a " +
                                        "INNER JOIN ( select a.RETUR_ID, STATUS = MAX(a.STATUS) from TRACK_RETUR_MASTER a group by a.RETUR_ID ) b ON a.RETUR_ID = b.RETUR_ID " +
                                        "INNER JOIN TRACK_RETUR_MASTER c ON c.STATUS = b.STATUS AND c.RETUR_ID = b.RETUR_ID) a " +
                                "INNER JOIN PARAM_TBL_BANK d on a.ACC_BANK = d.CODE collate database_default " +
                                "INNER JOIN SETTLEMENT_MASTER c on c.REKAPID = a.REKAP_ID COLLATE DATABASE_DEFAULT " +
                                "INNER JOIN PR_REASON_RETUR b ON b.CODE = a.REASON " +
                                "INNER JOIN TRACK_RETUR_MASTER e ON e.RETUR_ID = a.RETUR_ID " +
                                "LEFT JOIN PARAM_TBL_BANK db on a.ACC_BANK_BARU = db.CODE collate database_default " +
                                "WHERE a.RETUR_ID = '" + LB_RETURID.Text + "' " +
                                "ORDER BY e.STATUS";
            conn.ExecuteQuery();
            DGR_DETAIL.DataSource = conn.GetDataTable().Copy();
            DGR_DETAIL.DataBind();
        }

        protected void DGR_DETAIL_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ShowDetail(LB_RETURID.Text, LB_DOCNO.Text, e.NewPageIndex);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and c.APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and c.NOREK_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
            }

            if (DDL_STATUS.SelectedValue != "")
            {
                where = where + " and stsmax.STATUS='" + DDL_STATUS.SelectedValue + "'";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and c.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAP_ID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DESTACC.Text.Trim() != "")
            {
                where = where + " and (a.ACC_NO collate database_default + ' - ' + d.BANK collate database_default + ' - ' + a.ACC_NAME collate database_default) like '%" + TXT_DESTACC.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts2.USER_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts2.USER_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE3.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts3.USER_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE3.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            if (TXT_DATE4.Text.Trim() != "")
            {
                if (DDL_STATUS.SelectedValue != "1")
                {
                    where = where + " and convert(date,sts3.USER_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE4.Text.Trim(), "d/M/yyyy") + "')";
                }
            }

            conn.QueryString = "SELECT ACCESS = CASE WHEN ROLE_CODE IN ('36', '15', '35', '39', '0', '37') THEN '1' ELSE '0' END FROM SECURITY..M_USERS WHERE CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("ACCESS").ToString() != "1")
            {
                where = where + " and a.APPROVALBY IN (SELECT DISTINCT c.APPROVALBY FROM SECURITY.dbo.M_USERS a " +
                                    "INNER JOIN SECURITY.dbo.EMAIL_ROLE b ON b.ROLE_CODE = a.ROLE_CODE " +
                                    "INNER JOIN (SELECT a.APPROVALBY, c.EMAIL FROM RETUR_MASTER a " +
                                    "INNER JOIN SECURITY.dbo.M_USERS b ON b.CODE = a.APPROVALBY COLLATE DATABASE_DEFAULT " +
                                    "INNER JOIN SECURITY.dbo.EMAIL_ROLE c ON c.ROLE_CODE = b.ROLE_CODE) c ON c.EMAIL = b.EMAIL " +
                                    "WHERE a.CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "')";
            }

            conn.QueryString = "SELECT " +
                                    "a.RETUR_ID, a.REKAP_ID, a.ACC_NO, a.ACC_NAME, ACC_BANK = d.BANK, ACC_BANK_CODE = a.ACC_BANK, a.ACC_NO_BARU, a.ACC_NAME_BARU,  " +
                                    "ACC_BANK_BARU = db.BANK, " +
                                    "TIPE = CASE WHEN a.TIPE = 'OTOMATIS' THEN 'RET OTOM (RETUR FROM EXECUTE)' ELSE 'RET SISTEM (RETUR FROM FLAGING)' END, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''),  " +
                                    "a.BENEFICIARY,  " +
                                    "a.TRANS_TYPE,  " +
                                    "REASON = b.DESCR, " +
                                    "STATUS = CASE WHEN sts.STATUS = 2 THEN 'RETUR' WHEN sts.STATUS = 1 THEN 'PENDING' WHEN sts.STATUS = 3 THEN 'READY TO PAID' ELSE 'PAID' END,  " +
                                    "STATUS_BY = sts.USER_BY, " +
                                    "STATUS_DATE = convert(varchar, sts.USER_DATE, 103), " +
                                    "RETUR_DATE = case when a.RETUR_DATE is null then convert(varchar, sts2.USER_DATE, 103) else convert(varchar, a.RETUR_DATE, 103) end, " +
                                    "RETUR_BY = case when a.RETUR_DATE is null then sts2.USER_BY else 'SYSTEM' end, " +
                                    "PAID_DATE = CONVERT(varchar, sts4.USER_DATE, 103), " +
                                    "PAID_BY = sts4.USER_BY, " +
                                    "AGING = (case when sts4.USER_BY is null then datediff(dd, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end), GETDATE()) - (select count(HOLIDAY) from SECURITY.dbo.PARAM_HOLIDAY where convert(date,HOLIDAY) between convert(date, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end)) and convert(date,GETDATE())) " +
                                            "else datediff(dd, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end), sts4.USER_DATE) - (select count(HOLIDAY) from SECURITY.dbo.PARAM_HOLIDAY where convert(date,HOLIDAY) between convert(date, (case when a.TIPE = 'OTOMATIS' then sts2.USER_DATE else RETUR_DATE end)) and convert(date,sts4.USER_DATE)) end) " +
                                "FROM RETUR_MASTER a " +
                                "INNER JOIN PARAM_TBL_BANK d on a.ACC_BANK = d.CODE collate database_default " +
                                "INNER JOIN SETTLEMENT_MASTER c on c.REKAPID = a.REKAP_ID COLLATE DATABASE_DEFAULT " +
                                "INNER JOIN PR_REASON_RETUR b ON b.CODE = a.REASON " +
                                "LEFT JOIN PARAM_TBL_BANK db on a.ACC_BANK_BARU = db.CODE collate database_default " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts ON sts.RETUR_ID = a.RETUR_ID " +
                                "LEFT JOIN (SELECT RETUR_ID, STATUS = MAX(STATUS) FROM TRACK_RETUR_MASTER GROUP BY RETUR_ID) stsmax on stsmax.RETUR_ID = sts.RETUR_ID and sts.STATUS = stsmax.STATUS " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts2 ON sts2.RETUR_ID = a.RETUR_ID AND sts2.STATUS = 2 " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts3 ON sts3.RETUR_ID = a.RETUR_ID AND sts3.STATUS = 3 " +
                                "LEFT JOIN TRACK_RETUR_MASTER sts4 ON sts4.RETUR_ID = a.RETUR_ID AND sts4.STATUS = 4 " +
                                "WHERE 1 = 1 " + where +
                                " ORDER BY a.REKAP_ID, sts.STATUS";
            conn.ExecuteQuery(1000);

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            bool v = GlobalUse.ExportToExcel(dt, this, "RETUR", true);

            if (v)
            {
                FillDGR();
            }
            else { FillDGR(); }
        }
    }
}
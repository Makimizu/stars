using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.IO;

namespace FINANCE.Form_Settlement
{
    public partial class StlPaidDone : System.Web.UI.Page
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
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
                //FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery(1500000);
            DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select distinct " +
                                "a.NOREK_SOURCE, " +
                                "b.BANK " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join REKENING_MASTER b on a.NOREK_SOURCE=b.NOREK " +
                                "where a.NOREK_SOURCE is not null " +
                                "order by 2";
            conn.ExecuteQuery(1500000);
            DDL_ACCSOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_APP.Text != "")
            {
                DDL_APP.SelectedValue = LB_APP.Text;
                DDL_APP.Enabled = false;
            }

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }
        }

        protected void FillDDLTipe()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery(1500000);
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and a.APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and a.NOREK_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and a.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAPID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DESTACC.Text.Trim() != "")
            {
                where = where + " and (a.ACC_NO collate database_default + ' - ' + a.ACC_BANK_DESCR collate database_default + ' - ' + a.ACC_NAME collate database_default) like '%" + TXT_DESTACC.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.PAID_DATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.PAID_DATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_EXEDATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.USERDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_EXEDATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_EXEDATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.USERDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_EXEDATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            conn.QueryString = "select TOP (100)" +
                                "a.REKAPID, " +
                                "DESCR = a.TIPE_SETTLEMENT_DESCR, " +
                                "a.NOREK_SOURCE_DESCR, " +
                                "DESTINATION_ACC = a.ACC_NO collate database_default + ' - ' + a.ACC_BANK_DESCR collate database_default + ' - ' + a.ACC_NAME collate database_default, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "PAID_DATE = convert(varchar(20),a.PAID_DATE,106), " +
                                "a.RK_DESCR, " +
                                "EXECUTEBY = a.USERBY + ' (' + convert(varchar(50),a.USERDATE) + ')', " +
                                "a.URL_DONE, a.NO_SM " +
                                "from V_REKENING_JURNAL_CREDIT a " +
                                "LEFT JOIN (select a.*, b.STATUS, b.USER_DATE " +
                                "           from RETUR_MASTER a inner join (SELECT RETUR_ID, STATUS = MAX(STATUS), USER_DATE = MAX(USER_DATE) FROM TRACK_RETUR_MASTER GROUP BY RETUR_ID) b on b.RETUR_ID = a.RETUR_ID WHERE a.TIPE = 'MANUAL') b " +
                                "   ON a.REKAPID = b.REKAP_ID COLLATE DATABASE_DEFAULT " +
                                "   AND b.ACC_BANK = a.ACC_BANK COLLATE Latin1_General_CI_AS " +
                                "   AND b.ACC_NAME = a.ACC_NAME COLLATE Latin1_General_CI_AS " +
                                "   AND b.ACC_NO = a.ACC_NO COLLATE Latin1_General_CI_AS " +
                                "where " +
                                "1=1 and b.REKAP_ID is null " + where + " " +
                                "order by a.PAID_DATE,a.REKAPID";


            conn.ExecuteQuery(120000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            if (conn.GetRowCount() > 0)
            {
                BT_XLS.Visible = true;

                LB_SQL.Text = "select " +
                                "a.REKAPID, " +
                                "DESCR = a.TIPE_SETTLEMENT_DESCR, " +
                                "a.NOREK_SOURCE_DESCR, " +
                                "DESTINATION_ACC = a.ACC_NO collate database_default + ' - ' + a.ACC_BANK_DESCR collate database_default + ' - ' + a.ACC_NAME collate database_default, " +
                                "a.AMOUNT, " +
                                "PAID_DATE = convert(varchar(20),a.PAID_DATE,106), " +
                                "a.RK_DESCR, " +
                                "EXECUTEBY = a.USERBY + ' (' + convert(varchar(50),a.USERDATE) + ')', a.NO_SM " +
                                "from V_REKENING_JURNAL_CREDIT a " +
                                "LEFT JOIN (select a.*, b.STATUS, b.USER_DATE " +
                                "           from RETUR_MASTER a inner join (SELECT RETUR_ID, STATUS = MAX(STATUS), USER_DATE = MAX(USER_DATE) FROM TRACK_RETUR_MASTER GROUP BY RETUR_ID) b on b.RETUR_ID = a.RETUR_ID WHERE a.TIPE = 'MANUAL') b " +
                                "   ON a.REKAPID = b.REKAP_ID COLLATE DATABASE_DEFAULT " +
                                "   AND b.ACC_BANK = a.ACC_BANK COLLATE Latin1_General_CI_AS " +
                                "   AND b.ACC_NAME = a.ACC_NAME COLLATE Latin1_General_CI_AS " +
                                "   AND b.ACC_NO = a.ACC_NO COLLATE Latin1_General_CI_AS " +
                                "where " +
                                "1=1 and b.REKAP_ID is null " + where + " " +
                                "order by a.PAID_DATE,a.REKAPID";



            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                LinkButton lbFile = (LinkButton)DGR.Items[i].FindControl("LB_FILE");

                lbREKAPID.Text = DGR.Items[i].Cells[1].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_STLPAID", lbREKAPID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                //lbFile.Attributes.Add("onclick", "window.open('" +URL+"');");
                //ShowPopUp(((Button)sender).Text, URL);

                conn.QueryString = "select * from ARCHIEVE.dbo.FN_ARSIP where OWNER1 = '" + lbREKAPID.Text + "' and isnull(OWNER2,'') = '' and isnull(OWNER3,'') = '' and TIPE='" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "_STLPAID'";
                conn.ExecuteQuery(1500000);

                if (conn.GetRowCount() > 0)
                {
                    lbFile.Text = "Download File";
                }
            }
        }


        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = LB_SQL.Text;
            conn.ExecuteQuery(1500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            GlobalUse.ExportDataSetToExcel(dt, this, "PAID_DONE.xls", true);
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Button btARSIP = (Button)e.Item.FindControl("BT_ARSIP");

            if (e.CommandName == "BT_FILE")
            {
                string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_STLPAID", e.Item.Cells[1].Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                //lbFile.Attributes.Add("onclick", "window.open('" +URL+"');");
                ShowPopUp(e.Item.Cells[1].Text, URL);
            }

            //ANDEZ START 2024/06/13
            if (e.CommandName == "BT_DOWNLOAD")
            {
                conn.QueryString = "EXEC SP_SETTLEMENT_MEMO_ARCHIEVE '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery(1500000);

                string title = "RPT_SETTLEMENT_MEMO_" + e.Item.Cells[1].Text;
                string filename = title + ".pdf";

                string fullpath = "C:/tmp/" + filename;

                string SQL = "delete from ARCHIEVE.dbo.FN_ARSIP where OWNER1 = '" + e.Item.Cells[1].Text + "' and OWNER2 = 'ExportSettle' " +
                    "insert into ARCHIEVE.dbo.FN_ARSIP select " +
                            "ARCHIEVE.dbo.UFN_GET_NEWID()," +
                            "'FN_STLPAID'," +
                            "'" + e.Item.Cells[1].Text + "'," +
                            "'ExportSettle'," +
                            "null," +
                            "'" + title + "'," +
                            "'" + filename + ".pdf'," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "@File";
                GlobalUse.FileToSQL(fullpath, SQL);
                if (File.Exists(fullpath))
                    File.Delete(fullpath);

                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.FN_ARSIP where OWNER1 = '" + e.Item.Cells[1].Text + "' and OWNER2 = 'ExportSettle'",
                                    Page);
            }
            //ANDEZ END 2024/06/13

        }

        protected void ShowPopUp(string title, string url)
        {
            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", url);
        }
    }
}
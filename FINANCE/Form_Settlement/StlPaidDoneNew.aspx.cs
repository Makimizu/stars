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
    public partial class StlPaidDoneNew : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default " +
                                "/*WHERE CODE = 'GL'*/";

            conn.ExecuteQuery();
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
            conn.ExecuteQuery();
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
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
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

            /*
           conn.QueryString = "select " +
                               "a.REKAPID, " +
                               "DESCR = a.TIPE_SETTLEMENT_DESCR, " +
                               "a.NOREK_SOURCE_DESCR, " +
                               "DESTINATION_ACC = a.ACC_NO collate database_default + ' - ' + a.ACC_BANK_DESCR collate database_default + ' - ' + a.ACC_NAME collate database_default, " +
                               "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                               "PAID_DATE = convert(varchar(20),a.PAID_DATE,106), " +
                               "a.RK_DESCR, " +
                               "EXECUTEBY = a.USERBY + ' (' + convert(varchar(50),a.USERDATE) + ')', " +
                               "a.URL_DONE " +
                               "from V_REKENING_JURNAL_CREDIT a " +
                               "where " +
                               "1=1 " + where + " " +
                               "order by a.PAID_DATE,a.REKAPID";
            */
            /*
            conn.QueryString = "select " +
                               "a.REKAPID, " +
                               "DESCR = a.TIPE_SETTLEMENT_DESCR, " +
                               "a.NOREK_SOURCE_DESCR, " +
                               "DESTINATION_ACC = a.ACC_NO collate database_default + ' - ' + a.ACC_BANK_DESCR collate database_default + ' - ' + a.ACC_NAME collate database_default, " +
                               "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                               "PAID_DATE = convert(varchar(20),a.PAID_DATE,106), " +
                               "a.RK_DESCR, " +
                               "EXECUTEBY = a.USERBY + ' (' + convert(varchar(50),a.USERDATE) + ')', " +
                               "a.URL_DONE, " +
                               "DOCNO = sd.DOCNO, " +
                               "sd.ACC_NAME, " +
                               "sd.ACC_NO, " +
                               "sd.CUSTOMER_CODE, " +
                               "SLA = CASE WHEN sm.APPROVALDATE IS NULL THEN '-' ELSE CONVERT(VARCHAR(MAX), DATEDIFF(day,sm.APPROVALDATE, GETDATE())) END " +
                               "from V_REKENING_JURNAL_CREDIT a " +
                               "INNER JOIN FINANCE..SETTLEMENT_MASTER sm ON a.REKAPID = sm.REKAPID " +
                               "LEFT OUTER JOIN FINANCE..SETTLEMENT_DETAIL sd ON a.REKAPID = sd.REKAPID " +
                               "where " +
                               "1=1 " + where + " " +
                               "order by a.PAID_DATE,a.REKAPID";
            */
            /*
            conn.QueryString = "select " +
                               "REKAPID, " +
                               "DESCR, " +
                               "NOREK_SOURCE_DESCR, " +
                               "DESTINATION_ACC, " +
                               "AMOUNT, " +
                               "PAID_DATE, " +
                               "a.RK_DESCR, " +
                               "EXECUTEBY, " +
                               "URL_DONE, " +
                               "DOCNO, " +
                               "ACC_NAME, " +
                               "ACC_NO, " +
                               "CUSTOMER_CODE, " +
                               "SLA," +
                               "FULLNAME " +
                               "FROM GLIFE.[dbo].[V_APPLICATION_TRACK_SLA_PRERENEWAL] a " +
                               " WITH (NOLOCK) " +
                               "where " +
                               "1=1 " + where + " " +
                               "order by PAID_DATE, REKAPID ";*/

            conn.QueryString = "EXEC [GLIFE]..SP_APPLICATION_TRACK_SLA_PRERENEWAL '" + where.Replace("'", "''") + "'";
           
            conn.ExecuteQuery();
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
                                "AMOUNT, " +
                                "PAID_DATE = convert(varchar(20),a.PAID_DATE,106), " +
                                "a.RK_DESCR, " +
                                "EXECUTEBY = a.USERBY + ' (' + convert(varchar(50),a.USERDATE) + ')' " +
                                "from V_REKENING_JURNAL_CREDIT a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.PAID_DATE,a.REKAPID";
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");

                lbREKAPID.Text = DGR.Items[i].Cells[1].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

            }
        }


        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = LB_SQL.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            GlobalUse.ExportDataSetToExcel(dt, this, "PAID_DONE.xls", true);
        }


    }
}
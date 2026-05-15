using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class CollDep : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            DDL_APP.Items.Clear();
            conn.QueryString = "select distinct " +
                                "b.CODE, b.APP_NAME " +
                                "from PARAM_INVOICE_TYPE a " +
                                "inner join V_LINK_SC_M_APPS b on a.APP_ID = b.CODE collate database_default " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLTipe();
        }

        protected void FillDDLTipe()
        {
            DDL_TYPE.Items.Clear();

            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 1";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            LB_BALANCE.Text = "";
            LB_CREDIT.Text = "";
            LB_DEBET.Text = "";
            string where = "";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.TIPE_SETTLEMENT = '" + DDL_TYPE.SelectedValue + "' ";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + "and RK_DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + "and convert(date,a.THEDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.THEDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_BALANCE.SelectedValue != "")
                where = where + DDL_BALANCE.SelectedValue + " ";

            conn.QueryString = "select " +
                                "TRXID, " +
                                "TIPE_SETTLEMENT_DESCR, " +
                                "RFDDATE = convert(varchar(20),a.THEDATE,106), " +
                                "RK_DESCR, " +
                                "COMPANY_EMAIL, " +
                                "LAST_SEND_DATE, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "USED = replace(convert(varchar(100),convert(money,USED),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                "REPORT_URL, " +
                                "COVER_LTR_URL " +
                                "from V_SETTLEMENT_REFUND_COLLDEP a " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " + where + " " +
                                "order by a.THEDATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = " : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "USED = replace(convert(varchar(100),convert(money,SUM(USED)),1),'.00',''), " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,SUM(AMOUNT)),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,SUM(BALANCE)),1),'.00','') " +
                                "from V_SETTLEMENT_REFUND_COLLDEP a  " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " + where;
            conn.ExecuteQuery();

            LB_CREDIT.Text = " : " + conn.GetFieldValue("AMOUNT").ToString();
            LB_DEBET.Text = " : " + conn.GetFieldValue("USED").ToString();
            LB_BALANCE.Text = " : " + conn.GetFieldValue("BALANCE").ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btHST = (Button)DGR.Items[i].FindControl("BT_HST");
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                Button btCOVLTR = (Button)DGR.Items[i].FindControl("BT_COVLTR");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                Label lblLASTSEND = (Label)DGR.Items[i].FindControl("LB_LASTSEND");

                btHST.Attributes.Add("onclick", "window.open('../Form_Bank/RK_History.aspx?TRXID=" + DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btDETAIL.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btCOVLTR.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                txtEMAIL.Text = DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                lblLASTSEND.Text = "LAST SEND : " + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "");
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

            if (e.CommandName == "Email")
            {
                LB_ERROR.Text = "";
                if (txtEMAIL.Text.Trim() == "")
                    return;

                LB_ERROR.Text = GlobalUse.SendEmailSQL(System.Configuration.ConfigurationManager.AppSettings["appid"], "7", e.Item.Cells[0].Text, e.Item.Cells[1].Text, txtEMAIL.Text.Trim());
                FillDGR();
            }
        }
    }
}
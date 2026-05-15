using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Globalization;

namespace FINANCE.Form_Settlement
{
    public partial class StlHistory : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            DDL_APP.Items.Clear();
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLTipe();

            conn.QueryString = "select CODE,DESCR from PR_SETTLEMENT_TRACK";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRACK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "NOREK, BANK " +
                                "from REKENING_MASTER " +
                                "where " +
                                "STL > 0";

            conn.ExecuteQuery();
            DDL_ACCSOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "FIRSTDATE = convert(varchar(20), dateadd(day, -(DAY(GETDATE()))+1, GETDATE()), 103), " +
                                "TODAY = convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_DATE1.Text = conn.GetFieldValue("FIRSTDATE").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("TODAY").ToString();
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

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RESULT.Text = "";

            string appid = "";
            string tipe = "";
            string track = "";
            string startdate = "null";
            string enddate = "null";

            if(DDL_APP.SelectedValue != "")
                appid = DDL_APP.SelectedValue;
            if(DDL_TIPE.SelectedValue != "")
                tipe = DDL_TIPE.SelectedValue;
            if(DDL_TRACK.SelectedValue != "")
                track = DDL_TRACK.SelectedValue;
            if (TXT_DATE1.Text != "")
                startdate = "'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text, "d/M/yyyy") + "'";
            if (TXT_DATE2.Text != "")
                enddate = "'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text, "d/M/yyyy") + "'";

            conn.QueryString = "exec SP_SETTLEMENT_MASTER " +
                                "'" + appid + "'," +
                                "'" + tipe + "'," +
                                "'" + track + "'," +
                                "'" + DDL_ACCSOURCE.SelectedValue + "'," +
                                "'" + TXT_ID.Text.Trim() + "'," +
                                startdate + "," +
                                enddate;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                BT_XLS.Visible = true;
            

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            double TotalAmount = 0;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");

                lbREKAPID.Text = DGR.Items[i].Cells[1].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                TotalAmount = TotalAmount + double.Parse(dt.Rows[i][3].ToString().Replace(",", ""));
            }

            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "";

            LB_RESULT.Text = "<table style='border-spacing:0px;'>" +
                                "<tr><td style='width:100px;'>Records</td><td>:</td><td style='text-align:right;'>" + dt.Rows.Count.ToString() +" </td></tr>" +
                                "<tr><td>Total Amount</td><td>:</td><td style='text-align:right;'>" + String.Format(nfi, "{0:C}", TotalAmount).Replace(".00","") + "</td></tr>" +
                                "</table>";                                
            
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }
    }
}
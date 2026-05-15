using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Text;
using System.Net.Mime;

namespace SAVING.Form_Parameter
{
    public partial class InvestmentRate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }
        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where SEGMENT = 0 and PAYDI = 1  and UNITIZE = 0 order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select RATE_DATE = '31 Dec ' + convert(varchar(10),YEAR(GETDATE()) - a.SEQ + 1) from SC_SEQ a where a.SEQ <= 5 order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_RATE_DATE.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select RATE_DATE = '31 Dec ' + convert(varchar(10),YEAR(GETDATE()) - a.SEQ + 1) from SC_SEQ a where a.SEQ <= 5 order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_INPUT_RATE_DATE.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select THISYEAR	= '31 Dec ' + convert(varchar(10),YEAR(GETDATE()))";
            conn.ExecuteQuery();

            try
            {
                DDL_RATE_DATE.SelectedValue = conn.GetFieldValue("THISYEAR").ToString();
            }
            catch { }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select		" +
                                "PRODUCT_GROUP  = a.PRODUCT_GROUP,	" +
                                "RATE_DATE      = convert(varchar(15),a.RATE_DATE,106), " +
                                "CURRENCY       = a.CURRENCY, " +
                                "RATE           = a.RATE, " +
                                "USERBY         = a.USERBY, " +
                                "USERDATE       = convert(varchar(15),a.USERDATE,106) " +
                                "from           INVESTMENT_RATE a " +
                                "where " +
                                "a.PRODUCT_GROUP = '" + DDL_PRODUCT_GROUP.SelectedValue + "'" +
                                "and a.RATE_DATE = '" + DDL_RATE_DATE.SelectedValue + "' " +
                                "and a.CURRENCY = '" + DDL_CURRENCY.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
            /*
            for (int i = 0; i < DGR.Items.Count; i++)
            {
            }
            */
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_PRODUCT_GROUP.Text = e.Item.Cells[0].Text;
                DDL_INPUT_RATE_DATE.SelectedValue = e.Item.Cells[1].Text.Trim().Replace("&nbsp;", "");
                DDL_INPUT_CURRENCY.SelectedValue = e.Item.Cells[2].Text.Trim().Replace("&nbsp;", "");
                TXT_INPUT_RATE.Text = e.Item.Cells[3].Text.Trim().Replace("&nbsp;", "");
                TBL_EDIT.Visible = true;
            }
        }

        protected void DDL_RATE_DATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CURRENCY_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_INVESTMENT_RATE_UPSERT " +
                "'" + LB_PRODUCT_GROUP.Text + "'," +
                "'" + DDL_RATE_DATE.SelectedValue + "'," +
                "'" + DDL_INPUT_CURRENCY.SelectedValue + "'," +
                "'" + TXT_INPUT_RATE.Text + "'," +
                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            TBL_EDIT.Visible = false;
            FillDGR();
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            LB_PRODUCT_GROUP.Text = DDL_PRODUCT_GROUP.SelectedValue;
            try
            {
                DDL_INPUT_RATE_DATE.SelectedValue = "";
            }
            catch { }
            try
            {
                DDL_INPUT_CURRENCY.SelectedValue = "";
            }
            catch { }
            TXT_INPUT_RATE.Text = "";
            TBL_EDIT.Visible = true;
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_EDIT.Visible = false;
        }
    }
}
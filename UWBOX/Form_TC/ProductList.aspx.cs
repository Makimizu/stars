using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Globalization;
using System.Configuration;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductList : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from PR_CURRENCY order by 1";
            conn.ExecuteQuery();
            DDL_CURRENCY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_PRODUCT_TYPE order by 2";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Clear();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_PRODUCT_LINE_OF_BUSINESS order by 2";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            DDL_LOB.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLGroup();
        }

        protected void FillDDLGroup()
        {
            string where = "";

            if (DDL_SEGMENT.SelectedValue != "")
                where = where + " and a.SEGMENT = " + DDL_SEGMENT.SelectedValue + " ";

            if (DDL_PAYDI.SelectedValue != "")
                where = where + " and a.PAYDI = " + DDL_PAYDI.SelectedValue + " ";

            if (DDL_UNITIZE.SelectedValue != "")
                where = where + " and a.UNITIZE = " + DDL_UNITIZE.SelectedValue + " ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR " +
                                "from		PARAM_PRODUCT_GROUP a " +
                                "where " +
                                "1=1 " + where + " order by 2";
            conn.ExecuteQuery();

            DDL_GROUP.Items.Clear();
            DDL_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            string where = "";
            LB_RECORDS.Text = "";

            if (DDL_SEGMENT.SelectedValue != "")
                where = where + " and a.SEGMENT = " + DDL_SEGMENT.SelectedValue + " ";

            if (DDL_PAYDI.SelectedValue != "")
                where = where + " and a.PAYDI = " + DDL_PAYDI.SelectedValue + " ";

            if (DDL_UNITIZE.SelectedValue != "")
                where = where + " and a.UNITIZE = " + DDL_UNITIZE.SelectedValue + " ";

            if (DDL_GROUP.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP = '" + DDL_GROUP.SelectedValue + "' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.PRODUCT_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            if (DDL_STATUS.SelectedValue != "")
                where = where + " and a.STAT = '" + DDL_STATUS.SelectedValue + "' ";

            if (DDL_CURRENCY.SelectedValue != "")
                where = where + " and a.CURRENCY = '" + DDL_CURRENCY.SelectedValue + "' ";

            if (DDL_LOB.SelectedValue != "")
                where = where + " and a.LINE_OF_BUSINESS = '" + DDL_LOB.SelectedValue + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.PRODUCT_GROUP_DESCR like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.PRODUCT_CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "PRODUCT_DESCR = '<a href=''ProductFrame.aspx?CODE=' + PRODUCT_CODE + '''><table style=''width:100%;border-spacing:0px;''><tr><td style=''width:40px;''>' + PRODUCT_CODE + '</td><td>&nbsp;&nbsp;' + PRODUCT_DESCR + '</td></tr></table></a>', " +
                                "PRODUCT_GROUP_DESCR, " +
                                "PRODUCT_TYPE_DESCR, " +
                                "STAT_DESCR, " +
                                "PAYDI_DESCR, " +
                                "UNITIZE_DESCR, " +
                                "SEGMENT_DESCR, " +
                                "CURRENCY_DESCR, " +
                                "LINE_OF_BUSINESS_DESCR, " +
                                "START_DATE = convert(varchar(20), START_DATE, 106), " +
                                "STAT = convert(int, STAT), " +
                                "PRODUCT_CODE " +
                                "from		V_PARAM_PRODUCT_MASTER a " +
                                "where 1=1 " + where +
                                "order by a.PRODUCT_DESCR";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = null;
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[0].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_SEGMENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLGroup();
        }

        protected void DDL_PAYDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLGroup();
        }

        protected void DDL_UNITIZE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLGroup();
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLGroup();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductHeader.aspx?CODE=");
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    string stat = "0";
                    if (cb.Checked)
                        stat = "1";

                    conn.QueryString = "update PARAM_PRODUCT_MASTER set " +
                                        "STAT = " + stat + ", " +
                                        "LASTCHANGEBY   = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where PRODUCT_CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                    conn.ExecuteNonQuery();

                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }
        }

        protected void DDL_GROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductLicenceDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LICENCE_NO.Text = Request.QueryString["CODE"];
                LoadRecord(LICENCE_NO.Text);
            }

            //LICENCE_NO.Text = Request.QueryString["CODE"];
            //LoadRecord(LICENCE_NO.Text);
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select " +
                                "TAKEN = ISNULL(b.LICENCE_NO,0), " +
                                "PRODUCT_CODE,  " +
                                "PRODUCT_DESCR = UPPER(PRODUCT_DESCR), " +
                                "START_DATE = CONVERT(varchar, START_DATE, 106), " +
                                "END_DATE = CONVERT(varchar, END_DATE, 106), " +
                                "STAT = case when (STAT = 1) then 'ACTIVE' else '' end " +
                                "from PARAM_PRODUCT_MASTER a " +
                                "left join (select LICENCE_NO from PARAM_PRODUCT_MASTER " +
                                "where LICENCE_NO = '" + code + "') b on a.LICENCE_NO = b.LICENCE_NO";
            if (DDL_GROUP.SelectedIndex > 0)
            {
                conn.QueryString = conn.QueryString + " where a.PRODUCT_GROUP = '" + DDL_GROUP.SelectedValue + "'";
            }
            conn.QueryString = conn.QueryString + " group by b.LICENCE_NO,PRODUCT_CODE,PRODUCT_DESCR,START_DATE,END_DATE,STAT";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                return;
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = null;
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[0].Text != "0")
                {
                    cb.Checked = true;
                }
            }

            var res = dt.Select("TAKEN <> '0'").Count();
            LB_STATUS.Text = res + " Selected Records";
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE = 'All',DESCR = 'All' union all select CODE,DESCR from PARAM_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            DDL_GROUP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LoadRecord(LICENCE_NO.Text);
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            string LicenceTmp = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    if (!((CheckBox)sender).Checked)
                    {
                        LicenceTmp = "";
                    }
                    else
                    {
                        LicenceTmp = LICENCE_NO.Text;
                    }

                    conn.QueryString = "update PARAM_PRODUCT_MASTER set LICENCE_NO = '" + LicenceTmp + " where PRODUCT_CODE = '" + DGR.Items[i].Cells[1].Text + "';";
                    conn.ExecuteNonQuery();
                    //LB_TITLE.Text = conn.QueryString;
                }
            }
            LoadRecord(LICENCE_NO.Text);
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            //LICENCE_NO.Text = Request.QueryString["CODE"];
            LoadRecord(LICENCE_NO.Text);
        }

        protected void DDL_GROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord(LICENCE_NO.Text);
        }

    }
}
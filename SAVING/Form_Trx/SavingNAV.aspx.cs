using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace SAVING.Form_Trx
{
    public partial class SavingNAV : System.Web.UI.Page
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
            TXT_NAV.Text = "0";

            conn.QueryString = "select CODE, APP_NAME from V_LINK_SC_M_APPS";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APPID.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLFund();

            conn.QueryString = "select convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_NAVDATE.Text = conn.GetFieldValue(0, 0).ToString();
        }

        protected void FillDDLFund()
        {
            DDL_FUND.Items.Clear();

            conn.QueryString = "select CODE, DESCR from V_FUND_NON_UNITIZE where APPID = '" + DDL_APPID.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FUND.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "APP_ID		= APP_ID, " +
                                "FUND_CODE	= FUND_CODE, " +
                                "APP_NAME	= APP_NAME, " +
                                "BATCH_ID   = BATCH_ID, " +
                                "RESULT     = RESULT, " +
                                "RESULT_URL = RESULT_URL, " +
                                "FUND_DESCR	= FUND_DESCR, " +
                                "THEDATE	= convert(varchar(20), THEDATE, 106), " +
                                "AMOUNT		= convert(varchar(100), convert(money, AMOUNT), 1), " +
                                "USERBY		= USERBY + ' - ' + convert(varchar(20), USERDATE, 106), " +
                                "ENABLE_CHG	= ENABLE_CHG " +
                                "from V_NAV_SAVING a " +
                                "where " +
                                "a.APP_ID = '" + DDL_APPID.SelectedValue + "' " +
                                "and a.FUND_CODE = '" + DDL_FUND.SelectedValue + "' " +
                                "order by " +
                                "a.USERDATE desc, " +
                                "a.THEDATE desc, " +
                                "a.FUND_CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtNAV = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                Button btS = (Button)DGR.Items[i].FindControl("BT_S");
                Button btX = (Button)DGR.Items[i].FindControl("BT_X");
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");

                txtNAV.Text = DGR.Items[i].Cells[2].Text;
                if (DGR.Items[i].Cells[3].Text == "0")
                {
                    txtNAV.ReadOnly = true;
                    btS.Visible = false;
                    btX.Visible = false;
                }
                else
                {
                    txtNAV.BackColor = System.Drawing.Color.LightYellow;
                }

                if (DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    btDETAIL.Visible = true;
                    btDETAIL.Text = DGR.Items[i].Cells[5].Text + " Records";
                    btDETAIL.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "") + "','INVESTMENT RETURN','height=500px,width=900px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }
            }

        }

        protected void DDL_APPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDDLFund();
            FillDGR();
        }

        protected void DDL_FUND_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_NAV_SAVING_UPSERT " +
                                    "'" + DDL_APPID.SelectedValue + "'," +
                                    "'" + DDL_FUND.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_NAVDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    TXT_NAV.Text.Trim().Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtNAV = (TextBox)e.Item.FindControl("TXT_AMOUNT");

                conn.QueryString = "exec SP_NAV_SAVING_UPSERT " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[9].Text + "'," +
                                    txtNAV.Text.Trim().Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_NAV_SAVING_ROLLBACK " +
                                    "'" + e.Item.Cells[4].Text + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }
    }
}
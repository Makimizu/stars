using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  if (Session["s"] == null)
                //    Response.Redirect("logout.aspx");

                LB_CODE.Text = Request.QueryString["CODE"];
                Setup();

                if (LB_CODE.Text != "")
                {
                    LoadRecord();
                }
                else
                {
                    TR_STATUS.Visible = TR_BUTTONS.Visible = false;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            DDL_GROUP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from PR_CURRENCY order by 1";
            conn.ExecuteQuery();
            DDL_CURRENCY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_PRODUCT_LINE_OF_BUSINESS order by 2";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "PRODUCT_DESCR, " +
                                "PRODUCT_GROUP, " +
                                "CURRENCY, " +
                                "START_DATE		= convert(varchar(20), START_DATE, 103), " +
                                "STAT_DESCR, " +
                                "LINE_OF_BUSINESS " +
                                "from V_PARAM_PRODUCT_MASTER  " +
                                "where " +
                                "PRODUCT_CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            TXT_DESCR.Text = conn.GetFieldValue("PRODUCT_DESCR").ToString();
            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            LB_STATUS.Text = conn.GetFieldValue("STAT_DESCR").ToString();
            ////added by agung 31/3/21
            ////TXT_LICENCE.Text = conn.GetFieldValue("LICENCE_NO").ToString();
            //DDL_LICENCE.SelectedValue = conn.GetFieldValue("LICENCE_NO").ToString();

            try
            {
            DDL_GROUP.SelectedValue = conn.GetFieldValue("PRODUCT_GROUP").ToString();
            }
            catch { }

            try
            {
                DDL_CURRENCY.SelectedValue = conn.GetFieldValue("CURRENCY").ToString();
            }
            catch { }

            try
            {
                DDL_LOB.SelectedValue = conn.GetFieldValue("LINE_OF_BUSINESS").ToString();
            }
            catch { }

            if (conn.GetFieldValue("PRODUCT_GROUP").ToString() == "IH")
            {
                BT8.Text = "PACKAGE & PLAN";
            }

            ShowTC();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_STARTDATE.Text.Trim() == "" || TXT_DESCR.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_UPSERT " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + TXT_DESCR.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_GROUP.SelectedValue + "'," +
                                "'" + DDL_CURRENCY.SelectedValue + "'," +
                                "'" + DDL_LOB.SelectedValue + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (LB_CODE.Text != "")
                LoadRecord();
            else
            {
                Response.Redirect("ProductFrame.aspx?CODE=" + conn.GetFieldValue("ID").ToString());
            }
        }

        protected void ShowTC()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductTC_Frame.aspx?CODE=" + LB_CODE.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductChannel.aspx?CODE=" + LB_CODE.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_PROD", LB_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = '" + URL + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductLoading.aspx?CODE=" + LB_CODE.Text + "';</script>");

        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductReinsuranceFrame.aspx?CODE=" + LB_CODE.Text + "';</script>");

        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductOtherSetup.aspx?CODE=" + LB_CODE.Text + "';</script>");
        }

        protected void BT8_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            if (LB_TITLE.Text == "PACKAGE & PLAN") 
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductHealth.aspx?CODE=" + LB_CODE.Text + "';</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductMinMaxSA.aspx?CODE=" + LB_CODE.Text + "';</script>");
            }

        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductBody.location.href = 'ProductMedicalQuestion.aspx?CODE=" + LB_CODE.Text + "';</script>");
        }
    }
}
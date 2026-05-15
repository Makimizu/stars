using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace UWBOX.Form_Parameter
{
    public partial class ReinsTC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                try
                {
                    TXT_CODE.Text = Request.QueryString["CODE"];
                    LoadRecord(TXT_CODE.Text);
                }
                catch { }
            }
        }

        protected void Setup()
        {

        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select " +
                                "ID, " +
                                "COMPANY_NAME, " +
                                "DESCR, " +
                                "DOCNO, " +
                                "START_DATE = convert(varchar(20), START_DATE, 106), " +
                                "TYPE_DESCR, " +
                                "OWN_RETENTION = replace(convert(varchar(100),convert(money,OWN_RETENTION),1),'.00',''), " +
                                "QUOTA_SHARE, " +
                                "LOADING, " +
                                "MATRIX_ID, " +
                                "MATRIX_DESCR, " +
                                "RATE_ID, " +
                                "RATE_DESCR " +
                                "from V_LINK_REINS_TC_MASTER " +
                                "where ID = '" + code + "'";
            conn.ExecuteQuery();

            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_DOCNO.Text = conn.GetFieldValue("DOCNO").ToString();
            LB_OR.Text = conn.GetFieldValue("OWN_RETENTION").ToString();
            LB_SHARE.Text = conn.GetFieldValue("QUOTA_SHARE").ToString();
            LB_LOADING.Text = conn.GetFieldValue("LOADING").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_RATE.Text = conn.GetFieldValue("RATE_DESCR").ToString();
            LB_TYPE.Text = conn.GetFieldValue("TYPE_DESCR").ToString();
            LB_UW.Text = conn.GetFieldValue("MATRIX_DESCR").ToString();
            LB_RATE_CODE.Text = conn.GetFieldValue("RATE_ID").ToString();
            LB_UW_CODE.Text = conn.GetFieldValue("MATRIX_ID").ToString();

            if (LB_UW_CODE.Text.Trim().Replace("&nbsp;", "") == "")
                BT_UW.Visible = false;
            if (LB_RATE_CODE.Text.Trim().Replace("&nbsp;", "") == "")
                BT_RATE.Visible = false;
        }


        protected void ShowPopUp(string title, string mode)
        {
            DGR.Visible = false;
            iFrame.Visible = false;
            TBL_GENDER.Visible = false;

            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

            if (mode == "1")
            {
                conn.QueryString = "exec SP_LINK_REINS_PARAM_UW_MATRIX_DETAIL '" + LB_UW_CODE.Text + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    DGR.Visible = true;
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    DGR.DataSource = dt;
                    DGR.DataBind();
                }
            }

            if (mode == "2")
            {
                TBL_GENDER.Visible = true;
                LB_RATECODE.Text = LB_RATE_CODE.Text;
                DDL_GENDER.SelectedValue = "M";
                LoadDGRRate(LB_RATE_CODE.Text, "M");
            }


            if (mode == "3")
            {
                iFrame.Visible = true;
                string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                iFrame.Attributes.Add("src", URL);
            }
        }

        protected void LoadDGRRate(string code, string gender)
        {
            conn.QueryString = "exec SP_LINK_REINS_PARAM_PREMIUM_RATE_DETAIL '" + code + "','" + gender + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DGR.Visible = true;
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();
            }
        }

        protected void BT_UW_Click(object sender, EventArgs e)
        {
            ShowPopUp("MEDICAL TABLE : " + LB_UW.Text, "1");
        }

        protected void BT_RATE_Click(object sender, EventArgs e)
        {
            ShowPopUp("PREMIUM RATE : " + LB_RATE.Text, "2");
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            ShowPopUp("ARCHIEVE", "3");
        }

        protected void DDL_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDGRRate(LB_RATECODE.Text, DDL_GENDER.SelectedValue);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationMainInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                //bDone = true;
                Setup();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select SEQ = MAX(SEQ) from TRACK_DATA where TIPE_CODE='UW' and OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            //if (conn.GetRowCount() == 0)
            //    result = false;
            if (int.Parse(conn.GetFieldValue("SEQ").ToString()) < 3)
                result = false;

            return result;
        }

        protected void Setup()
        {
            conn.QueryString = "exec SP_APPLICATION_MAIN_INFO '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                TXT_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
                TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
                TXT_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();
                LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
                LB_UWCODE.Text = conn.GetFieldValue("UW_CODE").ToString();
                LB_PREMIUM.Text = conn.GetFieldValue("PREMIUM").ToString();
                LB_NETTPREMIUM.Text = conn.GetFieldValue("NETT_PREMIUM").ToString();
                TXT_STNC.Text = conn.GetFieldValue("STNC_DATE").ToString();

                LoadTenor();
                LoadJoinLife();
            }

            if (bDone || Request.QueryString["readonly"] == "1")
            {
                BT_MAINSAVE.Visible = false;
                TXT_SUMINS.Enabled = false;
                TXT_STARTDATE.Enabled = false;
                TXT_ENDDATE.Enabled = false;
                TXT_STNC.Enabled = false;

                DDL_TD.Enabled = false;
                DDL_TM.Enabled = false;
                DDL_TY.Enabled = false;
            }
        }

        protected void LoadJoinLife()
        {
            conn.QueryString = "exec SP_APPLICATION_JOIN_ACCOUNT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                TR_JOIN.Visible = true;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_JOIN.DataSource = dt;
            DGR_JOIN.DataBind();
        }

        protected void LoadTenor()
        {
            if (LB_REGNO.Text == "")
                return;

            DDL_TY.SelectedIndex = 0;
            DDL_TM.SelectedIndex = 0;
            DDL_TD.SelectedIndex = 0;

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MAIN_TENOR '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                DDL_TY.SelectedValue = conn.GetFieldValue("Y").ToString();
                DDL_TM.SelectedValue = conn.GetFieldValue("M").ToString();
                DDL_TD.SelectedValue = conn.GetFieldValue("D").ToString();
            }
            catch { }
        }

        protected void DDL_DEFINE_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDL_DEFINE.SelectedValue)
            {
                case "T": TR_TENOR.Visible = true;
                    TR_ENDDATE.Visible = false;
                    LoadTenor();
                    break;
                case "E": TR_TENOR.Visible = false;
                    TR_ENDDATE.Visible = true;
                    break;
            }
        }

        protected void BT_MAINSAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            string enddate = GlobalUse.GlobalDateFormat(TXT_ENDDATE.Text.Trim(), "d/M/yyyy");
            if (DDL_DEFINE.SelectedValue == "T")
            {
                conn.QueryString = "exec SSP_ENDDATE_YMD " +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    DDL_TY.SelectedValue + "," +
                                    DDL_TM.SelectedValue + "," +
                                    DDL_TD.SelectedValue;
                conn.ExecuteQuery();

                enddate = conn.GetFieldValue("END_DATE").ToString();
            }

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MAIN_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + TXT_SUMINS.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + enddate + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_STNC.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("ApplicationMainInfo.aspx?ID=" + LB_REGNO.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }


    }
}
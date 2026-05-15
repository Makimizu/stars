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
    public partial class ApplicationMainInfoUW : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                Setup();
            }
        }


        protected void Setup()
        {
            conn.QueryString = "select " +
                                "b.CODE, b.DESCR " +
                                "from " +
                                "( " +
                                "select CODE = '00' union all " +
                                "select CODE = 'AC' union all " +
                                "select CODE from V_LINK_UW_PR_UW_CODE where CODE like 'NM%' union all " +
                                "select CODE from V_LINK_UW_PR_UW_CODE where CODE not like 'NM%' and CODE not in ('00','AC') " +
                                ") a " +
                                "inner join V_LINK_UW_PR_UW_CODE b on a.CODE = b.CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_UWCODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''), " +
                                "START_AGE, " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "END_DATE = convert(varchar(20),END_DATE,106), " +
                                "UW_CODE = a.UW_CODE, " +
                                "PREMIUM = replace(convert(varchar(100), convert(money,isnull(PREMIUM,0)),1), '.00',''), " +
                                "NETT_PREMIUM = replace(convert(varchar(100), convert(money,isnull(NETT_PREMIUM,0)),1), '.00','') " +
                                "from V_APPLICATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
                LB_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
                LB_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();
                LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();                
                LB_PREMIUM.Text = conn.GetFieldValue("PREMIUM").ToString();
                LB_NETTPREMIUM.Text = conn.GetFieldValue("NETT_PREMIUM").ToString();

                try
                {
                    DDL_UWCODE.SelectedValue = conn.GetFieldValue("UW_CODE").ToString();
                }
                catch { }

                if (conn.GetFieldValue("UW_CODE").ToString() != "00")
                    DDL_UWCODE.Enabled = false;

                LoadTenor();
                LoadJoinLife();
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
       
            conn.QueryString = "exec SP_APPLICATION_MAIN_TENOR '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_TY.Text = conn.GetFieldValue("Y").ToString();
            LB_TM.Text = conn.GetFieldValue("M").ToString();
            LB_TD.Text = conn.GetFieldValue("D").ToString();
        }

        protected void DDL_UWCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_UPDATE_UWCODE_MANUAL " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_UWCODE.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
        }
    }
}
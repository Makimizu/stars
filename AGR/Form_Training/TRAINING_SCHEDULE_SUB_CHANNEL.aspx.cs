using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;


namespace AGR
{
    public partial class TRAINING_SCHEDULE_SUB_CHANNEL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_SCHEDULE_CODE.Text = Request.QueryString["code"];

                Setup();
                FillDGR();

            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "SCHEDULE_CODE = a.SCHEDULE_CODE, " +
                                "TRAINING_NAME = b.TRAINING_NAME + ' ('+ a.SCHEDULE_CODE + ')' " +
                                "from TRAINING_SCHEDULE a " +
                                "inner join TRAINING_MASTER b " +
                                "on a.TRAINING_CODE = b.TRAINING_CODE " +
                                "order by b.TRAINING_NAME";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SCHEDULE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION ORDER BY DESCR";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SUBCODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUB_CHANNEL_DISPLAY @SCHEDULE_CODE =  '" + TXT_SCHEDULE_CODE.Text + "' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {


            //   try
            //    {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUB_CHANNEL_UPSERT " +
                                 "@SUB_CODE = '" + DDL_SUBCODE.SelectedValue + "'," +                                                  
                                 "@SCHEDULE_CODE = '" + DDL_SCHEDULE.SelectedValue + "'," +
                                 "@SUB_CHANNEL = '" + TXT_SUB_CHANNEL.Text + "'," +
                                 "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR();

            //   }
            //   catch
            //  {
            //LB_ERROR.Text = "<BR> UPLINER CODE can not be blank <BR> UPLINER CODE might be wrong";
            //  }

        }

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUB_CHANNEL_DISPLAY @SCHEDULE_CODE =  '" + TXT_SCHEDULE_CODE.Text + "'";
            conn.ExecuteQuery();

            TXT_SUB_CHANNEL.Text = conn.GetFieldValue("SUB_CHANNEL").ToString();
            DDL_SCHEDULE.SelectedValue = conn.GetFieldValue("SCHEDULE_CODE").ToString();
            DDL_SUBCODE.SelectedValue = conn.GetFieldValue("SUB_CODE").ToString();

        }
    }
}
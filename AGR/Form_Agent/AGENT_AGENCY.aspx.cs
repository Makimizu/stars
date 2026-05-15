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
    public partial class AGENT_AGENCY : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                LB_ID.Text = Request.QueryString["code"];
                Setup();
                LoadRecord();
            }

        }

        protected void Setup()
        {



            conn.QueryString = "select SUB_CODE, DESCR =  DESCR +' ('+ SUB_CODE + ')' from PARAM_SUB_CHANNEL_DISTRIBUTION";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SUB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


        }

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_M_AGENT_AGENCY '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_M_AGENT_AGENCY_INSERT " +
                                         "@CODE = '" + LB_ID.Text + "'," +
                                         "@START_DATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "'," +
                                         "@END_DATE = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "'," +
                                         "@UPLINER = '" + TXT_UPLINER.Text + "'," +
                                         "@SUB_CODE = '" + DDL_SUB.SelectedValue + "'," +
                                         "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                LoadRecord();
            }
            catch
            {
                LB_ERROR.Text = "<BR> UPLINER CODE can not be blank <BR> UPLINER CODE might be wrong";
            }
        }
    }
}
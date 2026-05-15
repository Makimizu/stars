using System;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class ENDORSEMENT_ARCHIEVE_ARCHIEVE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string MODE = Request.QueryString["MODE"].ToString();
                string ID = Request.QueryString["ID"].ToString();
                LoadTitle(ID, MODE);
            }
        }

        protected void LoadTitle(string ID, string MODE)
        {
            string ENDORSEMENT_TYPE = "";

            conn.QueryString = "select DESCR from PR_BATCH_TYPE where CODE = '" + MODE + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                ENDORSEMENT_TYPE = conn.GetFieldValue("DESCR").ToString();
                conn.QueryString = "select RECORDS from V_BATCH_ENDORSEMENT where BATCH_ID = '" + ID + "'";
                conn.ExecuteQuery();

                LB_TITLE.Text = "<table style='border-spacing:0px;font-size:xx-small;'>" +
                                "<tr><td>BATCH ID</td><td><B>" + ID + "</B></td></tr>" +
                                "<tr><td style='width:100px;'>ENDORSEMENT TYPE</td><td><B>" + ENDORSEMENT_TYPE + "</B></td></tr>" +
                                "<tr><td>#RECORDS</td><td><B>" + conn.GetFieldValue("RECORDS").ToString() + "</B></td></tr>" +
                                "</table>";
            }
            else
            {
                conn.QueryString = "select DESCR from PARAM_ENDORSEMENT_TYPE where CODE = '" + MODE + "'";
                conn.ExecuteQuery();
                ENDORSEMENT_TYPE = conn.GetFieldValue("DESCR").ToString();

                conn.QueryString = "select FULLNAME from V_M_AGENTS where CODE = '" + ID + "'";
                conn.ExecuteQuery();

                LB_TITLE.Text = "<table style='border-spacing:0px;font-size:xx-small;'>" +
                                "<tr><td style='width:100px;'>ENDORSEMENT TYPE</td><td><B>" + ENDORSEMENT_TYPE + "</B></td></tr>" +
                                "<tr><td>AGENT CODE</td><td><B>" + ID + "</B></td></tr>" +
                                "<tr><td>AGENT NAME</td><td><B>" + conn.GetFieldValue("FULLNAME").ToString() + "</B></td></tr>" +
                                "</table>";
            }
        }
    }
}
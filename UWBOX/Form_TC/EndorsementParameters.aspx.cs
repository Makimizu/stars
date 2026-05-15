using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace UWBOX.Form_TC
{
    public partial class EndorsementParameters : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PREFIX.Text = Request.QueryString["prefix"];
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
            }
        }

        protected void Setup()
        {
            LB_PARAM.Text = LB_PREFIX.Text.Replace("_", " ").ToUpper();

            conn.QueryString = "select CODE,DESCR from PR_DATA_TYPE";
            conn.ExecuteQuery();

            DDL_DATA_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_DATA_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 1).ToString()));
            }

            DGR1.CurrentPageIndex = 0;
            FillGrid1();
            FillRecordGrid("");
        }


        protected void FillGrid1()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "DATA_TYPE, " +
                                "SQL_REFF " +
                                "from PARAM_ENDORSEMENT_DETAIL where ENDORSEMENT_CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR1.DataSource = dt;
            DGR1.DataBind();
        }

        protected void FillRecordGrid(string KODE)
        {
            conn.QueryString = "select CODE,DESCR,DATA_TYPE,SQL_REFF " +
                    "from PARAM_ENDORSEMENT_DETAIL where ENDORSEMENT_CODE = '" + LB_CODE.Text + "' and CODE  = '" + KODE + "'";
            conn.ExecuteQuery();

            TXT_CODE.Text = conn.GetFieldValue("CODE").ToString();
            TXT_CODE.Enabled = false;
            TXT_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            try
            {
                DDL_DATA_TYPE.SelectedValue = conn.GetFieldValue("DATA_TYPE").ToString();
            }
            catch { }
            TXT_SQL_REFF.Text = conn.GetFieldValue("SQL_REFF").ToString();
        }

        protected void DGR1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR1.CurrentPageIndex = e.NewPageIndex;
            //FillGrid1(LB_CODE.Text);
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                FillRecordGrid(e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {

            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            TXT_CODE.Text = "";
            TXT_CODE.Enabled = true;
            TXT_DESCR.Text = "";
            TXT_DESCR.Enabled = true;
            DDL_DATA_TYPE.SelectedIndex = 0;
            DDL_DATA_TYPE.Enabled = true;
            TXT_SQL_REFF.Text = "";
            TXT_SQL_REFF.Enabled = true;
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            string sql = "";

            if (TXT_CODE.Enabled != true)
            {
                sql = "update PARAM_ENDORSEMENT_DETAIL set DESCR = '" + TXT_DESCR.Text + "', DATA_TYPE = '" + DDL_DATA_TYPE.SelectedValue + "'" +
                    ",SQL_REFF = '" + TXT_SQL_REFF.Text + "', LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'" +
                    ",LASTCHANGEDATE = GetDate() where ENDORSEMENT_CODE = '" + LB_CODE.Text + "' and CODE = '" + TXT_CODE.Text + "'";
            }
            else
            {
                sql = "insert into PARAM_ENDORSEMENT_DETAIL values ('" + LB_CODE.Text + "','" + TXT_CODE.Text + "'," +
                            "'" + TXT_DESCR.Text + "','" + DDL_DATA_TYPE.SelectedValue + "','" + TXT_SQL_REFF.Text + "', " +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate())";
            }

            try
            {
                conn.QueryString = sql;
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            FillGrid1();
        }

    }
}
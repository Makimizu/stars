using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace REAS.Form_App
{
    public partial class ApplicationList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from COMPANY order by 2";
            conn.ExecuteQuery();
            DDL_REINS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REINS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            DDL_PRODUCT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (DDL_REINS.SelectedValue != "")
                where = where + " and a.REINSURANCE_CODE = '" + DDL_REINS.SelectedValue + "' ";

            if (DDL_PRODUCT.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP_CODE = '" + DDL_PRODUCT.SelectedValue + "' ";

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "REGNO, " +
                                "SEQ, " +
                                "TC_ID, " +
                                "FULLNAME, " +
                                "PRODUCT_GROUP_DESCR, " +
                                "DOB = convert(varchar(20), DOB, 106), " +
                                "SEX, " +
                                "START_AGE, " +
                                "PERIOD = convert(varchar(20), START_DATE, 106) + ' - ' + convert(varchar(20), END_DATE, 106), " +
                                "SUMINS = replace(convert(varchar(100),convert(money,SUMINS),1),'.00',''), " +
                                "REINSURANCE " +
                                "from V_APPLICATION_MASTER a " +
                                "where " +
                                "1=1 " + where +
                                "order by " +
                                "a.CREATEDATE desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");

                lbCODE.Text = DGR.Items[i].Cells[4].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
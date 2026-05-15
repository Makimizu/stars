using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_History_Endorsement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TIPE_ENDORSEMENT";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void FillDGR()
        {
            string field = "";
            string where = " 1=1 ";

            where = where + "and STATUS='" + DDL_STATUS.SelectedValue + "' ";

            if (TXT_ID.Text.Trim() != "")
                where = where + "and BATCH_ID like '%" + TXT_ID.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICY_NO.Text.Trim() != "")
                where = where + "and POLICY_NO like '%" + TXT_POLICY_NO.Text.Trim() + "%' ";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + "and DOCNO like '%" + TXT_DOCNO.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + "and TIPE_ENDORS='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_DATE1.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                string date1 = "1 jan 1980";
                string date2 = "31 dec 2030";
                if (TXT_DATE1.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                where = where + "and (convert(date,PROS_END) between convert(date,'" + date1 + "') and convert(date,'" + date2 + "')) ";
            }

            if (TXT_DATE_STATUS1.Text.Trim() != "" || TXT_DATE_STATUS2.Text.Trim() != "")
            {
                string date1 = "1 jan 1980";
                string date2 = "31 dec 2030";
                if (TXT_DATE_STATUS1.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE_STATUS1.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE_STATUS2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE_STATUS2.Text.Trim(), "d/M/yyyy");

                if (DDL_STATUS.SelectedValue == "1")
                    where = where + "and (convert(date,APRV_END) between convert(date,'" + date1 + "') and convert(date,'" + date2 + "')) ";
                else
                    where = where + "and (convert(date,REG_END) between convert(date,'" + date1 + "') and convert(date,'" + date2 + "')) ";
            }

            if (DDL_STATUS.SelectedValue == "1")
                field = "APRV_END";
            else
                field = "REG_END";

            where = where + "and " + field + " is not null ";

            conn.QueryString = "select  " +
                                "BATCH_ID, " +
                                "TIPE_ENDORS, " +
                                "TIPE_ENDORS_DESCR, " +
                                "DOCNO, " +
                                "CNT, " +
                                "COMPANY_NAME, " +
                                "TGL_BATCH, " +
                                "TGL_STATUS = " + field + ", " +
                                "REPORT_URL, " +
                                "REPORT_SSRS_URL " +
                                "from V_GPA_ENDORSEMENT_BATCH " +
                                "where " +
                                where +
                                "order by TGL_BATCH desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btView = (Button)DGR.Items[i].FindControl("BT_VIEW");
                Button btReport = (Button)DGR.Items[i].FindControl("BT_REPORT");
                
                btView.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[9].Text + "','GPA_ENDORSEMENT','height=450,width=900,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btReport.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text + "','GPA_ENDORSEMENT','height=450,width=900,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }

        }
    }
}
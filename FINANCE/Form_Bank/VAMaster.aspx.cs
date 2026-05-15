using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class VAMaster : System.Web.UI.Page
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
            conn.QueryString = "select distinct a.NOREK, b.BANK from V_REKENING_MASTER_VA a inner join REKENING_MASTER b on a.NOREK=b.NOREK";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_ACCNAME.Text.Trim() != "")
                where = where + " and a.ACCNAME like '%" + TXT_ACCNAME.Text.Trim() + "%' ";

            if (TXT_ACCNO.Text.Trim() != "")
                where = where + " and a.ACCNO like '%" + TXT_ACCNO.Text.Trim() + "%' ";

            if (TXT_CREATEDATE.Text.Trim() != "")
                where = where + " and convert(date,a.CREATEDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CREATEDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CREATEDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.CREATEDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CREATEDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "ACCNO, " +
                                "ACCNAME, " +
                                "BANK, " +
                                "CREATEDATE, " +
                                "NOREK, " +
                                "FIRST_TAKENBY, " +
                                "FIRST_TAKENDATE, " +
                                "LAST_TAKENBY, " +
                                "LAST_TAKENDATE " +
                                "from V_REKENING_MASTER_VA a " +
                                "where " +
                                "a.NOREK = '" + DDL_NOREK.SelectedValue + "' " + DDL_TAKEN.SelectedValue + where +
                                "order by " +
                                "a.CREATEDATE desc, a.ACCNO desc";

            conn.ExecuteQuery();
            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_EXPORT_Click(object sender, EventArgs e)
        {
            string VA = "";
            string sql = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    VA = VA + DGR.Items[i].Cells[1].Text.Trim() + ",";
                    sql = sql +  "exec SP_REKENING_MASTER_VA_UPSERT " +
                                    "'" + DDL_NOREK.SelectedValue + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
                }
            }

            if (VA.Trim().Length == 0)
                return;

            try
            {
                conn.QueryString = "exec RPT_REKENING_MASTER_VA " +
                                    "'" + DDL_NOREK.SelectedValue + "'," +
                                    "'" + VA + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                conn.QueryString = sql;
                conn.ExecuteNonQuery();
                FillDGR();

                GlobalUse.ExportDataSetToExcel(dt, this, "VA.xls", false);

            }
            catch 
            {
                
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}
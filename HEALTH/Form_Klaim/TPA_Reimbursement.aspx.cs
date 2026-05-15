using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class TPA_Reimbursement : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PARAM_ACT_TPA where CODE not in ('01') order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select DEFDATE = convert(varchar(20),GETDATE(),103)";
            conn.ExecuteQuery();
            TXT_APRDATE1.Text = conn.GetFieldValue("DEFDATE").ToString();
            TXT_APRDATE2.Text = conn.GetFieldValue("DEFDATE").ToString();

            FillDDLReport();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_APRDATE1.Text.Trim() != "")
                where = where + "and convert(date,a.APPROVEDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE1.Text.Trim(), "d/M/yyyy") + "') ";
            if (TXT_APRDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.APPROVEDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "CLAIM_NO, " +
                                "APPROVEDATE = convert(varchar(20),APPROVEDATE,106), " +
                                "NAMA, " +
                                "POLICY_NO, " +
                                "COMPANY, " +
                                "INCURRED = replace(convert(varchar(100),convert(money,INCURRED),1),'.00',''), " +
                                "UNPAID = replace(convert(varchar(100),convert(money,UNPAID),1),'.00',''), " +
                                "PAID = replace(convert(varchar(100),convert(money,PAID),1),'.00','') " +
                                "from V_CLM_TPA_REIMBURSEMENT a " +
                                "where " +
                                "TPA = '" + DDL_TPA.SelectedValue + "' " + where +
                                "order by a.APPROVEDATE";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();
            if (conn.GetRowCount() > 0)
            {
                BT_REPORT.Visible = true;
                DDL_REPORT.Visible = true;
            }

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = true;
            }
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            LB_APPDATE1.Text = "";
            LB_APPDATE2.Text = "";
            BT_REPORT.Visible = false;
            DDL_REPORT.Visible = false;

            if (TXT_APRDATE1.Text.Trim() == "" || TXT_APRDATE2.Text.Trim() == "")
                return;

            try
            {
                LB_APPDATE1.Text = GlobalUse.GlobalDateFormat(TXT_APRDATE1.Text.Trim(), "d/M/yyyy");
                LB_APPDATE2.Text = GlobalUse.GlobalDateFormat(TXT_APRDATE2.Text.Trim(), "d/M/yyyy");
                FillDGR();
            }
            catch
            {
                LB_APPDATE1.Text = "";
                LB_APPDATE2.Text = "";
            }
        }

        protected void FillDDLReport()
        {
            DDL_REPORT.Items.Clear();
            conn.QueryString = "select URL,TIPE from V_TPA_REPORT_REIMBURSEMENT where CODE='" + DDL_TPA.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REPORT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_TPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLReport();
        }

        protected void BT_REPORT_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (LB_APPDATE1.Text == "" || LB_APPDATE2.Text == "" || DDL_REPORT.SelectedValue == "")
                return;

            try
            {
                if (DDL_REPORT.SelectedValue.Substring(0, 4) != "exec")
                {
                    conn.QueryString = "select " +
                                    "STARTDATE = convert(varchar(20),'" + LB_APPDATE1.Text + "',112), " +
                                    "ENDDATE = convert(varchar(20),'" + LB_APPDATE2.Text + "',112)";
                    conn.ExecuteQuery();

                    string URL = DDL_REPORT.SelectedValue + "&STARTDATE=" + conn.GetFieldValue("STARTDATE").ToString() + "&ENDDATE=" + conn.GetFieldValue("ENDDATE").ToString();
                    Response.Redirect(URL);
                }
                else
                {
                    bool bFound = false;
                    string CLAIM_NO = "";

                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                        if (cb.Checked)
                        {
                            bFound = true;
                            CLAIM_NO = CLAIM_NO + DGR.Items[i].Cells[1].Text + ",";
                        }
                    }

                    if (!bFound)
                        return;

                    conn.QueryString = "exec SP_CLM_TPA_REIMBURSEMENT_FILENAME " +
                                        "'" + DDL_TPA.SelectedValue + "'," +
                                        "'" + DDL_REPORT.SelectedItem.Text + "'," +
                                        "'" + LB_APPDATE1.Text + "'," +
                                        "'" + LB_APPDATE2.Text + "'";
                    conn.ExecuteQuery();
                    string filename = conn.GetFieldValue("FILENAME").ToString();
                                        
                    string SQL = DDL_REPORT.SelectedValue + " '" + CLAIM_NO + "'";
                    conn.QueryString = SQL;
                    conn.ExecuteQuery();

                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();

                    GlobalUse.ToCSV(dt, this, filename, false, "\"", ",");
                }
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            bool bCheck = ((CheckBox)sender).Checked;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = bCheck;
            }
        }
    }
}
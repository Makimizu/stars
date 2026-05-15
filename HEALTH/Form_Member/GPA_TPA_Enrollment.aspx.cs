using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_TPA_Enrollment : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }
  
        protected void Setup()
        {
            conn.QueryString = "select DAY1 = convert(varchar(20),GETDATE() - day(GETDATE()) + 1,103)";
            conn.ExecuteQuery();
            TXT_DATE1.Text = conn.GetFieldValue("DAY1").ToString();

            conn.QueryString = "select CODE,DESCR from PR_TIPE_ENROLLMENT order by CREATEDATE";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PARAM_ACT_TPA order by DESCR";
            conn.ExecuteQuery();
            DDL_TPA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + "and TIPE = '" + DDL_TIPE.SelectedValue + "' ";

            if (DDL_TPA.SelectedValue != "")
                where = where + "and TPA = '" + DDL_TPA.SelectedValue + "' ";

            if (TXT_DATE1.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                string date1 = "1 jan 1980";
                string date2 = "31 dec 2030";
                if (TXT_DATE1.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                where = where + "and (convert(date,CREATEDATE) between convert(date,'" + date1 + "') and convert(date,'" + date2 + "')) ";
            }

            conn.QueryString = "select " +
                                "BATCH_ID, " +
                                "TIPE, " +
                                "TPA, " +
                                "TRACK, " +
                                "COMPANY_NAME, " +
                                "TIPE_DESCR, " +
                                "TPA_DESCR, " +
                                "MEMBER, " +
                                "CREATEDATE = convert(varchar(30),CREATEDATE,106) + ' ' + convert(varchar(30),CREATEDATE,108), " +
                                "URL, " +
                                "TANGGAL_ENROLL, " +
                                "STATUS_ENROLL, " +
                                "MESSAGE_ENROLL, " +
                                "COMPANY_CODE " +
                                "from V_TPA_MEMBER_ENROLLMENT a " +
                                "where " +
                                "TRACK in (1,35) " +
                                where +
                                "order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btView = (Button)DGR.Items[i].FindControl("BT_VIEW");

                if (DGR.Items[i].Cells[2].Text == "N" || DGR.Items[i].Cells[2].Text == "R")
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;

                if (DGR.Items[i].Cells[4].Text == "35")
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "ViewMsg")
            {
                string msg = e.CommandArgument.ToString();
                string companyCode = e.Item.Cells[14].Text.Replace("&nbsp;", "");
                string companyName = e.Item.Cells[5].Text.Replace("&nbsp;", "");

                // tampilkan di label modal
                MessageEnrollTitle.InnerText = companyCode + " - " + companyName;
                LblMessageDetail.Text = msg.Replace("\n", "<br/>");

                // show modal
                ModalMessage.Show();
            }
            if (e.CommandName == "Enroll")
            {
                if (e.Item.Cells[2].Text.Replace("&nbsp;", "") == "N" || e.Item.Cells[2].Text.Replace("&nbsp;", "") == "R")
                {
                    try
                    {
                        conn.QueryString = "exec SP_POLICY_PERIOD_NBRN_ACTIVITY_INSERT " +
                                            "'" + e.Item.Cells[1].Text + "'," +
                                            "3," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                if (e.Item.Cells[10].Text.Replace("&nbsp;", "").Substring(0, 4) != "exec")
                {
                    Response.Redirect(e.Item.Cells[10].Text.Replace("&nbsp;", ""));
                }
                else
                {
                    string SQL = e.Item.Cells[10].Text.Replace("&nbsp;", "");

                    conn.QueryString = SQL;
                    conn.ExecuteQuery();

                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();

                    GlobalUse.ToCSV(dt, this, e.Item.Cells[1].Text + ".txt", false, "\"", ",");
                }
            }
        }
    }
}
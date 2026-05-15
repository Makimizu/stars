using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class InquiryScreen : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                FillDGRPARAM();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select * from SC_MENU_INQUIRY where MENU_CODE ='" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("URL_SCREEN_NEW").ToString() != "")
            {
                BT_NEW.Visible = true;
                LB_NEW.Text = conn.GetFieldValue("URL_SCREEN_NEW").ToString();
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                
            }
        }

        protected void FillDGRPARAM()
        {
            conn.QueryString = "select * from SC_MENU_INQUIRY_DETAIL where MENU_CODE ='" + LB_CODE.Text + "' order by SEQ";
            conn.ExecuteQuery();
                        
            DataTable dt, dt2;
            dt = new DataTable();
            dt2 = new DataTable();
            dt = conn.GetDataTable().Copy();
            dt2 = conn.GetDataTable().Copy();

            double Ahalfrows = System.Math.Ceiling(((double)(dt.Rows.Count)) / 2);

            while (Ahalfrows < dt.Rows.Count)
            {   
                dt.Rows.RemoveAt((int)Ahalfrows);
            }

            for (int i = 0; i < (int)Ahalfrows; i++)
            {   
                dt2.Rows.RemoveAt(0);
            }

            DGR_PARAM.DataSource = dt;
            DGR_PARAM.DataBind();
            DGR_PARAM2.DataSource = dt2;
            DGR_PARAM2.DataBind();

            

            for (int i = 0; i < DGR_PARAM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_PARAM");
                TextBox txtDate = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_DATE");
                TextBox txtDate2 = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_DATE2");
                DropDownList ddl = (DropDownList)DGR_PARAM.Items[i].FindControl("DDL_PARAM");

                if (DGR_PARAM.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    txt.Visible = false;
                    ddl.Visible = true;

                    string sql = DGR_PARAM.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    ddl.Items.Clear();
                    //ddl.Items.Add(new ListItem("-- ALL --", ""));
                    conn.QueryString = sql;
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                }
                else
                {
                    if (DGR_PARAM.Items[i].Cells[2].Text == "DATE" || DGR_PARAM.Items[i].Cells[2].Text == "DATE1" || DGR_PARAM.Items[i].Cells[2].Text == "DATE2" || DGR_PARAM.Items[i].Cells[2].Text == "DATE3")
                    {
                        txt.Visible = false;
                        txtDate.Visible = true;
                        txtDate2.Visible = true;
                    }

                    if (txt.Visible)
                    {
                        if (DGR_PARAM.Items[i].Cells[2].Text == "NUMBER")
                        {
                            txt.Width = 150;
                        }
                    }
                }
            }

            for (int i = 0; i < DGR_PARAM2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_PARAM");
                TextBox txtDate = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_DATEa");
                TextBox txtDate2 = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_DATEa2");
                DropDownList ddl = (DropDownList)DGR_PARAM2.Items[i].FindControl("DDL_PARAM");

                if (DGR_PARAM2.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    txt.Visible = false;
                    ddl.Visible = true;

                    string sql = DGR_PARAM2.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    ddl.Items.Clear();
                    //ddl.Items.Add(new ListItem("-- ALL --", ""));
                    conn.QueryString = sql;
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                }
                else
                {
                    if (DGR_PARAM2.Items[i].Cells[2].Text == "DATE" || DGR_PARAM2.Items[i].Cells[2].Text == "DATE1" || DGR_PARAM2.Items[i].Cells[2].Text == "DATE2" || DGR_PARAM2.Items[i].Cells[2].Text == "DATE3")
                    {
                        txt.Visible = false;
                        txtDate.Visible = true;
                        txtDate2.Visible = true;
                    }

                    if (txt.Visible)
                    {
                        if (DGR_PARAM2.Items[i].Cells[2].Text == "NUMBER")
                        {
                            txt.Width = 150;
                        }
                    }
                }
            }
        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_SQL.Text = "";
            LB_RECORD.Text = "";

            conn.QueryString = "select V_SQL from SC_MENU_INQUIRY where MENU_CODE ='" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            string SQL = conn.GetFieldValue(0, 0).ToString();
            string where = "";

            for (int i = 0; i < DGR_PARAM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_PARAM");
                TextBox txtDate = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_DATE");
                TextBox txtDate2 = (TextBox)DGR_PARAM.Items[i].FindControl("TXT_DATE2");
                DropDownList ddl = (DropDownList)DGR_PARAM.Items[i].FindControl("DDL_PARAM");

                if (txt.Visible)
                {
                    switch (DGR_PARAM.Items[i].Cells[2].Text)
                    {
                        case "NUMBER": if (txt.Text.Trim() != "")
                            {
                                where = where + " and " + DGR_PARAM.Items[i].Cells[0].Text + "='" + txt.Text.Trim().Replace(",", "") + "' ";
                            }
                            break;
                        case "TXT": if (txt.Text.Trim() != "")
                            {
                                where = where + " and " + DGR_PARAM.Items[i].Cells[0].Text + " like '%" + txt.Text.Trim() + "%' ";
                            }
                            break;
                    }
                }

                if (ddl.Visible && ddl.SelectedValue != "")
                {
                    where = where + " and " + DGR_PARAM.Items[i].Cells[0].Text + "='" + ddl.SelectedValue + "' ";
                }

                if (txtDate.Visible)
                {
                    if (txtDate.Text.Trim() == "" && txtDate.Text.Trim() == "")
                        continue;

                    string sDate = "1 jan 1900";
                    string sDate2 = DateTime.Now.AddYears(10).ToShortDateString();

                    if (txtDate.Text.Trim() != "")
                        sDate = GlobalUse.GlobalDateFormat(txtDate.Text.Trim(), "d/M/yyyy");
                    if (txtDate2.Text.Trim() != "")
                        sDate2 = GlobalUse.GlobalDateFormat(txtDate2.Text.Trim(), "d/M/yyyy");

                    where = where + "and datediff(day,'" + sDate + "'," + DGR_PARAM.Items[i].Cells[0].Text + ") >= 0 and datediff(day," + DGR_PARAM.Items[i].Cells[0].Text + ",'" + sDate2 + "') >= 0 ";

                }
            }

            for (int i = 0; i < DGR_PARAM2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_PARAM");
                TextBox txtDate = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_DATEa");
                TextBox txtDate2 = (TextBox)DGR_PARAM2.Items[i].FindControl("TXT_DATEa2");
                DropDownList ddl = (DropDownList)DGR_PARAM2.Items[i].FindControl("DDL_PARAM");

                if (txt.Visible)
                {
                    switch (DGR_PARAM2.Items[i].Cells[2].Text)
                    {
                        case "NUMBER": if (txt.Text.Trim() != "")
                            {
                                where = where + " and " + DGR_PARAM2.Items[i].Cells[0].Text + "='" + txt.Text.Trim().Replace(",", "") + "' ";
                            }
                            break;
                        case "TXT": if (txt.Text.Trim() != "")
                            {
                                where = where + " and " + DGR_PARAM2.Items[i].Cells[0].Text + " like '%" + txt.Text.Trim() + "%' ";
                            }
                            break;
                    }
                }

                if (ddl.Visible && ddl.SelectedValue != "")
                {
                    where = where + " and " + DGR_PARAM2.Items[i].Cells[0].Text + "='" + ddl.SelectedValue + "' ";
                }

                if (txtDate.Visible)
                {
                    if (txtDate.Text.Trim() == "" && txtDate.Text.Trim() == "")
                        continue;

                    string sDate = "1 jan 1900";
                    string sDate2 = DateTime.Now.AddYears(10).ToShortDateString();

                    if (txtDate.Text.Trim() != "")
                        sDate = GlobalUse.GlobalDateFormat(txtDate.Text.Trim(), "d/M/yyyy");
                    if (txtDate2.Text.Trim() != "")
                        sDate2 = GlobalUse.GlobalDateFormat(txtDate2.Text.Trim(), "d/M/yyyy");

                    where = where + "and datediff(day,'" + sDate + "'," + DGR_PARAM2.Items[i].Cells[0].Text + ") >= 0 and datediff(day," + DGR_PARAM2.Items[i].Cells[0].Text + ",'" + sDate2 + "') >= 0 ";

                }
            }

            if (LB_CODE.Text == "8070" || LB_CODE.Text == "8052")
            {
                conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' and ROLE_CODE in (select code from PR_USER_ACCESS_POLICYNO)";
                conn.ExecuteQuery(120);
                string ROLECODE = conn.GetFieldValue("ROLE_CODE").ToString();
                if (!String.IsNullOrEmpty(ROLECODE))
                {
                    where = where + " and POLICY_NO in (select [DESCR] from [PR_USER_ACCESS_POLICYNO] where CODE = '" + ROLECODE + "') ";
                }
            }

            SQL = SQL.Replace("1=1", "1=1 " + where);

            conn.QueryString = SQL;
            conn.ExecuteQuery(120);

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            if (conn.GetRowCount() > 0)
            {
                BT_XLS.Visible = true;
                LB_SQL.Text = SQL;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select URL_SCREEN, URL_PARAM from SC_MENU_INQUIRY where MENU_CODE ='" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            string URL = conn.GetFieldValue("URL_SCREEN").ToString() + "?" + conn.GetFieldValue("URL_PARAM").ToString() + "=";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DGR.Items[i].Cells[0].Text = "<a href='" + URL + DGR.Items[i].Cells[0].Text + "'>" + DGR.Items[i].Cells[0].Text + "</a>";
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect(LB_NEW.Text);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = LB_SQL.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "DATA", true);
        }
    }
}
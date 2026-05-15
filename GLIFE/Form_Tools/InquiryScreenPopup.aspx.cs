using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GLIFE.Form_Tools
{
    public partial class InquiryScreenPopup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                LB_PARENT.Text = Request.QueryString["parent"];
                LB_TARGETCODE.Text = Request.QueryString["targetcode"];
                LB_TARGETDESCR.Text = Request.QueryString["targetdescr"];
                Setup();
                FillDGRPARAM();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select * from SC_MENU_INQUIRY where MENU_CODE ='" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
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

            SQL = SQL.Replace("1=1", "1=1 " + where);

            conn.QueryString = SQL;
            conn.ExecuteQuery();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            string mode = "";
            switch (LB_PARENT.Text)
            {
                case "0": mode = "forms[0]"; break;
                case "1": mode = "form1"; break;
            }


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                string script = "window.opener.document." + mode + "." + LB_TARGETCODE.Text + ".value = '" + DGR.Items[i].Cells[0].Text + "'; " +
                                //"window.opener.document." + mode + "." + LB_TARGETDESCR.Text + ".value = '" + DGR.Items[i].Cells[1].Text + "'; " +
                                "window.close();";

                DGR.Items[i].Cells[0].Text = "<a id=\"myLink\" href=\"#\" onclick=\"" + script + ";return false;\">" + DGR.Items[i].Cells[0].Text + "</a>";
            }
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
    }
}
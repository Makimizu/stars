using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GO
{
    public partial class ParamHolidayCalendar : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadCalendar();
            }
        }

        protected void Setup()
        {
            DDL_YEAR.Items.Clear();
            conn.QueryString = "select distinct year(GETDATE()), year(HOLIDAY) from PARAM_HOLIDAY order by 2 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 1).ToString()));
            }

            try
            {
                DDL_YEAR.SelectedValue = conn.GetFieldValue(0, 0).ToString();
            }
            catch { }


        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        protected void LoadCalendar()
        {
            FillDGR();
            FillDGR01();
            FillDGR02();
            FillDGR03();
            FillDGR04();
            FillDGR05();
            FillDGR06();
            FillDGR07();
            FillDGR08();
            FillDGR09();
            FillDGR10();
            FillDGR11();
            FillDGR12();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "D = convert(varchar(100), a.HOLIDAY, 106), " +
                                "DN = DATENAME(dw, a.HOLIDAY), " +
                                "DESCR = UPPER(a.DESCR) " +
                                "from PARAM_HOLIDAY a " +
                                "where " +
                                "year(HOLIDAY) = " + DDL_YEAR.SelectedValue + " " +
                                "order by a.HOLIDAY";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            DGR.SelectedIndex = -1;
        }

        protected void FillDGR01()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR01.DataSource = dt;
            DGR01.DataBind();

            for (int i = 0; i < DGR01.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR01.Items[i].FindControl("LB_1");
                LinkButton lb2 = (LinkButton)DGR01.Items[i].FindControl("LB_2");
                LinkButton lb3 = (LinkButton)DGR01.Items[i].FindControl("LB_3");
                LinkButton lb4 = (LinkButton)DGR01.Items[i].FindControl("LB_4");
                LinkButton lb5 = (LinkButton)DGR01.Items[i].FindControl("LB_5");
                LinkButton lb6 = (LinkButton)DGR01.Items[i].FindControl("LB_6");
                LinkButton lb7 = (LinkButton)DGR01.Items[i].FindControl("LB_7");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR01.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR01.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR01.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR01.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR01.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR01.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR01.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR01.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR01.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR01.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR01.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR01.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR01.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR01.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR01.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR01.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR01.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR02()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR02.DataSource = dt;
            DGR02.DataBind();

            for (int i = 0; i < DGR02.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR02.Items[i].FindControl("LB_8");
                LinkButton lb2 = (LinkButton)DGR02.Items[i].FindControl("LB_9");
                LinkButton lb3 = (LinkButton)DGR02.Items[i].FindControl("LB_10");
                LinkButton lb4 = (LinkButton)DGR02.Items[i].FindControl("LB_11");
                LinkButton lb5 = (LinkButton)DGR02.Items[i].FindControl("LB_12");
                LinkButton lb6 = (LinkButton)DGR02.Items[i].FindControl("LB_13");
                LinkButton lb7 = (LinkButton)DGR02.Items[i].FindControl("LB_14");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR02.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR02.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR02.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR02.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR02.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR02.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR02.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR02.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR02.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR02.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR02.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR02.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR02.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR02.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR02.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR02.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR02.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR03()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",3";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR03.DataSource = dt;
            DGR03.DataBind();

            for (int i = 0; i < DGR03.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR03.Items[i].FindControl("LB_15");
                LinkButton lb2 = (LinkButton)DGR03.Items[i].FindControl("LB_16");
                LinkButton lb3 = (LinkButton)DGR03.Items[i].FindControl("LB_17");
                LinkButton lb4 = (LinkButton)DGR03.Items[i].FindControl("LB_18");
                LinkButton lb5 = (LinkButton)DGR03.Items[i].FindControl("LB_19");
                LinkButton lb6 = (LinkButton)DGR03.Items[i].FindControl("LB_20");
                LinkButton lb7 = (LinkButton)DGR03.Items[i].FindControl("LB_21");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR03.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR03.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR03.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR03.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR03.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR03.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR03.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR03.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR03.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR03.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR03.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR03.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR03.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR03.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR03.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR03.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR03.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR04()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",4";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR04.DataSource = dt;
            DGR04.DataBind();

            for (int i = 0; i < DGR04.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR04.Items[i].FindControl("LB_1");
                LinkButton lb2 = (LinkButton)DGR04.Items[i].FindControl("LB_2");
                LinkButton lb3 = (LinkButton)DGR04.Items[i].FindControl("LB_3");
                LinkButton lb4 = (LinkButton)DGR04.Items[i].FindControl("LB_4");
                LinkButton lb5 = (LinkButton)DGR04.Items[i].FindControl("LB_5");
                LinkButton lb6 = (LinkButton)DGR04.Items[i].FindControl("LB_6");
                LinkButton lb7 = (LinkButton)DGR04.Items[i].FindControl("LB_7");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR04.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR04.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR04.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR04.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR04.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR04.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR04.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR04.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR04.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR04.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR04.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR04.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR04.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR04.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR04.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR04.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR04.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR05()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",5";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR05.DataSource = dt;
            DGR05.DataBind();

            for (int i = 0; i < DGR05.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR05.Items[i].FindControl("LB_8");
                LinkButton lb2 = (LinkButton)DGR05.Items[i].FindControl("LB_9");
                LinkButton lb3 = (LinkButton)DGR05.Items[i].FindControl("LB_10");
                LinkButton lb4 = (LinkButton)DGR05.Items[i].FindControl("LB_11");
                LinkButton lb5 = (LinkButton)DGR05.Items[i].FindControl("LB_12");
                LinkButton lb6 = (LinkButton)DGR05.Items[i].FindControl("LB_13");
                LinkButton lb7 = (LinkButton)DGR05.Items[i].FindControl("LB_14");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR05.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR05.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR05.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR05.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR05.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR05.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR05.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR05.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR05.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR05.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR05.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR05.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR05.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR05.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR05.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR05.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR05.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR06()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",6";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR06.DataSource = dt;
            DGR06.DataBind();

            for (int i = 0; i < DGR06.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR06.Items[i].FindControl("LB_15");
                LinkButton lb2 = (LinkButton)DGR06.Items[i].FindControl("LB_16");
                LinkButton lb3 = (LinkButton)DGR06.Items[i].FindControl("LB_17");
                LinkButton lb4 = (LinkButton)DGR06.Items[i].FindControl("LB_18");
                LinkButton lb5 = (LinkButton)DGR06.Items[i].FindControl("LB_19");
                LinkButton lb6 = (LinkButton)DGR06.Items[i].FindControl("LB_20");
                LinkButton lb7 = (LinkButton)DGR06.Items[i].FindControl("LB_21");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR06.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR06.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR06.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR06.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR06.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR06.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR06.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR06.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR06.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR06.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR06.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR06.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR06.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR06.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR06.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR06.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR06.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR07()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",7";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR07.DataSource = dt;
            DGR07.DataBind();

            for (int i = 0; i < DGR07.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR07.Items[i].FindControl("LB_1");
                LinkButton lb2 = (LinkButton)DGR07.Items[i].FindControl("LB_2");
                LinkButton lb3 = (LinkButton)DGR07.Items[i].FindControl("LB_3");
                LinkButton lb4 = (LinkButton)DGR07.Items[i].FindControl("LB_4");
                LinkButton lb5 = (LinkButton)DGR07.Items[i].FindControl("LB_5");
                LinkButton lb6 = (LinkButton)DGR07.Items[i].FindControl("LB_6");
                LinkButton lb7 = (LinkButton)DGR07.Items[i].FindControl("LB_7");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR07.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR07.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR07.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR07.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR07.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR07.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR07.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR07.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR07.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR07.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR07.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR07.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR07.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR07.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR07.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR07.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR07.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR08()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",8";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR08.DataSource = dt;
            DGR08.DataBind();

            for (int i = 0; i < DGR08.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR08.Items[i].FindControl("LB_8");
                LinkButton lb2 = (LinkButton)DGR08.Items[i].FindControl("LB_9");
                LinkButton lb3 = (LinkButton)DGR08.Items[i].FindControl("LB_10");
                LinkButton lb4 = (LinkButton)DGR08.Items[i].FindControl("LB_11");
                LinkButton lb5 = (LinkButton)DGR08.Items[i].FindControl("LB_12");
                LinkButton lb6 = (LinkButton)DGR08.Items[i].FindControl("LB_13");
                LinkButton lb7 = (LinkButton)DGR08.Items[i].FindControl("LB_14");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR08.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR08.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR08.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR08.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR08.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR08.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR08.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR08.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR08.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR08.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR08.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR08.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR08.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR08.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR08.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR08.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR08.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR09()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",9";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR09.DataSource = dt;
            DGR09.DataBind();

            for (int i = 0; i < DGR09.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR09.Items[i].FindControl("LB_15");
                LinkButton lb2 = (LinkButton)DGR09.Items[i].FindControl("LB_16");
                LinkButton lb3 = (LinkButton)DGR09.Items[i].FindControl("LB_17");
                LinkButton lb4 = (LinkButton)DGR09.Items[i].FindControl("LB_18");
                LinkButton lb5 = (LinkButton)DGR09.Items[i].FindControl("LB_19");
                LinkButton lb6 = (LinkButton)DGR09.Items[i].FindControl("LB_20");
                LinkButton lb7 = (LinkButton)DGR09.Items[i].FindControl("LB_21");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR09.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR09.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR09.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR09.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR09.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR09.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR09.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR09.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR09.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR09.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR09.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR09.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR09.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR09.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR09.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR09.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR09.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR10()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",10";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR10.DataSource = dt;
            DGR10.DataBind();

            for (int i = 0; i < DGR10.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR10.Items[i].FindControl("LB_1");
                LinkButton lb2 = (LinkButton)DGR10.Items[i].FindControl("LB_2");
                LinkButton lb3 = (LinkButton)DGR10.Items[i].FindControl("LB_3");
                LinkButton lb4 = (LinkButton)DGR10.Items[i].FindControl("LB_4");
                LinkButton lb5 = (LinkButton)DGR10.Items[i].FindControl("LB_5");
                LinkButton lb6 = (LinkButton)DGR10.Items[i].FindControl("LB_6");
                LinkButton lb7 = (LinkButton)DGR10.Items[i].FindControl("LB_7");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR10.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR10.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR10.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR10.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR10.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR10.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR10.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR10.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR10.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR10.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR10.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR10.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR10.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR10.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR10.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR10.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR10.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR11()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",11";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR11.DataSource = dt;
            DGR11.DataBind();

            for (int i = 0; i < DGR11.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR11.Items[i].FindControl("LB_8");
                LinkButton lb2 = (LinkButton)DGR11.Items[i].FindControl("LB_9");
                LinkButton lb3 = (LinkButton)DGR11.Items[i].FindControl("LB_10");
                LinkButton lb4 = (LinkButton)DGR11.Items[i].FindControl("LB_11");
                LinkButton lb5 = (LinkButton)DGR11.Items[i].FindControl("LB_12");
                LinkButton lb6 = (LinkButton)DGR11.Items[i].FindControl("LB_13");
                LinkButton lb7 = (LinkButton)DGR11.Items[i].FindControl("LB_14");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR11.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR11.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR11.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR11.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR11.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR11.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR11.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR11.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR11.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR11.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR11.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR11.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR11.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR11.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR11.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR11.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR11.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void FillDGR12()
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY_MONTH " + DDL_YEAR.SelectedValue + ",12";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR12.DataSource = dt;
            DGR12.DataBind();

            for (int i = 0; i < DGR12.Items.Count; i++)
            {
                LinkButton lb1 = (LinkButton)DGR12.Items[i].FindControl("LB_15");
                LinkButton lb2 = (LinkButton)DGR12.Items[i].FindControl("LB_16");
                LinkButton lb3 = (LinkButton)DGR12.Items[i].FindControl("LB_17");
                LinkButton lb4 = (LinkButton)DGR12.Items[i].FindControl("LB_18");
                LinkButton lb5 = (LinkButton)DGR12.Items[i].FindControl("LB_19");
                LinkButton lb6 = (LinkButton)DGR12.Items[i].FindControl("LB_20");
                LinkButton lb7 = (LinkButton)DGR12.Items[i].FindControl("LB_21");

                lb1.Visible = false;
                lb2.Visible = false;
                lb3.Visible = false;
                lb4.Visible = false;
                lb5.Visible = false;
                lb6.Visible = false;
                lb7.Visible = false;

                if (DGR12.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lb1.Visible = true;
                    lb1.Text = DGR12.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[9].Text.Replace("&nbsp;", "") != "")
                    {
                        lb1.ToolTip = DGR12.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                        lb1.ForeColor = System.Drawing.Color.Red;
                        lb1.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    lb2.Visible = true;
                    lb2.Text = DGR12.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[10].Text.Replace("&nbsp;", "") != "")
                    {
                        lb2.ToolTip = DGR12.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                        lb2.ForeColor = System.Drawing.Color.Red;
                        lb2.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lb3.Visible = true;
                    lb3.Text = DGR12.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                    {
                        lb3.ToolTip = DGR12.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                        lb3.ForeColor = System.Drawing.Color.Red;
                        lb3.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                {
                    lb4.Visible = true;
                    lb4.Text = DGR12.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                    {
                        lb4.ToolTip = DGR12.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                        lb4.ForeColor = System.Drawing.Color.Red;
                        lb4.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[6].Text.Replace("&nbsp;", "") != "")
                {
                    lb5.Visible = true;
                    lb5.Text = DGR12.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[13].Text.Replace("&nbsp;", "") != "")
                    {
                        lb5.ToolTip = DGR12.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                        lb5.ForeColor = System.Drawing.Color.Red;
                        lb5.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                {
                    lb6.Visible = true;
                    lb6.Text = DGR12.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                    {
                        lb6.ToolTip = DGR12.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                        lb6.ForeColor = System.Drawing.Color.Red;
                        lb6.Font.Bold = true;
                    }
                }

                if (DGR12.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    lb7.Visible = true;
                    lb7.Text = DGR12.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                    if (DGR12.Items[i].Cells[15].Text.Replace("&nbsp;", "") != "")
                    {
                        lb7.ToolTip = DGR12.Items[i].Cells[15].Text.Replace("&nbsp;", "");
                        lb7.ForeColor = System.Drawing.Color.Red;
                        lb7.Font.Bold = true;
                    }
                }
            }
        }

        protected void DateClick(DataGridCommandEventArgs e)
        {
            if (e.CommandName.Substring(0, 1) != "H")
                return;

            DGR.SelectedIndex = -1;

            string day = e.Item.Cells[int.Parse(e.CommandName.Substring(1, 1)) + 1].Text;
            LB_DATE.Text = e.Item.Cells[0].Text + "-" + e.Item.Cells[1].Text + "-" + day;
            DateTime dt = new DateTime(int.Parse(e.Item.Cells[0].Text), int.Parse(e.Item.Cells[1].Text), int.Parse(day));
            conn.QueryString = "select " +
                                "DESCR = UPPER(DESCR) " +
                                "from PARAM_HOLIDAY " +
                                "where " +
                                "HOLIDAY = '" + LB_DATE.Text + "'";
            conn.ExecuteQuery();
            LB_DATE_SHOWN.Text = (dt.ToString("dd MMMM yyyy")).ToUpper();
            TXT_DESCR.Text = conn.GetFieldValue("DESCR").ToString();

            string scrollscript = "";

            BT_DELETE.Visible = true;
            if (TXT_DESCR.Text.Trim() == "")
                BT_DELETE.Visible = false;
            else
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DateTime dgrdt = DateTime.Parse(DGR.Items[i].Cells[0].Text);
                    if (dt == dgrdt)
                    {
                        DGR.SelectedIndex = i;
                        float fheight = ((float)i)/DGR.Items.Count;
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "document.getElementById('DV').scrollTop = document.getElementById('DV').scrollHeight * " + fheight.ToString() + ";", true);
                        break;
                    }
                }
            }

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';" + scrollscript, true);
        }

        protected void DGR01_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR02_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR03_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR04_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR05_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR06_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR07_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR08_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR09_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR10_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR11_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void DGR12_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DateClick(e);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_DESCR.Text.Trim() == "")
                return;

            conn.QueryString = "delete from PARAM_HOLIDAY where datediff(day, HOLIDAY, '" + LB_DATE.Text + "')=0 " +
                                "insert into PARAM_HOLIDAY select " +
                                "'" + LB_DATE.Text + "','" + TXT_DESCR.Text.Trim().Replace("'", "`") + "'";
            conn.ExecuteNonQuery();
            LoadCalendar();
        }

        protected void BT_DELETE_Click(object sender, EventArgs e)
        {
            DeleteHoliday(LB_DATE.Text);
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if(e.CommandName == "Delete")
            {
                DeleteHoliday(e.Item.Cells[0].Text);
            }
        }

        protected void DeleteHoliday(string date)
        {
            conn.QueryString = "delete from PARAM_HOLIDAY where datediff(day, HOLIDAY, '" +date + "')=0 ";
            conn.ExecuteNonQuery();
            LoadCalendar();
        }
    }
}
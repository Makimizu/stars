using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace LIFE.Form_Parameter
{
    public partial class Parameter_Basic : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PARAM.Text = Request.QueryString["CODE"].ToString().Replace("PR_", "").Replace("_", " ");
                Setup();
            }
        }

        protected void Setup()
        {
            DGR1.CurrentPageIndex = 0;
            FillGrid1(LB_PARAM.Text);
            FillRecordGrid(LB_PARAM.Text);
        }

        protected void FillGrid1(string tablename)
        {
            conn.QueryString = "select * from PR_" + tablename.Replace(" ", "_");
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR1.DataSource = dt;
            DGR1.DataBind();
        }

        protected void FillRecordGrid(string tablename)
        {
            conn.QueryString = "exec SP_GET_TABLE_INFO 'PR_" + tablename.Replace(" ", "_") + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR2.DataSource = dt;
            DGR2.DataBind();
            for (int i = 0; i < DGR2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");
                if (DGR2.Items[i].Cells[1].Text != "61")
                {
                    txt.Visible = true;
                    txtdate.Visible = false;
                }
                else
                {
                    txt.Visible = false;
                    txtdate.Visible = true;
                }
            }
        }

        protected void DGR1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR1.CurrentPageIndex = e.NewPageIndex;
            FillGrid1("PR_" + LB_PARAM.Text.Replace(" ", "_"));
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                FillRecordGridValue(e.Item.Cells[1].Text);
            }
        }

        protected void FillRecordGridValue(string code)
        {
            try
            {
                conn.QueryString = "select * from " + "PR_" + LB_PARAM.Text.Replace(" ", "_") + " where CODE='" + code + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < DGR2.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                    TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");
                    if (DGR2.Items[i].Cells[1].Text == "167")
                        txt.MaxLength = int.Parse(DGR2.Items[i].Cells[2].Text);

                    if (DGR2.Items[i].Cells[1].Text != "61")
                        txt.Text = conn.GetFieldValue(0, DGR2.Items[i].Cells[0].Text).ToString();
                    else
                    {
                        DateTime dt = DateTime.Parse(conn.GetFieldValue(0, DGR2.Items[i].Cells[0].Text).ToString());
                        txtdate.Text = dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString();
                    }

                    if (DGR2.Items[i].Cells[4].Text == "1")
                    {
                        txt.Enabled = false;
                        txtdate.Enabled = false;
                    }
                }
            }
            catch { }
        }

        protected void DGR2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                for (int i = 0; i < DGR2.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                    TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");
                    txt.Text = "";
                    txt.Enabled = true;
                    txtdate.Text = "";
                    txtdate.Enabled = true;
                }
            }

            if (e.CommandName == "Submit")
            {
                LB_ERROR.Text = "";


                string sql = "";
                string sqlprimary = "";
                for (int i = 0; i < DGR2.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                    TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");

                    if (DGR2.Items[i].Cells[4].Text == "1")
                    {
                        sqlprimary = sqlprimary + DGR2.Items[i].Cells[0].Text + "='";
                        if (txt.Visible)
                            sqlprimary = sqlprimary + txt.Text + "'";
                        else
                            sqlprimary = sqlprimary + txtdate.Text + "'";

                        if (i < DGR2.Items.Count - 1)
                            sqlprimary = sqlprimary + " and ";
                    }
                }

                if (sqlprimary.Substring(sqlprimary.Length - 4, 4) == "and ")
                    sqlprimary = sqlprimary.Substring(0, sqlprimary.Length - 5);

                conn.QueryString = "select * from PR_" + LB_PARAM.Text.Replace(" ", "_") + " where " + sqlprimary;
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    for (int i = 0; i < DGR2.Items.Count; i++)
                    {
                        TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                        TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");

                        if (txt.Visible)
                            sql = sql + "'" + txt.Text + "',";
                        else
                            sql = sql + "'" + txtdate.Text + "',";
                    }

                    sql = "insert into PR_" + LB_PARAM.Text.Replace(" ", "_") + " values (" + sql +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate())";
                }
                else
                {
                    for (int i = 0; i < DGR2.Items.Count; i++)
                    {
                        TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_VAL");
                        TextBox txtdate = (TextBox)DGR2.Items[i].FindControl("TXT_DATE");

                        sql = sql + DGR2.Items[i].Cells[0].Text + "='";
                        if (txt.Visible)
                            sql = sql + txt.Text + "',";
                        else
                            sql = sql + txtdate.Text + "',";
                    }

                    sql = "update PR_" + LB_PARAM.Text.Replace(" ", "_") + " set " + sql +
                            "LASTCHANGEBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',LASTCHANGEDATE=GetDate() where " + sqlprimary;

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

                FillGrid1(LB_PARAM.Text);
            }
        }
    }
}
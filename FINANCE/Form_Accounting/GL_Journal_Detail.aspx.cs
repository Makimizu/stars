using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Accounting
{
    public partial class GL_Journal_Detail : System.Web.UI.Page
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

                LB_CODE.Text = Request.QueryString["CODE"];
                Setup();                
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select * " +
                                "from PARAM_GL_JOURNAL a " +
                                "where a.CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            TXT_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            TXT_PARAMDATE.Text = conn.GetFieldValue("SQL_DATE").ToString();
            TXT_PARAMKEY.Text = conn.GetFieldValue("SQL_PARAM_KEY").ToString();
            TXT_SQLFROM.Text = conn.GetFieldValue("SQL_FROM").ToString();
            TXT_BAMT.Text = conn.GetFieldValue("BASIC_AMT").ToString();
            TXT_PARAMDESCR.Text = conn.GetFieldValue("SQL_DESCR").ToString();

            TXT_RAMT00.Text = conn.GetFieldValue("RSV_AMT00").ToString();
            TXT_RAMT01.Text = conn.GetFieldValue("RSV_AMT01").ToString();
            TXT_RAMT02.Text = conn.GetFieldValue("RSV_AMT02").ToString();
            TXT_RAMT03.Text = conn.GetFieldValue("RSV_AMT03").ToString();
            TXT_RAMT04.Text = conn.GetFieldValue("RSV_AMT04").ToString();
            TXT_RAMT05.Text = conn.GetFieldValue("RSV_AMT05").ToString();
            TXT_RAMT06.Text = conn.GetFieldValue("RSV_AMT06").ToString();
            TXT_RAMT07.Text = conn.GetFieldValue("RSV_AMT07").ToString();
            TXT_RAMT08.Text = conn.GetFieldValue("RSV_AMT08").ToString();
            TXT_RAMT09.Text = conn.GetFieldValue("RSV_AMT09").ToString();
            TXT_RAMT10.Text = conn.GetFieldValue("RSV_AMT10").ToString();
            TXT_RAMT11.Text = conn.GetFieldValue("RSV_AMT11").ToString();
            TXT_RAMT12.Text = conn.GetFieldValue("RSV_AMT12").ToString();
            TXT_RAMT13.Text = conn.GetFieldValue("RSV_AMT13").ToString();
            TXT_RAMT14.Text = conn.GetFieldValue("RSV_AMT14").ToString();
            TXT_RAMT15.Text = conn.GetFieldValue("RSV_AMT15").ToString();
            TXT_RAMT16.Text = conn.GetFieldValue("RSV_AMT16").ToString();
            TXT_RAMT17.Text = conn.GetFieldValue("RSV_AMT17").ToString();
            TXT_RAMT18.Text = conn.GetFieldValue("RSV_AMT18").ToString();
            TXT_RAMT19.Text = conn.GetFieldValue("RSV_AMT19").ToString();

            TXT_PT00.Text = conn.GetFieldValue("T00").ToString();
            TXT_PT01.Text = conn.GetFieldValue("T01").ToString();
            TXT_PT02.Text = conn.GetFieldValue("T02").ToString();
            TXT_PT03.Text = conn.GetFieldValue("T03").ToString();
            TXT_PT04.Text = conn.GetFieldValue("T04").ToString();
            TXT_PT05.Text = conn.GetFieldValue("T05").ToString();
            TXT_PT06.Text = conn.GetFieldValue("T06").ToString();
            TXT_PT07.Text = conn.GetFieldValue("T07").ToString();
            TXT_PT08.Text = conn.GetFieldValue("T08").ToString();
            TXT_PT09.Text = conn.GetFieldValue("T09").ToString();

            conn.QueryString = "select COA, DESCR = COA + ' - ' + DESCR " +
                                "from PARAM_GL_COA a " +
                                "where " +
                                "COA not in (select COA from PARAM_GL_JOURNAL_DETAIL where CODE='" + LB_CODE.Text + "' and DC='D') " +
                                "order by COA";
            conn.ExecuteQuery();
            DDL_COA_D.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COA_D.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select COA, DESCR = COA + ' - ' + DESCR " +
                                "from PARAM_GL_COA a " +
                                "where " +
                                "COA not in (select COA from PARAM_GL_JOURNAL_DETAIL where CODE='" + LB_CODE.Text + "' and DC='C') " +
                                "order by COA";
            conn.ExecuteQuery();
            DDL_COA_C.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COA_C.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            try
            {
                conn.QueryString = "update PARAM_GL_JOURNAL set " +
                                    "DESCR = '" +TXT_DESCR.Text.Trim() + "', " +
                                    "SQL_DATE = '" + TXT_PARAMDATE.Text.Trim().Replace("'","''") + "', " +
                                    "SQL_PARAM_KEY = '" + TXT_PARAMKEY.Text.Trim().Replace("'", "''") + "', " +
                                    "SQL_DESCR = '" + TXT_PARAMDESCR.Text.Trim().Replace("'", "''") + "', " +
                                    "SQL_FROM = '" + TXT_SQLFROM.Text.Trim().Replace("'", "''") + "', " +

                                    "BASIC_AMT = '" + TXT_BAMT.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT00 = '" + TXT_RAMT00.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT01 = '" + TXT_RAMT01.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT02 = '" + TXT_RAMT02.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT03 = '" + TXT_RAMT03.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT04 = '" + TXT_RAMT04.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT05 = '" + TXT_RAMT05.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT06 = '" + TXT_RAMT06.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT07 = '" + TXT_RAMT07.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT08 = '" + TXT_RAMT08.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT09 = '" + TXT_RAMT09.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT10 = '" + TXT_RAMT10.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT11 = '" + TXT_RAMT11.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT12 = '" + TXT_RAMT12.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT13 = '" + TXT_RAMT13.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT14 = '" + TXT_RAMT14.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT15 = '" + TXT_RAMT15.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT16 = '" + TXT_RAMT16.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT17 = '" + TXT_RAMT17.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT18 = '" + TXT_RAMT18.Text.Trim().Replace("'", "''") + "', " +
                                    "RSV_AMT19 = '" + TXT_RAMT19.Text.Trim().Replace("'", "''") + "', " +

                                    "T00 = '" + TXT_PT00.Text.Trim().Replace("'", "''") + "', " +
                                    "T01 = '" + TXT_PT01.Text.Trim().Replace("'", "''") + "', " +
                                    "T02 = '" + TXT_PT02.Text.Trim().Replace("'", "''") + "', " +
                                    "T03 = '" + TXT_PT03.Text.Trim().Replace("'", "''") + "', " +
                                    "T04 = '" + TXT_PT04.Text.Trim().Replace("'", "''") + "', " +
                                    "T05 = '" + TXT_PT05.Text.Trim().Replace("'", "''") + "', " +
                                    "T06 = '" + TXT_PT06.Text.Trim().Replace("'", "''") + "', " +
                                    "T07 = '" + TXT_PT07.Text.Trim().Replace("'", "''") + "', " +
                                    "T08 = '" + TXT_PT08.Text.Trim().Replace("'", "''") + "', " +
                                    "T09 = '" + TXT_PT09.Text.Trim().Replace("'", "''") + "' " +

                                    "where " +
                                    "CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteNonQuery();
                
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message + "<BR>";
            }

            SaveDGR();
            Setup();
        }

        protected void SaveDGR()
        {
            for (int i = 0; i < DGR_DEBET.Items.Count; i++)
            {
                TextBox txtT00 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T00");
                TextBox txtT01 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T01");
                TextBox txtT02 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T02");
                TextBox txtT03 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T03");
                TextBox txtT04 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T04");
                TextBox txtT05 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T05");
                TextBox txtT06 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T06");
                TextBox txtT07 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T07");
                TextBox txtT08 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T08");
                TextBox txtT09 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T09");
                DropDownList ddlAMOUNT = (DropDownList)DGR_DEBET.Items[i].FindControl("DDL_SQLAMOUNT");

                try
                {
                    conn.QueryString = "update PARAM_GL_JOURNAL_DETAIL set " +
                                        "SQL_AMOUNT = '" + ddlAMOUNT.SelectedValue + "', " +
                                        "SQL_T00 = '" + txtT00.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T01 = '" + txtT01.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T02 = '" + txtT02.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T03 = '" + txtT03.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T04 = '" + txtT04.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T05 = '" + txtT05.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T06 = '" + txtT06.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T07 = '" + txtT07.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T08 = '" + txtT08.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T09 = '" + txtT09.Text.Trim().Replace("'", "''") + "' " +
                                        "where " +
                                        "CODE = '" + LB_CODE.Text + "' " +
                                        "and COA = '" + DGR_DEBET.Items[i].Cells[0].Text + "' " +
                                        "and DC = 'D'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message + "<BR>";
                }
            }

            for (int i = 0; i < DGR_CREDIT.Items.Count; i++)
            {
                TextBox txtT00 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T00");
                TextBox txtT01 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T01");
                TextBox txtT02 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T02");
                TextBox txtT03 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T03");
                TextBox txtT04 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T04");
                TextBox txtT05 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T05");
                TextBox txtT06 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T06");
                TextBox txtT07 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T07");
                TextBox txtT08 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T08");
                TextBox txtT09 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T09");
                DropDownList ddlAMOUNT = (DropDownList)DGR_CREDIT.Items[i].FindControl("DDL_SQLAMOUNT");

                try
                {
                    conn.QueryString = "update PARAM_GL_JOURNAL_DETAIL set " +
                                        "SQL_AMOUNT = '" + ddlAMOUNT.SelectedValue + "', " +
                                        "SQL_T00 = '" + txtT00.Text.Trim().Replace("'","''") + "', " +
                                        "SQL_T01 = '" + txtT01.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T02 = '" + txtT02.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T03 = '" + txtT03.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T04 = '" + txtT04.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T05 = '" + txtT05.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T06 = '" + txtT06.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T07 = '" + txtT07.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T08 = '" + txtT08.Text.Trim().Replace("'", "''") + "', " +
                                        "SQL_T09 = '" + txtT09.Text.Trim().Replace("'", "''") + "' " +
                                        "where " +
                                        "CODE = '" + LB_CODE.Text + "' " +
                                        "and COA = '" + DGR_CREDIT.Items[i].Cells[0].Text + "' " +
                                        "and DC = 'C'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message + "<BR>";
                }
            }

        }

        protected void FillDGR()
        {
            conn.QueryString = "select a.name from syscolumns a inner join sysobjects b on a.id=b.id and b.xtype='u' and b.name='GL_DATA_MASTER'  " +
                                "where a.name like '%AMT%' or a.name = 'AMOUNT' order by a.colid";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            conn.QueryString = "select " +
                                "a.COA, " +
                                "DESCR = a.COA + ' - ' + b.DESCR, " +
                                "SQL_AMOUNT, " +
                                "SQL_T00, " +
                                "SQL_T01, " +
                                "SQL_T02, " +
                                "SQL_T03, " +
                                "SQL_T04, " +
                                "SQL_T05, " +
                                "SQL_T06, " +
                                "SQL_T07, " +
                                "SQL_T08, " +
                                "SQL_T09 " +
                                "from PARAM_GL_JOURNAL_DETAIL a " +
                                "inner join PARAM_GL_COA b on a.COA=b.COA " +
                                "where a.CODE = '" + LB_CODE.Text + "' and DC='D'";
            conn.ExecuteQuery();
            DGR_DEBET.DataSource = conn.GetDataTable().Copy();
            DGR_DEBET.DataBind();
            

            for (int i = 0; i < DGR_DEBET.Items.Count; i++)
            {
                TextBox txtT00 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T00");
                TextBox txtT01 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T01");
                TextBox txtT02 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T02");
                TextBox txtT03 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T03");
                TextBox txtT04 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T04");
                TextBox txtT05 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T05");
                TextBox txtT06 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T06");
                TextBox txtT07 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T07");
                TextBox txtT08 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T08");
                TextBox txtT09 = (TextBox)DGR_DEBET.Items[i].FindControl("TXT_T09");
                DropDownList ddlAMOUNT = (DropDownList)DGR_DEBET.Items[i].FindControl("DDL_SQLAMOUNT");
                Button btDELETE = (Button)DGR_DEBET.Items[i].FindControl("BT_DELETE");

                ddlAMOUNT.DataTextField = "name";
                ddlAMOUNT.DataValueField = "name";
                ddlAMOUNT.DataSource = dt;
                ddlAMOUNT.DataBind();

                txtT00.Text = DGR_DEBET.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtT01.Text = DGR_DEBET.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtT02.Text = DGR_DEBET.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtT03.Text = DGR_DEBET.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtT04.Text = DGR_DEBET.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtT05.Text = DGR_DEBET.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                txtT06.Text = DGR_DEBET.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                txtT07.Text = DGR_DEBET.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                txtT08.Text = DGR_DEBET.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                txtT09.Text = DGR_DEBET.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                try
                {
                    ddlAMOUNT.SelectedValue = DGR_DEBET.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                }
                catch { }

                btDELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
            }

            conn.QueryString = "select " +
                                "a.COA, " +
                                "DESCR = a.COA + ' - ' + b.DESCR, " +
                                "SQL_AMOUNT, " +
                                "SQL_T00, " +
                                "SQL_T01, " +
                                "SQL_T02, " +
                                "SQL_T03, " +
                                "SQL_T04, " +
                                "SQL_T05, " +
                                "SQL_T06, " +
                                "SQL_T07, " +
                                "SQL_T08, " +
                                "SQL_T09 " +
                                "from PARAM_GL_JOURNAL_DETAIL a " +
                                "inner join PARAM_GL_COA b on a.COA=b.COA " +
                                "where a.CODE = '" + LB_CODE.Text + "' and DC='C'";
            conn.ExecuteQuery();
            DGR_CREDIT.DataSource = conn.GetDataTable().Copy();
            DGR_CREDIT.DataBind();

            for (int i = 0; i < DGR_CREDIT.Items.Count; i++)
            {
                TextBox txtT00 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T00");
                TextBox txtT01 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T01");
                TextBox txtT02 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T02");
                TextBox txtT03 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T03");
                TextBox txtT04 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T04");
                TextBox txtT05 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T05");
                TextBox txtT06 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T06");
                TextBox txtT07 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T07");
                TextBox txtT08 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T08");
                TextBox txtT09 = (TextBox)DGR_CREDIT.Items[i].FindControl("TXT_T09");
                DropDownList ddlAMOUNT = (DropDownList)DGR_CREDIT.Items[i].FindControl("DDL_SQLAMOUNT");
                Button btDELETE = (Button)DGR_CREDIT.Items[i].FindControl("BT_DELETE");

                ddlAMOUNT.DataTextField = "name";
                ddlAMOUNT.DataValueField = "name";
                ddlAMOUNT.DataSource = dt;
                ddlAMOUNT.DataBind();

                txtT00.Text = DGR_CREDIT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtT01.Text = DGR_CREDIT.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtT02.Text = DGR_CREDIT.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtT03.Text = DGR_CREDIT.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtT04.Text = DGR_CREDIT.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtT05.Text = DGR_CREDIT.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                txtT06.Text = DGR_CREDIT.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                txtT07.Text = DGR_CREDIT.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                txtT08.Text = DGR_CREDIT.Items[i].Cells[10].Text.Replace("&nbsp;", "");
                txtT09.Text = DGR_CREDIT.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                try
                {
                    ddlAMOUNT.SelectedValue = DGR_CREDIT.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                }
                catch { }

                btDELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
            }
        }

        protected void BT_D_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into PARAM_GL_JOURNAL_DETAIL (CODE,COA,DC) select " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_COA_D.SelectedValue + "'," +
                                    "'D'";
                conn.ExecuteNonQuery();
                Setup();
            }
            catch { }
        }

        protected void BT_C_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into PARAM_GL_JOURNAL_DETAIL (CODE,COA,DC) select " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_COA_C.SelectedValue + "'," +
                                    "'C'";
                conn.ExecuteNonQuery();
                Setup();
            }
            catch { }
        }

        protected void DGR_DEBET_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_GL_JOURNAL_DETAIL " +
                                        "where " +
                                        "CODE = '" + LB_CODE.Text + "' " +
                                        "and COA = '" + e.Item.Cells[0].Text + "' " +
                                        "and DC = 'D'";
                    conn.ExecuteNonQuery();
                    Setup();
                }
                catch { }
            }
        }

        protected void DGR_CREDIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_GL_JOURNAL_DETAIL " +
                                        "where " +
                                        "CODE = '" + LB_CODE.Text + "' " +
                                        "and COA = '" + e.Item.Cells[0].Text + "' " +
                                        "and DC = 'C'";
                    conn.ExecuteNonQuery();
                    Setup();
                }
                catch { }
            }
        }
    }
}
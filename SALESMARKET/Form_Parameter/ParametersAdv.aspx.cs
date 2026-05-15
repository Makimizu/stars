using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace SALESMARKET.Form_Parameter
{
    public partial class ParametersAdv : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PREFIX.Text = Request.QueryString["prefix"];
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
                //GlobalUse.SetReadOnly(Page, GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), "Basic Parameters");
            }
        }

        protected void Setup()
        {
            LB_PARAM.Text = LB_CODE.Text.Replace(LB_PREFIX.Text, "").Replace("_", " ");
            FillDGRQUERY();
        }


        protected void FillDGRQUERY()
        {
            conn.QueryString = "exec SP_PARAM_TABLE_QUERY '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRQUERY.DataSource = dt;
            DGRQUERY.DataBind();

            for (int i = 0; i < DGRQUERY.Items.Count; i++)
            {
                Label lbF1 = (Label)DGRQUERY.Items[i].FindControl("LB_F1");
                TextBox txt1 = (TextBox)DGRQUERY.Items[i].FindControl("TXT_VAL1");
                Button btDEL = (Button)DGRQUERY.Items[i].FindControl("BT_DELITEM");
                lbF1.Text = DGRQUERY.Items[i].Cells[0].Text.Replace("&nbsp;", "");
                if (DGRQUERY.Items[i].Cells[0].Text.Replace("&nbsp;", "") == "")
                {
                    lbF1.Visible = false;
                    txt1.Visible = true;
                    btDEL.Visible = false;
                    txt1.Text = DGRQUERY.Items[i].Cells[0].Text.Replace("&nbsp;", "");
                    DGRQUERY.Items[i].BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
                }
            }

            conn.QueryString = "select " +
                                    "b.column_id, " +
                                    "b.name, " +
                                    "coltype = (case	when b.system_type_id = 167 then 'STR' " +
                                    "                when b.system_type_id = 61 then 'DATE' " +
                                    "                when b.system_type_id = 56 then 'INT' " +
                                    "                when b.system_type_id = 62 then 'FLO' " +
                                    "                else 'STR' end), " +
                                    "b.max_length, " +
                                    "b.is_nullable, " +
                                    "c.referenced_table_name, " +
                                    "c.referenced_column_name " +
                                    "from sysobjects a " +
                                    "inner join sys.columns b on a.id = b.object_id " +
                                    "left join V_FK c on a.name = c.referencing_table_name and b.name = c.referencing_column_name " +
                                    "where " +
                                    "a.xtype = 'U' " +
                                    "and a.name = '" + LB_CODE.Text + "'  " +
                                    "and b.name not in ('CREATEBY','CREATEDATE','LASTCHANGEBY','LASTCHANGEDATE') " +
                                    "order by 1";
            conn.ExecuteQuery();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                if (i > 0)
                {
                    if (conn.GetFieldValue(i, 5).ToString() != "")
                    {
                        conn2.QueryString = "select * from " + conn.GetFieldValue(i, 5).ToString();
                        conn2.ExecuteQuery();

                        for (int j = 0; j < DGRQUERY.Items.Count; j++)
                        {
                            DropDownList ddl = (DropDownList)DGRQUERY.Items[j].FindControl("DDL_VAL" + (i + 1).ToString());
                            ddl.Visible = true;

                            if (conn.GetFieldValue(i, 4).ToString() == "1")
                                ddl.Items.Add(new ListItem("", ""));
                            for (int k = 0; k < conn2.GetRowCount(); k++)
                            {
                                ddl.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                            }

                            try
                            {
                                ddl.SelectedValue = DGRQUERY.Items[j].Cells[i].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < DGRQUERY.Items.Count; j++)
                        {
                            TextBox txt = (TextBox)DGRQUERY.Items[j].FindControl("TXT_VAL" + (i + 1).ToString());
                            txt.Visible = true;

                            if (conn.GetFieldValue(i, 2).ToString() == "STR")
                            {
                                txt.MaxLength = int.Parse(conn.GetFieldValue(i, 3).ToString());
                                txt.Width = int.Parse(conn.GetFieldValue(i, 3).ToString()) * 2;
                            }

                            txt.Text = DGRQUERY.Items[j].Cells[i].Text.Replace("&nbsp;", "");
                        }
                    }
                }
            }
        }

        protected void DGRQUERY_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 20; i < DGRQUERY.Columns.Count - 1; i++)
                {
                    DGRQUERY.Columns[i].Visible = false;
                }

                conn.QueryString = "select column_id, name from sys.columns " +
                                    "where  " +
                                    "OBJECT_NAME(object_id) = '" + LB_CODE.Text + "' " +
                                    "and name not in ('CREATEBY','CREATEDATE','LASTCHANGEBY','LASTCHANGEDATE') " +
                                    "order by 1";
                conn.ExecuteQuery();

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    e.Item.Cells[20 + i].Text = conn.GetFieldValue(i, 1).ToString();
                    DGRQUERY.Columns[20 + i].Visible = true;
                }
            }
        }

        protected void DGRQUERY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                string SQL = "";

                if (e.Item.Cells[0].Text.Replace("&nbsp;", "") == "")
                {
                    SQL = "insert into " + LB_CODE.Text + " select ";
                    for (int i = 20; i < DGRQUERY.Columns.Count - 1; i++)
                    {
                        if (DGRQUERY.Columns[i].Visible)
                        {
                            if (i == 20)
                            {
                                TextBox txt1 = (TextBox)e.Item.FindControl("TXT_VAL1");
                                if (txt1.Text.Trim() == "")
                                    return;
                                SQL = SQL + "'" + txt1.Text.Trim() + "',";
                            }
                            else
                            {
                                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_VAL" + (i - 19).ToString());
                                TextBox txt = (TextBox)e.Item.FindControl("TXT_VAL" + (i - 19).ToString());
                                if (txt.Visible)
                                    SQL = SQL + "'" + txt.Text.Trim() + "',";
                                if (ddl.Visible)
                                    SQL = SQL + "'" + ddl.SelectedValue + "',";
                            }
                        }
                    }

                    SQL = SQL +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                }
                else
                {
                    conn.QueryString = "select name from syscolumns " +
                                        "where " +
                                        "OBJECT_NAME(id) = '" + LB_CODE.Text + "' " +
                                        "order by colid";
                    conn.ExecuteQuery();
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();

                    Label lb1 = (Label)e.Item.FindControl("LB_F1");
                    SQL = "update " + LB_CODE.Text + " set ";
                    for (int i = 20; i < DGRQUERY.Columns.Count - 1; i++)
                    {
                        if (DGRQUERY.Columns[i].Visible)
                        {
                            if (i > 20)
                            {
                                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_VAL" + (i - 19).ToString());
                                TextBox txt = (TextBox)e.Item.FindControl("TXT_VAL" + (i - 19).ToString());
                                if (txt.Visible)
                                    SQL = SQL + dt.Rows[i - 20][0].ToString() + "='" + txt.Text.Trim() + "',";
                                if (ddl.Visible)
                                    SQL = SQL + dt.Rows[i - 20][0].ToString() + "='" + ddl.SelectedValue + "',";
                            }
                        }
                    }

                    SQL = SQL +
                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                            "LASTCHANGEDATE = GETDATE() " +
                            "where " +
                            dt.Rows[0][0].ToString() + "='" + lb1.Text + "'";
                }

                conn.QueryString = SQL;
                conn.ExecuteNonQuery();
                FillDGRQUERY();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "select name from syscolumns " +
                                        "where " +
                                        "OBJECT_NAME(id) = '" + LB_CODE.Text + "'  " +
                                        "and colid=1";
                    conn.ExecuteQuery();

                    conn.QueryString = "delete from " + LB_CODE.Text + " where " + conn.GetFieldValue("name").ToString() + "='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRQUERY();
                }
                catch { }
            }
        }
    }
}
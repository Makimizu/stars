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
    public partial class ReportList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
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
            DDL_APP.Items.Clear();
            conn.QueryString = "select distinct c.CODE, c.APP_NAME " +
                                "from MENU_ROLE a " +
                                "inner join M_MENU b on a.MENU_CODE=b.MENU_CODE " +
                                "inner join M_APPS c on b.APP_CODE=c.CODE " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_UNIT.Items.Clear();
            conn.QueryString = "select CODE='',DESCR='' union all " +
                                "select CODE,DESCR from PR_UNIT order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_UNIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";

            conn.QueryString = "select APP_ID = '" + DDL_APP.SelectedValue + "',CODE = 0,DESCR = '',URL = '',URL_ENGLISH = '',FOLDER = '',REPORT_NAME = '',UNIT = '',SHARE = 0 union all " +
                                "select * from REPORT_LIST where APP_ID = '" + DDL_APP.SelectedValue + "' and UNIT = (case when '" + DDL_UNIT.SelectedValue + "'='' then UNIT else '" + DDL_UNIT.SelectedValue + "' end) order by UNIT,CODE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_UNIT order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbCODE = (Label)DGR.Items[i].FindControl("LB_CODE");

                TextBox txtDESCR = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                TextBox txtURL = (TextBox)DGR.Items[i].FindControl("TXT_URL");
                TextBox txtURLENG = (TextBox)DGR.Items[i].FindControl("TXT_URL_ENG");
                TextBox txtNAME = (TextBox)DGR.Items[i].FindControl("TXT_NAME");
                TextBox txtFOLDER = (TextBox)DGR.Items[i].FindControl("TXT_FOLDER");

                DropDownList ddlUNIT = (DropDownList)DGR.Items[i].FindControl("DDL_UNIT");
                CheckBox cbSHARE = (CheckBox)DGR.Items[i].FindControl("CB");

                Button btDEL = (Button)DGR.Items[i].FindControl("BT_MENU_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                lbCODE.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");

                txtDESCR.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;","");
                txtURL.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtURLENG.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtNAME.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtFOLDER.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlUNIT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlUNIT.SelectedValue = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                }
                catch { }

                if (DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "1")
                    cbSHARE.Checked = true;

                if (DGR.Items[i].Cells[1].Text == "0")
                {
                    txtDESCR.BackColor = System.Drawing.Color.Yellow;
                    txtURL.BackColor = System.Drawing.Color.Yellow;
                    txtURLENG.BackColor = System.Drawing.Color.Yellow;
                    txtNAME.BackColor = System.Drawing.Color.Yellow;
                    txtFOLDER.BackColor = System.Drawing.Color.Yellow;
                    ddlUNIT.BackColor = System.Drawing.Color.Yellow;

                    btDEL.Visible = false;
                    DGR.Items[i].BackColor = System.Drawing.Color.Transparent;
                    DGR.Items[i].Cells[1].ForeColor = System.Drawing.Color.Transparent;
                    lbCODE.ForeColor = System.Drawing.Color.Transparent;
                }
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";

            if (e.CommandName == "Save")
            {
                TextBox txtDESCR = (TextBox)e.Item.FindControl("TXT_DESCR");
                TextBox txtURL = (TextBox)e.Item.FindControl("TXT_URL");
                TextBox txtURLENG = (TextBox)e.Item.FindControl("TXT_URL_ENG");
                TextBox txtNAME = (TextBox)e.Item.FindControl("TXT_NAME");
                TextBox txtFOLDER = (TextBox)e.Item.FindControl("TXT_FOLDER");

                DropDownList ddlUNIT = (DropDownList)e.Item.FindControl("DDL_UNIT");
                CheckBox cbSHARE = (CheckBox)e.Item.FindControl("CB");

                string share = "0";
                if(cbSHARE.Checked)
                    share = "1";

                try
                {
                    conn.QueryString = "exec SP_REPORT_LIST_UPSERT " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + txtDESCR.Text.Trim() + "'," +
                                        "'" + txtURL.Text.Trim() + "'," +
                                        "'" + txtURLENG.Text.Trim() + "'," +
                                        "'" + txtFOLDER.Text.Trim() + "'," +
                                        "'" + txtNAME.Text.Trim() + "'," +
                                        "'" + ddlUNIT.SelectedValue + "'," +
                                        "'" + share + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from REPORT_LIST where APP_ID='" + e.Item.Cells[0].Text + "' and CODE='" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }

        protected void DDL_UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
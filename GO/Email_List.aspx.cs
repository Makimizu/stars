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
    public partial class Email_List : System.Web.UI.Page
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
            conn.QueryString = "select a.CODE, a.APP_NAME from  M_APPS a  order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select CODE = '0', UNIT = '', DESCR = '', BODY = '', DEFAULT_SENDER = '', CC = '', BCC = '' union all " +
                                "select CODE,UNIT,DESCR,BODY,DEFAULT_SENDER,CC,BCC from PARAM_EMAIL where APP_ID ='" + DDL_APP.SelectedValue + "' order by CODE";
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
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DELETE");
                Button btVIEW = (Button)DGR.Items[i].FindControl("BT_VIEW");
                TextBox txtDESCR = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                TextBox txtSENDER = (TextBox)DGR.Items[i].FindControl("TXT_SENDER");
                TextBox txtCC = (TextBox)DGR.Items[i].FindControl("TXT_CC");
                TextBox txtBCC = (TextBox)DGR.Items[i].FindControl("TXT_BCC");
                DropDownList ddlUNIT = (DropDownList)DGR.Items[i].FindControl("DDL_UNIT");

                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlUNIT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlUNIT.SelectedValue = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                }
                catch { }

                lbCODE.Text = DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "");
                txtDESCR.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtSENDER.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtCC.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtBCC.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");

                if (DGR.Items[i].Cells[0].Text == "0")
                {
                    txtDESCR.BackColor = System.Drawing.Color.Yellow;
                    txtSENDER.BackColor = System.Drawing.Color.Yellow;
                    txtCC.BackColor = System.Drawing.Color.Yellow;
                    txtBCC.BackColor = System.Drawing.Color.Yellow;
                    ddlUNIT.BackColor = System.Drawing.Color.Yellow;

                    btDEL.Visible = false;
                    btVIEW.Visible = false;
                    DGR.Items[i].BackColor = System.Drawing.Color.Transparent;
                    DGR.Items[i].Cells[1].ForeColor = System.Drawing.Color.Transparent;
                    lbCODE.ForeColor = System.Drawing.Color.Transparent;
                }
            }
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtDESCR = (TextBox)e.Item.FindControl("TXT_DESCR");
            TextBox txtSENDER = (TextBox)e.Item.FindControl("TXT_SENDER");
            TextBox txtCC = (TextBox)e.Item.FindControl("TXT_CC");
            TextBox txtBCC = (TextBox)e.Item.FindControl("TXT_BCC");
            DropDownList ddlUNIT = (DropDownList)e.Item.FindControl("DDL_UNIT");

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_EMAIL_UPSERT " +
                                        "'" + DDL_APP.SelectedValue + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + ddlUNIT.SelectedValue + "'," +
                                        "'" + txtDESCR.Text.Trim().Replace("'", "") + "'," +
                                        "'" + txtSENDER.Text.Trim().Replace("'", "") + "'," +
                                        "'" + txtCC.Text.Trim().Replace("'", "") + "'," +
                                        "'" + txtBCC.Text.Trim().Replace("'", "") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_EMAIL where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "View")
            {   
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.emailbody.location.href = 'Email_Detail.aspx?APPID=" + DDL_APP.SelectedValue + "&CODE=" + e.Item.Cells[0].Text + "';</script>");                
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
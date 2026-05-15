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
    public partial class Menu : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
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
            conn.QueryString = "select CODE, APP_NAME from M_APPS order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGRGroup();
            FillDGRMenu();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {   
            FillDGRGroup();
            FillDGRMenu();
        }

        protected void FillDGRGroup()
        {
            conn.QueryString = "select GROUP_CODE,GROUP_DESCR=UPPER(GROUP_DESCR) from M_MENU_GROUP where APP_CODE='" + DDL_APP.SelectedValue + "' order by GROUP_CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_GROUP.DataSource = dt;
            DGR_GROUP.DataBind();

            for (int i = 0; i < DGR_GROUP.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_GROUP.Items[i].FindControl("LB_GROUP");
                lb.Text = DGR_GROUP.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_GROUP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                TXT_GRP_CODE.Text = e.Item.Cells[1].Text;
                TXT_GRP_NAME.Text = e.Item.Cells[2].Text;
                
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from M_MENU_GROUP where GROUP_CODE='" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRGroup();
                    FillDGRMenu();
                }
                catch { }
            }
        }

        protected void FillDGRMenu()
        {

            conn.QueryString = "select MENU_GROUP=0,MENU_CODE=null,MENU_DESCR='',MENU_PATH='',ACTIVE=1,MENU_FONT='' union all  " +
                                "select MENU_GROUP,MENU_CODE,MENU_DESCR,MENU_PATH,ACTIVE,MENU_FONT from M_MENU where APP_CODE='" +DDL_APP.SelectedValue+ "' order by MENU_GROUP,MENU_CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MENU.DataSource = dt;
            DGR_MENU.DataBind();

            conn.QueryString = "select GROUP_CODE,UPPER(GROUP_DESCR) from M_MENU_GROUP where APP_CODE='" + DDL_APP.SelectedValue + "' order by GROUP_DESCR";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_MENU.Items.Count; i++)
            {
                TextBox txtCODE = (TextBox)DGR_MENU.Items[i].FindControl("TXT_MENU_CODE");
                TextBox txtDESCR = (TextBox)DGR_MENU.Items[i].FindControl("TXT_MENU_DESCR");
                TextBox txtPATH = (TextBox)DGR_MENU.Items[i].FindControl("TXT_MENU_PATH");
                TextBox txtFONT = (TextBox)DGR_MENU.Items[i].FindControl("TXT_MENU_FONT");
                DropDownList ddlACTIVE = (DropDownList)DGR_MENU.Items[i].FindControl("DDL_ACTIVE");
                DropDownList ddlGROUP = (DropDownList)DGR_MENU.Items[i].FindControl("DDL_MENU_GROUP");
                Button btDEL = (Button)DGR_MENU.Items[i].FindControl("BT_MENU_DEL");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlGROUP.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                txtCODE.Text = DGR_MENU.Items[i].Cells[0].Text.Replace("&nbsp;","");
                if (i == 0)
                {
                    txtCODE.BackColor = System.Drawing.Color.Yellow;
                    txtDESCR.BackColor = System.Drawing.Color.Yellow;
                    txtPATH.BackColor = System.Drawing.Color.Yellow;
                    txtFONT.BackColor = System.Drawing.Color.Yellow;
                    ddlACTIVE.BackColor = System.Drawing.Color.Yellow;

                    btDEL.Visible = false;
                }
                else
                {
                    txtCODE.ReadOnly = true;
                }

                txtDESCR.Text = DGR_MENU.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtPATH.Text = DGR_MENU.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                ddlACTIVE.SelectedValue = DGR_MENU.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtFONT.Text = DGR_MENU.Items[i].Cells[5].Text.Replace("&nbsp;", "");

                try
                {
                    ddlGROUP.SelectedValue = DGR_MENU.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                }
                catch { }

                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }

        protected void BT_GROUP_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_MENU_GROUP_UPSERT " +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + TXT_GRP_CODE.Text.Trim() + "'," +
                                    "'" + TXT_GRP_NAME.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRGroup();
                FillDGRMenu();
            }
            catch { }
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtCODE = (TextBox)e.Item.FindControl("TXT_MENU_CODE");
            TextBox txtDESCR = (TextBox)e.Item.FindControl("TXT_MENU_DESCR");
            TextBox txtPATH = (TextBox)e.Item.FindControl("TXT_MENU_PATH");
            DropDownList ddlACTIVE = (DropDownList)e.Item.FindControl("DDL_ACTIVE");
            DropDownList ddlGROUP = (DropDownList)e.Item.FindControl("DDL_MENU_GROUP");
            TextBox txtFONT = (TextBox)e.Item.FindControl("TXT_MENU_FONT");

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "exec SP_MENU_UPSERT " +
                                        "'" + DDL_APP.SelectedValue + "'," +
                                        "'" + ddlGROUP.SelectedValue + "'," +
                                        "'" + txtCODE.Text.Trim() + "'," +
                                        "'" + txtPATH.Text.Trim() + "'," +
                                        "'" + txtDESCR.Text.Trim() + "'," +
                                        "'" + txtFONT.Text.Trim() + "'," +
                                        "'" + ddlACTIVE.SelectedValue.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGRMenu();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from M_MENU where MENU_CODE='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRMenu();
                }
                catch { }
            }
        }
    }
}
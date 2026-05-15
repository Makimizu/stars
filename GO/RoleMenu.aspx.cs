using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace GO
{
    public partial class RoleMenu : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGRMenu();
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

            conn.QueryString = "select CODE, UPPER(DESCR) from M_ROLES order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ROLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRMenu();
        }

        protected void DDL_ROLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRMenu();
        }

        protected void FillDGRMenu()
        {
            conn.QueryString = "select " +
                                "a.MENU_CODE, " +
                                "a.MENU_DESCR, " +
                                "GROUP_DESCR = UPPER(c.GROUP_DESCR), " +
                                "b.AUTH_TYPE, " +
                                "STAT = (case when b.MENU_CODE is not null then 1 else 0 end) " +
                                "from M_MENU a " +
                                "inner join M_MENU_GROUP c on a.MENU_GROUP=c.GROUP_CODE " +
                                "left join MENU_ROLE b on a.MENU_CODE=b.MENU_CODE and b.ROLE_CODE='" +DDL_ROLE.SelectedValue+ "' " +
                                "where " +
                                "a.APP_CODE='" + DDL_APP.SelectedValue + "' " +
                                "order by " +
                                "a.MENU_GROUP, " +
                                "a.MENU_CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MENU.DataSource = dt;
            DGR_MENU.DataBind();
                        

            conn.QueryString = "select CODE,DESCR from PR_PRIVILEDGE order by CODE";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_MENU.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MENU.Items[i].FindControl("CB");
                DropDownList ddlAUTH = (DropDownList)DGR_MENU.Items[i].FindControl("DDL_AUTH");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlAUTH.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                if (DGR_MENU.Items[i].Cells[5].Text == "1")
                    cb.Checked = true;

                try
                {
                    ddlAUTH.SelectedValue = DGR_MENU.Items[i].Cells[4].Text;
                }
                catch { }
            }
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "delete from MENU_ROLE where MENU_CODE in (select MENU_CODE from M_MENU where APP_CODE='" +DDL_APP.SelectedValue+ "') and ROLE_CODE='" + DDL_ROLE.SelectedValue + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                for (int i = 0; i < DGR_MENU.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_MENU.Items[i].FindControl("CB");
                    DropDownList ddlAUTH = (DropDownList)DGR_MENU.Items[i].FindControl("DDL_AUTH");

                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "insert into MENU_ROLE select " +
                                                "'" + DDL_ROLE.SelectedValue + "'," +
                                                "'" + DGR_MENU.Items[i].Cells[1].Text + "'," +
                                                "'" + ddlAUTH.SelectedValue + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "GETDATE()";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                FillDGRMenu();

                GlobalTools.popMessage(this, "DONE");
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbALL = (CheckBox)sender;

            for (int i = 0; i < DGR_MENU.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MENU.Items[i].FindControl("CB");

                if (cbALL.Checked)
                    cb.Checked = true;
                else
                    cb.Checked = false;
            }
        }

        protected void DDL_AUTH_ALL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlALL = (DropDownList)sender;
            if (ddlALL.SelectedValue != "")
            {
                for (int i = 0; i < DGR_MENU.Items.Count; i++)
                {
                    DropDownList ddlAUTH = (DropDownList)DGR_MENU.Items[i].FindControl("DDL_AUTH");
                    try
                    {
                        ddlAUTH.SelectedValue = ddlALL.SelectedValue;
                    }
                    catch { }
                }
            }
        }

        protected void DGR_MENU_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DropDownList ddlALL = (DropDownList)e.Item.FindControl("DDL_AUTH_ALL");

                conn.QueryString = "select CODE,DESCR from PR_PRIVILEDGE order by CODE";
                conn.ExecuteQuery();

                ddlALL.Items.Add(new ListItem("", ""));
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    ddlALL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }
            }
        }
    }
}
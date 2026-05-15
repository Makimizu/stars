using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace GO
{
    public partial class MenuReadOnlyControl : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GlobalUse.SetReadOnly(this, GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]);
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

        }

        protected void FillDGRMenu()
        {
            conn.QueryString = "select " +
                                "a.MENU_CODE, " +
                                "MENU_DESCR = UPPER(a.MENU_DESCR), " +
                                "b.GROUP_DESCR " +
                                "from M_MENU a " +
                                "inner join M_MENU_GROUP b on a.MENU_GROUP=b.GROUP_CODE " +
                                "where " +
                                "b.APP_CODE = '" +DDL_APP.SelectedValue+ "' " +
                                "order by " +
                                "a.MENU_GROUP, " +
                                "a.MENU_CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MENU.DataSource = dt;
            DGR_MENU.DataBind();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            TD_CONTROL.Visible = false;
            DGR_MENU.CurrentPageIndex = 0;
            FillDGRMenu();
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                TD_CONTROL.Visible = true;
                LB_MENU_CODE.Text = e.Item.Cells[0].Text;
                LB_MENU_DESCR.Text = e.Item.Cells[1].Text;

                FillLBControl();
            }
        }

        protected void DGR_MENU_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            TD_CONTROL.Visible = false;
            DGR_MENU.CurrentPageIndex = e.NewPageIndex;
            FillDGRMenu();
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString =
                                    "if exists (select CONTROL_ID from MENU_READONLY_CONTROL where MENU_CODE='" + LB_MENU_CODE.Text + "' and CONTROL_ID='" + TXT_CONTROLID.Text.Trim() + "') " +
                                    "begin  return  end " +
                                    "insert into MENU_READONLY_CONTROL select " +
                                    "NEWID()," +
                                    "'" + LB_MENU_CODE.Text + "'," +
                                    "'" + TXT_CONTROLID.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();
                FillLBControl();
            }
            catch { }
        }

        protected void BT_DEL_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "delete from MENU_READONLY_CONTROL where ROWID='" + LB_CONTROL_LIST.SelectedValue + "'";
                conn.ExecuteNonQuery();
                FillLBControl();
            }
            catch { }
        }

        protected void FillLBControl()
        {
            LB_CONTROL_LIST.Items.Clear();
            conn.QueryString = "select ROWID,CONTROL_ID from MENU_READONLY_CONTROL where MENU_CODE='" +LB_MENU_CODE.Text+ "' order by CONTROL_ID";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB_CONTROL_LIST.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }
    }
}
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
    public partial class RoleReport : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR("null");
            }
        }

        protected void Setup()
        {
            DDL_APP.Items.Clear();
            conn.QueryString = "select distinct b.CODE,b.APP_NAME from REPORT_LIST a inner join M_APPS b on a.APP_ID=b.CODE order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_ROLE.Items.Clear();
            conn.QueryString = "select CODE,DESCR=UPPER(DESCR) from M_ROLES order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ROLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR(string unit)
        {
            conn.QueryString = "exec SP_REPORT_ROLES " +
                                "'" + DDL_APP.SelectedValue + "'," +
                                "'" + DDL_ROLE.SelectedValue + "'," +
                                unit;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                Label lb = (Label)DGR.Items[i].FindControl("LB_UNIT");

                lb.Text = DGR.Items[i].Cells[3].Text;
                if (DGR.Items[i].Cells[4].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR("null");
        }

        protected void DDL_ROLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR("null");
        }

        protected void DGR_GROUP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
            conn.QueryString = "delete from REPORT_ROLES where APP_ID='" + DDL_APP.SelectedValue + "' and ROLES='" + DDL_ROLE.SelectedValue + "'";
            conn.ExecuteNonQuery();
            }
            catch { }

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "insert into REPORT_ROLES select " +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'" + DDL_ROLE.SelectedValue + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_UNIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR("'" + ((DropDownList)sender).SelectedValue + "'");
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_UNIT");

                ddl.Items.Clear();
                conn.QueryString = "select distinct " +
                                    "b.CODE, " +
                                    "b.DESCR  " +
                                    "from REPORT_LIST a " +
                                    "inner join PR_UNIT b on a.UNIT=b.CODE " +
                                    "where " +
                                    "a.APP_ID='" + DDL_APP.SelectedValue + "' " +
                                    "order by 2";
                conn.ExecuteQuery();
                ddl.Items.Add(new ListItem("-- ALL UNITS --", ""));
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }
            }
        }
    }
}
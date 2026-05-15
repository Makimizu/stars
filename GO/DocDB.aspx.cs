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
    public partial class DocDB : System.Web.UI.Page
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
            conn.QueryString = "select distinct APP_DBNAME from M_APPS order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DB.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";


            conn.QueryString = "exec SP_DOC_DB_OBJECT_LIST " +
                                "'" + DDL_DB.SelectedValue + "'," +
                                "'" + TXT_OBJ.Text.Trim() + "'," +
                                "'" + DDL_TYPE.SelectedValue + "'," +
                                "'" + DDL_USED.SelectedValue + "'," +
                                "'" + DDL_REMARK.SelectedValue + "'";
                                
            conn.ExecuteQuery();

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") == "1")
                    cb.Checked = true;

                txt.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_DB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_USED_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_REMARK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                string used = "0";
                if(cb.Checked)
                    used = "1";

                conn.QueryString = "exec SP_DOC_DB_OBJECT_UPSERT " +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'" + DGR.Items[i].Cells[2].Text + "'," +
                                    "'" + txt.Text.Trim() + "'," +
                                    "'" + used + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR();
        }
    }
}
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
    public partial class DocScreen : System.Web.UI.Page
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
            conn.QueryString = "select CODE, APP_NAME from M_APPS order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";


            conn.QueryString = "exec SP_DOC_SCREEN_LIST " +
                                "'" + DDL_APP.SelectedValue + "'," +
                                "'" + TXT_SCREEN.Text.Trim() + "'";

            conn.ExecuteQuery();

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");
                txt.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
        

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }        

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                conn.QueryString = "exec SP_DOC_SCREEN_UPSERT " +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'" + txt.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR();
        }
    }
}
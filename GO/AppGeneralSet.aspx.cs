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
    public partial class AppGeneralSet : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillGrid();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,APP_NAME from M_APPS order by APP_NAME";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillGrid()
        {
            conn.QueryString = "select * from SC_GENERAL_SET where APP_CODE='" +DDL_APP.SelectedValue+ "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VALUE");
                string value = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");

                if (DGR.Items[i].Cells[2].Text == "True")
                {
                    txtval.TextMode = TextBoxMode.Password;
                    try
                    {
                        txtval.Text = Crypto.DecryptStringAES(value);
                    }
                    catch { }
                }
                else
                {
                    txtval.Text = value;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VALUE");
                string value = txtval.Text.Trim().Replace("'", "`");

                if (DGR.Items[i].Cells[2].Text == "True")
                {
                    if (value == "")
                        continue;
                    conn.QueryString = "update SC_GENERAL_SET set VALUE='" + Crypto.EncryptStringAES(value) + "' where PARAMETER = '" + DGR.Items[i].Cells[0].Text + "'";
                }
                else
                {
                    conn.QueryString = "update SC_GENERAL_SET set VALUE='" + value + "' where PARAMETER = '" + DGR.Items[i].Cells[0].Text + "'";
                }

                try
                {
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillGrid();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}
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
    public partial class ParamHoliday : System.Web.UI.Page
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
            DDL_YEAR.Items.Clear();
            conn.QueryString = "select distinct year(GETDATE()), year(HOLIDAY) from PARAM_HOLIDAY order by 2 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 1).ToString()));
            }

            try
            {
                DDL_YEAR.SelectedValue = conn.GetFieldValue(0, 0).ToString();
            }
            catch { }

            FillDGR(DDL_YEAR.SelectedValue);
        }

        protected void FillDGR(string year)
        {
            conn.QueryString = "exec SP_PARAM_HOLIDAY " + year;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                txt.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (txt.Text.Trim() != "")
                    txt.BackColor = System.Drawing.Color.Pink;

                if (DGR.Items[i].Cells[3].Text == "1" || DGR.Items[i].Cells[3].Text == "7")
                {
                    DGR.Items[i].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Font.Bold = true;
                }
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR(DDL_YEAR.SelectedValue);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_HOLIDAY where year(HOLIDAY) = " + DDL_YEAR.SelectedValue;
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                if (txt.Text.Trim() != "")
                {
                    conn.QueryString = "insert into PARAM_HOLIDAY select " +
                                        "'" + DGR.Items[i].Cells[0].Text + "','" + txt.Text.Trim().Replace("'","`") + "'";
                    conn.ExecuteNonQuery();
                }
            }

            FillDGR(DDL_YEAR.SelectedValue);
        }
    }
}
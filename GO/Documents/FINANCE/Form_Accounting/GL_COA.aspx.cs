using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Accounting
{
    public partial class GL_COA : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            BT_NEW.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk INSERT ?')){return false;};");
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.COA, " +
                                "a.DESCR " +
                                "from PARAM_GL_COA a " +
                                "where " +
                                "COA like '" + TXT_FIND_CODE.Text.Trim() + "%' " +
                                "and DESCR like '%" + TXT_FIND_DESCR.Text.Trim() + "%' " +
                                "order by a.COA";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtDESCR = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                Button btSAVE = (Button)DGR.Items[i].FindControl("BT_SAVE");
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DELETE");

                btSAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SAVE ?')){return false;};");
                btDELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");

                txtDESCR.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "").Trim();
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                try
                {
                    TextBox txtDESCR = (TextBox)e.Item.FindControl("TXT_DESCR");

                    conn.QueryString = "update PARAM_GL_COA set " +
                                        "DESCR = '" + txtDESCR.Text.Trim() + "' " +
                                        "where " +
                                        "COA = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete PARAM_GL_COA " +
                                        "where " +
                                        "COA = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void TXT_FIND_DESCR_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void TXT_FIND_CODE_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            if (TXT_FIND_CODE.Text.Trim() == "" || TXT_FIND_DESCR.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "insert into PARAM_GL_COA select " +
                                    "'" + TXT_FIND_CODE.Text.Trim() + "'," +
                                    "'" + TXT_FIND_DESCR.Text.Trim() + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
            catch { }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                conn.QueryString = "select " +
                                "a.COA " +
                                "from PARAM_GL_COA a " +
                                "where " +
                                "COA like '" + TXT_FIND_CODE.Text.Trim() + "%' " +
                                "and DESCR like '%" + TXT_FIND_DESCR.Text.Trim() + "%'";
                conn.ExecuteQuery();
                e.Item.Cells[3].Text = conn.GetRowCount().ToString();
            }
        }
    }
}
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
    public partial class SystemObjects : System.Web.UI.Page
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
            conn.QueryString = "select " +
                                "CODE = 'exec ' + name, " +
                                "DESCR = replace(replace(name,'SP_',''),'_',' ') " +
                                "from SYSOBJECTS " +
                                "where " +
                                "xtype = 'P' and name like 'SP_LIST_%' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_OBJECTS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_OBJECTS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = DDL_OBJECTS.SelectedValue + " " +
                                "'" + TXT_DB.Text.Trim() + "'," +
                                "'" + TXT_OBJ.Text.Trim() + "'";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_DBNAME.Visible = false;
            LB_OBJNAME.Visible = false;
            TXT_OBJTEXT.Text = "";
            TXT_OBJTEXT.Visible = false;
        }

        protected void DGR_MENU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_DBNAME.Visible = false;
                LB_OBJNAME.Visible = false;
                TXT_OBJTEXT.Text = "";
                TXT_OBJTEXT.Visible = false;

                try
                {
                    LB_DBNAME.Text = e.Item.Cells[1].Text;
                    LB_OBJNAME.Text = e.Item.Cells[2].Text;

                    conn.QueryString =  "use " + LB_DBNAME.Text + " " +
                                        "select text from syscomments where OBJECT_NAME(id) = '" + LB_OBJNAME.Text + "'";
                    conn.ExecuteQuery();

                    TXT_OBJTEXT.Text = conn.GetFieldValue("text").ToString();

                    LB_DBNAME.Visible = true;
                    LB_OBJNAME.Visible = true;
                    TXT_OBJTEXT.Visible = true;
                }
                catch { }
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
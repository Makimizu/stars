using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GLIFE.Form_Parameter
{
    public partial class LoadingCN : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                FillDGR();
            }
        }


        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "LOADING_CODE	= a.CODE, " +
                                "LOADING_DESCR	= UPPER(a.DESCR), " +
                                "CN_DESCR		= c.DESCR " +
                                "from		PR_LOADING a " +
                                "left join	PARAM_LOADING_CREDIT_NOTE b on a.CODE = b.CODE " +
                                "left join	FINANCE.dbo.PARAM_NOTA_TYPE c on b.APP_ID = c.APP_ID collate database_default and b.NOTA_TYPE = c.TIPE_NOTA collate database_default " +
                                "order by " +
                                "c.APP_ID desc, " +
                                "a.CODE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {

                Label lb = (Label)DGR.Items[i].FindControl("LB_CN");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_CN");
                Button bt = (Button)DGR.Items[i].FindControl("BT_SAVE");

                lb.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                if (lb.Text == "")
                {
                    ddl.Visible = true;
                    bt.Visible = true;

                    bt.Attributes.Add("onclick", "if(!confirm('Are you sure to SAVE ?')){return false;};");

                    conn.QueryString = "select " +
                                        "CODE		= c.APP_ID + c.TIPE_NOTA collate database_default, " +
                                        "DESCR		= c.DESCR " +
                                        "from		FINANCE.dbo.PARAM_NOTA_TYPE c " +
                                        "left join	PARAM_LOADING_CREDIT_NOTE b on b.APP_ID = c.APP_ID collate database_default and b.NOTA_TYPE = c.TIPE_NOTA collate database_default " +
                                        "where " +
                                        "b.APP_ID is null " +
                                        "and c.APP_ID = 'GL' " +
                                        "and c.DC = 'C'";
                    conn.ExecuteQuery();
                    ddl.Items.Add(new ListItem("", ""));
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_CN");
                if (ddl.SelectedValue == "")
                    return;

                conn.QueryString = "insert into PARAM_LOADING_CREDIT_NOTE " +
                                    "select " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "APP_ID, " +
                                    "TIPE_NOTA " +
                                    "from FINANCE.dbo.PARAM_NOTA_TYPE " +
                                    "where " +
                                    "APP_ID + TIPE_NOTA collate database_default = '" + ddl.SelectedValue + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
        }

    }
}
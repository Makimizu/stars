using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class Autodebet : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                //FillDGR();

                string script = "$(document).ready(function () { $('[id*=BT_LOAD]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select BANK from FINANCE.dbo.PARAM_TBL_BANK where CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery(1500000);
            LB_TITLE.Text = conn.GetFieldValue(0, 0).ToString();

            conn.QueryString = "select THEYEAR = YEAR(GETDATE()) union all select YEAR(GETDATE()) - 1 order by 1 desc";
            conn.ExecuteQuery(1500000);
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_AUTODEBET " + DDL_YEAR.SelectedValue + ",'" + LB_CODE.Text + "'";
            conn.ExecuteQuery(1500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lb = (Label)DGR.Items[i].FindControl("LB_LINK");

                conn.QueryString = DGR.Items[i].Cells[0].Text;
                conn.ExecuteQuery(1500000);
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    lb.Text = lb.Text + conn.GetFieldValue(j, 0).ToString() + "<BR>";
                }
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_LOAD_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            FillDGR();
        }
    }
}
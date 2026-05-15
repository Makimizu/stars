using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Parameter
{
    public partial class InvoiceNoteCommission : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR_CN();
                FillDGR_DN();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct APPID, APPDESCR from V_PARAM_NOTA_COMMISSION order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR_CN()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "STAT " +
                                "from V_PARAM_NOTA_COMMISSION a " +
                                "where " +
                                "MODE = 'INCLUSION' " +
                                "and APPID = '" + DDL_APP.SelectedValue + "' " +
                                "order by CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CN.DataSource = dt;
            DGR_CN.DataBind();

            for (int i = 0; i < DGR_CN.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_CN.Items[i].FindControl("CB_CN");
                if (DGR_CN.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void FillDGR_DN()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "STAT " +
                                "from V_PARAM_NOTA_COMMISSION a " +
                                "where " +
                                "MODE = 'EXCLUSION' " +
                                "and APPID = '" + DDL_APP.SelectedValue + "' " +
                                "order by CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DN.DataSource = dt;
            DGR_DN.DataBind();

            for (int i = 0; i < DGR_DN.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_DN.Items[i].FindControl("CB_DN");
                if (DGR_DN.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void CB_CN_CheckedChanged(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_NOTA_COMMISSION where APP_ID = '" + DDL_APP.SelectedValue + "' and DC = 'C'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_CN.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_CN.Items[i].FindControl("CB_CN");
                if (cb.Checked)
                {
                    conn.QueryString = "insert into PARAM_NOTA_COMMISSION select '" + DDL_APP.SelectedValue + "','" + DGR_CN.Items[i].Cells[0].Text + "','C'";
                    conn.ExecuteNonQuery();
                }
            }

            FillDGR_CN();
        }

        protected void CB_DN_CheckedChanged(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_NOTA_COMMISSION where APP_ID = '" + DDL_APP.SelectedValue + "' and DC = 'D'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_DN.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_DN.Items[i].FindControl("CB_DN");
                if (cb.Checked)
                {
                    conn.QueryString = "insert into PARAM_NOTA_COMMISSION select '" + DDL_APP.SelectedValue + "','" + DGR_DN.Items[i].Cells[0].Text + "','D'";
                    conn.ExecuteNonQuery();
                }
            }

            FillDGR_DN();
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR_CN();
            FillDGR_DN();
        }
    }
}
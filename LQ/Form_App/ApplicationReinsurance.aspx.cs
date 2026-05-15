using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_App
{
    public partial class ApplicationReinsurance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                FillDGR();
                ShowAlert();
            }
        }

        protected void ShowAlert()
        {
            LB_FACULTATIVE_ALERT.Text = "";
            conn.QueryString = "exec SP_APPLICATION_FACULTATIVE_FLAG '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_FACULTATIVE_ALERT.Text = conn.GetFieldValue("ALERT").ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_REINSURANCE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select a.ID, a.DESCR from REINSURANCE.dbo.TC_MASTER a inner join REINSURANCE.dbo.TC_MASTER_USAGE b on a.ID = b.TC_ID and b.APP_ID = 'LF' order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_AGREEMENT");
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtOR = (TextBox)DGR.Items[i].FindControl("TXT_OR");
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                Label lbOR = (Label)DGR.Items[i].FindControl("LB_OR");
                Label lbQS = (Label)DGR.Items[i].FindControl("LB_QS");


                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtAMOUNT.Text = DGR.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "");
                txtRATE.Text = DGR.Items[i].Cells[4].Text.Trim().Replace("&nbsp;", "");
                txtOR.Text = DGR.Items[i].Cells[5].Text.Trim().Replace("&nbsp;", "");

                lbOR.Text = DGR.Items[i].Cells[6].Text.Trim().Replace("&nbsp;", "");
                lbQS.Text = DGR.Items[i].Cells[7].Text.Trim().Replace("&nbsp;", "");

                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "");
                }
                catch { }
            }
        }

    }
}
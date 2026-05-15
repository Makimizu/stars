using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationOtherInsurance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_MEMBERID.Text = Request.QueryString["MEMBERID"].ToString();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_INSURANCE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from UWBOX.dbo.PARAM_OTHER_INSURANCE order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtPOLICYNO = (TextBox)DGR.Items[i].FindControl("TXT_POLICYNO");
                DropDownList ddlINSURANCE = (DropDownList)DGR.Items[i].FindControl("DDL_INSURANCE");
                TextBox txtSTARTDATE = (TextBox)DGR.Items[i].FindControl("TXT_STARTDATE");
                TextBox txtENDDATE = (TextBox)DGR.Items[i].FindControl("TXT_ENDDATE");

                ddlINSURANCE.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlINSURANCE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtPOLICYNO.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtSTARTDATE.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtENDDATE.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");

                try
                {
                    ddlINSURANCE.SelectedValue = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from APPLICATION_OTHER_INSURANCE where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtPOLICYNO = (TextBox)DGR.Items[i].FindControl("TXT_POLICYNO");
                DropDownList ddlINSURANCE = (DropDownList)DGR.Items[i].FindControl("DDL_INSURANCE");
                TextBox txtSTARTDATE = (TextBox)DGR.Items[i].FindControl("TXT_STARTDATE");
                TextBox txtENDDATE = (TextBox)DGR.Items[i].FindControl("TXT_ENDDATE");

                if (txtPOLICYNO.Text.Trim() == "" || ddlINSURANCE.SelectedValue == "")
                    continue;

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_OTHER_INSURANCE_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + txtPOLICYNO.Text.Trim() + "'," +
                                        "'" + ddlINSURANCE.SelectedValue + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(txtSTARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(txtENDDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGR();
        }
    }
}
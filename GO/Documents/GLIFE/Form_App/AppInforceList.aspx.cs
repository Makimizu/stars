using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class AppInforceList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_LAPSE.Text = Request.QueryString["lapse"].ToString();
                //FillDGR();

                //string script = "$(document).ready(function () { $('[id*=BT_SEARCH]').click(); });";
                //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            string viewname = "V_APPLICATION_MASTER_TRACK_INFORCE";
            if (LB_LAPSE.Text == "1")
                viewname = "V_APPLICATION_MASTER_TRACK_LAPSE";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_PREMIUM.SelectedValue != "")
            {
                if (DDL_PREMIUM.SelectedValue == "STD")
                    where = where + " and a.EP_PREMIUM = 0 ";
                if (DDL_PREMIUM.SelectedValue == "SUBSTD")
                    where = where + " and a.EP_PREMIUM > 0 ";
            }

            conn.QueryString = "select " +
                                "REGNO,  " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "POLICY_NO,  " +
                                "COMPANY_NAME = '<span style=\"color:black;\">' + COMPANY_NAME + '</span>' + '<BR><i>' + TC_DESCR + '</i>',  " +
                                "BRANCH_CODE,  " +
                                "PREMIUM = replace(convert(varchar(100), convert(money,PREMIUM),1), '.00',''),  " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "START_DATE = convert(varchar(100), START_DATE, 106),  " +
                                "END_DATE = convert(varchar(100), END_DATE, 106) " +
                                "from " + viewname + " a " +
                                "where " +
                                "1=1 " + where + " " +
                                " order by a.START_DATE";
            conn.ExecuteQuery(10000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlREPORT = (DropDownList)DGR.Items[i].FindControl("DDL_REPORT");
                Button btREPORT = (Button)DGR.Items[i].FindControl("BT_REPORT");
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                Button btCLAIM = (Button)DGR.Items[i].FindControl("BT_CLAIM");
                Button btENDORSEMENT = (Button)DGR.Items[i].FindControl("BT_ENDORSEMENT");

                lbCODE.Text = DGR.Items[i].Cells[1].Text;

                if (LB_LAPSE.Text == "1")
                {
                    btENDORSEMENT.Visible = false;
                }
            }

            if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string ReadOnly = "&readonly=";
            if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                ReadOnly = "&readonly=1";

            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text + ReadOnly);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_MASTER_ROLLBACK '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Claim")
            {
                Response.Redirect("../Form_Claim/ClaimApp.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + ReadOnly);
            }

            if (e.CommandName == "Endorsement")
            {
                Response.Redirect("ApplicationEndorsement.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=&TYPE=" + ReadOnly);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

    }
}
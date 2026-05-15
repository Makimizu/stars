using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Claim
{
    public partial class ClaimPendingList : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PARAM_PENDING_TYPE where PROCESS_CODE = 'CLM'";
            conn.ExecuteQuery();
            DDL_PENDING.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PENDING.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (DDL_PENDING.SelectedValue != "")
                where = where + " and cc.PENDING_CODE = '" + DDL_PENDING.SelectedValue + "' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and cc.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_PENDINGDATE1.Text.Trim() != "")
                where = where + " and convert(date,cc.PENDINGDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_PENDINGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_PENDINGDATE2.Text.Trim() != "")
                where = where + " and convert(date,cc.PENDINGDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_PENDINGDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "cc.REGNO,  " +
                                "cc.SEQ,  " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "POLICY_NO,  " +
                                "COMPANY_NAME = '<span style=\"color:black;\">' + COMPANY_NAME + '</span>' + '<BR><i>' + TC_DESCR + '</i>',  " +
                                "TOTAL_AMOUNT = replace(convert(varchar(100), convert(money,TOTAL_AMOUNT),1), '.00',''),  " +
                                "PENDING_DESCR	= aa.DESCR, " +
                                "STARTBY        = cc.PENDINGBY + ' [' + convert(varchar(50), cc.PENDINGDATE) + ']', " +
                                "STOPBY			= cc.CLOSINGBY + ' [' + convert(varchar(50), cc.CLOSINGDATE) + ']' " +
                                "from APPLICATION_MASTER_PENDING cc " +
                                "inner join PARAM_PENDING_TYPE aa on cc.PENDING_CODE = aa.CODE and aa.PROCESS_CODE = 'CLM' " +
                                "inner join V_APPLICATION_CLAIM_MASTER a on cc.REGNO = a.REGNO and cc.SEQ = a.SEQ " +
                                "left join V_APPLICATION_CLAIM_AMOUNT_SUMM b on cc.REGNO = b.REGNO and cc.SEQ = b.SEQ " +
                                "where " +
                                DDL_STAT.SelectedValue + " " + where +
                                //"and convert(date,GETDATE()) < convert(date,a.END_DATE) " + where +
                                "order by cc.PENDINGDATE";
            conn.ExecuteQuery();

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
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                lbCODE.Text = DGR.Items[i].Cells[1].Text;
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("ClaimAppFrame.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + e.Item.Cells[2].Text + ReadOnly);
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
    }
}
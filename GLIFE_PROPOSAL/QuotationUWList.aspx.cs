using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationUWList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and (a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' or a.POLICY_NO like '%" + TXT_COMPANY.Text.Trim() + "%') ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date,USERDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date,USERDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_UW.SelectedValue != "")
                where = where + " " + DDL_UW.SelectedValue + " ";

            conn.QueryString = "select " +
                                "REGNO, " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>', " +
                                "POLICY_NO, " +
                                "COMPANY_NAME = '<span style=\"color:green;\">' + a.COMPANY_NAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + a.TC_DESCR + '</td><td style=\"text-align:right;\">' + a.POLICY_NO + '</td></tr></table>', " +
                                "BRANCH_CODE, " +
                                "UW_CODE, " +
                                "PREMIUM = '<span style=''color:blue;''>' + replace(convert(varchar(100), convert(money, a.SUMINS),1), '.00','') + '</span><BR>' + replace(convert(varchar(100), convert(money, a.PREMIUM),1), '.00',''), " +
                                "REGDATE = convert(varchar(100), USERDATE) " +
                                "from V_QUOTATION_MASTER a " +
                                "where REGNO in (select REGNO from V_LINK_GLIFE_APPLICATION_MASTER where SEQ < 3) " + where + " order by USERDATE desc";
            conn.ExecuteQuery(150000);

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
                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;
            }


            if (Request.Browser.IsMobileDevice)
            {
                DGR.Columns[0].HeaderText = "";

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                    lbCODE.Text = "Select";
                }

                for (int i = 0; i < DGR.Columns.Count; i++)
                {
                    DGR.Columns[i].Visible = false;
                    switch (i)
                    {
                        case 0: DGR.Columns[i].Visible = true; break;
                        case 2: DGR.Columns[i].Visible = true; break;
                        case 4: DGR.Columns[i].Visible = true; break;
                        case 7: DGR.Columns[i].Visible = true; break;
                    }
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("Quotation.aspx?ID=" + e.Item.Cells[1].Text + "&MEMBERID=");
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

        protected void DDL_UW_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
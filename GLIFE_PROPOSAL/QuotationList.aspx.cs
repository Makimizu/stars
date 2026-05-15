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
    public partial class QuotationList : System.Web.UI.Page
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
            string where = " and a.LAST_TRACK = 1 ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and (a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' or a.POLICY_NO like '%" + TXT_COMPANY.Text.Trim() + "%') ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date, a.USERDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date, a.USERDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_UW.SelectedValue != "")
                where = where + " " + DDL_UW.SelectedValue + " ";

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "FULLNAME = '<span style=\"color:green;\">' + a.FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),a.DOB,106) + '</td><td style=\"text-align:right;\">' + (case when a.SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>', " +
                                "COMPANY_NAME = '<span style=\"color:green;\">' + a.COMPANY_NAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + a.TC_DESCR + '</td><td style=\"text-align:right;\">' + a.POLICY_NO + '</td></tr></table>', " +
                                "a.BRANCH_CODE, " +
                                "a.UW_CODE, " +
                                "PREMIUM = '<span style=''color:blue;''>' + replace(convert(varchar(100), convert(money, a.SUMINS),1), '.00','') + '</span><BR>' + replace(convert(varchar(100), convert(money, a.PREMIUM),1), '.00',''), " +
                                "REGDATE = convert(varchar(100), a.USERDATE), " +
                                "MQ = ((isnull(q.CNT,0) + 1) *isnull(tc.CNT,0)) - isnull(tcq.CNT,0) " +
                                "from V_QUOTATION_MASTER a " +
                                "inner join (select TC_CODE, CNT = count(CODE) from V_LINK_UW_TC_QUESTIONS group by TC_CODE) tc on a.TC_ID = tc.TC_CODE " +
                                "left join (select REGNO, CNT = count(CODE) from APPLICATION_TC_QUESTIONS where LTRIM(isnull(VALUE,'')) <> '' group by REGNO) tcq on a.REGNO = tcq.REGNO " +
                                "left join (select REGNO, CNT = count(SEQ) from APPLICATION_JOIN_ACCOUNT group by REGNO) q on a.REGNO = q.REGNO " +
                                "where a.REGNO not in (select REGNO from V_LINK_GLIFE_APPLICATION_MASTER) " + where + " order by USERDATE desc";
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
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;

                if (DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "")
                    cb.Visible = false;

                //if (DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "0")
                //    cb.Visible = false;
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

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_QUOTATION_MASTER_DELETE '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }

            if (e.CommandName == "Check")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_QUOTATION_MASTER_GOTO_VERIFY " +
                                                "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                FillDGR();
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
                if(cb.Visible)
                    cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_UW_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
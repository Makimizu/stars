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
    public partial class AppNewList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                LB_TRACK.Text = Request.QueryString["track"].ToString();
                //FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";
            string joinrange = " ";
            string joinflag = " ";

            joinflag = " left join (select " +
                       " b1.REGNO, " +
                       " PENDING = case when b1.PENDINGDATE IS NULL and b1.CLOSINGDATE IS NULL then 'NEW' " +
                       "                when b1.PENDINGDATE IS NOT NULL and b1.CLOSINGDATE IS NOT NULL then 'NEW' " +
                       "                else 'PENDING' " +
                       "           END " +
                       " from PARAM_PENDING_TYPE a1 " +
                       " left join APPLICATION_MASTER_PENDING b1 on a1.CODE = b1.PENDING_CODE " +
                       " where  " +
                       " a1.PROCESS_CODE = 'UW' and b1.REGNO IS NOT NULL " +
                       " GROUP BY b1.REGNO,b1.PENDINGDATE,b1.CLOSINGDATE) d on a.REGNO = d.REGNO";

            switch (LB_MODE.Text)
            {
                //case "AC": where = " and UW_CODE = '" + LB_MODE.Text + "' "; break;
                case "AC": where = " and UW_CODE in ('" + LB_MODE.Text + "','HD') "; break;
                case "NM": where = " and UW_CODE = '" + LB_MODE.Text + "' "; break;
                case "MED": where = " and UW_CODE not in ('AC','NM','00') "; break;
                case "00": where = " and (UW_CODE = '00' or ROUND(a.SUMINS,0) = 0) "; break;
                default: where = " and 1=1 "; break;
            }

            if (LB_TRACK.Text == "2")
            {
                joinrange = "inner join PARAM_APPROVAL_RANGE b on b.CODE='UW' and a.SUMINS between b.START_AMOUNT and b.END_AMOUNT " +
                            "inner join PARAM_APPROVAL_DETAIL c on b.CODE = c.CODE and b.START_AMOUNT = c.START_AMOUNT and c.USERID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
            }

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_CONFDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.CONFIRMED_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CONFDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.CONFIRMED_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            if (DDL_PREMIUM.SelectedValue != "")
            {
                if (DDL_PREMIUM.SelectedValue == "STD")
                    where = where + " and a.EP_PREMIUM = 0 ";
                if (DDL_PREMIUM.SelectedValue == "SUBSTD")
                    where = where + " and a.EP_PREMIUM > 0 ";
            }

            conn.QueryString = "select " +
                                "a.REGNO,  " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "POLICY_NO,  " +
                                "COMPANY_NAME = '<span style=\"color:black;\">' + COMPANY_NAME + '</span>' + '<BR><i>' + TC_DESCR + '</i>',  " +
                                "BRANCH_CODE,  " +
                                "UW_CODE,  " +
                                "NETT_PREMIUM = replace(convert(varchar(100), convert(money, NETT_PREMIUM),1), '.00',''),  " +
                                "ADJ_PREMIUM = replace(convert(varchar(100), convert(money, isnull(a.NETT_PREMIUM,0) - isnull(a.PREMIUM,0)),1), '.00',''),  " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "CONFIRMED_DATE = convert(varchar(100), CONFIRMED_DATE), " +
                                "AGING, " +
                                "PENDING = case ISNULL(PENDING,'') when '' then 'NEW' ELSE PENDING end " +
                                "from V_APPLICATION_MASTER_TRACK_" + LB_TRACK.Text + " a " + joinrange + " " + joinflag + " " +
                                "where " +
                                "a.REGNO NOT IN (SELECT REGNO FROM [GLIFE_PROPOSAL].[dbo].[APPLICATION_SPECIALRATE] WHERE DATECONFIRM IS NULL OR STATUS = 'REJECT') " +
                                where +
                               " order by a.CONFIRMED_DATE desc";
            conn.ExecuteQuery(120);

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
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DEL");

                lbCODE.Text = DGR.Items[i].Cells[1].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");
            }

            if (LB_MODE.Text != "AC")
            {
                DGR.Columns[DGR.Columns.Count - 2].Visible = false;
            }


            if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
                DGR.Columns[DGR.Columns.Count - 2].Visible = false;
            }

            /*
            if (LB_TRACK.Text == "2")
            {
                DGR.Columns[10].Visible = true;
            }
            */
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                AppApprove();
            }

            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

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
        }

        protected void AppApprove()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_MASTER_NEXT_TRACK " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "4," +
                                            "null," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

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

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Button btAPPROVE = (Button)e.Item.FindControl("BT_APPROVE");
                btAPPROVE.Attributes.Add("onclick", "if(!confirm('Are you sure to APPROVE ?')){return false;};");
            }
        }
    }
}
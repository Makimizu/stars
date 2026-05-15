using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_POS
{
    public partial class HistoryList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
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
            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'CLM' order by (case when SEQ in (5,6) then SEQ-1.9 else SEQ end) desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "exec SP_LINK_UB_ENDORSEMENT_LIST";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, "ENDORSEMENT_DESCR").ToString(), conn.GetFieldValue(i, "ENDORSEMENT_CODE").ToString()));
            }

            conn.QueryString = "select " +
                                "FOM		= convert(varchar(20), convert(date, convert(varchar(6), GETDATE(), 112) + '01'), 103), " +
                                "EOM		= convert(varchar(20), EOMONTH(GETDATE()), 103)";
            conn.ExecuteQuery();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string joinsales = "";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.ENDORSEMENT_TYPE_DESCR like '%" + DDL_TYPE.SelectedItem.Text + "%' ";

            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            if (role == "99")
            {
                joinsales = "inner join (select distinct REGNO, AGENT_CODE from APPLICATION_AGENT) ag on a.REGNO = ag.REGNO and ag.AGENT_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
            }

            conn.QueryString = "select " +
                                "REGNO                  = a.REGNO, " +
                                "SEQ                    = a.SEQ, " +
                                "FULLNAME               = a.FULLNAME, " +
                                "ENDORSEMENT_TYPE_DESCR = a.ENDORSEMENT_TYPE_DESCR, " +
                                "DOB                    = convert(varchar(20), a.DOB, 106), " +
                                "PRODUCT                = a.PRODUCT_NAME + '<BR><B>' + a.PRODUCT_GROUP + '</B>', " +
                                "POLICY_NO              = a.POLICY_NO, " +
                                "AUTHOR_DATE            = convert(varchar(20),  (case   when a.LAST_TRACK = 4 then aa.TRACK4_DATE " +
                                "                                                       when a.LAST_TRACK = 5 then aa.TRACK5_DATE " +
                                "                                                       else null end), 106), " +
                                "AUTHOR_BY              = isnull(mu.FRONT_NAME + ' ' + mu.LAST_NAME, (case when a.LAST_TRACK = 4 then aa.TRACK4_BY when a.LAST_TRACK = 5 then aa.TRACK5_BY else null end)), " +
                                "TRACK                  = tr.DESCR " +
                                "from                   V_APPLICATION_ENDORSEMENT_PARENT a " + joinsales + " " +
                                "inner join             PARAM_TRACK tr on a.LAST_TRACK = tr.SEQ and tr.TIPE_CODE = 'POS' " +
                                "inner join             UWBOX.dbo.PARAM_PRODUCT_GROUP pg on a.PRODUCT_GROUP_CODE = pg.CODE " +
                                "inner join	            APPLICATION_TRACK aa on a.REGNO = aa.REGNO and aa.TRACK_TYPE = 'POS' and aa.PARAM_VALUE = convert(varchar(10), a.SEQ) " +
                                "left join	            V_LINK_SC_M_USERS mu on (case when a.LAST_TRACK = 4 then aa.TRACK4_BY when a.LAST_TRACK = 5 then aa.TRACK5_BY else null end) = mu.CODE collate database_default " +
                                "where " +
                                "a.LAST_TRACK = " + DDL_STAT.SelectedValue + " " + where + " " +
                                "order by (case when a.LAST_TRACK = 4 then aa.TRACK4_DATE when a.LAST_TRACK = 5 then aa.TRACK5_DATE else null end) desc";
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
                lbCODE.Text = DGR.Items[i].Cells[3].Text + "<BR>" + DGR.Items[i].Cells[1].Text;
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + e.Item.Cells[2].Text);
            }

            //if (e.CommandName == "Delete")
            //{
            //    try
            //    {
            //        conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_ROLLBACK '" + e.Item.Cells[1].Text + "'," + e.Item.Cells[2].Text;
            //        conn.ExecuteNonQuery();
            //        FillDGR();
            //    }
            //    catch { }
            //}
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
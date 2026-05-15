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
    public partial class AppList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'UW' order by (case when SEQ = 5 then 3.8 when SEQ = 99 then 3.9 else SEQ end) desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where SEGMENT = 0";
            conn.ExecuteQuery();
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_LAPSE_FLAG";
            conn.ExecuteQuery();
            DDL_LAPSE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LAPSE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";
            string joinlapse = "";
            string joinsales = "";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO = '" + TXT_REGNO.Text.Trim() + "' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (DDL_PREMIUM.SelectedValue != "")
                where = where + DDL_PREMIUM.SelectedValue;

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                where = where + "and a.PRODUCT_GROUP_CODE = '" + DDL_PRODUCT_GROUP.SelectedValue + "' ";

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.STAT_TRACK = " + DDL_STAT.SelectedValue + " ";

            if (TR_LAPSE.Visible && DDL_LAPSE.SelectedValue != "")
                joinlapse = "inner join APPLICATION_TRACK b on a.REGNO = b.REGNO and b.TRACK_TYPE = 'UW' and b.TRACK99_FLAG = '" + DDL_LAPSE.SelectedValue + "' ";

            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            if (role == "99")
            {
                joinsales = "inner join (select distinct REGNO, AGENT_CODE from APPLICATION_AGENT) ag on a.REGNO = ag.REGNO and ag.AGENT_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
            }

            conn.QueryString = "select " +
                                "REGNO          = a.REGNO,  " +
                                "POLICY_NO      = a.POLICY_NO,  " +
                                "FULLNAME       = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "PRODUCT        = '<span style=\"color:black;\">' + PRODUCT_NAME + '</span>' + '<BR><i>' + PRODUCT_GROUP_NAME + '</i>',  " +
                                "UW_CODE        = UW_CODE,  " +
                                "PERIOD         = convert(varchar(20), START_DATE,106) + '<BR>' + convert(varchar(20), END_DATE,106)," +
                                "BASICPREMIUM   = replace(convert(varchar(100), convert(money, BASICPREMIUM),1), '.00',''),  " +
                                "SUMINS         = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "STAT           = a.STAT_DESCR " +
                                "from           V_APPLICATION_MASTER  a " + joinlapse + " " + joinsales + " " +
                                "where " +
                                "1=1 " + where +
                                " order by a.START_DATE desc";
            conn.ExecuteQuery(500000);

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
                lbCODE.Text = DGR.Items[i].Cells[2].Text + "<BR>" + DGR.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text);
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

        protected void DDL_STAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_STAT.SelectedValue == "99")
                TR_LAPSE.Visible = true;
            else
                TR_LAPSE.Visible = false;
        }
    }
}
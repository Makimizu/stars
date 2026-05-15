using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
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

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where SEGMENT = 0";
            conn.ExecuteQuery(150000);
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";
            string joinrange = " ";


            switch (LB_MODE.Text)
            {
                case "AC": where = " and a.UW_CODE = '" + LB_MODE.Text + "' "; break;
                case "NM": where = " and a.UW_CODE = '" + LB_MODE.Text + "' "; break;
                case "MED": where = " and a.UW_CODE not in ('AC','NM','00') "; break;
                case "00": where = " and (a.UW_CODE = '00' or ROUND(a.SUMINS,0) = 0) "; break;
                default: where = " and 1=1 "; break;
            }


            if (DDL_MODE.SelectedValue != "")
                where = where + DDL_MODE.SelectedValue;

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (DDL_PREMIUM.SelectedValue != "")
                where = where + DDL_PREMIUM.SelectedValue;

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                where = where + "and a.PRODUCT_GROUP_CODE = '" + DDL_PRODUCT_GROUP.SelectedValue + "' ";

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "REGNO          = a.REGNO,  " +
                                "POLICY_NO      = a.POLICY_NO,  " +
                                "FULLNAME       = '<span style=\"color:green;\">' + a.FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "PRODUCT        = '<span style=\"color:black;\">' + a.PRODUCT_NAME + '</span>' + '<BR><i>' + a.PRODUCT_GROUP_NAME + '</i>',  " +
                                "UW_CODE        = a.UW_CODE,  " +
                                "PERIOD         = convert(varchar(20), a.START_DATE,106) + '<BR>' + convert(varchar(20), a.END_DATE,106)," +
                                "BASICPREMIUM   = replace(convert(varchar(100), convert(money, a.BASICPREMIUM),1), '.00',''),  " +
                                "SUMINS         = replace(convert(varchar(100), convert(money,a.SUMINS),1), '.00',''),  " +
                                "AGING          = a.AGING, " +
                                "MODE			= (case when bb.REGNO is not null and b.REGNO is not null then 'PENDING START' " +
                                "                       when bb.REGNO is not null and b.REGNO is null then 'PENDING DONE' " +
                                "                       else 'NEW' " +
                                "                       end) " +
                                "from V_APPLICATION_MASTER  a " +
                                "left join		(	select distinct " +
                                "                   a.REGNO " +
                                "                   from			APPLICATION_MASTER_PENDING a " +
                                "                   inner join		PARAM_PENDING_TYPE b on a.PENDING_CODE = b.CODE and b.PROCESS_CODE = 'UW' " +
                                "                   where " +
                                "                   a.CLOSINGBY		is null " +
                                "               ) b on a.REGNO = b.REGNO " +
                                "left join		(	select distinct " +
                                "                   a.REGNO " +
                                "                   from			APPLICATION_MASTER_PENDING a " +
                                "                   inner join		PARAM_PENDING_TYPE b on a.PENDING_CODE = b.CODE and b.PROCESS_CODE = 'UW' " +
                                "               ) bb on a.REGNO = bb.REGNO " +
                                "where " +
                                "STAT_TRACK = " + LB_TRACK.Text + where +
                                " order by a.START_DATE desc";
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
                lbCODE.Text = DGR.Items[i].Cells[1].Text;
            }

            if (LB_TRACK.Text != "1" && LB_TRACK.Text != "2")
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text + ReadOnly);
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
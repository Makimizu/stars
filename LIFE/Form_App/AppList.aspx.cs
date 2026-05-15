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
    public partial class AppList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where SEGMENT = 0";
            conn.ExecuteQuery();
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'UW' and SEQ not in (99)";
            conn.ExecuteQuery();
            DDL_STAT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            /*
            string where = " ";
            string joinlapse = " ";

            //switch (LB_MODE.Text)
            //{
            //    case "AC": where = " and UW_CODE = '" + LB_MODE.Text + "' "; break;
            //    case "NM": where = " and UW_CODE = '" + LB_MODE.Text + "' "; break;
            //    case "MED": where = " and UW_CODE not in ('AC','NM','00') "; break;
            //    case "00": where = " and (UW_CODE = '00' or ROUND(a.SUMINS,0) = 0) "; break;
            //    default: where = " and 1=1 "; break;
            //}

            
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

            if (DDL_WAIVED.SelectedValue != "")
                where = where + " and a.WAIVED = '" + DDL_WAIVED.SelectedValue + "' ";


            conn.QueryString = "select " +
                                "REGNO          = a.REGNO,  " +
                                "POLICY_NO      = a.POLICY_NO,  " +
                                "FULLNAME       = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "PRODUCT        = '<span style=\"color:black;\">' + PRODUCT_NAME + '</span>' + '<BR><i>' + PRODUCT_GROUP_NAME + '</i>',  " +
                                "UW_CODE        = UW_CODE,  " +
                                "PERIOD         = convert(varchar(20), START_DATE,106) + '<BR>' + convert(varchar(20), END_DATE,106)," +
                                "BASICPREMIUM   = replace(convert(varchar(100), convert(money, BASICPREMIUM),1), '.00',''),  " +
                                "SUMINS         = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "STAT           = a.STAT_DESCR, " +
                                "ENABLE_ROLLBACK= (case when a.REGNO = a.POLICY_NO and a.STAT_TRACK = '3.5' then 1 else 0 end)," +
                                "WAIVED " + 
                                "from           V_APPLICATION_MASTER  a " + joinlapse + " " +
                                "where " +
                                "STAT_TRACK > 2 " + where +
                                " order by a.START_DATE desc";
            */
            string startDate = "1 Jan 1900";
            string endDate = "31 Dec 2099";
            string stardobtDate = "1 Jan 1900";
            string enddobDate = "31 Dec 2099";

            if (TXT_STARTDATE1.Text.Trim() != "") {
                startDate = GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy");
            }

            if (TXT_STARTDATE2.Text.Trim() != "") {
                endDate = GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy");
            }

            if (TXT_DOB1.Text.Trim() != "")
            {
                stardobtDate = GlobalUse.GlobalDateFormat(TXT_DOB1.Text.Trim(), "d/M/yyyy");
            }

            if (TXT_DOB2.Text.Trim() != "")
            {
                enddobDate = GlobalUse.GlobalDateFormat(TXT_DOB2.Text.Trim(), "d/M/yyyy");
            }

            conn.QueryString = "exec SP_APPLICATION_MASTER_LIST " +
                                    "'" + TXT_REGNO.Text.Trim() + "'," +
                                    "'" + TXT_POLICYNO.Text.Trim() + "'," +
                                    "'" + TXT_FULLNAME.Text.Trim() + "'," +
                                    "'" + TXT_PRODUCT.Text.Trim()  + "'," +
                                    "'" + DDL_PREMIUM.SelectedValue  + "'," +
                                    "'" + DDL_PRODUCT_GROUP.SelectedValue + "'," +
                                    "'" + startDate +"'," +
                                    "'" + endDate +"'," +
                                    "'" + DDL_STAT.SelectedValue  + "'," +
                                    "'" + DDL_WAIVED.SelectedValue + "'," +
                                    "'" + stardobtDate + "'," +
                                    "'" + enddobDate + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
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

                Button btnRollBack = (Button)DGR.Items[i].FindControl("BT_ROLLBACK");
                if (DGR.Items[i].Cells[3].Text == "1")
                {
                    btnRollBack.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");
                    btnRollBack.Visible = true;
                }
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

            if (e.CommandName == "Rollback")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ROLLBACK_INFORCE " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    FillDGR();
                }
                catch { }
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
        }
        
        /*
        protected int IsUw()
        {
            int uwFlag = 0;

            conn.QueryString = "select ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("ROLE_CODE").ToString() == "950" || conn.GetFieldValue("ROLE_CODE").ToString() == "951" || conn.GetFieldValue("ROLE_CODE").ToString() == "18" || conn.GetFieldValue("ROLE_CODE").ToString() == "1")
            {
                uwFlag = 1;
            }

            return uwFlag;
        }
        */
    }
}
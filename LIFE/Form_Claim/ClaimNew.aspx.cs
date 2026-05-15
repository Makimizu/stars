using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimNew : System.Web.UI.Page
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
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where SEGMENT = 0";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_GROUP_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (DDL_PREMIUM.SelectedValue != "")
                where = where + DDL_PREMIUM.SelectedValue;

            if (DDL_SAVING.SelectedValue != "")
                where = where + DDL_SAVING.SelectedValue;

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP_CODE = '" + DDL_PRODUCT_GROUP.SelectedValue + "' ";

            if (where.Trim() == "")
                return;

            conn.QueryString = "select " +
                                "REGNO          = a.REGNO, " +
                                "POLICY_NO      = a.POLICY_NO,  " +
                                "MEMBER         = a.FULLNAME,  " +
                                "PRODUCT        = '<span style=\"color:black;\">' + PRODUCT_NAME + '</span>' + '<BR><i>' + PRODUCT_GROUP_NAME + '</i>',  " +
                                "UW_CODE        = UW_CODE,  " +
                                "PERIOD         = convert(varchar(20), START_DATE,106) + '<BR>' + convert(varchar(20), END_DATE,106)," +
                                "BASICPREMIUM   = replace(convert(varchar(100), convert(money, BASICPREMIUM),1), '.00',''),  " +
                                "SAVING_BALANCE = replace(convert(varchar(100), convert(money,isnull(dbo.UFN_REGNO_BALANCE(a.REGNO), 0)),1), '.00',''),  " +
                                "SUMINS         = replace(convert(varchar(100), convert(money,SUMINS),1), '.00','')  " +
                                "from       V_APPLICATION_MASTER  a " +
                                "where " +
                                "a.STAT_TRACK in (4, 99) " +
                                where +
                                " order by a.START_DATE desc";

            //if (where.Trim() == "")
            //    return;

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
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_CLAIM");

                lbCODE.Text = DGR.Items[i].Cells[2].Text + "<BR>" + DGR.Items[i].Cells[1].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to SUBMIT CLAIIM ?')){return false;};");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("../Form_App/ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text + ReadOnly);
            }

            if (e.CommandName == "Claim")
            {
                Response.Redirect("ClaimApp.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=&READONLY=0");
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
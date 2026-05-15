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
    public partial class AppPOSHistory : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from V_LINK_UB_PR_ENDORSEMENT_TYPE";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select SEQ,DESCR from PARAM_TRACK where TIPE_CODE='POS' and SEQ > 3 order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_APVDATE1.Text.Trim() != "")
                where = where + " and convert(date, a.LAST_TRACK_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_APVDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_APVDATE2.Text.Trim() != "")
                where = where + " and convert(date, a.LAST_TRACK_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_APVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.ENDORSEMENT_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.LAST_TRACK = '" + DDL_STAT.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.SEQ, " +
                                "a.ENDORSEMENT_TYPE, " +
                                "a.FULLNAME, " +
                                "DOB = convert(varchar(20), a.DOB, 106), " +
                                "APV_DATE = convert(varchar(20), a.LAST_TRACK_DATE, 106), " +
                                "a.TC_DESCR, " +
                                "a.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "AR_AMOUNT = replace(convert(varchar(100), convert(money, a.AR_AMOUNT),1), '.00',''), " +
                                "AP_AMOUNT = replace(convert(varchar(100), convert(money, a.AP_AMOUNT),1), '.00',''), " +
                                "SETOFF = replace(convert(varchar(100), convert(money, a.SETOFF),1), '.00',''), " +
                                "a.ENDORSEMENT_TYPE_DESCR, " +
                                "a.LAST_TRACK_DESCR, " +
                                "a.URL_REPORT " +
                                "from V_APPLICATION_ENDORSEMENT_MASTER a " +
                                "where " +
                                "a.LAST_TRACK > 3 " + where + " " +
                                "order by a.REG_DATE desc";
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
                Button btREPORT = (Button)DGR.Items[i].FindControl("BT_REPORT");

                lbCODE.Text = DGR.Items[i].Cells[4].Text;
                btREPORT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "") + "','ENDORSEMENT LETTER','height=400px,width=600px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }



        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("AppPOSFrame.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + e.Item.Cells[2].Text + "&TYPE=" + e.Item.Cells[3].Text);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
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
                LB_TRACK.Text = Request.QueryString["TRACK"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct PRODUCT_GROUP, PRODUCT_GROUP_DESCR from UWBOX.dbo.V_PARAM_PRODUCT_MASTER where SEGMENT = 0";
            conn.ExecuteQuery();
            DDL_PRODUCTGROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCTGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";
            string dob = "";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO = '" + TXT_REGNO.Text.Trim() + "' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCTNAME.Text.Trim() != "")
                where = where + " and a.PRODUCT_DESCR like '%" + TXT_PRODUCTNAME.Text.Trim() + "%' ";

            if (DDL_PRODUCTGROUP.SelectedValue != "")
                where = where + " and a.PRODUCT_GROUP = '" + DDL_PRODUCTGROUP.SelectedValue + "' ";

            if (TXT_DOB.Text.Trim() != "")
            {
                //dob = "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'";
                where = where + " and a.DOB = '" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "' ";
            }

            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            if (role == "99")
            {
                where = where + " and a.AGENT_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
            }


            conn.QueryString = "select " +
                                "POLICY_HOLDER		=   '<table style=''border-spacing:0px;font-size:x-small;''>' + " +
                                "                        '<tr><td style=''width:60px;''>REGNO</td><td style=''width:200px;''>:&nbsp;<a href=''QuotationFrame.aspx?REGNO=' + a.REGNO collate database_default + '''>' + a.REGNO collate database_default + '</a></td></tr>' + " +
                                "                        '<tr><td>DOB</td><td>:&nbsp;' + isnull(convert(varchar(20), a.DOB, 106), '') + '</td></tr>' + " +
                                "                        '<tr><td>NAME</td><td>:&nbsp;<B>' + a.FULLNAME collate database_default + '</B></td></tr>' + " +
                                "                        '<tr><td>GENDER</td><td>:&nbsp;' + a.GENDER collate database_default + '</td></tr>' + " +
                                "                        '</table>', " +
                                "PRODUCT				= '<table style=''border-spacing:0px;font-size:x-small;''>' + " +
                                "                        '<tr><td style=''color:green;''><B>' + a.PRODUCT_DESCR collate database_default + '</B></td></tr>' + " +
                                "                        '<tr><td style=''color:green;''>' + a.PRODUCT_GROUP_DESCR collate database_default + '</td></tr>' + " +
                                "                        '</table>', " +
                                "POLICY_INFO			= '<table style=''border-spacing:0px;width:100%;font-size:x-small;''>' + " +
                                "                        '<tr><td style=''width:80px;''>START AGE</td><td style=''width:100px;color:green;''>:&nbsp;' + convert(varchar(10), START_AGE) + '</td></tr>' + " +
                                "                        '<tr><td style=''color;green;''>START DATE</td><td style=''color:green;''>:&nbsp;' + convert(varchar(20), a.START_DATE, 106) + '</td></tr>' + " +
                                "                        '<tr><td>U/W CODE</td><td style=''color:green;''>:&nbsp;' + isnull(a.UW_CODE_DESCR, '') collate database_default + '</td></tr>' + " +
                                "                        '<tr><td>SUM INSURED</td><td style=''color:green;''>:&nbsp;' +replace(convert(varchar(100), convert(money, isnull(a.SUMINS, 0)), 1), '.00', '') + '</td></tr>' + " +
                                "                        '</table>', " +
                                "CREATEBY   			= '<table style=''border-spacing:0px;width:100%;font-size:x-small;''>' + " +
                                "                        '<tr><td style=''width:80px;''>CREATE BY</td><td style=''width:100px;color:green;''>:&nbsp;' + isnull(a.CREATENAME, a.CREATEBY) + '</td></tr>' + " +
                                "                        '<tr><td style=''color;green;''>CREATE DATE</td><td style=''color:green;''>:&nbsp;' + convert(varchar(100), a.CREATEDATE) + '</td></tr>' + " +
                                "                        '</table>', " +
                                "a.MEMBERID, " +
                                "a.REGNO " +
                                "from V_APPLICATION_MASTER a " +
                                "where " +
                                "isnull(a.POLICY_NO, '') = '' " +
                                "and a.TRACK = " + LB_TRACK.Text + " " + where + " " +
                                "order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btX = (Button)DGR.Items[i].FindControl("BT_X");
                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                if (DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") == "")
                    DGR.Items[i].BackColor = System.Drawing.Color.LightPink;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from APPLICATION_MASTER where REGNO = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void DDL_PRODUCTGROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}
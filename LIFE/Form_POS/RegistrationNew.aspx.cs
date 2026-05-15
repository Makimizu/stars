using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class RegistrationNew : System.Web.UI.Page
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
            DDL_PRODUCT_GROUP.Items.Clear();
            TXT_FULLNAME.Text = "";
            TXT_POLICYNO.Text = "";
            TXT_PRODUCT.Text = "";
            TXT_REGNO.Text = "";
            TXT_STARTDATE1.Text = "";
            TXT_STARTDATE2.Text = "";

            conn.QueryString = "select CODE = '', DESCR = '' union all " +
                                "select " +
                                "CODE, " +
                                "DESCR " +
                                "from UWBOX.dbo.PARAM_PRODUCT_GROUP " +
                                "where SEGMENT = 0 " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DDL_STATUS.Items.Add(new ListItem("", ""));
            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'UW' and SEQ in (4,5,99)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGRPolicy()
        {
            LB_RESULT.Text = "";
            /*
            string where = " ";
            if (DDL_STATUS.SelectedValue != "")
                where = where + " and a.STAT_TRACK = " + DDL_STATUS.SelectedValue + " ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO = '" + TXT_REGNO.Text.Trim() + "' ";

            if (DDL_PRODUCT_GROUP.SelectedValue != "")
                where = where + "and a.PRODUCT_GROUP_CODE = '" + DDL_PRODUCT_GROUP.SelectedValue + "' ";

            if (TXT_STARTDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_STARTDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.START_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "REGNO          = a.REGNO,  " +
                                "POLICY_NO      = a.POLICY_NO,  " +
                                "FULLNAME       = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "PRODUCT        = '<span style=\"color:black;\">' + PRODUCT_NAME + '</span>' + '<BR><i>' + PRODUCT_GROUP_NAME + '</i>',  " +
                                "UW_CODE        = UW_CODE,  " +
                                "PERIOD         = convert(varchar(20), START_DATE,106) + '<BR>' + convert(varchar(20), END_DATE,106)," +
                                "BASICPREMIUM   = replace(convert(varchar(100), convert(money, BASICPREMIUM),1), '.00',''),  " +
                                "SUMINS         = replace(convert(varchar(100), convert(money,SUMINS),1), '.00','')  " +
                                "from       V_APPLICATION_MASTER  a " +
                                "where " +
                                "a.STAT_TRACK in (4,5,99.1,99.2,99.5) " + where +
                                " order by a.START_DATE desc";
            */
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT " +
                                "'" + DDL_STATUS.SelectedValue + "'," +
                                "'" + TXT_FULLNAME.Text.Trim() + "'," +
                                "'" + TXT_PRODUCT.Text.Trim() + "'," +
                                "'" + TXT_POLICYNO.Text.Trim() + "'," +
                                "'" + TXT_REGNO.Text.Trim() + "'," +
                                "'" + DDL_PRODUCT_GROUP.SelectedValue + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE1.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE2.Text.Trim(), "d/M/yyyy") + "'";
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
                lbCODE.Text = DGR.Items[i].Cells[2].Text + "<BR>" + DGR.Items[i].Cells[1].Text;
                lbCODE.Attributes.Add("onclick", "if(!confirm('Are you sure to REGISTER ?')){return false;};");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_UPSERT " +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                string remark = conn.GetFieldValue("REMARK").ToString();
                if (remark.Trim() != "")
                {
                    LB_ERROR.Text = remark;
                    ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
                    return;
                }


                string REGNO = e.Item.Cells[1].Text + "-" + conn.GetFieldValue("SEQ").ToString();
                Response.Redirect("EndorsementFrame.aspx?ID=" + REGNO);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGRPolicy();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGRPolicy();
        }


    }
}
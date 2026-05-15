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
    public partial class ClaimSendEmail : System.Web.UI.Page
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
            conn.QueryString = "select SEQ = null, DESCR = '' union all select SEQ = -1, DESCR = 'PENDING' union all select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'CLM' and SEQ in (4,5)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_CLAIMDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CLAIMDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.LAST_TRACK = '" + DDL_STAT.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "REGNO              = a.REGNO, " +
                                "SEQ                = a.SEQ, " +
                                "POLICY_NO          = a.POLICY_NO, " +
                                "CLAIM_DATE         = convert(varchar(15), a.CLAIM_DATE, 106), " +
                                "RECEIVED_DATE      = convert(varchar(15), a.RECEIVED_DATE, 106), " +
                                "OCCURED_DATE       = convert(varchar(15), a.OCCURED_DATE, 106), " +
                                "MEMBER_ID          = a.MEMBER_ID, " +
                                "FULLNAME           = a.FULLNAME, " +
                                "PRODUCT_CODE       = a.PRODUCT_CODE, " +
                                "PRODUCT_NAME       = a.PRODUCT_NAME, " +
                                "LAST_TRACK         = a.LAST_TRACK, " +
                                "LAST_TRACK_DESCR   = a.LAST_TRACK_DESCR, " +
                                "CLAIM_TYPE         = a.CLAIM_TYPE, " +
                                "CLAIM_TYPE_DESCR   = a.CLAIM_TYPE_DESCR, " +
                                "INCURRED           = replace(convert(varchar(100), convert(money, a.INCURRED),1), '.00',''), " +
                                "ADDITION           = replace(convert(varchar(100), convert(money, a.ADDITION),1), '.00',''), " +
                                "DEDUCTION          = replace(convert(varchar(100), convert(money, a.DEDUCTION),1), '.00',''), " +
                                "TOTAL              = replace(convert(varchar(100), convert(money, a.TOTAL_AMOUNT),1), '.00',''), " +
                                "EMAIL              = a.EMAIL, " +
                                "FIRST_SENT         = convert(varchar(15), a.FIRST_SENT_DATE, 106), " +
                                "LAST_SENT          = convert(varchar(15), a.LAST_SENT_DATE, 106)," +
                                "LAST_SENT_BY       = a.LAST_SENT_BY " +
                                "from               V_APPLICATION_CLAIM_SEND_EMAIL a " +
                                "where " +
                                "a.SENT_STATUS      = " + DDL_SENT.SelectedValue + " " + 
                                where +
                                "order by " + 
                                "a.CLAIM_DATE desc";
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
                LinkButton lbFULLNAME = (LinkButton)DGR.Items[i].FindControl("LBT_FULLNAME");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                lbFULLNAME.Text = DGR.Items[i].Cells[2].Text;
                txtEMAIL.Text = DGR.Items[i].Cells[3].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ClaimAppFrame.aspx?REGNO=" + e.Item.Cells[0].Text + "&SEQ=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Send")
            {
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                conn.QueryString = "exec SP_APPLICATION_CLAIM_SEND_EMAIL " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    e.Item.Cells[1].Text + "," +
                                    "'" + txtEMAIL.Text.Trim() + "'," + 
                                    e.Item.Cells[4].Text + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(),"UserID") + "'";
                conn.ExecuteNonQuery();

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
    }
}
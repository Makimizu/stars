using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationBatchList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (IsSales())
            {
                where = where + " and CREATEBY in (select b.CODE from(select * from MARKETING.dbo.M_AGENTS where 'bri.' + CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' union " +
                          "select * from MARKETING.dbo.M_AGENTS where 'bri.' + UPLINER = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' union " +
                          "select * from MARKETING.dbo.M_AGENTS where 'bri.' + UPLINER in (select 'bri.' + CODE from MARKETING.dbo.M_AGENTS  where UPLINER = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ) " +
                          ") a, security.dbo.M_USERS b where 'bri.' + a.code = b.code COLLATE DATABASE_DEFAULT) ";
            }

            if (TXT_QUOTNO.Text.Trim() != "")
                where = where + " and a.QUOTNO = '" + TXT_QUOTNO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_GROUP_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date, a.CREATEDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date, a.CREATEDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "QUOTNO, " +
                                "COMPANY_NAME, " +
                                "PRODUCT_GROUP_DESCR, " +
                                "VERSIONS, " +
                                "URL, " +
                                "CREATEBY, " +
                                "CREATEDATE " +
                                "from V_QUOTATION a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by CREATEDATE desc";
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
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_QUOTNO");
                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string URL = Crypto.EncryptStringAES(e.Item.Cells[2].Text);
                while (URL.IndexOf("/") >= 0 || URL.IndexOf("\\") >= 0 || URL.IndexOf("+") >= 0)
                {
                    URL = Crypto.EncryptStringAES(e.Item.Cells[2].Text);
                }

                Response.Redirect("Frame.aspx?URL=" + URL);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_QUOTATION_DELETE '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    DGR.CurrentPageIndex = 0;
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

        protected bool IsSales()
        {
            /*
            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            conn.QueryString = "select ROLE_CODE from PARAM_SALES_ROLE where ROLE_CODE = '" + role + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return false;

            return true;
            */
            return false;
        }
    }
}
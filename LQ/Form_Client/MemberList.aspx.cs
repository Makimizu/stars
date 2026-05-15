using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Client
{
    public partial class MemberList : System.Web.UI.Page
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
            if (TXT_FULLNAME_SEARCH.Text.Trim() == "")
                return;

            string where = "";

            if (TXT_FULLNAME_SEARCH.Text.Trim() != "")
            {
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME_SEARCH.Text.Trim() + "%' ";
            }

            conn.QueryString = "select " +
                                "ID, " +
                                "FULLNAME, " +
                                "ID_NO, " +
                                "DOB = convert(varchar(20), DOB, 106), " +
                                "SEX, " +
                                "PROVINCE = b.DESCR " +
                                "from V_LINK_CB_MEMBER_MASTER a " +
                                "inner join V_LINK_CB_PR_PROPINSI b on a.PROVINCE = b.CODE " +
                                "where " +
                                "1 = 1 " + where +
                                "order by a.FULLNAME";
            conn.ExecuteQuery();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_FULLNAME");

                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;
            }

            if (Request.Browser.IsMobileDevice)
            {
                for (int i = 0; i < DGR.Columns.Count; i++)
                {
                    DGR.Columns[i].Visible = false;
                    switch (i)
                    {
                        case 2: DGR.Columns[i].Visible = true; break;
                        case 4: DGR.Columns[i].Visible = true; break;
                        case 5: DGR.Columns[i].Visible = true; break;
                    }
                }
            }
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("MemberEntry.aspx?ID=" + e.Item.Cells[0].Text + "QUOT=0");
            }
        }

        protected void DGR1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void TXT_FULLNAME_SEARCH_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

    }
}
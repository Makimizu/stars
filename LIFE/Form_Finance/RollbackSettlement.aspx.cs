using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class RollbackSettlement : System.Web.UI.Page
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
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO = '" + TXT_REGNO.Text.Trim() + "' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_INVOICENO.Text.Trim() != "")
                where = where + " and a.INVOICENO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "SETTLEDATE     = convert(varchar(15),a.SETTLEDATE,106), " +
                                "REGNO, " +
                                "POLICY_NO, " +
                                "FULLNAME, " +
                                "PRODUCT, " +
                                "AMOUNT         = replace(convert(varchar(100), convert(money, isnull(a.AMOUNT, 0)), 1), '.00', ''), " +
                                "UNITIZE, " +
                                "SQLEXEC " +
                                "from           V_APPLICATION_UNSETTLE_INVOICE a " + 
                                "where 1=1 " +
                                where +
                                " order by a.SETTLEDATE desc";
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
                Button btnRollBack = (Button)DGR.Items[i].FindControl("BT_ROLLBACK");
                btnRollBack.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");
                btnRollBack.Visible = true;
                
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Rollback")
            {
                try
                {
                    conn.QueryString = e.Item.Cells[0].Text;
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

    }
}
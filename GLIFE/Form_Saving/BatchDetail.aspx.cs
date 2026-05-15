using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Saving
{
    public partial class BatchDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "a.USERDATE, " +
                                "c.POLICY_NO, " +
                                "c.COMPANY_NAME, " +
                                "c.TC_DESCR, " +
                                "CONTRIB    = replace(convert(varchar(100), convert(money, SUM(isnull(d.ER_AMOUNT,0) + isnull(d.EE_AMOUNT,0))) ,1), '.00', '') " +
                                "from BATCH_MASTER a " +
                                "inner join APPLICATION_DATA_RAW b on a.ID = b.BATCH_ID " +
                                "inner join V_POLICY c on b.POLICY_ID = c.ID " +
                                "left join APPLICATION_SAVING_TRX d on b.REGNO = d.REGNO and a.ID = d.BATCH_ID " +
                                "where " +
                                "a.ID = '" + LB_ID.Text + "' " +
                                "group by " +
                                "a.USERDATE, " +
                                "c.POLICY_NO, " +
                                "c.COMPANY_NAME, " +
                                "c.TC_DESCR";
            conn.ExecuteQuery();

            LB_BATCHTIME.Text = conn.GetFieldValue("USERDATE").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_CONTRIB.Text = conn.GetFieldValue("CONTRIB").ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "REGNO		= a.REGNO, " +
                                "FULLNAME	= c.FULLNAME, " +
                                "DOB		= convert(varchar(20), c.DOB, 106), " +
                                "START_AGE	= b.START_AGE, " +
                                "SUMINS		= replace(convert(varchar(100), convert(money, b.SUMINS) ,1), '.00', ''), " +
                                "START_DATE	= convert(varchar(20), b.START_DATE, 106), " +
                                "END_DATE	= convert(varchar(20), b.END_DATE, 106), " +
                                "TENOR		= dbo.UFN_GET_LOM(b.START_DATE, b.END_DATE), " +
                                "UW_CODE_DESCR		= (case when isnull(b.UW_CODE, '00') in ('AC','FC') then 'AUTOMATIC COVER' " +
                                "                        when isnull(b.UW_CODE, '00') = '00' then 'UNDEFINED' " +
                                "                        when isnull(b.UW_CODE, '00') = 'NM' then 'NON MEDICAL' " +
                                "                        else 'MEDICAL' end), " +
                                "UW_CODE    = isnull(b.UW_CODE, '00') " +
                                "from APPLICATION_MASTER a " +
                                "inner join APPLICATION_MAIN_INFO b on a.REGNO = b.REGNO " +
                                "left join V_LINK_CB_MEMBER_MASTER c on a.MEMBER_ID = c.ID " +
                                //"left join APPLICATION_SAVING_RAW d on a.REGNO = d.REGNO and a.BATCH_ID = d.BATCH_ID " +
                                "where " +
                                "a.BATCH_ID = '" + LB_ID.Text + "' " +
                                "order by c.FULLNAME";
            conn.ExecuteQuery();

            LB_COUNT.Text = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_POLICY.DataSource = dt;
            DGR_POLICY.DataBind();

            for (int i = 0; i < DGR_POLICY.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_POLICY.Items[i].FindControl("LB_REGNO");
                lbCODE.Text = DGR_POLICY.Items[i].Cells[1].Text;

                if (DGR_POLICY.Items[i].Cells[2].Text == "00")
                    DGR_POLICY.Items[i].BackColor = System.Drawing.Color.Pink;
            }
        }

        protected void DGR_POLICY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_POLICY.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("..\\Form_App\\ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text);
            }
        }
    }
}
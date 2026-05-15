using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductBenefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "a.CODE,a.DESCR  " +
                                "from PR_BENEFIT a  " +
                                "inner join PARAM_BENEFIT_SEQ b on a.CODE=b.CODE " +
                                "where " +
                                "a.CODE in (select distinct BENEFIT_ID from PARAM_PRODUCT_PLAN where PRODUCT_CODE='" +LB_CODE.Text+ "') " +
                                "order by b.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR, " +                                
                                "TAKEN = (case when isnull(b.BENEFIT_DETAIL_ID,'')<>'' then 1 else 0 end) " +
                                "from PARAM_BENEFIT_DETAIL a " +
                                "left join PARAM_PRODUCT_BENEFIT_DETAIL b on a.CODE=b.BENEFIT_DETAIL_ID and b.PRODUCT_CODE='" + LB_CODE.Text + "' " +
                                "where " +                                
                                "a.BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    string taken = "1";
                    if (!cb.Checked)
                        taken = "0";

                    //try
                    //{
                        conn.QueryString = "exec SP_PARAM_PRODUCT_BENEFIT_DETAIL " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            taken + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    //}
                    //catch { }
                }

                FillDGR();
            }
        }
    }
}
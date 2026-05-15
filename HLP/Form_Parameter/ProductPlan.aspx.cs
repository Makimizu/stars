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
    public partial class ProductPlan : System.Web.UI.Page
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
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select a.CODE,a.DESCR from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ b on a.CODE=b.CODE " +
                                "order by b.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "PLAN_VALUE, FACTOR1, PLAN_GROUP " +
                                "from PARAM_PRODUCT_PLAN " +
                                "where " +
                                "PRODUCT_CODE='" + LB_CODE.Text + "' " +
                                "and BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "' " +
                                "order by PLAN_VALUE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_FACTOR1");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_GROUP");

                txt.Text = DGR.Items[i].Cells[1].Text;
                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[2].Text;
                }
                catch { }
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            bool bCek = false;
            if (((CheckBox)sender).Checked)
                bCek = true;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = bCek;
            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_INSERT " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_BENEFIT.SelectedValue + "'," +
                                    "'" + TXT_PLANNEW.Text.Trim() + "'," +
                                    "'" + DDL_PLANGROUP.SelectedValue + "'," +
                                    "'" + TXT_FACTORNEW.Text.Trim() + "'," +                                    
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
            catch { }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_FACTOR1");
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_GROUP");
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "update PARAM_PRODUCT_PLAN set " +
                                                "FACTOR1 = " + txt.Text.Trim() + ", " +
                                                "PLAN_GROUP = " + ddl.SelectedValue + ", " +
                                                "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "LASTCHANGEDATE = GETDATE() " +
                                                "where " +
                                                "CODE = '" + LB_CODE.Text + "_" + DDL_BENEFIT.SelectedValue + DGR.Items[i].Cells[0].Text + "'";
                            conn.ExecuteQuery();
                        }
                        catch { }
                    }                    
                }

                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_DELETE " +
                                                "'" + LB_CODE.Text + "_" + DDL_BENEFIT.SelectedValue + DGR.Items[i].Cells[0].Text + "'";
                            conn.ExecuteQuery();
                        }
                        catch { }
                    }
                }

                FillDGR();
            }
        }

    }
}
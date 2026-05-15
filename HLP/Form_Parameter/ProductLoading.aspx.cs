using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductLoading : System.Web.UI.Page
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
            DDL_FUND.Items.Add(new ListItem("UJROH", "PR_LOADING"));
            DDL_FUND.Items.Add(new ListItem("TABARRU", "PR_LOADING_TABARRU"));

            conn.QueryString = "select CODE,DESCR from PR_NBRN ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NBRN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_CHN.Items.Add(new ListItem("-- ALL --", ""));
            conn.QueryString = "select " +
                                "a.SUB_CODE, " +
                                "DESCR = b.DESCR + ' - ' + a.DESCR  " +
                                "from V_LINK_MARKETING_PARAM_SUB_CHANNEL_DISTRIBUTION a " +
                                "inner join V_LINK_MARKETING_PR_CHANNEL_DISTRIBUTION b on a.CD_CODE=b.CODE " +
                                "order by " +
                                "b.CODE, a.SUB_CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            FillDGR();
        }

        protected void FillDGR()
        {
            DGR.Visible = false;
            DGR_ALL.Visible = false;

            if (DDL_CHN.SelectedValue != "")
            {
                string nmtable = "PARAM_PRODUCT_LOADING_VALUE";
                if (DDL_FUND.SelectedValue == "PR_LOADING_TABARRU")
                    nmtable = "PARAM_PRODUCT_LOADING_TABARRU_VALUE";

                DGR.Visible = true;
                conn.QueryString = "SELECT " +
                                    "a.CODE, " +
                                    "a.DESCR, " +
                                    "VAL = isnull(b.VAL,0)*100 " +
                                    "from " + DDL_FUND.SelectedValue + " a " +
                                    "left join " + nmtable + " b on a.CODE=b.LOADING_CODE and b.SUB_CODE='" + DDL_CHN.SelectedValue + "' and b.NBRN = '" + DDL_NBRN.SelectedValue + "' and b.PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "order by 1";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    txt.Text = DGR.Items[i].Cells[2].Text;
                }
            }
            else
            {
                string SP = "SP_PARAM_PRODUCT_LOADING_VALUE";
                if (DDL_FUND.SelectedValue == "PR_LOADING_TABARRU")
                    SP = "SP_PARAM_PRODUCT_LOADING_TABARRU_VALUE";

                DGR_ALL.Visible = true;
                conn.QueryString = "exec " + SP + " " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_NBRN.SelectedValue + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_ALL.DataSource = dt;
                DGR_ALL.DataBind();

                for (int i = 0; i < DGR_ALL.Items.Count; i++)
                {
                    if (DGR_ALL.Items[i].Cells[0].Text == "TOTAL :")
                    {
                        DGR_ALL.Items[i].Font.Bold = true;
                        DGR_ALL.Items[i].ForeColor = System.Drawing.Color.White;
                        DGR_ALL.Items[i].BackColor = System.Drawing.Color.Gray;
                    }
                }
            }
        }

        protected void DDL_CHN_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string SP = "PARAM_PRODUCT_LOADING_VALUE";
                if (DDL_FUND.SelectedValue == "PR_LOADING_TABARRU")
                    SP = "PARAM_PRODUCT_LOADING_TABARRU_VALUE";

                conn.QueryString = "select TOTAL = SUM(isnull(VAL,0)*100) from " + SP + " " +
                                    "where " +
                                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "and SUB_CODE = '" + DDL_CHN.SelectedValue + "' " +
                                    "and NBRN = '" + DDL_NBRN.SelectedValue + "'";
                conn.ExecuteQuery();
                e.Item.Cells[3].Text = conn.GetFieldValue("TOTAL").ToString();
            }
        }

        protected void DGR_ALL_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                for (int j = 2; j < DGR_ALL.Columns.Count; j++)
                {
                    e.Item.Cells[j].Text = "0";
                    float val = 0;
                    for (int i = 0; i < DGR_ALL.Items.Count; i++)
                    {
                        val = val + float.Parse(DGR_ALL.Items[i].Cells[j].Text);
                    }
                    e.Item.Cells[j].Text = val.ToString();
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                float nilai1 = 0, nilai2 = 0;
                string type1 = "UJROH", type2 = "TABARRU";
                string SPTOTAL = "PARAM_PRODUCT_LOADING_VALUE";
                if (DDL_FUND.SelectedValue != "PR_LOADING_TABARRU")
                {
                    SPTOTAL = "PARAM_PRODUCT_LOADING_TABARRU_VALUE";
                    type1 = "TABARRU";
                    type2 = "UJROH";
                }

                conn.QueryString = "select TOTAL = isnull(SUM(isnull(VAL,0)*100),0) from " + SPTOTAL + " " +
                                    "where " +
                                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "and SUB_CODE = '" + DDL_CHN.SelectedValue + "' " +
                                    "and NBRN = '" + DDL_NBRN.SelectedValue + "'";
                conn.ExecuteQuery();
                nilai1 = float.Parse(conn.GetFieldValue("TOTAL").ToString());


                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtVal = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    //if (!txttabarru.Enabled)
                    //    continue;
                    nilai2 += float.Parse(txtVal.Text);
                }

                //if (nilai1 + nilai2 == 100)
                //{
                    string SP = "SP_PARAM_PRODUCT_LOADING_VALUE_UPSERT";
                    if (DDL_FUND.SelectedValue == "PR_LOADING_TABARRU")
                        SP = "SP_PARAM_PRODUCT_LOADING_TABARRU_VALUE_UPSERT";

                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        TextBox txtVal = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                        try
                        {
                            conn.QueryString = "exec " + SP + " " +
                                                "'" + LB_CODE.Text + "'," +
                                                "'" + DGR.Items[i].Cells[0].Text + "'," +
                                                "'" + DDL_CHN.SelectedValue + "'," +
                                                "'" + DDL_NBRN.SelectedValue + "'," +
                                                "'" + txtVal.Text.Trim() + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                /*
                }
                else
                {
                    Response.Write("<script>alert('Jumlah " + type1 + " : " + nilai1 + " dan " + type2 + " : " + nilai2 + " tidak 100%')</script>");
                }*/

                //Response.Redirect("ProductLoading.aspx?code=" + LB_CODE.Text);
                FillDGR();
            }
        }

        protected void DDL_NBRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
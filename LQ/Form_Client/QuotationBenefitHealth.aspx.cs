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
    public partial class QuotationBenefitHealth : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN_IH '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_PACKAGE");

                if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Items.Add(new ListItem("", ""));

                    if (DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                    {
                        conn.QueryString = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                        conn.ExecuteQuery();
                        for (int j = 0; j < conn.GetRowCount(); j++)
                        {
                            ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 2).ToString(), conn.GetFieldValue(j, 1).ToString()));
                        }
                    }

                    try
                    {
                        ddl.SelectedValue = DGR.Items[i].Cells[3].Text;
                        if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                            ddl.BackColor = System.Drawing.Color.Yellow;
                    }
                    catch { }
                }
                else
                {
                    ddl.Visible = false;
                }


                if (DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") == "1")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }

        protected void BT_SAVE_BENEFIT_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "delete from APPLICATION_BENEFIT_HEALTH_PLAN where REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteNonQuery();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    string memberid = "null";
                    string packageid = "null";
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_PACKAGE");

                    if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                        memberid = "'" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "'";
                    if (ddl.SelectedValue != "")
                        packageid = "'" + ddl.SelectedValue + "'";

                    conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN_IH_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        memberid + "," +
                                        packageid + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                conn.QueryString = "exec SP_APPLICATION_PREMIUM_COI_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                    "PREMIUM = replace(convert(varchar(100), convert(money, SUM(a.PREMIUM)), 1), '.00', '') " +
                                    "from APPLICATION_BENEFIT_HEALTH_PLAN a " +
                                    "where " +
                                    "a.REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                e.Item.Cells[11].Text = conn.GetFieldValue("PREMIUM").ToString();
            }
        }
    }
}
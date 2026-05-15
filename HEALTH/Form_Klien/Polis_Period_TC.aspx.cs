using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_TC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["PolicyPeriod"]))
                    Response.Redirect("~/Login.aspx");
                else
                    LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];

                Setup();
            }

        }
        protected void Setup()
        {
            Fill_DGR_BENEFIT();
        }

        protected void Fill_DGR_BENEFIT()
        {
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_TC_BENEFIT '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                Label lb = (Label)DGR_BENEFIT.Items[i].FindControl("LB_BENEFIT");
                DataGrid dgr = (DataGrid)DGR_BENEFIT.Items[i].FindControl("DGR");

                lb.Text = "<BR>" + DGR_BENEFIT.Items[i].Cells[1].Text + " :";
                Fill_DGR(dgr, DGR_BENEFIT.Items[i].Cells[0].Text);
            }
        }

        protected void Fill_DGR(DataGrid DGR, string BENEFIT)
        {
            conn.QueryString = "SELECT * FROM dbo.V_POLICY_PERIOD_TC WHERE POLICY_PERIOD_ID LIKE '" + LB_PERIOD.Text + "' and BENEFIT_ID = '" + BENEFIT + "' ORDER BY ORDER_NO";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable();

            DGR.DataSource = dt;
            DGR.DataBind();

            for (int ir = 0; ir < DGR.Items.Count; ir++)
            {
                var x = dt.Rows[ir]["TC_ID"].ToString();
                DropDownList ddl = (DropDownList)DGR.Items[ir].FindControl("DDL_LOADING_ID");
                HyperLink hl = (HyperLink)DGR.Items[ir].FindControl("HL_INFO");
                conn.QueryString = "SELECT " +
                                    "LOADING_ID = CODE," +
                                    "DESCR = UPPER(DESCR) " +
                                    "FROM PARAM_ACT_TC_FORMULA_DETAIL " +
                                    "WHERE " +
                                    "BENEFIT_ID = '" + DGR.Items[ir].Cells[1].Text + "' " +
                                    "and TC_CODE = '" + DGR.Items[ir].Cells[2].Text + "' " +
                                    "order by CODE";
                conn.ExecuteQuery();

                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1), conn.GetFieldValue(j, 0)));
                }

                ddl.SelectedValue = dt.Rows[ir]["TC_VAL"].ToString();
                if (ddl.SelectedValue == "")
                    DGR.Items[ir].BackColor = Color.Pink;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            LB_ERR.Text = "";
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DataGrid dgr = (DataGrid)DGR_BENEFIT.Items[i].FindControl("DGR");
                SaveTC(dgr);
            }

            Fill_DGR_BENEFIT();
        }

        protected void SaveTC(DataGrid DGR)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_LOADING_ID");
                string val = "null";
                if (ddl.SelectedValue != "")
                    val = "'" + ddl.SelectedValue + "'";
                try
                {
                    conn.QueryString = "update POLICY_PERIOD_TC set " +
                                        "TC_VAL = " + val + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + DGR.Items[i].Cells[0].Text + "' " +
                                        "and BENEFIT_ID = '" + DGR.Items[i].Cells[1].Text + "' " +
                                        "and TC_ID = '" + DGR.Items[i].Cells[2].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }
        }

        protected void BT_PRINT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rc:Parameters=False&POLICY_PERIOD_ID=" + LB_PERIOD.Text + "' from V_LINK_SC_REPORT_LIST where CODE='370'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + conn.GetFieldValue("URL").ToString() + "','PRINT POLICY PERIOD TC','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationClosingProcess : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PR_NBRN";
            conn.ExecuteQuery();
            DDL_NBRN.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NBRN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (DDL_NBRN.SelectedValue != "")
                where = where + "and NBRN_CODE = '" + DDL_NBRN.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "QUOTNO, " +
                                "VERNO, " +
                                "SEQ, " +
                                "NOREGFORM, " +
                                "COMPANY_NAME, " +
                                "PRODUCT, " +
                                "NBRN, " +
                                "PREMIUM = replace(convert(varchar(100),convert(money,PREMIUM),1),'.00',''), " +
                                "MEMBER, " +
                                "TIPE_KOMISI," +
                                "LOADING_TOTAL, " +
                                "MOP," +
                                "TPA," +
                                "SUBCD_DESCR, " +
                                "POLICY_STARTDATE, " +
                                "POLICY_ENDDATE, " +
                                "AGENT_NAME, " +
                                "REQUESTDATE " +
                                "from V_QUOTATION_VERSION_CLOSING " +
                                "where " +
                                "PROCESSBY is null " + where +
                                "order by QUOTNO";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LinkButton lbVER = (LinkButton)DGR.Items[i].FindControl("LB_VER");
                Button btEXEC = (Button)DGR.Items[i].FindControl("BT_EXEC");
                Button btCANCEL = (Button)DGR.Items[i].FindControl("BT_CANCEL");

                lbVER.Text = DGR.Items[i].Cells[1].Text;
                btEXEC.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk PROSES ?')){return false;};");
                btCANCEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk CANCEL ?')){return false;};");

                if (DGR.Items[i].Cells[6].Text == "New Business")
                {
                    DGR.Items[i].Cells[6].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[6].BackColor = System.Drawing.Color.Pink;
                }
                else
                {
                    DGR.Items[i].Cells[6].ForeColor = System.Drawing.Color.White;
                    DGR.Items[i].Cells[6].BackColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("QuotationFrame.aspx?code=" + e.Item.Cells[0].Text + ReadOnly);
            }

            if (e.CommandName == "Exec")
            {
                LB_ERR.Text = "";
                try
                {
                    /*conn.QueryString = "exec SP_QUOTATION_VERSION_CLOSING_PROCESS " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + e.Item.Cells[2].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    DGR.CurrentPageIndex = 0;
                    FillDGR();*/

                    conn.QueryString = "exec SP_QUOTATION_VERSION_LIMIT_VALIDATION  '" + e.Item.Cells[0].Text + "'," + e.Item.Cells[1].Text + "," + e.Item.Cells[2].Text + ",'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
                    conn.ExecuteQuery(120);

                    if (conn.GetRowCount() > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + conn.GetFieldValue(0, 1).ToString() + " ')", true);
                    }
                    else
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_CLOSING_PROCESS " +
                                            "'" + e.Item.Cells[0].Text + "'," +
                                            "'" + e.Item.Cells[1].Text + "'," +
                                            "'" + e.Item.Cells[2].Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                        DGR.CurrentPageIndex = 0;
                        FillDGR();
                    }
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }

            if (e.CommandName == "Cancel")
            {
                LB_ERR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_QUOTATION_VERSION_CLOSING_CANCEL " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + e.Item.Cells[2].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}

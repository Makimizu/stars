using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_REMUN_CORPORATE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_GROUP.Text = Request.QueryString["GROUP"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP where CODE = '" + LB_GROUP.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            conn.QueryString = "exec SP_CORPORATE_POLICY_AGENT '" + LB_GROUP.Text + "','" + TXT_COMPANY.Text.Trim() + "'";
            conn.ExecuteQuery();
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtUPLINER1 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER1");
                TextBox txtUPLINER2 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER2");
                TextBox txtUPLINER3 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER3");
                TextBox txtROLEADER = (TextBox)DGR.Items[i].FindControl("TXT_ROLEADER");

                Label lbAGENTCODE = (Label)DGR.Items[i].FindControl("LB_AGENTNAME");
                Label lbUPLINER1 = (Label)DGR.Items[i].FindControl("LB_UPLINER1NAME");
                Label lbUPLINER2 = (Label)DGR.Items[i].FindControl("LB_UPLINER2NAME");
                Label lbUPLINER3 = (Label)DGR.Items[i].FindControl("LB_UPLINER3NAME");
                Label lbROLEADER = (Label)DGR.Items[i].FindControl("LB_ROLEADERNAME");

                TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
                TextBox txtOR1 = (TextBox)DGR.Items[i].FindControl("TXT_OR1");
                TextBox txtOR2 = (TextBox)DGR.Items[i].FindControl("TXT_OR2");
                TextBox txtOR3 = (TextBox)DGR.Items[i].FindControl("TXT_OR3");
                TextBox txtBA = (TextBox)DGR.Items[i].FindControl("TXT_BA");

                txtAGENTCODE.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtUPLINER1.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtUPLINER2.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtUPLINER3.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtROLEADER.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");

                lbAGENTCODE.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                lbUPLINER1.Text = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                lbUPLINER2.Text = DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                lbUPLINER3.Text = DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                lbROLEADER.Text = DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "");

                txtCOMM.Text = DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                txtOR1.Text = DGR.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                txtOR2.Text = DGR.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                txtOR3.Text = DGR.Items[i].Cells[14].Text.Replace("&nbsp;", "");
                txtBA.Text = DGR.Items[i].Cells[15].Text.Replace("&nbsp;", "");

                if (txtAGENTCODE.Text.Trim() != "")
                    txtAGENTCODE.BackColor = System.Drawing.Color.Yellow;

                if (txtUPLINER1.Text.Trim() != "")
                    txtUPLINER1.BackColor = System.Drawing.Color.Yellow;

                if (txtUPLINER2.Text.Trim() != "")
                    txtUPLINER2.BackColor = System.Drawing.Color.Yellow;

                if (txtUPLINER3.Text.Trim() != "")
                    txtUPLINER3.BackColor = System.Drawing.Color.Yellow;

                if (txtROLEADER.Text.Trim() != "")
                    txtROLEADER.BackColor = System.Drawing.Color.Yellow;

                if (txtCOMM.Text.Trim() != "0")
                    txtCOMM.BackColor = System.Drawing.Color.Yellow;

                if (txtOR1.Text.Trim() != "0")
                    txtOR1.BackColor = System.Drawing.Color.Yellow;

                if (txtOR2.Text.Trim() != "0")
                    txtOR2.BackColor = System.Drawing.Color.Yellow;

                if (txtOR3.Text.Trim() != "0")
                    txtOR3.BackColor = System.Drawing.Color.Yellow;

                if (txtBA.Text.Trim() != "0")
                    txtBA.BackColor = System.Drawing.Color.Yellow;

                if (DGR.Items[i].Cells[16].Text == "0")
                    DGR.Items[i].BackColor = System.Drawing.Color.LightPink;
            }
        }

        protected void TXT_COMPANY_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_REPORT_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=29");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                bool bFound = false;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    TextBox txtAGENTCODE = (TextBox)DGR.Items[i].FindControl("TXT_AGENTCODE");
                    TextBox txtUPLINER1 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER1");
                    TextBox txtUPLINER2 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER2");
                    TextBox txtUPLINER3 = (TextBox)DGR.Items[i].FindControl("TXT_UPLINER3");
                    TextBox txtROLEADER = (TextBox)DGR.Items[i].FindControl("TXT_ROLEADER");

                    TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
                    TextBox txtOR1 = (TextBox)DGR.Items[i].FindControl("TXT_OR1");
                    TextBox txtOR2 = (TextBox)DGR.Items[i].FindControl("TXT_OR2");
                    TextBox txtOR3 = (TextBox)DGR.Items[i].FindControl("TXT_OR3");
                    TextBox txtBA = (TextBox)DGR.Items[i].FindControl("TXT_BA");

                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_CORPORATE_POLICY_AGENT_UPSERT " +
                                                "'" + LB_GROUP.Text + "'," +
                                                "'" + DGR.Items[i].Cells[0].Text + "'," +
                                                "'" + txtAGENTCODE.Text.Trim() + "'," +
                                                "'" + txtUPLINER1.Text.Trim() + "'," +
                                                "'" + txtUPLINER2.Text.Trim() + "'," +
                                                "'" + txtUPLINER3.Text.Trim() + "'," +
                                                "'" + txtROLEADER.Text.Trim() + "'," +
                                                "'" + txtCOMM.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtOR1.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtOR2.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtOR3.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtBA.Text.Trim().Replace(",", "") + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                            conn.ExecuteNonQuery();
                        }
                        catch { }
                        bFound = true;
                    }
                }

                if (bFound)
                    FillDGR();
            }
        }
    }
}
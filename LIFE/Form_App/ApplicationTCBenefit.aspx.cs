using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LIFE.Form_App
{
    public partial class ApplicationTCBenefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected float track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                ShowBenefitDetail();
                FillDGR_ITEM();
                FillDGRFund();
                FillClaimOrganization();
                CheckTrack();

                ShowBenefit();
                ShowPolicyAddress();
            }
        }

        protected void FillClaimOrganization()
        {
            DDL_ACCBANK.Items.Clear();
            conn.QueryString = "select KODE, BANK = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by 1";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select * from V_APPLICATION_ORG_BENEFICIARY where REGNO = '" + LB_REGNO.Text + "' and SEQ = 1";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                BT_ORGBEN.Visible = false;
                return;
            }

            TXT_ORG.Text = conn.GetFieldValue("ORG_NAME").ToString();
            TXT_CERNO.Text = conn.GetFieldValue("CERNO").ToString();
            TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
            TXT_ACCNAME.Text = conn.GetFieldValue("ACCNAME").ToString();

            try
            {
                DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
            }
            catch { }

            string PCT_CLM = conn.GetFieldValue("PCT_RISK").ToString();
            string PCT_CLM_SQL = conn.GetFieldValue("PCT_RISK_SQL").ToString();
            string PCT_INV = conn.GetFieldValue("PCT_NONRISK").ToString();
            string PCT_INV_SQL = conn.GetFieldValue("PCT_NONRISK_SQL").ToString();

            DDL_PCTCLM.Items.Clear();
            conn.QueryString = PCT_CLM_SQL;
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PCTCLM.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            try
            {
                DDL_PCTCLM.SelectedValue = PCT_CLM;
            }
            catch { }

            DDL_PCTINV.Items.Clear();
            conn.QueryString = PCT_INV_SQL;
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PCTINV.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            try
            {
                DDL_PCTINV.SelectedValue = PCT_INV;
            }
            catch { }

        }

        protected void FillDGRFund()
        {
            conn.QueryString = "exec SP_APPLICATION_FUND " +
                                "'" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DGR_FUND.DataSource = conn.GetDataTable().Copy();
                DGR_FUND.DataBind();
            }
            else
            {
                BT_FUND.Visible = false;
            }
        }

        protected void ShowBenefitDetail()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") < 3)
            {
                if (!IsHEALTH())
                    IF.Src = "ApplicationBenefit.aspx?REGNO=" + LB_REGNO.Text;
                else
                {
                    conn.QueryString = "select " +
                                        "URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) + '&REGNO=" + LB_REGNO.Text + "&BENEFIT_ID=''''' " +
                                        "from SECURITY.dbo.REPORT_LIST " +
                                        "where " +
                                        "APP_ID = 'LQ' " +
                                        "and REPORT_NAME = 'RPT_APPLICATION_QUOTATION_HEALTH_MEMBER'";
                    conn.ExecuteQuery();
                    IF.Src = conn.GetFieldValue("URL").ToString();
                }
            }
            else
                IF.Src = "../../ReportViewer/Viewer.aspx?APPID=LF&CODE=3&REGNO=" + LB_REGNO.Text;
        }

        protected bool IsHEALTH()
        {
            conn.QueryString = "select a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PRODUCT_GROUP = 'IH' " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                return true;

            return false;
        }
        protected bool IsPAYDI()
        {
            conn.QueryString = "select a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PAYDI = 1 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                return true;

            return false;
        }

        protected void CheckTrack()
        {
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            float track = float.Parse(conn.GetFieldValue("TRACK").ToString());
            if (track >= 3)
            {

            }
        }

        protected void FillDGR_ITEM()
        {
            try
            {
                conn.QueryString = "exec SP_PARAM_OTHER_SETTING " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'POL'";
                conn.ExecuteQuery();
                DGR_ITEM.DataSource = conn.GetDataTable().Copy();
                DGR_ITEM.DataBind();

                for (int j = 0; j < DGR_ITEM.Items.Count; j++)
                {
                    DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");
                    TextBox txtDATE = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_DATE");
                    Label lbVAL = (Label)DGR_ITEM.Items[j].FindControl("LB_VAL");

                    if (DGR_ITEM.Items[j].Cells[4].Text.Replace("&nbsp;", "") != "1")
                    {
                        if (DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                        {
                            ddl.Visible = true;
                            conn.QueryString = DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                            conn.ExecuteQuery();
                            for (int k = 0; k < conn.GetRowCount(); k++)
                                ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                            try
                            {
                                ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                        }
                        else
                        {
                            txtVAL.Visible = true;
                            switch (DGR_ITEM.Items[j].Cells[1].Text.Replace("&nbsp;", ""))
                            {
                                case "STR":
                                    txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                                    break;
                                case "INT":
                                    txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                                    txtVAL.Attributes.Add("text-align", "right");
                                    break;
                                case "FLO":
                                    try
                                    {
                                        conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                        conn.ExecuteQuery();
                                        txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                        txtVAL.Attributes.Add("text-align", "right");
                                    }
                                    catch { }
                                    break;
                                case "BIT":
                                    txtVAL.Visible = false;
                                    ddl.Visible = true;
                                    ddl.Items.Add(new ListItem("YES", "1"));
                                    ddl.Items.Add(new ListItem("NO", "0"));
                                    try
                                    {
                                        ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                                    }
                                    catch { }
                                    break;
                                case "DATE":
                                    txtVAL.Visible = false;
                                    ddl.Visible = false;
                                    txtDATE.Visible = true;
                                    txtDATE.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lbVAL.Visible = true;
                        ddl.Visible = false;
                        txtVAL.Visible = false;
                        lbVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    }
                }
            }
            catch { }
        }

        protected void DGR_ITEM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_ITEM.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                    DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");
                    TextBox txtDate = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_DATE");

                    string val = txt.Text.Trim();
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    if (txtDate.Visible)
                        val = txtDate.Text.Trim();

                    if (DGR_ITEM.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "1")
                    {
                        conn.QueryString = "exec SP_PARAM_OTHER_SETTING_UPSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                            "'" + val + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGR_ITEM();
            }
        }

        protected void BT_ORG_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_ORG_BENEFICIARY_UPSERT" +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + TXT_ORG.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_CERNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_ACCNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_ACCBANK.SelectedValue + "'," +
                                "'" + TXT_ACCNAME.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_PCTCLM.SelectedValue + "'," +
                                "'" + DDL_PCTINV.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillClaimOrganization();
        }

        protected void ShowBenefit()
        {
            LB_TITLE.Text = BT_BENEFIT.Text;
            TBL_BENEFIT.Visible = true;
            TBL_OTHERINFO.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
        }

        protected void BT_BENEFIT_Click(object sender, EventArgs e)
        {
            ShowBenefit();
        }

        protected void BT_OTHERINFO_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_BENEFIT.Visible = false;
            TBL_OTHERINFO.Visible = true;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
        }

        protected void BT_FUND_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_BENEFIT.Visible = false;
            TBL_OTHERINFO.Visible = false;
            TBL_FUND.Visible = true;
            TBL_ORGBEN.Visible = false;
        }

        protected void BT_ORGBEN_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_BENEFIT.Visible = false;
            TBL_OTHERINFO.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = true;
        }

        protected void ShowPolicyAddress()
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_POLICY_ADDRESS '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    LB_POLICY_DELIV_DEST.Text = conn.GetFieldValue("POLICY_DEV_DEST").ToString();
                    LB_POLICY_DELIV_ADDRESS.Text = conn.GetFieldValue("POLICY_DEV_ADDR").ToString();
                    LB_POLICY_DELIV_STAT.Text = conn.GetFieldValue("POLICY_DEV_STAT").ToString();
                    LB_POLICY_RECEIVE_DATE.Text = conn.GetFieldValue("POLICY_RECEIVE_DATE").ToString();
                }
            }
            catch { }
        }
    }
}
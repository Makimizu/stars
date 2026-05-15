using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyOtherSetup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();

                Setup();
                FillDGRChannel();
                FillDGRUploadFormat();
                FillDGRFund();
                FillDGRCharge();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                TBL_CHARGE.Visible = false;
            }
            else
            {
                conn.QueryString = "select CODE,DESCR from V_LINK_UB_PR_ENDORSEMENT_TYPE order by 2";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_TRXTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

                conn.QueryString = "select CODE,DESCR from V_LINK_SAVING_PR_CHARGE_TYPE order by 2";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_CHGTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGRUploadFormat()
        {
            conn.QueryString = "exec SP_POLICY_UW_EXCEL_UPLOAD_FORMAT " + LB_ID.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_EXCELFORMAT.DataSource = dt;
            DGR_EXCELFORMAT.DataBind();

            for (int i = 0; i < DGR_EXCELFORMAT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_EXCELFORMAT.Items[i].FindControl("CB_TAKEN");
                if (DGR_EXCELFORMAT.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;

                if (Request.QueryString["readonly"].ToString() == "1")
                    cb.Enabled = false;
            }
        }

        protected void FillDGRChannel()
        {
            conn.QueryString = "exec SP_POLICY_INVOICE_METHOD " + LB_ID.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INVOICE.DataSource = dt;
            DGR_INVOICE.DataBind();

            for (int i = 0; i < DGR_INVOICE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_INVOICE.Items[i].FindControl("CB");
                if (DGR_INVOICE.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;

                if (Request.QueryString["readonly"].ToString() == "1")
                    cb.Enabled = false;
            }
        }

        protected void FillDGRFund()
        {
            conn.QueryString = "exec SP_POLICY_FUND " + LB_ID.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FUND.DataSource = dt;
            DGR_FUND.DataBind();

            for (int i = 0; i < DGR_FUND.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FUND.Items[i].FindControl("TXT_FUND_PCT");
                txt.Text = DGR_FUND.Items[i].Cells[1].Text;

            }

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR_FUND.ShowFooter = false;
            }
        }

        protected void FillDGRCharge()
        {
            conn.QueryString = "exec SP_POLICY_CHARGE " + LB_ID.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CHARGE.DataSource = dt;
            DGR_CHARGE.DataBind();

            for (int i = 0; i < DGR_CHARGE.Items.Count; i++)
            {
                TextBox txtCHARGE = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_CHARGE_AMOUNT");
                TextBox txtPCT = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_PCT_AMOUNT");
                CheckBox cb = (CheckBox)DGR_CHARGE.Items[i].FindControl("CB_LAPSE");
                Button btDEL = (Button)DGR_CHARGE.Items[i].FindControl("BT_DEL");

                txtCHARGE.Text = DGR_CHARGE.Items[i].Cells[2].Text;
                txtPCT.Text = DGR_CHARGE.Items[i].Cells[3].Text;

                if (DGR_CHARGE.Items[i].Cells[4].Text == "1")
                    cb.Checked = true;

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtCHARGE.Enabled = false;
                    txtPCT.Enabled = false;
                    cb.Enabled = false;
                }
                else
                {
                    btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
                }
            }

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR_CHARGE.Columns[DGR_CHARGE.Columns.Count - 1].Visible = false;
                DGR_CHARGE.ShowFooter = false;
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_INVOICE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_INVOICE.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    string batch = "0";
                    if (cb.Checked)
                        batch = "1";

                    conn.QueryString = "exec SP_POLICY_INVOICE_METHOD_INSERT " + LB_ID.Text + ",'" + DGR_INVOICE.Items[i].Cells[0].Text + "'," + batch;
                    conn.ExecuteNonQuery();
                }
            }

            FillDGRChannel();
        }

        protected void CB_TAKEN_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_EXCELFORMAT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_EXCELFORMAT.Items[i].FindControl("CB_TAKEN");
                if (cb == (CheckBox)sender)
                {
                    string taken = "0";
                    if (cb.Checked)
                        taken = "1";

                    conn.QueryString = "exec SP_POLICY_UW_EXCEL_UPLOAD_FORMAT_INSERT " + LB_ID.Text + ",'" + DGR_EXCELFORMAT.Items[i].Cells[0].Text + "'," + taken;
                    conn.ExecuteNonQuery();
                }
            }

            FillDGRUploadFormat();
        }

        protected void DGR_FUND_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_FUND.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_FUND.Items[i].FindControl("TXT_FUND_PCT");

                    try
                    {
                        conn.QueryString = "exec SP_POLICY_FUND_UPSERT " +
                                            LB_ID.Text + "," +
                                            "'" + DGR_FUND.Items[i].Cells[0].Text + "'," +
                                            txt.Text.Replace(",", "") + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGRFund();
            }
        }

        protected void DGR_CHARGE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_CHARGE.Items.Count; i++)
                {
                    TextBox txtCHG = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_CHARGE_AMOUNT");
                    TextBox txtPCT = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_PCT_AMOUNT");
                    CheckBox cb = (CheckBox)DGR_CHARGE.Items[i].FindControl("CB_LAPSE");

                    string lapse = "0";
                    if (cb.Checked)
                        lapse = "1";

                    try
                    {
                        conn.QueryString = "exec SP_POLICY_CHARGE_UPDATE " +
                                            LB_ID.Text + "," +
                                            "'" + DGR_CHARGE.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR_CHARGE.Items[i].Cells[1].Text + "'," +
                                            txtCHG.Text.Replace(",", "") + "," +
                                            txtPCT.Text.Replace(",", "") + "," +
                                            lapse + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGRCharge();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from POLICY_CHARGE where " +
                                        "POLICY_ID = " + LB_ID.Text + " " +
                                        "and TRANS_TYPE = '" + e.Item.Cells[0].Text + "' " +
                                        "and CHARGE_TYPE = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRCharge();
                }
                catch { }
            }
        }

        protected void BT_CHGADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_POLICY_CHARGE_INSERT " +
                                    LB_ID.Text + "," +
                                    "'" + DDL_TRXTYPE.SelectedValue + "'," +
                                    "'" + DDL_CHGTYPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRCharge();
            }
            catch { }
        }
    }
}
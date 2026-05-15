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
    public partial class SavingMaturity : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select KODE, BANK from V_LINK_FN_PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "FULLNAME, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TC_DESCR, " +
                                "PERIODE = convert(varchar(20), START_DATE, 106) + ' - ' + convert(varchar(20), END_DATE, 106), " +
                                "BALANCE = replace(convert(varchar(100), convert(money, BALANCE), 1), '.00', '') " +
                                "from V_APPLICATION_SAVING_MATURITY a " +
                                "left join APPLICATION_ENDORSEMENT_SAVING b on a.REGNO = b.REGNO and b.ENDORSEMENT_TYPE = 'SV_MAT' " +
                                "where " +
                                "b.REGNO is null " + where + " " +
                                "order by a.END_DATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Execute")
            {
                LB_TITLE.Text = "";
                TXT_ACCNO.Text = "";
                TXT_ACCNAME.Text = "";

                int cnt = 0;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                        cnt = cnt + 1;
                }

                if (cnt == 0)
                    return;

                LB_TITLE.Text = cnt.ToString() + " records are selected";
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                    "set @BATCH_ID = NEWID() " +
                                    "insert into BATCH_MASTER " +
                                    "select @BATCH_ID, 'SV_MAT', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE() " +
                                    "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery();
            string batchid = conn.GetFieldValue("BATCH_ID").ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    //try
                    //{
                        conn.QueryString = "exec SP_APPLICATION_SAVING_MATURITY_INSERT " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'" + batchid + "'," +
                                            "'" + TXT_ACCNO.Text.Trim() + "'," +
                                            "'" + DDL_BANK.SelectedValue + "'," +
                                            "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    //}
                    //catch { }
                }
            }

            FillDGR();
        }

        protected void BT_INQUIRY_Click(object sender, EventArgs e)
        {

            try
            {
                string accountNo = TXT_ACCNO.Text.Trim();
                string accountName = TXT_ACCNAME.Text.Trim();
                string bankCode = DDL_BANK.SelectedValue;

                conn.QueryString = "select CLEARING_CODE from FINANCE.dbo.PARAM_TBL_BANK where KODE = '" + bankCode + "'";
                conn.ExecuteQuery(3000);

                string clearingCode = conn.GetFieldValue("CLEARING_CODE").ToString().Substring(0, 3).Trim(); //"009";
                string accounBMITakaful = "3040031803";
                string transferAmount = "0";
                string transferDesc = "-";
                string userBy = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                conn.QueryString = "exec FINANCE.dbo.SP_API_BMI_INQUIRY " +
                                    "'" + clearingCode + "'," +
                                    "'" + accounBMITakaful + "'," +
                                    "'" + accountNo + "'," +
                                    "'" + transferAmount + "'," +
                                    "'" + transferDesc + "'," +
                                    "'" + userBy + "'";


                conn.ExecuteQuery(3000);

                if (conn.GetRowCount() > 0)
                {

                    if (conn.GetFieldValue("errorCode").ToString() == "00")
                    {

                        lblDestName.Text = conn.GetFieldValue("toAccName").ToString();
                        lblDestAccNo.Text = accountNo;
                        lblDestBank.Text = DDL_BANK.SelectedItem.Text;

                        spanInquery.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('ERROR - " + conn.GetFieldValue("errorDesc").ToString() + "')", true);

                        spanInquery.Visible = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + ex.Message.ToString() + "')", true);
            }

        }

    }
}
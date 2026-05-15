using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace CUSTOMERS.Form_Client
{
    public partial class CompanyHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                try
                {
                    TXT_CODE.Text = Request.QueryString["code"];
                    LoadRecord(TXT_CODE.Text);

                    if (TXT_CODE.Text == "" || DDL_STATUS.SelectedValue != "002")
                    {
                        TR_BUTTON.Visible = false;
                    }
                }
                catch { }
            }
        }

        protected void Setup()
        {

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_TYPE";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_LOB";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_PROPINSI";
            conn.ExecuteQuery();
            DDL_PROPINSI.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_CATEGORY";
            conn.ExecuteQuery();
            DDL_CATEGORY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CATEGORY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_STATUS order by 1";
            conn.ExecuteQuery();
            DDL_STATUS.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_COMPANY_STATUS order by 1";
            conn.ExecuteQuery();
            DDL_STATUS.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DDL_COMPANY_GROUP.Items.Clear();
            DDL_COMPANY_GROUP.DataSource = getCompanyGroup();
            DDL_COMPANY_GROUP.DataValueField = "ID";
            DDL_COMPANY_GROUP.DataTextField = "NAME";
            DDL_COMPANY_GROUP.DataBind();
            DDL_COMPANY_GROUP.Items.Add(new ListItem("Pilih", ""));
            DDL_COMPANY_GROUP.SelectedValue = "";
        }

        private DataTable getCompanyGroup()
        {
            DataTable dt = new DataTable();
            try
            {
                conn.QueryString = "select * from PR_COMPANY_GROUP";
                conn.ExecuteQuery();

                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select * from V_COMPANY where COMPANY_CODE = '" + code + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TXT_ADDRESS1.Text = conn.GetFieldValue("COMPANY_ADDRESS").ToString();
            TXT_ADDRESS2.Text = conn.GetFieldValue("COMPANY_ADDRESS2").ToString();
            TXT_COMPANY_NAME.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("COMPANY_EMAIL").ToString();
            TXT_FAX.Text = conn.GetFieldValue("COMPANY_FAX").ToString();
            TXT_KOTAMADYA.Text = conn.GetFieldValue("KOTAMADYA").ToString();
            TXT_NPWP.Text = conn.GetFieldValue("COMPANY_NPWP").ToString();
            TXT_PHONE.Text = conn.GetFieldValue("COMPANY_PHONE").ToString();
            TXT_PIC1.Text = conn.GetFieldValue("COMPANY_PIC").ToString();
            TXT_PIC2.Text = conn.GetFieldValue("COMPANY_PIC2").ToString();
            TXT_PICTITLE.Text = conn.GetFieldValue("COMPANY_PIC_JABATAN").ToString();
            TXT_REGDATE.Text = conn.GetFieldValue("COMPANY_REGISTERED_DATE").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("KODE_POS").ToString();

            try
            {
                DDL_CATEGORY.SelectedValue = conn.GetFieldValue("COMPANY_CATEGORY").ToString();
            }
            catch { }

            try
            {
                DDL_LOB.SelectedValue = conn.GetFieldValue("COMPANY_LOB").ToString();
            }
            catch { }

            try
            {
                DDL_PROPINSI.SelectedValue = conn.GetFieldValue("PROPINSI").ToString();
            }
            catch { }

            try
            {
                DDL_STATUS.SelectedValue = conn.GetFieldValue("STAT").ToString();
            }
            catch { }

            try
            {
                DDL_TIPE.SelectedValue = conn.GetFieldValue("COMPANY_TYPE").ToString();
            }
            catch { }

            if (DDL_STATUS.SelectedValue != "001")
                ShowBranch();

            //binding company group
            var companyGroupID = conn.GetFieldValue("COMPANY_GROUP_ID").ToString();
            if (!string.IsNullOrEmpty(companyGroupID))
            {
                DDL_COMPANY_GROUP.SelectedValue = companyGroupID;
            }
            else {
                DDL_COMPANY_GROUP.SelectedValue = string.Empty;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            //try
            //{
            conn.QueryString = "exec SP_COMPANY_UPSERT " +
                                "'" + TXT_CODE.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "'" + TXT_COMPANY_NAME.Text.Trim() + "'," +
                                "'" + DDL_TIPE.SelectedValue + "'," +
                                "'" + DDL_CATEGORY.SelectedValue + "'," +
                                "'" + DDL_LOB.SelectedValue + "'," +
                                "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                "'" + TXT_ADDRESS2.Text.Trim() + "'," +
                                "'" + TXT_KOTAMADYA.Text.Trim() + "'," +
                                "'" + DDL_PROPINSI.SelectedValue + "'," +
                                "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                "'" + TXT_PHONE.Text.Trim() + "'," +
                                "'" + TXT_FAX.Text.Trim() + "'," +
                                "'" + TXT_EMAIL.Text.Trim() + "'," +
                                "'" + TXT_PIC1.Text.Trim() + "'," +
                                "'" + TXT_PIC2.Text.Trim() + "'," +
                                "'" + TXT_PICTITLE.Text.Trim() + "'," +
                                "'" + TXT_NPWP.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            var companyCode = conn.GetFieldValue("COMPANY_CODE").ToString();

            //insert or update company group
            if (!string.IsNullOrEmpty(companyCode) && !string.IsNullOrEmpty(DDL_COMPANY_GROUP.SelectedValue.Trim()))
            {
                var sql = string.Format("exec SP_COMPANY_GROUP_UPSERT {0}, {1}", companyCode, DDL_COMPANY_GROUP.SelectedValue.Trim());
                conn.QueryString =  sql;
                conn.ExecuteQuery();
            }

            TXT_CODE.Text = companyCode;
            LoadRecord(conn.GetFieldValue("COMPANY_CODE").ToString());
            //}
            //catch (SystemException ex)
            //{
            //    LB_ERROR.Text = ex.Message;
            //}
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowBranch();
        }

        protected void ShowBranch()
        {
            LBL_TITLE.Text = BT1.Text;
            Response.Write("<script language='javascript'>parent.companybody.location.href = 'CompanyBranch.aspx?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.companybody.location.href = 'CompanyAgent.aspx?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            Response.Write("<script language='javascript'>parent.companybody.location.href = '" + URL + "';</script>");
        }
    }
}
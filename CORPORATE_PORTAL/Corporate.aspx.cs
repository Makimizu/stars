using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CORPORATE_PORTAL
{
    public partial class Corporate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_TYPE";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_LINE_OF_BUSINESS";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI";
            conn.ExecuteQuery();
            DDL_PROPINSI.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_PROPCAB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_CATEGORY";
            conn.ExecuteQuery();
            DDL_CATEGORY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CATEGORY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            LoadCompany(GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
        }

        protected void EmptyCompany()
        {
            TXT_CODE.Text = "";
            LB_COMPANY_NAME.Text = "";
            TXT_ADDRESS1.Text = "";
            TXT_ADDRESS2.Text = "";
            TXT_EMAIL.Text = "";
            TXT_FAX.Text = "";
            TXT_KOTAMADYA.Text = "";
            TXT_NPWP.Text = "";
            TXT_PHONE.Text = "";
            TXT_PIC1.Text = "";
            TXT_PIC2.Text = "";
            TXT_PICTITLE.Text = "";
            TXT_ZIPCODE.Text = "";

            DDL_CATEGORY.SelectedIndex = 0;
            DDL_LOB.SelectedIndex = 0;
            DDL_PROPINSI.SelectedIndex = 0;
            DDL_TIPE.SelectedIndex = 0;
        }

        protected void EmptyBranch()
        {
            TXT_KDCAB.Text = "";
            TXT_NAMACAB.Text = "";
            TXT_ALAMATCAB1.Text = "";
            TXT_ALAMATCAB2.Text = "";
            TXT_PHNCAB.Text = "";
            TXT_FAXCAB.Text = "";
            TXT_EMAILCAB.Text = "";
            TXT_PICCAB.Text = "";
            TXT_PICTITLECAB.Text = "";
            TXT_PICEMAILCAB.Text = "";
            TXT_KODEPOSCAB.Text = "";
            TXT_KOTAMADYACAB.Text = "";
        }

        protected void LoadCompany(string ID)
        {
            conn.QueryString = "select " +
                                    "a.* " +
                                    "from V_LINK_CB_COMPANY a " +
                                    "inner join V_LINK_HO_POLICY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                    "where " +
                                    "b.ID = " + ID;
            conn.ExecuteQuery();

            TXT_CODE.Text = conn.GetFieldValue("COMPANY_CODE").ToString();
            LB_COMPANY_NAME.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            TXT_ADDRESS1.Text = conn.GetFieldValue("COMPANY_ADDRESS").ToString();
            TXT_ADDRESS2.Text = conn.GetFieldValue("COMPANY_ADDRESS2").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("COMPANY_EMAIL").ToString();
            TXT_FAX.Text = conn.GetFieldValue("COMPANY_FAX").ToString();
            TXT_KOTAMADYA.Text = conn.GetFieldValue("KOTAMADYA").ToString();
            TXT_NPWP.Text = conn.GetFieldValue("COMPANY_NPWP").ToString();
            TXT_PHONE.Text = conn.GetFieldValue("COMPANY_PHONE").ToString();
            TXT_PIC1.Text = conn.GetFieldValue("COMPANY_PIC").ToString();
            TXT_PIC2.Text = conn.GetFieldValue("COMPANY_PIC2").ToString();
            TXT_PICTITLE.Text = conn.GetFieldValue("COMPANY_PIC_JABATAN").ToString();
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
                DDL_TIPE.SelectedValue = conn.GetFieldValue("COMPANY_TYPE").ToString();
            }
            catch { }

            FillDDLBranch(TXT_CODE.Text);
        }

        protected void FillDDLBranch(string company)
        {
            EmptyBranch();

            conn.QueryString = "select BRANCH_CODE, NAMA_CABANG from V_LINK_CB_BRANCH where COMPANY_CODE = '" + company + "' order by 2";
            conn.ExecuteQuery();
            DDL_BRANCH.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            LoadBranch(DDL_BRANCH.SelectedValue);
        }

        protected void BT_NEWCAB_Click(object sender, EventArgs e)
        {
            EmptyBranch();
        }

        protected void BT_SAVE_CAB_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_LINK_CB_BRANCH_UPSERT " +
                                    "'" + TXT_KDCAB.Text + "'," +
                                    "'" + TXT_CODE.Text + "'," +
                                    "''," +
                                    "''," +
                                    "'" + TXT_NAMACAB.Text.Trim() + "'," +
                                    "'" + TXT_ALAMATCAB1.Text.Trim() + "'," +
                                    "'" + TXT_ALAMATCAB2.Text.Trim() + "'," +
                                    "'" + TXT_PHNCAB.Text.Trim() + "'," +
                                    "'" + TXT_FAXCAB.Text.Trim() + "'," +
                                    "'" + TXT_EMAILCAB.Text.Trim() + "'," +
                                    "'" + TXT_PICCAB.Text.Trim() + "'," +
                                    "'" + TXT_PICTITLECAB.Text.Trim() + "'," +
                                    "'" + TXT_PICEMAILCAB.Text.Trim() + "'," +
                                    "'" + TXT_KODEPOSCAB.Text.Trim() + "'," +
                                    "'" + TXT_KOTAMADYACAB.Text.Trim() + "'," +
                                    "'" + DDL_PROPCAB.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                TXT_KDCAB.Text = conn.GetFieldValue("BRANCH_CODE").ToString();

            }
            catch (SystemException ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DDL_BRANCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBranch(DDL_BRANCH.SelectedValue);
        }

        protected void LoadBranch(string branch)
        {
            conn.QueryString = "select * from V_LINK_CB_BRANCH where BRANCH_CODE = '" + branch + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TXT_KDCAB.Text = conn.GetFieldValue(0, "BRANCH_CODE").ToString();
            TXT_NAMACAB.Text = conn.GetFieldValue(0, "NAMA_CABANG").ToString();
            TXT_ALAMATCAB1.Text = conn.GetFieldValue(0, "BRANCH_ADDRESS1").ToString();
            TXT_ALAMATCAB2.Text = conn.GetFieldValue(0, "BRANCH_ADDRESS2").ToString();
            TXT_PHNCAB.Text = conn.GetFieldValue(0, "BRANCH_PHONE").ToString();
            TXT_FAXCAB.Text = conn.GetFieldValue(0, "BRANCH_FAX").ToString();
            TXT_EMAILCAB.Text = conn.GetFieldValue(0, "BRANCH_EMAIL").ToString();
            TXT_PICCAB.Text = conn.GetFieldValue(0, "BRANCH_PIC").ToString();
            TXT_PICTITLECAB.Text = conn.GetFieldValue(0, "BRANCH_PIC_TITLE").ToString();
            TXT_PICEMAILCAB.Text = conn.GetFieldValue(0, "BRANCH_PIC_EMAIL").ToString();
            TXT_KODEPOSCAB.Text = conn.GetFieldValue(0, "BRANCH_ZIP").ToString();
            TXT_KOTAMADYACAB.Text = conn.GetFieldValue(0, "KOTAMADYA").ToString();

            try
            {
                DDL_PROPCAB.SelectedValue = conn.GetFieldValue(0, "PROPINSI").ToString();
            }
            catch { }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_LINK_CB_COMPANY_UPSERT " +
                                    "'" + TXT_CODE.Text.Trim() + "'," +
                                    "'" + LB_COMPANY_NAME.Text.Trim() + "'," +
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
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

    }
}
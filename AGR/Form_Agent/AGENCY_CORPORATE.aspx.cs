using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR
{
    public partial class AGENCY_CORPORATE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                //try
                //{
                TXT_CODE.Text = Request.QueryString["ID"];
                LoadRecord(TXT_CODE.Text);
                ShowBranch();
                //}
                //catch { }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_TYPE";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_LOB";
            conn.ExecuteQuery();
            DDL_LOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI";
            conn.ExecuteQuery();
            DDL_PROPINSI.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_CATEGORY";
            conn.ExecuteQuery();
            DDL_CATEGORY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CATEGORY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_AREA order by 2";
            conn.ExecuteQuery();
            DDL_AREA.Items.Clear();
            DDL_AREA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AREA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select * from V_LINK_CB_COMPANY where COMPANY_CODE = '" + code + "'";
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
            TXT_PICTITLE.Text = conn.GetFieldValue("COMPANY_PIC_JABATAN").ToString();
            TXT_REGDATE.Text = conn.GetFieldValue("COMPANY_REGISTERED_DATE").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("KODE_POS").ToString();
            TXT_LASTCHANGEBY.Text = conn.GetFieldValue("COMPANY_LASTCHANGEBY").ToString();
            TXT_LASTCHANGEDATE.Text = conn.GetFieldValue("COMPANY_LASTCHANGEDATE").ToString();

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

            try
            {
                DDL_AREA.SelectedValue = conn.GetFieldValue("COMPANY_PIC2").ToString();
            }
            catch
            { }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_LINK_CB_COMPANY_UPSERT " +
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
                                "'" + DDL_AREA.SelectedValue + "'," +
                                "'" + TXT_PICTITLE.Text.Trim() + "'," +
                                "'" + TXT_NPWP.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            TXT_CODE.Text = conn.GetFieldValue("COMPANY_CODE").ToString();

            Response.Redirect("AGENCY_CORPORATE.aspx?ID=" + TXT_CODE.Text);
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowBranch();
        }

        protected void ShowBranch()
        {
            LBL_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgencyBody.location.href = 'AGENCY_CORPORATE_BRANCH.ASPX?ID=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL("CS", "CS_1", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgencyBody.location.href = '" + URL + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgencyBody.location.href = 'AGENCY_CORPORATE_PARAMREMUN.ASPX?ID=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgencyBody.location.href = 'AGENCY_CORPORATE_AGENT.ASPX?ID=" + TXT_CODE.Text + "';</script>");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace UWBOX.Form_Partners
{
    public partial class ParamReinsurance : System.Web.UI.Page
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
                }
                catch { }
            }
        }

        protected void Setup()
        {

            conn.QueryString = "select CODE,DESCR from PR_PROVINCE";
            conn.ExecuteQuery();
            DDL_PROPINSI.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select " +
                                "COMPANY_CODE, " +
                                "COMPANY_NAME = UPPER(COMPANY_NAME), " +
                                "COMPANY_ADDRESS, " +
                                "COMPANY_ADDRESS2, " +
                                "KOTAMADYA, " +
                                "PROVINCE_CODE, " +
                                "ZIP, " +
                                "COMPANY_PHONE, " +
                                "COMPANY_FAX, " +
                                "COMPANY_EMAIL, " +
                                "COMPANY_PIC, " +
                                "COMPANY_PIC_JABATAN, " +
                                "COMPANY_LICENCENO, " +
                                "COMPANY_LICENCEDATE = convert(varchar(20),COMPANY_LICENCEDATE,103) " +
                                "from V_PARAM_REINS_COMPANY " +
                                "where COMPANY_CODE = '" + code + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TXT_ADDRESS1.Text = conn.GetFieldValue("COMPANY_ADDRESS").ToString();
            TXT_ADDRESS2.Text = conn.GetFieldValue("COMPANY_ADDRESS2").ToString();
            TXT_COMPANY_NAME.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("COMPANY_EMAIL").ToString();
            TXT_FAX.Text = conn.GetFieldValue("COMPANY_FAX").ToString();
            TXT_KOTAMADYA.Text = conn.GetFieldValue("KOTAMADYA").ToString();
            TXT_LICENCENO.Text = conn.GetFieldValue("COMPANY_LICENCENO").ToString();
            TXT_LICENCEDATE.Text = conn.GetFieldValue("COMPANY_LICENCEDATE").ToString();
            TXT_PHONE.Text = conn.GetFieldValue("COMPANY_PHONE").ToString();
            TXT_PIC1.Text = conn.GetFieldValue("COMPANY_PIC").ToString();
            TXT_PICTITLE.Text = conn.GetFieldValue("COMPANY_PIC_JABATAN").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIP").ToString();


            try
            {
                DDL_PROPINSI.SelectedValue = conn.GetFieldValue("PROVINCE_CODE").ToString();
            }
            catch { }


        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            /*
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

            TXT_CODE.Text = conn.GetFieldValue("COMPANY_CODE").ToString();
            LoadRecord(conn.GetFieldValue("COMPANY_CODE").ToString());
            //}
            //catch (SystemException ex)
            //{
            //    LB_ERROR.Text = ex.Message;
            //}
            */
        }
    }
}
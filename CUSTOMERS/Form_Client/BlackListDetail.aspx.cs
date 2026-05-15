using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace CUSTOMERS.Form_Client
{
    public partial class BlackListDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
                LoadMember(LB_ID.Text);
            }
        }

        protected void Setup()
        {
            LB_ID.Text = Request.QueryString["ID"];

            conn.QueryString = "select CODE,DESCR from PR_BLACKLIST_FLAG";
            conn.ExecuteQuery();
            DDL_FLAG.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FLAG.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_BLACKLIST_SOURCE";
            conn.ExecuteQuery();
            DDL_SOURCE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            frm1.Src = "../../ARCHIEVE/Arsip.aspx?app=CS&tipe=CS_2&owner1=" + LB_ID.Text + "&owner2=&owner3=&user=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
        }

        protected void LoadMember(string ID)
        {
            conn.QueryString = "select * from V_MEMBER_MASTER_BLACKLIST where ID = '" + ID + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS1.Text = conn.GetFieldValue("ADDRESS_1").ToString();
            TXT_ADDRESS2.Text = conn.GetFieldValue("ADDRESS_2").ToString();
            TXT_CITY.Text = conn.GetFieldValue("CITY").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_IDNO.Text = conn.GetFieldValue("ID_NO").ToString();
            TXT_MMN.Text = conn.GetFieldValue("MMN").ToString();
            TXT_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            TXT_PHONE1.Text = conn.GetFieldValue("PHONE_1").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE_2").ToString();
            TXT_POB.Text = conn.GetFieldValue("POB").ToString();
            TXT_TAXNO.Text = conn.GetFieldValue("TAX_NO").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIP_CODE").ToString();
            TXT_CITIZENSHIP.Text = conn.GetFieldValue("CITIZENSHIP").ToString();
            TXT_COUNTRY.Text = conn.GetFieldValue("COUNTRY").ToString();
            TXT_IDTYPE.Text = conn.GetFieldValue("ID_TYPE").ToString();
            TXT_JOB.Text = conn.GetFieldValue("JOB").ToString();
            TXT_MARITAL.Text = conn.GetFieldValue("MARITAL_STATUS").ToString();
            TXT_PROVINCE.Text = conn.GetFieldValue("PROVINCE").ToString();
            TXT_RELIGION.Text = conn.GetFieldValue("RELIGION").ToString();
            TXT_EDUCATION.Text = conn.GetFieldValue("EDUCATION").ToString();
            TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();

            DateTime dob = DateTime.Parse(conn.GetFieldValue("DOB").ToString());
            TXT_DOB.Text = dob.Day.ToString() + "/" + dob.Month.ToString() + "/" + dob.Year.ToString();

            try
            {
                DDL_SEX.SelectedValue = conn.GetFieldValue("SEX").ToString();
            }
            catch { }

            try
            {
                DDL_FLAG.SelectedValue = conn.GetFieldValue("FLAG").ToString();
            }
            catch { }

            try
            {
                DDL_SOURCE.SelectedValue = conn.GetFieldValue("SOURCE").ToString();
            }
            catch { }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            try
            {
                conn.QueryString = "exec SP_MEMBER_MASTER_BLACKLIST_UPSERT " +
                                "'" + TXT_NAME.Text + "'," +
                                "'" + DDL_SEX.SelectedValue.ToString() + "'," +
                                "'" + TXT_MMN.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + TXT_POB.Text + "'," +
                                "'" + TXT_JOB.Text + "'," +
                                "'" + TXT_RELIGION.Text + "'," +
                                "'" + TXT_MARITAL.Text + "'," +
                                "'" + TXT_IDTYPE.Text + "'," +
                                "'" + TXT_IDNO.Text + "'," +
                                "'" + TXT_TAXNO.Text + "'," +
                                "'" + TXT_CITIZENSHIP.Text + "'," +
                                "'" + TXT_PHONE1.Text + "'," +
                                "'" + TXT_PHONE2.Text + "'," +
                                "'" + TXT_EMAIL.Text + "'," +
                                "'" + TXT_ADDRESS1.Text + "'," +
                                "'" + TXT_ADDRESS2.Text + "'," +
                                "'" + TXT_CITY.Text + "'," +
                                "'" + TXT_PROVINCE.Text + "'," +
                                "'" + TXT_COUNTRY.Text + "'," +
                                "'" + TXT_ZIPCODE.Text + "'," +
                                "'" + TXT_EDUCATION.Text + "'," +
                                "'" + DDL_FLAG.SelectedValue.ToString() + "'," +
                                "'" + DDL_SOURCE.SelectedValue.ToString() + "'," +
                                "'" + TXT_REMARK.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
            }catch(Exception ex)
            {
                LB_ERROR.Text = ex.Message.ToString();
            }

            //Response.Redirect("BlackListDetail.aspx?ID=" + LB_ID.Text);
            LB_ERROR.Text = "Data Saved";
        }
    }
}
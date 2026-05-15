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
    public partial class PersonalDetail : System.Web.UI.Page
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

            conn.QueryString = "select CODE,DESCR from PR_PROPINSI";
            conn.ExecuteQuery();
            DDL_PROVINCE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_CITIZENSHIP";
            conn.ExecuteQuery();
            DDL_CITIZENSHIP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CITIZENSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_COUNTRY";
            conn.ExecuteQuery();
            DDL_COUNTRY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_IDTYPE";
            conn.ExecuteQuery();
            DDL_IDTYPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_IDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_JOB";
            conn.ExecuteQuery();
            DDL_JOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_JOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_MARITAL";
            conn.ExecuteQuery();
            DDL_MARITAL.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MARITAL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_RELIGION";
            conn.ExecuteQuery();
            DDL_RELIGION.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_RELIGION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from PR_BANK";
            conn.ExecuteQuery();
            DDL_BANK.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void LoadMember(string ID)
        {
            conn.QueryString = "select * from V_MEMBER_MASTER where ID = '" + ID + "'";
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


            DateTime dob = DateTime.Parse(conn.GetFieldValue("DOB").ToString());
            TXT_DOB.Text = dob.Day.ToString() + "/" + dob.Month.ToString() + "/" + dob.Year.ToString();


            try
            {
                DDL_CITIZENSHIP.SelectedValue = conn.GetFieldValue("CITIZENSHIP").ToString();
            }
            catch { }

            try
            {
                DDL_COUNTRY.SelectedValue = conn.GetFieldValue("COUNTRY").ToString();
            }
            catch { }

            try
            {
                DDL_IDTYPE.SelectedValue = conn.GetFieldValue("ID_TYPE").ToString();
            }
            catch { }

            try
            {
                DDL_JOB.SelectedValue = conn.GetFieldValue("JOB").ToString();
            }
            catch { }

            try
            {
                DDL_MARITAL.SelectedValue = conn.GetFieldValue("MARITAL_STATUS").ToString();
            }
            catch { }

            try
            {
                DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
            }
            catch { }

            try
            {
                DDL_RELIGION.SelectedValue = conn.GetFieldValue("RELIGION").ToString();
            }
            catch { }

            try
            {
                DDL_SEX.SelectedValue = conn.GetFieldValue("SEX").ToString();
            }
            catch { }

            LoadMemberBankAcc(ID);
        }

        protected void LoadMemberBankAcc(string ID)
        {
            conn.QueryString = "select * from MEMBER_BANK_ACCOUNT where MEMBER_ID = '" + ID + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
                TXT_ACCNAMA.Text = conn.GetFieldValue("ACCNAME").ToString();

                try
                {
                    DDL_BANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            conn.QueryString = "exec SP_MEMBER_MASTER_VERIFY " +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + DDL_SEX.SelectedValue + "'," +
                                "'" + TXT_MMN.Text.Trim() + "'," +
                                "'" + TXT_DOB.Text.Trim() + "'," +
                                "'" + TXT_POB.Text.Trim() + "'," +
                                "'" + DDL_JOB.SelectedValue + "'," +
                                "'" + DDL_RELIGION.SelectedValue + "'," +
                                "'" + DDL_MARITAL.SelectedValue + "'," +
                                "'" + DDL_IDTYPE.SelectedValue + "'," +
                                "'" + TXT_IDNO.Text.Trim() + "'," +
                                "'" + TXT_TAXNO.Text.Trim() + "'," +
                                "'" + DDL_CITIZENSHIP.SelectedValue + "'," +
                                "'" + TXT_PHONE1.Text.Trim() + "'," +
                                "'" + TXT_PHONE2.Text.Trim() + "'," +
                                "'" + TXT_EMAIL.Text.Trim() + "'," +
                                "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                "'" + TXT_ADDRESS2.Text.Trim() + "'," +
                                "'" + TXT_CITY.Text.Trim() + "'," +
                                "'" + DDL_PROVINCE.SelectedValue + "'," +
                                "'" + DDL_COUNTRY.SelectedValue + "'," +
                                "'" + TXT_ZIPCODE.Text.Trim() + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                LB_ERROR.Text = "<table style='border-spacing:0px; width:100%;'>";
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "<tr><td>-</td><td>" + conn.GetFieldValue(i, 0).ToString() + "</td></tr>";
                }
                LB_ERROR.Text = LB_ERROR.Text + "</table>";
                return;
            }

            string ID = "null";
            if (LB_ID.Text != "")
                ID = "'" + LB_ID.Text + "'";

            conn.QueryString = "exec SP_MEMBER_MASTER_SIMILARITY " +
                                ID + "," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + DDL_SEX.SelectedValue + "'," +
                                "'" + TXT_MMN.Text.Trim() + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + TXT_POB.Text.Trim() + "'," +
                                "'" + DDL_JOB.SelectedValue + "'," +
                                "'" + DDL_RELIGION.SelectedValue + "'," +
                                "'" + DDL_MARITAL.SelectedValue + "'," +
                                "'" + DDL_IDTYPE.SelectedValue + "'," +
                                "'" + TXT_IDNO.Text.Trim() + "'," +
                                "'" + TXT_TAXNO.Text.Trim() + "'," +
                                "'" + DDL_CITIZENSHIP.SelectedValue + "'," +
                                "'" + TXT_PHONE1.Text.Trim() + "'," +
                                "'" + TXT_PHONE2.Text.Trim() + "'," +
                                "'" + TXT_EMAIL.Text.Trim() + "'," +
                                "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                "'" + TXT_ADDRESS2.Text.Trim() + "'," +
                                "'" + TXT_CITY.Text.Trim() + "'," +
                                "'" + DDL_PROVINCE.SelectedValue + "'," +
                                "'" + DDL_COUNTRY.SelectedValue + "'," +
                                "'" + TXT_ZIPCODE.Text.Trim() + "'";
            conn.ExecuteQuery();

            Save();
            Response.Redirect("PersonalDetail.aspx?ID=" + LB_ID.Text);
        }

        protected void Save()
        {
            LB_ERROR.Text = "";

            string ID = "null";
            if (LB_ID.Text != "")
                ID = "'" + LB_ID.Text + "'";

            string userid = "";
            try
            {
                userid = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            }
            catch
            {
                userid = Request.QueryString["USERBY"];
            }

            try
            {
                conn.QueryString = "exec SP_MEMBER_MASTER_UPSERT " +
                                    ID + "," +
                                    "'" + TXT_NAME.Text.Trim() + "'," +
                                    "'" + DDL_SEX.SelectedValue + "'," +
                                    "'" + TXT_MMN.Text.Trim() + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + TXT_POB.Text.Trim() + "'," +
                                    "'" + DDL_JOB.SelectedValue + "'," +
                                    "'" + DDL_RELIGION.SelectedValue + "'," +
                                    "'" + DDL_MARITAL.SelectedValue + "'," +
                                    "'" + DDL_IDTYPE.SelectedValue + "'," +
                                    "'" + TXT_IDNO.Text.Trim() + "'," +
                                    "'" + TXT_TAXNO.Text.Trim() + "'," +
                                    "'" + DDL_CITIZENSHIP.SelectedValue + "'," +
                                    "'" + TXT_PHONE1.Text.Trim() + "'," +
                                    "'" + TXT_PHONE2.Text.Trim() + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                    "'" + TXT_ADDRESS2.Text.Trim() + "'," +
                                    "'" + TXT_CITY.Text.Trim() + "'," +
                                    "'" + DDL_PROVINCE.SelectedValue + "'," +
                                    "'" + DDL_COUNTRY.SelectedValue + "'," +
                                    "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                    "'" + userid + "'";
                conn.ExecuteQuery();


                if (TXT_ACCNAMA.Text.Trim() == "" || TXT_ACCNO.Text.Trim() == "")
                    return;

                conn.QueryString = "exec SP_MEMBER_BANK_ACCOUNT_UPSERT " +
                                    ID + "," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                    "'" + DDL_BANK.SelectedValue + "'," +
                                    "'" + userid + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }

        }
    }
}
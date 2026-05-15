using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LQ.Form_Client
{
    public partial class MemberEntry : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("../logout.aspx");
                Setup();
                if (LB_ID.Text != "")
                    LoadMember(LB_ID.Text);

                FillDGRPolicy();
            }
        }

        protected void FillDGRPolicy()
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER_ACTIVE '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DGR_MEMBER.DataSource = conn.GetDataTable().Copy();
                DGR_MEMBER.DataBind();


                DV_MEMBER.Visible = true;

                TXT_ACCNAMA.Enabled = false;
                TXT_ACCNO.Enabled = false;
                TXT_ADDRESS1.Enabled = false;
                TXT_ADDRESS2.Enabled = false;
                TXT_BANK.Visible = false;
                TXT_CITIZENSHIP.Visible = false;
                TXT_CITY.Visible = false;
                TXT_COUNTRY.Visible = false;
                TXT_DOB.Enabled = false;
                TXT_EMAIL.Enabled = false;
                TXT_IDNO.Enabled = false;
                TXT_MMN.Enabled = false;
                TXT_NAME.Enabled = false;
                TXT_PHONE1.Enabled = false;
                TXT_PHONE2.Enabled = false;
                TXT_POB.Enabled = false;
                TXT_PROVINCE.Visible = false;
                TXT_TAXNO.Enabled = false;
                TXT_ZIPCODE.Enabled = false;

                DDL_BANK.Enabled = false;
                DDL_CITIZENSHIP.Enabled = false;
                DDL_CITY.Enabled = false;
                DDL_COUNTRY.Enabled = false;
                DDL_EDUCATION.Enabled = false;
                DDL_IDTYPE.Enabled = false;
                DDL_JOB.Enabled = false;
                DDL_MARITAL.Enabled = false;
                DDL_PHONE_COUNTRY_1.Enabled = false;
                DDL_PHONE_COUNTRY_2.Enabled = false;
                DDL_PROVINCE.Enabled = false;
                DDL_RELIGION.Enabled = false;
                DDL_SEX.Enabled = false;

                BT_SAVE.Visible = false;
            }
        }

        protected void LoadMember(string ID)
        {
            string cityCode = "";

            conn.QueryString = "select *, " +
                                "PHONE1 = case when LEFT(PHONE_1,1) = '+' then SUBSTRING(PHONE_1,4,LEN(PHONE_1)) else PHONE_1 end, " +
                                "PHONE2 = case when LEFT(PHONE_2,1) = '+' then SUBSTRING(PHONE_2,4,LEN(PHONE_2)) else PHONE_2 end " +
                                "from V_LINK_CB_MEMBER_MASTER where ID = '" + ID + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS1.Text = conn.GetFieldValue("ADDRESS_1").ToString();
            TXT_ADDRESS2.Text = conn.GetFieldValue("ADDRESS_2").ToString();
            //TXT_CITY.Text = conn.GetFieldValue("CITY").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_IDNO.Text = conn.GetFieldValue("ID_NO").ToString();
            TXT_MMN.Text = conn.GetFieldValue("MMN").ToString();
            TXT_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            TXT_PHONE1.Text = conn.GetFieldValue("PHONE1").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_POB.Text = conn.GetFieldValue("POB").ToString();
            TXT_TAXNO.Text = conn.GetFieldValue("TAX_NO").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIP_CODE").ToString();

            cityCode = conn.GetFieldValue("CITY").ToString();

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

            try
            {
                DDL_EDUCATION.SelectedValue = conn.GetFieldValue("EDUCATION").ToString();
            }
            catch { }

            try
            {
                FillDDLCity();
                DDL_CITY.SelectedValue = cityCode;
            }
            catch { }

            LoadMemberBankAcc(ID);
        }

        protected void LoadMemberBankAcc(string ID)
        {
            conn.QueryString = "select * from V_LINK_CB_MEMBER_BANK_ACCOUNT where MEMBER_ID = '" + ID + "'";
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

        protected void Setup()
        {
            LB_ID.Text = Request.QueryString["ID"];
            LB_QUOT.Text = Request.QueryString["QUOT"];
            try
            {
                LB_REGNO.Text = Request.QueryString["REGNO"];
                LB_MEMBER_TYPE.Text = Request.QueryString["MEMBER_TYPE"];
            }
            catch { }

            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99" || GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "25")
            {
                trow.Visible = false;
                trow2.Visible = false;
                trow3.Visible = false;
                trow4.Visible = false;
                trow5.Visible = false;
                //trow6.Visible = false;
                trow7.Visible = false;
                trow8.Visible = false;
                trow9.Visible = false;
                trow10.Visible = false;
                trow11.Visible = false;
                trow12.Visible = false;
                trow13.Visible = false;
                trow14.Visible = false;
                trow15.Visible = false;
                trow16.Visible = false;
                trow17.Visible = false;
                trow18.Visible = false;
                trow19.Visible = false;
                trow20.Visible = false;
                trow21.Visible = false;
            }



            //conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI";
            //conn.ExecuteQuery();
            //DDL_PROVINCE.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}
            FillDDLProvince();

            //conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_CITIZENSHIP";
            //conn.ExecuteQuery();
            //DDL_CITIZENSHIP.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_CITIZENSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}
            FillDDLCitizenship();

            //conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY";
            //conn.ExecuteQuery();
            //DDL_COUNTRY.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}
            FillDDLCountry();

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_IDTYPE where code in ('000','002')";
            conn.ExecuteQuery();
            DDL_IDTYPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_IDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_JOB";
            conn.ExecuteQuery();
            DDL_JOB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_JOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_MARITAL";
            conn.ExecuteQuery();
            DDL_MARITAL.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MARITAL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_RELIGION";
            conn.ExecuteQuery();
            DDL_RELIGION.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_RELIGION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_EDUCATION";
            conn.ExecuteQuery();
            DDL_EDUCATION.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_EDUCATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_PHONE_COUNTRY_CODE order by DESCR";
            conn.ExecuteQuery();
            DDL_PHONE_COUNTRY_1.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PHONE_COUNTRY_1.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_PHONE_COUNTRY_CODE order by DESCR";
            conn.ExecuteQuery();
            DDL_PHONE_COUNTRY_2.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PHONE_COUNTRY_2.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLBank();
        }

        protected void FillDDLBank()
        {
            conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from V_LINK_CB_PR_BANK where DESCR like '%" + TXT_BANK.Text.Trim() + "%'";
            conn.ExecuteQuery();
            DDL_BANK.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLCitizenship()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_CITIZENSHIP where DESCR like '%" + TXT_CITIZENSHIP.Text.Trim() + "%'";
            conn.ExecuteQuery();
            DDL_CITIZENSHIP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CITIZENSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLProvince()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI where DESCR like '%" + TXT_PROVINCE.Text.Trim() + "%'";
            conn.ExecuteQuery();
            DDL_PROVINCE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLCountry()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY where DESCR like '%" + TXT_COUNTRY.Text.Trim() + "%'";
            conn.ExecuteQuery();
            DDL_COUNTRY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLCity()
        {
            DDL_CITY.Items.Clear();
            conn.QueryString = "select CODE = '', DESCR = '' union all " +
                                "select " +
                                "CODE = CITY_CODE, " +
                                "DESCR = CITY_NAME " +
                                "from V_LINK_CB_PARAM_CITY " +
                                "where " +
                                "PROVINCE_CODE = '" + DDL_PROVINCE.SelectedValue + "' " +
                                "and CITY_NAME like '%" + TXT_CITY.Text.Trim() + "%'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CITY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";


            conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_VERIFY " +
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
                                "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                "'" + DDL_PHONE_COUNTRY_1.SelectedValue + "'," +
                                "'" + DDL_PHONE_COUNTRY_2.SelectedValue + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                LB_ERROR.Text = "<table style='border-spacing:0px; width:100%;'>";
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "<tr><td>-</td><td>" + conn.GetFieldValue(i, 0).ToString() + "</td></tr>";
                }
                LB_ERROR.Text = LB_ERROR.Text + "</table>";
                ShowError();
                return;
            }


            if (LB_ID.Text == "")
            {

                string ID = "null";
                if (LB_ID.Text != "")
                    ID = "'" + LB_ID.Text + "'";

                conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_SIMILARITY " +
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

                if (conn.GetRowCount() > 0)
                {
                    ShowDGRSimilarity(conn);
                    ShowSimilarity();
                    return;
                }
            }

            Save();
            //Response.Redirect("Quotation.aspx?ID=&MEMBERID=" + LB_ID.Text);
        }

        protected void ShowDGRSimilarity(Connection conns)
        {
            LB_SIMCNT.Text = conns.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conns.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_FULLNAME");

                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;
            }

            if (Request.Browser.IsMobileDevice)
            {
                for (int i = 0; i < DGR.Columns.Count; i++)
                {
                    DGR.Columns[i].Visible = false;
                    switch (i)
                    {
                        case 2: DGR.Columns[i].Visible = true; break;
                        //START ASWIN
                        case 3: DGR.Columns[i].Visible = true; break;
                        case 4: DGR.Columns[i].Visible = true; break;
                        case 5: DGR.Columns[i].Visible = true; break;
                        //END
                        case 6: DGR.Columns[i].Visible = true; break;
                    }
                }
            }
        }

        protected void ShowError()
        {
            DV_ENTRY.Visible = false;
            DV_ERROR.Visible = true;
        }

        protected void ShowSimilarity()
        {
            DV_ENTRY.Visible = false;
            DV_SIMILARITY.Visible = true;
        }

        protected void BT_ERRORCLOSE_Click(object sender, EventArgs e)
        {
            DV_ENTRY.Visible = true;
            DV_ERROR.Visible = false;
        }

        protected void BT_SIMILARITYCLOSE_Click(object sender, EventArgs e)
        {
            DV_ENTRY.Visible = true;
            DV_SIMILARITY.Visible = false;
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (LB_QUOT.Text == "0")
                    Response.Redirect("MemberEntry.aspx?ID=" + e.Item.Cells[0].Text + "&QUOT=" + LB_QUOT.Text);
                else
                {
                    Response.Redirect("Quotation.aspx?ID=&MEMBERID=" + e.Item.Cells[0].Text);
                }
            }
        }

        protected void Save()
        {
            LB_ERROR.Text = "";

            string ID = "null";
            string AGE;

            if (LB_ID.Text != "")
                ID = "'" + LB_ID.Text + "'";

            conn.QueryString = "select " +
                                "START_AGE		= dbo.UFN_AGE_CALC(b.DOB, a.START_DATE, '1') " +
                                "from			APPLICATION_MASTER a " +
                                "inner join		CLIENT_BASE.dbo.MEMBER_MASTER b on b.ID = " + ID + " " +
                                "where " +
                                "a.REGNO		= '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            AGE = conn.GetFieldValue("START_AGE").ToString();

            try
            {
                conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_UPSERT " +
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
                                    "'" + DDL_CITY.SelectedValue + "'," +
                                    "'" + DDL_PROVINCE.SelectedValue + "'," +
                                    "'" + DDL_COUNTRY.SelectedValue + "'," +
                                    "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                    "'" + DDL_EDUCATION.SelectedValue + "'," +
                                    "'" + DDL_PHONE_COUNTRY_1.SelectedValue + "'," +
                                    "'" + DDL_PHONE_COUNTRY_2.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (TXT_ACCNO.Text != "")
                {
                    conn.QueryString = "exec SP_LINK_CB_MEMBER_BANK_ACCOUNT_UPSERT " +
                                            ID + "," +
                                            "'" + TXT_ACCNO.Text.Trim() + "'," +
                                            "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                            "'" + DDL_BANK.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                LB_ID.Text = conn.GetFieldValue("ID").ToString();


                conn.QueryString = "exec SP_APPLICATION_ADDRESS_CHECK " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();


                if (LB_MEMBER_TYPE.Text.Trim() == "1")
                {
                    conn.QueryString = "exec	SP_APPLICATION_MAIN_MEMBER_UPDATE " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'1'," +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }


                Task.Run(() => MemberAgeChangeCheck(AGE));


                /*
                string isPolicyHolder = "0";
                conn.QueryString = "select " + 
                                    "ISPOLICY_HOLDER = case when a.MEMBER_ID = b.MEMBER_ID then 1 else 0 end " + 
                                    "from APPLICATION_MEMBER a  " + 
                                    "inner join APPLICATION_MEMBER b on a.REGNO = b.REGNO and b.MEMBER_TYPE = '3' " + 
                                    "where " + 
                                    "a.REGNO = '20220419111027990' " + 
                                    "and a.MEMBER_TYPE = '1' ";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("ISPOLICY_HOLDER").ToString() == "1")
                {
                    isPolicyHolder = "1";
                }

                if ((isPolicyHolder == "1" || LB_MEMBER_TYPE.Text.Trim() == "3") && LB_REGNO.Text.Trim() != "")
                {
                    conn.QueryString = "exec SP_APPLICATION_ADDRESS_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'COR'," +
                                        "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                        "'" + DDL_CITY.SelectedValue + "'," +
                                        "'" + DDL_PROVINCE.SelectedValue + "'," +
                                        "'" + DDL_COUNTRY.SelectedValue + "'," +
                                        "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                        "'" + TXT_PHONE1.Text.Trim() + "'," +
                                        "'" + TXT_PHONE2.Text.Trim() + "'," +
                                        "'" + TXT_EMAIL.Text.Trim() + "'," +
                                        "'" + DDL_PHONE_COUNTRY_1.SelectedValue + "'," +
                                        "'" + DDL_PHONE_COUNTRY_2.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    conn.QueryString = "exec SP_APPLICATION_ADDRESS_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'HOM'," +
                                        "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                        "'" + DDL_CITY.SelectedValue + "'," +
                                        "'" + DDL_PROVINCE.SelectedValue + "'," +
                                        "'" + DDL_COUNTRY.SelectedValue + "'," +
                                        "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                        "'" + TXT_PHONE1.Text.Trim() + "'," +
                                        "'" + TXT_PHONE2.Text.Trim() + "'," +
                                        "'" + TXT_EMAIL.Text.Trim() + "'," +
                                        "'" + DDL_PHONE_COUNTRY_1.SelectedValue + "'," +
                                        "'" + DDL_PHONE_COUNTRY_2.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                */
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                ShowError();
            }
        }

        protected void MemberAgeChangeCheck(string AGE)
        {
            conn.QueryString = "exec	SP_APPLICATION_MEMBER_AGE_CHANGE_CHECK " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_ID.Text + "'," +
                                AGE + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
        }

        protected void BT_CONTINUE_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("Quotation.aspx?ID=&MEMBERID=" + LB_ID.Text);
        }

        protected void TXT_BANK_TextChanged(object sender, EventArgs e)
        {
            FillDDLBank();
        }

        protected void TXT_CITIZENSHIP_TextChanged(object sender, EventArgs e)
        {
            FillDDLCitizenship();
        }

        protected void TXT_PROVINCE_TextChanged(object sender, EventArgs e)
        {
            FillDDLProvince();
        }

        protected void TXT_COUNTRY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCountry();
        }

        protected void TXT_CITY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCity();
        }

        protected void DDL_PROVINCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLCity();
        }
    }
}
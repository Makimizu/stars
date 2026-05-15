using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization;

namespace GLIFE_PROPOSAL
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
                    Response.Redirect("logout.aspx");
                Setup();
                if (LB_ID.Text != "")
                    LoadMember(LB_ID.Text);
            }
        }

        protected void LoadMember(string ID)
        {
            conn.QueryString = "select * from V_LINK_CB_MEMBER_MASTER a " +
                                "LEFT JOIN GLIFE_PROPOSAL.dbo.APPLICATION_MEMBER_BERKAS b ON a.ID=b.MEMBER_ID  " +
                                "where a.ID = '" + ID + "'";
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
            TXT_TRM_BERKAS.Text= conn.GetFieldValue("TGL_BERKAS").ToString();

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
            }

           

                conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI";
                conn.ExecuteQuery();
                DDL_PROVINCE.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_CITIZENSHIP";
                conn.ExecuteQuery();
                DDL_CITIZENSHIP.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_CITIZENSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY";
                conn.ExecuteQuery();
                DDL_COUNTRY.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_IDTYPE";
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

                conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from V_LINK_CB_PR_BANK";
                conn.ExecuteQuery();
                DDL_BANK.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }
            }
        

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_VERIFY_BERKAS  " +
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
                                "'" + TXT_TRM_BERKAS.Text.Trim() + "'";
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

            Save();
            Response.Redirect("Quotation.aspx?ID=&MEMBERID=" + LB_ID.Text);
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
            string tglberaksdummy = "'" +
                DateTime.ParseExact(TXT_TRM_BERKAS.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture)
                    .ToString("yyyy-MM-dd") + "'";
            string ID = "null";
            if (LB_ID.Text != "")
                ID = "'" + LB_ID.Text + "'";

            try
            {
                conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_UPSERT_BERKAS " +
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
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    tglberaksdummy;
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                ShowError();
            }

            LB_ID.Text = conn.GetFieldValue("ID").ToString();
        }

        protected void BT_CONTINUE_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("Quotation.aspx?ID=&MEMBERID=" + LB_ID.Text);
        }
    }
}
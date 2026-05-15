using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationPersonHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_CATEGORY.Text = "MAIN";
                Setup();
                FillDGR();
                ShowList();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from LIFE.dbo.PR_CLIENT_ADDRESS_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ADDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from LIFE.dbo.PR_CLIENT_BANK_ACCOUNT_TYPE order by (case when CODE = 'TRX' then 'AA' else CODE end)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

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

            FillDDLProvince();
            FillDDLCountry();
            FillDDLCity();
            FillDDLBank();


            LoadAddress();
            LoadAccount();
        }

        protected void LoadAddress()
        {
            string cityCode = "";

            TXT_ADDRESS.Text = "";
            TXT_CITY.Text = "";
            TXT_ZIPCODE.Text = "";
            TXT_PHONE1.Text = "";
            TXT_PHONE2.Text = "";
            TXT_EMAIL.Text = "";
            DDL_PROVINCE.SelectedIndex = 0;
            DDL_COUNTRY.SelectedIndex = 0;

            conn.QueryString = "exec SP_APPLICATION_ADDRESS '" + LB_REGNO.Text + "','" + DDL_ADDTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS.Text = conn.GetFieldValue("ADDRESS").ToString();
            cityCode = conn.GetFieldValue("CITY").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIPCODE").ToString();
            TXT_PHONE1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();

            try
            {
                DDL_COUNTRY.SelectedValue = conn.GetFieldValue("COUNTRY").ToString();
            }
            catch { }

            try
            {
                DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
                FillDDLCity();
            }
            catch { }

            try
            {
                DDL_CITY.SelectedValue = cityCode;
            }
            catch { }
        }

        protected void LoadAccount()
        {
            TXT_ACCNO.Text = "";
            TXT_ACCNAMA.Text = "";
            DDL_BANK.SelectedIndex = 0;
            //DGR_ITEM.Visible = false;

            conn.QueryString = "exec SP_APPLICATION_BANK_ACCOUNT '" + LB_REGNO.Text + "','" + DDL_ACCTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
            TXT_ACCNAMA.Text = conn.GetFieldValue("ACCNAME").ToString();
            try
            {
                DDL_BANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
            }catch { }

            FillDGRItem();

            //    if (DDL_ACCTYPE.SelectedValue == "AUD")
            //    {
            //        FillDGRItem();
            //        DGR_ITEM.Visible = true;
            //    }
        }


        protected void FillDGR()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            conn.QueryString = "exec LQ.dbo.SP_APPLICATION_MEMBER '" + LB_REGNO.Text + "','" + LB_CATEGORY.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER.DataSource = dt;
            DGR_MEMBER.DataBind();

            ////conn.QueryString = "select CODE, DESCR=UPPER(DESCR) from LIFE.dbo.PR_MEMBER_RELATIONSHIP";
            ////conn.ExecuteQuery();

            Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn1.QueryString = "select CODE, DESCR=UPPER(DESCR) from LIFE.dbo.PR_MEMBER_RELATIONSHIP where CODE in ('02','05','08','09','10')";
            conn1.ExecuteQuery();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            //conn1.QueryString = "select CODE, DESCR=UPPER(DESCR) from LIFE.dbo.PR_MEMBER_RELATIONSHIP where CODE in ('02','05','08','09','10')";
            conn2.QueryString = "select CODE, DESCR=UPPER(DESCR) from LIFE.dbo.PR_MEMBER_RELATIONSHIP";
            conn2.ExecuteQuery();

            string warning = "";

            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                Button lbE = (Button)DGR_MEMBER.Items[i].FindControl("BT_EDIT");
                Button lbN = (Button)DGR_MEMBER.Items[i].FindControl("BT_N");
                Button lbC = (Button)DGR_MEMBER.Items[i].FindControl("BT_C");
                Button lbX = (Button)DGR_MEMBER.Items[i].FindControl("BT_X");
                Button lbD = (Button)DGR_MEMBER.Items[i].FindControl("BT_D");
                LinkButton lbt = (LinkButton)DGR_MEMBER.Items[i].FindControl("LBT_FULLNAME");
                DropDownList ddlREL = (DropDownList)DGR_MEMBER.Items[i].FindControl("DDL_RELATION");
                DropDownList ddlGENDER = (DropDownList)DGR_MEMBER.Items[i].FindControl("DDL_GENDER");
                TextBox txtDOB = (TextBox)DGR_MEMBER.Items[i].FindControl("TXT_DOB_DUMMY");
                Label lbGENDER = (Label)DGR_MEMBER.Items[i].FindControl("LB_GENDER");
                Label lbDOB = (Label)DGR_MEMBER.Items[i].FindControl("LB_DOB");
                Label lbAGE = (Label)DGR_MEMBER.Items[i].FindControl("LB_AGE");

                if (DGR_MEMBER.Items[i].Cells[4].Text == "1")
                    lbX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
                else
                    lbX.Visible = false;

                lbAGE.Text = DGR_MEMBER.Items[i].Cells[6].Text.Replace("&nbsp;", "");

                if (DGR_MEMBER.Items[i].Cells[1].Text.Replace("&nbsp;", "") != "")
                {
                    DGR_MEMBER.Items[i].BackColor = System.Drawing.Color.LightYellow;

                    lbGENDER.Visible = true;
                    lbDOB.Visible = true;
                    ddlGENDER.Visible = false;
                    txtDOB.Visible = false;

                    lbt.Text = DGR_MEMBER.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                    lbGENDER.Text = DGR_MEMBER.Items[i].Cells[5].Text;
                    lbDOB.Text = DGR_MEMBER.Items[i].Cells[11].Text;
                }
                else
                {
                    //lbt.Text = DGR_MEMBER.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                    lbt.Enabled = false;
                    lbGENDER.Visible = false;
                    lbDOB.Visible = false;
                    ddlGENDER.Visible = true;
                    txtDOB.Visible = true;

                    ddlGENDER.SelectedValue = DGR_MEMBER.Items[i].Cells[5].Text.Substring(0, 1);
                    txtDOB.Text = DGR_MEMBER.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                }

                //if (DGR_MEMBER.Items[i].Cells[2].Text.Replace("&nbsp;", "") == "")
                //{
                //    lbD.Visible = true;
                //    ddlGENDER.Visible = false;
                //    ddlAGE.Visible = false;
                //    ddlREL.Visible = false;
                //}
                //else
                //{
                //    lbD.Visible = false;
                //}


                if (DGR_MEMBER.Items[i].Cells[3].Text == "1")
                {
                    ddlREL.Visible = true;
                    ddlREL.Items.Clear();
                    ddlREL.Items.Add(new ListItem("", ""));
                    
                    if(DGR_MEMBER.Items[i].Cells[0].Text == "9"){
                        for (int k = 0; k < conn1.GetRowCount(); k++)
                        {
                            ddlREL.Items.Add(new ListItem(conn1.GetFieldValue(k, 1).ToString(), conn1.GetFieldValue(k, 0).ToString()));
                        }
                    }else{
                        for (int j = 0; j < conn2.GetRowCount(); j++)
                        {
                            ddlREL.Items.Add(new ListItem(conn2.GetFieldValue(j, 1).ToString(), conn2.GetFieldValue(j, 0).ToString()));
                        }
                    }

                    try
                    {
                        ddlREL.SelectedValue = DGR_MEMBER.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }

                if (DGR_MEMBER.Items[i].Cells[8].Text.Replace("&nbsp;", "") == "0")
                {
                    lbE.Visible = lbN.Visible = false;
                }

                if (DGR_MEMBER.Items[i].Cells[9].Text.Replace("&nbsp;", "") == "0")
                {
                    lbC.Visible = false;
                }

                if (DGR_MEMBER.Items[i].Cells[10].Text.Replace("&nbsp;", "") == "0")
                {
                    lbD.Visible = false;
                }
                else
                {
                    lbD.Visible = true;
                    ddlGENDER.Visible = false;
                    txtDOB.Visible = false;
                }

                if (!string.IsNullOrEmpty(conn.GetFieldValue(i, "WARNING").ToString()) &&
                    !string.IsNullOrWhiteSpace(conn.GetFieldValue(i, "WARNING").ToString()))
                {
                    if (!string.IsNullOrEmpty(warning))
                    {
                        warning += "<br>" + conn.GetFieldValue(i, "WARNING").ToString();
                    }
                    else
                    {
                        warning = conn.GetFieldValue(i, "WARNING").ToString();
                    }
                }
            }

            if (!string.IsNullOrEmpty(warning))
            {
                LB_WARNING.Text = warning;
                LB_WARNING.CssClass = "alert";
            }
        }

        protected void DGR_MEMBER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_MEMBER_TITLE.Text = e.Item.Cells[12].Text;
                IF_MEMBER.Attributes.Remove("onload");
                IF_MEMBER.Attributes.Add("onload", " resizeIframe(this)");
                IF_MEMBER.Src = "MemberEntry.aspx?ID=" + e.Item.Cells[1].Text + "&MEMBER_TYPE=" + e.Item.Cells[0].Text + "&REGNO=" +LB_REGNO.Text;

                DV_LIST.Visible = false;
                DV_MEMBER.Visible = true;
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_APPLICATION_MEMBER_DELETE '" + LB_REGNO.Text + "','" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }

            if (e.CommandName == "Edit")
            {
                DV_LIST.Visible = false;
                DV_SEARCH.Visible = true;
                LB_MEMBERTYPE.Text = e.Item.Cells[0].Text;
            }

            if (e.CommandName == "New")
            {
                DV_LIST.Visible = false;
                DV_NEW.Visible = true;
                LB_MEMBERTYPE.Text = e.Item.Cells[0].Text;
                TXT_FULLNAME_ENTRY.Text = "";
                TXT_IDNO_ENTRY.Text = "";
                TXT_DOB_ENTRY.Text = "";
                DDL_GENDER_ENTRY.SelectedIndex = 0;
            }

            if (e.CommandName == "Copy")
            {
                conn.QueryString = "exec SP_APPLICATION_MEMBER_COPY " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Dummy")
            {
                conn.QueryString = "exec SP_APPLICATION_MEMBER_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Info")
            {
                if (e.Item.Cells[19].Text.Replace("&nbsp;", "") != "")
                {
                    /*
                    string url = "QuotationPersonOtherInfo.aspx?REGNO=" + LB_REGNO.Text + "&MEMBERID=" + e.Item.Cells[19].Text;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>window.open('" + url + "','_blank','toolbar=no,scrollbars=no, resizable=yes,top=500, left=500, width=550, height=300')</script>");
                    */
                    LB_MEMBER_TITLE.Text = e.Item.Cells[12].Text;
                    IF_MEMBER.Attributes.Remove("onload");
                    IF_MEMBER.Attributes.Add("onload", " resizeIframe(this)");
                    IF_MEMBER.Src = "QuotationPersonOtherInfo.aspx?REGNO=" + LB_REGNO.Text + "&MEMBERID=" + e.Item.Cells[19].Text;

                    DV_LIST.Visible = false;
                    DV_MEMBER.Visible = true;
                }
            }
        }

        protected void BT_SEARCH_EXISTING_Click(object sender, EventArgs e)
        {
            DGR_PERSON_EXISTING.CurrentPageIndex = 0;
            FillDGRExisting();
        }

        protected void FillDGRExisting()
        {
            if (TXT_FULLNAME_EXISTING.Text.Trim() == "" && TXT_DOB_EXISTING.Text.Trim() == "" && TXT_IDNO_EXISTING.Text.Trim() == "")
                return;

            string where = "";

            if (TXT_IDNO_EXISTING.Text.Trim() != "")
                where = where + " and a.ID_NO like '%" + TXT_IDNO_EXISTING.Text.Trim() + "%' ";

            if (TXT_FULLNAME_EXISTING.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME_EXISTING.Text.Trim() + "%' ";

            if (TXT_DOB_EXISTING.Text.Trim() != "")
                where = where + " and a.DOB = '" + GlobalUse.GlobalDateFormat(TXT_DOB_EXISTING.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "ID			= a.ID, " +
                                "FULLNAME	= LTRIM(a.FULLNAME), " +
                                "DOB		= convert(varchar(20), a.DOB, 106), " +
                                "GENDER		= (case when a.SEX = 'M' then 'MALE' when a.SEX = 'F' then 'FEMALE' else '' end), " +
                                "ID_NO		= a.ID_NO " +
                                "from		V_LINK_CB_MEMBER_MASTER a " +
                                "where " +
                                "a.SEX = '" + DDL_GENDER_EXISTING.SelectedValue + "' " + where + " " +
                                "order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERSON_EXISTING.DataSource = dt;
            DGR_PERSON_EXISTING.DataBind();

            for (int i = 0; i < DGR_PERSON_EXISTING.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_PERSON_EXISTING.Items[i].FindControl("LBT_SELECT_EXISTING");
                lbt.Text = DGR_PERSON_EXISTING.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_PERSON_EXISTING_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_PERSON_EXISTING.CurrentPageIndex = e.NewPageIndex;
            FillDGRExisting();
        }

        protected void DGR_PERSON_EXISTING_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_APPLICATION_MEMBER_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_MEMBERTYPE.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                DV_LIST.Visible = true;
                DV_SEARCH.Visible = false;
                FillDGR();
            }
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            DV_LIST.Visible = true;
            DV_SEARCH.Visible = false;
        }

        protected void LB_NEW_BACK_Click(object sender, EventArgs e)
        {
            DV_LIST.Visible = true;
            DV_NEW.Visible = false;
        }

        protected void BT_NEW_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_IDNO_ENTRY.Text.Trim() == "")
            {
                conn.QueryString = "select CODE from V_LINK_LF_PR_MEMBER_TYPE where CODE = '" + LB_MEMBERTYPE.Text + "' and ID_REQ = 1";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                    return;
            }


            try
            {
                conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_UPSERT " +
                                        "null," +
                                        "'" + TXT_FULLNAME_ENTRY.Text.Trim() + "'," +
                                        "'" + DDL_GENDER_ENTRY.SelectedValue + "'," +
                                        "null," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_DOB_ENTRY.Text.Trim(), "d/M/yyyy") + "'," +
                                        "''," +
                                        "'000'," +
                                        "null," +
                                        "'001'," +
                                        "'000'," +
                                        "'" + TXT_IDNO_ENTRY.Text.Trim() + "'," +
                                        "''," +
                                        "'1NA'," +
                                        "''," +
                                        "''," +
                                        "''," +
                                        "''," +
                                        "''," +
                                        "''," +
                                        "'00'," +
                                        "'1NA'," +
                                        "''," +
                                        "null," +
                                        "null," +
                                        "null," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                string memberid = conn.GetFieldValue("ID").ToString();

                conn.QueryString = "exec SP_APPLICATION_MEMBER_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_MEMBERTYPE.Text + "'," +
                                    "'" + memberid + "'," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                DV_LIST.Visible = true;
                DV_NEW.Visible = false;
                FillDGR();
            }
            catch { }
        }

        protected void DDL_RELATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                DropDownList ddlREL = (DropDownList)DGR_MEMBER.Items[i].FindControl("DDL_RELATION");
                if (ddlREL == (DropDownList)sender)
                {
                    SaveItem(DGR_MEMBER.Items[i]);
                    return;
                }
            }
        }

        protected void DDL_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                DropDownList ddlGENDER = (DropDownList)DGR_MEMBER.Items[i].FindControl("DDL_GENDER");
                if (ddlGENDER == (DropDownList)sender)
                {
                    SaveItem(DGR_MEMBER.Items[i]);
                    return;
                }
            }
        }

        protected void TXT_DOB_DUMMY_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                TextBox txtDOB = (TextBox)DGR_MEMBER.Items[i].FindControl("TXT_DOB_DUMMY");
                if (txtDOB == (TextBox)sender)
                {
                    SaveItem(DGR_MEMBER.Items[i]);
                    return;
                }
            }
        }

        protected void SaveItem(DataGridItem e)
        {
            DropDownList ddlREL = (DropDownList)e.FindControl("DDL_RELATION");
            DropDownList ddlGENDER = (DropDownList)e.FindControl("DDL_GENDER");
            TextBox txtDOB = (TextBox)e.FindControl("TXT_DOB_DUMMY");

            string relation = "null";
            if (ddlREL.SelectedValue != "")
            {
                relation = "'" + ddlREL.SelectedValue + "'";
            }

            if (e.Cells[1].Text.Trim().Replace("&nbsp;", "") == "")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_MEMBER_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + e.Cells[0].Text + "'," +
                                        "null," +
                                        relation + "," +
                                        "'" + ddlGENDER.SelectedValue + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(txtDOB.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            else
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_MEMBER_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + e.Cells[0].Text + "'," +
                                        "'" + e.Cells[1].Text.Trim().Replace("&nbsp;", "") + "'," +
                                        relation + "," +
                                        "null," +
                                        "null," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGR();
        }

        protected void FillDDLProvince()
        {
            DDL_PROVINCE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI where DESCR like '%" + TXT_PROVINCE.Text.Trim() + "%'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLCountry()
        {
            DDL_COUNTRY.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY where DESCR like '%" + TXT_COUNTRY.Text.Trim() + "%'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLBank()
        {
            DDL_BANK.Items.Clear();
            //conn.QueryString = "select CODE,DESCR = CODE + ' - ' + DESCR from V_LINK_CB_PR_BANK where DESCR like '%" + TXT_BANK.Text.Trim() + "%'";
            conn.QueryString = "select CODE = KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' and BANK like '%" + TXT_BANK.Text.Trim() + "%' order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void TXT_PROVINCE_TextChanged(object sender, EventArgs e)
        {
            FillDDLProvince();
        }

        protected void TXT_COUNTRY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCountry();
        }

        protected void TXT_BANK_TextChanged(object sender, EventArgs e)
        {
            FillDDLBank();
        }

        protected void BT_SAVE_ADDRESS_Click(object sender, EventArgs e)
        {
            try
            {
                LB_ERR_ADDRESS.Text = "";
                if (IsValidEmail(TXT_EMAIL.Text.Trim()))
                {
                    conn.QueryString = "exec SP_APPLICATION_ADDRESS_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DDL_ADDTYPE.SelectedValue + "'," +
                                        "'" + TXT_ADDRESS.Text.Trim() + "'," +
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

                    LoadAddress();
                }
            }
            catch { }
        }

        protected bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                LB_ERR_ADDRESS.Text = "EMAIL IS NOT VALID";
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                LB_ERR_ADDRESS.Text = "EMAIL IS NOT VALID";
                return false;
            }
        }

        protected void BT_SAVE_ACCOUNT_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_BANK_ACCOUNT_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + DDL_ACCTYPE.SelectedValue + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                    "'" + DDL_BANK.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                SaveOtherItems();

                LoadAccount();
            }
            catch { }
        }

        protected void DDL_ADDTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAddress();
        }

        protected void DDL_ACCTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccount();
        }

        protected void ShowList()
        {
            LB_TITLE.Text = BT_LIST.Text;
            TBL_LIST.Visible = true;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = false;
        }

        protected void ShowBeneficiaryList()
        {
            LB_TITLE.Text = BT_BEN.Text;
            TBL_LIST.Visible = true;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = false;
        }

        protected void BT_LIST_Click(object sender, EventArgs e)
        {
            LB_CATEGORY.Text = "MAIN";
            FillDGR();
            ShowList();
        }

        protected void BT_BEN_Click(object sender, EventArgs e)
        {
            LB_CATEGORY.Text = "BEN";
            FillDGR();
            ShowBeneficiaryList();
        }

        protected void BT_ADDRESS_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_LIST.Visible = false;
            TBL_BANK.Visible = false;
            
            LoadAddress();
            TBL_ADDRESS.Visible = true;
        }

        protected void BT_BANK_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_LIST.Visible = false;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = true;
        }


        protected void LB_MEMBER_BACK_Click(object sender, EventArgs e)
        {
            DV_LIST.Visible = true;
            DV_MEMBER.Visible = false;
        }

        protected void DDL_PROVINCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLCity();
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

        protected void TXT_CITY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCity();
        }

        protected void FillDGRItem()
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_INFO_ADDITIONAL " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_ACCTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");
                TextBox txtVALDATE = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VALDATE");

                if (DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_ITEM.Items[j].Cells[1].Text)
                    {
                        case "STR": txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Attributes.Add("text-align", "right");
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Attributes.Add("text-align", "right");
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;
                        case "DATE": txtVALDATE.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Visible = false;
                            ddl.Visible = false;
                            txtVALDATE.Visible = true;
                            break;

                    }
                }
            }
        }

        protected void DDL_REFF_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveOtherItems();
            FillDGRItem();
        }

        protected void SaveOtherItems()
        {
            for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");
                TextBox txtValDate = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VALDATE");

                string val = txt.Text.Trim();
                if (ddl.Visible)
                    val = ddl.SelectedValue;

                if (txtValDate.Visible && txtValDate.Text.Trim() != "")
                    val = GlobalUse.GlobalDateFormat(txtValDate.Text.Trim(), "d/M/yyyy");

                conn.QueryString = "exec SP_APPLICATION_OTHER_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRItem();
        }
    }
}
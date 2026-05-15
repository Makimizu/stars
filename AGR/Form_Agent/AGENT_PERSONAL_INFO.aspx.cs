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
    public partial class AGENT_PERSONAL_INFO : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["code"];
                Setup();
                LoadRecord();
            }

        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_AGENT_FAMILY_STATUS where CODE not in (11,12,13,4,5,6,7) order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FAMILY_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            conn.QueryString = "select CODE, DESCR from PR_SCHOOL_GRADE order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FAMILY_EDUCATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from CLIENT_BASE.dbo.PR_GENDER order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FAMILY_GENDER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_AGENT_LICENCE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LICENSE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_AGENT_LICENCE_STATUS";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LICENSE_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select THEYEAR = YEAR(GETDATE()) - SEQ + 1 from SC_SEQ where SEQ <= 10";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT CODE,DESCR FROM PR_AGENT_NPWP order by 1 asc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MARITAL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT '1' as code, 'YES' as descr union all SELECT '0' as code, 'NO' as descr order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ANOTHERINCOME.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            try
            {
                DDL_YEAR.SelectedValue = DateTime.Now.Year.ToString();
            }
            catch { }
        }

        protected void LoadRecord()
        {
            FillDGR_ITEM();
            Show_DV_BANK();
        }

        protected void FillDGR_BankACC()
        {
            conn.QueryString = "exec SP_M_AGENT_ACCOUNT '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

        }

        protected void FillDGRSocialMedia()
        {
            conn.QueryString = "exec SP_M_AGENT_SOCIALMEDIA '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SOCIALMEDIA.DataSource = dt;
            DGR_SOCIALMEDIA.DataBind();

            for (int i = 0; i < DGR_SOCIALMEDIA.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_SOCIALMEDIA.Items[i].FindControl("TXT_SOCMED");
                txt.Text = DGR_SOCIALMEDIA.Items[i].Cells[1].Text.Replace("&nbsp;", "");
            }
        }

        protected void FillDGRMaritalStatus()
        {
            conn.QueryString = "exec SP_M_AGENT_MARITAL_STATUS '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MARITAL.DataSource = dt;
            DGR_MARITAL.DataBind();

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TBL_BANK.Visible)
            {
                LB_ERROR.Text = "";

                string vacc = "0";
                if (CB_VACC.Checked)
                    vacc = "1";

                try
                {
                    conn.QueryString = "exec SP_M_AGENT_ACCOUNT_INSERT " +
                                            "@CODE = '" + LB_ID.Text + "'," +
                                            "@ACCNO = '" + TXT_ACCOUNT.Text.Trim() + "'," +
                                            "@ACCBANK ='" + DDL_BANK.SelectedValue + "'," +
                                            "@ACCNAME = '" + TXT_NAME.Text.Trim() + "'," +
                                            "@VIRTUAL_ACC = " + vacc + "," +
                                            "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR_BankACC();
                    LoadRecord();

                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TBL_BANK.Visible = true;
                BT_BANK_CANCEL.Visible = true;
                DGR.Visible = false;
            }
        }


        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_PARAM_OTHER_SETTING " +
                                "'" + LB_ID.Text + "'," +
                                "'AGN'";
            conn.ExecuteQuery();

            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");
                Button btCopy = (Button)DGR_ITEM.Items[j].FindControl("BT_COPY");
                Label lbVAL = (Label)DGR_ITEM.Items[j].FindControl("LB_VAL");

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
                        case "STR":
                            txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            if (DGR_ITEM.Items[j].Cells[4].Text != "0")
                            {
                                txtVAL.MaxLength = int.Parse(DGR_ITEM.Items[j].Cells[4].Text);
                            }
                            break;
                        case "INT":
                            txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Attributes.Add("text-align", "right");
                            break;
                        case "FLO":
                            try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Attributes.Add("text-align", "right");
                            }
                            catch { }
                            break;
                        case "BIT":
                            txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }

                //if(DGR_ITEM.Items[j].Cells[0].Text.Replace("&nbsp;", "") == "AGN07A")
                //{
                //    btCopy.Visible = true;
                //    btCopy.Text = "COPY FROM ID";
                //}

                if (DGR_ITEM.Items[j].Cells[0].Text.Replace("&nbsp;", "") == "AGN06" || DGR_ITEM.Items[j].Cells[0].Text.Replace("&nbsp;", "") == "AGN07C")
                {
                    conn.QueryString = "select PROVINCE_CODE,PROVINCE_NAME " +
                                        "from V_LINK_CB_PARAM_CITY a " +
                                        "where " +
                                        "a.CITY_CODE = '" + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "'";
                    conn.ExecuteQuery();
                    lbVAL.Visible = true;
                    lbVAL.Text = conn.GetFieldValue("PROVINCE_NAME").ToString();
                }
            }
        }

        protected void DGR_ITEM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                SaveOtherInfo();
                FillDGR_ITEM();
            }

            if (e.CommandName == "Copy")
            {
                SaveOtherInfo();
                conn.QueryString = "update a set " +
                                    "VAL = b.VAL " +
                                    "from M_AGENT_OTHER_INFO a " +
                                    "left join M_AGENT_OTHER_INFO b on a.AGENT_CODE = b.AGENT_CODE and b.FIELD_CODE = (case	when a.FIELD_CODE = 'AGN07A' then 'AGN04' " +
                                    "                                                                                                    when a.FIELD_CODE = 'AGN07B' then 'AGN05' " +
                                    "                                                                                                    when a.FIELD_CODE = 'AGN07C' then 'AGN06' " +
                                    "                                                                                                    when a.FIELD_CODE = 'AGN07D' then 'AGN07' " +
                                    "                                                                                                    else '' " +
                                    "                                                                                                    end) " +
                                    "where " +
                                    "a.AGENT_CODE = '" + LB_ID.Text + "' " +
                                    "and a.FIELD_CODE in ('AGN07A', 'AGN07B', 'AGN07C','AGN07D')";
                conn.ExecuteNonQuery();
                FillDGR_ITEM();
            }
        }

        protected void SaveOtherInfo()
        {
            for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");

                string val = txt.Text.Trim();
                if (ddl.Visible)
                    val = ddl.SelectedValue;

                conn.QueryString = "exec SP_PARAM_OTHER_SETTING_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();


            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENTS_ACCOUNT where " +
                                    "CODE = '" + LB_ID.Text + "' " +
                                    "and ACCNO = '" + e.Item.Cells[1].Text + "' " +
                                    "and ACCBANK = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR_BankACC();
            }
        }

        protected void DGR_ITEM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Show_DV_BANK()
        {
            LB_TITLE.Text = BT_BANK.Text;

            DV_BANK.Visible = true;
            DV_LICENCE.Visible = false;
            DV_FAMILY.Visible = false;
            DV_NONFAMILY.Visible = false;
            DV_SOCIALMEDIA.Visible = false;
            TBL_BANK.Visible = false;
            DGR.Visible = true;
            DV_MARITAL.Visible = false;

            FillDGR_BankACC();
        }

        protected void Show_DV_LICENCE()
        {
            LB_TITLE.Text = BT_LICENCE.Text;

            DV_BANK.Visible = false;
            DV_LICENCE.Visible = true;
            DV_FAMILY.Visible = false;
            DV_NONFAMILY.Visible = false;
            DV_SOCIALMEDIA.Visible = false;
            TBL_LICENCE.Visible = false;
            DGR_LICENCE.Visible = true;
            DV_MARITAL.Visible = false;

            FillDGRLicence();
        }

        protected void Show_DV_FAMILY()
        {
            LB_TITLE.Text = BT_FAMILY.Text;

            DV_BANK.Visible = false;
            DV_LICENCE.Visible = false;
            DV_FAMILY.Visible = true;
            DV_NONFAMILY.Visible = false;
            DV_SOCIALMEDIA.Visible = false;
            TBL_FAMILY.Visible = false;
            DGR_FAMILY.Visible = true;
            DV_MARITAL.Visible = false;

            FillDGRFamily();
        }

        protected void Show_DV_NONFAMILY()
        {
            LB_TITLE.Text = BT_NONFAMILY.Text;

            DV_BANK.Visible = false;
            DV_LICENCE.Visible = false;
            DV_FAMILY.Visible = false;
            DV_NONFAMILY.Visible = true;
            DV_SOCIALMEDIA.Visible = false;
            TBL_NONFAMILY.Visible = false;
            DGR_NONFAMILY.Visible = true;
            DV_MARITAL.Visible = false;

            FillDGRNonFamily();
        }

        protected void Show_DV_SOCIALMEDIA()
        {
            LB_TITLE.Text = BT_SOCMED.Text;

            DV_BANK.Visible = false;
            DV_LICENCE.Visible = false;
            DV_FAMILY.Visible = false;
            DV_NONFAMILY.Visible = false;
            DV_SOCIALMEDIA.Visible = true;
            DV_MARITAL.Visible = false;

            FillDGRSocialMedia();
        }

        protected void Show_DV_MARITAL()
        {
            LB_TITLE.Text = BT_MARITAL.Text;

            DV_BANK.Visible = false;
            DV_LICENCE.Visible = false;
            DV_FAMILY.Visible = false;
            DV_NONFAMILY.Visible = false;
            DV_SOCIALMEDIA.Visible = false;
            DV_MARITAL.Visible = true;

            FillDGRMaritalStatus();
        }


        protected void BT_BANK_Click(object sender, EventArgs e)
        {
            Show_DV_BANK();
        }

        protected void BT_FAMILY_Click(object sender, EventArgs e)
        {
            Show_DV_FAMILY();
        }

        protected void BT_NONFAMILY_Click(object sender, EventArgs e)
        {
            Show_DV_NONFAMILY();
        }

        protected void BT_LICENCE_Click(object sender, EventArgs e)
        {
            Show_DV_LICENCE();
        }

        protected void BT_SOCMED_Click(object sender, EventArgs e)
        {
            Show_DV_SOCIALMEDIA();
        }

        protected void BT_MARITAL_Click(object sender, EventArgs e)
        {
            Show_DV_MARITAL();
        }

        protected void FillDGRLicence()
        {
            conn.QueryString = "exec SP_M_AGENTS_LICENCE '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LICENCE.DataSource = dt;
            DGR_LICENCE.DataBind();

            for (int i = 0; i < DGR_LICENCE.Items.Count; i++)
            {
                Button btDEL = (Button)DGR_LICENCE.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
                //if (i > 0)
                //{
                //    btDEL.Visible = false;
                //}
            }
        }

        protected void FillDGRFamily()
        {
            conn.QueryString = "exec SP_M_AGENT_FAMILY '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FAMILY.DataSource = dt;
            DGR_FAMILY.DataBind();
        }

        protected void FillDGRNonFamily()
        {
            conn.QueryString = "exec SP_M_AGENT_NON_FAMILY '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_NONFAMILY.DataSource = dt;
            DGR_NONFAMILY.DataBind();
        }

        protected void BT_SAVEFAMILY_Click(object sender, EventArgs e)
        {
            if (TBL_FAMILY.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_FAMILY_UPSERT " +
                                       "@CODE = '" + LB_ID.Text + "', " +
                                       "@SEQ = null," +
                                       "@FAMILY_STATUS =  '" + DDL_FAMILY_STATUS.SelectedValue + "'," +
                                       "@NAME =  '" + TXT_FAMILY_NAME.Text + "'," +
                                       "@POB = '" + GlobalUse.GlobalDateFormat(TXT_FAMILY_POB.Text, "d/M/yyyy") + "'," +
                                       "@DOB = '" + GlobalUse.GlobalDateFormat(TXT_FAMILY_DOB.Text, "d/M/yyyy") + "'," +
                                       "@GENDER = '" + DDL_FAMILY_GENDER.SelectedValue + "', " +
                                       "@ADDRESS = '" + TXT_FAMILY_ADDRESS.Text + "'," +
                                       "@EDUCATION = '" + DDL_FAMILY_EDUCATION.SelectedValue + "', " +
                                       "@PROFESSION  = '" + TXT_FAMILY_PROFESSION.Text.Trim() + "'," +
                                       "@PHONE = '" + TXT_FAMILY_PHONE.Text.Trim() + "'," +
                                       "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();
                    FillDGRFamily();
                    LoadRecord();
                    Show_DV_FAMILY();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TBL_FAMILY.Visible = true;
                BT_FAMILY_CANCEL.Visible = true;
                DGR_FAMILY.Visible = false;
            }
        }

        protected void BT_NONFAMILY_SAVE_Click(object sender, EventArgs e)
        {
            if (TBL_NONFAMILY.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_NON_FAMILY_UPSERT " +
                                        "@CODE = '" + LB_ID.Text + "', " +
                                        "@SEQ = null," +
                                        "@NAME =  '" + TXT_NONFAMILY_NAME.Text + "'," +
                                        "@ADDRESS = '" + TXT_NONFAMILY_ADDRESS.Text + "'," +
                                        "@PHONE = '" + TXT_NONFAMILY_PHONE.Text.Trim() + "'," +
                                        "@RELATION = '" + TXT_NONFAMILY_RELATION.Text.Trim() + "'," +
                                        "@PERIOD_RELATIONSHIP = '" + TXT_NONFAMILY_PERIOD_RELATIONSHIP.Text.Trim() + "'," +
                                        "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();
                    FillDGRNonFamily();
                    LoadRecord();
                    Show_DV_NONFAMILY();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TBL_NONFAMILY.Visible = true;
                BT_NONFAMILY_CANCEL.Visible = true;
                DGR_NONFAMILY.Visible = false;
            }
        }

        protected void BT_SAVE_LICENCE_click(object sender, EventArgs e)
        {
            if (TBL_LICENCE.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_AGENT_LICENCE_UPSERT " +
                                        "@CODE = '" + LB_ID.Text + "', " +
                                        "@LICENCE_NO = '" + TXT_LICENCE_NUMBER.Text + "'," +
                                        "@STARTDATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE_LICENCE.Text, "d/M/yyyy") + "'," +
                                        "@ENDDATE = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE_LICENCE.Text, "d/M/yyyy") + "'," +
                                        "@ISSUED_BY = '" + DDL_LICENSE.SelectedValue + "', " +
                                        "@STATUS_LICENCE = '" + DDL_LICENSE_STATUS.SelectedValue + "', " +
                                        "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();

                    FillDGRLicence();
                    LoadRecord();
                    Show_DV_LICENCE();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TBL_LICENCE.Visible = true;
                BT_LICENCE_CANCEL.Visible = true;
                DGR_LICENCE.Visible = false;
            }
        }

        protected void BT_MARITAL_SAVE_Click(object sender, EventArgs e)
        {
            if (TBL_MARITAL.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_AGENT_MARITAL_STATUS_UPSERT " +
                                        "@AGENT_CODE = '" + LB_ID.Text + "', " +
                                        "@YEAR = " + DDL_YEAR.SelectedValue + "," +
                                        "@MARITAL_STATUS = '" + DDL_MARITAL.SelectedValue + "'," +
                                        "@ANOTHER_INCOME = '" + DDL_ANOTHERINCOME.SelectedValue + "'," +
                                        "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();

                    FillDGRMaritalStatus();
                    LoadRecord();
                    Show_DV_MARITAL();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TBL_MARITAL.Visible = true;
                BT_MARITAL_CANCEL.Visible = true;
                DGR_MARITAL.Visible = false;
            }
        }

        protected void DGR_FAMILY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_FAMILY where CODE = '" + LB_ID.Text + "' and SEQ = " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();
                FillDGRFamily();
            }
        }

        protected void DGR_NONFAMILY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_NON_FAMILY where CODE = '" + LB_ID.Text + "' and SEQ = " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();
                FillDGRNonFamily();
            }

        }

        protected void DGR_MARITAL_ITEMCOMMAND(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENTS_MARITAL_PERIOD where AGENT_CODE = '" + LB_ID.Text + "' and [YEAR] = " + e.Item.Cells[0].Text + "";
                conn.ExecuteNonQuery();
                FillDGRMaritalStatus();
            }
        }

        protected void BT_BANK_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_BANK.Visible = false;
            BT_BANK_CANCEL.Visible = false;
            DGR.Visible = true;
        }

        protected void BT_LICENCE_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_LICENCE.Visible = false;
            BT_LICENCE_CANCEL.Visible = false;
            DGR_LICENCE.Visible = true;
        }

        protected void BT_FAMILY_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_FAMILY.Visible = false;
            BT_FAMILY_CANCEL.Visible = false;
            DGR_FAMILY.Visible = true;
        }

        protected void BT_NONFAMILY_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_NONFAMILY.Visible = false;
            BT_NONFAMILY_CANCEL.Visible = false;
            DGR_NONFAMILY.Visible = true;
        }

        protected void BT_MARITAL_CANCEL_Click(object sender, EventArgs e)
        {
            TBL_MARITAL.Visible = false;
            BT_MARITAL_CANCEL.Visible = false;
            DGR_MARITAL.Visible = true;
        }

        protected void BT_SAVE_SOCMED_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_SOCIALMEDIA.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_SOCIALMEDIA.Items[i].FindControl("TXT_SOCMED");
                conn.QueryString = "exec SP_M_AGENT_SOCIALMEDIA_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DGR_SOCIALMEDIA.Items[i].Cells[0].Text + "'," +
                                    "'" + txt.Text.Trim().Replace("'", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRSocialMedia();
        }

        protected void DGR_LICENCE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                //conn.QueryString = "delete from M_AGENT_LICENCE where CODE = '" + LB_ID.Text + "' and LICENCE_NO = " + e.Item.Cells[0].Text;
                conn.QueryString = "exec SP_AGENT_LICENCE_DELETE " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRLicence();
            }
        }

        protected void BT_COPY_ADDRESS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_M_AGENT_OTHER_INFO_COPY " +
                                "'" + LB_ID.Text + "'," +
                                "'AGN04'," +
                                "'AGN07A'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR_ITEM();
        }

        protected void BT_COPY_IDNO_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_M_AGENT_OTHER_INFO_COPY " +
                                "'" + LB_ID.Text + "'," +
                                "'AGN00'," +
                                "'AGN09'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR_ITEM();
        }
    }
}
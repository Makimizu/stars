using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Drawing;

namespace HLP.Form_Quot
{
    public partial class QuotationHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text =  Request.QueryString["code"];
                Setup();
                ShowTC();
            }
        }

        protected bool VerifySalesAccess()
        {
            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            conn.QueryString = "select ROLE_CODE from PARAM_SALES_ROLE where ROLE_CODE = '" + role + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return false;

            return true;
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                LB_READONLY.Text = "&readonly=1";
                BT_SAVE.Visible = false;
                BT_NEW.Visible = false;
                BT_COPY.Visible = false;
                BT_DEL.Visible = false;
                DDL_TPA.Enabled = false;
            }
            else
            {
                LB_READONLY.Text = "&readonly=";
            }

            if (VerifySalesAccess())
            {
                TD_06.Visible = false;
                TD_07.Visible = false;
            }

            DDL_COMPANY.Items.Clear();
            DDL_PRODUCT.Items.Clear();
            DDL_VER.Items.Clear();

            conn.QueryString = "select COMPANY_CODE,COMPANY_NAME = LEFT(COMPANY_NAME,40) from COMPANY where STAT='002' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PARAM_PRODUCT order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select TPA_CODE, DESCR from PARAM_TPA order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select VERNO from QUOTATION_VERSION where QUOTNO = '" +LB_QUOTNO.Text+ "' order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_VER.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            LoadRecord();

            BT_DEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");                
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "QUOTNO, " +
                                "COMPANY_CODE, " +
                                "PRODUCT_CODE, " +
                                "TPA_CODE, " +
                                "AGENT = isnull(AGENT_CODE,'') + ' - ' + isnull(AGENT_NAME,''), " +
                                "SUBCD_DESCR, " +
                                "CREATEBY, " +
                                "CREATEDATE " +
                                "from V_QUOTATION_VERSION " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + DDL_VER.SelectedValue;
            conn.ExecuteQuery();

            LB_AGENT.Text = conn.GetFieldValue("AGENT").ToString();
            LB_CHANNEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();
            LB_CREATEBY.Text = conn.GetFieldValue("CREATEBY").ToString();
            LB_CREATEDATE.Text = conn.GetFieldValue("CREATEDATE").ToString();

            try
            {
                DDL_COMPANY.SelectedValue = conn.GetFieldValue("COMPANY_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_PRODUCT.SelectedValue = conn.GetFieldValue("PRODUCT_CODE").ToString();
            }
            catch { }

            //try
            //{
                DDL_TPA.SelectedValue = conn.GetFieldValue("TPA_CODE").ToString();
            //}
            //catch { }

            LoadDGR("QVER", "QVER00", LB_QUOTNO.Text + "-" + DDL_VER.SelectedValue);
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void ShowTC()
        {
            LBL_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationTC.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationPackage.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationASO.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select count(QUOTNO) from QUOTATION_VERSION_PACKAGE_PLAN_DETAIL where " +
                                "QUOTNO = '" +LB_QUOTNO.Text + "' " +
                                "and VERNO = " +DDL_VER.SelectedValue;
            conn.ExecuteQuery();

            if (conn.GetFieldValue(0, 0).ToString() == "0")
                return;

            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationBenefit.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            /*
            conn.QueryString = "select count(QUOTNO) from QUOTATION_VERSION_PACKAGE_PLAN_MEMBER where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + DDL_VER.SelectedValue;
            conn.ExecuteQuery();

            if (conn.GetFieldValue(0, 0).ToString() == "0")
                return;
            */

            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationMember.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select count(QUOTNO) from QUOTATION_VERSION_PACKAGE_PLAN_MEMBER where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + DDL_VER.SelectedValue;
            conn.ExecuteQuery();

            if (conn.GetFieldValue(0, 0).ToString() == "0")
                return;

            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationPrint.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationClosing.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + "&prod=" + DDL_PRODUCT.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void BT8_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            string param = Crypto.EncryptStringAES("?code=" + DDL_COMPANY.SelectedValue);            
            //Response.Write("<script language='javascript'>parent.quotbody.location.href = '../Form_Tools/JumpAppMenu.aspx?appid=CS&menu=104&param=" + param + "';</script>");
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = '../Form_Company/CompanyBranch.aspx?code=" + DDL_COMPANY.SelectedValue + LB_READONLY.Text + "';</script>");
        }

        protected void DDL_VER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
            ShowTC();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_INSERT " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationHeader.aspx?code=" + LB_QUOTNO.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void BT_COPY_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_COPY " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    DDL_VER.SelectedValue + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationHeader.aspx?code=" + LB_QUOTNO.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        private void LoadDGR(string tipe, string group, string owner)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "exec SP_PARAM_ADDITIONAL_INFO " +
                                    "'" + tipe + "'," +
                                    "'" + group + "'," +
                                    "'" + owner + "'";
                conn.ExecuteQuery();
                DGR.DataSource = conn.GetDataTable();
                DGR.DataBind();
                string GroupID = "";
                Color ItemColor = Color.Empty;
                Color Item1Color = Color.LightYellow;
                Color Item2Color = Color.LightSeaGreen;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    /*
                    if (GroupID == DGR.Items[i].Cells[1].Text)
                    {
                        DGR.Items[i].BackColor = ItemColor;
                        DGR.Items[i].Cells[6].Text = "";
                    }
                    else
                    {
                        if (ItemColor.IsEmpty)
                            ItemColor = Item1Color;
                        else
                        {
                            if (ItemColor == Item1Color)
                                ItemColor = Item2Color;
                            else
                                ItemColor = Item1Color;
                        }

                        DGR.Items[i].BackColor = ItemColor;
                        GroupID = DGR.Items[i].Cells[1].Text;
                    }
                    */
                    TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txtdate.Enabled = false;
                        ddlval.Enabled = false;
                        txtval.Enabled = false;
                    }

                    switch (DGR.Items[i].Cells[2].Text)
                    {
                        case "001": txtval.Visible = true;
                            txtval.Width = 200;
                            txtval.MaxLength = int.Parse(DGR.Items[i].Cells[3].Text);
                            break;
                        case "002": txtval.Visible = true;
                            txtval.Width = 40;
                            break;
                        case "003": txtval.Visible = true;
                            txtval.Width = 100;
                            txtval.MaxLength = int.Parse(DGR.Items[i].Cells[3].Text);
                            txtval.CssClass = "ASPTextBoxNumber";
                            break;
                        case "004": txtdate.Visible = true;
                            break;
                        case "005": ddlval.Visible = true;
                            ddlval.Items.Add(new ListItem("YA", "1"));
                            ddlval.Items.Add(new ListItem("TIDAK", "0"));
                            break;
                        case "006": txtval.Visible = true;
                            txtval.Width = 200;
                            break;
                    }

                    if (DGR.Items[i].Cells[4].Text.Replace("&nbsp;","") != "")
                    {
                        txtval.Visible = false;
                        txtdate.Visible = false;
                        ddlval.Visible = true;

                        LB_ERR.Text = "";
                        try
                        {
                            conn.QueryString = DGR.Items[i].Cells[4].Text;
                            conn.ExecuteQuery();
                            for (int j = 0; j < conn.GetRowCount(); j++)
                                ddlval.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERR.Text = ex.Message;
                        }
                    }

                    if (DGR.Items[i].Cells[5].Text != "&nbsp;")
                    {
                        if (txtval.Visible)
                        {
                            txtval.Text = DGR.Items[i].Cells[5].Text;
                        }
                        if (ddlval.Visible)
                        {
                            ddlval.SelectedValue = DGR.Items[i].Cells[5].Text;
                        }
                        if (txtdate.Visible)
                        {
                            //txtdate.Text = GlobalUse.GlobalDateFormat(DGR.Items[i].Cells[5].Text, "d/M/yyyy");
                            txtdate.Text = DGR.Items[i].Cells[5].Text;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            SaveDGR(LB_QUOTNO.Text + "-" +DDL_VER.SelectedValue);
        }

        protected void SaveDGR(string owner)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "delete from ADDITIONAL_INFO_DATA where OWNER='" + owner + "' and CODE in (select CODE from PARAM_ADDITIONAL_INFO where GROUP_ID = 'QVER00')";
                conn.ExecuteNonQuery();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                    string val = "";
                    if (txtval.Visible)
                    {
                        if (txtval.Text.Trim() != "")
                            val = txtval.Text.Trim();
                    }
                    if (ddlval.Visible)
                    {
                        val = ddlval.SelectedValue;
                    }
                    if (txtdate.Visible)
                    {
                        if (txtdate.Text.Trim() != "")
                        {
                            val = GlobalUse.GlobalDateFormat(txtdate.Text.Trim(), "d/M/yyyy");
                        }
                    }

                    if (val.Trim() != "")
                    {
                        try
                        {
                            conn.QueryString = "insert into ADDITIONAL_INFO_DATA " +
                                                "select newid(), " +
                                                "'" + owner + "'," +
                                                "'" + DGR.Items[i].Cells[0].Text + "'," +
                                                "'" + val + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                            conn.ExecuteQuery();
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERR.Text = ex.Message;
                        }

                    }
                }

            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void BT_DEL_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_DELETE " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    DDL_VER.SelectedValue;
                conn.ExecuteNonQuery();

                Setup();
                ShowTC();               
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void BT9_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_2", LB_QUOTNO.Text + "-" + DDL_VER.SelectedValue, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = '" + URL + "';</script>");
        }

        protected void BT10_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;

            //string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_2", LB_QUOTNO.Text + "-" + DDL_VER.SelectedValue, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = '" + URL + "';</script>");

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.quotbody.location.href = 'QuotationClinicalPathway.aspx?code=" + LB_QUOTNO.Text + "&ver=" + DDL_VER.SelectedValue + "';</script>");
        }

        protected void BT_VERIFY_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "EXEC SP_VERIFY_QUOTATION '" + LB_QUOTNO.Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }


        }
    }
}

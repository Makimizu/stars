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
    public partial class QuotationCreate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {

            DDL_COMPANY.Items.Clear();
            DDL_PRODUCT.Items.Clear();

            conn.QueryString = "exec SP_LINK_CB_COMPANY " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
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

            LoadDGR("QUOT", "");
        }

        private void LoadDGR(string tipe, string owner)
        {
            try
            {
                conn.QueryString = "exec SP_PARAM_ADDITIONAL_INFO " +
                                    "'" + tipe + "'," +
                                    "'" + owner + "'";
                conn.ExecuteQuery();
                DGR.DataSource = conn.GetDataTable();
                DGR.DataBind();
                
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    
                    TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

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

                    if (DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                    {
                        txtval.Visible = false;
                        txtdate.Visible = false;
                        ddlval.Visible = true;

                        try
                        {
                            conn.QueryString = DGR.Items[i].Cells[4].Text;
                            conn.ExecuteQuery();
                            for (int j = 0; j < conn.GetRowCount(); j++)
                                ddlval.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                        }
                        catch { }
                    }

                    if (DGR.Items[i].Cells[5].Text != "&nbsp;")
                    {
                        if (txtval.Visible)
                        {
                            txtval.Text = DGR.Items[i].Cells[5].Text;
                        }
                        if (ddlval.Visible)
                        {
                            try
                            {
                                ddlval.SelectedValue = DGR.Items[i].Cells[5].Text;
                            }
                            catch { }
                        }
                        if (txtdate.Visible)
                        {
                            txtdate.Text = DGR.Items[i].Cells[5].Text;
                        }
                    }
                }
            }
            catch { }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_QUOTATION_INSERT " +
                                    "'" + DDL_COMPANY.SelectedValue + "'," +
                                    "'" + DDL_PRODUCT.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    LB_QUOTNO.Text = conn.GetFieldValue("QUOTNO").ToString();

                    conn.QueryString = "exec SP_QUOTATION_VERSION_INSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    SaveDGR(LB_QUOTNO.Text);

                    Response.Redirect("QuotationFrame.aspx?code=" + LB_QUOTNO.Text);
                }
            }
            catch (SystemException ex)
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_DELETE '" + LB_QUOTNO.Text + "',1 " +
                                    "delete from ADDITIONAL_INFO_DATA where OWNER='" + LB_QUOTNO.Text + "' ";

                LB_ERROR.Text = ex.Message;
                return;
            }
        }

        protected void SaveDGR(string owner)
        {
            try
            {
                conn.QueryString = "delete from ADDITIONAL_INFO_DATA where OWNER='" + owner + "' ";
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
                        catch { }

                    }
                }

            }
            catch { }
        }
    }
}
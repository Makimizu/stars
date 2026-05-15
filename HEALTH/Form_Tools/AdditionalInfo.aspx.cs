using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class AdditionalInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_OWNER.Text = Request.QueryString["OWNER"];
                LB_READONLY.Text = Request.QueryString["READONLY"];
                LB_TIPE.Text = Request.QueryString["TIPE"];
                LoadDGR();
            }
        }

        private void LoadDGR()
        {
            try
            {
                if (LB_READONLY.Text == "1")
                {
                    DGR.Enabled = false;
                    BTN_SAVE.Enabled = false;
                }

                conn.QueryString = "SELECT a.CODE,a.GROUP_ID,GROUP_DESCR = b.DESCR,ADD_DESCR = a.DESCR,DATA_TYPE,DATA_LEN,SQL_REFF,c.VAL " +
                                        "FROM dbo.PARAM_ADDITIONAL_INFO a " +
                                        "INNER JOIN dbo.PARAM_ADDITIONAL_INFO_GROUP b ON b.CODE = a.GROUP_ID " +
                                        "LEFT JOIN dbo.ADDITIONAL_INFO_DATA c ON c.OWNER='" + LB_OWNER.Text + "' AND c.CODE = a.CODE " +
                                        "WHERE b.TIPE_ADD='" + LB_TIPE.Text + "' ORDER BY GROUP_ID,A.ORDER_NO";
                conn.ExecuteQuery();
                DGR.DataSource = conn.GetDataTable();
                DGR.DataBind();
                string GroupID = "";
                Color ItemColor = Color.Empty;
                Color Item1Color = Color.LightYellow;
                Color Item2Color = Color.Gainsboro;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
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
                    TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                    switch (DGR.Items[i].Cells[2].Text)
                    {
                        case "001": txtval.Visible = true;
                            txtval.Width = 400;
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

                    if (DGR.Items[i].Cells[4].Text != "&nbsp;")
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
                            ddlval.SelectedValue = DGR.Items[i].Cells[5].Text;
                        }
                        if (txtdate.Visible)
                        {
                            txtdate.Text = GlobalUse.GlobalDateFormat(DGR.Items[i].Cells[5].Text, "d/M/yyyy");

                        }
                    }
                }
            }
            catch (Exception ec)
            {
                LB_MSG.Text = ec.Message;
            }
        }

        protected void BTN_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "delete from ADDITIONAL_INFO_DATA where OWNER='" + LB_OWNER.Text + "' ";
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
                                                "'" + LB_OWNER.Text + "'," +
                                                "'" + DGR.Items[i].Cells[0].Text + "'," +
                                                "'" + val + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                            conn.ExecuteQuery();
                        }
                        catch { }

                    }
                }

                LB_MSG.Text = "Berhasil Simpan!";
            }
            catch (Exception ec)
            {
                LB_MSG.Text = ec.Message;
            }
        }

        protected void BTN_PRINT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rc:Parameters=False&NOMOR_SURAT_JAMINAN=" + LB_OWNER.Text + "' from V_LINK_SC_REPORT_LIST where CODE='367'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + conn.GetFieldValue("URL").ToString() + "','INFO MEDIS DIAGNOSA PENJAMINAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
        }
    }
}
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
    public partial class QuotationPersonOtherInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_MEMBERID.Text = Request.QueryString["MEMBERID"].ToString();

                Setup();

                FillDGRItem();
            }
        }

        protected void Setup()
        {
            /*
            conn.QueryString = "select FULLNAME " +
                                "from V_LINK_CB_MEMBER_MASTER " +
                                "where " +
                                "ID = '" + LB_MEMBERID.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            }
             */
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
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

                conn.QueryString = "exec SP_APPLICATION_MEMBER_OTHER_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_MEMBERID.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRItem();
        }

        protected void DDL_REFF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void FillDGRItem()
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER_OTHER_INFO " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_MEMBERID.Text + "'," +
                                "'UW'";
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
    }
}
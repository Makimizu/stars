using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GLIFE.Form_Tools
{
    public partial class OtherSetup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                LB_MODE.Text = Request.QueryString["MODE"].ToString();
                LB_readonly.Text = Request.QueryString["readonly"].ToString();
                LB_s.Text = Request.QueryString["s"].ToString();

                FillDGR_ITEM();
            }
        }

        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_PARAM_OTHER_SETTING " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_MODE.Text + "'";
            conn.ExecuteQuery();

            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");

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

                    }
                }
            }
        }

        protected void DGR_ITEM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
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
                                        "'" + GlobalUse.GetUserMgmt(LB_s.Text, "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                FillDGR_ITEM();
            }
        }
    }
}
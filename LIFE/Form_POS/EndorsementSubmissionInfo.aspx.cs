using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LIFE.Form_POS
{
    public partial class EndorsementSubmissionInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                FillDGR_ITEM();
                FillDGR_UW();
                CheckTrack();
            }
        }

        protected void FillDGR_UW()
        {
            conn.QueryString = "select " +
                                "[UNDERWRITING VERIFICATION :] = '- ' + REMARK " +
                                "from V_APPLICATION_ENDORSEMENT_PARENT_UW " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DGR_UW.DataSource = conn.GetDataTable().Copy();
                DGR_UW.DataBind();
            }
        }

        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_INFO " +
                                "'" + LB_REGNO.Text + "','" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();
            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");
                TextBox txtDATE = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_DATE");
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
                    switch (DGR_ITEM.Items[j].Cells[1].Text.Replace("&nbsp;", ""))
                    {
                        case "STR":
                            txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
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
                        case "DATE":
                            txtVAL.Visible = false;
                            ddl.Visible = false;
                            txtDATE.Visible = true;
                            txtDATE.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            break;
                    }

                    if (txtVAL.Text == "")
                    { txtVAL.Text = DateTime.Now.ToString("dd/MM/yyyy"); ; }
                    if (txtDATE.Text == "")
                    { txtDATE.Text = DateTime.Now.ToString("dd/MM/yyyy"); }

            
                }
            }
        }

        protected void CheckTrack()
        {
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_SEQ.Text + "')";
            conn.ExecuteQuery();

            int track = int.Parse(conn.GetFieldValue("TRACK").ToString());
            if (track >= 4)
            {
                DGR_ITEM.Enabled = false;
                DGR_ITEM.ShowFooter = false;
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
                    TextBox txtDate = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_DATE");

                    string val = txt.Text.Trim();
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    if (txtDate.Visible)
                        val = txtDate.Text.Trim();

                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_INFO_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                        "'" + val + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                FillDGR_ITEM();
            }

            if (e.CommandName == "Email")
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_QUOTATION_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
        }

        protected void DGR_ITEM_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Button btEMAIL = (Button)e.Item.FindControl("BT_EMAIL");

                //int allowSend = 0;
                conn.QueryString = "select CHK = count(*) from APPLICATION_ENDORSEMENT_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text;
                conn.ExecuteQuery();
                if (conn.GetFieldValue("CHK").ToString() != "0")
                {
                    //allowSend = 1;
                    btEMAIL.Visible = true;
                }
            }
        }
    }
}
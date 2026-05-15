using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class ProviderPIC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();

                FillDGR_PIC();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_PIC_TITLE_PROVIDER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PIC_TITLE_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDGR_PIC()
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.KODE_PIC, " +
                                "b.DESCR, " +
                                "a.NAMA_PIC, " +
                                "a.ALAMAT, " +
                                "a.TELP, " +
                                "a.TELP_EXT, " +
                                "a.HP, " +
                                "a.EMAIL " +
                                "from PROVIDER_PIC a " +
                                "inner join PR_PIC_TITLE_PROVIDER b on a.KODE_PIC=b.CODE " +
                                "where " +
                                "KODE_PROVIDER='" + LB_CODE.Text + "' " +
                                "order by b.DESCR,a.NAMA_PIC ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PIC.DataSource = dt;
            DGR_PIC.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_PIC_TITLE_PROVIDER order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_PIC.Items.Count; i++)
            {
                Label lbNAMA = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_NAMA");
                Label lbTITLE = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_TITLE");
                Label lbALAMAT = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_ALAMAT");
                Label lbTELP = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_TELP");
                Label lbEXT = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_EXT");
                Label lbHP = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_HP");
                Label lbEMAIL = (Label)DGR_PIC.Items[i].FindControl("LB_PIC_EMAIL");
                Button btDEL = (Button)DGR_PIC.Items[i].FindControl("BT_PIC_DELETE");

                TextBox txtNAMA = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_NAMA");
                DropDownList ddlTITLE = (DropDownList)DGR_PIC.Items[i].FindControl("DDL_PIC_TITLE");
                TextBox txtALAMAT = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_ALAMAT");
                TextBox txtTELP = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_TELP");
                TextBox txtEXT = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_EXT");
                TextBox txtHP = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_HP");
                TextBox txtEMAIL = (TextBox)DGR_PIC.Items[i].FindControl("TXT_PIC_EMAIL");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTITLE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                lbNAMA.Text = DGR_PIC.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                lbTITLE.Text = DGR_PIC.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                lbALAMAT.Text = DGR_PIC.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                lbTELP.Text = DGR_PIC.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                lbEXT.Text = DGR_PIC.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                lbHP.Text = DGR_PIC.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                lbEMAIL.Text = DGR_PIC.Items[i].Cells[8].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_PIC_ADD_Click(object sender, EventArgs e)
        {
            BT_PIC_ADD.Visible = false;
            TBL_PIC_ADD.Visible = true;
        }

        protected void BT_PIC_ADD0_Click(object sender, EventArgs e)
        {
            conn.QueryString = "insert into PROVIDER_PIC select " +
                                "NEWID(), " +
                                "'" + LB_CODE.Text + "', " +
                                "'" + DDL_PIC_TITLE_ADD.SelectedValue + "', " +
                                "'" + TXT_PIC_NAMA_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + TXT_PIC_ALAMAT_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + TXT_PIC_TELP_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + TXT_PIC_EXT_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + TXT_PIC_HP_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + TXT_PIC_EMAIL_ADD.Text.Trim().Replace("'", "`") + "', " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                "GETDATE(), " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                "GETDATE()";
            conn.ExecuteNonQuery();

            BT_PIC_ADD.Visible = true;
            TBL_PIC_ADD.Visible = false;

            FillDGR_PIC();
        }

        protected void DGR_PIC_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Label lbNAMA = (Label)e.Item.FindControl("LB_PIC_NAMA");
            Label lbTITLE = (Label)e.Item.FindControl("LB_PIC_TITLE");
            Label lbALAMAT = (Label)e.Item.FindControl("LB_PIC_ALAMAT");
            Label lbTELP = (Label)e.Item.FindControl("LB_PIC_TELP");
            Label lbEXT = (Label)e.Item.FindControl("LB_PIC_EXT");
            Label lbHP = (Label)e.Item.FindControl("LB_PIC_HP");
            Label lbEMAIL = (Label)e.Item.FindControl("LB_PIC_EMAIL");
            Button btEDIT = (Button)e.Item.FindControl("BT_PIC_EDIT");
            Button btSAVE = (Button)e.Item.FindControl("BT_PIC_SAVE");
            Button btDEL = (Button)e.Item.FindControl("BT_PIC_DELETE");

            TextBox txtNAMA = (TextBox)e.Item.FindControl("TXT_PIC_NAMA");
            DropDownList ddlTITLE = (DropDownList)e.Item.FindControl("DDL_PIC_TITLE");
            TextBox txtALAMAT = (TextBox)e.Item.FindControl("TXT_PIC_ALAMAT");
            TextBox txtTELP = (TextBox)e.Item.FindControl("TXT_PIC_TELP");
            TextBox txtEXT = (TextBox)e.Item.FindControl("TXT_PIC_EXT");
            TextBox txtHP = (TextBox)e.Item.FindControl("TXT_PIC_HP");
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_PIC_EMAIL");

            if (e.CommandName == "Edit")
            {
                btEDIT.Visible = false;
                lbNAMA.Visible = false;
                lbTITLE.Visible = false;
                lbALAMAT.Visible = false;
                lbTELP.Visible = false;
                lbEXT.Visible = false;
                lbHP.Visible = false;
                lbEMAIL.Visible = false;

                btSAVE.Visible = true;
                txtNAMA.Visible = true;
                ddlTITLE.Visible = true;
                txtALAMAT.Visible = true;
                txtTELP.Visible = true;
                txtEXT.Visible = true;
                txtHP.Visible = true;
                txtEMAIL.Visible = true;

                try
                {
                    txtNAMA.Text = e.Item.Cells[3].Text.Replace("&nbsp;", "");
                    txtALAMAT.Text = e.Item.Cells[4].Text.Replace("&nbsp;", "");
                    txtTELP.Text = e.Item.Cells[5].Text.Replace("&nbsp;", "");
                    txtEXT.Text = e.Item.Cells[6].Text.Replace("&nbsp;", "");
                    txtHP.Text = e.Item.Cells[7].Text.Replace("&nbsp;", "");
                    txtEMAIL.Text = e.Item.Cells[8].Text.Replace("&nbsp;", "");

                    try
                    {
                        ddlTITLE.SelectedValue = e.Item.Cells[1].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                conn.QueryString = "update PROVIDER_PIC set " +
                                    "KODE_PIC = '" + ddlTITLE.SelectedValue + "'," +
                                    "NAMA_PIC = '" + txtNAMA.Text.Trim() + "'," +
                                    "ALAMAT = '" + txtALAMAT.Text.Trim() + "'," +
                                    "TELP = '" + txtTELP.Text.Trim() + "'," +
                                    "TELP_EXT = '" + txtEXT.Text.Trim() + "'," +
                                    "HP = '" + txtHP.Text.Trim() + "'," +
                                    "EMAIL = '" + txtEMAIL.Text.Trim() + "' " +
                                    "where KODE_PROVIDER='" + LB_CODE.Text + "' and ID='" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                FillDGR_PIC();

            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PROVIDER_PIC where KODE_PROVIDER='" + LB_CODE.Text + "' and ID='" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                FillDGR_PIC();
            }
        }
    }
}
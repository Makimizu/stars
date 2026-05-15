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
    public partial class ProviderRekening : System.Web.UI.Page
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
                FillDGRRekening();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by BANK";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDGRRekening()
        {
            conn.QueryString = "select * from V_PROVIDER_REKENING where KODE_PROVIDER='" + LB_CODE.Text + "' ORDER BY STAT,LASTCHANGEDATE DESC";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REKENING.DataSource = dt;
            DGR_REKENING.DataBind();

            conn.QueryString = "select CODE,BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by BANK";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_REKENING.Items.Count; i++)
            {
                TextBox txtaccno = (TextBox)DGR_REKENING.Items[i].FindControl("TXT_ACCNO2");
                TextBox txtaccnama = (TextBox)DGR_REKENING.Items[i].FindControl("TXT_ACCNAMA2");
                DropDownList ddl = (DropDownList)DGR_REKENING.Items[i].FindControl("DDL_BANK");
                DropDownList ddlStat = (DropDownList)DGR_REKENING.Items[i].FindControl("DDL_ACC_STAT");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtaccno.Text = DGR_REKENING.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtaccnama.Text = DGR_REKENING.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                try
                {
                    ddl.SelectedValue = DGR_REKENING.Items[i].Cells[2].Text;
                }
                catch { }
                try
                {
                    ddlStat.SelectedValue = DGR_REKENING.Items[i].Cells[4].Text;
                }
                catch { }
            }
        }

        protected void BT_ACCSAVE_Click(object sender, EventArgs e)
        {
            if (TBL_ACC.Visible)
            {
                if (TXT_ACCNO.Text.Trim() == "" || TXT_ACCNAMA.Text.Trim() == "")
                    return;
                try
                {
                    conn.QueryString = "exec SP_CLM_PROVIDER_REKENING_UPSERT " +
                                        "null," +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + TXT_ACCNO.Text.Trim() + "'," +
                                        "'" + TXT_ACCNAMA.Text.Trim() + "'," +
                                        "'" + DDL_ACCBANK.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    TBL_ACC.Visible = false;
                    FillDGRRekening();
                }
                catch { return; }
            }
            else
            {
                TBL_ACC.Visible = true;
            }

            TXT_ACCNO.Text = "";
            TXT_ACCNAMA.Text = "";
        }

        protected void DGR_REKENING_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtaccno = (TextBox)e.Item.FindControl("TXT_ACCNO2");
                TextBox txtaccnama = (TextBox)e.Item.FindControl("TXT_ACCNAMA2");
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_BANK");
                DropDownList ddlStat = (DropDownList)e.Item.FindControl("DDL_ACC_STAT");

                if (txtaccno.Text.Trim() == "" || txtaccnama.Text.Trim() == "")
                    return;

                try
                {
                    conn.QueryString = "update PROVIDER_REKENING set " +
                                        "ACC_BANK='" + txtaccno.Text.Trim() + "', " +
                                        "NAMA='" + txtaccnama.Text.Trim() + "', " +
                                        "KODE_BANK='" + ddl.SelectedValue + "', " +
                                        "STAT='" + ddlStat.SelectedValue + "', " +
                                        "LASTCHANGEBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE=GETDATE() " +
                                        "where ID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRRekening();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PROVIDER_REKENING where ID='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRRekening();
                }
                catch { }
            }
        }
    }
}
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
    public partial class ProviderLayanan : System.Web.UI.Page
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
                FillDGR_Benefit();
                FillDGR_Layanan();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TIPE_LAYANAN_PROVIDER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LAY_TIPE_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR_Benefit()
        {
            conn.QueryString = "exec SP_CLM_PROVIDER_BENEFIT '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");

                if (DGR_BENEFIT.Items[i].Cells[2].Text == "True")
                    cb.Checked = true;
            }
        }

        protected void FillDGR_Layanan()
        {
            conn.QueryString = "select " +
                                "ID, " +
                                "TIPE_LAYANAN, " +
                                "LAYANAN, " +
                                "UNGGULAN=CONVERT(int,UNGGULAN), " +
                                "TGL_BERLAKU=convert(varchar(20),a.TGL_BERLAKU,103), " +
                                "TARIF = replace(convert(varchar(100),convert(money,isnull(TARIF,0)),1),'.00','') " +
                                "from PROVIDER_LAYANAN a  " +
                                "inner join PR_TIPE_LAYANAN_PROVIDER b on a.TIPE_LAYANAN=b.CODE " +
                                "where " +
                                "KODE_PROVIDER='" + LB_CODE.Text + "' " +
                                "order by TIPE_LAYANAN,LAYANAN";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LAYANAN.DataSource = dt;
            DGR_LAYANAN.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_TIPE_LAYANAN_PROVIDER order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_LAYANAN.Items.Count; i++)
            {

                DropDownList ddlTIPE = (DropDownList)DGR_LAYANAN.Items[i].FindControl("DDL_LAY_TIPE");
                TextBox txtLAYANAN = (TextBox)DGR_LAYANAN.Items[i].FindControl("TXT_LAY_LAYANAN");
                DropDownList ddlUNGGULAN = (DropDownList)DGR_LAYANAN.Items[i].FindControl("DDL_LAY_UNG");
                TextBox txtTARIF = (TextBox)DGR_LAYANAN.Items[i].FindControl("TXT_LAY_TARIF");
                TextBox txtTGL = (TextBox)DGR_LAYANAN.Items[i].FindControl("TXT_LAY_TGLEDIT");

                Button btSAVE = (Button)DGR_LAYANAN.Items[i].FindControl("BT_LAY_SAVE");
                Button btDELETE = (Button)DGR_LAYANAN.Items[i].FindControl("BT_LAY_DELETE");

                btDELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTIPE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                ddlTIPE.SelectedValue = DGR_LAYANAN.Items[i].Cells[1].Text;
                txtLAYANAN.Text = DGR_LAYANAN.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                ddlUNGGULAN.SelectedValue = DGR_LAYANAN.Items[i].Cells[3].Text;
                txtTARIF.Text = DGR_LAYANAN.Items[i].Cells[4].Text;
                txtTGL.Text = DGR_LAYANAN.Items[i].Cells[5].Text;
            }
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                CheckBox cb = (CheckBox)e.Item.FindControl("CB");
                string cek = "0";
                if (cb.Checked)
                    cek = "1";

                conn.QueryString = "delete from PROVIDER_BENEFIT where KODE_PROVIDER='" + LB_CODE.Text + "' and BENEFIT_ID='" + e.Item.Cells[0].Text + "'";
                conn.QueryString = conn.QueryString + " insert into PROVIDER_BENEFIT select NEWID(),'" + LB_CODE.Text + "','" + e.Item.Cells[0].Text + "'," + cek + ",'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE(),'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE() ";

                try
                {
                    conn.ExecuteNonQuery();
                    FillDGR_Benefit();
                }
                catch { }
            }
        }

        protected void BT_LAY_ADD_Click(object sender, EventArgs e)
        {
            BT_LAY_ADD.Visible = false;
            TBL_LAY_ADD.Visible = true;
        }

        protected void BT_LAY_ADD2_Click(object sender, EventArgs e)
        {
            if (TXT_LAY_LAYANAN_ADD.Text.Trim() == "" || TXT_LAY_TGL_ADD.Text.Trim() == "")
                return;

            conn.QueryString = "insert into PROVIDER_LAYANAN select " +
                                "NEWID(), " +
                                "'" + LB_CODE.Text + "', " +
                                "'" + DDL_LAY_TIPE_ADD.SelectedValue + "', " +
                                "'" + TXT_LAY_LAYANAN_ADD.Text.Trim() + "', " +
                                "'" + DDL_LAY_UNG_ADD.SelectedValue + "', " +
                                "'" + TXT_LAY_TARIF_ADD.Text.Trim().Replace(",", "") + "', " +
                                "'" + GlobalUse.GlobalDateFormat(TXT_LAY_TGL_ADD.Text.Trim(), "d/M/yyyy") + "', " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                "GETDATE(), " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                "GETDATE()";
            conn.ExecuteNonQuery();

            BT_LAY_ADD.Visible = true;
            TBL_LAY_ADD.Visible = false;

            FillDGR_Layanan();
        }

        protected void DGR_LAYANAN_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DropDownList ddlTIPE = (DropDownList)e.Item.FindControl("DDL_LAY_TIPE");
            TextBox txtLAYANAN = (TextBox)e.Item.FindControl("TXT_LAY_LAYANAN");
            DropDownList ddlUNGGULAN = (DropDownList)e.Item.FindControl("DDL_LAY_UNG");
            TextBox txtTARIF = (TextBox)e.Item.FindControl("TXT_LAY_TARIF");
            TextBox txtTGL = (TextBox)e.Item.FindControl("TXT_LAY_TGLEDIT");

            Button btSAVE = (Button)e.Item.FindControl("BT_LAY_SAVE");


            if (e.CommandName == "Save")
            {
                if (txtLAYANAN.Text.Trim() == "")
                    return;

                conn.QueryString = "update PROVIDER_LAYANAN set " +
                                    "TIPE_LAYANAN = '" + ddlTIPE.SelectedValue + "'," +
                                    "TARIF = '" + txtTARIF.Text.Trim().Replace(",", "") + "'," +
                                    "TGL_BERLAKU = '" + GlobalUse.GlobalDateFormat(txtTGL.Text.Trim(), "d/M/yyyy") + "'," +
                                    "LAYANAN = '" + txtLAYANAN.Text.Trim().Replace("'", "`") + "'," +
                                    "UNGGULAN = '" + ddlUNGGULAN.SelectedValue + "', " +
                                    "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                    "LASTCHANGEDATE = GETDATE() " +
                                    "where KODE_PROVIDER='" + LB_CODE.Text + "' and ID='" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                FillDGR_Layanan();
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PROVIDER_LAYANAN where KODE_PROVIDER='" + LB_CODE.Text + "' and ID='" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                FillDGR_Layanan();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Peserta_Deletion_List : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_BATCH_ID.Text = Request.QueryString["BATCH_ID"];
                FillDGR();
				
				//NOTIF STATUS BIAYA DELETION
                conn.QueryString = "select " +
                                    "BIAYA_DEL = (case when ISNULL(bb.BIAYA_AMOUNT,0) = 0 then '<br>BIAYA DELETION BELUM DISET DI MENU POLIS TAB TPA > SHOW CHARGE. <br> DATA YANG SUDAH DITAMBAHKAN DIBAWAH INI MOHON DIHAPUS TERLEBIH DAHULU. AGAR BIAYA DELETION MASUK DALAM PERHITUNGAN REFUND PREMI UNTUK MASA KEPESERTAAN <= 30 HARI.<br> NAMUN, APABILA MEMANG TIDAK ADA BIAYA DELETION MOHON ABAIKAN PESAN INI DAN SILAHKAN LANJUTKAN PROSES SELANJUTNYA' else '' end) " +
                                    "FROM PESERTA_KELUAR_TEMP a " +
                                    "INNER JOIN ENDORSEMENT_BATCH aa ON a.BATCH_ID = aa.BATCH_ID " +
                                    "LEFT JOIN ASKES_MIGRASI.dbo.POLICY_PERIOD_BIAYA bb on bb.POLICY_PERIOD_ID = aa.POLICY_PERIOD_ID and bb.TIPE_BIAYA='BIAYA_DEL' " +
                                    "where " +
                                    "a.BATCH_ID = '" + LB_BATCH_ID.Text + "'";
                conn.ExecuteQuery();

                LB_STATUS_BIAYA_DEL.Text = conn.GetFieldValue("BIAYA_DEL").ToString();
            }
        }

        protected bool isApproved()
        {
            bool bResult = false;

            try
            {
                conn.QueryString = "select ROW_ID from TRACK_DATA where TIPE_CODE='GPA' and OWNER='" + LB_BATCH_ID.Text + "' and SEQ > 3";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                    bResult = true;
            }
            catch { }

            return bResult;
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";

            conn.QueryString = "exec SP_GPA_PESERTA_KELUAR_TEMP @BATCH_ID = '" + LB_BATCH_ID.Text + "'";
            conn.ExecuteQuery();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btZERO = (Button)DGR.Items[i].FindControl("BT_ZERO");

                bt.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TAKE OUT ?')){return false;};");
                btZERO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk CLEAR REFUND PREMI ?')){return false;};");

                if (DGR.Items[i].Cells[3].Text == "Employee")
                {
                    DGR.Items[i].Cells[2].Text = "<B>" + DGR.Items[i].Cells[2].Text + "</B>";
                    DGR.Items[i].Cells[3].Text = "<B>" + DGR.Items[i].Cells[3].Text + "</B>";
                }
            }

            if (isApproved())
            {
                DGR.Columns[0].Visible = false;
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                    "REFUND         	= replace(convert(varchar(100),convert(money,SUM(REFUND_PREMI)),1),'.00','') " +
                                    "from V_PESERTA_KELUAR_TEMP " +
                                    "where " +
                                    "BATCH_ID = '" + LB_BATCH_ID.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[10].Text = "TOTAL";
                e.Item.Cells[11].Text = conn.GetFieldValue("REFUND").ToString();
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_GPA_PESERTA_KELUAR_PROSES_DELETE '" + LB_BATCH_ID.Text + "','" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                FillDGR();
            }

            if (e.CommandName == "Zero")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "update PESERTA_KELUAR_TEMP set REFUND_PREMI = 0 where BATCH_ID = '" + LB_BATCH_ID.Text + "' and REGNO = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                FillDGR();
            }
        }
    }
}
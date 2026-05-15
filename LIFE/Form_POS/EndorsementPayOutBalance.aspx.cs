using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Drawing;

namespace LIFE.Form_POS
{
    public partial class EndorsementPayOutBalance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_STATUS.Text = "ADD";
                if (Convert.ToString(LB_SEQ.Text) != "")
                {
                    string seqText = LB_SEQ.Text;
                    string allKeys = string.Join(",", Request.QueryString.AllKeys);
                    if (Request.QueryString.AllKeys.Contains("REMARK") == true)
                    {

                        LB_REMARK.Text = Request.QueryString["remark"].ToString();
                        string remarkText = LB_REMARK.Text;
                        int Seq = int.Parse(LB_SEQ.Text.Split(',')[0]);

                        if (Seq > 1)
                        {
                            { BT_SAVE_TRX.Enabled = false; }
                            if (remarkText != "")
                            {
                                int remarkValue = int.Parse(remarkText);
                            }

                        }
                        else
                        { BT_SAVE_TRX.Enabled = true; }


                    }
                    else { BT_SAVE_TRX.Enabled = true; }

                    CheckPAYDI();
                    ShowTRX();
                    ShowTransactionList();
                }
                else 
                {
                    BT_SAVE_TRX.Enabled = false;
                }
            }
            
        }

        protected void CheckPAYDI()
        {
            conn.QueryString = "select " +
                                "a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default and b.PAYDI = 0 " +
                                "where " +
                                "a.REGNO	= '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                Response.Redirect("../Standard/default.html");
        }

        protected void ShowTRX()
        {
            conn.QueryString = "select " +
                                "b.UNITIZE ,a.POLICY_NO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default " +
                                "where " +
                                "a.REGNO		= '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            if (conn.GetFieldValue("UNITIZE").ToString() == "0")
            {
                conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_SUMM '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_TRX.DataSource = dt;
                DGR_TRX.DataBind();

                for (int i = 0; i < DGR_TRX.Items.Count; i++)
                {
                    if (DGR_TRX.Items[i].Cells[0].Text == "D")
                    {
                        DGR_TRX.Items[i].ForeColor = System.Drawing.Color.Red;
                    }

                    if (DGR_TRX.Items[i].Cells[0].Text == "Z")
                    {
                        DGR_TRX.Items[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            else
            {
                conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_SUMM_UNIT_LINK '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_TRX_UNITLINK.DataSource = dt;
                DGR_TRX_UNITLINK.DataBind();

                for (int i = 0; i < DGR_TRX_UNITLINK.Items.Count; i++)
                {
                    if (DGR_TRX_UNITLINK.Items[i].Cells[0].Text == "D")
                    {
                        DGR_TRX_UNITLINK.Items[i].ForeColor = System.Drawing.Color.Red;
                    }

                    if (DGR_TRX_UNITLINK.Items[i].Cells[0].Text == "Z")
                    {
                        DGR_TRX_UNITLINK.Items[i].Cells[1].Font.Bold = true;
                        DGR_TRX_UNITLINK.Items[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }

        protected void BT_SAVE_TRX_Click(object sender, EventArgs e)
        {
                       
            string regno = LB_POLICYNO.Text + " - " +  LB_REGNO.Text;
            string seq = LB_SEQ.Text;
            string trxType = DDL_TRX_TYPE.SelectedValue;
            string txtpolicyNo = TXT_POLICY_NO.Text.Trim();
            string amount = TXT_AMOUNT.Text.Trim().Replace(",", "");
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            string tipe = LB_TYPE.Text;

            string[] parts = regno.Split('-').Select(p => p.Trim()).ToArray();

            // Check if policyNo contains either part
            //if (parts.Any(p => txtpolicyNo.Contains(p)))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "swal",
            //           "Swal.fire({ position:'center', icon:'success', title:'Gagal!', text:'Transaksi tidak berhasil disimpan ! Cek Policy No / RegNo yg di input', showConfirmButton:true });", true);

            //}
            //else
            //{
             
                //conn.QueryString = "DECLARE @SEARCH VARCHAR(50) ='" + txtpolicyNo + "';" +
                //" SELECT REGNO, POLICY_NO FROM V_APPLICATION_MASTER WHERE " +
                //" (CASE WHEN @SEARCH LIKE '%[^0-9]%'  THEN REGNO  ELSE POLICY_NO END) LIKE '%' + @SEARCH + '%';";

                //conn.ExecuteQuery();
                //if (conn.GetRowCount() > 0)
                //{
                    conn.QueryString = "EXEC SP_TRANSACTION_UPSERT '" + LB_REGNO.Text + "', '" + seq + "', '" + trxType + "', '" + txtpolicyNo + "', '" + amount + "', '" + user + "', '" + tipe + "'";
                    conn.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, GetType(), "swal",
                        "Swal.fire({ position:'center', icon:'success', title:'Berhasil!', text:'Transaksi berhasil disimpan', showConfirmButton:true });", true);
                        ShowTransactionList();
             //   }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "swal",
                //     "Swal.fire({ position:'center', icon:'success', title:'Gagal!', text:'Transaksi tidak berhasil disimpan ! Cek Policy No / RegNo yg di input', showConfirmButton:true });", true);


                //}
          //  }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "swal",
            //         "Swal.fire({ position:'center', icon:'success', title:'Gagal!', text:'Transaksi tidak berhasil disimpan, Policy No = RegNo ', showConfirmButton:true });", true);


            //}
       //    ShowTransactionList();

            string strRemark1 = "";

            // ambil reason dari database
            conn.QueryString = " SELECT REMARK FROM APPLICATION_ENDORSEMENT_REMARK WHERE REGNO ='" + LB_REGNO.Text + "' and SEQ= '" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();

            DataTable dt1 = conn.GetDataTable().Copy();
            string strRemark = conn.GetFieldValue("REMARK").ToString();
            if (LB_STATUS.Text.Equals("EDIT"))
            {
                string oldRemark = "_" + LB_EDIT_TRX_TYPE.Text + "_" + LB_EDIT_POLICYNO.Text + "_" + LB_EDIT_AMOUNT.Text;
                strRemark = strRemark.Replace(oldRemark, "");
            }

            for (int i = 0; i < DGR_TRX_LIST.Items.Count; i++)
            {
                if (DGR_TRX_LIST.Items[i].Cells[2].Text.Trim() != "0")
                {
                    trxType = DGR_TRX_LIST.Items[i].Cells[0].Text.Trim() + "_" + DGR_TRX_LIST.Items[i].Cells[1].Text.Trim() + "_" + DGR_TRX_LIST.Items[i].Cells[2].Text.Trim() + "";

                    //     if (i == 0)
                    //{ E
                    strRemark +=   "_" + trxType;
                }
                //}
                //else
                //{ strRemark = strRemark +  "_" + trxType; }

            }

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARKS_UPSERT " +
                             "'" + LB_REGNO.Text + "'," +
                             "'" + LB_SEQ.Text + "'," +
                             "'" + strRemark +
                             "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            LB_STATUS.Text = "ADD";

            string script = "parent.EndorseementPayoutheader.location.reload();";
            ScriptManager.RegisterStartupScript(this, GetType(), "RefreshFrame", script, true);
        }
 

        protected void ShowTransactionList()
        {  
            conn.QueryString = "EXEC SP_TRANSACTION_LIST '" + LB_REGNO.Text + "', '" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable();//.Copy();
            DGR_TRX_LIST.DataSource = dt;
            DGR_TRX_LIST.DataBind();

             
            // 🎨 Tambahkan pewarnaan baris setelah databind
            for (int i = 0; i < DGR_TRX_LIST.Items.Count; i++)
            {
                string trxType = DGR_TRX_LIST.Items[i].Cells[0].Text.Trim();
                switch (trxType.ToUpper())
                {
                    case "KONTRIBUSI":
                    case "KONTRIBUSI PERTAMA":
                        DGR_TRX_LIST.Items[i].BackColor = System.Drawing.Color.LightBlue;
                        break;

                    case "TOPUP":
                    case "TOP UP":
                    case "TOP UP IRREGULAR":
                        DGR_TRX_LIST.Items[i].BackColor = System.Drawing.Color.Honeydew;
                        DGR_TRX_LIST.Items[i].ForeColor = System.Drawing.Color.DarkGreen;
                        break;

                    case "WITHDRAWAL":
                    case "PENARIKAN":
                        DGR_TRX_LIST.Items[i].BackColor = System.Drawing.Color.MistyRose;
                        DGR_TRX_LIST.Items[i].ForeColor = System.Drawing.Color.DarkRed;
                        break;

                    default:
                        // warna default untuk baris lain
                        DGR_TRX_LIST.Items[i].BackColor = System.Drawing.Color.White;
                        break;
                }
      
            }
             
          


        }


        protected void DGR_TRX_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string policyNo = e.Item.Cells[1].Text;
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "EXEC SP_TRANSACTION_DELETE '" + LB_REGNO.Text + "', '" + LB_SEQ.Text + "', '" + policyNo + "'";
                conn.ExecuteNonQuery();

                //menghilangkan remark karena datanya dihapus
                conn.QueryString = " SELECT REMARK FROM APPLICATION_ENDORSEMENT_REMARK WHERE REGNO ='" + LB_REGNO.Text + "' and SEQ= '" + LB_SEQ.Text + "'";
                conn.ExecuteQuery();
                string strRemark = conn.GetFieldValue("REMARK").ToString();
                string tobeDeleted = "_" + e.Item.Cells[0].Text + "_" + e.Item.Cells[1].Text + "_" + e.Item.Cells[2].Text.Replace("&nbsp;", "");
                strRemark = strRemark.Replace(tobeDeleted, "");

                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REMARKS_UPSERT " +
                             "'" + LB_REGNO.Text + "'," +
                             "'" + LB_SEQ.Text + "'," +
                             "'" + strRemark +
                             "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, GetType(), "swalDel",
                    "Swal.fire({ position:'center', icon:'success', title:'Data dihapus', text:'Transaksi berhasil dihapus', showConfirmButton:false, timer:1500 });", true);
                ShowTransactionList();
            }
            else if (e.CommandName == "Update")
            {
                DDL_TRX_TYPE.SelectedValue = e.Item.Cells[0].Text;
                TXT_POLICY_NO.Text = e.Item.Cells[1].Text;
                TXT_AMOUNT.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
                LB_STATUS.Text = "EDIT";
                LB_EDIT_TRX_TYPE.Text = e.Item.Cells[0].Text;
                LB_EDIT_POLICYNO.Text = e.Item.Cells[1].Text;
                LB_EDIT_AMOUNT.Text = e.Item.Cells[2].Text.Replace("&nbsp;", "");
            }
        }

    }
}
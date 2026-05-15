using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Proses_ADD_List : System.Web.UI.Page
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
            conn.QueryString = "EXEC SP_GPA_PESERTA_MASUK_TEMP @BATCH_ID='" + LB_BATCH_ID.Text + "',@CREATEBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records"; ;

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
            if (conn.GetRowCount() == 0)
            {
                LB_RECORD.Visible = false;
                DGR.Visible = false;
            }
            else
            {
                LB_RECORD.Visible = true;
                DGR.Visible = true;
            }

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrPREMIUM = (DataGrid)DGR.Items[i].FindControl("DGR_PREMIUM");

                Label lbNAMA = (Label)DGR.Items[i].FindControl("LB_NAMA");
                Label lbREGNO = (Label)DGR.Items[i].FindControl("LB_REGNO");
                Label lbEMPLOYEE = (Label)DGR.Items[i].FindControl("LB_EMPLOYEE");
                Label lbGRUP = (Label)DGR.Items[i].FindControl("LB_GRUP");
                Label lbGENDER = (Label)DGR.Items[i].FindControl("LB_GENDER");
                Label lbTIPEID = (Label)DGR.Items[i].FindControl("LB_TIPEID");
                Label lbNOID = (Label)DGR.Items[i].FindControl("LB_NOID");
                Label lbDOB = (Label)DGR.Items[i].FindControl("LB_DOB");
                Label lbAGE = (Label)DGR.Items[i].FindControl("LB_AGE");

                Label lbPAKET = (Label)DGR.Items[i].FindControl("LB_TGLMASUK");
                Label lbTGLMASUK = (Label)DGR.Items[i].FindControl("LB_PAKET");
                Label lbBRANCH = (Label)DGR.Items[i].FindControl("LB_BRANCH");

                Label lbPHONE = (Label)DGR.Items[i].FindControl("LB_PHONE");
                Label lbEMAIL = (Label)DGR.Items[i].FindControl("LB_EMAIL");

                Label lbACCBANK = (Label)DGR.Items[i].FindControl("LB_BANK");
                Label lbACCNO = (Label)DGR.Items[i].FindControl("LB_ACCNO");
                Label lbACCNAMA = (Label)DGR.Items[i].FindControl("LB_ACCNAMA");

                TextBox txtPREMIUM = (TextBox)DGR.Items[i].FindControl("TXT_PREMIUM");

                Button bt = (Button)DGR.Items[i].FindControl("BTN_HAPUS");
                bt.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TAKE OUT ?')){return false;};");

                lbREGNO.Text = DGR.Items[i].Cells[1].Text;
                lbACCBANK.Text = DGR.Items[i].Cells[15].Text;
                lbACCNAMA.Text = DGR.Items[i].Cells[17].Text;
                lbACCNO.Text = DGR.Items[i].Cells[16].Text;
                lbDOB.Text = DGR.Items[i].Cells[8].Text;
                lbEMAIL.Text = DGR.Items[i].Cells[13].Text;
                lbGENDER.Text = DGR.Items[i].Cells[5].Text;
                lbGRUP.Text = DGR.Items[i].Cells[6].Text;
                lbNAMA.Text = DGR.Items[i].Cells[4].Text;
                lbNOID.Text = DGR.Items[i].Cells[11].Text;
                lbPAKET.Text = DGR.Items[i].Cells[18].Text;
                lbPHONE.Text = DGR.Items[i].Cells[12].Text;
                lbTGLMASUK.Text = DGR.Items[i].Cells[14].Text;
                lbTIPEID.Text = DGR.Items[i].Cells[10].Text;
                lbBRANCH.Text = DGR.Items[i].Cells[3].Text;
                lbAGE.Text = DGR.Items[i].Cells[9].Text;
                txtPREMIUM.Text = DGR.Items[i].Cells[25].Text;
                lbEMPLOYEE.Text = DGR.Items[i].Cells[27].Text;

                if (DGR.Items[i].Cells[1].Text == DGR.Items[i].Cells[2].Text)
                    DGR.Items[i].BackColor = Color.Yellow;

                if (DGR.Items[i].Cells[19].Text == "0")
                    DGR.Items[i].BackColor = Color.Pink;

            }

            if (isApproved())
            {
                DGR.Columns[0].Visible = false;
            }
        }

        protected void FillDGRReject()
        {
            conn.QueryString = "select " +
                                "WORKSHEET, " +
                                "[NO]			= a.SEQ, " +
                                "[FAMILY GROUP]	= F1, " +
                                "[REGNO EMP]		= F2, " +
                                "[BRANCH CODE]	= F3, " +
                                "[NAMA]			= F4, " +
                                "[SEX]			= F5, " +
                                "[DOB]			= F6, " +
                                "[TGL MASUK]		= F7, " +
                                "[IDENTITY NO]	= F8, " +
                                "[PHONE]			= F9, " +
                                "[ACC BANK]		= F10, " +
                                "[ACC NO]		= F11, " +
                                "[ACC NAMA]		= F12, " +
                                "[EMAIL]			= F13, " +
                                "[VIP]			= F14  " +
                                "from ENDORSEMENT_BATCH_DATAUPLOAD a " +
                                "left join PESERTA_MASUK_TEMP b on a.BATCH_ID=b.BATCH_ID and a.F4=b.NAMA and a.F1=b.FAMILY_GROUP and a.F5=b.SEX and convert(date,a.F6)=convert(date,b.DOB) " +
                                "left join POLICY_PERIOD_PACKAGE c on b.PACKAGE=c.ID and a.WORKSHEET = replace(c.DESCR, ' ','') " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCH_ID.Text + "' " +
                                "and b.NAMA is null " +
                                "order by 1,2";
            conn.ExecuteQuery();

            LB_REJECTED.Text = conn.GetRowCount().ToString() + " Records";
            DGR_REJECT.DataSource = conn.GetDataTable();
            DGR_REJECT.DataBind();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERR.Text = "";

            if (e.CommandName == "delete")
            {
                try
                {
                    conn.QueryString = "exec SP_GPA_PESERTA_MASUK_PROSES_ENTRY_DELETE '" + LB_BATCH_ID.Text + "','" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                    return;
                }
                FillDGR();
            }

            if (e.CommandName == "deleteAll")
            {
                try
                {
                    conn.QueryString = "delete from PESERTA_MASUK_TEMP where BATCH_ID = '" + LB_BATCH_ID.Text + "' " +
                                        "delete from ENDORSEMENT_BATCH_DATAUPLOAD where BATCH_ID = '" + LB_BATCH_ID.Text + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                    return;
                }
                FillDGR();
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Button btALL = (Button)e.Item.FindControl("BTN_HEADER_HAPUS");
                btALL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TAKE OUT semua ?')){return false;};");
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {

                conn.QueryString = "select " +
                                    "PREMIUM = replace(convert(varchar(100),convert(money,SUM(PREMIUM)),1),'.00','') " +
                                    "from PESERTA_MASUK_TEMP " +
                                    "where " +
                                    "BATCH_ID = '" + LB_BATCH_ID.Text + "' ";
                conn.ExecuteQuery();

                e.Item.Cells[24].Text = "TOTAL";
                e.Item.Cells[26].Text = conn.GetFieldValue("PREMIUM").ToString();
            }
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_MODE.SelectedValue == "1")
            {
                TR_APPROVED.Visible = true;
                TR_REJECT.Visible = false;
                FillDGR();
            }
            else
            {
                TR_APPROVED.Visible = false;
                TR_REJECT.Visible = true;
                FillDGRReject();
            }
        }
    }
}
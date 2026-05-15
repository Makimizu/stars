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
    public partial class ClaimRejectPending : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_MODE.Text = Request.QueryString["MODE"].ToString();
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                Setup();
                ShowTP();
            }

        }

        protected void CheckTrack()
        {
            if (LB_TRACK.Text == "3" || LB_TRACK.Text == "4")
            {
                TR_DOK.Visible = false;
            }
        }

        protected void Setup()
        {
            if (LB_MODE.Text == "3")
                LB_MODE_DESCR.Text = "KLAIM - TOLAK";
            else
                LB_MODE_DESCR.Text = "KLAIM - PENDING";

            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();

            conn.QueryString = "select A.CODE,DESCR = B.DESCR+' - '+A.DESCR1 from PARAM_CLAIM_TP_REASON A " +
                                "INNER JOIN dbo.PR_CLAIM_TP_CATEGORY B ON B.CODE = A.TP_CTGRY " +
                                "where " +
                                "a.code not in (select CODE from CLAIM_TP_REASON where CLAIM_NO='" + LB_CLAIMNO.Text + "') " +
                                "and TP='" + LB_MODE.Text + "' " +
                                "ORDER BY B.DESCR,A.DESCR";
            conn.ExecuteQuery();
            LB_TP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                LB_TP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE,DESCR from PR_TIPE_CLAIM_DOKUMEN where CODE not in " +
                                "(select CODE from CLAIM_MASTER_DOKUMEN where CLAIM_NO='" + LB_CLAIMNO.Text + "') " +
                                "order by DESCR";
            conn.ExecuteQuery();
            LB_DOK.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB_DOK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            CheckTrack();
        }

        protected void ShowTP()
        {
            FillDGR_REASONTP();
            FillDGRDOC();
            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_TP '" + LB_CLAIMNO.Text + "','" + LB_MODE.Text + "'";
                conn.ExecuteQuery();
                TXT_AMOUNTTP.Text = conn.GetFieldValue("AMOUNT").ToString();
            }
            catch { }

            if (conn.GetFieldValue("NEW").ToString() == "0")
            {
                LB_TP.Visible = true;
                DGR_REASONTP.Visible = true;
            }
            /*
        else
        {
            if (LB_MODE.Text != "3")
            {
                TXT_ALASAN_CARI.Visible = false;
                LB_TP.Visible = false;
                DGR_REASONTP.Visible = false;
            }
            else
            {
                TXT_ALASAN_CARI.Visible = true;
                LB_TP.Visible = true;
                DGR_REASONTP.Visible = true;
            }
        }
             * */
        }

        protected void FillDGR_REASONTP()
        {
            DGR_REASONTP.Visible = true;

            conn.QueryString = "select " +
                                    "a.CODE, " +
                                    "DESCR = b.DESCR1, " +
                                    "a.REMARK " +
                                    "from CLAIM_TP_REASON a " +
                                    "inner join PARAM_CLAIM_TP_REASON b on a.CODE=b.CODE and b.TP='" + LB_MODE.Text + "' " +
                                    "where a.CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                DGR_REASONTP.Visible = false;
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REASONTP.DataSource = dt;
            DGR_REASONTP.DataBind();

            for (int i = 0; i < DGR_REASONTP.Items.Count; i++)
            {
                Button btDel = (Button)DGR_REASONTP.Items[i].FindControl("BT_TPDEL");
                Label lbREASON = (Label)DGR_REASONTP.Items[i].FindControl("LB_REASON");
                TextBox txtRemark = (TextBox)DGR_REASONTP.Items[i].FindControl("TXT_TPREMARK");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                lbREASON.Text = DGR_REASONTP.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtRemark.Text = DGR_REASONTP.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void AddReason()
        {
            try
            {
                conn.QueryString = "insert into CLAIM_TP_REASON " +
                                    "select " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + LB_TP.SelectedValue + "'," +
                                    "''," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE(), NULL";
                conn.ExecuteNonQuery();
                FillDGR_REASONTP();
            }
            catch { }
        }

        protected void BT_SUBMITTP_Click(object sender, EventArgs e)
        {
            LB_TP_ERROR.Text = "";

            if (TXT_AMOUNTTP.Text.Replace(",", "") == "0" || LB_MODE.Text == "" || TXT_AMOUNTTP.Text.Replace(",", "") == "")
            {
                LB_TP_ERROR.Text = "Jumlah nilai klaim tidak boleh 0 (nol)";
                return;
            }
            try
            {
                if (LB_MODE.Text == "3")
                {
                    conn.QueryString = "SELECT * FROM dbo.CLAIM_TP_REASON WHERE CLAIM_NO='" + LB_CLAIMNO.Text + "'";
                    conn.ExecuteQuery();
                    if (conn.GetRowCount() == 0)
                    {
                        LB_TP_ERROR.Text = "Minimal pilih 1(satu) REMARK!";
                        return;
                    }
                }

                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_TP " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + LB_MODE.Text + "'," +
                                    "'" + TXT_AMOUNTTP.Text.Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                SaveDGRREASON();
                SaveDGRDOC();

                ShowTP();

            }
            catch (System.Exception ex)
            {
                LB_TP_ERROR.Text = ex.Message;
            }
        }

        protected void DGR_REASONTP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_TP_REASON where CLAIM_NO='" + LB_CLAIMNO.Text + "' and CODE='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    ShowTP();
                }
                catch { }
            }
        }

        protected void AddDOC()
        {
            try
            {
                conn.QueryString = "insert into CLAIM_MASTER_DOKUMEN " +
                                    "select " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + LB_DOK.SelectedValue + "'," +
                                    "''," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "null," +
                                    "null," +
                                    "null";
                conn.ExecuteNonQuery();
                FillDGRDOC();
            }
            catch { }
        }

        protected void FillDGRDOC()
        {
            DGR_DOK.Visible = true;
            DGR_DOK.Visible = true;
            conn.QueryString = "exec SP_CLM_CLAIM_MASTER_DOKUMEN '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOK.DataSource = dt;
            DGR_DOK.DataBind();

            if (conn.GetRowCount() == 0)
            {
                DGR_DOK.Visible = false;
                return;
            }

            for (int i = 0; i < DGR_DOK.Items.Count; i++)
            {
                Button btDel = (Button)DGR_DOK.Items[i].FindControl("BT_DOKDEL");

                Label lbDOC = (Label)DGR_DOK.Items[i].FindControl("LB_DOC");
                TextBox txtRemark = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKREMARK");
                TextBox txtReq = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKREQ");
                TextBox txtCom = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKCOM");
                DropDownList ddlFile = (DropDownList)DGR_DOK.Items[i].FindControl("DDL_DOKFILE");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE " + DGR_DOK.Items[i].Cells[2].Text.Replace("&nbsp;", "") + " ?')){return false;};");

                lbDOC.Text = DGR_DOK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtRemark.Text = DGR_DOK.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtReq.Text = DGR_DOK.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtCom.Text = DGR_DOK.Items[i].Cells[5].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_ADDREASON_Click(object sender, EventArgs e)
        {
            AddReason();
        }

        protected void BT_ADDDOC_Click(object sender, EventArgs e)
        {
            AddDOC();
        }

        protected void DGR_DOK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_MASTER_DOKUMEN where CLAIM_NO='" + LB_CLAIMNO.Text + "' and CODE='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRDOC();
                }
                catch { }
            }

        }

        protected void SaveDGRREASON()
        {
            for (int i = 0; i < DGR_REASONTP.Items.Count; i++)
            {
                TextBox txtRemark = (TextBox)DGR_REASONTP.Items[i].FindControl("TXT_TPREMARK");

                conn.QueryString = "update CLAIM_TP_REASON set " +
                                    "REMARK = '" + txtRemark.Text.Trim().Replace("'", "`") + "' " +
                                    "where " +
                                    "CLAIM_NO='" + LB_CLAIMNO.Text + "' " +
                                    "and CODE='" + DGR_REASONTP.Items[i].Cells[0].Text + "'";
                conn.ExecuteNonQuery();

            }
        }

        protected void SaveDGRDOC()
        {
            for (int i = 0; i < DGR_DOK.Items.Count; i++)
            {
                TextBox txtRemark = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKREMARK");
                TextBox txtReq = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKREQ");
                TextBox txtCom = (TextBox)DGR_DOK.Items[i].FindControl("TXT_DOKCOM");

                string dateReq = "null";
                string userReq = "null";
                string dateCom = "null";
                string userCom = "null";
                string File = "null";

                if (txtReq.Text.Trim() != "")
                {
                    dateReq = "'" + GlobalUse.GlobalDateFormat(txtReq.Text, "d/M/yyyy") + "'";
                    userReq = "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                }
                if (txtCom.Text.Trim() != "")
                {
                    dateCom = "'" + GlobalUse.GlobalDateFormat(txtCom.Text, "d/M/yyyy") + "'";
                    userCom = "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                }

                conn.QueryString = "exec SP_CLM_CLAIM_MASTER_DOKUMEN_SAVE " +
                                    "'" + txtRemark.Text.Trim() + "', " +
                                    dateReq + ", " +
                                    userReq + ", " +
                                    dateCom + ", " +
                                    userCom + ", " +
                                    "null, " +
                                    "'" + LB_CLAIMNO.Text + "', " +
                                    "'" + DGR_DOK.Items[i].Cells[0].Text + "'";
                conn.ExecuteNonQuery();

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace CORPORATE_PORTAL
{
    public partial class ClaimDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                try
                {
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                LoadClaim();
                LoadPending();
            }
        }

        protected void LoadClaim()
        {
            if (LB_CLAIMNO.Text.Trim() != "")
            {
                conn.QueryString = "select " +
                                    "a.NAMA,  " +
                                    "a.LAST_TRACK_DESCR,  " +
                                    "a.PROVIDER,  " +
                                    "THEDATE = convert(varchar(20), a.TGL_RAWAT_DARI, 106) + ' - ' + convert(varchar(20), a.TGL_RAWAT_SAMPAI, 106),  " +
                                    "a.BENEFIT_DESCR, " +
                                    "a.PR_DESCR " +
                                    "from V_LINK_HO_CLM_CLAIM_MASTER a " +
                                    "where " +
                                    "a.CLAIM_NO = '" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();

                LB_MEMBER.Text = conn.GetFieldValue("NAMA").ToString();
                LB_DATE.Text = conn.GetFieldValue("THEDATE").ToString();
                LB_STAT.Text = conn.GetFieldValue("LAST_TRACK_DESCR").ToString();
                LB_BENEFIT.Text = conn.GetFieldValue("BENEFIT_DESCR").ToString();
                LBL_PROVIDER_NAME.Text = conn.GetFieldValue("PROVIDER").ToString();
                LB_PR.Text = conn.GetFieldValue("PR_DESCR").ToString();

                conn.QueryString = "select a.ID, b.DESCR " +
                                    "from V_LINK_HO_CLAIM_ICD a " +
                                    "inner join V_LINK_HO_PR_ICD b on a.ICD_CODE = b.CODE " +
                                    "where " +
                                    "a.CLAIM_NO = '" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_ICD.DataSource = dt;
                DGR_ICD.DataBind();

                if (DGR_ICD.Items.Count == 0)
                    DGR_ICD.Visible = false;
                else
                    DGR_ICD.Visible = true;


                DisableEdit(this);
            }

            LoadArchieve();
            FillDGRSubmission();
        }

        protected void LoadPending()
        {
            DV_PENDING.Visible = false;

            conn.QueryString = "select " +
                                "CLAIM_NO = a.DOCNO " +
                                "from V_LINK_HO_CLAIM_TP a " +
                                "inner join V_LINK_HO_CLM_CLAIM_MASTER b on a.DOCNO = b.CLAIM_NO and b.LAST_TRACK in (1, 2) " +
                                "where " +
                                "a.TP = '2' " +
                                "and a.DOCNO = '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                return;

            DV_PENDING.Visible = true;

            conn.QueryString = "select " +
                                    "SEQ = ROW_NUMBER() over (order by USERDATE), " +
                                    "a.CODE, " +
                                    "DESCR = b.DESCR1, " +
                                    "a.REMARK " +
                                    "from V_LINK_HO_CLAIM_TP_REASON a " +
                                    "inner join V_LINK_HO_PARAM_CLAIM_TP_REASON b on a.CODE=b.CODE and b.TP='2' " +
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
        }

        protected void LoadArchieve()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "NAMAFILE, " +
                                "SQL = 'select THEFILE from V_LINK_ARCHIEVE where CODE=''' + CODE + '''' " +
                                "from V_LINK_ARCHIEVE " +
                                "where " +
                                "TIPE = 'HO_CLMNO' " +
                                "and OWNER1 = '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ARSIP.DataSource = dt;
            DGR_ARSIP.DataBind();

            for (int i = 0; i < DGR_ARSIP.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_ARSIP.Items[i].FindControl("LB_VIEW");
                lb.Text = DGR_ARSIP.Items[i].Cells[1].Text;
            }
        }

        protected void DisableEdit(Control root)
        {
            /*
            DDL_MEMBER.Enabled = false;
            DDL_BENEFIT.Enabled = false;
            BT_SAVE.Visible = false;
            LB_SEARCH_PROV.Visible = false;
            LB_SEARCH_ICD.Visible = false;

            TXT_STARTCARE.ReadOnly = true;
            TXT_ENDCARE.ReadOnly = true;

            for (int i = 0; i < DGR_ICD.Items.Count; i++)
            {
                DGR_ICD.Items[i].Cells[2].Visible = false;
            }

            for (int i = 0; i < DGR_ARSIP.Items.Count; i++)
            {
                DGR_ARSIP.Items[i].Cells[3].Visible = false;
            }
            */

            foreach (Control control in root.Controls)
            {
                if (control is Button)
                    control.Visible = false;

                if (control is LinkButton && control != LB_BACK)
                    control.Visible = false;

                if (control is TextBox)
                    ((TextBox)control).Enabled = false;

                if (control is DropDownList)
                    ((DropDownList)control).Enabled = false;

                if (control.Controls != null)
                {
                    DisableEdit(control);
                }
            }
        }

        protected void FillDGRSubmission()
        {
            conn.QueryString = "select " +
                                    "DESCR = c.DIS_BENEFIT_DETAIL_NAME, " +
                                    "INCURRED = replace(convert(varchar(100), convert(money, a.AMOUNT_PENGAJUAN), 1), '.00', '') , " +
                                    "REJECTED = replace(convert(varchar(100), convert(money, isnull(a.AMOUNT_EXCESS, 0) + isnull(a.AMOUNT_UNPAID, 0)), 1), '.00', '') , " +
                                    "PAID = replace(convert(varchar(100), convert(money, a.AMOUNT_BAYAR), 1), '.00', '') " +
                                    "from V_LINK_HO_CLAIM_BENEFIT a " +
                                    "inner join V_LINK_HO_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID = b.ID " +
                                    "inner join V_LINK_HO_PARAM_ACT_BENEFIT_DETAIL c on b.MORBIDITY_ID = c.ID " +
                                    "where CLAIM_NO = '" + LB_CLAIMNO.Text + "' " +
                                    "union all " +
                                    "select " +
                                    "DESCR = 'TOTAL', " +
                                    "INCURRED = replace(convert(varchar(100), convert(money, SUM(a.AMOUNT_PENGAJUAN)), 1), '.00', ''), " +
                                    "REJECTED = replace(convert(varchar(100), convert(money, SUM(isnull(a.AMOUNT_EXCESS, 0) + isnull(a.AMOUNT_UNPAID, 0))), 1), '.00', ''), " +
                                    "PAID = replace(convert(varchar(100), convert(money, SUM(a.AMOUNT_BAYAR)), 1), '.00', '') " +
                                    "from V_LINK_HO_CLAIM_BENEFIT a " +
                                    "where " +
                                    "CLAIM_NO = '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DONE.DataSource = dt;
            DGR_DONE.DataBind();

            for (int i = 0; i < DGR_DONE.Items.Count; i++)
            {
                if (DGR_DONE.Items[i].Cells[0].Text == "TOTAL")
                {
                    DGR_DONE.Items[i].BackColor = System.Drawing.Color.Silver;
                }
            }
        }
        
        protected void DGR_ARSIP_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                LB_FILENAME.Text = e.Item.Cells[1].Text;
                LB_SQL.Text = e.Item.Cells[2].Text;

                conn.QueryString = e.Item.Cells[2].Text;
                conn.ExecuteQuery();
                string base64image = GlobalUse.GetStringImageURL(conn.QueryString, "THEFILE");
                IMG.ImageUrl = base64image;
                DV_IMAGE.Visible = true;
            }

        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimDetail.aspx?CLAIM_NO=" + LB_CLAIMNO.Text);
        }

        protected void LB_DOWNLOAD_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalUse.SQLToFile(LB_FILENAME.Text, LB_SQL.Text, Page);
            }
            catch { }
        }
        

        
    }
}
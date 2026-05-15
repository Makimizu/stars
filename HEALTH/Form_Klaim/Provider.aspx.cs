using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Security.Claims;

namespace HEALTH.Form_Klaim
{
    //By Ferdi V2
    public partial class Provider : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TITLE_PROVIDER";
            conn.ExecuteQuery();
            DDL_TITLE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TITLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_JENIS_PROVIDER order by DESCR desc";
            conn.ExecuteQuery();
            DDL_JENIS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_JENIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ProviderFrame.aspx?code=" + e.Item.Cells[1].Text);
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string select = "";

            if (DDL_TITLE.SelectedValue != "")
                where = where + " and KODE_TITLE='" + DDL_TITLE.SelectedValue + "' ";

            if (DDL_JENIS.SelectedValue != "")
                where = where + " and JENIS_PROVIDER='" + DDL_JENIS.SelectedValue + "' ";

            if (TXT_NAMA.Text.Trim() != "")
                where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_GROUP.Text.Trim() != "")
                where = where + " and PROVIDER_GROUP like '%" + TXT_GROUP.Text.Trim() + "%' ";

            if (TXT_KOTA.Text.Trim() != "")
                where = where + " and KOTA_DESCR like '%" + TXT_KOTA.Text.Trim() + "%' ";

            if (TXT_PROPINSI.Text.Trim() != "")
                where = where + " and PROPINSI_DESCR like '%" + TXT_PROPINSI.Text.Trim() + "%' ";

            if (DDL_PKS.SelectedValue != "" && chk_2_5Bulan.Checked)
            {
                if (DDL_PKS.SelectedValue == "0")
                {

                    if (chk_2_5Bulan.Checked)
                    {
                        where = where += " AND TGL_AKHIR_PKS >= DATEADD(DAY, -15, DATEADD(MONTH, -2, GETDATE())) AND TGL_AKHIR_PKS <= GETDATE() ";

                        select = @" CASE 

                            WHEN TGL_AKHIR_PKS is not null THEN 'AKAN BERAKHIR'

                            ELSE ''

                        END AS STATUS_TGL_AKHIR_PKS ";
                    }
                    else
                    {
                        where = where + " and TGL_AKHIR_PKS>convert(date,GETDATE()) ";

                        select = @" CASE 

                            WHEN TGL_AKHIR_PKS is not null THEN 'BELUM BERAKHIR'

                            ELSE ''

                        END AS STATUS_TGL_AKHIR_PKS ";
                    }
                }
                else
                {
                    where = where + " and TGL_AKHIR_PKS>convert(date,GETDATE()) ";

                    where = where += " AND TGL_AKHIR_PKS >= DATEADD(DAY, -15, DATEADD(MONTH, -2, GETDATE())) AND TGL_AKHIR_PKS <= GETDATE() ";

                    select = @" CASE 

                            WHEN TGL_AKHIR_PKS is not null THEN 'AKAN BERAKHIR'

                            ELSE ''

                        END AS STATUS_TGL_AKHIR_PKS ";
                }

            }
            else if (DDL_PKS.SelectedValue != "" && chk_2_5Bulan.Checked == false)
            {
                if (DDL_PKS.SelectedValue == "0")
                {
                    where = where += " AND TGL_AKHIR_PKS > CONVERT(DATE, GETDATE())  ";

                    select = @" CASE 

                            WHEN TGL_AKHIR_PKS is not null THEN 'BELUM BERAKHIR'

                            ELSE ''

                        END AS STATUS_TGL_AKHIR_PKS ";
                }
                else
                {
                    where = where + " AND TGL_AKHIR_PKS < CONVERT(DATE, GETDATE()) ";
                    select = @"CASE 

                            WHEN TGL_AKHIR_PKS < GETDATE() THEN 'SUDAH BERAKHIR'

                            ELSE ''

                            END AS STATUS_TGL_AKHIR_PKS";
                }
            }
            else if (DDL_PKS.SelectedValue == "" && chk_2_5Bulan.Checked)
            {
                where = where += " AND TGL_AKHIR_PKS >= DATEADD(DAY, -15, DATEADD(MONTH, -2, GETDATE())) AND TGL_AKHIR_PKS <= GETDATE() ";

                select = @" CASE 

                            WHEN TGL_AKHIR_PKS is not null THEN 'AKAN BERAKHIR'

                            ELSE ''

                        END AS STATUS_TGL_AKHIR_PKS ";
            }
            else
            {
                select = @"CASE 
		                    WHEN TGL_AKHIR_PKS >= DATEADD(DAY, -15, DATEADD(MONTH, -2, GETDATE())) AND TGL_AKHIR_PKS <= GETDATE() THEN 'AKAN BERAKHIR'
		                    ELSE 
		                    CASE
		                    WHEN CONVERT(DATE, GETDATE()) < TGL_AKHIR_PKS THEN 'BELUM BERAKHIR'
		                    ELSE 'SUDAH BERAKHIR'
		                    END
	                    END AS STATUS_TGL_AKHIR_PKS";
            }

            if (DDL_RI.SelectedValue != "")
                where = where + " and RI='" + DDL_RI.SelectedValue + "' ";

            if (DDL_RJ.SelectedValue != "")
                where = where + " and RJ='" + DDL_RJ.SelectedValue + "' ";

            if (DDL_MCU.SelectedValue != "")
                where = where + " and MCU='" + DDL_MCU.SelectedValue + "' ";

            if (DDL_OPT.SelectedValue != "")
                where = where + " and OPT='" + DDL_OPT.SelectedValue + "' ";

            if (DDL_TOPUP.SelectedValue != "")
                where = where + " and TOPUP='" + DDL_TOPUP.SelectedValue + "' ";

            if (DDL_IDV.SelectedValue != "")
                where = where + " and IDV='" + DDL_IDV.SelectedValue + "' ";

            if (DDL_DRB.SelectedValue != "")
                where = where + " and DRB='" + DDL_DRB.SelectedValue + "' ";

            if (DDL_LAB.SelectedValue != "")
                where = where + " and LAB='" + DDL_LAB.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "KODE_PROVIDER, " +
                                "TITLE, " +
                                "NAMA = UPPER(ltrim(NAMA)), " +
                                "PROVIDER_GROUP, " +
                                "JENIS_PROVIDER_DESCR, " +
                                "KOTA_DESCR, " +
                                "PROPINSI_DESCR, " +
                                "STAT_DESCR, " +
                                "TGL_AKHIR_PKS, " +
                                "EXP = (case when convert(date,GETDATE())<TGL_AKHIR_PKS then 0 else 1 end), " +
                                "STAT, " +
                                "RI = (case when RI=1 then 'X' else '' end), " +
                                "RJ = (case when RJ=1 then 'X' else '' end), " +
                                "MCU = (case when MCU=1 then 'X' else '' end), " +
                                "OPT = (case when OPT=1 then 'X' else '' end), " +
                                "TOPUP = (case when TOPUP=1 then 'X' else '' end), " +
                                "IDV = (case when IDV=1 then 'X' else '' end), " +
                                "DRB = (case when DRB=1 then 'X' else '' end), " +
                                "LAB = (case when LAB=1 then 'X' else '' end), " +
                                " " + select + " " +
                                "from V_CLM_PROVIDER " +
                                "where " +
                                "KODE_PROVIDER in (select OWNER from TRACK_DATA where TIPE_CODE='CLMPROV' and SEQ=2) " +
                                "and STAT like '%" + DDL_STAT.SelectedValue + "%' " + where +
                                "order by ltrim(NAMA) ";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                string status = DGR.Items[i].Cells[19].Text;
                if (status == "AKAN BERAKHIR")
                {
                    DGR.Items[i].Cells[19].ForeColor = System.Drawing.Color.Orange;
                    DGR.Items[i].Cells[19].Font.Bold = true;
                }
                else if (status == "SUDAH BERAKHIR")
                {
                    DGR.Items[i].Cells[19].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[19].Font.Bold = true;
                }
                else if (status == "BELUM BERAKHIR")
                {
                    DGR.Items[i].Cells[19].ForeColor = System.Drawing.Color.Black;
                    DGR.Items[i].Cells[19].Font.Bold = true;
                }
            }
        }

        protected void BT_CP_LIST_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "CPList.aspx");
        }
        protected void BT_CP_Click(object sender, EventArgs e)
        {
            ShowPopUp("Laporan Provider Clinical Pathway", "CPbyRs.aspx");
        }

        protected void BT_TR_Click(object sender, EventArgs e)
        {
            ShowPopUp("Laporan Tarif Kamar", "CPbyTr.aspx");
        }

        protected void BT_TR_Quo_Click(object sender, EventArgs e)
        {
            ShowPopUp("Laporan Tarif Quotation Clinical Pathway", "CPbyTrQuo.aspx");
        }

        protected void BT_TR_ICD_Click(object sender, EventArgs e)
        {
            //Add By Ferdi
            ShowPopUp("Laporan Tarif ICD Clinical Pathway", "CPbyTrICD.aspx"); 

        }
        protected void ShowPopUp(string title, string url)
        {
            LB_TITLE.Text = title;
            //ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';   popup.style.width = '1000px'; popup.style.height = '10000px'; ", true);
            ClientScript.RegisterStartupScript(
            this.GetType(),
            "focus",
            "var popup = document.getElementById('pnlpopup'); popup.style.display = 'block';",
            true
            );
            ifClaim.Attributes.Add("src", url);
        }
    }
} 
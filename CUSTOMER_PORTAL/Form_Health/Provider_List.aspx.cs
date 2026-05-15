using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Form_Health
{
    public partial class Provider_List : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from V_LINK_HO_PR_TITLE_PROVIDER";
            conn.ExecuteQuery();
            DDL_TITLE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TITLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from V_LINK_HO_PR_JENIS_PROVIDER order by DESCR desc";
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


        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

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

            if (DDL_PKS.SelectedValue != "")
            {
                if (DDL_PKS.SelectedValue == "1")
                    where = where + " and TGL_AKHIR_PKS<=convert(date,GETDATE()) ";
                else
                    where = where + " and TGL_AKHIR_PKS>convert(date,GETDATE()) ";
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
                                "TGL_AKHIR_PKS = convert(varchar(20),TGL_AKHIR_PKS,106), " +
                                "EXP = (case when convert(date,GETDATE())<TGL_AKHIR_PKS then 0 else 1 end), " +
                                "STAT, " +
                                "REPORT_URL, " +
                                "RI = (case when RI=1 then 'X' else '' end), " +
                                "RJ = (case when RJ=1 then 'X' else '' end), " +
                                "MCU = (case when MCU=1 then 'X' else '' end), " +
                                "OPT = (case when OPT=1 then 'X' else '' end), " +
                                "TOPUP = (case when TOPUP=1 then 'X' else '' end), " +
                                "IDV = (case when IDV=1 then 'X' else '' end), " +
                                "DRB = (case when DRB=1 then 'X' else '' end), " +
                                "LAB = (case when LAB=1 then 'X' else '' end) " +
                                "from V_LINK_HO_CLM_PROVIDER " +
                                "where " +
                                "KODE_PROVIDER in (select OWNER from V_LINK_HO_TRACK_DATA where TIPE_CODE='CLMPROV' and SEQ=2) " +
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
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_SELECT");
                lbID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[20].Text + "','Claim','height=500px,width=700px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[18].Text == "1")
                {
                    DGR.Items[i].Cells[17].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[17].Font.Bold = true;
                }

                if (DGR.Items[i].Cells[19].Text == "0")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }
            }
        }
    }
}
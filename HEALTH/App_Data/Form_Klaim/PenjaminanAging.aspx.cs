using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace HEALTH.Form_Klaim
{
    public partial class PenjaminanAging : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TIPE_SURAT_JAMINAN";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_NAMA.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_PROVIDER.Text.Trim() != "")
                where = where + " and a.PROVIDER like '%" + TXT_PROVIDER.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.TIPE_CODE = '" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_AGING1.Text.Trim() != "")
                where = where + " and a.AGING >= " + TXT_AGING1.Text.Trim() + " ";

            if (TXT_AGING2.Text.Trim() != "")
                where = where + " and a.AGING <= " + TXT_AGING2.Text.Trim() + " ";

            conn.QueryString = "select " +
                                "CLAIM_NO, " +
                                "NOMOR_SURAT_JAMINAN, " +
                                "TIPE, " +
                                "NAMA, " +
                                "INCURRED = replace(convert(varchar(100),convert(money,INCURRED),1),'.00',''), " +
                                "PROVIDER, " +
                                "EMAIL, " +
                                "PHONE, " +
                                "TGL_MASUK = convert(varchar(20),TGL_MASUK,106), " +
                                "TGL_AKHIR = convert(varchar(20),TGL_AKHIR,106), " +
                                "AGING, " +
                                "REPORT_URL	= b.URL + '&rc:Parameters=False&CLAIM_NO=' + a.CLAIM_NO + '&rc:Zoom=Page20%Width' " +
                                "from V_CLM_PENJAMINAN_AGING a " +
                                "inner join V_LINK_SC_REPORT_LIST b on b.APP_ID='HO' and b.CODE='288' " +
                                "where 1=1 " + where +
                                "order by " +
                                "AGING desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton btCLAIMNO = (LinkButton)DGR.Items[i].FindControl("LB_CLAIMNO");
                LinkButton btPENJAMINAN = (LinkButton)DGR.Items[i].FindControl("LB_PENJAMINAN");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                btCLAIMNO.Text = DGR.Items[i].Cells[2].Text;
                btPENJAMINAN.Text = DGR.Items[i].Cells[3].Text;
                txtEMAIL.Text = DGR.Items[i].Cells[12].Text.Replace("&nbsp;","");

                btCLAIMNO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[13].Text + "','CLAIM_DETAIL','height=400,width=800,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "ClaimNo")
            {

            }

            if (e.CommandName == "Penjaminan")
            {
                Response.Redirect("Penjaminan.aspx?NOSURAT=" + e.Item.Cells[3].Text);
            }
        }
    }
}
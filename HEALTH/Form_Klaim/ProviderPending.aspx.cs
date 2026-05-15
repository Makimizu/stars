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
    public partial class ProviderPending : System.Web.UI.Page
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

            conn.QueryString = "select CODE,DESCR from PR_JENIS_PROVIDER";
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
                Response.Redirect("ProviderRegistrasi.aspx?code=" + e.Item.Cells[1].Text);
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

            if (DDL_TITLE.SelectedValue != "")
                where = where + " and a.KODE_TITLE='" + DDL_TITLE.SelectedValue + "' ";

            if (DDL_JENIS.SelectedValue != "")
                where = where + " and a.JENIS_PROVIDER='" + DDL_JENIS.SelectedValue + "' ";

            if (TXT_NAMA.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_GROUP.Text.Trim() != "")
                where = where + " and pg.NAMA like '%" + TXT_GROUP.Text.Trim() + "%' ";

            if (TXT_KOTA.Text.Trim() != "")
                where = where + " and KOTA_DESCR like '%" + TXT_KOTA.Text.Trim() + "%' ";

            if (TXT_PROPINSI.Text.Trim() != "")
                where = where + " and a.PROPINSI_DESCR like '%" + TXT_PROPINSI.Text.Trim() + "%' ";


            conn.QueryString = "select " +
                                "a.KODE_PROVIDER,  " +
                                "TITLE = tl.DESCR,  " +
                                "NAMA = UPPER(ltrim(a.NAMA)),  " +
                                "PROVIDER_GROUP = pg.NAMA,  " +
                                "JENIS_PROVIDER_DESCR = jp.DESCR,  " +
                                "KOTA_DESCR = ko.DESCR,  " +
                                "PROPINSI_DESCR = pr.DESCR " +
                                "from PROVIDER_MASTER a  " +
                                "left join PROVIDER_MASTER pg on a.KODE_PROVIDERGROUP=pg.KODE_PROVIDER " +
                                "inner join PR_JENIS_PROVIDER jp on a.JENIS_PROVIDER=jp.CODE " +
                                "inner join PARAM_TBL_KOTA_PROVIDER ko on a.KOTA=ko.CODE " +
                                "left join PR_TITLE_PROVIDER tl on a.KODE_TITLE=tl.CODE " +
                                "inner join PR_PROPINSI pr on ko.PROPINSI=pr.CODE " +
                                "left join TRACK_DATA b on a.KODE_PROVIDER=b.OWNER and b.TIPE_CODE='CLMPROV' and b.SEQ=2  " +
                                "where   " +
                                "b.OWNER is null  " + where +
                                "order by ltrim(a.NAMA)";

            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }
    }
}
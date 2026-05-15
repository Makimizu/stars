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
    public partial class Member_Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                ShowMASTER();
                DGR_FAMILY.CurrentPageIndex = 0;
                ShowFAMILY();
                ShowPERIOD();
                ShowTC();
            }
        }

        protected void ShowMASTER()
        {
            conn.QueryString = "exec SP_LINK_HO_UW_PESERTA_INFO '" + LB_REGNO.Text + "','1',null,null";
            conn.ExecuteQuery();

            LB_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
            LB_CABANG.Text = conn.GetFieldValue("NAMA_CABANG").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            LB_FAMILY.Text = conn.GetFieldValue("FAMILY_GROUP_DESCR").ToString();
            LB_IDNO.Text = conn.GetFieldValue("ID_NO").ToString();
            LB_IDTYPE.Text = conn.GetFieldValue("ID_DESCR").ToString();
            LB_KELAS.Text = conn.GetFieldValue("KELAS").ToString();
            LB_NOPOLIS.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_NOREK.Text = conn.GetFieldValue("ACC_NO").ToString();
            LB_NOREKBANK.Text = conn.GetFieldValue("ACC_BANK_DESCR").ToString();
            LB_NOREKNAMA.Text = conn.GetFieldValue("ACC_NAMA").ToString();
            LB_PACKAGE.Text = conn.GetFieldValue("PACKAGE_DESCR").ToString();
            LB_PHONE.Text = conn.GetFieldValue("PHONE").ToString();
            LB_SEX.Text = conn.GetFieldValue("SEX").ToString();
            LB_STATUS.Text = conn.GetFieldValue("STAT").ToString();
            LB_TGLKELUAR.Text = conn.GetFieldValue("TGL_KELUAR").ToString();
            LB_TGLMASUK.Text = conn.GetFieldValue("TGL_MASUK").ToString();
            LB_VIP.Text = conn.GetFieldValue("VIP").ToString();
        }

        protected void ShowFAMILY()
        {
            conn.QueryString = "exec SP_LINK_HO_UW_PESERTA_INFO '" + LB_REGNO.Text + "','2',null,null";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FAMILY.DataSource = dt;
            DGR_FAMILY.DataBind();

            for (int i = 0; i < DGR_FAMILY.Items.Count; i++)
            {
                Label lb = (Label)DGR_FAMILY.Items[i].FindControl("LB_FAMILY_NAMA");
                lb.Text = "<a href='Member_Header.aspx?regno=" + DGR_FAMILY.Items[i].Cells[0].Text + "' target='memberheader'><span style='color: #0000FF'>" + DGR_FAMILY.Items[i].Cells[1].Text + "</span>";
            }
        }

        protected void ShowPERIOD()
        {
            DDL_PERIOD.Items.Clear();
            conn.QueryString = "exec SP_LINK_HO_UW_PESERTA_INFO '" + LB_REGNO.Text + "','3',null,null";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, "PACKAGE").ToString(), conn.GetFieldValue(i, "ID").ToString()));
            }

            try
            {
                DDL_PERIOD.SelectedIndex = 0;
            }
            catch { }
        }

        protected void DGR_FAMILY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_FAMILY.CurrentPageIndex = e.NewPageIndex;
            ShowFAMILY();
        }

        protected void ShowTC()
        {
            LB_MODE.Text = BT_5c.Text;
            ShowReport("HO", "314", "&POLICY_PERIOD_ID=" + DDL_PERIOD.SelectedValue);
        }

        protected void BT_5c_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BT_5d_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ShowReport("HO", "280", "&POLICY_PERIOD_ID=" + DDL_PERIOD.SelectedValue);
        }

        protected void BT_5b_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'Member_Benefit.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + DDL_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_5_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'Member_ClaimHistory.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + DDL_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_5a_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'Member_ClaimTrx.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + DDL_PERIOD.SelectedValue + "';</script>");
        }

        protected void ShowReport(string appid, string reportcode, string param)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where APP_ID='" + appid + "' and CODE='" + reportcode + "'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() + "&rc:Parameters=False" + param;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = '" + URL + "';</script>");
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTC();
        }


    }
}
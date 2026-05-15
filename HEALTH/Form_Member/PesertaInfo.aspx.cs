using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class PesertaInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                //LB_REGNO.Text = "1325_00001P";
                ShowMASTER();
                DGR_FAMILY.CurrentPageIndex = 0;
                ShowFAMILY();
                ShowPERIOD();
                ShowTC();
                ShowTPACard();
                string warning = ShowBlacklist(LB_REGNO.Text);
                if (!string.IsNullOrEmpty(warning)) {
                    TD_WARNING.Visible = true;
                    LB_WARNING.Text = warning;
                }
            }
        }


        protected void ShowMASTER()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','1',null,null";
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
            //LB_NO_KARTU.Text = conn.GetFieldValue("NO_KARTU_TPA").ToString();
            Session["kartutpa"] = conn.GetFieldValue("NO_KARTU_TPA").ToString();
            LB_TPA_DESC.Text = conn.GetFieldValue("TPA_DESCR").ToString();
        }

        protected void ShowFAMILY()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','2',null,null";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FAMILY.DataSource = dt;
            DGR_FAMILY.DataBind();

            for (int i = 0; i < DGR_FAMILY.Items.Count; i++)
            {
                Label lb = (Label)DGR_FAMILY.Items[i].FindControl("LB_FAMILY_NAMA");
                lb.Text = "<a href='PesertaInfo.aspx?regno=" + DGR_FAMILY.Items[i].Cells[0].Text + "' target='memberheader'><span style='color: #0000FF'>" + DGR_FAMILY.Items[i].Cells[1].Text + "</span>";
            }
        }

        protected void ShowPERIOD()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','3',null,null";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                //DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, "PAKET").ToString() + " : " + conn.GetFieldValue(i, "POLIS_START_DATE").ToString() + " - " + conn.GetFieldValue(i, "POLIS_END_DATE").ToString(), conn.GetFieldValue(i, "ID").ToString()));
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            try
            {
                DDL_PERIOD.SelectedIndex = 0;
            }
            catch { }
        }

        protected void ShowTPACard() 
        {
            try
            {
                conn.QueryString = "EXEC USP_GET_APPR_STAT_TPA '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                if (dt.Rows.Count > 0)
                {
                    LB_NO_KARTU.Text = Session["kartutpa"].ToString();
                }
                else 
                {
                    LB_NO_KARTU.Text = "-";
                }
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }

        protected void BT_5_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'PesertaInfo_CLAIMHISTORY.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 1) + "';</script>");
        }

        protected void BT_5a_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'PesertaInfo_CLAIMTRX.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 1) + "';</script>");
        }

        protected void BT_5b_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'PesertaInfo_CLAIMBENEFIT.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 1) + "';</script>");
        }

        protected void ShowTC()
        {
            LB_MODE.Text = BT_5c.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = '../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 0) + "';</script>");
        }

        protected void BT_5c_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BT_5d_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = '../Form_Klien/Polis_Period_Benefit.aspx?PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 0) + "';</script>");
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void DGR_FAMILY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_FAMILY.CurrentPageIndex = e.NewPageIndex;
            ShowFAMILY();
        }

        protected string GetPeriodCode(string code, int seq)
        {
            try
            {
                conn.QueryString = "SELECT value FROM STRING_SPLIT('" + code + "', '%')";
                conn.ExecuteQuery();
                code = conn.GetFieldValue(seq, 0).ToString();
            }
            catch { }

            return code;
        }

        protected void BT_6_Click(object sender, EventArgs e)
        {
            LB_MODE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.membercontent.location.href = 'PesertaInfo_PREMIUM.aspx?REGNO=" + LB_REGNO.Text + "&PolicyPeriod=" + GetPeriodCode(DDL_PERIOD.SelectedValue, 1) + "';</script>");
        }

        protected string ShowBlacklist(string regno)
        {
            string warning = "";

            conn.QueryString = "exec SP_MASTER_MEMBER_BLACKLIST '" + regno + "'";
            conn.ExecuteQuery();

            string MAIN_INSURED_BLACKLISTED = conn.GetFieldValue("MAIN_INSURED_BLACKLISTED").ToString();
            string POLICY_HOLDER_BLACKLISTED = conn.GetFieldValue("POLICY_HOLDER_BLACKLISTED").ToString();
            string FLAG = conn.GetFieldValue("FLAG").ToString();
            string SOURCE = conn.GetFieldValue("SOURCE").ToString();

            if (!string.IsNullOrEmpty(MAIN_INSURED_BLACKLISTED) && !string.IsNullOrWhiteSpace(MAIN_INSURED_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning = MAIN_INSURED_BLACKLISTED;
                }
                else
                {
                    warning += MAIN_INSURED_BLACKLISTED + "<br/>";
                }
            }

            return warning;
        }
    }
}
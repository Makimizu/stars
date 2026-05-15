using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Drawing;

namespace HEALTH.Form_Klaim
{
    public partial class RevisionReg : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery();
            DDL_PR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_DOC_SOURCE order by CODE";
            conn.ExecuteQuery();
            DDL_DOC_SOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DOC_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string date1 = "1 jan 1980";
            string date2 = "31 dec 2090";

            if (DDL_DOC_SOURCE.SelectedValue != "")
                where = where + " and DOC_SOURCE='" + DDL_DOC_SOURCE.SelectedValue + "' ";

            if (TXT_CLAIMNO.Text.Trim() != "")
                where = where + " and CLAIM_NO like '%" + TXT_CLAIMNO.Text.Trim() + "%' ";

            if (TXT_NAMA.Text.Trim() != "")
                where = where + " and NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_PROV.Text.Trim() != "")
                where = where + " and PROVIDER like '%" + TXT_PROV.Text.Trim() + "%' ";

            if (DDL_PR.SelectedValue != "")
                where = where + " and PR='" + DDL_PR.SelectedValue + "' ";

            if (TXT_SM.Text.Trim() != "")
                where = where + " and DOC_NO like '%" + TXT_SM.Text.Trim() + "%' ";

            if (TXT_SMDATE.Text.Trim() != "")
                where = where + " and cast(TGL_DOC AS DATE) = '" + GlobalUse.GlobalDateFormat(TXT_SMDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DATE.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                if (TXT_DATE.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                where = where + " and (a.TGL_KLAIM between '" + date1 + "' and '" + date2 + "') ";
            }

            conn.QueryString = "select " +
                                "[NOREG CLAIM] = CLAIM_NO, " +
                                "[NAMA PESERTA] = NAMA, " +
                                "[PERUSAHAAN] = LEFT(COMPANY_NAME,30), " +
                                "[PROVIDER] = PROVIDER,[P/R]=PR_DESCR, " +
                                "[TIPE] = TIPE_CLAIM_DESCR, " +
                                "[TGL KLAIM] = convert(varchar(20),TGL_KLAIM,106), " +
                                "[TGL RAWAT DARI] = convert(varchar(20),TGL_RAWAT_DARI,106), " +
                                "[TGL RAWAT SAMPAI] = convert(varchar(20),TGL_RAWAT_SAMPAI,106), " +
                                "[LAST TRACK]=LAST_TRACK_DESCR  " +
                                "from V_CLM_CLAIM_MASTER a  " +
                                "where  " +
                                "LAST_TRACK in ('3','4') " + where +
                                "order by TGL_KLAIM";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btRev = (Button)DGR.Items[i].FindControl("BT_REV");
                DropDownList ddlRevRemark = (DropDownList)DGR.Items[i].FindControl("DDL_REVISION_REMARK");

                conn.QueryString = "SELECT A.CODE,B.DESCR+' - '+A.DESCR FROM PARAM_CLAIM_ADD_REASON A " +
                             "INNER JOIN dbo.PR_CLAIM_CTGRY_ADD_REASON B ON B.CODE = A.CTGRY_ADD " +
                             "WHERE A.TIPE_ADD ='R' " +
                             "ORDER BY B.DESCR,A.DESCR";
                conn.ExecuteQuery();
                ddlRevRemark.Items.Add(new ListItem("", ""));
                for (int revi = 0; revi < conn.GetRowCount(); revi++)
                    ddlRevRemark.Items.Add(new ListItem(conn.GetFieldValue(revi, 1).ToString(), conn.GetFieldValue(revi, 0).ToString()));

                btRev.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ajukan REVISI untuk " + DGR.Items[i].Cells[1].Text + " ?')){return false;};");
            }
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
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
            if (e.CommandName == "Revisi")
            {
                try
                {
                    DropDownList ddlRevRemark = (DropDownList)e.Item.FindControl("DDL_REVISION_REMARK");
                    if (ddlRevRemark.SelectedValue == "")
                    {
                        LB_RESULT.Text = "Remark tidak boleh kosong!";
                        LB_RESULT.ForeColor = Color.Red;
                        return;
                    }
                    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_REVISION " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + ddlRevRemark.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();
                    Response.Redirect("ClaimHeader.aspx?CLAIM_NO=" + conn.GetFieldValue("CLAIM_NO").ToString());
                }
                catch (Exception ex)
                {
                    LB_RESULT.Text = ex.Message;
                }
            }
        }
    }
}
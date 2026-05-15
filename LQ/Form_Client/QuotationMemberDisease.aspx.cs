using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationMemberDisease : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_MEMBERID.Text = Request.QueryString["MEMBERID"].ToString();

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER_DISEASE '" + LB_REGNO.Text + "','" + LB_MEMBERID.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbICD = (Label)DGR.Items[i].FindControl("LB_ICD");
                Label lbPROVIDER = (Label)DGR.Items[i].FindControl("LB_PROVIDER");
                Label lbDATE = (Label)DGR.Items[i].FindControl("LB_DATE");
                Label lbREMARK1 = (Label)DGR.Items[i].FindControl("LB_REMARK1");
                Label lbREMARK2 = (Label)DGR.Items[i].FindControl("LB_REMARK2");

                lbICD.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                lbPROVIDER.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                lbDATE.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                lbREMARK1.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                lbREMARK2.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (DDL_ICD.SelectedValue == "" || DDL_PROVIDER.SelectedValue == "")
                return;

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MEMBER_DISEASE_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_MEMBERID.Text + "'," +
                                    "null," +
                                    "'" + DDL_ICD.SelectedValue + "'," +
                                    "'" + DDL_PROVIDER.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + TXT_REMARK1.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + TXT_REMARK2.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            FillDGR();
        }

        protected void TXT_ICD_TextChanged(object sender, EventArgs e)
        {
            if (TXT_ICD.Text.Trim().Length < 3)
                return;

            DDL_ICD.Items.Clear();
            conn.QueryString = "select CODE, DESCR from ASKES_MIGRASI.dbo.PR_ICD where DESCR like '%" + TXT_ICD.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ICD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void TXT_PROVIDER_TextChanged(object sender, EventArgs e)
        {
            if (TXT_PROVIDER.Text.Trim().Length < 3)
                return;

            DDL_PROVIDER.Items.Clear();
            conn.QueryString = "select KODE_PROVIDER, NAMA from ASKES_MIGRASI.dbo.PROVIDER_MASTER where NAMA like '%" + TXT_PROVIDER.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVIDER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from APPLICATION_MEMBER_DISEASE " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and MEMBER_ID = '" + LB_MEMBERID.Text + "' " +
                                    "and SEQ = " + e.Item.Cells[0].Text;
                conn.ExecuteQuery();
                FillDGR();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimAddReason : System.Web.UI.Page
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
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }
                Setup();
                Show_Reason();
                FillDGR_REASON();
            }

            if (Request["__EVENTARGUMENT"] != null && Request["__EVENTARGUMENT"] == "LB_TP_DoubleClick")
            {
                try
                {
                    AddReason();
                }
                catch { }
            }
            LB_TP.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(LB_TP, "LB_TP_DoubleClick"));
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from dbo.PR_CLAIM_TIPE_ADD_REASON WHERE CODE IN ('A','E')";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void Show_Reason()
        {
            string where = "";
            if (TXT_ALASAN_CARI.Text != "")
                where = "and A.DESCR like '%" + TXT_ALASAN_CARI.Text.Trim() + "%' ";
            conn.QueryString = "select A.CODE,DESCR = B.DESCR+' - '+A.DESCR1 from PARAM_CLAIM_ADD_REASON A " +
                                "INNER JOIN dbo.PR_CLAIM_CTGRY_ADD_REASON B ON B.CODE = A.CTGRY_ADD " +
                                "where " +
                                "a.code not in (select CODE from CLAIM_ADD_REASON where CLAIM_NO='" + LB_CLAIMNO.Text + "') " +
                                "and TIPE_ADD='" + DDL_TIPE_ADD.SelectedValue + "' " +
                                where +
                                "ORDER BY B.DESCR,A.DESCR";
            conn.ExecuteQuery();
            LB_TP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                LB_TP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            Show_Reason();
        }
        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_ADD_REASON where CLAIM_NO='" + e.Item.Cells[0].Text + "' and CODE='" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }


            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtRemark = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                    conn.QueryString = "update CLAIM_ADD_REASON set " +
                                        "REMARK = '" + txtRemark.Text.Trim().Replace("'", "`") + "' " +
                                        "where " +
                                        "CLAIM_NO='" + DGR.Items[i].Cells[0].Text + "' " +
                                        "and CODE='" + DGR.Items[i].Cells[1].Text + "'";
                    conn.ExecuteNonQuery();

                }
            }

            FillDGR_REASON();
        }
        protected void DDL_TIPE_ADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show_Reason();
        }

        protected void AddReason()
        {
            try
            {
                conn.QueryString = "insert into CLAIM_ADD_REASON " +
                                    "select " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + LB_TP.SelectedValue + "'," +
                                    "''," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();
                FillDGR_REASON();
            }
            catch { }
        }

        protected void FillDGR_REASON()
        {
            conn.QueryString = "SELECT  a.CLAIM_NO, " +
                                "        TIPE = D.DESCR, " +
                                "        CATEGORY = C.DESCR, " +
                                "        a.CODE , " +
                                "        DESCR = b.DESCR1 , " +
                                "        a.REMARK " +
                                "FROM    CLAIM_ADD_REASON a " +
                                "        INNER JOIN dbo.PARAM_CLAIM_ADD_REASON b ON a.CODE = b.CODE " +
                                "        INNER JOIN dbo.PR_CLAIM_CTGRY_ADD_REASON C ON C.CODE = B.CTGRY_ADD " +
                                "        INNER JOIN dbo.PR_CLAIM_TIPE_ADD_REASON D ON D.CODE = b.TIPE_ADD " +
                                "WHERE   a.CLAIM_NO = '" + LB_CLAIMNO.Text + "' " +
                                "ORDER BY D.DESCR,C.DESCR,B.DESCR1 ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDel = (Button)DGR.Items[i].FindControl("BT_DEL");
                TextBox txtRemark = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");
                txtRemark.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
            }
        }

    }
}
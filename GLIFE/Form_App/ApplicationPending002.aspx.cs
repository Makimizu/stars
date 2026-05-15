using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationPending002 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_READONLY.Text = Request.QueryString["readonly"].ToString();

                Setup();
                FillDGRRemark();

                if (LB_READONLY.Text == "1")
                {
                    DGR_REMARK.Enabled = false;
                    TBL_REMARK.Visible = false;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PARAM_PENDING_TYPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select " +
                                "c.PIC_EMAIL, " +
                                "a.REMARK " +
                                "from APPLICATION_MASTER_PENDING a " +
                                "inner join APPLICATION_MASTER b on a.REGNO = b.REGNO " +
                                "left join V_LINK_CB_BRANCH_CORRESPONDENCE c on b.BRANCH_CODE = c.BRANCH_CODE and c.TIPE_KORESPONDEN = 'ADM' " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_EMAIL.Text = conn.GetFieldValue("PIC_EMAIL").ToString();
                TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();
            }
        }

        protected void BT_EMAIL_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "1," +
                                        "'" + LB_TYPE.Text + "'," +
                                        "'" + TXT_EMAIL.Text.Trim() + "'";
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "update APPLICATION_MASTER_PENDING set " +
                                    "REMARK = '" + TXT_REMARK.Text.Trim().Replace("'", "`") + "' " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = 1 " +
                                    "and PENDING_CODE = '" + LB_TYPE.Text + "' " +
                                    "exec SP_APPLICATION_DOCUMENT_LETTER_INSERT '" + LB_REGNO.Text + "','008'";
                conn.ExecuteNonQuery();
            }
            catch { }

            Setup();
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_03a", LB_REGNO.Text, "1", LB_TYPE.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", URL);
        }

        protected void FillDGRRemark()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_REMARK " +
                                "'" + LB_REGNO.Text + "'," +
                                "1," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();

            for (int i = 0; i < DGR_REMARK.Items.Count; i++)
            {
                TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_ADDREMARK");
                txtREMARK.Text = DGR_REMARK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_REMARK.Items.Count; i++)
                {
                    TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_ADDREMARK");

                    conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_REMARK_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "1," +
                                        "'" + LB_TYPE.Text + "'," +
                                        DGR_REMARK.Items[i].Cells[0].Text + "," +
                                        "'" + txtREMARK.Text.Trim().Replace("'","`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                }

                FillDGRRemark();
            }
        }
    }
}
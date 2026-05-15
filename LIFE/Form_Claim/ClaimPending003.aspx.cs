using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimPending003 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_READONLY.Text = Request.QueryString["readonly"].ToString();

                Setup();
                FillDGRUnselected();
                FillDGRSelected();

                if (LB_READONLY.Text == "1")
                {
                    DGR_SELECTED.ShowFooter = false;
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
                                "b.EMAIL, " +
                                "a.REMARK " +
                                "from       APPLICATION_MASTER_PENDING a " +
                                "inner join APPLICATION_ADDRESS b on a.REGNO = b.REGNO and ADDRESS_TYPE = 'COR' " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
                TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();
            }
        }

        protected void BT_EMAIL_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + LB_TYPE.Text + "'," +
                                        "'" + TXT_EMAIL.Text + "'";
                */
                conn.QueryString = "exec SP_CLAIM_PENDING_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + TXT_EMAIL.Text + "'," +
                                        "'" + TXT_REMARK.Text.Replace(Environment.NewLine, "<br>").Replace("'","`") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            //try
            //{
            conn.QueryString = "update APPLICATION_MASTER_PENDING set " +
                                "REMARK = '" + TXT_REMARK.Text.Trim().Replace("'", "`") + "' " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text + " " +
                                "and PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteNonQuery();
            //}
            //catch { }

            Setup();
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_04b", LB_REGNO.Text, LB_SEQ.Text, LB_TYPE.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", URL);
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_PENDING_DOC '" + LB_REGNO.Text + "'," + LB_SEQ.Text + ",0";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNSELECTED.DataSource = dt;
            DGR_UNSELECTED.DataBind();

            for (int i = 0; i < DGR_UNSELECTED.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_UNSELECTED.Items[i].FindControl("LBT_SELECT");
                lbCODE.Text = DGR_UNSELECTED.Items[i].Cells[1].Text;

                if (LB_READONLY.Text == "1")
                    lbCODE.Enabled = false;
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_PENDING_DOC '" + LB_REGNO.Text + "'," + LB_SEQ.Text + ",1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();

            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_SELECTED.Items[i].FindControl("LBT_DELETE");
                TextBox txtDATE = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_COMPLETEDDATE");
                lbCODE.Text = DGR_SELECTED.Items[i].Cells[1].Text;
                txtDATE.Text = DGR_SELECTED.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (LB_READONLY.Text == "1")
                {
                    lbCODE.Enabled = false;
                    txtDATE.Enabled = false;
                }
                else
                    lbCODE.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

            }
        }

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "insert into APPLICATION_MASTER_PENDING_DOC select " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + e.Item.Cells[2].Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "null,null";
                conn.ExecuteNonQuery();
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from APPLICATION_MASTER_PENDING_DOC where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = " + LB_SEQ.Text + " " +
                                    "and PENDING_CODE = '" + LB_TYPE.Text + "' " +
                                    "and DOC_GROUP = '" + e.Item.Cells[3].Text + "' " +
                                    "and DOC_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRUnselected();
                FillDGRSelected();
            }

            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
                {
                    TextBox txtDATE = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_COMPLETEDDATE");
                    string date = "null";
                    string user = "null";
                    if (txtDATE.Text.Trim() != "")
                    {
                        date = "'" + GlobalUse.GlobalDateFormat(txtDATE.Text.Trim(), "d/M/yyyy") + "'";
                        user = "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    }

                    try
                    {
                        conn.QueryString = "update APPLICATION_MASTER_PENDING_DOC set " +
                                            "COMPLETEDBY = " + user + "," +
                                            "COMPLETEDDATE = " + date + " " +
                                            "where " +
                                            "REGNO = '" + LB_REGNO.Text + "' " +
                                            "and SEQ = " + LB_SEQ.Text + " " +
                                            "and PENDING_CODE = '" + LB_TYPE.Text + "' " +
                                            "and DOC_GROUP = '" + DGR_SELECTED.Items[i].Cells[3].Text + "' " +
                                            "and DOC_CODE = '" + DGR_SELECTED.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGRSelected();
            }
        }
    }
}
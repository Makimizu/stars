using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementPending005 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        private string username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                Setup();
                FillDGRUnselected();
                FillDGRSelected();
                username = string.IsNullOrEmpty(Session["s"].ToString()) ? Session["username"].ToString() : GlobalUse.GetSession(Session["s"].ToString());
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PARAM_PENDING_TYPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select " +
                                "PIC_EMAIL  = c.EMAIL, " +
                                "REMARK     = a.REMARK " +
                                "from       APPLICATION_MASTER_PENDING a " +
                                "inner join APPLICATION_ADDRESS c on c.REGNO = a.REGNO AND c.ADDRESS_TYPE = 'COR'  " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = '" + LB_SEQ.Text + "' " +
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
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PENDING_EMAIL " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + LB_TYPE.Text + "'," +
                                        "'" + TXT_EMAIL.Text.Trim() + "'";
                conn.ExecuteNonQuery();
                string frameScript = "<script language='javascript'> alert('Email Terkirim!');</script>";
                Response.Write(frameScript);
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
                                "and SEQ = '" + LB_SEQ.Text + "' " +
                                "and PENDING_CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteNonQuery();
            //}
            //catch { }

            Setup();
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_06", LB_REGNO.Text, LB_SEQ.Text, LB_TYPE.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", URL);
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PENDING_DOC '" + LB_REGNO.Text + "','" + LB_SEQ.Text + "',0";
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
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PENDING_DOC '" + LB_REGNO.Text + "','" + LB_SEQ.Text + "',1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
            TXT_REMARK.Text = "Rincian Tunda karena" + System.Environment.NewLine;
            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_SELECTED.Items[i].FindControl("LBT_DELETE");
                TextBox txtDATE = (TextBox)DGR_SELECTED.Items[i].FindControl("TXT_COMPLETEDDATE");
                lbCODE.Text = DGR_SELECTED.Items[i].Cells[1].Text;
                txtDATE.Text = DGR_SELECTED.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                lbCODE.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
                TXT_REMARK.Text = TXT_REMARK.Text + DGR_SELECTED.Items[i].Cells[1].Text + System.Environment.NewLine;
            }
        }

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "insert into APPLICATION_MASTER_PENDING_DOC select " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
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
                                    "and SEQ = '" + LB_SEQ.Text + "' " +
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
                                            "and SEQ = '" + LB_SEQ.Text + "' " +
                                            "and PENDING_CODE = '" + LB_TYPE.Text + "' " +
                                            "and DOC_GROUP = '" + DGR_SELECTED.Items[i].Cells[3].Text + "' " +
                                            "and DOC_CODE = '" + DGR_SELECTED.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                
                string warning = ShowBlacklist(LB_REGNO.Text);
                if (warning != "")
                {
                    var sValue = Session["s"] as string;  
                    username = string.IsNullOrEmpty(sValue) ? (Session["username"] as string) : GlobalUse.GetSession(sValue);
                    conn.QueryString = "EXEC SP_SEND_EMAIL_POS_NASABAH_INDIVIDU_BERESIKO_TINGGI2 '" + LB_REGNO.Text + "-" + username + "'";
                    conn.ExecuteNonQuery();
                }

                FillDGRSelected();
            }
        }

        private string ShowBlacklist(string regno)
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER '" + regno + "'";
            conn.ExecuteQuery();

            string status1 = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string status2 = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string status3 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status4 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status5 = conn.GetFieldValue("RISK_COUNTRY").ToString();
            string status6 = conn.GetFieldValue("RISK_JOB").ToString();
            string warning = "";

            if (!string.IsNullOrEmpty(status1) && !string.IsNullOrWhiteSpace(status1))
            {
                warning = status1;
            }

            if (!string.IsNullOrEmpty(status2) && !string.IsNullOrWhiteSpace(status2))
            {
                if (warning == "")
                {
                    warning = status2;
                }
                else
                {
                    warning += "<br>" + status2;
                }
            }

            if (!string.IsNullOrEmpty(status3) && !string.IsNullOrWhiteSpace(status3))
            {
                if (warning == "")
                {
                    warning = status3;
                }
                else
                {
                    warning += "<br>" + status3;
                }
            }
            else if (!string.IsNullOrEmpty(status4) && !string.IsNullOrWhiteSpace(status4))
            {
                if (warning == "")
                {
                    warning = status4;
                }
                else
                {
                    warning += "<br>" + status4;
                }
            }

            if (!string.IsNullOrEmpty(status5) && !string.IsNullOrWhiteSpace(status5))
            {
                if (warning == "")
                {
                    warning = status5;
                }
                else
                {
                    warning += "<br>" + status5;
                }
            }

            if (!string.IsNullOrEmpty(status6) && !string.IsNullOrWhiteSpace(status6))
            {
                if (warning == "")
                {
                    warning = status6;
                }
                else
                {
                    warning += "<br>" + status6;
                }
            }

            return warning;
        }
    }
}
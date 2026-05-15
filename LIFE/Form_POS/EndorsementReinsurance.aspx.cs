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
    public partial class EndorsementReinsurance : System.Web.UI.Page
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
                FillDGR();
                //ShowAlert();
                //CheckTrack();
            }
        }

        protected void ShowAlert()
        {
            LB_FACULTATIVE_ALERT.Text = "";
            conn.QueryString = "exec SP_APPLICATION_FACULTATIVE_FLAG '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_FACULTATIVE_ALERT.Text = conn.GetFieldValue("ALERT").ToString();
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") > 2)
            {
                BT_SAVE.Visible = false;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_AGREEMENT");
                    TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                    TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");

                    ddl.Enabled = false;
                    txtAMOUNT.Enabled = false;
                    txtRATE.Enabled = false;
                }
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_QUOT_REINSURANCE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select a.ID, a.DESCR from REINSURANCE.dbo.TC_MASTER a inner join REINSURANCE.dbo.TC_MASTER_USAGE b on a.ID = b.TC_ID and b.APP_ID = 'LF' order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_AGREEMENT");
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtOR = (TextBox)DGR.Items[i].FindControl("TXT_OR");
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                Label lbOR = (Label)DGR.Items[i].FindControl("LB_OR");
                Label lbQS = (Label)DGR.Items[i].FindControl("LB_QS");


                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtAMOUNT.Text = DGR.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "");
                txtRATE.Text = DGR.Items[i].Cells[4].Text.Trim().Replace("&nbsp;", "");
                txtOR.Text = DGR.Items[i].Cells[5].Text.Trim().Replace("&nbsp;", "");

                lbOR.Text = DGR.Items[i].Cells[6].Text.Trim().Replace("&nbsp;", "");
                lbQS.Text = DGR.Items[i].Cells[7].Text.Trim().Replace("&nbsp;", "");

                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "");
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_AGREEMENT");
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");

                string agreement, amount, rate;
                agreement = amount = rate = "null";

                if (ddl.SelectedValue != "")
                {
                    agreement = "'" + ddl.SelectedValue + "'";

                    if (txtAMOUNT.Text.Trim().Replace(",", "") != "")
                        amount = txtAMOUNT.Text.Trim().Replace(",", "");

                    if (txtRATE.Text.Trim().Replace(",", "") != "")
                        rate = txtRATE.Text.Trim().Replace(",", "");
                }

                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_QUOT_REINSURANCE_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR.Items[i].Cells[1].Text + "'," +
                                        agreement + "," +
                                        amount + "," +
                                        rate + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR();
        }
    }
}
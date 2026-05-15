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
    public partial class ApplicationPremium : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                FillDGR();
                LoadGTLR();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select SEQ = MAX(SEQ) from TRACK_DATA where TIPE_CODE='UW' and OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            //if (conn.GetRowCount() == 0)
            //    result = false;
            if (int.Parse(conn.GetFieldValue("SEQ").ToString()) < 3)
                result = false;

            return result;
        }

        protected void FillDGR()
        {
            if (bDone || Request.QueryString["readonly"] == "1")
            {
                BT_SAVE.Visible = false;
            }

            conn.QueryString = "exec SP_APPLICATION_PREMIUM_COMPONENT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                Label lbPRINCIPAL = (Label)DGR.Items[i].FindControl("LB_PRINCIPAL");
                Label lbMULTIPLY = (Label)DGR.Items[i].FindControl("LB_MULTIPLY");
                Label lbAMOUNT = (Label)DGR.Items[i].FindControl("LB_AMOUNT");
                TextBox txtREMARK = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                lbAMOUNT.Text = DGR.Items[i].Cells[4].Text;
                txtREMARK.Text = DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "");

                switch (DGR.Items[i].Cells[2].Text)
                {
                    case "0":
                        txtRATE.Visible = false;
                        lbPRINCIPAL.Visible = false;
                        break;
                    default:
                        lbPRINCIPAL.Text = DGR.Items[i].Cells[2].Text;
                        txtRATE.Text = DGR.Items[i].Cells[3].Text;
                        lbMULTIPLY.Text = " X ";
                        break;
                }

                switch (DGR.Items[i].Cells[5].Text)
                {
                    case "0":
                        DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                        lbAMOUNT.ForeColor = System.Drawing.Color.Red;
                        break;
                    case "1":
                        lbAMOUNT.ForeColor = System.Drawing.Color.Green;
                        break;
                }

                if (DGR.Items[i].Cells[7].Text == "1")
                {
                    txtRATE.Enabled = false;
                    txtRATE.BackColor = System.Drawing.Color.Gainsboro;
                }
                else
                {
                    txtREMARK.Visible = true;
                }

                if (i == DGR.Items.Count - 1)
                {
                    DGR.Items[i].Font.Bold = true;
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                }

                if (bDone || Request.QueryString["readonly"] == "1")
                {
                    txtRATE.Enabled = false;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                Label lbPRINCIPAL = (Label)DGR.Items[i].FindControl("LB_PRINCIPAL");
                TextBox txtREMARK = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                if (txtRATE.Visible)
                {
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_PREMIUM_COMPONENT_INSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + txtRATE.Text.Trim().Replace(",", "") + "'," +
                                            "'" + txtREMARK.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                /*
                else if (txtAMOUNT.Visible)
                {
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_PREMIUM_COMPONENT_INSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                            "'" + txtREMARK.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                */
            }

            FillDGR();
        }

        private void LoadGTLR()
        {
            conn.QueryString = "select REGNO from APPLICATION_MASTER a " +
                                "inner join POLICY b on a.POLICY_ID=b.ID and b.PRODUCT_GROUP='GTLR' " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                TR_CYCLE.Visible = false;
                return;
            }

            conn.QueryString = "select URLAPP = URLAPP + '&REGNO=" + LB_REGNO.Text + "' from V_LINK_SC_REPORT_LIST where REPORT_NAME='RPT_APPLICATION_PAYMENT_CYCLE'";
            conn.ExecuteQuery();
            IF.Src = conn.GetFieldValue("URLAPP").ToString();
        }
    }
}
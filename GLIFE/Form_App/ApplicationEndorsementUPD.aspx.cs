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
    public partial class ApplicationEndorsementUPD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = "UPD";
                bDone = TrackDone();
                FillDGR();
                FillDGRARAP();
                LoadPaymentAcc();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_ENDORSEMENT_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "' and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void LoadPaymentAcc()
        {
            string disable = "0";
            if (bDone)
            {
                disable = "1";
            }
            I1.Visible = true;
            I1.Src = "../Form_Tools/PaymentAcc.aspx?REGNO=" + LB_REGNO.Text + "-" + LB_SEQ.Text + "-UPD&SEQ=1&CODE=POS&DISABLE=" + disable;
        }

        protected void FillDGR()
        {
            if (bDone)
            {
                BT_SAVE.Visible = false;
            }

            conn.QueryString = "exec SP_APPLICATION_ALTERATION_DETAIL " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtNEWVAL = (TextBox)DGR.Items[i].FindControl("TXT_NEWVAL");
                TextBox txtNEWDATEVAL = (TextBox)DGR.Items[i].FindControl("TXT_NEWDATEVAL");

                if (DGR.Items[i].Cells[2].Text == "DATE")
                {
                    txtNEWVAL.Visible = false;
                    txtNEWDATEVAL.Visible = true;
                    txtNEWDATEVAL.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");

                }
                else
                {
                    txtNEWVAL.Visible = true;
                    txtNEWDATEVAL.Visible = false;
                    txtNEWVAL.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");

                    if (DGR.Items[i].Cells[2].Text == "FLO")
                    {
                        txtNEWVAL.Width = 100;
                        txtNEWVAL.Attributes.Add("style", "text-align:right;");

                        try
                        {
                            conn.QueryString = "select AMOUNT = replace(convert(varchar(100), convert(money," + txtNEWVAL.Text.Trim().Replace(",", "") + "),1), '.00','')";
                            conn.ExecuteQuery();
                            txtNEWVAL.Text = conn.GetFieldValue("AMOUNT").ToString();
                        }
                        catch { }
                    }
                    else
                    {
                        txtNEWVAL.Attributes.Add("style", "width:100%;");
                    }

                }

                if (bDone)
                {
                    txtNEWVAL.ReadOnly = true;
                    txtNEWDATEVAL.ReadOnly = true;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtNEWVAL = (TextBox)DGR.Items[i].FindControl("TXT_NEWVAL");
                TextBox txtNEWDATEVAL = (TextBox)DGR.Items[i].FindControl("TXT_NEWDATEVAL");

                string val = txtNEWVAL.Text.Trim();
                if (txtNEWDATEVAL.Visible)
                    val = txtNEWDATEVAL.Text.Trim();

                conn.QueryString = "exec SP_APPLICATION_ALTERATION_DETAIL_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }


            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_ARAP_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();


            Response.Redirect("ApplicationEndorsementUPD.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text);
        }

        protected void FillDGRARAP()
        {
            conn.QueryString = "select " +
                                "ID, " +
                                "DESCR, " +
                                "AMOUNT = replace(convert(varchar(100), convert(money,AMOUNT),1),'.00','') " +
                                "from APPLICATION_ENDORSEMENT_ARAP a where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text + " " +
                                "and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "' " +
                                "and MODE = 'AR' order by a.AMOUNT desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AR.DataSource = dt;
            DGR_AR.DataBind();

            for (int i = 0; i < DGR_AR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_AR.Items[i].FindControl("TXT_ARAMT");
                Button bt = (Button)DGR_AR.Items[i].FindControl("BT_ARSAVE");

                txt.Text = DGR_AR.Items[i].Cells[2].Text;

                if (bDone)
                {
                    txt.ReadOnly = true;
                    bt.Visible = false;
                }
            }


            conn.QueryString = "select " +
                                "ID, " +
                                "DESCR, " +
                                "AMOUNT = replace(convert(varchar(100), convert(money,AMOUNT),1),'.00','') " +
                                "from APPLICATION_ENDORSEMENT_ARAP where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text + " " +
                                "and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "' " +
                                "and MODE = 'AP' order by 2 desc";
            conn.ExecuteQuery();

            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AP.DataSource = dt;
            DGR_AP.DataBind();

            for (int i = 0; i < DGR_AP.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_AP.Items[i].FindControl("TXT_APAMT");
                Button bt = (Button)DGR_AP.Items[i].FindControl("BT_APSAVE");

                txt.Text = DGR_AP.Items[i].Cells[2].Text;

                if (bDone)
                {
                    txt.ReadOnly = true;
                    bt.Visible = false;
                }
            }

            conn.QueryString = "select " +
                                "MODE, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money, SUM(AMOUNT)),1),'.00','') " +
                                "from APPLICATION_ENDORSEMENT_ARAP " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text + " " +
                                "and ENDORSEMENT_TYPE = '" + LB_TYPE.Text + "' " +
                                "group by MODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                switch (conn.GetFieldValue(i, 0).ToString())
                {
                    case "AR": LB_AR.Text = conn.GetFieldValue(i, 1).ToString(); break;
                    case "AP": LB_AP.Text = conn.GetFieldValue(i, 1).ToString(); break;
                }
            }
        }

        protected void DGR_AR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_ARAMT");

                try
                {
                    conn.QueryString = "update APPLICATION_ENDORSEMENT_ARAP set " +
                                        "AMOUNT = " + txt.Text.Trim().Replace(",", "") + "," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRARAP();
            }
        }

        protected void DGR_AP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_APAMT");

                try
                {
                    conn.QueryString = "update APPLICATION_ENDORSEMENT_ARAP set " +
                                        "AMOUNT = " + txt.Text.Trim().Replace(",", "") + "," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRARAP();
            }
        }
    }
}
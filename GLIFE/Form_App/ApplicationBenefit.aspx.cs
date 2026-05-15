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
    public partial class ApplicationBenefit : System.Web.UI.Page
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
                LoadBenefit();
                LoadLoading();
                LoadAccumulation();
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

        protected void LoadBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();


            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                Button btRATE = (Button)DGR_BENEFIT.Items[i].FindControl("BT_RATESAVE");
                TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_RATE");
                txtRATE.Text = DGR_BENEFIT.Items[i].Cells[4].Text;


                if (DGR_BENEFIT.Items[i].Cells[0].Text.Replace("&nbsp;", "") == "")
                {
                    btRATE.Visible = false;
                    txtRATE.Visible = false;

                    DGR_BENEFIT.Items[i].Cells[5].Text = DGR_BENEFIT.Items[i].Cells[4].Text + " &permil;";
                    DGR_BENEFIT.Items[i].BackColor = System.Drawing.Color.Yellow;
                    DGR_BENEFIT.Items[i].Font.Bold = true;
                }
                else
                {
                    if (bDone || Request.QueryString["readonly"] == "1")
                    {
                        btRATE.Visible = false;
                        txtRATE.Visible = false;
                        DGR_BENEFIT.Items[i].Cells[5].Text = DGR_BENEFIT.Items[i].Cells[4].Text + " &permil;";
                    }
                }
            }

            /*
            conn.QueryString = "select " +
                                "b.PRODUCT_GROUP " +
                                "from APPLICATION_MASTER a " +
                                "inner join POLICY b on a.POLICY_ID = b.ID " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("PRODUCT_GROUP").ToString() == "SP")
            {
                DGR_BENEFIT.Columns[4].Visible = false;
                DGR_BENEFIT.Columns[5].Visible = false;
                DGR_BENEFIT.Columns[6].Visible = false;
            }
            */
        }

        protected void LoadLoading()
        {
            conn.QueryString = "select " +
                                "a.POLICY_ID " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	POLICY b on a.POLICY_ID = b.ID and b.PRODUCT_GROUP in ('SP','GTLR') " +
                                "where " +
                                "a.REGNO		= '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                string POLICY_ID = conn.GetFieldValue(0, 0).ToString();
                conn.QueryString = "select URL = URLAPP + '&POLICY_ID=" + POLICY_ID + "' from V_LINK_SC_REPORT_LIST where REPORT_NAME = 'RPT_POLICY_LOADING_PERIODIC'";
                conn.ExecuteQuery();
                IF_LOADING.Src = conn.GetFieldValue(0, 0).ToString();

                TR_LOADING_PERIODIC.Visible = true;
                TR_LOADING.Visible = false;
            }
            else
            {
                conn.QueryString = "exec SP_APPLICATION_LOADING '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_LOADING.DataSource = dt;
                DGR_LOADING.DataBind();

                for (int i = 0; i < DGR_LOADING.Items.Count; i++)
                {
                    Button btRATE = (Button)DGR_LOADING.Items[i].FindControl("BT_RATESAVE");
                    TextBox txtRATE = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_RATE");
                    txtRATE.Text = DGR_LOADING.Items[i].Cells[2].Text;

                    string CODE = DGR_LOADING.Items[i].Cells[0].Text.Replace("&nbsp;", "");

                    if (CODE == "" || CODE == "REINS" || CODE == "COINS")
                    {
                        btRATE.Visible = false;
                        txtRATE.Visible = false;

                        DGR_LOADING.Items[i].Cells[3].Text = DGR_LOADING.Items[i].Cells[2].Text + " %";

                        if (CODE == "")
                        {
                            DGR_LOADING.Items[i].BackColor = System.Drawing.Color.Yellow;
                            DGR_LOADING.Items[i].Font.Bold = true;
                        }
                    }
                    else
                    {
                        if (bDone || Request.QueryString["readonly"] == "1")
                        {
                            btRATE.Visible = false;
                            txtRATE.Visible = false;
                            DGR_LOADING.Items[i].Cells[3].Text = DGR_LOADING.Items[i].Cells[2].Text + " %";
                        }
                    }
                }

                TR_LOADING_PERIODIC.Visible = false;
                TR_LOADING.Visible = true;
            }
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                try
                {
                    TextBox txtRATE = (TextBox)e.Item.FindControl("TXT_RATE");
                    conn.QueryString = "update APPLICATION_BENEFIT set " +
                                        "RATE = " + txtRATE.Text.Trim().Replace(",", "") + " " +
                                        "where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and BENEFIT_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    LoadBenefit();
                    LoadLoading();
                }
                catch { }
            }
        }


        protected void DGR_LOADING_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                try
                {
                    TextBox txtRATE = (TextBox)e.Item.FindControl("TXT_RATE");
                    conn.QueryString = "update APPLICATION_LOADING set " +
                                        "VAL = " + txtRATE.Text.Trim().Replace(",", "") + " " +
                                        "where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and LOADING_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    LoadLoading();
                }
                catch { }
            }
        }

        protected void LoadAccumulation()
        {
            conn.QueryString = "exec SP_APPLICATION_SUMINS_ACCUMULATION '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ACCU.DataSource = dt;
            DGR_ACCU.DataBind();

            for (int i = 0; i < DGR_ACCU.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_ACCU.Items[i].FindControl("LBT_REGNO");
                Button btDEL = (Button)DGR_ACCU.Items[i].FindControl("BT_DEL");
                lbt.Text = DGR_ACCU.Items[i].Cells[0].Text;

                if (DGR_ACCU.Items[i].Cells[0].Text == LB_REGNO.Text)
                {
                    DGR_ACCU.Items[i].BackColor = System.Drawing.Color.LawnGreen;
                    btDEL.Visible = false;
                }
            }

            if (bDone || Request.QueryString["readonly"] == "1")
                DGR_ACCU.Columns[8].Visible = false;
        }

        protected void DGR_ACCU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.parent.location.href = 'ApplicationFrame.aspx?ID=" + e.Item.Cells[0].Text + "';</script>");
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_APPLICATION_SUMINS_ACCUMULATION_DELETE '" + LB_REGNO.Text + "','" + e.Item.Cells[0].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                LoadAccumulation();
            }
        }

        protected void DGR_ACCU_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                    "ACCUMULATION = replace(convert(varchar(100),convert(money,SUM(a.SUMINS)),1),'.00',''), " +
                                    "OWNRETENTION = replace(convert(varchar(100),convert(money,SUM(a.OWNRETENTION)),1),'.00','') " +
                                    "from APPLICATION_SUMINS_ACCUMULATION a " +
                                    "where a.REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[1].Text = "TOTAL";
                e.Item.Cells[6].Text = conn.GetFieldValue("ACCUMULATION").ToString();
                e.Item.Cells[7].Text = conn.GetFieldValue("OWNRETENTION").ToString();
            }
        }
    }
}
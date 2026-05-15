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
    public partial class QuotationBenefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();

                try
                {
                    FillDGRMember();
                    ShowInsuredPerson(0);
                }
                catch { }
            }
        }

        protected void FillDGRMember()
        {
            conn.QueryString = "exec SP_APPLICATION_INSURED_MEMBER '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            int member = conn.GetRowCount();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INSUREDLIST.DataSource = dt;
            DGR_INSUREDLIST.DataBind();

            for (int i = 0; i < DGR_INSUREDLIST.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_INSUREDLIST.Items[i].FindControl("LBT_MEMBER");
                Label lb2 = (Label)DGR_INSUREDLIST.Items[i].FindControl("LB_MEMBER");

                lb.Text = "<B>" + DGR_INSUREDLIST.Items[i].Cells[2].Text + "</B>";
                lb2.Text = DGR_INSUREDLIST.Items[i].Cells[1].Text + " (" + DGR_INSUREDLIST.Items[i].Cells[4].Text + " " + DGR_INSUREDLIST.Items[i].Cells[3].Text + " yold)";
            }

            if (member <= 1)
            {
                TD_MEMBERLIST.Visible = false;
            }
        }

        protected void FillDGRBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT '" + LB_REGNO.Text + "','" + LB_MEMBERID.Text + "', 0";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                Label lbBASIC = (Label)DGR_BENEFIT.Items[i].FindControl("LB_BASIC");
                Label lbBENEFIT = (Label)DGR_BENEFIT.Items[i].FindControl("LB_BENEFIT_DESCR");
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_BENEFIT");
                TextBox txtSUMINSURED = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_SUMINS");
                DropDownList ddlSUMINSURED = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_SUMINS");
                TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_RATE");
                TextBox txtENDDATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_ENDDATE");
                Label lbMIN = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MIN");
                Label lbMAX = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MAX");
                System.Web.UI.HtmlControls.HtmlTableRow trSUMINS = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_SUMINS");
                System.Web.UI.HtmlControls.HtmlTableRow trSUMINSLIMIT = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_SUMINS_LIMIT");
                System.Web.UI.HtmlControls.HtmlTableRow trENDDATE = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_ENDDATE");


                lbBENEFIT.Text = DGR_BENEFIT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtSUMINSURED.Text = DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtENDDATE.Text = DGR_BENEFIT.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                lbMIN.Text = DGR_BENEFIT.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                lbMAX.Text = DGR_BENEFIT.Items[i].Cells[10].Text.Replace("&nbsp;", "");


                if (DGR_BENEFIT.Items[i].Cells[7].Text == "1")
                    cb.Checked = true;

                if (DGR_BENEFIT.Items[i].Cells[8].Text == "1")
                {
                    lbBENEFIT.Font.Bold = true;
                    DGR_BENEFIT.Items[i].BackColor = System.Drawing.Color.FromArgb(222, 243, 222);
                }
                else
                {
                    lbBASIC.Text = "RIDER";
                }

                if (DGR_BENEFIT.Items[i].Cells[11].Text == "1")
                {
                    cb.Enabled = false;
                }

                txtSUMINSURED.Visible = true;
                trSUMINSLIMIT.Visible = true;

                if (DGR_BENEFIT.Items[i].Cells[12].Text.Replace("&nbsp;", "") != "")
                {
                    conn1.QueryString = DGR_BENEFIT.Items[i].Cells[12].Text.Replace("&nbsp;", "");
                    conn1.ExecuteQuery();

                    for (int j = 0; j < conn1.GetRowCount(); j++)
                    {
                        ddlSUMINSURED.Items.Add(new ListItem(conn1.GetFieldValue(j, 0).ToString(), conn1.GetFieldValue(j, 1).ToString()));
                    }

                    ddlSUMINSURED.Visible = true;
                    txtSUMINSURED.Visible = false;
                    trSUMINSLIMIT.Visible = false;

                    try
                    {
                        ddlSUMINSURED.SelectedValue = txtSUMINSURED.Text.Replace(",", "");
                    }
                    catch { }
                }
            }
        }

        protected void FillDGRPayor()
        {
            TBL_PAYOR.Visible = true;
            conn.QueryString = "exec SP_APPLICATION_BENEFIT '" + LB_REGNO.Text + "','" + LB_MEMBERID.Text + "', 1";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                TBL_PAYOR.Visible = false;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PAYOR.DataSource = dt;
            DGR_PAYOR.DataBind();

            for (int i = 0; i < DGR_PAYOR.Items.Count; i++)
            {
                Label lbBASIC = (Label)DGR_PAYOR.Items[i].FindControl("LB_BASIC");
                Label lbBENEFIT = (Label)DGR_PAYOR.Items[i].FindControl("LB_BENEFIT_DESCR");
                CheckBox cb = (CheckBox)DGR_PAYOR.Items[i].FindControl("CB_BENEFIT");
                DropDownList ddlMAXAGE = (DropDownList)DGR_PAYOR.Items[i].FindControl("DDL_MAXAGE");
                Label lbMAXAGE = (Label)DGR_PAYOR.Items[i].FindControl("LB_MAXAGE");
                Label lbMAXPAYOR = (Label)DGR_PAYOR.Items[i].FindControl("LB_MAX_PAYOR");

                lbBENEFIT.Text = DGR_PAYOR.Items[i].Cells[2].Text.Replace("&nbsp;", "");


                if (DGR_PAYOR.Items[i].Cells[4].Text == "1")
                    cb.Checked = true;

                if (DGR_PAYOR.Items[i].Cells[5].Text == "1")
                {
                    lbBENEFIT.Font.Bold = true;
                    DGR_PAYOR.Items[i].BackColor = System.Drawing.Color.FromArgb(222, 243, 222);
                }
                else
                {
                    lbBASIC.Text = "RIDER";
                }

                if (DGR_PAYOR.Items[i].Cells[6].Text.Replace("&nbsp;", "") == "")
                {
                    lbMAXPAYOR.Text = "MANFAAT PAYOR SEPANJANG MASA PEMBAYARAN PREMI";
                    ddlMAXAGE.Visible = false;
                    lbMAXAGE.Visible = false;
                }
                else
                {
                    lbMAXPAYOR.Text = "MAXIMUM USIA PAYOR";
                    conn.QueryString = DGR_PAYOR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int py = 0; py < conn.GetRowCount(); py++)
                        ddlMAXAGE.Items.Add(new ListItem(conn.GetFieldValue(py, 0).ToString(), conn.GetFieldValue(py, 0).ToString()));

                    try
                    {
                        ddlMAXAGE.SelectedValue = DGR_PAYOR.Items[i].Cells[7].Text;
                    }
                    catch { }
                }
            }
        }

        protected void FillDGRBenefitHealthPlan()
        {
            TBL_HEALTH.Visible = true;
            conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN '" + LB_REGNO.Text + "','" + LB_MEMBERID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                TBL_HEALTH.Visible = false;
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_HEALTHPLAN.DataSource = dt;
            DGR_BENEFIT_HEALTHPLAN.DataBind();

            for (int i = 0; i < DGR_BENEFIT_HEALTHPLAN.Items.Count; i++)
            {
                DropDownList ddlPLAN = (DropDownList)DGR_BENEFIT_HEALTHPLAN.Items[i].FindControl("DDL_PLAN");
                ddlPLAN.Items.Add("");
                conn.QueryString = DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[2].Text;
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlPLAN.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddlPLAN.SelectedValue = DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[1].Text;
                }
                catch { }
            }
        }

        protected void BT_SAVE_BENEFIT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_BENEFIT");
                TextBox txtSUMINSURED = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_SUMINS");
                TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_RATE");
                TextBox txtENDDATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_ENDDATE");
                DropDownList ddlSUMINSURED = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_SUMINS");

                if (txtENDDATE.Text.Trim() == "")
                    continue;

                try
                {
                    string minSI = "null";
                    string maxSI = "null";
                    string SI = "0";

                    if (DGR_BENEFIT.Items[i].Cells[9].Text.Replace(",", "").Replace("&nbsp;", "").Trim() != "")
                        minSI = DGR_BENEFIT.Items[i].Cells[9].Text.Replace(",", "").Replace("&nbsp;", "").Trim();
                    if (DGR_BENEFIT.Items[i].Cells[10].Text.Replace(",", "").Replace("&nbsp;", "").Trim() != "")
                        maxSI = DGR_BENEFIT.Items[i].Cells[10].Text.Replace(",", "").Replace("&nbsp;", "").Trim();
                    if (txtSUMINSURED.Text.Trim().Replace(",", "") != "")
                        SI = txtSUMINSURED.Text.Trim().Replace(",", "");
                    if (ddlSUMINSURED.SelectedValue.Trim().Replace(",", "") != "")
                        SI = ddlSUMINSURED.SelectedValue.Trim().Replace(",", "");

                    if (!cb.Checked)
                        SI = "0";

                    conn.QueryString = "exec SP_APPLICATION_BENEFIT_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[6].Text + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[1].Text + "'," +
                                        SI + "," +
                                        minSI + "," +
                                        maxSI + "," +
                                        "'" + GlobalUse.GlobalDateFormat(txtENDDATE.Text.Trim(), "d/M/yyyy") + "', " +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            FillDGRBenefit();


            if (TBL_PAYOR.Visible)
            {
                for (int i = 0; i < DGR_PAYOR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PAYOR.Items[i].FindControl("CB_BENEFIT");
                    DropDownList ddl = (DropDownList)DGR_PAYOR.Items[i].FindControl("DDL_MAXAGE");

                    string tenor = "0";
                    if (ddl.SelectedValue != "")
                        tenor = ddl.SelectedValue;
                    else
                        tenor = "-1";

                    if (!cb.Checked)
                        tenor = "0";

                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_BENEFIT_PAYOR_UPSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR_PAYOR.Items[i].Cells[3].Text + "'," +
                                            "'" + DGR_PAYOR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR_PAYOR.Items[i].Cells[1].Text + "'," +
                                            tenor + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                FillDGRPayor();
            }

            if (TBL_HEALTH.Visible)
            {
                for (int i = 0; i < DGR_BENEFIT_HEALTHPLAN.Items.Count; i++)
                {
                    string PLAN = "null";
                    DropDownList ddlPLAN = (DropDownList)DGR_BENEFIT_HEALTHPLAN.Items[i].FindControl("DDL_PLAN");
                    if (ddlPLAN.SelectedValue != "")
                        PLAN = "'" + ddlPLAN.SelectedValue + "'";

                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN_UPSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[3].Text + "'," +
                                            PLAN + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                FillDGRBenefitHealthPlan();
            }


            conn.QueryString = "exec SP_APPLICATION_PREMIUM_COI_UPDATE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();


            conn.QueryString = "select REGNO from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "' and MEMBER_ID = '" + LB_MEMBERID.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.parent.appheader.location.href = 'ApplicationHeader.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void DGR_INSUREDLIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LinkButton lb0 = (LinkButton)e.Item.FindControl("LBT_MEMBER");
                for (int i = 0; i < DGR_INSUREDLIST.Items.Count; i++)
                {
                    LinkButton lb = (LinkButton)DGR_INSUREDLIST.Items[i].FindControl("LBT_MEMBER");
                    if (lb0 == lb)
                    {
                        ShowInsuredPerson(i);
                    }
                }
            }
        }

        protected void ShowInsuredPerson(int index)
        {
            LB_MEMBERID.Text = DGR_INSUREDLIST.Items[index].Cells[0].Text;
            LB_MEMBERNAME.Text = DGR_INSUREDLIST.Items[index].Cells[2].Text;
            LB_MEMBERTYPE.Text = DGR_INSUREDLIST.Items[index].Cells[1].Text;
            LB_MEMBERAGE.Text = DGR_INSUREDLIST.Items[index].Cells[4].Text + " - " + DGR_INSUREDLIST.Items[index].Cells[3].Text;

            FillDGRBenefit();
            FillDGRPayor();
            FillDGRBenefitHealthPlan();
        }
    }
}
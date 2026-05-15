using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace UWBOX.Form_TC
{
    public partial class TC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_CODE.Text = Request.QueryString["CODE"];
                FillDGRTrack();
                Setup();
                ShowBenefitDetails();

                if (TXT_CODE.Text != "")
                    LoadRecord(TXT_CODE.Text);
            }
        }

        protected void Setup()
        {
            BT_APPROVE.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO APPROVE ?')){return false;};");

            conn.QueryString = "select CODE,DESCR from PARAM_PRODUCT_GROUP";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCTGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE = '',DESCR = '' union all select CODE = LICENCENO,DESCR = LICENCENAME from PRODUCT_LICENCE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LICENCENO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "exec SP_PARAM_TC_ITEMS '" + TXT_CODE.Text + "',null";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_GROUP.DataSource = dt;
            DGR_GROUP.DataBind();

            for (int i = 0; i < DGR_GROUP.Items.Count; i++)
            {
                Label lblGROUP = (Label)DGR_GROUP.Items[i].FindControl("LBL_GROUP");
                DataGrid dgr = (DataGrid)DGR_GROUP.Items[i].FindControl("DGR_ITEM");

                lblGROUP.Text = DGR_GROUP.Items[i].Cells[1].Text;

                conn.QueryString = "exec SP_PARAM_TC_ITEMS '" + TXT_CODE.Text + "','" + DGR_GROUP.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                dgr.DataSource = conn.GetDataTable().Copy();
                dgr.DataBind();

                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    CheckBox cb = (CheckBox)dgr.Items[j].FindControl("CB");
                    DropDownList ddl = (DropDownList)dgr.Items[j].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)dgr.Items[j].FindControl("TXT_VAL");

                    if (dgr.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "")
                    {
                        cb.Checked = true;
                    }


                    if (dgr.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                    {
                        ddl.Visible = true;
                        conn.QueryString = dgr.Items[j].Cells[2].Text.Replace("&nbsp;", "").Replace("@CODE", "'" + TXT_CODE.Text + "'");
                        conn.ExecuteQuery();
                        for (int k = 0; k < conn.GetRowCount(); k++)
                            ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                        try
                        {
                            ddl.SelectedValue = dgr.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                        }
                        catch { }
                    }
                    else
                    {
                        txtVAL.Visible = true;
                        switch (dgr.Items[j].Cells[1].Text)
                        {
                            case "STR": txtVAL.Text = dgr.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                                break;
                            case "INT": txtVAL.Text = dgr.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                                txtVAL.Attributes.Add("text-align", "right");
                                break;
                            case "FLO": try
                                {
                                    conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + dgr.Items[j].Cells[4].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                    conn.ExecuteQuery();
                                    txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                    txtVAL.Attributes.Add("text-align", "right");
                                }
                                catch { }
                                break;
                            case "BIT": txtVAL.Visible = false;
                                ddl.Visible = true;
                                ddl.Items.Add(new ListItem("YES", "1"));
                                ddl.Items.Add(new ListItem("NO", "0"));
                                try
                                {
                                    ddl.SelectedValue = dgr.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                                }
                                catch { }
                                break;

                        }
                    }
                }
            }

            FillDGRBenefit();
            FillDGRDetailBenefit();
            FillDGRMemberBenefit();
        }

        protected void FillDGRBenefit()
        {
            conn.QueryString = "exec SP_TC_BENEFITS '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();


            Connection connRATE = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection connCLAIM = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection connMEMBERTYPE = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            connRATE.QueryString = "select b.CODE, b.DESCR " +
                                    "from		TC_MASTER a " +
                                    "inner join	PARAM_PREMIUM_RATE_MASTER b on a.PRODUCT_GROUP = b.PRODUCT_GROUP " +
                                    "where " +
                                    "a.CODE = '" + TXT_CODE.Text + "' " +
                                    "order by 2";
            connRATE.ExecuteQuery();
            connCLAIM.QueryString = "select CODE,DESCR from PR_BENEFIT_PAYMENT_TYPE order by 2";
            connCLAIM.ExecuteQuery();
            connMEMBERTYPE.QueryString = "select CODE,DESCR from PR_MEMBER_TYPE order by convert(int, CODE)";
            connMEMBERTYPE.ExecuteQuery();


            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DropDownList ddlRATE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_RATE");
                DropDownList ddlCLAIM = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_CLAIM");
                //DropDownList ddlMEMBERTYPE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_MEMBER_TYPE");
                Button btCOINS = (Button)DGR_BENEFIT.Items[i].FindControl("BT_COINS");
                CheckBox cbRIDER = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_RIDER");
                TextBox txtPCT = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_PCT");
                Label lbCOINS = (Label)DGR_BENEFIT.Items[i].FindControl("LB_COINS");

                DGR_BENEFIT.Items[i].Cells[1].Text = DGR_BENEFIT.Items[i].Cells[1].Text.ToUpper();
                ddlCLAIM.Items.Add(new ListItem("", ""));

                txtPCT.Text = DGR_BENEFIT.Items[i].Cells[4].Text;

                //for (int j = 0; j < connRATE.GetRowCount(); j++)
                //    ddlRATE.Items.Add(new ListItem(connRATE.GetFieldValue(j, 1).ToString(), connRATE.GetFieldValue(j, 0).ToString()));
                for (int j = 0; j < connCLAIM.GetRowCount(); j++)
                    ddlCLAIM.Items.Add(new ListItem(connCLAIM.GetFieldValue(j, 1).ToString(), connCLAIM.GetFieldValue(j, 0).ToString()));
                //for (int j = 0; j < connMEMBERTYPE.GetRowCount(); j++)
                //    ddlMEMBERTYPE.Items.Add(new ListItem(connMEMBERTYPE.GetFieldValue(j, 1).ToString(), connMEMBERTYPE.GetFieldValue(j, 0).ToString()));

                //try
                //{
                //    ddlRATE.SelectedValue = DGR_BENEFIT.Items[i].Cells[2].Text;
                //}
                //catch { }

                if (DGR_BENEFIT.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    conn.QueryString = "select CODE, DESCR from PARAM_PREMIUM_RATE_MASTER where CODE = '" + DGR_BENEFIT.Items[i].Cells[2].Text + "'";
                    conn.ExecuteQuery();
                    ddlRATE.Items.Add(new ListItem(conn.GetFieldValue("DESCR").ToString(), conn.GetFieldValue("CODE").ToString()));
                }

                try
                {
                    ddlCLAIM.SelectedValue = DGR_BENEFIT.Items[i].Cells[3].Text;
                }
                catch { }
                //try
                //{
                //    ddlMEMBERTYPE.SelectedValue = DGR_BENEFIT.Items[i].Cells[5].Text;
                //}
                //catch { }

                if (DGR_BENEFIT.Items[i].Cells[6].Text.Replace("&nbsp;", "") == "1")
                    cbRIDER.Checked = true;

                if (DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    DGR_BENEFIT.Items[i].BackColor = System.Drawing.Color.LightYellow;
                }

                lbCOINS.Text = DGR_BENEFIT.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                if (DGR_BENEFIT.Items[i].Cells[7].Text.Replace("&nbsp;", "") != "")
                    btCOINS.Visible = true;
            }
        }

        protected void FillDGRMemberBenefit()
        {
            conn.QueryString = "exec SP_TC_BENEFITS_MEMBER_TYPE '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER_BENEFIT.DataSource = dt;
            DGR_MEMBER_BENEFIT.DataBind();

            for (int i = 0; i < DGR_MEMBER_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MEMBER_BENEFIT.Items[i].FindControl("CB");
                if (DGR_MEMBER_BENEFIT.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;
            }

        }

        protected void FillDGRDetailBenefit()
        {
            conn.QueryString = "exec SP_TC_BENEFITS_DETAIL '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_DETAIL.DataSource = dt;
            DGR_BENEFIT_DETAIL.DataBind();

            for (int i = 0; i < DGR_BENEFIT_DETAIL.Items.Count; i++)
            {
                Label lbBENEFIT = (Label)DGR_BENEFIT_DETAIL.Items[i].FindControl("LB_BENEFIT");
                TextBox txtPCT = (TextBox)DGR_BENEFIT_DETAIL.Items[i].FindControl("TXT_PCT");
                CheckBox cb = (CheckBox)DGR_BENEFIT_DETAIL.Items[i].FindControl("CB_LAPSE");

                lbBENEFIT.Text = "<B>" + DGR_BENEFIT_DETAIL.Items[i].Cells[4].Text + "</B><BR><span style=\"text-wrap:true;\">" + DGR_BENEFIT_DETAIL.Items[i].Cells[5].Text + "</span>";
                txtPCT.Text = DGR_BENEFIT_DETAIL.Items[i].Cells[2].Text;
                if (DGR_BENEFIT_DETAIL.Items[i].Cells[3].Text == "1")
                    cb.Checked = true;
            }

        }

        protected void FillDGRTrack()
        {
            conn.QueryString = "exec SP_TC_TRACK '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRACK.DataSource = dt;
            DGR_TRACK.DataBind();
        }

        protected void SaveMemberBenefit()
        {   
            for (int i = 0; i < DGR_MEMBER_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MEMBER_BENEFIT.Items[i].FindControl("CB");

                string taken = "0";
                if (cb.Checked)
                    taken = "1";

                try
                {
                    conn.QueryString = "exec SP_TC_BENEFITS_MEMBER_TYPE_UPSERT " +
                                        "'" + TXT_CODE.Text + "'," +
                                        "'" + DGR_MEMBER_BENEFIT.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "'," +
                                        "'" + DGR_MEMBER_BENEFIT.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "'," +
                                        taken + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
        }

        protected void SaveDetailBenefit()
        {
            for (int i = 0; i < DGR_BENEFIT_DETAIL.Items.Count; i++)
            {
                TextBox txtPCT = (TextBox)DGR_BENEFIT_DETAIL.Items[i].FindControl("TXT_PCT");
                CheckBox cblLAPSE = (CheckBox)DGR_BENEFIT_DETAIL.Items[i].FindControl("CB_LAPSE");

                string lapse = "0";
                if (cblLAPSE.Checked)
                    lapse = "1";

                try
                {
                    conn.QueryString = "exec SP_TC_BENEFITS_DETAIL_UPSERT " +
                                        "'" + TXT_CODE.Text + "', " +
                                        "'" + DGR_BENEFIT_DETAIL.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "'," +
                                        "'" + DGR_BENEFIT_DETAIL.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "'," +
                                        "'" + txtPCT.Text.Trim() + "', " +
                                        lapse + ", " +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
        }

        protected void LoadRecord(string code)
        {
            BT_ARCHIEVE.Visible = true;

            conn.QueryString = "select " +
                                "PRODUCT_GROUP, " +
                                "DESCR, " +
                                "LICENCE_NO, " +
                                "APPROVEBY, " +
                                "APPROVEDATE, " +
                                "START_DATE		= convert(varchar(20), START_DATE, 103), " +
                                "END_DATE		= convert(varchar(20), END_DATE, 103) " +
                                "from TC_MASTER " +
                                "where CODE = '" + code + "'";
            conn.ExecuteQuery();

            TXT_NAME.Text = conn.GetFieldValue("DESCR").ToString();
            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();

            try
            {
                DDL_PRODUCTGROUP.SelectedValue = conn.GetFieldValue("PRODUCT_GROUP").ToString();
                DDL_LICENCENO.SelectedValue = conn.GetFieldValue("LICENCE_NO").ToString();
            }
            catch { }

            //FillDGRBenefit();

            StatusCheck(conn.GetFieldValue("APPROVEBY").ToString());
        }

        protected void StatusCheck(string approveby)
        {
            if (approveby.Trim() != "")
            {
                TR_SAVE.Visible = false;

                TXT_NAME.ReadOnly = true;
                DDL_PRODUCTGROUP.Enabled = false;
                DDL_LICENCENO.Enabled = false;


                for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
                {
                    DropDownList ddlRATE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_RATE");
                    DropDownList ddlCLAIM = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_CLAIM");

                    ddlRATE.Enabled = false;
                    ddlCLAIM.Enabled = false;
                }

                for (int i = 0; i < DGR_GROUP.Items.Count; i++)
                {
                    DataGrid dgr = (DataGrid)DGR_GROUP.Items[i].FindControl("DGR_ITEM");
                    for (int j = 0; j < dgr.Items.Count; j++)
                    {
                        CheckBox cb = (CheckBox)dgr.Items[j].FindControl("CB");
                        DropDownList ddl = (DropDownList)dgr.Items[j].FindControl("DDL_REFF");
                        TextBox txtVAL = (TextBox)dgr.Items[j].FindControl("TXT_VAL");

                        if (!cb.Checked)
                        {
                            dgr.Items[j].Visible = false;
                            continue;
                        }
                        else
                        {
                            cb.Enabled = false;
                            if (ddl.Visible)
                                ddl.Enabled = false;

                            if (txtVAL.Visible)
                                txtVAL.ReadOnly = true;
                        }
                    }
                }
            }
            else
            {
                conn.QueryString = "exec SP_TC_MASTER_ENABLE_APPROVE '" + TXT_CODE.Text + "'";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("RES").ToString() == "2")
                    BT_APPROVE.Visible = true;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_NAME.Text.Trim().Length < 5)
                return;

            string ID = "null", enddate = "null";

            if (TXT_CODE.Text != "")
                ID = "'" + TXT_CODE.Text + "'";
            if (TXT_ENDDATE.Text.Trim() != "")
                enddate = "'" + GlobalUse.GlobalDateFormat(TXT_ENDDATE.Text.Trim(), "d/M/yyyy") + "'";

            conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                                ID + "," +
                                "'" + DDL_PRODUCTGROUP.SelectedValue + "'," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + DDL_LICENCENO.SelectedValue + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                enddate + "," +
                                "null," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            ID = conn.GetFieldValue("CODE").ToString();


            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DropDownList ddlRATE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_RATE");
                DropDownList ddlCLAIM = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_CLAIM");
                CheckBox cbRIDER = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_RIDER");
                TextBox txtPCT = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_PCT");

                string ratetable = "null";
                if (ddlRATE.SelectedValue != "")
                    ratetable = "'" + ddlRATE.SelectedValue + "'";

                string rider = "null";
                if (cbRIDER.Checked)
                    rider = "1";

                try
                {
                    conn.QueryString = "exec SP_TC_BENEFITS_BENEFIT " +
                                        "'" + ID + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                                        ratetable + "," +
                                        "'" + ddlCLAIM.SelectedValue + "'," +
                                        rider + "," +
                                        txtPCT.Text.Replace(",", "").Trim() + "," +
                                        "null," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            for (int i = 0; i < DGR_GROUP.Items.Count; i++)
            {
                DataGrid dgr = (DataGrid)DGR_GROUP.Items[i].FindControl("DGR_ITEM");

                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    CheckBox cb = (CheckBox)dgr.Items[j].FindControl("CB");
                    DropDownList ddl = (DropDownList)dgr.Items[j].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)dgr.Items[j].FindControl("TXT_VAL");

                    if (cb.Checked)
                    {
                        string val = "";

                        if (ddl.Visible)
                            val = ddl.SelectedValue;
                        else
                        {
                            val = txtVAL.Text.Trim();
                            if (dgr.Items[j].Cells[1].Text == "FLO")
                                val = val.Replace(",", "");
                        }

                        try
                        {
                            conn.QueryString = "exec SP_TC_ITEMS_UPSERT" +
                                                "'" + ID + "'," +
                                                "'" + dgr.Items[j].Cells[0].Text + "'," +
                                                "'" + val + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            //conn.QueryString = "insert into TC_ITEMS select " +
                            //                    "'" + ID + "'," +
                            //                    "'" + dgr.Items[j].Cells[0].Text + "'," +
                            //                    "'" + val + "'," +
                            //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                            //                    "GETDATE()," +
                            //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                            //                    "GETDATE()";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

            }

            SaveMemberBenefit();
            SaveDetailBenefit();
            Response.Redirect("TC.aspx?CODE=" + ID);
        }

        protected void ShowPopUp(string title, string url)
        {
            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", url);
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ShowPopUp(((Button)sender).Text, URL);
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                                "'" + TXT_CODE.Text + "'," +
                                "'" + DDL_PRODUCTGROUP.SelectedValue + "'," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + DDL_LICENCENO.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            Response.Redirect("TC.aspx?CODE=" + TXT_CODE.Text);
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Coins")
            {
                ShowPopupCoins(TXT_CODE.Text, e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Rate")
            {
                DropDownList ddlRATE = (DropDownList)e.Item.FindControl("DDL_RATE");
                ddlRATE.Enabled = true;

                conn.QueryString =  "select CODE = '', DESCR = '' " +
                                    "union all " +
                                    "select b.CODE, b.DESCR " +
                                    "from		TC_MASTER a " +
                                    "inner join	PARAM_PREMIUM_RATE_MASTER b on a.PRODUCT_GROUP = b.PRODUCT_GROUP " +
                                    "where " +
                                    "a.CODE = '" + TXT_CODE.Text + "' " +
                                    "order by 2";
                conn.ExecuteQuery();

                ddlRATE.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    ddlRATE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                try
                {
                    ddlRATE.SelectedValue = e.Item.Cells[2].Text;
                }
                catch { }
            }
        }

        protected void ShowPopupCoins(string tccode, string benefitcode, string benefitname)
        {
            LB_COINS_BENEFIT_CODE.Text = benefitcode;
            LB_COINS_BENEFIT.Text = benefitname;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlCoins').style.display = 'block';", true);

            FillDDLCoins(TXT_CODE.Text, benefitcode);
            FillDGRCoins(TXT_CODE.Text, benefitcode);
        }


        protected void FillDDLCoins(string tccode, string benefitcode)
        {
            conn.QueryString = "select " +
                                "CODE = a.COMPANY_CODE, " +
                                "DESCR = b.DESCR + ' - ' + a.COMPANY_NAME " +
                                "from PARAM_OTHER_INSURANCE a " +
                                "inner join PR_INSURANCE_TYPE b on a.TYPE = b.CODE " +
                                "where " +
                                "TYPE in ('LI','GN') " +
                                "and a.COMPANY_CODE not in (select COMPANY_CODE from TC_BENEFITS_COINSURANCE where TC_CODE = '" + tccode + "' and BENEFIT_CODE = '" + benefitcode + "') " +
                                "order by " +
                                "a.TYPE desc, a.COMPANY_NAME";
            conn.ExecuteQuery();
            DDL_COINS.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COINS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGRCoins(string tccode, string benefitcode)
        {
            conn.QueryString = "select " +
                                "a.COMPANY_CODE, " +
                                "b.COMPANY_NAME, " +
                                "a.SHARE, " +
                                "a.LOADING " +
                                "from TC_BENEFITS_COINSURANCE a " +
                                "inner join PARAM_OTHER_INSURANCE b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "a.TC_CODE = '" + tccode + "' and a.BENEFIT_CODE = '" + benefitcode + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_COINS.DataSource = dt;
            DGR_COINS.DataBind();

            for (int i = 0; i < DGR_COINS.Items.Count; i++)
            {
                TextBox txtSHARE = (TextBox)DGR_COINS.Items[i].FindControl("TXT_COINSSHARE");
                TextBox txtLOADING = (TextBox)DGR_COINS.Items[i].FindControl("TXT_COINSLOADING");
                txtSHARE.Text = DGR_COINS.Items[i].Cells[2].Text;
                txtLOADING.Text = DGR_COINS.Items[i].Cells[3].Text;
            }
        }

        protected void BT_COINSADD_Click(object sender, EventArgs e)
        {
            LB_COINSERROR.Text = "";

            try
            {
                conn.QueryString = "insert into TC_BENEFITS_COINSURANCE select " +
                                    "'" + TXT_CODE.Text + "'," +
                                    "'" + LB_COINS_BENEFIT_CODE.Text + "'," +
                                    "'" + DDL_COINS.SelectedValue + "'," +
                                    "'1'," +
                                    "'0'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();
                FillDGRBenefit();
                ShowPopupCoins(TXT_CODE.Text, LB_COINS_BENEFIT_CODE.Text, LB_COINS_BENEFIT.Text);
            }
            catch (System.Exception ex)
            {
                LB_COINSERROR.Text = ex.Message;
            }
        }

        protected void DGR_COINS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_COINS.Items.Count; i++)
                {
                    TextBox txtSHARE = (TextBox)DGR_COINS.Items[i].FindControl("TXT_COINSSHARE");
                    TextBox txtLOADING = (TextBox)DGR_COINS.Items[i].FindControl("TXT_COINSLOADING");
                    try
                    {
                        conn.QueryString = "update TC_BENEFITS_COINSURANCE set " +
                                                "SHARE = " + txtSHARE.Text.Replace(",", "") + ", " +
                                                "LOADING = " + txtLOADING.Text.Replace(",", "") + " " +
                                                "where " +
                                                "TC_CODE = '" + TXT_CODE.Text + "' " +
                                                "and BENEFIT_CODE = '" + LB_COINS_BENEFIT_CODE.Text + "' " +
                                                "and COMPANY_CODE = '" + DGR_COINS.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                ShowPopupCoins(TXT_CODE.Text, LB_COINS_BENEFIT_CODE.Text, LB_COINS_BENEFIT.Text);
            }

            if (e.CommandName == "Delete")
            {
                LB_COINSERROR.Text = "";

                try
                {
                    conn.QueryString = "delete from TC_BENEFITS_COINSURANCE where " +
                                        "TC_CODE = '" + TXT_CODE.Text + "' " +
                                        "and BENEFIT_CODE = '" + LB_COINS_BENEFIT_CODE.Text + "' " +
                                        "and COMPANY_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRBenefit();
                    ShowPopupCoins(TXT_CODE.Text, LB_COINS_BENEFIT_CODE.Text, LB_COINS_BENEFIT.Text);
                }
                catch (System.Exception ex)
                {
                    LB_COINSERROR.Text = ex.Message;
                }
            }
        }


        protected void FillDGRBenefitDetail(string benefitCode)
        {
            conn.QueryString = "exec SP_TC_BENEFITS_DETAIL " +
                                "'" + TXT_CODE.Text + "', " +
                                "'" + benefitCode + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_DETAIL.DataSource = dt;
            DGR_BENEFIT_DETAIL.DataBind();

            if (conn.GetRowCount() > 0)
            {
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    TextBox txtPCT = (TextBox)DGR_BENEFIT_DETAIL.Items[i].FindControl("TXT_BEN_DETAIL_PCT");
                    DropDownList ddlLAPSE = (DropDownList)DGR_BENEFIT_DETAIL.Items[i].FindControl("DDL_BEN_DETAIL_LAPSE");

                    txtPCT.Text = DGR_BENEFIT_DETAIL.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    try
                    {
                        ddlLAPSE.SelectedValue = DGR_BENEFIT_DETAIL.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
            }
        }


        protected void ShowBenefitDetails()
        {
            LB_BENEFIT_TITLE.Text = BT_BENEFIT_DETAILS.Text;
            DGR_BENEFIT_DETAIL.Visible = true;
            DGR_MEMBER_BENEFIT.Visible = false;
        }

        protected void BT_BENEFIT_DETAILS_Click(object sender, EventArgs e)
        {
            ShowBenefitDetails();
        }

        protected void BT_BENEFIT_MEMBERS_Click(object sender, EventArgs e)
        {
            LB_BENEFIT_TITLE.Text = BT_BENEFIT_MEMBERS.Text;
            DGR_BENEFIT_DETAIL.Visible = false;
            DGR_MEMBER_BENEFIT.Visible = true;
        }
    }
}
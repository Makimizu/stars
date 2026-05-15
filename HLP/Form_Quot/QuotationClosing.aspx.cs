using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationClosing : System.Web.UI.Page
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
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_QUOTNO.Text = Request.QueryString["code"];
                LB_VER.Text = Request.QueryString["ver"];
                LB_PROD.Text = Request.QueryString["prod"];
                Setup();
            }
        }

        protected void Setup()
        {
            LoadRecord();
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
            }
        }

        protected void LoadRecord()
        {
            LoadLoading();
            LoadLoadingTabarru();
            LoadClosing("QVER", "QVER01", LB_QUOTNO.Text + "-" + LB_VER.Text);
            LoadTerm();
            LoadPremiumTotal();
            ShowPendingError();
            LoadAgentCommission();
        }

        protected void LoadAgentCommission()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_AGENT " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
            conn.ExecuteQuery();
            DGR_AGENT.DataSource = conn.GetDataTable();
            DGR_AGENT.DataBind();

            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtVAL = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMMVAL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtAGENTCODE.Enabled = false;
                    txtVAL.Enabled = false;
                }

                txtAGENTCODE.Text = DGR_AGENT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtVAL.Text = DGR_AGENT.Items[i].Cells[1].Text;
            }
        }

        protected void LoadPremiumTotal()
        {
            /*
            conn.QueryString = "exec SP_QUOTATION_VERSION_PREMIUM_TOTAL " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
            */

            conn.QueryString = "select " +
                                "SEQ, " +
                                "CODE, " +
                                "DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00','') " +
                                "from V_QUOTATION_VERSION_PREMIUM_COMPONENT " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = '" + LB_VER.Text + "' " +
                                "order by " +
                                "SEQ";

            conn.ExecuteQuery();
            DGR_PRM_TOTAL.DataSource = conn.GetDataTable();
            DGR_PRM_TOTAL.DataBind();

            for (int i = 0; i < DGR_PRM_TOTAL.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_PRM_TOTAL.Items[i].FindControl("TXT_AMOUNT");
                txtAMOUNT.Text = DGR_PRM_TOTAL.Items[i].Cells[2].Text;

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtAMOUNT.Enabled = false;
                }

                if (DGR_PRM_TOTAL.Items[i].Cells[0].Text == "0" || DGR_PRM_TOTAL.Items[i].Cells[0].Text == "1")
                {
                    txtAMOUNT.Enabled = false;
                }

                if (DGR_PRM_TOTAL.Items[i].Cells[0].Text == "2")
                {
                    txtAMOUNT.BackColor = System.Drawing.Color.Blue;
                    txtAMOUNT.ForeColor = System.Drawing.Color.White;
                }

                if (DGR_PRM_TOTAL.Items[i].Cells[0].Text == "3")
                {
                    txtAMOUNT.BackColor = System.Drawing.Color.Pink;
                    txtAMOUNT.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void LoadClosing(string tipe, string group, string owner)
        {
            LB_ERR.Text = "";
            try
            {
                conn.QueryString = "exec SP_PARAM_ADDITIONAL_INFO " +
                                    "'" + tipe + "'," +
                                    "'" + group + "'," +
                                    "'" + owner + "'";
                conn.ExecuteQuery();
                DGR_ADD_INFO.DataSource = conn.GetDataTable();
                DGR_ADD_INFO.DataBind();
                string GroupID = "";
                Color ItemColor = Color.Empty;
                Color Item1Color = Color.LightYellow;
                Color Item2Color = Color.LightSeaGreen;
                for (int i = 0; i < DGR_ADD_INFO.Items.Count; i++)
                {
                    TextBox txtval = (TextBox)DGR_ADD_INFO.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR_ADD_INFO.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR_ADD_INFO.Items[i].FindControl("TXT_DATE");

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txtval.Enabled = false;
                        ddlval.Enabled = false;
                        txtdate.Enabled = false;
                    }

                    switch (DGR_ADD_INFO.Items[i].Cells[2].Text)
                    {
                        case "001": txtval.Visible = true;
                            txtval.Width = 200;
                            txtval.MaxLength = int.Parse(DGR_ADD_INFO.Items[i].Cells[3].Text);
                            break;
                        case "002": txtval.Visible = true;
                            txtval.Width = 40;
                            break;
                        case "003": txtval.Visible = true;
                            txtval.Width = 100;
                            txtval.MaxLength = int.Parse(DGR_ADD_INFO.Items[i].Cells[3].Text);
                            txtval.CssClass = "ASPTextBoxNumber";
                            break;
                        case "004": txtdate.Visible = true;
                            break;
                        case "005": ddlval.Visible = true;
                            ddlval.Items.Add(new ListItem("YA", "1"));
                            ddlval.Items.Add(new ListItem("TIDAK", "0"));
                            break;
                        case "006": txtval.Visible = true;
                            txtval.Width = 200;
                            break;
                    }

                    if (DGR_ADD_INFO.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                    {
                        txtval.Visible = false;
                        txtdate.Visible = false;
                        ddlval.Visible = true;

                        LB_ERR.Text = "";
                        try
                        {
                            if (DGR_ADD_INFO.Items[i].Cells[0].Text == "14")
                            {
                                var allowedProducts = new[] { "719", "679", "800" };
                                var productCode = LB_PROD.Text;

                                if (allowedProducts.Contains(productCode))
                                {
                                    conn.QueryString = string.Format(
                                        "SELECT DISTINCT ID, " +
                                        "CONCAT(' ', UPPER(COMPANY_NAME), ' ', CONVERT(VARCHAR(10), QUOTA_SHARE), '%', ' ', CONVERT(VARCHAR(10), LOADING), '%') AS REINSURANCE, " +
                                        "CASE WHEN QUOTA_SHARE = '50' THEN '1' ELSE '0' END AS SORT " +
                                        "FROM REINSURANCE.dbo.V_TC_MASTER a " +
                                        "INNER JOIN PARAM_PRODUCT_REINSURANCE b ON a.ID = b.REINS_ID " +
                                        "AND b.PRODUCT_CODE = '{0}' " +
                                        "ORDER BY SORT DESC",
                                        productCode
                                    );
                                }
                            }
                            else
                            {
                                conn.QueryString = DGR_ADD_INFO.Items[i].Cells[4].Text;
                            }
                            conn.ExecuteQuery();
                            for (int j = 0; j < conn.GetRowCount(); j++)
                            {
                                string id = conn.GetFieldValue(j, 0).ToString();
                                string reins = conn.GetFieldValue(j, 1).ToString();
                                ddlval.Items.Add(new ListItem(
                                    string.Format("{0} - {1}", id, reins), id)
                                );

                                //ddlval.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                            }
                                
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERR.Text = ex.Message;
                        }
                    }

                    if (DGR_ADD_INFO.Items[i].Cells[5].Text != "&nbsp;")
                    {
                        if (txtval.Visible)
                        {
                            txtval.Text = DGR_ADD_INFO.Items[i].Cells[5].Text;
                        }
                        if (ddlval.Visible)
                        {
                            ddlval.SelectedValue = DGR_ADD_INFO.Items[i].Cells[5].Text;
                        }
                        if (txtdate.Visible)
                        {
                            txtdate.Text = DGR_ADD_INFO.Items[i].Cells[5].Text;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }

        protected void LoadLoading()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_LOADING " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                txt.Text = DGR.Items[i].Cells[1].Text;

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txt.Enabled = false;
                }

                if (DGR.Items[i].Cells[2].Text == "1")
                {
                    txt.Enabled = false;
                }
            }
        }

        protected void LoadLoadingTabarru()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_LOADING_TABARRU " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
            conn.ExecuteQuery();
            DGR_TABARRU.DataSource = conn.GetDataTable();
            DGR_TABARRU.DataBind();

            for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                txt.Text = DGR_TABARRU.Items[i].Cells[1].Text;

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txt.Enabled = false;
                }
            }
        }

        protected void LoadTerm()
        {
            DGR_TERM.Visible = false;
            conn.QueryString = "select VAL from ADDITIONAL_INFO_DATA " +
                                "where " +
                                "OWNER = '" + LB_QUOTNO.Text + "-" + LB_VER.Text + "' " +
                                "and CODE = '6' and VAL = '06'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DGR_TERM.Visible = true;

            conn.QueryString = "exec SP_QUOTATION_VERSION_MOP_TERM " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
            conn.ExecuteQuery();
            DGR_TERM.DataSource = conn.GetDataTable();
            DGR_TERM.DataBind();

            for (int i = 0; i < DGR_TERM.Items.Count; i++)
            {
                TextBox txtDate = (TextBox)DGR_TERM.Items[i].FindControl("TXT_DATE");
                TextBox txtAmt = (TextBox)DGR_TERM.Items[i].FindControl("TXT_INVAMOUNT");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtDate.Enabled = false;
                    txtAmt.Enabled = false;
                }

                txtDate.Text = DGR_TERM.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtAmt.Text = DGR_TERM.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void SaveLoading()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                if (!txt.Enabled)
                    continue;

                conn.QueryString = "update QUOTATION_VERSION_LOADING set " +
                                    "VAL = convert(float," + txt.Text + ")/100, " +
                                    "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE = GETDATE() " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = '" + LB_VER.Text + "' " +
                                    "and LOADING_CODE = '" + DGR.Items[i].Cells[0].Text + "'";
                conn.ExecuteNonQuery();
            }
            for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                conn.QueryString = "update QUOTATION_VERSION_LOADING_TABARRU set " +
                                    "VAL = convert(float," + txt.Text + ")/100, " +
                                    "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE = GETDATE() " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = '" + LB_VER.Text + "' " +
                                    "and LOADING_CODE = '" + DGR_TABARRU.Items[i].Cells[0].Text + "'";
                conn.ExecuteNonQuery();
            }

            /*
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_RECALC " +
                                    "'" + LB_QUOTNO.Text + "', " +
                                    "'" + LB_VER.Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch(System.Exception ex)
            {
                LB_ERR.Text = LB_ERR.Text + "<BR>" + ex.Message;
            }
            */

            LoadLoading();
            LoadLoadingTabarru();
        }

        protected void SaveClosing(string owner)
        {
            //LB_ERR.Text = "";
            try
            {
                conn.QueryString = "delete from ADDITIONAL_INFO_DATA where OWNER='" + owner + "' and CODE in (select CODE from PARAM_ADDITIONAL_INFO where GROUP_ID = 'QVER01')";
                conn.ExecuteNonQuery();

                for (int i = 0; i < DGR_ADD_INFO.Items.Count; i++)
                {
                    TextBox txtval = (TextBox)DGR_ADD_INFO.Items[i].FindControl("TXT_VAL");
                    DropDownList ddlval = (DropDownList)DGR_ADD_INFO.Items[i].FindControl("DDL_VAL");
                    TextBox txtdate = (TextBox)DGR_ADD_INFO.Items[i].FindControl("TXT_DATE");

                    string val = "";
                    if (txtval.Visible)
                    {
                        if (txtval.Text.Trim() != "")
                            val = txtval.Text.Trim();
                    }
                    if (ddlval.Visible)
                    {
                        val = ddlval.SelectedValue;
                    }
                    if (txtdate.Visible)
                    {
                        if (txtdate.Text.Trim() != "")
                        {
                            val = GlobalUse.GlobalDateFormat(txtdate.Text.Trim(), "d/M/yyyy");
                        }
                    }

                    if (val.Trim() != "")
                    {
                        try
                        {
                            conn.QueryString = "insert into ADDITIONAL_INFO_DATA " +
                                                "select newid(), " +
                                                "'" + owner + "'," +
                                                "'" + DGR_ADD_INFO.Items[i].Cells[0].Text + "'," +
                                                "'" + val + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                            conn.ExecuteQuery();

                            conn.QueryString = "exec SP_PARAM_ADDITIONAL_INFO_UPDATE " +
                                    "'" + owner + "'";
                            conn.ExecuteQuery();
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERR.Text = LB_ERR.Text + "<BR>" + ex.Message;
                        }

                    }
                }

            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = LB_ERR.Text + "<BR>" + ex.Message;
            }

        }

        protected string MOPChecking()
        {
            string result = "";

            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_MOP_CHECKING " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VER.Text + "'";
                conn.ExecuteQuery();
                result = conn.GetFieldValue("VAL").ToString();
            }
            catch
            {
                return result;
            }

            return result;
        }

        protected void SaveTERM()
        {
            if (MOPChecking() != "06")
                return;

            try
            {
                conn.QueryString = "delete from QUOTATION_VERSION_MOP_TERM " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = '" + LB_VER.Text + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                return;
            }

            int seq = 1;
            for (int i = 0; i < DGR_TERM.Items.Count; i++)
            {
                TextBox txtDate = (TextBox)DGR_TERM.Items[i].FindControl("TXT_DATE");
                TextBox txtAmt = (TextBox)DGR_TERM.Items[i].FindControl("TXT_INVAMOUNT");

                try
                {
                    DateTime date = DateTime.Parse(GlobalUse.GlobalDateFormat(txtDate.Text.Trim(), "d/M/yyyy"));
                    float Amount = float.Parse(txtAmt.Text.Trim().Replace(",", ""));

                    conn.QueryString = "exec SP_QUOTATION_VERSION_MOP_TERM_UPSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        "'" + LB_VER.Text + "'," +
                                        seq.ToString() + "," +
                                        "'" + GlobalUse.GlobalDateFormat(txtDate.Text.Trim(), "d/M/yyyy") + "'," +
                                        txtAmt.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    seq = seq + 1;
                }
                catch (System.Exception ex)
                {
                    //LB_ERR.Text = LB_ERR.Text  + "<BR>" + ex.Message;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                double ujroh = 0, tabarru = 0, komisi = 0;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtujroh = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                    if (!txtujroh.Enabled)
                        continue;
                    ujroh += double.Parse(txtujroh.Text);
                    //LB_ERR.Text = LB_ERR.Text + "<BR>ujroh : " + ujroh + " --> " + i + " : " + double.Parse(txtujroh.Text);
                }
                for (int i = 0; i < DGR_AGENT.Items.Count; i++)
                {
                    TextBox txtujroh = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMMVAL");
                    if (!txtujroh.Enabled)
                        continue;
                    komisi += double.Parse(txtujroh.Text);
                    //LB_ERR.Text = LB_ERR.Text + "<BR>komisi : " + komisi + " --> " + i + " : " + double.Parse(txtujroh.Text); ;
                }
                for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
                {
                    TextBox txttabarru = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                    //if (!txttabarru.Enabled)
                    //    continue;
                    tabarru += double.Parse(txttabarru.Text);
                    //LB_ERR.Text = LB_ERR.Text + "<BR>tabarru : " + tabarru + " --> " + i + " : " + double.Parse(txttabarru.Text); ;
                }
                //double tottabakom = tabarru + komisi;
                double totujrohkom = ujroh + komisi;
                //double tottabujroh = tabarru + ujroh;
                double alltotal = tabarru + totujrohkom;
                double tot100 = 100;
                //LB_ERR.Text = LB_ERR.Text + "<BR>alltotal : " + alltotal + "<BR>tottabakom : " + tottabakom +
                //                            "<BR>totujrohkom : " + totujrohkom + "<BR>tottabujroh : " + tottabujroh;
                if (alltotal == tot100)
                {
                    //Response.Write("<script>alert('BERHASIL')</script>");
                    SaveCommissionAgent();
                    SaveLoading();
                    SaveClosing(LB_QUOTNO.Text + "-" + LB_VER.Text);
                    SavePremiumComponent();
                    SaveTERM();
                    ClosingValidation();
                    LoadRecord();
                }
                else
                {
                    Response.Write("<script>alert('Jumlah UJROH dan TABARRU tidak 100%, hasil : " + alltotal + "%')</script>");
                }
                //Response.Redirect("QuotationClosing.aspx?code=" + LB_QUOTNO.Text + "&ver=" + LB_VER.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = LB_ERR.Text + "<BR>" + ex.Message;
            }
        }

        protected void SaveCommissionAgent()
        {
            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtVAL = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMMVAL");

                try
                {
                    conn.QueryString = "exec SP_QUOTATION_VERSION_AGENT_UPSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        LB_VER.Text + "," +
                                        "'" + DGR_AGENT.Items[i].Cells[0].Text + "'," +
                                        "'" + txtAGENTCODE.Text.Trim() + "'," +
                                        txtVAL.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
        }

        protected void ShowPendingError()
        {
            DGR_ERROR.Visible = false;

            conn.QueryString = "select " +
                                "[NO] = SEQ, " +
                                "[PENDING ITEM] = REMARK " +
                                "from QUOTATION_VERSION_PENDING_ERROR " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = '" + LB_VER.Text + "' " +
                                "order by SEQ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DGR_ERROR.Visible = true;
                DGR_ERROR.DataSource = conn.GetDataTable();
                DGR_ERROR.DataBind();
            }
        }

        protected void ClosingValidation()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_CLOSING_VALIDATION " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_LOADING_TOTAL " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
                conn.ExecuteQuery();
                e.Item.Cells[4].Text = conn.GetFieldValue("VAL").ToString();
                e.Item.Cells[5].Text = conn.GetFieldValue("AMOUNT").ToString();
            }
        }
        protected void DGR_TABARRU_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_LOADING_TABARRU_TOTAL " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[3].Text = conn.GetFieldValue("VAL").ToString();
                e.Item.Cells[4].Text = conn.GetFieldValue("AMOUNT").ToString();
            }
        }

        protected void DGR_TERM_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbINV = (Label)e.Item.FindControl("LB_INV");
                Label lbPRM = (Label)e.Item.FindControl("LB_PREMIUM");
                Label lbDIF = (Label)e.Item.FindControl("LB_DIFF");

                conn.QueryString = "exec SP_QUOTATION_VERSION_MOP_TERM_DIFF " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VER.Text + "'";
                conn.ExecuteQuery();

                lbINV.Text = conn.GetFieldValue("INVOICE").ToString();
                lbPRM.Text = conn.GetFieldValue("PREMIUM").ToString();
                lbDIF.Text = conn.GetFieldValue("DIFF").ToString();
            }
        }

        protected void DGR_PRM_TOTAL_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbTOTAL = (Label)e.Item.FindControl("LB_AMOUNT_TOTAL");

                conn.QueryString = "select " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,SUM((case when SEQ=3 then -1 else 1 end) * AMOUNT)),1),'.00','')  " +
                                "from V_QUOTATION_VERSION_PREMIUM_COMPONENT " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = '" + LB_VER.Text + "'";
                conn.ExecuteQuery();

                lbTOTAL.Text = conn.GetFieldValue("AMOUNT").ToString();
                e.Item.Cells[3].Text = "TOTAL";
            }
        }

        protected void SavePremiumComponent()
        {
            for (int i = 0; i < DGR_PRM_TOTAL.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_PRM_TOTAL.Items[i].FindControl("TXT_AMOUNT");

                if (DGR_PRM_TOTAL.Items[i].Cells[0].Text != "0" && DGR_PRM_TOTAL.Items[i].Cells[0].Text != "1")
                {
                    //try
                    //{
                    conn.QueryString = "exec SP_QUOTATION_VERSION_PREMIUM_COMPONENT_UPSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        "'" + LB_VER.Text + "'," +
                                        "'" + DGR_PRM_TOTAL.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR_PRM_TOTAL.Items[i].Cells[1].Text + "'," +
                                        "'" + txtAMOUNT.Text.Replace(",", "").Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    //}
                    //catch { }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace REAS.Form_Company
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
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
                
                try
                {
                    TXT_CODE.Text = Request.QueryString["CODE"];
                    LoadRecord(TXT_CODE.Text);
                    FillDGRComposition();
                    FillDGRBenefit();
                    
                }
                catch { }
            }
        }

        protected void FillDGRComposition()
        {
            conn.QueryString = "exec SP_TC_MASTER_COMPOSITION '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_COMPOSITION.DataSource = dt;
            DGR_COMPOSITION.DataBind();

            for (int i = 0; i < DGR_COMPOSITION.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_COMPOSITION.Items[i].FindControl("TXT_PCT");
                txt.Text = DGR_COMPOSITION.Items[i].Cells[1].Text;
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from COMPANY order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select 'LF' CODE, 'INDIVIDU' PRODUCT";
            conn.ExecuteQuery();
            DDL_PRODUCT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_REINS_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PARAM_PREMIUM_RATE_MASTER order by 2";
            conn.ExecuteQuery();
            DDL_RATE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_RATE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PARAM_UW_MATRIX_MASTER order by 2";
            conn.ExecuteQuery();
            DDL_UW.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_UW.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            GetPaymentFreq();
            FillDDResiko();
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "COMPANY_CODE, " +
                                "a.DESCR, " +
                                "DOCNO, " +
                                "START_DATE = convert(varchar(20),START_DATE,103), " +
                                "TYPE, " +
                                "OWN_RETENTION = replace(convert(varchar(100),convert(money,OWN_RETENTION),1),'.00',''), " +
                                "QUOTA_SHARE, " +
                                "MATRIX_ID, " +
                                "RATE_ID, " +
                                "LOADING = isnull(LOADING,0), " +
                                "b.ID_PAYMENT_FREQUENCY, " +
                                "c.DESCR AS DESC_PAYMENT_FREQ, " +
                                "b.CODE, " +
                                "d.DESCR AS DESC_TIPE_RISK, e.APP_ID " +
                                "from TC_MASTER a " +
                                "left join TC_MASTER_RESIKO b " +
                                "on a.ID = b.ID_TC_MASTER " +
                                "left join PR_PAYMENT_FREQUENCY c " +
                                "on b.ID_PAYMENT_FREQUENCY = c.CODE " +
                                "left join PARAM_RESIKO_MASTER d " +
                                "on b.CODE = d.CODE " +
                                "left join TC_MASTER_USAGE e " +
                                "on a.ID = e.TC_ID " +
                                "where a.ID = '" + code + "'";

            conn.ExecuteQuery();

            TXT_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            TXT_DOCNO.Text = conn.GetFieldValue("DOCNO").ToString();
            TXT_OR.Text = conn.GetFieldValue("OWN_RETENTION").ToString();
            TXT_SHARE.Text = conn.GetFieldValue("QUOTA_SHARE").ToString();
            TXT_LOADING.Text = conn.GetFieldValue("LOADING").ToString();
            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();

            try
            {
                DDL_COMPANY.SelectedValue = conn.GetFieldValue("COMPANY_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_PRODUCT.SelectedValue = conn.GetFieldValue("APP_ID").ToString();
            }
            catch { }

            try
            {
                DDL_RATE.SelectedValue = conn.GetFieldValue("RATE_ID").ToString();
                BT_RATE.Visible = true;
            }
            catch { }

            try
            {
                DDL_TYPE.SelectedValue = conn.GetFieldValue("TYPE").ToString();
            }
            catch { }

            try
            {
                DDL_UW.SelectedValue = conn.GetFieldValue("MATRIX_ID").ToString();
                BT_UW.Visible = true;
            }
            catch { }

            if (TXT_CODE.Text != "")
                BT_ARCHIEVE.Visible = true;

            try
            {
                ddPaymentFreq.SelectedValue = conn.GetFieldValue("ID_PAYMENT_FREQUENCY").ToString();
            }
            catch { }

            try
            {
                ddPenurunanResiko.SelectedValue = conn.GetFieldValue("CODE").ToString();
            }
            catch { }
        }

        protected void DDL_UW_SelectedIndexChanged(object sender, EventArgs e)
        {
            BT_UW.Visible = false;
            if (DDL_UW.SelectedValue != "")
                BT_UW.Visible = true;
        }

        protected void DDL_RATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BT_RATE.Visible = false;
            if (DDL_RATE.SelectedValue != "")
                BT_RATE.Visible = true;
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            string idTC = string.Empty;

            LB_ERROR.Text = "";
            string ID = "null";
            if (TXT_CODE.Text != "")
                ID = "'" + TXT_CODE.Text + "'";

            string UW = "null";
            string RATE = "null";
            if (DDL_UW.SelectedValue != "")
                UW = "'" + DDL_UW.SelectedValue + "'";
            if (DDL_RATE.SelectedValue != "")
                RATE = "'" + DDL_RATE.SelectedValue + "'";

            try
            {
                conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                                                   ID + "," +
                                                   "'" + DDL_COMPANY.SelectedValue + "'," +
                                                   "'" + TXT_DESCR.Text.Trim() + "'," +
                                                   "'" + TXT_DOCNO.Text.Trim() + "'," +
                                                   "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                                   "'" + DDL_TYPE.SelectedValue + "'," +
                                                   "'" + TXT_OR.Text.Trim().Replace(",", "") + "'," +
                                                   "'" + TXT_SHARE.Text.Trim() + "'," +
                                                   UW + "," +
                                                   RATE + "," +
                                                   "'" + TXT_LOADING.Text.Trim() + "'," +
                                                   "'" + DDL_PRODUCT.SelectedValue + "'," +
                                                   "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                //SaveTCResiko(TXT_DESCR.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                SaveTCResiko(TXT_CODE.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                idTC = GetIDTC(TXT_CODE.Text.Trim());



                //if (ddPaymentFreq.SelectedItem.Text == "SEKALIGUS")
                //{
                //    conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                //                                       ID + "," +
                //                                       "'" + DDL_COMPANY.SelectedValue + "'," +
                //                                       "'" + TXT_DESCR.Text.Trim() + "'," +
                //                                       "'" + TXT_DOCNO.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                //                                       "'" + DDL_TYPE.SelectedValue + "'," +
                //                                       "'" + TXT_OR.Text.Trim().Replace(",", "") + "'," +
                //                                       "'" + TXT_SHARE.Text.Trim() + "'," +
                //                                       UW + "," +
                //                                       RATE + "," +
                //                                       "'" + TXT_LOADING.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //    conn.ExecuteQuery();
                //    //SaveTCResiko(TXT_DESCR.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                //    SaveTCResiko(TXT_CODE.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                //    idTC = GetIDTC(TXT_CODE.Text.Trim());
                    
                //}
                //else if (ddPaymentFreq.SelectedItem.Text == "TAHUNAN")
                //{
                //    conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                //                                       ID + "," +
                //                                       "'" + DDL_COMPANY.SelectedValue + "'," +
                //                                       "'" + TXT_DESCR.Text.Trim() + "'," +
                //                                       "'" + TXT_DOCNO.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                //                                       "'" + DDL_TYPE.SelectedValue + "'," +
                //                                       "'" + TXT_OR.Text.Trim().Replace(",", "") + "'," +
                //                                       "'" + TXT_SHARE.Text.Trim() + "'," +
                //                                       UW + "," +
                //                                       RATE + "," +
                //                                       "'" + TXT_LOADING.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //    conn.ExecuteQuery();
                //    //SaveTCResiko(TXT_DESCR.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                //    SaveTCResiko(TXT_CODE.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                //    idTC = GetIDTC(TXT_CODE.Text.Trim());

                //}
                //else 
                //{
                //    conn.QueryString = "exec SP_TC_MASTER_UPSERT " +
                //                                       ID + "," +
                //                                       "'" + DDL_COMPANY.SelectedValue + "'," +
                //                                       "'" + TXT_DESCR.Text.Trim() + "'," +
                //                                       "'" + TXT_DOCNO.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                //                                       "'" + DDL_TYPE.SelectedValue + "'," +
                //                                       "'" + TXT_OR.Text.Trim().Replace(",", "") + "'," +
                //                                       "'" + TXT_SHARE.Text.Trim() + "'," +
                //                                       UW + "," +
                //                                       RATE + "," +
                //                                       "'" + TXT_LOADING.Text.Trim() + "'," +
                //                                       "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //    conn.ExecuteQuery();
                //    SaveTCResiko(TXT_CODE.Text.Trim(), ddPenurunanResiko.SelectedValue.ToString(), Convert.ToInt32(ddPaymentFreq.SelectedValue.ToString()));
                //    idTC = GetIDTC(TXT_CODE.Text.Trim());
                //}
                
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void ShowPopUp(string title, string mode)
        {
            DGR.Visible = false;
            iFrame.Visible = false;

            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

            if (mode == "1")
            {
                conn.QueryString = "exec SP_PARAM_UW_MATRIX_DETAIL '" + DDL_UW.SelectedValue + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    DGR.Visible = true;
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    DGR.DataSource = dt;
                    DGR.DataBind();
                }
            }

            if (mode == "2")
            {
                conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_DETAIL '" + DDL_RATE.SelectedValue + "','M'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    DGR.Visible = true;
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    DGR.DataSource = dt;
                    DGR.DataBind();
                }
            }


            if (mode == "3")
            {
                iFrame.Visible = true;
                string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", TXT_CODE.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                iFrame.Attributes.Add("src", URL);
            }

            if (mode == "4") 
            {
                GetDataYear(ddPenurunanResiko.SelectedValue, "Y");
                GetDataMonth(ddPenurunanResiko.SelectedValue, "M");
                lblStatus.Visible = true;
                lblStatus2.Visible = true;
                lblStatus.Text = "TAHUNAN";
                lblStatus2.Text = "BULANAN";

            }
        }

        protected void BT_UW_Click(object sender, EventArgs e)
        {
            DGRNULL();
            ShowPopUp("MEDICAL TABLE : " + DDL_UW.SelectedItem.Text, "1");
        }

        protected void BT_RATE_Click(object sender, EventArgs e)
        {
            DGRNULL();
            ShowPopUp("PREMIUM RATE : " + DDL_RATE.SelectedItem.Text, "2");
        }

        protected void BT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            DGRNULL();
            ShowPopUp("ARCHIEVE", "3");
        }

        protected void BT_SAVE_COMPOSITION_Click(object sender, EventArgs e)
        {
            LB_ERR_COMPOSITION.Text = "";

            float PCT = 0;
            for (int i = 0; i < DGR_COMPOSITION.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_COMPOSITION.Items[i].FindControl("TXT_PCT");

                try
                {
                    PCT = PCT + float.Parse(txt.Text.Trim().Replace(",", ""));
                }
                catch { }
            }

            if (PCT != 100)
            {
                LB_ERR_COMPOSITION.Text = "Composition is not equal to 100%";
                FillDGRComposition();
                return;
            }

            conn.QueryString = "delete from TC_MASTER_COMPOSITION where TC_ID = '" + TXT_CODE.Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_COMPOSITION.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_COMPOSITION.Items[i].FindControl("TXT_PCT");

                try
                {
                    conn.QueryString = "exec SP_TC_MASTER_COMPOSITION_UPSERT " +
                                        "'" + TXT_CODE.Text + "'," +
                                        "'" + DGR_COMPOSITION.Items[i].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRComposition();
        }
        
        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Rate")
            {
                DropDownList ddlRATE = (DropDownList)e.Item.FindControl("DDL_RATE");
                ddlRATE.Enabled = true;

                conn.QueryString = "select CODE='',DESCR='' union all select CODE,DESCR from PARAM_PREMIUM_RATE_MASTER order by 2";
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

        protected void FillDGRBenefit()
        {
            conn.QueryString = "exec SP_TC_MASTER_BENEFIT " +
                                    "'" + TXT_CODE.Text + "'," +
                                    "'" + DDL_NBRN.SelectedValue +"'," +
                                    "'" + DDL_UWCODE.SelectedValue +"'"; 
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            Connection connRATE = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            connRATE.QueryString = "select CODE='',DESCR='' union all select CODE,DESCR from PARAM_PREMIUM_RATE_MASTER order by 2";
            connRATE.ExecuteQuery();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DropDownList ddlRATE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_RATE");

                for (int j = 0; j < connRATE.GetRowCount(); j++)
                    ddlRATE.Items.Add(new ListItem(connRATE.GetFieldValue(j, 1).ToString(), connRATE.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlRATE.SelectedValue = DGR_BENEFIT.Items[i].Cells[2].Text;
                }
                catch { }

                if (DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "") == "")
                    DGR_BENEFIT.Items[i].Cells[3].Text = DDL_NBRN.SelectedValue;

                if (DGR_BENEFIT.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "")
                    DGR_BENEFIT.Items[i].Cells[4].Text = DDL_UWCODE.SelectedValue;
            }
        }

        protected void BT_SAVE_BENEFITS_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DropDownList ddlRATE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_RATE");

                conn.QueryString = "exec SP_TC_MASTER_BENEFIT_UPSERT " +
                                    "'" + TXT_CODE.Text + "', " + 
                                    "'" + DGR_BENEFIT.Items[i].Cells[0].Text +"'," +
                                    "'" + DDL_NBRN.SelectedValue+ "', " +
                                    "'" + DDL_UWCODE.SelectedValue + "', " + 
                                    "'" + ddlRATE.SelectedValue +"'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            FillDGRBenefit();
        }

        protected void DDL_NBRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRBenefit();
        }

        protected void DDL_UWCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRBenefit();
        }

        private void GetPaymentFreq() 
        {
            conn.QueryString = "exec USP_GET_PAYMENT_FREQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            ddPaymentFreq.DataSource = dt;
            ddPaymentFreq.DataTextField = "DESCR";
            ddPaymentFreq.DataValueField = "CODE";
            ddPaymentFreq.DataBind();
                                    
        }

        protected void ddPaymentFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddPaymentFreq.SelectedValue == "1")
            {
                FillDDResiko();
                ddPenurunanResiko.Enabled = true;
                btnViewRisk.Visible = true;
            }
            else 
            {
                FillDDResiko();
                ddPenurunanResiko.Enabled = false;
                btnViewRisk.Visible = false;
            }
            
        }

        private void FillDDResiko() 
        {
            conn.QueryString = "SELECT '0' AS CODE, '' AS DESCR UNION ALL select CODE, DESCR from PARAM_RESIKO_MASTER";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            ddPenurunanResiko.DataSource = dt;
            ddPenurunanResiko.DataTextField = "DESCR";
            ddPenurunanResiko.DataValueField = "CODE";
            ddPenurunanResiko.DataBind();
        }

        protected void btnViewRisk_Click(object sender, EventArgs e)
        {
            DGRNULL();
            ShowPopUp("PENURUNAN RESIKO", "4");
        }

        private void GetDataYear(string code, string flag)
        {
            string _query = "USP_GET_RESIKO_TAHUNAN_PVT '" + @code + "', '" + flag + "'";
            conn.QueryString = _query;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.Visible = true;
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        private void GetDataMonth(string code, string flag)
        {
            string _query = "USP_GET_RESIKO_BULANAN_PVT '" + @code + "', '" + flag + "'";
            conn.QueryString = _query;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR2.Visible = true;
            DGR2.DataSource = dt;
            DGR2.DataBind();
        }

        private void DGRNULL() 
        {
            lblStatus.Visible = false;
            lblStatus2.Visible = false;
            DGR.DataSource = null;
            DGR.DataBind();
            DGR2.DataSource = null;
            DGR2.DataBind();

        }

        private string GetIDTC(string desc) 
        {
            string idTC = string.Empty;
            conn.QueryString = "select ID from TC_MASTER where DESCR = '" + desc + "'";
            conn.ExecuteQuery();
            idTC = conn.GetFieldValue("ID").ToString();

            return idTC;
        }


        //private void SaveTCResiko(string desc, string CodeRisk, int codePayment) 
        //{
        //    try
        //    {
        //        string idTC = string.Empty;
        //        conn.QueryString = "select ID from TC_MASTER where DESCR = '" + desc + "'";
        //        conn.ExecuteQuery();
        //        idTC = conn.GetFieldValue("ID").ToString();
                
        //        string _query = "USP_INSERT_TC_RESIKO '" + idTC + "', '" + CodeRisk + "', '" + @codePayment + "'";
        //        conn.QueryString = _query;
        //        conn.ExecuteQuery();
        //    }
        //    catch (Exception ex) 
        //    {
        //        ex.Message.ToString();
        //    }
        //}

        private void SaveTCResiko(string idTC, string CodeRisk, int codePayment)
        {
            try
            {
                string _query = "USP_INSERT_TC_RESIKO '" + idTC + "', '" + CodeRisk + "', '" + codePayment + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        

        private void DeleteTCMasterResiko(string idTc)
        {
            try
            {
                string _query = "USP_DELETE_TC_MASTER_RESIKO '" + idTc;
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }
    }
}
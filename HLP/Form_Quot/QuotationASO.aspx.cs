using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationASO : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text = Request.QueryString["code"];
                LB_VER.Text = Request.QueryString["ver"];
                Setup();
                FillDGR();
                FillDGRFactor();
                FillTPA();
                FillDGRClaim();
                FillDGRBenefitRefresh();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
                TXT_EXCESS.Enabled = false;
                TXT_EXCESSMIN.Enabled = false;
                DDL_TPA.Enabled = false;
                TXT_CHG0.Enabled = false;
                TXT_CHG1.Enabled = false;
                TXT_CHG2.Enabled = false;
            }

            conn.QueryString = "select CODE,DESCR from PR_PREMIUM_FACTOR";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FACTOR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select TPA_CODE,DESCR from PARAM_TPA order by 1";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TPA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_ASO_PREMIUM " +
                                "'" + LB_QUOTNO.Text + "'," +
                                LB_VER.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtMin = (TextBox)DGR.Items[i].FindControl("TXT_MIN");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txt.Enabled = false;
                    txtMin.Enabled = false;
                }

                txt.Text = DGR.Items[i].Cells[2].Text;
                txtMin.Text = DGR.Items[i].Cells[3].Text;
            }

            TXT_EXCESS.Text = "0";
            TXT_EXCESSMIN.Text = "25";
            conn.QueryString = "select " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,isnull(AMOUNT,0)),1), '.00',''), " +
                                "PCT_MIN = convert(varchar(10), isnull(PCT_MIN,25)) " +
                                "from QUOTATION_VERSION_ASO_EXCESS " +
                                "where " +
                                "QUOTNO='" + LB_QUOTNO.Text + "' " +
                                "and VERNO=" + LB_VER.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_EXCESS.Text = conn.GetFieldValue("AMOUNT").ToString();
                TXT_EXCESSMIN.Text = conn.GetFieldValue("PCT_MIN").ToString();
            }
        }

        protected void FillDGRFactor()
        {
            conn.QueryString = "select " +
                                "BENEFIT_ID = a.CODE, " +
                                "a.DESCR, " +
                                "VAL = isnull(b.VAL,1)*100  " +
                                "from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ c on c.CODE=a.CODE  " +
                                "left join QUOTATION_VERSION_BENEFIT_FACTOR b on a.CODE=b.BENEFIT_ID and b.QUOTNO='" + LB_QUOTNO.Text + "' and b.VERNO='" +LB_VER.Text+ "' and b.FACTOR_CODE='" + DDL_FACTOR.SelectedValue + "' " +
                                "order by " +
                                "c.SEQ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FACTOR.DataSource = dt;
            DGR_FACTOR.DataBind();

            for (int i = 0; i < DGR_FACTOR.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_FACTOR.Items[i].FindControl("TXT_FACTOR0");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtVal.Enabled = false;
                }

                txtVal.Text = DGR_FACTOR.Items[i].Cells[2].Text;
            }
        }

        protected void FillDGRClaim()
        {
            conn.QueryString = "select " +
                                "BENEFIT_ID = a.CODE, " +
                                "a.DESCR, " +
                                "VAL = isnull(b.VAL,100)  " +
                                "from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ c on c.CODE=a.CODE  " +
                                "left join QUOTATION_VERSION_BENEFIT_PCT_CLAIM b on a.CODE=b.BENEFIT_ID and b.QUOTNO='" + LB_QUOTNO.Text + "' and b.VERNO='" + LB_VER.Text + "' " +
                                "order by " +
                                "c.SEQ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENCLAIM.DataSource = dt;
            DGR_BENCLAIM.DataBind();

            for (int i = 0; i < DGR_BENCLAIM.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_BENCLAIM.Items[i].FindControl("TXT_CLAIM");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtVal.Enabled = false;
                }

                txtVal.Text = DGR_BENCLAIM.Items[i].Cells[2].Text;
            }
        }

        protected void FillDGRBenefitRefresh()
        {
            conn.QueryString = "select " +
                                "BENEFIT_ID = a.CODE, " +
                                "a.DESCR, " +
                                "DAY = isnull(b.DAY,0) " +
                                "from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ c on c.CODE=a.CODE  " +
                                "left join QUOTATION_VERSION_BENEFIT_REFRESH_DAY b on a.CODE=b.BENEFIT_ID and b.QUOTNO='" + LB_QUOTNO.Text + "' and b.VERNO='" + LB_VER.Text + "' " +
                                "order by " +
                                "c.SEQ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENREFRESH.DataSource = dt;
            DGR_BENREFRESH.DataBind();

            for (int i = 0; i < DGR_BENREFRESH.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_BENREFRESH.Items[i].FindControl("TXT_DAY");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtVal.Enabled = false;
                }

                txtVal.Text = DGR_BENREFRESH.Items[i].Cells[2].Text;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            SaveDGRFactor();
            SaveTPA();
            SaveDGRClaim();
            SaveDGRBenefitRefresh();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                TextBox txtMin = (TextBox)DGR.Items[i].FindControl("TXT_MIN");

                try
                {
                    conn.QueryString = "exec SP_QUOTATION_VERSION_ASO_PREMIUM_UPSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        LB_VER.Text + "," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        txt.Text.Replace(",","") + "," +
                                        txtMin.Text.Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }

            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_ASO_EXCESS_UPSERT " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    TXT_EXCESS.Text.Replace(",", "") + "," +
                                    TXT_EXCESSMIN.Text.Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = "<BR>" + ex.Message;
            }

            FillDGR();
            FillDGRFactor();
            FillTPA();
            FillDGRClaim();
            FillDGRBenefitRefresh();
        }

        protected void DDL_FACTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRFactor();
        }

        protected void SaveTPA()
        {
            try
            {
                conn.QueryString = "update QUOTATION_VERSION_TPA set " +
                                    "TPA_CODE = '" + DDL_TPA.SelectedValue + "', " +
                                    "BASIC_CHG = " + TXT_CHG0.Text.Trim().Replace(",", "") + ", " +
                                    "EXT1_CHG = " + TXT_CHG1.Text.Trim().Replace(",", "") + ", " +
                                    "EXT2_CHG = " + TXT_CHG2.Text.Trim().Replace(",", "") + " " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = " + LB_VER.Text;
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void SaveDGRFactor()
        {
            for (int i = 0; i < DGR_FACTOR.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_FACTOR.Items[i].FindControl("TXT_FACTOR0");
                try
                {
                    conn.QueryString = "update QUOTATION_VERSION_BENEFIT_FACTOR set " +
                                        "VAL = convert(float," + txtVal.Text.Trim() + ")/100.00, " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and VERNO = '" + LB_VER.Text + "' " +
                                        "and BENEFIT_ID = '" + DGR_FACTOR.Items[i].Cells[0].Text + "' " +
                                        "and FACTOR_CODE = '" + DDL_FACTOR.SelectedValue + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = "<BR>" + ex.Message;
                }
            }

            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_RECALC " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = "<BR>" + ex.Message;
            }
        }

        protected void SaveDGRClaim()
        {
            for (int i = 0; i < DGR_BENCLAIM.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_BENCLAIM.Items[i].FindControl("TXT_CLAIM");
                try
                {
                    conn.QueryString = "update QUOTATION_VERSION_BENEFIT_PCT_CLAIM set " +
                                        "VAL = " + txtVal.Text.Trim().Replace(",","") + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and VERNO = '" + LB_VER.Text + "' " +
                                        "and BENEFIT_ID = '" + DGR_BENCLAIM.Items[i].Cells[0].Text + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = "<BR>" + ex.Message;
                }
            }
        }

        protected void SaveDGRBenefitRefresh()
        {
            for (int i = 0; i < DGR_BENREFRESH.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR_BENREFRESH.Items[i].FindControl("TXT_DAY");
                try
                {
                    conn.QueryString = "update QUOTATION_VERSION_BENEFIT_REFRESH_DAY set " +
                                        "DAY = " + txtVal.Text.Trim().Replace(",", "") + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and VERNO = '" + LB_VER.Text + "' " +
                                        "and BENEFIT_ID = '" + DGR_BENREFRESH.Items[i].Cells[0].Text + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = "<BR>" + ex.Message;
                }
            }
        }

        protected void DDL_TPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.QueryString = "select " +
                                "BASIC_CHG = replace(convert(varchar(100),convert(money,isnull(BASIC_CHG,0)),1),'.00',''), " +
                                "EXT1_CHG = replace(convert(varchar(100),convert(money,isnull(EXT1_CHG,0)),1),'.00',''), " +
                                "EXT2_CHG = replace(convert(varchar(100),convert(money,isnull(EXT2_CHG,0)),1),'.00','') " +
                                "from PARAM_TPA  " +
                                "where TPA_CODE = '" + DDL_TPA.SelectedValue + "'";
            conn.ExecuteQuery();
            TXT_CHG0.Text = conn.GetFieldValue("BASIC_CHG").ToString();
            TXT_CHG1.Text = conn.GetFieldValue("EXT1_CHG").ToString();
            TXT_CHG2.Text = conn.GetFieldValue("EXT2_CHG").ToString();
        }

        protected void FillTPA()
        {
            conn.QueryString = "select " +
                                "TPA_CODE, " +
                                "BASIC_CHG = replace(convert(varchar(100),convert(money,isnull(BASIC_CHG,0)),1),'.00',''), " +
                                "EXT1_CHG = replace(convert(varchar(100),convert(money,isnull(EXT1_CHG,0)),1),'.00',''), " +
                                "EXT2_CHG = replace(convert(varchar(100),convert(money,isnull(EXT2_CHG,0)),1),'.00','') " +
                                "from QUOTATION_VERSION_TPA  " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + LB_VER.Text;
            conn.ExecuteQuery();

            try
            {
                DDL_TPA.SelectedValue = conn.GetFieldValue("TPA_CODE").ToString();
            }
            catch { }

            TXT_CHG0.Text = conn.GetFieldValue("BASIC_CHG").ToString();
            TXT_CHG1.Text = conn.GetFieldValue("EXT1_CHG").ToString();
            TXT_CHG2.Text = conn.GetFieldValue("EXT2_CHG").ToString();
        }
    }
}

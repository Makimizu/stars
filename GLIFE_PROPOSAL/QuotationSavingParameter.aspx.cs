using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationSavingParameter : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                LB_QUOTNO.Text = Request.QueryString["quotno"];
                LB_VERNO.Text = Request.QueryString["verno"];

                Setup();
                LoadVersion();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "exec SP_LINK_UW_PR_TENOR_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TENOR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadVersion()
        {
            conn.QueryString = "select " +
                                "START_DATE = convert(varchar(20),START_DATE,103), " +
                                "TENOR_MONTH, " +
                                "MAX_AGE, " +
                                "COI_RATE, " +
                                "ADM_FEE, " +
                                "INV_ANNUAL_RATE, " +
                                "INV_CHARGE_RATE, " +
                                "TENOR_TYPE " +
                                "from QUOTATION_VERSION_SAVING " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + LB_VERNO.Text;
            conn.ExecuteQuery();

            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_ADMFEE.Text = conn.GetFieldValue("ADM_FEE").ToString();
            TXT_COIRATE.Text = conn.GetFieldValue("COI_RATE").ToString();
            TXT_INVCHG.Text = conn.GetFieldValue("INV_CHARGE_RATE").ToString();
            TXT_INVRET.Text = conn.GetFieldValue("INV_ANNUAL_RATE").ToString();
            TXT_MAXAGE.Text = conn.GetFieldValue("MAX_AGE").ToString();
            TXT_TENOR.Text = conn.GetFieldValue("TENOR_MONTH").ToString();

            try
            {
                DDL_TENOR.SelectedValue = conn.GetFieldValue("TENOR_TYPE").ToString();
            }
            catch { }


            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_LOADING " +
                                "'" + LB_QUOTNO.Text + "'," +
                                LB_VERNO.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LOADING.DataSource = dt;
            DGR_LOADING.DataBind();

            for (int i = 0; i < DGR_LOADING.Items.Count; i++)
            {
                TextBox txtCLUN = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_CLUNRET");
                TextBox txtCREG = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_CREGRET");
                TextBox txtTOP = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_TOPRET");

                txtCLUN.Text = DGR_LOADING.Items[i].Cells[1].Text;
                txtCREG.Text = DGR_LOADING.Items[i].Cells[2].Text;
                txtTOP.Text = DGR_LOADING.Items[i].Cells[3].Text;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_UPSERT " +
                                "'" + LB_QUOTNO.Text + "'," +
                                LB_VERNO.Text + "," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text, "d/M/yyyy") + "'," +
                                TXT_TENOR.Text.Trim().Replace(",", "") + "," +
                                TXT_MAXAGE.Text.Trim().Replace(",", "") + "," +
                                TXT_COIRATE.Text.Trim().Replace(",", "") + "," +
                                TXT_ADMFEE.Text.Trim().Replace(",", "") + "," +
                                TXT_INVRET.Text.Trim().Replace(",", "") + "," +
                                TXT_INVCHG.Text.Trim().Replace(",", "") + "," +
                                "'" + DDL_TENOR.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            Setup();
            Task.Run(() => ProcessBatch());
        }

        protected void BT_SAVE_LOADING_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_LOADING.Items.Count; i++)
            {
                TextBox txtCLUN = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_CLUNRET");
                TextBox txtCREG = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_CREGRET");
                TextBox txtTOP = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_TOPRET");

                conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_LOADING_UPSERT " +
                                "'" + LB_QUOTNO.Text + "'," +
                                LB_VERNO.Text + "," +
                                DGR_LOADING.Items[i].Cells[0].Text + "," +
                                txtCLUN.Text.Trim().Replace(",", "") + "," +
                                txtCREG.Text.Trim().Replace(",", "") + "," +
                                txtTOP.Text.Trim().Replace(",", "") + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            Setup();
            Task.Run(() => ProcessBatch());
        }

        protected void ProcessBatch()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_MEMBER_TRX '" + LB_QUOTNO.Text + "'," + LB_VERNO.Text;
            conn.ExecuteNonQuery();
        }
    }
}
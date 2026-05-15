using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class RK_Refund : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LB_CODE.Text = Request.QueryString["CODE"];
                Setup();
            }
        }

        protected void FillDGRInfo()
        {
            conn.QueryString = "exec SP_INVOICE_RK " +
                                "'RK'," +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO.DataSource = dt;
            DGR_INFO.DataBind();
        }

        protected void Setup()
        {
            FillDGRInfo();

            BT_SUBMIT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk REFUND ?')){return false;};");

            conn.QueryString = "select CODE,BANK from PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            if (TXT_ACCNO.Text.Trim() == "")
            {
                LB_ERROR.Text = "ACC NO is Empty";
                return;
            }

            if (TXT_ACCNAME.Text.Trim() == "")
            {
                LB_ERROR.Text = "ACC NAME is Empty";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_REKENING_JURNAL_REFUND_INSERT " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + TXT_ACCNO.Text.Trim() + "'," +
                                    "'" + TXT_ACCNAME.Text.Trim() + "'," +
                                    "'" + DDL_ACCBANK.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("RK.aspx");
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }
    }
}
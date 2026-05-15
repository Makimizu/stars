using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Note_Manual : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["INVOICENO"] != null)
                    TXT_INVOICENO.Text = Request.QueryString["INVOICENO"];
                if (Request.QueryString["NOTENO"] != null)
                    TXT_NOTENO.Text = Request.QueryString["NOTENO"];

                Setup();
                LoadRecord();
                ShowDDLReason();    
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select b.CODE, b.APP_NAME from PARAM_NOTA_TYPE a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default " +
                                "where a.TIPE_NOTA = '999' order by 2";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                        
        }

        protected void ShowDDLReason()
        {
            DDL_REASON.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_NOTA_REASON " +
                                "where " +
                                "DC = '" + DDL_DC.SelectedValue + "' " +
                                "and APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                "order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REASON.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord()
        {
            if (TXT_NOTENO.Text == "")
            {
                conn.QueryString = "select APP_ID,OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), CUSTOMER_NAME from V_INVOICE_MASTER where INVOICENO = '" + TXT_INVOICENO.Text + "'";
                conn.ExecuteQuery();

                DDL_APP.SelectedValue = conn.GetFieldValue("APP_ID").ToString();
                LB_OUTSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
                LB_CUSTOMER.Text = conn.GetFieldValue("CUSTOMER_NAME").ToString();

            }
            else
            {
                conn.QueryString = "select " +
                                    "NOTA_NO, " +
                                    "INVOICENO, " +
                                    "NOTA_DESCR, " +
                                    "APP_ID, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                    "DC, " +
                                    "NOTA_DATE = convert(varchar(20),NOTA_DATE,106) " +
                                    "from INVOICE_NOTA  " +
                                    "where NOTA_NO='" + TXT_NOTENO.Text + "'";
                conn.ExecuteQuery();

                DDL_APP.SelectedValue = conn.GetFieldValue("APP_ID").ToString();
                DDL_DC.SelectedValue = conn.GetFieldValue("DC").ToString();
                TXT_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();
                TXT_DATE.Text = conn.GetFieldValue("NOTA_DATE").ToString();
                TXT_DESCR.Text = conn.GetFieldValue("NOTA_DESCR").ToString();
                TXT_INVOICENO.Text = conn.GetFieldValue("INVOICENO").ToString();

                conn.QueryString = "select APP_ID,OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), CUSTOMER_NAME from V_INVOICE_MASTER where INVOICENO = '" + TXT_INVOICENO.Text + "'";
                conn.ExecuteQuery();

                DDL_APP.SelectedValue = conn.GetFieldValue("APP_ID").ToString();
                LB_OUTSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
                LB_CUSTOMER.Text = conn.GetFieldValue("CUSTOMER_NAME").ToString();
            }

            BT_HST.Attributes.Add("onclick", "window.open('Invoice_History.aspx?INVOICENO=" + TXT_INVOICENO.Text + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_INVOICE_NOTA_MANUAL_UPSERT " +
                                    "'" + TXT_NOTENO.Text + "'," +
                                    "'" + TXT_INVOICENO.Text + "'," +
                                    "'" + TXT_DESCR.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + DDL_REASON.SelectedValue + "'," +
                                    "'" + TXT_AMOUNT.Text.Replace(",", "") + "'," +
                                    "'" + DDL_DC.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                string noteno = conn.GetFieldValue("NOTA_NO").ToString();
                Response.Redirect("Note_Manual.aspx?NOTENO=" + noteno);

            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DDL_DC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDDLReason();
        }
    }
}
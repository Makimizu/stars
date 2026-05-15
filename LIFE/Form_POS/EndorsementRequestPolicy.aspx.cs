using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementRequestPolicy : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                LoadRecord();
            }
        }

        protected void LoadRecord()
        {
            DDL_PROVINCE.SelectedIndex = 0;

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REQUEST_POLICY " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS.Text = conn.GetFieldValue("ADDRESS").ToString().Replace("'", "`");
            TXT_CHARGE.Text = conn.GetFieldValue("CHARGE").ToString().Replace("'", "`");
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString().Replace("'", "`");
            TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString().Replace("'", "`");
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIPCODE").ToString().Replace("'", "`");

            try
            {
                DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
            }
            catch { }

            try
            {
                DDL_TYPE.SelectedValue = conn.GetFieldValue("POLICY_TYPE").ToString();
            }
            catch { }

            if (TXT_CHARGE.Text.Trim() == "")
            {
                SetCharge();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "select CODE, DESCR from CLIENT_BASE.dbo.PR_PROPINSI";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR = replace(DESCR, 'BIAYA CETAK ULANG POLIS : ', '') from UWBOX.dbo.PR_CHARGES where CODE in ('RPOL-E','RPOL-EH','RPOL-H') order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCharge();
        }

        protected void SetCharge()
        {
            TXT_CHARGE.Text = "0";

            conn.QueryString = "select " +
                                "AMOUNT			= replace(convert(varchar(100), convert(money, isnull(a.AMOUNT, 0)), 1), '.00', '') " +
                                "from			APPLICATION_MASTER b " +
                                "inner join		UWBOX.dbo.PARAM_PRODUCT_MASTER_CHARGE a on b.PRODUCT_CODE = a.PRODUCT_CODE and a.YEARSEQ = 1 and a.TRANS_TYPE = '" + DDL_TYPE.SelectedValue + "' " +
                                "where " +
                                "b.REGNO	    = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_CHARGE.Text = conn.GetFieldValue("AMOUNT").ToString();
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_REQUEST_POLICY_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + DDL_TYPE.SelectedValue + "'," +
                                    "'" + TXT_CHARGE.Text.Trim() + "'," +
                                    "'" + TXT_REMARK.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + TXT_EMAIL.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + TXT_ADDRESS.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + DDL_PROVINCE.SelectedValue + "'," +
                                    "'" + TXT_ZIPCODE.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            LoadRecord();
        }
    }
}
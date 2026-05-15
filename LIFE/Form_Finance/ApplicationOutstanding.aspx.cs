using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class ApplicationOutstanding : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_TRXID.Text = Request.QueryString["TRXID"].ToString();
                ShowTRXID();
                ShowREGNO();
            }
        }

        protected void ShowTRXID()
        {
            string descr = "(case when a.REGNO is null then DESCR " +
                                "                        else DESCR +  " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + POLICY_NO + ' - ' + VACC + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + FULLNAME + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + PRODUCT_NAME + '</span>' " +
                                "                        end)";

            conn.QueryString = "select " +
                                "ACCNO			= BANK, " +
                                "TRXID			= TRXID, " +
                                "POST_DATE		= convert(varchar(20), POST_DATE, 106), " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, BALANCE), 1), '.00', ''), " +
                                "DESCR			= " + descr + ", " +
                                "REGNO			= REGNO " +
                                "from			V_LINK_FINANCE_REKENING_JURNAL_SUSPEND a " +
                                "where " +
                                "TRXID          = '" + LB_TRXID.Text + "'";
            conn.ExecuteQuery(150000);

            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_BALANCE.Text = conn.GetFieldValue("BALANCE").ToString();
            LB_POSTDATE.Text = conn.GetFieldValue("POST_DATE").ToString();
        }

        protected void ShowREGNO()
        {
            conn.QueryString = "select " +
                                "POLICY_NO	= a.POLICY_NO + ' - ' + a.VACC, " +
                                "FULLNAME	= a.FULLNAME, " +
                                "PRODUCT	= a.PRODUCT_CODE + ' - ' + a.PRODUCT_NAME, " +
                                "IP			= convert(varchar(20), a.START_DATE, 106) + ' - ' + convert(varchar(20), a.END_DATE, 106), " +
                                "PP			= convert(varchar(20), a.START_PAYMENT_DATE, 106) + ' - ' + convert(varchar(20), a.END_PAYMENT_DATE, 106), " +
                                "FOP        = a.FOP_DESCR " +
                                "from		V_APPLICATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery(150000);

            LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("PRODUCT").ToString();
            LB_IP.Text = conn.GetFieldValue("IP").ToString();
            LB_PP.Text = conn.GetFieldValue("PP").ToString();
            LB_FOP.Text = conn.GetFieldValue("FOP").ToString();

            FillDGRRegno();
        }


        protected void FillDGRRegno()
        {
            conn.QueryString = "exec SP_APPLICATION_PAYMENT_CYCLE_OUTSTANDING '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REGNO.DataSource = dt;
            DGR_REGNO.DataBind();

            for (int i = 0; i < DGR_REGNO.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_REGNO.Items[i].FindControl("CB");
                if (DGR_REGNO.Items[i].Cells[2].Text == "0")
                    cb.Enabled = false;
            }

            if (LB_TRXID.Text == "")
                DGR_REGNO.Columns[DGR_REGNO.Columns.Count - 1].Visible = false;
        }

        protected void DGR_REGNO_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                LB_ERR.Text = "";

                for (int i = 0; i < DGR_REGNO.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_REGNO.Items[i].FindControl("CB");
                    if (!cb.Checked)
                        continue;

                    try
                    {
                        conn.QueryString = "exec SP_LINK_FINANCE_INVOICE_SETTLE " +
                                            "'" + DGR_REGNO.Items[i].Cells[0].Text + "'," +
                                            "'" + LB_TRXID.Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();

                        string endorsmnetType = "";
                        string seq = "";

                        // escape dulu biar aman dari tanda '
                        string regno = DGR_REGNO.Items[i].Cells[0].Text.Split('-')[0];

                        conn.QueryString =
                            "SELECT TOP 1 " +
                            "c.ENDORSEMENT_TYPE, " +
                            "a.PARAM_VALUE " +
                            "FROM APPLICATION_TRACK a " +
                            "INNER JOIN V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                            "INNER JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                            "INNER JOIN APPLICATION_ENDORSEMENT_MASTER c ON a.REGNO=b.REGNO " +
                            "WHERE pg.UNITIZE = 0 and c.ENDORSEMENT_TYPE='MBR13' " +
                            "AND a.REGNO = '" + regno + "' " +
                            "AND a.TRACK_TYPE = 'POS' " +
                            "ORDER BY a.PARAM_VALUE DESC";

                        conn.ExecuteQuery();

                        if (conn.GetRowCount() > 0)
                        {
                            endorsmnetType = conn.GetFieldValue(0, 0);
                            seq = conn.GetFieldValue(0, 1);
                        }

                        // cek ISP/ Top Up Kusus
                        if ((endorsmnetType ?? "") == "MBR13" && cb.Checked)
                        {
                            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID").Replace("'", "''");
                            string trxid = LB_TRXID.Text.Replace("'", "''");

                            conn.QueryString =
                                "exec SP_APPLICATION_ENDORSEMENT_NOTIFICATION_EMAIL " +
                                "'" + regno + "'," +
                                "'" + seq + "'," +
                                "'" + user + "'";

                            conn.ExecuteNonQuery();
                        }

                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + ex.Message;
                    }
                }

                //FillDGRRegno();
                Response.Redirect("ApplicationOutstanding.aspx?REGNO=" + LB_REGNO.Text + "&TRXID=" + LB_TRXID.Text);
            }
        }
    }
}
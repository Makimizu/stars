using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationMCUInvoice : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_BATCHID.Text = Request.QueryString["batchid"].ToString();
                Setup();
                if (LB_BATCHID.Text != "")
                {
                    TR_DETAIL.Visible = true;
                    bDone = TrackDone();
                    LoadInvoice();
                    LoadPaymentAcc();
                    LoadDGRUnselected();
                    LoadDGRSelected();
                    LoadTrack();
                    LoadArchieve();
                }
            }
        }

        protected void LoadArchieve()
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_05", LB_BATCHID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            IF.Src = URL;
        }

        protected void LoadPaymentAcc()
        {
            string disable = "0";
            if (bDone)
            {
                disable = "1";
            }
            IF1.Visible = true;
            IF1.Src = "../Form_Tools/PaymentAcc.aspx?REGNO=" + LB_BATCHID.Text + "&SEQ=1&CODE=MCU&DISABLE=" + disable;
        }

        protected void LoadTrack()
        {
            if (bDone)
            {
                BT_APPROVE.Visible = false;
                IF2.Visible = true;
                IF2.Src = "../Form_Tools/Track.aspx?tipe=MCU&owner=" + LB_BATCHID.Text;
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_MEDICAL_LAB_INVOICE where BATCH_ID = '" + LB_BATCHID.Text + "' and LAST_TRACK in (2)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void Setup()
        {
            BT_APPROVE.Attributes.Add("onclick", "if(!confirm('Are you sure to APPROVE ?')){return false;};");

            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME = UPPER(COMPANY_NAME) " +
                                "from V_LINK_UB_PARAM_MEDICAL_LAB " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LAB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void LoadInvoice()
        {
            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money, AMOUNT),1), '.00',''), " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,103), " +
                                "COMPANY_CODE " +
                                "from V_MEDICAL_LAB_INVOICE " +
                                "where " +
                                "BATCH_ID = '" + LB_BATCHID.Text + "'";
            conn.ExecuteQuery();

            TXT_INVOICENO.Text = conn.GetFieldValue("INVOICENO").ToString();
            TXT_INVOICEDATE.Text = conn.GetFieldValue("INVOICE_DATE").ToString();
            TXT_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();

            try
            {
                DDL_LAB.SelectedValue = conn.GetFieldValue("COMPANY_CODE").ToString();
            }
            catch { }

            if (bDone)
            {
                BT_SAVE.Visible = false;
                TXT_AMOUNT.ReadOnly = true;
                TXT_INVOICEDATE.ReadOnly = true;
                TXT_INVOICENO.ReadOnly = true;
                DDL_LAB.Enabled = false;
            }
        }

        protected void LoadDGRUnselected()
        {
            if (bDone)
            {
                TBL_UNSELECTED.Visible = false;
                return;
            }

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "aa.FULLNAME, " +
                                "aa.COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20), aa.START_DATE, 106), " +
                                "CHARGE = replace(convert(varchar(100),convert(money,SUM(a.PRICE)),1),'.00','') " +
                                "from V_APPLICATION_MEDICAL_LAB_ITEMS a " +
                                "inner join V_APPLICATION_MASTER aa on a.REGNO = aa.REGNO " +
                                "left join ( select " +
                                "            a.REGNO, " +
                                "            b.COMPANY_CODE " +
                                "            from MEDICAL_LAB_INVOICE_DETAIL a " +
                                "            inner join MEDICAL_LAB_INVOICE b on a.BATCH_ID = b.BATCH_ID " +
                                "            ) b on a.REGNO = b.REGNO and a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where  " +
                                "b.REGNO is null " +
                                "and a.COMPANY_CODE = '" + DDL_LAB.SelectedValue + "' " +
                                "group by " +
                                "a.REGNO, " +
                                "a.COMPANY_CODE, " +
                                "aa.FULLNAME, " +
                                "aa.START_DATE, " +
                                "aa.COMPANY_NAME " +
                                "order by aa.FULLNAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REGNO.DataSource = dt;
            DGR_REGNO.DataBind();

            for (int i = 0; i < DGR_REGNO.Items.Count; i++)
            {
                LinkButton lbtDESCR = (LinkButton)DGR_REGNO.Items[i].FindControl("LBT_DESCR");
                lbtDESCR.Text = DGR_REGNO.Items[i].Cells[2].Text;
            }
        }

        protected void LoadDGRSelected()
        {
            conn.QueryString = "select  " +
                                "a.REGNO, " +
                                "aa.FULLNAME, " +
                                "aa.COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20), aa.START_DATE, 106), " +
                                "CHARGE  = replace(convert(varchar(100),convert(money,c.CHARGE),1),'.00','')  " +
                                "from MEDICAL_LAB_INVOICE_DETAIL a " +
                                "inner join V_APPLICATION_MASTER aa on a.REGNO = aa.REGNO " +
                                "inner join MEDICAL_LAB_INVOICE b on a.BATCH_ID = b.BATCH_ID " +
                                "inner join (select " +
                                "            REGNO, " +
                                "            COMPANY_CODE, " +
                                "            CHARGE = SUM(PRICE) " +
                                "            from V_APPLICATION_MEDICAL_LAB_ITEMS " +
                                "            group by " +
                                "            REGNO, " +
                                "            COMPANY_CODE " +
                                "            ) c on a.REGNO = c.REGNO and b.COMPANY_CODE = c.COMPANY_CODE " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCHID.Text + "' " +
                                "order by aa.FULLNAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REGNOSELECTED.DataSource = dt;
            DGR_REGNOSELECTED.DataBind();

            for (int i = 0; i < DGR_REGNOSELECTED.Items.Count; i++)
            {
                LinkButton lbtDESCR = (LinkButton)DGR_REGNOSELECTED.Items[i].FindControl("LBT_DESCR2");
                lbtDESCR.Text = DGR_REGNOSELECTED.Items[i].Cells[2].Text;
            }

            if (bDone)
            {
                DGR_REGNOSELECTED.Columns[DGR_REGNOSELECTED.Columns.Count - 1].Visible = false;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_MEDICAL_LAB_INVOICE_UPSERT " +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + TXT_INVOICENO.Text.Trim() + "'," +
                                    "'" + DDL_LAB.SelectedValue + "'," +
                                    "'" + TXT_AMOUNT.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_INVOICEDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                Response.Redirect("ApplicationMCUInvoice.aspx?batchid=" + conn.GetFieldValue("BATCH_ID").ToString());
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DGR_REGNO_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "insert into MEDICAL_LAB_INVOICE_DETAIL select " +
                                        "'" + LB_BATCHID.Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    LoadDGRUnselected();
                    LoadDGRSelected();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }

        protected void DGR_REGNOSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationMCU.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "delete from MEDICAL_LAB_INVOICE_DETAIL where " +
                                        "BATCH_ID = '" + LB_BATCHID.Text + "' " +
                                        "and REGNO = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    LoadDGRUnselected();
                    LoadDGRSelected();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_MEDICAL_LAB_INVOICE_APV_VALIDATION " +
                                    "'" + LB_BATCHID.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    LB_ERROR.Text = "<BR>UNCOMPLETED ITEMS :<table>";
                    for (int i = 0; i < conn.GetRowCount(); i++)
                    {
                        LB_ERROR.Text = LB_ERROR.Text +
                                        "<tr><td style='width: 20px;'>" + conn.GetFieldValue(i, 0).ToString() + ".</td><td>" + conn.GetFieldValue(i, 1).ToString().ToUpper() + "</td></tr>";
                    }
                    LB_ERROR.Text = LB_ERROR.Text + "</table>";
                    return;
                }

                conn.QueryString = "exec SP_MEDICAL_LAB_INVOICE_APPROVE " +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("AppMCUInvoiceList.aspx");
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }
    }
}